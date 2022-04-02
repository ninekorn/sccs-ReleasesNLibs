using System;
using System.Collections.Generic;
using System.Text;

//using(console);
//include(AIPathFindUtilities.js);
//include(SC_Utilities.js);
using System.Runtime.InteropServices;
using SharpDX;
namespace _sc_core_systems.SC_pathfind_assets
{
    public class sc_pathfind_grid
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);


        float _min_geometry = 9.31415926535f;
        float _max_geometry = 10.31415926535f;

        public struct sc_node_data
        {
            public List<sc_node> openSet;// = new List<node>();
            public sc_node[] grid;
        }


        //var stationTilesArray = [];
        //var initialPathfindStartingPos = [];
        //var initialPathfindTargetPos = [];
        public List<sc_node> openSet = new List<sc_node>();
        public sc_node[] grid;
        float switchForStationTiles = 0;
        public List<Vector3> initialPathfindStartingPos = new List<Vector3>();
        int lastPlayerID;
        int indexOfPlayer;
        int globIndex;
        Vector3 targetPos;
        Vector3 initialPos;
        Vector3 startPos;


















        sc_grid_size _gridWorldSize;

        sc_node_data _sc_node_data = new sc_node_data();
        int _max = 0;
        public sc_node_data sc_init_pathfind_grid(int _size_x_left, int _size_x_right, int _size_y_left, int _size_y_right, int _size_z_left, int _size_z_right)
        {

            _gridWorldSize = new sc_grid_size();
            _gridWorldSize._size_x_left = _size_x_left;
            _gridWorldSize._size_x_right = _size_x_right;
            _gridWorldSize._size_y_left = _size_y_left;
            _gridWorldSize._size_y_right = _size_y_right;
            _gridWorldSize._size_z_left = _size_z_left;
            _gridWorldSize._size_z_right = _size_z_right;




        //_sc_node_data.openSet = new List<node>();
        //_sc_node_data.grid = grid;



            initialPathfindStartingPos = new List<Vector3>();

            startPos = Vector3.Zero;

            sc_curData _cdata = new sc_curData();
            _cdata.glip = startPos;
            _cdata.lsp = startPos;
            _cdata.fSwtc = 1;
            _cdata.sSwtc = 1;
            _cdata.pfc = -2;
            _cdata.stopS = 1;
            _cdata.stopSC = 0;
            _cdata.stopSCM = 5;
















            _max = ((_gridWorldSize._size_x_left + _gridWorldSize._size_x_right + 1) * (_gridWorldSize._size_y_left + _gridWorldSize._size_y_right + 1) * (_gridWorldSize._size_z_left + _gridWorldSize._size_z_right + 1));
            grid = new sc_node[_max];
            _cdata.data_of_grid = grid;
            _cdata.openset = new List<sc_node>();



            sc_node noder = new sc_node();
            noder.parent_grid_index = 0;
            noder.parent_node_index = 0;
            noder._is_connected = -1;


            _sc_node_data.grid = new sc_node[_max];
            _sc_node_data.openSet = new List<sc_node>();

            _sc_node_data = sc_pathfind_grid_init(_gridWorldSize, 1, noder.parent_grid_index, _cdata, noder);

            //_sc_node_data.openSet = _cdata.openset;// new List<node>();
            //_sc_node_data.grid = _cdata.data_of_grid;//;

            return _sc_node_data;

            //MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
        }




        //node parenter = null;
        int parent_grid_index;
        int parent_node_index;


        sc_node_data sc_pathfind_grid_init(sc_grid_size gridWorldSize, float nodeRadius, int indexOfGrid, sc_curData cData,sc_node noder)//, cData, noder, current_base)
        {
            //parenter = null;

            if (noder._is_connected == -1)
            {
                parent_grid_index = noder.parent_grid_index;
                parent_node_index = noder.parent_node_index;

                noder._is_connected = 1;
            }

            for (var x = -_gridWorldSize._size_x_left; x <= _gridWorldSize._size_x_right; x++)
            {
                for (var y = -_gridWorldSize._size_y_left; y <= _gridWorldSize._size_y_right; y++)
                {
                    for (var z = -_gridWorldSize._size_z_left; z <= _gridWorldSize._size_z_right; z++)
                    {

                        var xx = x;
                        var yy = y;
                        var zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = (_gridWorldSize._size_x_right) + xx;
                        }
                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = (_gridWorldSize._size_y_right) + yy;
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = (_gridWorldSize._size_z_right) + zz;
                        }

                        int boolWalker = 1;

                        float roundedX = (float)((Math.Round(cData.glip.Value.X)) + x);
                        float roundedY = (float)((Math.Round(cData.glip.Value.Y)) + y);
                        float roundedZ = (float)((Math.Round(cData.glip.Value.Z)) + z);

                        var worldPoint = new Vector3(roundedX, roundedY, roundedZ);
                        float gcosty = 0;
                        float hcosty = 0;
                        float fcosty = 0;

                        //var index = xx * (gridWorldSize.xL + gridWorldSize.xR + 1) + yy;
                        int _index = (int)(xx + (_gridWorldSize._size_x_left+ _gridWorldSize._size_x_right + 1) * (yy + (_gridWorldSize._size_y_left + _gridWorldSize._size_y_right + 1) * zz));

                        if (worldPoint.X == startPos.X && worldPoint.Y == startPos.Y && worldPoint.Z == startPos.Z)
                        {
                            //Console.WriteLine("found start Node");
                            float startNodegcoster = 0;
                            //float startNodehcoster = sc_maths.sc_sebastian_lague_check_distance_node_3d_ellipsoid_not_really_ellipsoid(worldPoint, startPos);
                            float startNodehcoster = sc_maths.sc_check_distance_node_3d_geometry(worldPoint, new Vector3(startPos.X, startPos.Y, startPos.Z), _min_geometry, _min_geometry, _min_geometry, _max_geometry, _max_geometry, _max_geometry);
                            float startNodefcoster = startNodegcoster + startNodehcoster;

                            var startNode = new sc_node();
                            startNode.can_walk = boolWalker;
                            startNode.worldPosition = cData.lsp.Value;
                            startNode.gcost = startNodegcoster;
                            startNode.hcost = startNodehcoster;
                            startNode.fcost = startNodefcoster;
                            startNode.gridTileX = x;
                            startNode.gridTileY = y;
                            startNode.gridTileZ = z;
                            startNode.index = _index;
                            startNode.parent_grid_index = parent_grid_index;
                            startNode.parent_node_index = parent_node_index;
                            startNode.gridIndex = indexOfGrid;
                            startNode.gridPos = cData.glip.Value;
                            startNode.opened = 0;
                            startNode.closed = 0;

                            _sc_node_data.grid[_index] = startNode;
                            _sc_node_data.openSet.Add(startNode);
                        }
                        else if (cData.lsp.Value.X == worldPoint.X && cData.lsp.Value.Y == worldPoint.Y)
                        {
                            //gcosty = sc_maths.sc_sebastian_lague_check_distance_node_3d_ellipsoid_not_really_ellipsoid(cData.lsp, worldPoint);
                            gcosty = sc_maths.sc_check_distance_node_3d_geometry(cData.lsp.Value, new Vector3(worldPoint.X, worldPoint.Y, worldPoint.Z), _min_geometry, _min_geometry, _min_geometry, _max_geometry, _max_geometry, _max_geometry);

                            //hcosty = sc_maths.sc_sebastian_lague_check_distance_node_3d_ellipsoid_not_really_ellipsoid(worldPoint, startPos);
                            hcosty = sc_maths.sc_check_distance_node_3d_geometry(worldPoint, new Vector3(startPos.X, startPos.Y, startPos.Z), _min_geometry, _min_geometry, _min_geometry, _max_geometry, _max_geometry, _max_geometry);

                            fcosty = gcosty + hcosty;

                            var startNode = new sc_node();
                            startNode.can_walk = boolWalker;
                            startNode.worldPosition = cData.lsp.Value;
                            startNode.gcost = gcosty;
                            startNode.hcost = hcosty;
                            startNode.fcost = fcosty;
                            startNode.gridTileX = x;
                            startNode.gridTileY = y;
                            startNode.gridTileZ = z;
                            startNode.index = _index;
                            startNode.parent_grid_index = parent_grid_index;
                            startNode.parent_node_index = parent_node_index;
                            startNode.gridIndex = indexOfGrid;
                            startNode.gridPos = cData.glip.Value;
                            startNode.opened = 0;
                            startNode.closed = 0;

                            _sc_node_data.grid[_index] = startNode;
                            _sc_node_data.openSet.Add(startNode);

                        }
                        else
                        {    
                            gcosty = sc_maths.sc_check_distance_node_3d_geometry(cData.lsp.Value, new Vector3(worldPoint.X, worldPoint.Y, worldPoint.Z), _min_geometry, _min_geometry, _min_geometry, _max_geometry, _max_geometry, _max_geometry);
                            hcosty = sc_maths.sc_check_distance_node_3d_geometry(startPos, new Vector3(worldPoint.X, worldPoint.Y, worldPoint.Z), _min_geometry, _min_geometry, _min_geometry, _max_geometry, _max_geometry, _max_geometry);

                            fcosty = gcosty + hcosty;

                            var startNode = new sc_node();
                            startNode.can_walk = boolWalker;
                            startNode.worldPosition = cData.lsp.Value;
                            startNode.gcost = gcosty;
                            startNode.hcost = hcosty;
                            startNode.fcost = fcosty;
                            startNode.gridTileX = x;
                            startNode.gridTileY = y;
                            startNode.gridTileZ = z;
                            startNode.index = _index;
                            startNode.parent_grid_index = parent_grid_index;
                            startNode.parent_node_index = parent_node_index;
                            startNode.gridIndex = indexOfGrid;
                            startNode.gridPos = cData.glip.Value;
                            startNode.opened = 0;
                            startNode.closed = 0;

                            cData.data_of_grid[_index] = startNode;
                            cData.openset.Add(startNode);
                        }
                    }
                }
            }
            return _sc_node_data;
        }
    }
}


















