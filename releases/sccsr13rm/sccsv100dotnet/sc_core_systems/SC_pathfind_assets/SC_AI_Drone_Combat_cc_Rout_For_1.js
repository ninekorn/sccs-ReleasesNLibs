using(console);
using(npc);
using(ship);
using(storage);


include(SC_Utilities.js);

include(SC_AI_Drone_Get_nData.js);
include(SC_AI_Drone_Get_pData.js);

include(SC_AI_Drone_Combat_cc_Rout_For_FWPB_1.js);
include(SC_AI_Drone_Combat_cc_Rout_For_FWP_1.js);
include(SC_AI_Drone_Combat_cc_Rout_Com_1.js);
include(SC_AI_Drone_Combat_cc_Reset_Speed_1.js);

var nData = null;
var pData = null;
var hpNPC;
var hpPlayer;

var cData = [];
var cDat = null;

var ceData = [];
var ceDat = null;

var lcData = [];
var lcDat = null;

var lceData = [];
var lceDat = null;

var distToWaypoint = 0;
var currentFormationWaypoint = { x: null, y: null };
var initOnce = 1;
var initArrayOfFriendlies = 1;
var arrayOfFriendlies = [];
var dataToReturn = null;
var globIndex;
var resetSwitch = 0;
var someTester = 1;

