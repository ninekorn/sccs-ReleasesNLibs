using System;
using System.Collections.Generic;
using System.Text;
using SharpDX;






using _sc_core_systems.SC_pathfind_assets;


namespace _sc_core_systems.SC_pathfind_assets
{
    public class sc_ai_pathfind
    {
        public void sc_ai_pathfind_start(sc_curData cData)
        {
            if (cData.cFWPD > 1)
            {
                if (cData.mSwtc == 1)
                {
                    cData.sSwtc = 1;


                }
            }
        }
    }
}
/*if (cData.cFWPD > 1) {

    if (cData.mSwtc == 1)
    {
        npc.InstantStop(cData.objt.nid);
        npc.StopEvasion(cData.objt.nid);
        if (cData.mSwtc == 1) {

            cData.sSwtc = 1;
            //gridWorldSize, nodeRadius, gridIndex, cData, node, current_base);
            var pathData = AIPathFindInitGrid.npcBuildGrid(gridWorldSize, nodeRadius, 0, cData, null, current_base);
//cData.dog.gridOffset = pathData.gridOffset;

cData.dog.grid = pathData.grid;
            cData.dog.openSet = pathData.openSet;
            //cData.dog.path = pathData.path;

            var someDataOfGrid = { grid: cData.dog.grid }; //, index: cData.log.length 
cData.log.push(someDataOfGrid);
        }
cData.mSwtc = 3;
    }
    else if (cData.mSwtc == 3) {




        var pathData = null;
        for (var i = 0; i< 10; i++)
        {
            pathData = AIPathFind.npcPathFind(gridWorldSize, nodeRadius, cData, current_base);
            cData.pfc = pathData.currentCommand;

            if (pathData.openSet != null && pathData.openSet.length > 0)
            {

                cData.xtra = pathData.extra;
                cData.dog.node = pathData.node;
                cData.dog.openSet = pathData.openSet;
                cData.dog.path = pathData.path;

                if (cData.pfc == 10)
                {
                    cData.mSwtc = 4;
                    break;
                }               
            }
        }
    }
    else if (cData.mSwtc == 4)
    {
        console.PrintError("***END OF PATHFIND***");
        cData.mSwtc = 50;
    }
    else if (cData.mSwtc == 5)
    {
        if (cData.dog.path != null)
        {
            if (cData.dog.path.length > 0)
            {
                cData.dog.path[0].worldPosition.x = Math.round(cData.dog.path[0].worldPosition.x);
                cData.dog.path[0].worldPosition.y = Math.round(cData.dog.path[0].worldPosition.y);

                cData.nData.nCoord.x = Math.round(cData.nData.nCoord.x);
                cData.nData.nCoord.y = Math.round(cData.nData.nCoord.y);

                var distToNode = SC_Utilities.GetDistance(cData.dog.path[0].worldPosition, cData.nData.nCoord);

var dirToWaypointX = ((cData.dog.path[0].worldPosition.x) - (cData.nData.nCoord.x)) / distToNode;
var dirToWaypointY = ((cData.dog.path[0].worldPosition.y) - (cData.nData.nCoord.y)) / distToNode;

var newOffsetWaypointPosX = (cData.dog.path[0].worldPosition.x + (dirToWaypointX * 6));
var newOffsetWaypointPosY = (cData.dog.path[0].worldPosition.y + (dirToWaypointY * 6));

var newOffsetWaypointPos = { x: newOffsetWaypointPosX, y: newOffsetWaypointPosY };

var rotationDOTRL = SC_Utilities.Dot(dirToWaypointY, -dirToWaypointX, -cData.nData.nForward.x, -cData.nData.nForward.y);
var rotationDOTFB = SC_Utilities.Dot(dirToWaypointX, dirToWaypointY, cData.nData.nForward.x, cData.nData.nForward.y);
var rotationDOTVELO = SC_Utilities.Dot(dirToWaypointX, dirToWaypointY, cData.nData.nVelo.x, cData.nData.nVelo.y);

                if (distToNode > 0.1) {
                    if (rotationDOTFB >= 0.99) {
                        //npc.Unstick(cData.objt.nid);
                        //npc.GoForward(cData.objt.nid);
                        npc.StickToPoint(cData.objt.nid, newOffsetWaypointPos.x, newOffsetWaypointPos.y, 0);
                    }
                    else {
                        if (rotationDOTVELO< 0.97) {
                            cData.stopSCM = 10;
                            if (cData.stopS == 0) {
                                if (cData.cLFWP.x != cData.cFWP.x && cData.cLFWP.y != cData.cFWP.y) {
                                }
                                npc.InstantStop(cData.objt.nid);

                                //npc.Stop(cData.objt.nid);

                                cData.stopS = 1;
                            }
                            npc.StickToPoint(cData.objt.nid, newOffsetWaypointPos.x, newOffsetWaypointPos.y, 0);
                        }
                        else {
                            cData.stopSCM = 20;
                            if (cData.stopS == 0) {
                                if (cData.cLFWP.x != cData.cFWP.x && cData.cLFWP.y != cData.cFWP.y) {

                                }
                                npc.InstantStop(cData.objt.nid);
                                //npc.Stop(cData.objt.nid);

                                cData.stopS = 1;
                            }
                            npc.StickToPoint(cData.objt.nid, newOffsetWaypointPos.x, newOffsetWaypointPos.y, 0);
                        }
                    }
                }
                else {
                    if (Math.round(cData.nData.nCoord.x) == Math.round(cData.dog.path[0].worldPosition.x) && Math.round(cData.nData.nCoord.y) == Math.round(cData.dog.path[0].worldPosition.y)) {
                        npc.Unstick(cData.objt.nid);
                        npc.InstantStop(cData.objt.nid);
                        //npc.Stop(cData.objt.nid);

                        cData.dog = null;
                        cData.mSwtc = 6;
                    }
                    else {
                        npc.Unstick(cData.objt.nid);
                        cData.dog.path.shift();
                        //console.PrintError("03");
                        if (cData.stopS == 0) {
                            if (cData.cLFWP.x != cData.cFWP.x && cData.cLFWP.y != cData.cFWP.y) {

                            }
                            npc.InstantStop(cData.objt.nid);
                            //npc.Stop(cData.objt.nid);

                            cData.stopS = 1;
                        }
                    }
                }
            }
            else {
                if (cData.cFWPD > 1) {
                    if (cData.cLFWP.x != cData.cFWP.x && cData.cLFWP.y != cData.cFWP.y) {

                    }

                    cData.mSwtc = 2;
                }
                else {
                    //console.PrintError("reached waypoint");
                }
            }
        }
        else {
            if (cData.cFWPD > 1) {
                if (cData.cLFWP.x != cData.cFWP.x && cData.cLFWP.y != cData.cFWP.y) {

                }

                cData.mSwtc = 2;
            }
            else {
                //console.PrintError("reached waypoint");
            }
        }
    }
    else if (cData.mSwtc == 6) {
        if (cData.cFWPD > 1) {
            if (cData.cLFWP.x != cData.cFWP.x && cData.cLFWP.y != cData.cFWP.y) {

            }

            cData.mSwtc = 2;
        }
    }
    else if (cData.mSwtc == 7) {
        if (cData.cFWPD > 1) {
            console.PrintError("waypoint doesnt exist");
            //npc.StickToObject(cData.objt.nid, cData.objt.pid, 0);
            cData.stopSCM = 20;

            if (cData.stopSC >= cData.stopSCM) {


                cData.stopSC = 0;
            }
            cData.stopSC++;
        }
    }
}
else {

    //idle
}

if (cData.stopS == 1) {
    if (cData.stopSC >= cData.stopSCM) {
        cData.stopS = 0;
        cData.stopSC = 0;
    }
    cData.stopSC++;
}
cData.cLFWP = cData.cFWP;
return cData;*/




















//using(console);
//using(player);
//using(npc);
//using(ship);
//using(generator);
//include(SC_Utilities.js);
//include(AIPathFindInitGrid.js);
//include(AIPathFind.js);
//include(AIPathFindUtilities.js);


//var gridWorldSize = { xL: 2, xR: 1, yB: 2, yT: 1 }; //{ xL: 5, xR: 4, yB: 5, yT: 4};
//var nodeRadius = 1;
//var pathData;
