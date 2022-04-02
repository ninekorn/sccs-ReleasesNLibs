using System;

namespace sccs
{
    public static class SC_Globals
    {
        public const int tinyChunkWidth = 1; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION
        public const int tinyChunkHeight = 1; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION
        public const int tinyChunkDepth = 1; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION

        public const int numberOfInstancesPerObjectInWidth = 10; // CAN BE CHANGED
        public const int numberOfInstancesPerObjectInHeight = 10; // CAN BE CHANGED
        public const int numberOfInstancesPerObjectInDepth = 1; // CAN BE CHANGED

        public const int numberOfObjectInWidth = 10; // CAN BE CHANGED
        public const int numberOfObjectInHeight = 10; // CAN BE CHANGED
        public const int numberOfObjectInDepth = 1; // CAN BE CHANGED

        //THIS SETTING WORKS AT 1f and 0.1f. OTHERWISE FAILING THE PERLIN NOISE IN THE chunk.cs script.

        public const float planeSize = 0.1f;
    }
}