var SC_AI_Drone_Combat_cc_Rout_For_1 =
{
    AIRoutineInit: function (currentObjective, addFriendSwitch) {

        //console.PrintError(someTester);

        if (!storage.IsSetGlobal("FriendliesList" + currentObjective.pid))
        {
            arrayOfFriendlies = [];
            arrayOfFriendlies.push(currentObjective.pName.toLowerCase());
            arrayOfFriendlies.push("turret");
            arrayOfFriendlies.push("merchant");
            arrayOfFriendlies.push("miner");
            arrayOfFriendlies.push("patrol");
            arrayOfFriendlies.push("repair");
            arrayOfFriendlies.push("mining");
            arrayOfFriendlies.push("combat");
            arrayOfFriendlies.push("scurvy");
            storage.SetGlobal("FriendliesList" + currentObjective.pid, arrayOfFriendlies);

         
        }
        else // if the global friendlist list for that player is not set, set it now.
        {
        

            /*for (var i = 0; i < currentList.length; i++)
            {
                console.PrintError(currentList[i]);
            }*/

            if (arrayOfFriendlies.length <= 0 || arrayOfFriendlies == null)
            {
                var currentList = storage.GetGlobal("FriendliesList" + currentObjective.pid);

                arrayOfFriendlies = currentList;
                /*arrayOfFriendlies = [];
                arrayOfFriendlies.push(currentObjective.pName.toLowerCase());
                arrayOfFriendlies.push("turret");
                arrayOfFriendlies.push("merchant");
                arrayOfFriendlies.push("miner");
                arrayOfFriendlies.push("patrol");
                arrayOfFriendlies.push("repair");
                arrayOfFriendlies.push("mining");
                arrayOfFriendlies.push("combat");
                arrayOfFriendlies.push("scurvy");
                storage.SetGlobal("FriendliesList" + currentObjective.pid, arrayOfFriendlies);*/
            }
            
            
        }














        ///var playerAskedMeToGetInFormation = storage.SetGlobal("currentCommand" + id1, { id: id1, command: 0, formation: chosenPosition, lastCommand: 0, wepPropType: weaponPropulsionType, wepDistType: weaponDistanceType, droneIndex: index, addFriend: 2 }); //, behavior: behavior

        /*var playerDocked = ship.IsDocked(currentObjective.pid);
        //var npcDocked = ship.IsDocked(currentObjective.nid);
        //var isNPCDocked = npc.IsOnBase(nextCommandToDispatch.id);
        var jumpGateToTravel = ship.EnteringJumpgate(currentObjective.pid);

        if (playerDocked)
        {
            npc.InstantStop(currentObjective.nid);
            
            var currentCom = storage.GetGlobal("currentCommand" + currentObjective.nid);
            currentCom.command = 2;
            storage.SetGlobal("currentCommand" + currentObjective.nid, currentCom);

            var current_base = ship.GetCurrentBase(currentObjective.pid);
            var sys_idPlayer = ship.GetSystemID(currentObjective.pid);
            var theBase = storage.GetGlobal("system_" + sys_idPlayer + "_base_" + current_base + "_xmlStationType");


            npc.NextObjective(currentObjective.nid);
            npc.CleanObjectives(currentObjective.nid);
            return;
        }
        else if (jumpGateToTravel != 0)
        {
            console.PrintError("player has jumped");
            var currentCom = storage.GetGlobal("currentCommand" + currentObjective.nid);
            currentCom.command = 3;
            storage.SetGlobal("currentCommand" + currentObjective.nid, currentCom);

            npc.NextObjective(currentObjective.nid);
            npc.CleanObjectives(currentObjective.nid);
            return;
        } */

        if (addFriendSwitch == 1)
        {
            var playerToAddToFriendlies = storage.GetGlobal("FriendlyToAdd" + currentObjective.pid);
            arrayOfFriendlies.push(playerToAddToFriendlies.toLowerCase());

            var glob = storage.GetGlobal("currentCommand" + currentObjective.nid);
            glob.addFriend = 0;
            storage.SetGlobal("currentCommand" + currentObjective.nid, glob);
        }
        else if (addFriendSwitch == 2) // Add the player for starters and ALL NPCS of the game.... which aren't all setup yet.
        {
            if (storage.IsSetGlobal("FriendliesList" + currentObjective.pid)) {
                var currentList = storage.GetGlobal("FriendliesList" + currentObjective.pid);
                arrayOfFriendlies = currentList;


                for (var i = 0; i < currentList.length; i++) {
                    console.PrintError(currentList[i]);
                }

                var glob = storage.GetGlobal("currentCommand" + currentObjective.nid);
                glob.addFriend = 0;
                storage.SetGlobal("currentCommand" + currentObjective.nid, glob);
            }
            else // if the global friendlist list for that player is not set, set it now.
            {
                arrayOfFriendlies = [];
                arrayOfFriendlies.push(currentObjective.pName.toLowerCase());
                arrayOfFriendlies.push("turret");
                arrayOfFriendlies.push("merchant");
                arrayOfFriendlies.push("miner");
                arrayOfFriendlies.push("patrol");
                arrayOfFriendlies.push("repair");
                arrayOfFriendlies.push("mining");
                arrayOfFriendlies.push("combat");
                arrayOfFriendlies.push("scurvy");

                storage.SetGlobal("FriendliesList" + currentObjective.pid, arrayOfFriendlies);

                var glob = storage.GetGlobal("currentCommand" + currentObjective.nid);
                glob.addFriend = 0;
                storage.SetGlobal("currentCommand" + currentObjective.nid, glob);
            }
        }
        else if (addFriendSwitch == 3) // Add the player for starters and ALL NPCS of the game.... which aren't all setup yet.
        {
            var playerToRemoveFromFriendlies = storage.GetGlobal("FriendlyToAdd" + currentObjective.pid);
            var indexOfToRemovePlayer = playerToRemoveFromFriendlies.index;
            arrayOfFriendlies.splice(indexOfToRemovePlayer, 1);

            var glob = storage.GetGlobal("currentCommand" + currentObjective.nid);
            glob.addFriend = 0;
            storage.SetGlobal("currentCommand" + currentObjective.nid, glob);
        }

        //globIndex = storage.GetGlobal("maxDroneIndex0");

        if (currentObjective.formation == 1) {
            globIndex = storage.GetGlobal("maxDroneIndex0");
        }
        else if (currentObjective.formation == 2) {
            globIndex = storage.GetGlobal("maxDroneIndex1");
        }
        else if (currentObjective.formation == 3) {
            globIndex = storage.GetGlobal("maxDroneIndex2");
        }
        else if (currentObjective.formation == 4) {
            globIndex = storage.GetGlobal("maxDroneIndex3");
        }
        else if (currentObjective.formation == 5) {
            globIndex = storage.GetGlobal("maxDroneIndex4");
        }



        //console.PrintError("ind0: " + currentObjective.droneIndex + " ind0: " + globIndex);

        if (currentObjective.droneIndex <= globIndex)
        {
            hpNPC = ship.GetCurrentValue(currentObjective.nid, "structure");

            if (hpNPC > 0)
            {
                nData = SC_AI_Drone_Get_nData.npcGetSelfNPCData(currentObjective, 1);
            }
            else
            {
                return;
            }
            hpPlayer = ship.GetCurrentValue(currentObjective.pid, "structure");

            if (hpPlayer > 0)
            {
                pData = SC_AI_Drone_Get_pData.npcGetPlayerData(currentObjective, 0);
            }
            //else
            //{
            //  pData = lastArrayOfPlayerData[currentObjective.droneIndex];
            //}

            if (currentObjective.speedSwitch == 1)
            {
                if (nData != null && pData != null)
                {
                    if (cData[currentObjective.droneIndex] == null || ceData[currentObjective.droneIndex] == null)
                    {
                        //console.PrintError("reset: " + currentObjective.droneIndex + " id: " + currentObjective.nid);
                        //Server reset or something...gotta reset all main arrays. gotta re-size all the main arrays to the current droneIndex in the server.
                        cData = [];
                        ceData = [];
                        addToArray(currentObjective, globIndex, 0);
                        return;
                    }
                    cData[currentObjective.droneIndex].nData = nData;
                    cData[currentObjective.droneIndex].pData = pData;
                    cData[currentObjective.droneIndex].objt = currentObjective;

                    ceData[currentObjective.droneIndex].nData = nData;
                    ceData[currentObjective.droneIndex].pData = pData;
                    ceData[currentObjective.droneIndex].pLData = pData;
                    ceData[currentObjective.droneIndex].objt = currentObjective;
                    //console.PrintError("id: " + currentObjective.nid + " ___ " + currentObjective.command);

                    //console.PrintError(currentObjective.droneIndex);

                    if (currentObjective.command == 0) // || currentObjective.command == 1
                    {
                        if (currentObjective.command == 0) {
                            //console.PrintError("0ind: " + currentObjective.droneIndex + " id: " + currentObjective.nid);
                            currentFormationWaypoint = SC_AI_Drone_Combat_cc_Rout_For_FWP_1.npcGWFP(currentObjective, 5);
                            distToWaypoint = SC_Utilities.GetDistance(currentFormationWaypoint, nData.nCoord);

                            cData[currentObjective.droneIndex].cFWP = currentFormationWaypoint;
                            cData[currentObjective.droneIndex].cFWPD = distToWaypoint;

                            if (distToWaypoint >= 25) {
                                //console.PrintError("id: " + currentObjective.nid + "too far");
                                npc.Unstick(currentObjective.nid);
                                npc.Unlock(currentObjective.nid);
                                npc.StickToPoint(currentObjective.nid, cData[currentObjective.droneIndex].cFWP.x, cData[currentObjective.droneIndex].cFWP.y, 0);
                            }
                            else {
                                dataToReturn = SC_AI_Drone_Combat_cc_Rout_Com_1.AICombatRoutine(cData[currentObjective.droneIndex], ceData[currentObjective.droneIndex]);
                                //console.PrintError("id: " + currentObjective.nid);
                                cData[currentObjective.droneIndex] = dataToReturn.forData;
                                ceData[currentObjective.droneIndex] = dataToReturn.comData;
                            }
                        }
                        /*else if (currentObjective.command == 1)
                        {
                            currentFormationWaypoint = SC_AI_Drone_Combat_cc_Rout_For_FWPB_1.npcGWFPB(currentObjective, 5);
                            distToWaypoint = SC_Utilities.GetDistance(currentFormationWaypoint, nData.nCoord);

                            //stay near base entrance... to do so, find the station type with getting the baseID and then getting the Global Info of that station.
                            //if outpost, find the spawn point of turrets and select a random spot there to stay in "formation" but keep the same behavior
                            //in order to do that, i have to change the "attack script to get the distance to the current selected Point of PROTECTION instead of just the player."
                        }*/                    
                    }
                }
                else if (nData != null && pData == null)
                {
                    if (ceData[currentObjective.droneIndex].pLData != null)
                    {
                        console.PrintError("check if player is docked? or destroyed");
                    }
                }
            }
            else if (currentObjective.speedSwitch == 0)
            {
                SC_AI_Drone_Combat_cc_Reset_Speed_1.AICombatResetSpeed(currentObjective);
            }
        }
        else if (currentObjective.droneIndex > globIndex)
        {
            console.PrintError("adding drone index0000");
            addToArray(currentObjective, globIndex, 1);
            // storage.SetGlobal("maxDroneIndex0", globIndex);
        }
    }
};