/*if (worldPoint.x === cData.lip.x && worldPoint.y === cData.lip.y) {
    //console.PrintError("found start Node");
    var startNodegcoster = 0;
    var startNodehcoster = AIPathFindUtilities.npcCheckDistance(worldPoint, cData.ltp);
    var startNodefcoster = startNodegcoster + startNodehcoster;
    var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

    grid[index] = startNode;
    openSet.push(startNode);
}
else if (cData.lsp.x === worldPoint.x && cData.lsp.y === worldPoint.y) {
    //console.PrintError("new Grid");
    gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
    hcosty = AIPathFindUtilities.npcCheckDistance(worldPoint, cData.ltp);
    fcosty = gcosty + hcosty;

    var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

    grid[index] = startNode;
    openSet.push(startNode);
}
else {
    gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
    hcosty = AIPathFindUtilities.npcCheckDistance(cData.ltp, worldPoint);
    fcosty = gcosty + hcosty;

    grid[index] = { boolWalk: boolWalker, worldPosition: worldPoint, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };
}



if (worldPoint.x === initialPathfindStartingPos[globIndex].x && worldPoint.y === initialPathfindStartingPos[globIndex].y)
{
    //console.PrintError("found start Node");
    var startNodegcoster = 0;
var startNodehcoster = AIPathFindUtilities.npcCheckDistance(worldPoint, initialPathfindTargetPos[globIndex]);
var startNodefcoster = startNodegcoster + startNodehcoster;
var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: startNodegcoster, hcost: startNodehcoster, fcost: startNodefcoster, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

grid[index] = startNode;
    openSet.push(startNode);
}
else if (cData.lsp.x === worldPoint.x && cData.lsp.y === worldPoint.y)
{
    console.PrintError("new Grid");
    gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
    hcosty = AIPathFindUtilities.npcCheckDistance(worldPoint, initialPathfindTargetPos[globIndex]);
    fcosty = gcosty + hcosty;

    var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: gcosty, hcost: hcosty, fcost: fcosty, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

grid[index] = startNode;
    openSet.push(startNode);
}
else {
    gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
    hcosty = AIPathFindUtilities.npcCheckDistance(initialPathfindTargetPos[globIndex], worldPoint);
    fcosty = gcosty + hcosty;

    grid[index] = { boolWalk: boolWalker, worldPosition: worldPoint, gcost: gcosty, hcost: hcosty, fcost: fcosty, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };
}



        }
    }
}*/
/*// grid.push(null);
var xx = x;
var yy = y;

if (xx < 0)
{
    xx *= -1;
    xx = (gridSizeXR) + xx;
}
if (yy < 0)
{
    yy *= -1;
    yy = (gridSizeYT) + yy;
}

var boolWalker = 1;


var roundedX = (Math.round(cData.glip.x)) + x;
var roundedY = (Math.round(cData.glip.y)) + y;
var rounded = { x: roundedX, y: roundedY };

if (switchForStationTiles == 1)
{
var coords = game.GetObjectCoordinates(cData.objt.sid, stationTilesArray[indexOfPlayer].baseID);

//var remnant = 360 - cData.stationTiles.rot;
//coords= SC_Utilities.RotatePoint(target, cData.stationTiles.coord, remnant);

coords.x = Math.round(coords.x);
coords.y = Math.round(coords.y);

var widthLeft = stationTilesArray[indexOfPlayer].widthL;
var widthRight = stationTilesArray[indexOfPlayer].widthR;

var heightBottom = stationTilesArray[indexOfPlayer].heightB;
var heightTop = stationTilesArray[indexOfPlayer].heightT;

//var testX = coords.x + x;
//var testY = coords.y + y;

var diffX = Math.round(Math.abs(Math.abs(roundedX) - Math.abs(coords.x)));
var diffY = Math.round(Math.abs(Math.abs(roundedY) - Math.abs(coords.y)));

var test = { x: roundedX, y: roundedY };

if (test.x < coords.x)
{
diffX *= -1;
}

if (test.y < coords.y)
{
diffY *= -1;
}

if (test.x >= coords.x - widthLeft && test.x <= coords.x + widthRight && test.y >= coords.y - heightBottom && test.y <= coords.y + heightTop)
{
if (diffX < 0)
{
    diffX = (widthRight) + (diffX * -1);
}

if (diffY < 0)
{
    diffY = (heightTop) + (diffY * -1);
}

var indexer = diffX + (widthLeft + widthRight + 1) * diffY;

if (stationTilesArray[indexOfPlayer].grid[indexer] == 0)
{
    boolWalker = 1;
}
else
{
    /*if (stationTiles.visualTiles[indexer] == 1)
    {
        var idObj = generator.AddSpecialObject(cData.objt.sid, roundedX, roundedY, "stopSign_02", 0);

        if (storage.IsSetGlobal("crates_Stop" + cData.objt.sid))
        {
            var crates = storage.GetGlobal("crates_Stop" + cData.objt.sid);
            crates.push({ id: idObj, staID: stationTiles.baseID });
            storage.SetGlobal("crates_Stop" + cData.objt.sid, crates);
        }
        else
        {
            var crates = [];
            crates.push({ id: idObj, staID: stationTiles.baseID });
            storage.SetGlobal("crates_Stop" + cData.objt.sid, crates);
        }
        stationTiles.visualTiles[indexer] = 2;
    }

    boolWalker = 0;
}
}
}
var worldPoint = { x: roundedX, y: roundedY };
var gcosty = 0;
var hcosty = 0;
var fcosty = 0;

var index = xx * (gridWorldSize.xL + gridWorldSize.xR + 1) + yy;

/*if (worldPoint.x === cData.lip.x && worldPoint.y === cData.lip.y) {
//console.PrintError("found start Node");
var startNodegcoster = 0;
var startNodehcoster = AIPathFindUtilities.npcCheckDistance(worldPoint, cData.ltp);
var startNodefcoster = startNodegcoster + startNodehcoster;
var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

grid[index] = startNode;
openSet.push(startNode);
}
else if (cData.lsp.x === worldPoint.x && cData.lsp.y === worldPoint.y) {
//console.PrintError("new Grid");
gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
hcosty = AIPathFindUtilities.npcCheckDistance(worldPoint, cData.ltp);
fcosty = gcosty + hcosty;

var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

grid[index] = startNode;
openSet.push(startNode);
}
else {
gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
hcosty = AIPathFindUtilities.npcCheckDistance(cData.ltp, worldPoint);
fcosty = gcosty + hcosty;

grid[index] = { boolWalk: boolWalker, worldPosition: worldPoint, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };
}



if (worldPoint.x === initialPathfindStartingPos[globIndex].x && worldPoint.y === initialPathfindStartingPos[globIndex].y)
{
//console.PrintError("found start Node");
var startNodegcoster = 0;
var startNodehcoster = AIPathFindUtilities.npcCheckDistance(worldPoint, initialPathfindTargetPos[globIndex]);
var startNodefcoster = startNodegcoster + startNodehcoster;
var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: startNodegcoster, hcost: startNodehcoster, fcost: startNodefcoster, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

grid[index] = startNode;
openSet.push(startNode);
}
else if (cData.lsp.x === worldPoint.x && cData.lsp.y === worldPoint.y)
{
console.PrintError("new Grid");
gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
hcosty = AIPathFindUtilities.npcCheckDistance(worldPoint, initialPathfindTargetPos[globIndex]);
fcosty = gcosty + hcosty;

var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: gcosty, hcost: hcosty, fcost: fcosty, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

grid[index] = startNode;
openSet.push(startNode);
}
else {
gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
hcosty = AIPathFindUtilities.npcCheckDistance(initialPathfindTargetPos[globIndex], worldPoint);
fcosty = gcosty + hcosty;

grid[index] = { boolWalk: boolWalker, worldPosition: worldPoint, gcost: gcosty, hcost: hcosty, fcost: fcosty, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };
}*/


















