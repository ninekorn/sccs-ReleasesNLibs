using System;
using System.Collections.Generic;
using System.Text;
using SharpDX;

namespace _sc_core_systems.SC_pathfind_assets
{
    public struct sc_curData
    {
        public sc_aiData? aiData;
        public sc_pData? pData;
        public sc_eData? eData;

        public Vector3? cFWP;
        public Vector3? cLFWP;
        public int cFWPD;
        public int fSwtc;
        public int mSwtc;
        public int sSwtc;

        public Vector3? lsp;
        public Vector3? ltp;
        public Vector3? lip;
        public Vector3? glip;

        public int pfc;
        public int cdSwtc;
        public int cdtSwtc;
        public int loRLDOT;
        public int stopS;
        public int stopSC;
        public int stopSCM;
        public int noFWP;

        public List<sc_node> openset; //dog
        public sc_node[] data_of_grid; //dog
        public List<sc_node> log; //log // last data of grid ldog i think im not sure. reviewing.
    }
}





/*
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
    mSwtc: 1,
    lsP: { x: null, y: null },
    ltP: { x: null, y: null },
    liP: { x: null, y: null },
    gliP: { x: null, y: null },
    sSwtc: 1,
dog: { openSet: null, currentCommand: null, grid: null, path: null, node: null, iot: null },

    log: [],
    pfc: -2,
    stopS: 1,
    stopSC: 0,
    stopSCM: 5,
    stRot: 0,
    stCoord: { x: null, y: null },
    xtra: null
};

cData.push(cDat);*/












