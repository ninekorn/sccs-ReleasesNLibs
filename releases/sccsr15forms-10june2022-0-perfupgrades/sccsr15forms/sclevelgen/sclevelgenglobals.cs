using System;

namespace sccsr15forms
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

        public int widthlod0 = 10;
        public int heightlod0 = 10;
        public int depthlod0 = 10;

        public int widthlod1 = 5;
        public int heightlod1 = 5;
        public int depthlod1 = 5;

        public int widthlod2 = 3;
        public int heightlod2 = 3;
        public int depthlod2 = 3;

        public int widthlod3 = 2;
        public int heightlod3 = 2;
        public int depthlod3 = 2;

        public float planeSize = 0.1f;
    }
}
