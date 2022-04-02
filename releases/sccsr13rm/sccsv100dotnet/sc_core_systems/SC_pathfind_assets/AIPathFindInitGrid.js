using(console);
include(AIPathFindUtilities.js);
include(SC_Utilities.js);

var switchForStationTiles = 0;

var stationTilesArray = [];
var initialPathfindStartingPos = [];
var initialPathfindTargetPos = [];

var lastPlayerID;
var indexOfPlayer;
var globIndex;
var targetPos;
var initialPos;

var AIPathFindInitGrid =
{
    npcBuildGrid: function (gridWorldSize, nodeRadius, indexOfGrid, cData, noder, current_base)
    {
        if (storage.IsSetGlobal("GlobalIndex_Player_" + cData.objt.pName))
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
        }

        if (initialPathfindStartingPos[globIndex] == null && cData.sSwtc == 1)
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
                            }*/

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
                }*/



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
        var data = { grid: grid, openSet: openSet};    
        return data;
    }
};