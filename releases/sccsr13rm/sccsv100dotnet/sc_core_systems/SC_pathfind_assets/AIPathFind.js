using(console);
using(storage);

include(AIPathFindUtilities.js);
include(AIPathFindCheckAllSides.js);
include(SC_Utilities.js);
include(SC_Pathfind_Heap.js);

var lastDist;
var gridSizeX;
var gridSizeY;

var someSwitcher = 0;


var AIPathFind =
{
    npcPathFind: function (gridWorldSize, nodeRadius, cData, current_base) // cData.objt.sid for generator.AddContainer when testing
    {
        var someExtratiles = [];

        if (cData.dog.openSet.length > 0)
        {
            /*if (someSwitcher == 0)
            {
                cData.dog.openSet = SC_Pathfind_Heap.heapSort(cData.dog.openSet, cData.dog.openSet.length - 1, "fcost");
                cData.dog.openSet = SC_Pathfind_Heap.heapSort(cData.dog.openSet, cData.dog.openSet.length - 1, "hcost");

                someSwitcher = 1;
            }*/

            cData.dog.openSet = SC_Pathfind_Heap.heapSort(cData.dog.openSet, cData.dog.openSet.length - 1);
            //cData.dog.openSet = SC_Pathfind_Heap.heapSort(cData.dog.openSet, cData.dog.openSet.length - 1, "hcost");
            //cData.dog.openSet = SC_Pathfind_Heap.heapSort(cData.dog.openSet, cData.dog.openSet.length - 1, "gcost");

            gridSizeX = (gridWorldSize.x);
            gridSizeY = (gridWorldSize.y);



            var node = cData.dog.openSet.shift();

            cData.log[node.gridIndex].grid[node.index].closed = 1;

            var targetX = (cData.ltp.x);
            var targetY = (cData.ltp.y);

            var nodeX = (node.worldPosition.x);
            var nodeY = (node.worldPosition.y);

            var noderPos = { x: nodeX, y: nodeY };
            var currentPoint = SC_Utilities.RotatePoint(noderPos, cData.stCoord, cData.stRot);
            currentPoint.x = (currentPoint.x);
            currentPoint.y = (currentPoint.y);

            if ((nodeX) == (targetX) && (nodeY) == (targetY) || (cData.lsp.x) == (targetX) && (cData.lsp.y) == (targetY)) //nodeX == targetX && nodeY == targetY
            {
                var path = RetracePath(cData.lip, cData.ltp, node, cData.log);
                //path.splice(0, 1);
                //path.splice(path.length - 1, 1);

                for (var i = 0; i < path.length; i++) {
                    //path[i].worldPosition = SC_Utilities.RotatePoint(path[i].worldPosition, cData.stCoord, cData.stRot);  
                    var idObj = generator.AddSpecialObject(cData.objt.sid, path[i].worldPosition.x, path[i].worldPosition.y, "goSign_02", 0); //scrap_metal_00

                    if (storage.IsSetGlobal("signs_Go" + cData.objt.sid)) {
                        var crates = storage.GetGlobal("signs_Go" + cData.objt.sid);
                        crates.push(idObj);
                        storage.SetGlobal("signs_Go" + cData.objt.sid, crates);
                    }
                    else {
                        var crates = [];
                        crates.push(idObj);
                        storage.SetGlobal("signs_Go" + cData.objt.sid, crates);
                    }
                }

                console.PrintError("found Waypoint");
                var data = { openSet: cData.dog.openSet,currentCommand: 10, grid: cData.log[node.gridIndex].grid, path: path, node: node, iot: null };
                return data;
            }

            //------------------------------------
            //------------------------------------
            var someData = AIPathFindCheckAllSides.checkAllSidesGridIndex(node, gridWorldSize, cData);
            //------------------------------------
            //------------------------------------

            if (someData.extraTiles != null)
            {
                if (someData.extraTiles.length > 0)
                {
                    someExtratiles = someData.extraTiles;
                    var sometester = [];

                    for (var i = 0; i < someExtratiles.length; i++)
                    {
                        //console.PrintError("x: " + someExtratiles[i].sgp.x + " y: " + someExtratiles[i].sgp.y);
                        cData.lsp  = { x: (someExtratiles[i].sgp.x), y: (someExtratiles[i].sgp.y) };                      
                        cData.glip = { x: someExtratiles[i].docg.gridData.x, y: someExtratiles[i].docg.gridData.y };

                        var gridIndex = someExtratiles[i].docg.index;

                        if (cData.log[gridIndex] == null)
                        {
                            var pathData = AIPathFindInitGrid.npcBuildGrid(gridWorldSize, nodeRadius, gridIndex, cData, node, current_base);
                            var data = pathData.openSet;

                            cData.dog.grid = pathData.grid;

                            var someDataOfGrid = { grid: cData.dog.grid };
                            cData.log[gridIndex] = someDataOfGrid;
                            sometester.push(data[0]);

                            //continue;
                        }
                        else
                        {
                            sometester.push(cData.log[gridIndex].grid[someExtratiles[i].iot]);
                        }
                    }
                    for (var i = 0; i < sometester.length; i++)
                    {
                        var testerr = { swtc: 0, node: sometester[i], sgp: null, iot: null, iog: null };
                        someData.neighboors.push(testerr);
                    }
                }
            }

            var idObj = generator.AddSpecialObject(cData.objt.sid, node.worldPosition.x, node.worldPosition.y, "waypoint_00", 0); //scrap_metal_00

            if (storage.IsSetGlobal("crates_" + cData.objt.sid)) {
                var crates = storage.GetGlobal("crates_" + cData.objt.sid);
                crates.push(idObj);
                storage.SetGlobal("crates_" + cData.objt.sid, crates);
            }
            else {
                var crates = [];
                crates.push(idObj);
                storage.SetGlobal("crates_" + cData.objt.sid, crates);
            }

            if (someData.neighboors.length > 0)
            {
                for (var j = 0; j < someData.neighboors.length; j++)
                {
                    if (cData.log[someData.neighboors[j].node.gridIndex].grid[someData.neighboors[j].node.index].closed == 1 || someData.neighboors[j].node.boolWalk == 0) //AIPathFindUtilities.doesContain(closedSet, someData.neighboors[j].node) == 1 
                    {
                        continue;
                    }

                    //var containsOrNot = AIPathFindUtilities.doesContain(cData.dog.openSet, someData.neighboors[j].node);

                    var gcost = node.gcost + AIPathFindUtilities.npcCheckDistance(node.worldPosition, someData.neighboors[j].node.worldPosition);

                    if (gcost < someData.neighboors[j].node.gcost || cData.log[someData.neighboors[j].node.gridIndex].grid[someData.neighboors[j].node.index].open == 0)
                    {
                        someData.neighboors[j].node.gcost = gcost;
                        someData.neighboors[j].node.hcost = AIPathFindUtilities.npcCheckDistance(someData.neighboors[j].node.worldPosition, cData.ltp);
                        someData.neighboors[j].node.fcost = someData.neighboors[j].node.gcost + someData.neighboors[j].node.hcost;
                        //if (node.gridIndex == null || node.index == null) {
                        //    console.PrintError("WTF IS GOING ON. NULL PARENT NODE");
                        //}
                        //if (cData.log[someData.neighboors[j].node.gridIndex].grid[someData.neighboors[j].node.index] == null) {
                        //    console.PrintError("NULL NODE");
                        //}

                        cData.log[someData.neighboors[j].node.gridIndex].grid[someData.neighboors[j].node.index].parent = { iog: node.gridIndex, iot: node.index };

                        if (cData.log[someData.neighboors[j].node.gridIndex].grid[someData.neighboors[j].node.index].open == 0)
                        {
                            var idObj = generator.AddSpecialObject(cData.objt.sid, someData.neighboors[j].node.worldPosition.x, someData.neighboors[j].node.worldPosition.y, "waypoint_01", 0); //scrap_metal_00

                            if (storage.IsSetGlobal("crates_" + cData.objt.sid)) {
                                var crates = storage.GetGlobal("crates_" + cData.objt.sid);
                                crates.push(idObj);
                                storage.SetGlobal("crates_" + cData.objt.sid, crates);
                            }
                            else {
                                var crates = [];
                                crates.push(idObj);
                                storage.SetGlobal("crates_" + cData.objt.sid, crates);
                            }

                            cData.dog.openSet.push(someData.neighboors[j].node);
                            cData.log[someData.neighboors[j].node.gridIndex].grid[someData.neighboors[j].node.index].open = 1;
                        }
                    }
                }
            }
            var data = { openSet: cData.dog.openSet, currentCommand: 0, grid: null, path: null, node: node, iot: null };
            return data;

            //while (cData.dog.openSet.length > 0)
            //{
            //}
        }
        else {
            console.PrintError("PathFind initiated without a grid or without something else");

            var data = { openSet: cData.dog.openSet, currentCommand: 9, grid: null, path: null, node: null, iot: null };
            return data;
        }
    }
};