function addToArray(currentObjective, globIndex , switcher)
{
    //console.PrintError("some fucking problem00");
    for (var i = 0; i < (currentObjective.droneIndex + 1) - globIndex; i++) {

        cDat =
            {
                nData: null,
                pData: null,
                pLData: null,
                objt: currentObjective,
                cFWP: { x: null, y: null },
                cLFWP: { x: null, y: null },
                cFWPD: 0,

                fSwtc: 1,
                mSwtc: 0,
                lsP: { x: null, y: null },
                ltP: { x: null, y: null },
                liP: { x: null, y: null },
                gliP: { x: null, y: null },

                dog: [],
                log: [],
                pfc: -2,
                cdSwtc: 0,
                cdtSwtc: 0,
                loRLDOT: 0,
                stopS: 1,
                stopSC: 0,
                stopSCM: 5,
                noFWP: 0
            };
        cData.push(cDat);

        ceDat =
            {
                nData: null,
                pData: null,
                eData: null,
                arrF: arrayOfFriendlies,
                objt: currentObjective,
                ts: -1,
                lLSwtc: 0,
                hasLoC: 0,
                hasLoFC: 0,
                hasStPC: 0,
                hasStOC: 0,
                hasNAC: 0,
                hasNA: 0,
                hasLo: 0,
                hasStP: 0,
                hasStO: 0,
                hasLoF: 0,
                tec: 0,
                tecC: 0,
                mReset: 0,
                evad: 0,
                evadC: 0,
                tec0: 0,
                tec0C: 0,
                tec2: 0,
                tec2C: 0,
                loRLDOT: 0,
                tec3: 0,
                tec3C: 0,
                dLStrc: 0,
                eneL: 0,
                eneLD: 0,
                eToP: 0,
                dToE: 0,
                eneDC: 0,
                eneDSwtc: 0,
                fSwtc: 0,

                eneLSh: 0,
                npcLD: 0,

                forProD: 0,
                forProDC: { x: null, y: null },
                //cdSwtc: switchesForChangeDir,
                //cdtSwtc: switchesForChangeDirTWO

                stopS: null,
                stopSC: null
            };
        ceData.push(ceDat);
        globIndex++;
    }
    if (switcher == 1)
    {
        //console.PrintError("some fucking problem");
        if (currentObjective.formation == 1) {
            storage.SetGlobal("maxDroneIndex0", globIndex);
        }
        else if (currentObjective.formation == 2) {
            storage.SetGlobal("maxDroneIndex1", globIndex);
        }
        else if (currentObjective.formation == 3) {
            storage.SetGlobal("maxDroneIndex2", globIndex);
        }
        else if (currentObjective.formation == 4) {
            storage.SetGlobal("maxDroneIndex3", globIndex);
        }
        else if (currentObjective.formation == 5) {
            storage.SetGlobal("maxDroneIndex4", globIndex);
        }
    }
    

    //storage.SetGlobal("maxDroneIndex0", globIndex);



}
