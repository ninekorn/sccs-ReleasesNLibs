using System;
using System.Collections.Generic;
using System.Text;
using SharpDX;

namespace _sc_core_systems.SC_pathfind_assets
{
    public struct sc_node
    {
        public int _is_connected;
        //var startNode = { boolWalk: boolWalker, worldPosition: cData.lsp, gcost: 0, hcost: 0, fcost: 0, gridTileX: x, gridTileY: y, index: index, parent: parenter, gridIndex: indexOfGrid, gridPos: cData.glip, open: 0, closed: 0 };
        public int can_walk;
        public Vector3 worldPosition;
        public float gcost;
        public float hcost;
        public float fcost;
        public float gridTileX;
        public float gridTileY;
        public float gridTileZ;
        public int index;
        public int parent_grid_index;
        public int parent_node_index;
        public int gridIndex;
        public Vector3 gridPos;
        public int closed;
        public int opened;
    }
}