var counting = 0;
function RetracePath(initialPos, targetPos, node, listOfGrids) {
    var currentNode = node.parent;
    counting = 0;
    var path = [];

    var currentX = targetPos.x;
    var currentY = targetPos.y;

    var startX = (initialPos.x);
    var startY = (initialPos.y);

    var mainSwitch = 1;

    var lastNodeGridIndex;

    while (mainSwitch == 1)
    {
        path.unshift(node);

        currentX = (node.worldPosition.x);
        currentY = (node.worldPosition.y);

        if (node.parent != null)
        {
            var gridIndex = node.parent.iog;
            var nodeIndex = node.parent.iot;

            currentNode = listOfGrids[gridIndex].grid[nodeIndex];
            node = currentNode;
        }
        else
        {
            console.PrintError("node.parent is NULL");
            mainSwitch = 0;
            break;
        }

        if (currentX == startX && currentY == startY)
        {
            mainSwitch = 0;
            break;
        }

        if (counting > 1500) {
            mainSwitch = 0;
            break;
        }

        counting++;
    }
    return path;

}

































/*function RetracePath(initialPos, targetPos, node, gridWorldSize, listOfGrids)
{
    counting = 0;
    //List < Node > path = new List<Node>();

    var currentGrid = listOfGrids[listOfGrids.length - 1];
    var indexOfGrid = listOfGrids.length - 1;

    var path = [];
   
    var indexOfNode = node.index;


    var currentX = targetPos.x;
    var currentY = targetPos.y;

    var startX = (initialPos.x);
    var startY = (initialPos.y);

    var mainSwitch = true;

    var currentNode = node;

    while (mainSwitch)
    {
        currentNode = node;
        path.push(currentNode);

        currentX = (node.worldPosition.x);
        currentY = (node.worldPosition.y);

        if (currentX != startX || currentY != startY)
        {
            if (currentNode.link == 1)
            {
                indexOfGrid -= 1;
                currentGrid = listOfGrids[indexOfGrid];

                for (var r = 0; r < currentGrid.grid.length; r++)
                {
                    if (currentGrid.grid[r].link == 2)
                    {
                        //console.PrintError("*****************GRID TILE FOUND***************");
                        currentNode = currentGrid.grid[r];
                        currentNode = currentNode.parent;
                    }
                }
            }
            else// if (currentNode.link == 0)
            {
                currentNode = node.parent;
                //console.PrintError(currentNode.link);
            }
        }
        else if (currentX == startX && currentY == startY)
        {
            mainSwitch = false;
            break;
        }

        if (currentNode == null) {
            mainSwitch = false;
            break;
        }
        node = currentNode;

        if (counting > 250)
        {
            mainSwitch = false;
            break;
        }
        counting++;
    }

    //console.PrintError(counting);

    return path;

}*/