/*if (storage.IsSetGlobal("GlobalIndex_Player_" + cData.objt.pName))
{
    indexOfPlayer = storage.GetGlobal("GlobalIndex_Player_" + cData.objt.pName);

    //console.PrintError(cData.sSwtc);
    if (stationTilesArray[indexOfPlayer] == null && cData.sSwtc  == 1)
    {
        stationTilesArray[indexOfPlayer] = storage.GetGlobal("station_tiles" + current_base);
        //stationTiles = stationTilesArray[indexOfPlayer];
        cData.stRot = stationTilesArray[indexOfPlayer].rot;
        cData.stCoord = stationTilesArray[indexOfPlayer].coord;
        //cData.sSwtc = 2;
    }
    else if (stationTilesArray[indexOfPlayer] != null && cData.sSwtc == 1)
    {
        cData.stRot = stationTilesArray[indexOfPlayer].rot;
        cData.stCoord = stationTilesArray[indexOfPlayer].coord;
        //cData.sSwtc = 2;
    }
}

if (cData.objt.formation == 1)
{
    globIndex = storage.GetGlobal("maxDroneIndex0");
}
else if (cData.objt.formation == 2)
{
    globIndex = storage.GetGlobal("maxDroneIndex1");
}
else if (cData.objt.formation == 3)
{
    globIndex = storage.GetGlobal("maxDroneIndex2");
}
else if (cData.objt.formation == 4)
{
    globIndex = storage.GetGlobal("maxDroneIndex3");
}
else if (cData.objt.formation == 5)
{
    globIndex = storage.GetGlobal("maxDroneIndex4");
}*/

