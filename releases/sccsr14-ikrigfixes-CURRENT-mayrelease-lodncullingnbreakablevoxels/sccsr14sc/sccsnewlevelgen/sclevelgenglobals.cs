using System;

namespace sccs
{
    public class sclevelgenglobals
    {
        public int tinyChunkWidth = 1; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION
        public int tinyChunkHeight = 1; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION
        public int tinyChunkDepth = 1; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION

        public int numberOfInstancesPerObjectInWidth = 10; // CAN BE CHANGED
        public int numberOfInstancesPerObjectInHeight = 10; // CAN BE CHANGED
        public int numberOfInstancesPerObjectInDepth = 1; // CAN BE CHANGED

        public int numberOfObjectInWidth = 10; // CAN BE CHANGED
        public int numberOfObjectInHeight = 10; // CAN BE CHANGED
        public int numberOfObjectInDepth = 1; // CAN BE CHANGED

        //THIS SETTING WORKS AT 1f and 0.1f. OTHERWISE FAILING THE PERLIN NOISE IN THE chunk.cs script.

        public float planeSize = 0.1f;
    }
}
