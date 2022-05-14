using System;

namespace sccs
{
    public class SC_GlobalsVoxelInstancing
    {

        //personal record - 402 million vertices and 603 million triangles - was able to be loaded in the same scene, with extreme lag, but it loaded and i was able to be in the scene. 
        //i thought 4 million vertices was pushing any video cards/cpu at all times?! ryzen 2600 8gb ram msi rx570 4gb video card.

        public int tinyChunkWidth = 4; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION
        public int tinyChunkHeight = 4; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION
        public int tinyChunkDepth = 4; // CANNOT BE CHANGED FOR THE MOMENT. VERTEX BINDING LIMITATION

        public int numberOfInstancesPerObjectInWidth = 1; // CAN BE CHANGED // 192 //96
        public int numberOfInstancesPerObjectInHeight = 1; // CAN BE CHANGED // 108// 54
        public int numberOfInstancesPerObjectInDepth = 1; // CAN BE CHANGED

        public int numberOfObjectInWidth = 1; // CAN BE CHANGED
        public int numberOfObjectInHeight = 1; // CAN BE CHANGED
        public int numberOfObjectInDepth = 1; // CAN BE CHANGED

        //THIS SETTING WORKS AT 1f and 0.1f. OTHERWISE FAILING THE PERLIN NOISE IN THE chunk.cs script.
         
        public float planeSize = 1.0f; 
        //1         SUCCESS IN POSITIVE AXIS ONLY.
        //0.1f      WIP4DESTRUCTVOXEL
        //0.01f     WIP4DESTRUCTVOXEL
        //0.001f    WIP4DESTRUCTVOXEL
    }
}





//4*4*4 x 4*4*4 x 4*4*4 x 6 faces x 4 vertices =  6,291,456 vertices // 9,437,184 faces

