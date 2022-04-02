using(console);

include(AIPathFindUtilities.js);

var AIPathFindCheckAllSides =
{
    checkAllSidesGridIndex: function (node, gridWorldSize, cData)
    {
        var neighboors = [];
        var extraTiles = [];
        for (var x = -1; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                /*if (x == 0 && y == 0)
                {
                    continue;
                }*/

                //console.PrintError(node.gridPos.x);


                var xpos = Math.round(node.worldPosition.x + x);
                var ypos = Math.round(node.worldPosition.y + y);

                var pos = { x: xpos, y: ypos };

                var gridData = AIPathFindUtilities.getNewGridIndex(cData.lip, pos, gridWorldSize);

                var indexOfGrid = gridData.index;

                var xpos = Math.round(node.worldPosition.x + x);
                var ypos = Math.round(node.worldPosition.y + y);

                var pos = { x: xpos, y: ypos };

                if (xpos >= node.gridPos.x - gridWorldSize.xL && xpos <= node.gridPos.x + gridWorldSize.xR &&
                    ypos >= node.gridPos.y - gridWorldSize.yB && ypos <= node.gridPos.y + gridWorldSize.yT)
                {
                    var gridTileX = node.gridTileX + x;
                    var gridTileY = node.gridTileY + y;

                    if (gridTileX < 0)
                    {
                        gridTileX = (gridWorldSize.xR) + (gridTileX * -1);
                    }

                    if (gridTileY < 0)
                    {
                        gridTileY = (gridWorldSize.yT) + (gridTileY * -1);
                    }
                    var index = ((gridTileX) * (gridWorldSize.xL + gridWorldSize.xR + 1)) + (gridTileY);

                    neighboors.push({ swtc: 0, node: cData.log[node.gridIndex].grid[index], sgp: null, iot: null, iog: null});
                }
                else
                {             
                    var indexOfGrid = gridData.index;
     
                    var diffX = Math.round(Math.abs(Math.abs(pos.x) - Math.abs(gridData.gridData.x)));
                    var diffY = Math.round(Math.abs(Math.abs(pos.y) - Math.abs(gridData.gridData.y)));

                    var starterGriderPos = { x: gridData.gridData.x, y: gridData.gridData.y };

                    //console.PrintError("iog: " + indexOfGrid);

                    /*var idObj = generator.AddSpecialObject(cData.objt.sid, gridData.gridData.x, gridData.gridData.y, "waypoint_02", 0);
                    
                    if (storage.IsSetGlobal("signs_Go" + cData.objt.sid)) {
                        var crates = storage.GetGlobal("signs_Go" + cData.objt.sid);
                        crates.push(idObj);
                        storage.SetGlobal("signs_Go" + cData.objt.sid, crates);
                    }
                    else {
                        var crates = [];
                        crates.push(idObj);
                        storage.SetGlobal("signs_Go" + cData.objt.sid, crates);
                    }*/

                    if (pos.x < starterGriderPos.x)
                    {
                        diffX *= -1;
                    }

                    if (pos.y < starterGriderPos.y) {
                        diffY *= -1;
                    }

                    if (diffX < 0)
                    {
                        diffX = (gridWorldSize.xR) + (diffX*-1);
                    }

                    if (diffY < 0) {
                        diffY = (gridWorldSize.yT) + (diffY * -1);
                    }

                    var indexer = diffX * (gridWorldSize.xL + gridWorldSize.xR + 1) + diffY;
                    //extraTiles.push({ sgp: pos, iot: indexer, docg: gridData });

                    if (cData.log[indexOfGrid] == null)
                    {
                        extraTiles.push({ sgp: pos, iot: indexer, docg: gridData });
                        //neighboors.push({ swtc: 1, node: null, sgp: pos, iot: indexer, iog: indexOfGrid});
       
                    }
                    else
                    {
                        neighboors.push({ swtc: 0, node: cData.log[indexOfGrid].grid[indexer], sgp: null, iot: null, iog: null});
                    }
                }
            }
        }
        return { neighboors: neighboors, extraTiles: extraTiles };
    }
};