/*  if (initialPathfindStartingPos[globIndex] == null && cData.sSwtc == 1)
{
  var initialPosX = Math.round(cData.nData.nCoord.x);
var initialPosY = Math.round(cData.nData.nCoord.y);
var initialPos = { x: initialPosX, y: initialPosY };

var remnant = 360 - cData.stRot;
initialPathfindStartingPos[globIndex] = SC_Utilities.RotatePoint(initialPos, cData.stCoord, remnant);

  initialPathfindStartingPos[globIndex].x = Math.round(initialPathfindStartingPos[globIndex].x);
  initialPathfindStartingPos[globIndex].y = Math.round(initialPathfindStartingPos[globIndex].y);

  cData.lip = initialPathfindStartingPos[globIndex];
  cData.glip = initialPathfindStartingPos[globIndex];
  cData.lsp = initialPathfindStartingPos[globIndex];

  //cData.sSwtc = 3;
}
else if (initialPathfindStartingPos[globIndex] != null && cData.sSwtc == 1)
{
  cData.lip = initialPathfindStartingPos[globIndex];
  cData.glip = initialPathfindStartingPos[globIndex];
  cData.lsp = initialPathfindStartingPos[globIndex];
  //cData.sSwtc = 3;
}

if (initialPathfindTargetPos[globIndex] == null && cData.sSwtc == 1)
{
  var targetX = Math.round(cData.cFWP.x);
var targetY = Math.round(cData.cFWP.y);
var target = { x: targetX, y: targetY };

var remnant = 360 - cData.stRot;
initialPathfindTargetPos[globIndex] = SC_Utilities.RotatePoint(target, cData.stCoord, remnant);

  initialPathfindTargetPos[globIndex].x = Math.round(initialPathfindTargetPos[globIndex].x);
  initialPathfindTargetPos[globIndex].y = Math.round(initialPathfindTargetPos[globIndex].y);

  cData.ltp = { x: initialPathfindTargetPos[globIndex].x, y: initialPathfindTargetPos[globIndex].y };
  cData.sSwtc = 0;
}
else if (initialPathfindTargetPos[globIndex] != null && cData.sSwtc == 1)
{
  //targetPos = initialPathfindTargetPos[globIndex];
  cData.ltp = initialPathfindTargetPos[globIndex];
  cData.sSwtc = 0;
}


var nodeDiameter = nodeRadius * 2;

var gridSizeXL = Math.round(gridWorldSize.xL * 1);
var gridSizeYB = Math.round(gridWorldSize.yB * 1);

var gridSizeXR = Math.round(gridWorldSize.xR * 1);
var gridSizeYT = Math.round(gridWorldSize.yT * 1);

var parenter = null;

if (noder != null)
{
  parenter = noder.parent;
}

var grid = [];
var openSet = [];
var closedSet = [];

if (stationTilesArray[indexOfPlayer] != null)
{
  if (stationTilesArray[indexOfPlayer].grid != null)
  {
      if (stationTilesArray[indexOfPlayer].grid.length > 0) {
          //console.PrintError("station TILES EXIST");
          switchForStationTiles = 1;
      }
      else
      {
          switchForStationTiles = 0;
      }
  }   
  else {
      switchForStationTiles = 0;
  }
}
else {
  switchForStationTiles = 0;
}

for (var x = -gridSizeXL; x <= gridSizeXR; x++)
{
  for (var y = -gridSizeYB; y <= gridSizeYT; y++)
  {
      //grid.push(null);
      var xx = x;
var yy = y;

      if (xx< 0)
      {
          xx *= -1;
          xx = (gridSizeXR) + xx;
      }
      if (yy< 0)
      {
          yy *= -1;
          yy = (gridSizeYT) + yy;
      }

      var boolWalker = 1;


var roundedX = (Math.round(cData.glip.x)) + x;
var roundedY = (Math.round(cData.glip.y)) + y;
var rounded = { x: roundedX, y: roundedY };

      if (switchForStationTiles == 1)
      {
          var coords = game.GetObjectCoordinates(cData.objt.sid, stationTilesArray[indexOfPlayer].baseID);

//var remnant = 360 - cData.stationTiles.rot;
//coords= SC_Utilities.RotatePoint(target, cData.stationTiles.coord, remnant);

coords.x = Math.round(coords.x);
          coords.y = Math.round(coords.y);

          var widthLeft = stationTilesArray[indexOfPlayer].widthL;
var widthRight = stationTilesArray[indexOfPlayer].widthR;

var heightBottom = stationTilesArray[indexOfPlayer].heightB;
var heightTop = stationTilesArray[indexOfPlayer].heightT;

//var testX = coords.x + x;
//var testY = coords.y + y;

var diffX = Math.round(Math.abs(Math.abs(roundedX) - Math.abs(coords.x)));
var diffY = Math.round(Math.abs(Math.abs(roundedY) - Math.abs(coords.y)));

var test = { x: roundedX, y: roundedY };

          if (test.x<coords.x)
          {
              diffX *= -1;
          }

          if (test.y<coords.y)
          {
              diffY *= -1;
          }

          if (test.x >= coords.x - widthLeft && test.x <= coords.x + widthRight && test.y >= coords.y - heightBottom && test.y <= coords.y + heightTop)
          {
              if (diffX< 0)
              {
                  diffX = (widthRight) + (diffX* -1);
              }

              if (diffY< 0)
              {
                  diffY = (heightTop) + (diffY* -1);
              }

              var indexer = diffX + (widthLeft + widthRight + 1) * diffY;

              if (stationTilesArray[indexOfPlayer].grid[indexer] == 0)
              {
                  boolWalker = 1;
              }
              else
              {
                  /*if (stationTiles.visualTiles[indexer] == 1)
                  {
                      var idObj = generator.AddSpecialObject(cData.objt.sid, roundedX, roundedY, "stopSign_02", 0);

                      if (storage.IsSetGlobal("crates_Stop" + cData.objt.sid))
                      {
                          var crates = storage.GetGlobal("crates_Stop" + cData.objt.sid);
                          crates.push({ id: idObj, staID: stationTiles.baseID });
                          storage.SetGlobal("crates_Stop" + cData.objt.sid, crates);
                      }
                      else
                      {
                          var crates = [];
                          crates.push({ id: idObj, staID: stationTiles.baseID });
                          storage.SetGlobal("crates_Stop" + cData.objt.sid, crates);
                      }
                      stationTiles.visualTiles[indexer] = 2;
                  }

                  boolWalker = 0;
              }
          }
      }
      var worldPoint = { x: roundedX, y: roundedY };
var gcosty = 0;
var hcosty = 0;
var fcosty = 0;

var index = xx * (gridWorldSize.xL + gridWorldSize.xR + 1) + yy;

      /*if (worldPoint.x === cData.lip.x && worldPoint.y === cData.lip.y) {
          //console.PrintError("found start Node");
          var startNodegcoster = 0;
          var startNodehcoster = AIPathFindUtilities.npcCheckDistance(worldPoint, cData.ltp);
          var startNodefcoster = startNodegcoster + startNodehcoster;
          var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

          grid[index] = startNode;
          openSet.push(startNode);
      }
      else if (cData.lsp.x === worldPoint.x && cData.lsp.y === worldPoint.y) {
          //console.PrintError("new Grid");
          gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
          hcosty = AIPathFindUtilities.npcCheckDistance(worldPoint, cData.ltp);
          fcosty = gcosty + hcosty;

          var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

          grid[index] = startNode;
          openSet.push(startNode);
      }
      else {
          gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
          hcosty = AIPathFindUtilities.npcCheckDistance(cData.ltp, worldPoint);
          fcosty = gcosty + hcosty;

          grid[index] = { boolWalk: boolWalker, worldPosition: worldPoint, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };
      }



      if (worldPoint.x === initialPathfindStartingPos[globIndex].x && worldPoint.y === initialPathfindStartingPos[globIndex].y)
      {
          //console.PrintError("found start Node");
          var startNodegcoster = 0;
var startNodehcoster = AIPathFindUtilities.npcCheckDistance(worldPoint, initialPathfindTargetPos[globIndex]);
var startNodefcoster = startNodegcoster + startNodehcoster;
var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: startNodegcoster, hcost: startNodehcoster, fcost: startNodefcoster, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

grid[index] = startNode;
          openSet.push(startNode);
      }
      else if (cData.lsp.x === worldPoint.x && cData.lsp.y === worldPoint.y)
      {
          console.PrintError("new Grid");
          gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
          hcosty = AIPathFindUtilities.npcCheckDistance(worldPoint, initialPathfindTargetPos[globIndex]);
          fcosty = gcosty + hcosty;

          var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: gcosty, hcost: hcosty, fcost: fcosty, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };

grid[index] = startNode;
          openSet.push(startNode);
      }
      else {
          gcosty = AIPathFindUtilities.npcCheckDistance(cData.lsp, worldPoint);
          hcosty = AIPathFindUtilities.npcCheckDistance(initialPathfindTargetPos[globIndex], worldPoint);
          fcosty = gcosty + hcosty;

          grid[index] = { boolWalk: boolWalker, worldPosition: worldPoint, gcost: gcosty, hcost: hcosty, fcost: fcosty, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };
      }

  }
}

lastPlayerID = cData.objt.pid;
var data = { grid: grid, openSet: openSet };    
return data;*/
