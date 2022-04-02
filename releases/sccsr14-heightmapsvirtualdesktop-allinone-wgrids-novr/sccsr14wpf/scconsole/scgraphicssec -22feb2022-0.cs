using System;
using System.Collections.Generic;
using System.Text;
using SharpDX;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;
using scmessageobject = sccs.scmessageobject.scmessageobject;
using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;
using scgraphicssecpackage = sccs.scmessageobject.scgraphicssecpackage;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Ab3d.OculusWrap;
using sccs.scgraphics;
using Ab3d.OculusWrap.DemoDX11;
using System.Runtime.InteropServices;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Jitter.DataStructures;
using SingleBodyConstraints = Jitter.Dynamics.Constraints.SingleBody;
using Jitter.Dynamics.Constraints;
using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;
using Vector4 = SharpDX.Vector4;
using Quaternion = SharpDX.Quaternion;
using Matrix = SharpDX.Matrix;
using Plane = SharpDX.Plane;
using Ray = SharpDX.Ray;
using SharpDX.Multimedia;
using SharpDX.IO;
using System.Xml;
using SharpDX.XAudio2;
using System.Linq;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

//using System.Windows.Forms;
using System.IO.Ports;

using SharpDX.D3DCompiler;

namespace sccs.scgraphics
{
    public class scgraphicssec //: sccs.scgraphics.scupdate//SC_Intermediate_Update
    {




        int bitmapcounter = 0;
        float lightrotationspeed = 0.25f;

        Quaternion _testQuater;
        FastNoise fastNoise = new FastNoise();

        int setoncevrdesktop = 0;
        public static int activatevrheightmapfeature = 0;

        public static int activatelevelgen = 0;


        public static int activatenotyetinfinitevoxelterrain = 0;

        public static int createinstancedvd = 1;


        public int createikrig = 0;

        scgraphicssecpackage scgraphicssecpackagemessage;

        public int mapWidth = 20;
        public int mapHeight = 1;
        public int mapDepth = 20;

        public int tinyChunkWidth = 20;
        public int tinyChunkHeight = 20;
        public int tinyChunkDepth = 20;

        public int mapObjectInstanceWidth = 1;
        public int mapObjectInstanceHeight = 1;
        public int mapObjectInstanceDepth = 1;

        float _planeSize = 0.1f;
        int InstanceCount = 0;

        /*SC_VR_Chunk[] arrayOfChunks;

        //SC_VR_Chunk.DInstanceType[] instances;


        sclevelgenchunk[] arrayoflevelchunk;
        sclevelgenchunk[] arrayoflevelchunkfloortiles;
        sclevelgenchunk[] arrayoflevelfrontwall;
        sclevelgenchunk[] arrayoflevelbackwall;
        sclevelgenchunk[] arrayoflevelrightwall;
        sclevelgenchunk[] arrayoflevelleftwall;
        sclevelgenchunk[] arrayoflevelleftfrontinsidecorner;
        sclevelgenchunk[] arrayoflevelrightfrontinsidecorner;
        sclevelgenchunk[] arrayoflevelleftbackinsidecorner;
        sclevelgenchunk[] arrayoflevelrightbackinsidecorner;
        sclevelgenchunk[] arrayoflevelleftfrontoutsidecorner;
        sclevelgenchunk[] arrayoflevelrightfrontoutsidecorner;
        sclevelgenchunk[] arrayoflevelleftbackoutsidecorner;
        sclevelgenchunk[] arrayoflevelrightbackoutsidecorner;*/




        //sclevelgenchunk.DInstanceType[] instances;


        int playerusebrush = 0;


        //HUMAN RIG
        int tempMultiInstancePhysicsTotal = 1;
        int _human_inst_rig_x = 1;
        int _human_inst_rig_y = 1;
        int _human_inst_rig_z = 1;
        const int _addToWorld = 0;

        public scgraphicssec() //sccs.scgraphics.scdirectx _sccs.scgraphics.scdirectx, IntPtr _HWND
        {

        }

        World thejitterworld;
        World thejitterworld1;
        object somedata0;
        object somedata00;



        public int somevoxelvirtualdesktopw = 1;
        public int somevoxelvirtualdesktoph = 1;
        public int somevoxelvirtualdesktopd = 1;
        public SC_instancedChunkPrim[] somevoxelvirtualdesktop;
        public static SC_GlobalsVoxelInstancing somevoxelvirtualdesktopglobals;
        Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        Vector3 lightDirection = new Vector3(0, -1, -1);
        Vector3 dirLight = new Vector3(0, -1, 0);
        Vector3 lightpos = new Vector3(0, 20, 0);





        /*
        sccsikvoxellimbs[] ikarmvoxel;
        //Matrix[][][][] worldMatrix_instances_ikarmvoxel;
        int somechunkpriminstancesikarmvoxelwidthR = 2;
        int somechunkpriminstancesikarmvoxelheightR = 2;
        int somechunkpriminstancesikarmvoxeldepthR = 0;
        int somechunkpriminstancesikarmvoxelwidthL = 0;
        int somechunkpriminstancesikarmvoxelheightL = 0;
        int somechunkpriminstancesikarmvoxeldepthL = 0;



        public static sccsikvoxellimbs[] ikvoxelbody;
        // Matrix[][][][] worldMatrix_instances_ikvoxelbody;
        int somechunkpriminstancesikvoxelbodywidthR = 1;
        int somechunkpriminstancesikvoxelbodyheightR = 0;
        int somechunkpriminstancesikvoxelbodydepthR = 0;
        int somechunkpriminstancesikvoxelbodywidthL = 0;
        int somechunkpriminstancesikvoxelbodyheightL = 0;
        int somechunkpriminstancesikvoxelbodydepthL = 0;*/



        Vector3[][][] _array_of_last_frame_voxel_pos;
        Vector3[][][] _array_of_last_frame_cube_pos;
        Vector3[][][] _array_of_last_frame_cone_pos;
        Vector3[][][] _array_of_last_frame_cylinder_pos;
        Vector3[][][] _array_of_last_frame_capsule_pos;
        Vector3[][][] _array_of_last_frame_sphere_pos;
        Matrix[] worldMatrix_base;
        Matrix[][][] worldMatrixinstancesvoxelcube;
        Matrix[][][] worldMatrix_instances_screen_assets;

        Matrix[][][] worldMatrix_instances_screens;
        //Matrix[][][] world_last_Matrix_instances_screens;
        Matrix[][][] worldMatrix_instances_cubes;
        Matrix[][][] _screenDirMatrix;

        Vector3[][][] point3DCollection;
        Matrix[][][] _screenDirMatrix_correct_pos;


        /*
        SC_cube[][] _world_cube_list;
        sc_voxel[][] worldvoxelcubelists;
        SC_cube[][] _world_screen_list;
        SC_cube[][] _world_screen_assets_list;*/


        public static Jitter.Forces.Buoyancy _buo;
        Jitter.Forces.Buoyancy[] _buoyancy_area;
        int has_water_buo_effect = -1;
        bool containsCoord;
        JVector rh_attract_force = JVector.Zero;
        JVector lh_attract_force = JVector.Zero;

        //the physics engine can run 4000 objects enabled and having angularOrLinear velocities but the voxels lag a bit at that many objects. but loading as many as 475000 cubes
        //having 36 vertices each and 72 triangles each but at that point it will also lag. will try later to improve the performance.
        float _voxel_mass = 100;

        int _inst_voxel_cube_x = 10;
        int _inst_voxel_cube_y = 1;
        int _inst_voxel_cube_z = 1;

        float _voxel_cube_size_x = 0.15f;//0.0115f //restitution
        float _voxel_cube_size_y = 0.15f;//0.0115f //static friction
        float _voxel_cube_size_z = 0.15f;//0.0015f //kinetic friction

        float voxel_general_size = 0.0025f;
        int voxel_type = -1;

        float _voxel_rig_cube_size_x = 0.15f;//0.0115f //restitution
        float _voxel_rig_cube_size_y = 0.15f;//0.0115f //static friction
        float _voxel_rig_cube_size_z = 0.15f;//0.0015f //kinetic friction

        //PHYSICS CUBES
        int _inst_cube_x = 2;
        int _inst_cube_y = 2;
        int _inst_cube_z = 2;

        int consolewritecounter = 0;
        int consolewritecountermax = 0;
        int consolewritecounterswtc = 0;

        int someinstancebytesindex = 0;
        int resetvoxelladdercounter = 1;
        int resetvoxelladder = 0;


        int somechunkresetcounter = 0;
        int somechunkplayerspatiallocationresetcounter = 0;


        int sc_menu_scroller = 0;
        int sc_menu_scroller_counter = 0;


        double m11 = 0;
        double m12 = 0;
        double m13 = 0;
        double m14 = 0;
        double m21 = 0;
        double m22 = 0;
        double m23 = 0;
        double m24 = 0;
        double m31 = 0;
        double m32 = 0;
        double m33 = 0;
        double m34 = 0;
        double m41 = 0;
        double m42 = 0;
        double m43 = 0;
        double m44 = 0;

        double m11b = 0;
        double m12b = 0;
        double m13b = 0;
        double m14b = 0;
        double m21b = 0;
        double m22b = 0;
        double m23b = 0;
        double m24b = 0;
        double m31b = 0;
        double m32b = 0;
        double m33b = 0;
        double m34b = 0;
        double m41b = 0;
        double m42b = 0;
        double m43b = 0;
        double m44b = 0;

        double m11c = 0;
        double m12c = 0;
        double m13c = 0;
        double m14c = 0;
        double m21c = 0;
        double m22c = 0;
        double m23c = 0;
        double m24c = 0;
        double m31c = 0;
        double m32c = 0;
        double m33c = 0;
        double m34c = 0;
        double m41c = 0;
        double m42c = 0;
        double m43c = 0;
        double m44c = 0;

        double m11d = 0;
        double m12d = 0;
        double m13d = 0;
        double m14d = 0;
        double m21d = 0;
        double m22d = 0;
        double m23d = 0;
        double m24d = 0;
        double m31d = 0;
        double m32d = 0;
        double m33d = 0;
        double m34d = 0;
        double m41d = 0;
        double m42d = 0;
        double m43d = 0;
        double m44d = 0;

        Vector3 rayDirRighter;

        float vertoffsetx;
        float vertoffsety;
        float vertoffsetz;

        Matrix originRotScreen;
        Matrix rotatingMatrixScreen;
        float oriRotationScreenY { get; set; }
        float oriRotationScreenX { get; set; }
        float oriRotationScreenZ { get; set; }

        int _has_locked_screen_pos = 0;
        int _has_locked_screen_pos_counter = 0;
        Matrix _direction_offsetter;
        Matrix _screen_direction_offsetter_two;
        float sizeWidtherer = 0.0f;
        float sizeheighterer = 0.0f;



        float heightmapscale = 0.001f;
        float heightmapscaleMin = 0.0001f;
        float heightmapscaleMax = 100f;

        Vector3[] directionvectoroffsets;

        struct _rigid_data
        {
            public RigidBody _body;
            public Matrix position;
            public Vector3 directionToGrabber;
            public Vector3 rayGrabDir;
            public float rayGrabDirLength;
            public Vector3 grabHitPoint;
            public float grabHitPointLength;
            public float dirDiffX;
            public float dirDiffY;
            public float dirDiffZ;

            public int _index;
            public int _physics_engine_index;
        }

        _rigid_data _grab_rigid_data;


        //SCREEN SETTINGS
        int _inst_screen_x = 1;
        int _inst_screen_y = 1;
        int _inst_screen_z = 1;

        float _screen_size_x = 2; //0.0115f //1.5f
        float _screen_size_y = 2; //0.0115f //1.5f
        float _screen_size_z = 0.0035f; //0.0025f

        float mulScreen = 0.85f;

        int _inst_screen_assets_x = 3;
        int _inst_screen_assets_y = 1;
        int _inst_screen_assets_z = 3;

        float _screen_assets_size_x = 0.005f; //0.0115f //1.5f
        float _screen_assets_size_y = 0.005f; //0.0115f //1.5f
        float _screen_assets_size_z = 0.025f;
        float _size_screen;
        bool is_static = false;
        //END OF
        double RotationScreenY { get; set; }
        double RotationScreenX { get; set; }
        double RotationScreenZ { get; set; }

        /*
        sccs.scgraphics.SC_cube.DLightBuffer[] _DLightBuffer_cube = new SC_cube.DLightBuffer[1];

        sccs.scgraphics.sc_voxel.DLightBuffer[] _DLightBuffer_voxel_cube = new sc_voxel.DLightBuffer[1];*/

        public scmessageobjectjitter[][] _sc_create_world_objects(scmessageobjectjitter[][] _sc_jitter_tasks)
        {
            /*
            if (activatelevelgen == 1)
            {


                LevelGenerator4 levelgen = new LevelGenerator4();

                levelgen.tileAmount = 150;
                levelgen.chunkwidth = 1;
                levelgen.chunkheight = 1;
                levelgen.chunkdepth = 1;
                levelgen.planesize = 1.0f;

                levelgen.chanceUp = 0.25f;
                levelgen.chanceRight = 0.5f;
                levelgen.chanceDown = 0.75f;
                levelgen.SpawnerMoveWaitTime = 0.01f;
                levelgen.BuildingWaitTime = 0.01f;
                levelgen.blockSize = 1.0f;
                levelgen.StartGeneratingVoxelLevel();
                //levelgen.createfinal();


                /*Program.MessageBox((IntPtr)0, "gen" + levelgen.builtFloorTiles.Count , "scmsg", 0);
                Program.MessageBox((IntPtr)0, "gen" + levelgen.createdTiles.Count, "scmsg", 0);
                Program.MessageBox((IntPtr)0, "gen" + levelgen.builtFrontWall.Count, "scmsg", 0);
                Program.MessageBox((IntPtr)0, "gen" + levelgen.builtBackWall.Count, "scmsg", 0);
                Program.MessageBox((IntPtr)0, "gen" + levelgen.builtLeftWall.Count, "scmsg", 0);
                Program.MessageBox((IntPtr)0, "gen" + levelgen.builtRightWall.Count, "scmsg", 0);
                Program.MessageBox((IntPtr)0, "gen" + levelgen.adjacentWall.Count, "scmsg", 0);

                Vector3 originchunkpos = new Vector3(0, 0, -1);


                _planeSize = 0.1f;
                mapWidth = 1;
                mapHeight = 1;
                mapDepth = 1;

                tinyChunkWidth = 10;
                tinyChunkHeight = 10;
                tinyChunkDepth = 10;

                mapObjectInstanceWidth = 1;
                mapObjectInstanceHeight = 1;
                mapObjectInstanceDepth = 1;


                arrayoflevelchunk = new sclevelgenchunk[levelgen.createdTiles.Count];
                var enumerator1 = levelgen.createdTiles.GetEnumerator();
                int ichunkleveltile = 0;
                while (enumerator1.MoveNext())
                {
                    var currentTuile = enumerator1.Current;
                    var chunkPos = currentTuile.Key;
                    arrayoflevelchunk[ichunkleveltile] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]


                    arrayoflevelchunk[ichunkleveltile].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };
                    ichunkleveltile++;
                }





                arrayoflevelchunkfloortiles = new sclevelgenchunk[levelgen.builtFloorTiles.Count];
                for (int w = 0; w < levelgen.builtFloorTiles.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtFloorTiles[w];
                    arrayoflevelchunkfloortiles[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }




                arrayoflevelfrontwall = new sclevelgenchunk[levelgen.builtFrontWall.Count];
                for (int w = 0; w < levelgen.builtFrontWall.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtFrontWall[w];
                    arrayoflevelfrontwall[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelbackwall = new sclevelgenchunk[levelgen.builtBackWall.Count];
                for (int w = 0; w < levelgen.builtBackWall.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtBackWall[w];
                    arrayoflevelbackwall[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelrightwall = new sclevelgenchunk[levelgen.builtRightWall.Count];
                for (int w = 0; w < levelgen.builtRightWall.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtRightWall[w];
                    arrayoflevelrightwall[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelleftwall = new sclevelgenchunk[levelgen.builtLeftWall.Count];
                for (int w = 0; w < levelgen.builtLeftWall.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtLeftWall[w];
                    arrayoflevelleftwall[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelleftfrontinsidecorner = new sclevelgenchunk[levelgen.builtLeftFrontInsideCorner.Count];
                for (int w = 0; w < levelgen.builtLeftFrontInsideCorner.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtLeftFrontInsideCorner[w];
                    arrayoflevelleftfrontinsidecorner[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelrightfrontinsidecorner = new sclevelgenchunk[levelgen.builtRightFrontInsideCorner.Count];
                for (int w = 0; w < levelgen.builtRightFrontInsideCorner.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtRightFrontInsideCorner[w];
                    arrayoflevelrightfrontinsidecorner[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelleftbackinsidecorner = new sclevelgenchunk[levelgen.builtLeftBackInsideCorner.Count];
                for (int w = 0; w < levelgen.builtLeftBackInsideCorner.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtLeftBackInsideCorner[w];
                    arrayoflevelleftbackinsidecorner[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelrightbackinsidecorner = new sclevelgenchunk[levelgen.builtRightBackInsideCorner.Count];
                for (int w = 0; w < levelgen.builtRightBackInsideCorner.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtRightBackInsideCorner[w];
                    arrayoflevelrightbackinsidecorner[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }



                arrayoflevelleftfrontoutsidecorner = new sclevelgenchunk[levelgen.builtLeftFrontOutsideCorner.Count];
                for (int w = 0; w < levelgen.builtLeftFrontOutsideCorner.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtLeftFrontOutsideCorner[w];
                    arrayoflevelleftfrontoutsidecorner[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelrightfrontoutsidecorner = new sclevelgenchunk[levelgen.builtRightFrontOutsideCorner.Count];
                for (int w = 0; w < levelgen.builtRightFrontOutsideCorner.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtRightFrontOutsideCorner[w];
                    arrayoflevelrightfrontoutsidecorner[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelleftbackoutsidecorner = new sclevelgenchunk[levelgen.builtLeftBackOutsideCorner.Count];
                for (int w = 0; w < levelgen.builtLeftBackOutsideCorner.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtLeftBackOutsideCorner[w];
                    arrayoflevelleftbackoutsidecorner[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }

                arrayoflevelrightbackoutsidecorner = new sclevelgenchunk[levelgen.builtRightBackOutsideCorner.Count];
                for (int w = 0; w < levelgen.builtRightBackOutsideCorner.Count; w++)
                {
                    Vector3 chunkPos = levelgen.builtRightBackOutsideCorner[w];
                    arrayoflevelrightbackoutsidecorner[w] = new sclevelgenchunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize, levelgen); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                }
            }*/


            /*
            sclevelgenchunk[] arrayoflevelfrontwall;
            sclevelgenchunk[] arrayoflevelbackwall;
            sclevelgenchunk[] arrayoflevelrightwall;
            sclevelgenchunk[] arrayoflevelleftwall;
            sclevelgenchunk[] arrayoflevelleftfrontinsidecorner;
            sclevelgenchunk[] arrayoflevelrightfrontinsidecorner;
            sclevelgenchunk[] arrayoflevelleftbackinsidecorner;
            sclevelgenchunk[] arrayoflevelrightbackinsidecorner;
            sclevelgenchunk[] arrayoflevelleftfrontoutsidecorner;
            sclevelgenchunk[] arrayoflevelrightfrontoutsidecorner;
            sclevelgenchunk[] arrayoflevelleftbackoutsidecorner;
            sclevelgenchunk[] arrayoflevelrightbackoutsidecorner;
            */






















            /*
            if (activatenotyetinfinitevoxelterrain == 1)
            {
                //builtFloorTiles

                //CHUNK CREATION
                arrayOfChunks = new SC_VR_Chunk[mapObjectInstanceWidth * mapObjectInstanceHeight * mapObjectInstanceDepth];
                Vector3 originchunkpos = new Vector3(0, 0, -1);
                InstanceCount = mapWidth * mapHeight * mapDepth; //(widther * heigther * depther)
                                                                 //instances = new SC_VR_Chunk.DInstanceType[mapWidth * mapHeight * mapDepth]; //(widther * heigther * depther)

                for (int x = 0; x < mapObjectInstanceWidth; x++)
                {
                    for (int y = 0; y < mapObjectInstanceHeight; y++)
                    {
                        for (int z = 0; z < mapObjectInstanceDepth; z++)
                        {
                            Vector3 chunkPos = new Vector3(x, y, z) + originchunkpos;

                            chunkPos.X = chunkPos.X * (mapWidth * tinyChunkWidth * _planeSize);// * (mapWidth * tinyChunkWidth * _planeSize); //4
                            chunkPos.Y = chunkPos.Y * (mapHeight * tinyChunkHeight * _planeSize);// * (mapHeight * tinyChunkHeight * _planeSize); //1
                            chunkPos.Z = chunkPos.Z * (mapDepth * tinyChunkDepth * _planeSize);// * (mapDepth * tinyChunkDepth * _planeSize); //4

                            //instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)] = new SC_VR_Chunk.DInstanceType[InstanceCount];
                            arrayOfChunks[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)] = new SC_VR_Chunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth, _planeSize); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                        }
                    }
                }
            }*/











            directionvectoroffsets = new Vector3[4];

            for (int i = 0; i < directionvectoroffsets.Length; i++)
            {
                directionvectoroffsets[i] = Vector3.Zero;
            }


            //Program.MessageBox((IntPtr)0, "test0", "scmsg", 0);           

            float r = 0; //0.75f
            float g = 0; //0.75f
            float b = 0; //0.75f
            float a = 1;
            Matrix _object_worldmatrix = Matrix.Identity;
            float offsetPosX;
            float offsetPosY;
            float offsetPosZ;



            if (Program.usejitterphysics == 1)
            {
                //SETTING UP SINGLE WORLD OBJECTS
                //END OF LOOP FOR PHYSICS ENGINE INSTANCES
                somedata0 = (object)_sc_jitter_tasks[0][0]._world_data[0];
                //World[] _jitter_worlds0 = (World[])somedata0;
                thejitterworld = (World)somedata0;
            }


            worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;


            _screenDirMatrix = new Matrix[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];
            _screenDirMatrix_correct_pos = new Matrix[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];
            point3DCollection = new Vector3[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];


            //_world_cube_list = new SC_cube[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];
            //worldvoxelcubelists = new sc_voxel[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];
            worldMatrixinstancesvoxelcube = new Matrix[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];
            _array_of_last_frame_cube_pos = new Vector3[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];
            _array_of_last_frame_voxel_pos = new Vector3[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];

            worldMatrix_instances_screens = new Matrix[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];
            //_world_screen_list = new SC_cube[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];
            //_world_screen_assets_list = new SC_cube[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];
            worldMatrix_instances_screen_assets = new Matrix[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];





            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
            {
                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                {
                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                    {
                        var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);
                        Vector3 physics_engine_offset_pos = new Vector3(xx * 2, yy * 2, zz * 2);


                        //worldMatrix_instances_screens[indexer00] = new Matrix[Program.worldwidth * Program.worldheight * Program.worlddepth][];
                        worldMatrixinstancesvoxelcube[indexer00] = new Matrix[Program.worldwidth * Program.worldheight * Program.worlddepth][];
                        //_world_cube_list[indexer00] = new SC_cube[Program.worldwidth * Program.worldheight * Program.worlddepth];
                        //worldvoxelcubelists[indexer00] = new sc_voxel[Program.worldwidth * Program.worldheight * Program.worlddepth];
                        worldMatrixinstancesvoxelcube[indexer00] = new Matrix[Program.worldwidth * Program.worldheight * Program.worlddepth][];
                        _array_of_last_frame_cube_pos[indexer00] = new Vector3[Program.worldwidth * Program.worldheight * Program.worlddepth][];
                        _array_of_last_frame_voxel_pos[indexer00] = new Vector3[Program.worldwidth * Program.worldheight * Program.worlddepth][];
                        //_world_screen_list[indexer00] = new SC_cube[Program.worldwidth * Program.worldheight * Program.worlddepth];


                        float offsetCubeY = 0;
                        float offsetVoxelY = 0;
                        float offsetCapsuleY = 0;
                        float offsetConeY = 0;
                        float offsetCylinderY = 0;
                        float offsetSphereY = 0;


                        for (int x = 0; x < Program.worldwidth; x++)
                        {
                            for (int y = 0; y < Program.worldheight; y++)
                            {
                                for (int z = 0; z < Program.worlddepth; z++)
                                {
                                    var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);

                                    Vector3 world_pos_offset = new Vector3(x * 2, y * 2, z * 2);
                                    if (Program.usejitterphysics == 1)
                                    {

                                        somedata00 = (object)_sc_jitter_tasks[indexer00][indexer01]._world_data[0];
                                        //World _jitter_worlds = (World)_some_data_00;
                                        thejitterworld1 = (World)somedata00;//= _jitter_worlds[0];

                                    }



                                    if (createinstancedvd == 1)
                                    {



                                        //VOXEL VIRTUAL DESKTOP
                                        //VOXEL VIRTUAL DESKTOP
                                        //VOXEL VIRTUAL DESKTOP
                                        somevoxelvirtualdesktopglobals = new SC_GlobalsVoxelInstancing();

                                        somevoxelvirtualdesktopglobals.planeSize = 0.005f; // * 10
                                        somevoxelvirtualdesktopglobals.tinyChunkWidth = 8; // 4  // 8 // 4  // 8
                                        somevoxelvirtualdesktopglobals.tinyChunkHeight = 8; // 4 // 8 // 4 // 8
                                        somevoxelvirtualdesktopglobals.tinyChunkDepth = 1; // 4 // 8 // 1 // 1 
                                        somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth = 240; //256 // 512¸// 240 // 480 // 128 // 160  //320 // 192
                                        somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight = 135; //128 // 256 // 135 // 270 // 72 // 90 //180 //108
                                        somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth = 1;
                                        somevoxelvirtualdesktopglobals.numberOfObjectInWidth = 1; // cannot change the value
                                        somevoxelvirtualdesktopglobals.numberOfObjectInHeight = 1; // cannot change the value
                                        somevoxelvirtualdesktopglobals.numberOfObjectInDepth = 1; // cannot change the value


                                        somevoxelvirtualdesktop = new SC_instancedChunkPrim[somevoxelvirtualdesktopw * somevoxelvirtualdesktoph * somevoxelvirtualdesktopd];

                                        //Vector3 originvirtualdesktoppos = new Vector3(0, 0.5f, 15);
                                        //Vector3 originsomechunksceneposition = new Vector3(0, 0, 0);

                                        var pitch = (float)(Math.PI * (0) / 180.0f);
                                        var yaw = (float)(Math.PI * (0) / 180.0f);
                                        var roll = (float)(Math.PI * (0) / 180.0f);

                                        var somematrixrot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                                        //somematrixrot = Matrix.Identity;

                                        //Vector3 originvirtualdesktoppos = new Vector3(0, 0.5f, 15);
                                        Vector3 originvirtualdesktoppos = new Vector3(0, 0, 0);

                                        originvirtualdesktoppos.X += sccs.scgraphics.scupdate.originPosScreen.X;
                                        originvirtualdesktoppos.Y += sccs.scgraphics.scupdate.originPosScreen.Y;
                                        originvirtualdesktoppos.Z += -sccs.scgraphics.scupdate.originPosScreen.Z;

                                        //Vector3 originvirtualdesktoppos = new Vector3(-0.4f, 0.4f, 0.4f);
                                        //Vector3 originvirtualdesktoppos = new Vector3(1.333333f * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * somevoxelvirtualdesktopglobals.numberOfObjectInWidth * somevoxelvirtualdesktopglobals.tinyChunkWidth * somevoxelvirtualdesktopglobals.planeSize, 0, 5); // somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth * somevoxelvirtualdesktopglobals.numberOfObjectInDepth * somevoxelvirtualdesktopglobals.tinyChunkDepth * somevoxelvirtualdesktopglobals.planeSize * 10
                                        //Vector3 originvirtualdesktoppos = new Vector3(-1 * 1.25f * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * somevoxelvirtualdesktopglobals.numberOfObjectInWidth * somevoxelvirtualdesktopglobals.tinyChunkWidth * somevoxelvirtualdesktopglobals.planeSize, 1, somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth * somevoxelvirtualdesktopglobals.numberOfObjectInDepth * somevoxelvirtualdesktopglobals.tinyChunkDepth * somevoxelvirtualdesktopglobals.planeSize * 10f);

                                        for (int xxx = 0; xxx < somevoxelvirtualdesktopw; xxx++)
                                        {
                                            for (int yyy = 0; yyy < somevoxelvirtualdesktoph; yyy++)
                                            {
                                                for (int zzz = 0; zzz < somevoxelvirtualdesktopd; zzz++)
                                                {
                                                    int somevoxelindex = xxx + somevoxelvirtualdesktopw * (yyy + somevoxelvirtualdesktoph * zzz);

                                                    //Vector3 somechunkpriminstancepos = new Vector3(xxx, yyy, zzz) * SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfObjectInWidth * SC_Globals.tinyChunkWidth * SC_Globals.planeSize*4;
                                                    Vector3 somevoxelposition = new Vector3(xxx, yyy, zzz);

                                                    somevoxelposition.X *= somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * somevoxelvirtualdesktopglobals.numberOfObjectInWidth * somevoxelvirtualdesktopglobals.tinyChunkWidth * somevoxelvirtualdesktopglobals.planeSize;
                                                    somevoxelposition.Y *= somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * somevoxelvirtualdesktopglobals.numberOfObjectInHeight * somevoxelvirtualdesktopglobals.tinyChunkHeight * somevoxelvirtualdesktopglobals.planeSize;
                                                    somevoxelposition.Z *= somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth * somevoxelvirtualdesktopglobals.numberOfObjectInDepth * somevoxelvirtualdesktopglobals.tinyChunkDepth * somevoxelvirtualdesktopglobals.planeSize;

                                                    somevoxelposition += originvirtualdesktoppos;

                                                    float tempwidth = 0.1f;
                                                    float tempheight = 0.1f;

                                                    somematrixrot.M41 = somevoxelposition.X;
                                                    somematrixrot.M42 = somevoxelposition.Y;
                                                    somematrixrot.M43 = somevoxelposition.Z;

                                                    int swtcshader = 0;



                                                    if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 4 && activatevrheightmapfeature == 1)
                                                    {
                                                        swtcshader = 2;
                                                    }
                                                    else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 8 && activatevrheightmapfeature == 1)
                                                    {
                                                        swtcshader = 3;
                                                    }
                                                    else
                                                    {
                                                        swtcshader = 0;
                                                    }



                                                    //benchmarking and instancing chunked virtual desktops.
                                                    //benchmarking and instancing chunked virtual desktops.
                                                    //benchmarking and instancing chunked virtual desktops.
                                                    somevoxelvirtualdesktop[somevoxelindex] = new SC_instancedChunkPrim();
                                                    somevoxelvirtualdesktop[somevoxelindex].createChunk(sccs.scgraphics.scdirectx.D3D, tempwidth, tempheight, new Vector3(0, 10, 0), new Vector3(0, -1, 0), somevoxelposition, somevoxelindex, 0, 100, thejitterworld1, false, sccs.scgraphics.scdirectx.BodyTag.physicsinstancedvertexbindingchunk, somevoxelvirtualdesktopw, somevoxelvirtualdesktoph, somevoxelvirtualdesktopd, somevoxelvirtualdesktopglobals.numberOfObjectInWidth, somevoxelvirtualdesktopglobals.numberOfObjectInHeight, somevoxelvirtualdesktopglobals.numberOfObjectInDepth, somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth, somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight, somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth, somevoxelvirtualdesktopglobals.tinyChunkWidth, somevoxelvirtualdesktopglobals.tinyChunkHeight, somevoxelvirtualdesktopglobals.tinyChunkDepth, somevoxelvirtualdesktopglobals.planeSize, swtcshader, 1.0f, 1, 0, somevoxelindex, 3, somematrixrot, 1, xxx, yyy, zzz);
                                                    //benchmarking and instancing chunked virtual desktops.
                                                    //benchmarking and instancing chunked virtual desktops.
                                                    //benchmarking and instancing chunked virtual desktops.

                                                    Matrix someinitmat = somevoxelvirtualdesktop[somevoxelindex].worldmatofobj;
                                                    Quaternion somedirquat;
                                                    Quaternion.RotationMatrix(ref someinitmat, out somedirquat);

                                                    var dirInstanceRight = sc_maths._newgetdirleft(somedirquat);
                                                    var dirInstanceUp = sc_maths._newgetdirup(somedirquat);
                                                    var dirInstanceForward = sc_maths._newgetdirforward(somedirquat);

                                                    //position = position - (new Vector4(dirInstanceRight.X, dirInstanceRight.Y, dirInstanceRight.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                                                    //position = position - (new Vector4(dirInstanceUp.X, dirInstanceUp.Y, dirInstanceUp.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                                                    //position = position + (new Vector4(dirInstanceForward.X, dirInstanceForward.Y, dirInstanceForward.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));

                                                    Vector4 somevecx = new Vector4(dirInstanceRight.X, dirInstanceRight.Y, dirInstanceRight.Z, 1.0f);
                                                    Vector4 somevecy = new Vector4(dirInstanceUp.X, dirInstanceUp.Y, dirInstanceUp.Z, 1.0f);
                                                    Vector4 somevecz = new Vector4(dirInstanceForward.X, dirInstanceForward.Y, dirInstanceForward.Z, 1.0f);

                                                    Vector4 somemainpos = new Vector4(someinitmat.M41, someinitmat.M42, someinitmat.M43, 1.0f);
                                                    Vector4 someinstancepostest = somemainpos;// Vector4.Zero;

                                                    //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                                                    //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                                                    //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                                                    someinstancepostest = someinstancepostest + (somevecx * (x * somevoxelvirtualdesktopglobals.tinyChunkWidth * somevoxelvirtualdesktopglobals.planeSize));
                                                    someinstancepostest = someinstancepostest + (somevecy * (y * somevoxelvirtualdesktopglobals.tinyChunkWidth * somevoxelvirtualdesktopglobals.planeSize));
                                                    someinstancepostest = someinstancepostest + (somevecz * (z * somevoxelvirtualdesktopglobals.tinyChunkWidth * somevoxelvirtualdesktopglobals.planeSize));
                                                    //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                                                    //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                                                    //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES

                                                    someinitmat.M41 = someinstancepostest.X;
                                                    someinitmat.M42 = someinstancepostest.Y;
                                                    someinitmat.M43 = someinstancepostest.Z;

                                                    somevoxelvirtualdesktop[somevoxelindex].worldmatofobj = someinitmat;

                                                    somevoxelvirtualdesktop[somevoxelindex].lightBufferInstChunk[0] = new SC_instancedChunkPrim.DLightBufferEr()
                                                    {
                                                        ambientColor = ambientColor,
                                                        diffuseColor = diffuseColour,
                                                        lightDirection = dirLight,
                                                        padding0 = 0,
                                                        lightPosition = lightpos,
                                                        padding1 = 10
                                                    };

                                                    somevoxelvirtualdesktop[somevoxelindex].arrayOfMatrixBuff[0] = new SC_instancedChunkPrim.DMatrixBuffer()
                                                    {
                                                        world = Matrix.Identity,
                                                        view = Matrix.Identity,
                                                        proj = Matrix.Identity,
                                                    };


                                                    break;

                                                    //somevoxelvirtualdesktop[somevoxelindex].somechunk = null;
                                                    //somevoxelvirtualdesktop[somevoxelindex].sccstrigvertbuilder = null;
                                                }
                                            }
                                        }



                                        /*
                                        for (int  i = 0;i < somevoxelvirtualdesktop.Length;i++)
                                        {
                                            somevoxelvirtualdesktop[i] = somevoxelvirtualdesktop[i];
                                        }*/



                                        //VOXEL VIRTUAL DESKTOP
                                        //VOXEL VIRTUAL DESKTOP
                                        //VOXEL VIRTUAL DESKTOP
                                    }






                                    /*
                                    if (createikrig == 1)
                                    {



                                        try
                                        {



                                            //_human_inst_rig_x = 10;
                                            //_human_inst_rig_y = 1;
                                            //_human_inst_rig_z = 10;

                                            ikvoxelbody = new sccsikvoxellimbs[(somechunkpriminstancesikvoxelbodywidthR)];

                                            Vector3 originpositionikvoxelbody = new Vector3(0, 0, 0); //around 0.165f to 0.17f

                                            int grabtargetitem = 1;

                                            Matrix worldmatofobj = Matrix.Identity;
                                            //worldmatofobj.M41 = originpositionikvoxelbody.X;
                                            //worldmatofobj.M42 = originpositionikvoxelbody.Y;
                                            //worldmatofobj.M43 = originpositionikvoxelbody.Z;

                                            //float pelvisvaluey = -0.625f;
                                            //int realtorsowidth = 4;
                                            float ikvoxelrigbodysize = 0.1f;

                                            Vector3 somechunkpriminstanceikvoxelbodypos = Vector3.Zero;

                                            for (int xxx = 0; xxx < somechunkpriminstancesikvoxelbodywidthR; xxx++)
                                            {

                                                float posX = (xxx);
                                                float posY = (0);
                                                float posZ = (0);

                                                var xxi = xxx;
                                                var yyi = 0;
                                                var zzi = 0;

                                                if (xxi < 0)
                                                {
                                                    xxi *= -1;
                                                    xxi = (somechunkpriminstancesikvoxelbodywidthR) + xxi;
                                                }
                                                if (yyi < 0)
                                                {
                                                    yyi *= -1;
                                                    yyi = (somechunkpriminstancesikvoxelbodyheightR) + yyi;
                                                }
                                                if (zzi < 0)
                                                {
                                                    zzi *= -1;
                                                    zzi = (somechunkpriminstancesikvoxelbodydepthR) + zzi;
                                                }

                                                int somechunkpriminstanceikvoxelbodyindex = xxi;

                                                somechunkpriminstanceikvoxelbodypos = ((new Vector3(posX, posY, posZ)) + originpositionikvoxelbody);

                                                ikvoxelbody[somechunkpriminstanceikvoxelbodyindex] = new sccsikvoxellimbs();
                                                _sc_jitter_tasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].createikbody(_sc_jitter_tasks, tempMultiInstancePhysicsTotal, somechunkpriminstanceikvoxelbodypos, null, worldmatofobj, _human_inst_rig_x, _human_inst_rig_y, _human_inst_rig_z, grabtargetitem);


                                            }






                                            grabtargetitem = 0;
                                            ikarmvoxel = new sccsikvoxellimbs[(somechunkpriminstancesikarmvoxelwidthR) + (somechunkpriminstancesikarmvoxelheightR)];

                                            Vector3 originpositionikarmvoxel = new Vector3(0, 0, 0);

                                            //float pelvisvaluey = -0.625f;
                                            //int realtorsowidth = 4;
                                            //float ikvoxelrigsize = 0.1f;

                                            Vector3 somechunkpriminstanceikarmvoxelpos = Vector3.Zero;

                                            for (int xxx = 0; xxx < somechunkpriminstancesikarmvoxelwidthR; xxx++)
                                            {
                                                for (int yyy = 0; yyy < somechunkpriminstancesikarmvoxelheightR; yyy++)
                                                {


                                                    float posX = (xxx);
                                                    float posY = (yyy);
                                                    float posZ = (0);

                                                    var xxi = xxx;
                                                    var yyi = yyy;
                                                    var zzi = 0;

                                                    if (xxi < 0)
                                                    {
                                                        xxi *= -1;
                                                        xxi = (somechunkpriminstancesikarmvoxelwidthR) + xxi;
                                                    }
                                                    if (yyi < 0)
                                                    {
                                                        yyi *= -1;
                                                        yyi = (somechunkpriminstancesikarmvoxelheightR) + yyi;
                                                    }
                                                    if (zzi < 0)
                                                    {
                                                        zzi *= -1;
                                                        zzi = (somechunkpriminstancesikarmvoxeldepthR) + zzi;
                                                    }

                                                    int somechunkpriminstanceikarmvoxelindex = xxi + (yyi * (somechunkpriminstancesikarmvoxelheightR));

                                                    somechunkpriminstanceikarmvoxelpos = ((new Vector3(posX, posY, posZ)) + originpositionikarmvoxel);


                                                    if (somechunkpriminstanceikarmvoxelindex == 0) // bottom left
                                                    {

                                                        somechunkpriminstanceikarmvoxelpos.X = 0;
                                                        somechunkpriminstanceikarmvoxelpos.Y = 0;

                                                    }
                                                    else if (somechunkpriminstanceikarmvoxelindex == 1) // top right
                                                    {

                                                        somechunkpriminstanceikarmvoxelpos.X = 0;
                                                        somechunkpriminstanceikarmvoxelpos.Y = 0;

                                                    }
                                                    else if (somechunkpriminstanceikarmvoxelindex == 2)
                                                    {
                                                        somechunkpriminstanceikarmvoxelpos.X = 0;
                                                        somechunkpriminstanceikarmvoxelpos.Y = 0;
                                                    }
                                                    else if (somechunkpriminstanceikarmvoxelindex == 3)
                                                    {
                                                        somechunkpriminstanceikarmvoxelpos.X = 0;
                                                        somechunkpriminstanceikarmvoxelpos.Y = 0;
                                                    }

                                                    //somechunkpriminstanceikarmvoxelpos = ((new Vector3(posX, posY, posZ)) + originpositionikarmvoxel);

                                                    ikarmvoxel[somechunkpriminstanceikarmvoxelindex] = new sccsikvoxellimbs();
                                                    _sc_jitter_tasks = ikarmvoxel[somechunkpriminstanceikarmvoxelindex].createikarm(_sc_jitter_tasks, tempMultiInstancePhysicsTotal, somechunkpriminstanceikarmvoxelpos, ikvoxelbody[0], somechunkpriminstanceikarmvoxelindex, _human_inst_rig_x, _human_inst_rig_y, _human_inst_rig_z, grabtargetitem);

                                                    for (int zzz = -somechunkpriminstancesikarmvoxeldepthL; zzz <= somechunkpriminstancesikarmvoxeldepthR; zzz++)
                                                    {

                                                    }
                                                }
                                            }






                                        }
                                        catch (Exception ex)
                                        {
                                            Program.MessageBox((IntPtr)0, "create" + ex.ToString(), "scmsg", 0);

                                        }



                                        _inst_voxel_cube_x = 10;
                                        _inst_voxel_cube_y = 1;
                                        _inst_voxel_cube_z = 1;

                                        offsetVoxelY = 40;
                                        //VOXELS
                                        r = 0.95f; //0.75f
                                        g = 0.35f; //0.75f
                                        b = 0.35f; //0.75f
                                        a = 1;
                                        _object_worldmatrix = Matrix.Identity;
                                        offsetPosX = _voxel_cube_size_x * (1.15f); //x between each world instance
                                        offsetPosY = _voxel_cube_size_y * (1.15f); //y between each world instance
                                        offsetPosZ = _voxel_cube_size_z * (1.15f); //z between each world instance
                                                                                   //_offsetPos = new Vector3(0, 0, 0);
                                        _object_worldmatrix = sccs.scgraphics.scdirectx.WorldMatrix;
                                        _object_worldmatrix.M41 = 0 + x + physics_engine_offset_pos.X + world_pos_offset.X;
                                        _object_worldmatrix.M42 = 3 + y + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetVoxelY;
                                        _object_worldmatrix.M43 = 0 + z + physics_engine_offset_pos.Z + world_pos_offset.Z;
                                        _object_worldmatrix.M44 = 1;
                                        var sc_voxel_spheroid = new sc_voxel();
                                        voxel_general_size = somevoxelvirtualdesktopglobals.planeSize * 0.005f; //0.0015f //0.00075f
                                        voxel_type = 1;
                                        bool is_static = false;
                                        _voxel_mass = 100;

                                        var _hasinit00 = sc_voxel_spheroid.Initialize(sccs.scgraphics.scdirectx.D3D, sccs.scgraphics.scdirectx.D3D.SurfaceWidth, sccs.scgraphics.scdirectx.D3D.SurfaceHeight, 0, 1, 1, 1,
                                            _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, Program.consoleHandle,
                                            _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, thejitterworld1, _voxel_mass, is_static, sccs.scgraphics.scdirectx.BodyTag.sc_perko_voxel,
                                            9, 9, 9, 9, 9, 9, 9, 9, 9, 60, 60, 60, 60, 60, 60,
                                            //9, 9, 9, 9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 23, 22,
                                            voxel_general_size, Vector3.Zero, 7, 0, 0, 0, 2, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                                                                                                          //9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19
                                                                                                          //9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 20, 19, 
                                                                                                          //FOR CUBES AND SET TO voxel_type = 1                  
                                                                                                          //var _hasinit00 = sc_voxel_spheroid.Initialize(sccs.scgraphics.scdirectx.D3D, sccs.scgraphics.scdirectx.D3D.SurfaceWidth, sccs.scgraphics.scdirectx.D3D.SurfaceHeight, 1, 1, 1, _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, _voxel_mass, is_static, SCCoreSystems.sc_console.sccs.scgraphics.scdirectx.BodyTag._voxel_spheroid, 2, 2, 2, 2, 2, 2, 20, 19, 20, 19, 20, 19, voxel_general_size, Vector3.Zero, 250, 0, 0, 0, 2, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f                                  
                                                                                                          //FOR CUBES AND SET TO voxel_type = 1

                                        _array_of_last_frame_voxel_pos[indexer00][indexer01] = new Vector3[_inst_voxel_cube_x * _inst_voxel_cube_y * _inst_voxel_cube_z];

                                        worldvoxelcubelists[indexer00][indexer01] = sc_voxel_spheroid;
                                        worldMatrixinstancesvoxelcube[indexer00][indexer01] = new Matrix[_inst_voxel_cube_x * _inst_voxel_cube_y * _inst_voxel_cube_z];
                                        for (int i = 0; i < worldMatrixinstancesvoxelcube[indexer00][indexer01].Length; i++)
                                        {
                                            _array_of_last_frame_voxel_pos[indexer00][indexer01][i] = Vector3.Zero;
                                            worldMatrixinstancesvoxelcube[indexer00][indexer01][i] = Matrix.Identity;
                                        }

                                    }*/

                                }
                            }
                        }
                    }
                }
            }






            if (Program.usejitterphysics == 1 && Program.usejitterphysicsbuo == 1)
            {
                _buoyancy_area = new Jitter.Forces.Buoyancy[1];
            }





            /*




            //PHYSICS SCREENS
            _grab_rigid_data = new _rigid_data();
            _grab_rigid_data._body = null;
            _grab_rigid_data.position = Matrix.Identity;
            _grab_rigid_data._index = -1;
            _grab_rigid_data._physics_engine_index = -1;
            //SET TO 0 AND YOU HAVE USE A SHADERRESOURCE INSTEAD for the texture instead of using the color. cheap way for the moment as my switch wasnt working.
            r = 0;
            g = 0;
            b = 0;
            a = 0;
            _object_worldmatrix = Matrix.Identity;
            var _offsetPos = new Vector3(0.15f, 0.15f, 0.15f);
            _object_worldmatrix = sccs.scgraphics.scdirectx.WorldMatrix;
            _object_worldmatrix.M41 = 0 + sccs.scgraphics.scupdate.originPos.X; //-1.5f
            _object_worldmatrix.M42 = 2 + 0 + sccs.scgraphics.scupdate.originPos.Y;
            _object_worldmatrix.M43 = -6.5f + sccs.scgraphics.scupdate.originPos.Z - 3; //-1.5f
            _object_worldmatrix.M44 = 1;





            _object_worldmatrix.M41 += _offsetPos.X;
            _object_worldmatrix.M42 += _offsetPos.Y;
            _object_worldmatrix.M43 += _offsetPos.Z;
            _size_screen = 0.0005f;
            var sizeWidth01 = (float)(((float)sccs.scgraphics.scdirectx.D3D.SurfaceWidth * mulScreen) * _size_screen) * _screen_size_x;
            var sizeheight01 = (float)((float)(sccs.scgraphics.scdirectx.D3D.SurfaceHeight * mulScreen) * _size_screen) * _screen_size_y;
            var sizedepth01 = 1 * _screen_size_z;
            float sizeWidther01 = (float)(sizeWidth01 * 0.5f);
            float sizeHeighter01 = (float)(sizeheight01 * 0.5f);
            float sizeDepther01 = (float)(sizedepth01 * 0.5f);
            offsetPosX = sizeWidth01 * 2;
            offsetPosY = sizeheight01 * 2;
            offsetPosZ = sizedepth01 * 2;
            _world_screen_list[0] = new SC_cube[1];
            _world_screen_list[0][0] = new SC_cube();
            worldMatrix_instances_screens[0] = new Matrix[1][];
            _world_screen_list[0][0].Initialize(sccs.scgraphics.scdirectx.D3D, sccs.scgraphics.scdirectx.D3D.SurfaceWidth, sccs.scgraphics.scdirectx.D3D.SurfaceHeight, 1, 1, 1, sizeWidther01, sizeHeighter01, sizeDepther01, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, Program.consoleHandle, _object_worldmatrix, 3, offsetPosX, offsetPosY, offsetPosZ, thejitterworld, sccs.scgraphics.scdirectx.BodyTag.physicsInstancedScreen, true, 0, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            if (Program.usejitterphysics == 1 && Program.usejitterphysicsbuo == 1)
            {
                _buo.Add(_world_screen_list[0][0]._arrayOfInstances[0].transform.Component.rigidbody, 3);
                _world_screen_list[0][0]._arrayOfInstances[0].transform.Component.rigidbody.AllowDeactivation = false;
            }
            worldMatrix_instances_screens[0][0] = new Matrix[1];
            worldMatrix_instances_screens[0][0][0] = _world_screen_list[0][0]._arrayOfInstances[0].current_pos; //







            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.05f;
            g = 0.05f;
            b = 0.05f;
            a = 1;
            _object_worldmatrix = Matrix.Identity;
            _object_worldmatrix.M41 = 0;
            _object_worldmatrix.M42 = 0;
            _object_worldmatrix.M43 = 0;
            _object_worldmatrix.M44 = 1;
            offsetPosX = sizeWidth01 * 2;
            offsetPosY = sizeheight01 * 2;
            offsetPosZ = sizedepth01 * 2;
            _world_screen_assets_list[0] = new SC_cube[1];
            _world_screen_assets_list[0][0] = new SC_cube();
            worldMatrix_instances_screen_assets[0] = new Matrix[1][];
            _world_screen_assets_list[0][0].Initialize(sccs.scgraphics.scdirectx.D3D, sccs.scgraphics.scdirectx.D3D.SurfaceWidth, sccs.scgraphics.scdirectx.D3D.SurfaceHeight, 1, 1, 1, _screen_assets_size_x, _screen_assets_size_y, _screen_assets_size_z, new Vector4(r, g, b, a), _inst_screen_assets_x, _inst_screen_assets_y, _inst_screen_assets_z, Program.consoleHandle, _object_worldmatrix, 3, offsetPosX, offsetPosY, offsetPosZ, thejitterworld, sccs.scgraphics.scdirectx.BodyTag._screen_assets, true, 0, 10, vertoffsetx, vertoffsety, vertoffsetz); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_screen_assets[0][0] = new Matrix[_inst_screen_assets_x * _inst_screen_assets_y * _inst_screen_assets_z];
            for (int i = 0; i < worldMatrix_instances_screen_assets[0][0].Length; i++)
            {
                worldMatrix_instances_screen_assets[0][0][i] = Matrix.Identity;
            }








            oriRotationScreenX = 0;
            oriRotationScreenY = 0;
            oriRotationScreenZ = 0;

            RotationScreenX = oriRotationScreenX;
            RotationScreenY = oriRotationScreenY;
            RotationScreenZ = oriRotationScreenZ;

            //pitcher = oriRotationScreenX * 0.0174532925f;
            //yawer = oriRotationScreenY * 0.0174532925f;
            //roller = oriRotationScreenZ * 0.0174532925f;

            var pitcher = (float)(Math.PI * (oriRotationScreenX) / 180.0f);
            var yawer = (float)(Math.PI * (oriRotationScreenY) / 180.0f);
            var roller = (float)(Math.PI * (oriRotationScreenZ) / 180.0f);


            originRotScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);
            rotatingMatrixScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);


            float oriRotationScreenX0 = 0;
            float oriRotationScreenY0 = 180;
            float oriRotationScreenZ0 = 0;

            //pitcher = oriRotationScreenX0 * 0.0174532925f;
            //yawer = oriRotationScreenY0 * 0.0174532925f;
            //roller = oriRotationScreenZ0 * 0.0174532925f;
            pitcher = (float)(Math.PI * (oriRotationScreenX0) / 180.0f);
            yawer = (float)(Math.PI * (oriRotationScreenY0) / 180.0f);
            roller = (float)(Math.PI * (oriRotationScreenZ0) / 180.0f);

            _direction_offsetter = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);

            _screen_direction_offsetter_two = SharpDX.Matrix.RotationYawPitchRoll(0, 0, 0);

            _size_screen = 0.0005f;
            sizeWidtherer = (float)(((float)sccs.scgraphics.scdirectx.D3D.SurfaceWidth * mulScreen) * _size_screen);
            sizeheighterer = (float)((float)(sccs.scgraphics.scdirectx.D3D.SurfaceHeight * mulScreen) * _size_screen);

            //float sizeWidther = (float)(sizeWidth * 0.5f);
            //float sizeHeighter = (float)(sizeheight * 0.5f);
            //float sizeDepther = (float)(sizedepth * 0.5f);







            //_screenCorners = new DModelClass4_cube[4];
            rotatingMatrixScreen.M41 = sccs.scgraphics.scupdate.originPosScreen.X;
            rotatingMatrixScreen.M42 = sccs.scgraphics.scupdate.originPosScreen.Y;
            rotatingMatrixScreen.M43 = sccs.scgraphics.scupdate.originPosScreen.Z;
            _screenDirMatrix[0] = new Matrix[1][];
            point3DCollection[0] = new Vector3[1][];
            _screenDirMatrix_correct_pos[0] = new Matrix[1][];
            _screenDirMatrix[0][0] = new Matrix[4];
            point3DCollection[0][0] = new Vector3[4];
            _screenDirMatrix_correct_pos[0][0] = new Matrix[4];
            for (int i = 0; i < _screenDirMatrix[0][0].Length; i++)
            {
                _screenDirMatrix[0][0][i] = new Matrix();
                _screenDirMatrix[0][0][i] = rotatingMatrixScreen;
            }
            _screenDirMatrix[0][0][0].M41 = _world_screen_list[0][0].Vertices[16].position.X;// + originPosScreen.X;
            _screenDirMatrix[0][0][0].M42 = _world_screen_list[0][0].Vertices[16].position.Y;// + originPosScreen.Y;
            _screenDirMatrix[0][0][0].M43 = _world_screen_list[0][0].Vertices[16].position.Z;// + originPosScreen.Z;
            _screenDirMatrix[0][0][1].M41 = _world_screen_list[0][0].Vertices[13].position.X;// + originPosScreen.X;
            _screenDirMatrix[0][0][1].M42 = _world_screen_list[0][0].Vertices[13].position.Y;// + originPosScreen.Y;
            _screenDirMatrix[0][0][1].M43 = _world_screen_list[0][0].Vertices[13].position.Z;// + originPosScreen.Z;
            _screenDirMatrix[0][0][2].M41 = _world_screen_list[0][0].Vertices[15].position.X;// + originPosScreen.X;
            _screenDirMatrix[0][0][2].M42 = _world_screen_list[0][0].Vertices[15].position.Y;// + originPosScreen.Y;
            _screenDirMatrix[0][0][2].M43 = _world_screen_list[0][0].Vertices[15].position.Z;// + originPosScreen.Z;
            _screenDirMatrix[0][0][3].M41 = _world_screen_list[0][0].Vertices[17].position.X;// + originPosScreen.X;
            _screenDirMatrix[0][0][3].M42 = _world_screen_list[0][0].Vertices[17].position.Y;// + originPosScreen.Y;
            _screenDirMatrix[0][0][3].M43 = _world_screen_list[0][0].Vertices[17].position.Z;// + originPosScreen.Z;
                                                                                             //16//13//15//17 
                                                                                             //8//9//10//11
            for (int i = 0; i < _screenDirMatrix[0][0].Length; i++)
            {
                point3DCollection[0][0][i] = new Vector3(_screenDirMatrix[0][0][i].M41, _screenDirMatrix[0][0][i].M42, _screenDirMatrix[0][0][i].M43);
            }





            //lightpos = new Vector3(0, 100, 0);
            ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
            diffuseColour = new Vector4(1, 1, 1, 1);
            lightDirection = new Vector3(0, -1, -1);


            _DLightBuffer_cube[0] = new sccs.scgraphics.SC_cube.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };


            _DLightBuffer_voxel_cube[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };*/







            _sc_jitter_tasks[0][0].hasinit = 1;

            return _sc_jitter_tasks;
        }



        scmessageobjectjitter[][] _sc_jitter_tasks;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        Matrix originRot;
        Matrix rotatingMatrix;
        Matrix hmdrotMatrix;
        Matrix hmd_matrix;
        Matrix rotatingMatrixForPelvis;
        //Matrix _rightTouchMatrix;
        //Matrix _leftTouchMatrix;
        Matrix oriProjectionMatrix;
        Matrix extramatrix;
        Vector3 OFFSETPOS;
        Posef handPoseRight;
        Posef handPoseLeft;

        Matrix _worldMatrix;
        Matrix _viewMatrix;
        Matrix _projectionMatrix;

        Matrix _rightTouchMatrix;
        Matrix _leftTouchMatrix;
        //SC_VR_Chunk.chunkData chunkDat = new SC_VR_Chunk.chunkData();
        //sclevelgenchunk.chunkData chunkdatlevel = new sclevelgenchunk.chunkData();




        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;
        EyeType eye;
        //EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        Vector3 _hmdPoser;
        Quaternion _hmdRoter;
        Vector3 oculusRiftDir = Vector3.Zero;

        /*
        public struct chunkData
        {
            public SharpDX.Direct3D11.Buffer instanceBuffer;
            public Vector4[][] arrayOfInstanceVertex;
            public SC_VR_Chunk.DInstanceType[] arrayOfInstancePos;
            public int[][] arrayOfInstanceIndices;
            public Vector3[][] arrayOfInstanceNormals;
            public Vector2[][] arrayOfInstanceTextureCoordinates;
            public Vector4[][] arrayOfInstanceColors;
            public SC_VR_Chunk.DVertex[][] dVertexData;

            public SharpDX.Direct3D11.Device Device;
            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
            //public DShaderManager shaderManager;
            public SC_VR_Chunk_Shader chunkShader;
            public SC_VR_Chunk.DMatrixBuffer[] matrixBuffer;
            public SC_VR_Chunk.DLightBuffer[] lightBuffer;
            public SharpDX.Direct3D11.Buffer[] vertBuffers;
            public SharpDX.Direct3D11.Buffer[] colorBuffers;
            public SharpDX.Direct3D11.Buffer[] indexBuffers;
            public SharpDX.Direct3D11.Buffer[] normalBuffers;
            public SharpDX.Direct3D11.Buffer[] texBuffers;
            public SharpDX.Direct3D11.Buffer[] dVertBuffers;


            public DeviceContext _renderingContext;
            public SharpDX.Direct3D11.Buffer[] instanceBuffers;

        }*/

        //SC_VR_Chunk.chunkData chunkDat = new SC_VR_Chunk.chunkData();



        /*

        public scmessageobjectjitter[][] workonvoxelterrain(scgraphicssecpackage scgraphicssecpackagemessage)
        {
            scmessageobjectjitter[][] _sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
            Matrix viewMatrix = scgraphicssecpackagemessage.viewMatrix;
            Matrix projectionMatrix = scgraphicssecpackagemessage.projectionMatrix; //_projectionMatrix;
            Matrix originRot = scgraphicssecpackagemessage.originRot; // originRot;
            Matrix rotatingMatrix = scgraphicssecpackagemessage.rotatingMatrix; //rotatingMatrix;
            Matrix hmdrotMatrix = scgraphicssecpackagemessage.hmdmatrixRot; //hmdmatrixRot;
            Matrix hmd_matrix = scgraphicssecpackagemessage.hmd_matrix; //hmd_matrix;
            Matrix rotatingMatrixForPelvis = scgraphicssecpackagemessage.rotatingMatrixForPelvis; //rotatingMatrixForPelvis;
            Matrix _rightTouchMatrix = scgraphicssecpackagemessage.rightTouchMatrix; //_rightTouchMatrix;
            Matrix _leftTouchMatrix = scgraphicssecpackagemessage.leftTouchMatrix; //_leftTouchMatrix;
            Matrix oriProjectionMatrix = scgraphicssecpackagemessage.oriProjectionMatrix; //oriProjectionMatrix;
            Matrix extramatrix = scgraphicssecpackagemessage.someextrapelvismatrix; //someextrapelvismatrix;
            Vector3 OFFSETPOS = scgraphicssecpackagemessage.offsetpos; // OFFSETPOS;
            Posef handPoseRight = scgraphicssecpackagemessage.handPoseRight; //handPoseRight;
            Posef handPoseLeft = scgraphicssecpackagemessage.handPoseLeft; //handPoseLeft;


            Matrix _worldMatrix = sccs.scgraphics.scdirectx.WorldMatrix;
            Matrix _viewMatrix = viewMatrix;
            Matrix _projectionMatrix = oriProjectionMatrix;
            //_worldMatrix.Transpose();


            if (Program._useOculusRift == 1)
            {
                _projectionMatrix = oriProjectionMatrix;
                _viewMatrix.Transpose();
            }
            else
            {
                _viewMatrix.Transpose();
                _projectionMatrix = scgraphicssecpackagemessage.projectionMatrix;
                _projectionMatrix.Transpose();
                _worldMatrix.Transpose();
            }




            
            if (activatenotyetinfinitevoxelterrain == 1)
            {

                for (int c = 0; c < arrayOfChunks.Length; c++)
                {
                    arrayOfChunks[c].arrayOfMatrixBuff[0] = new SC_VR_Chunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkDat = new SC_VR_Chunk.chunkData();
                    chunkDat.instanceBuffer = arrayOfChunks[c].InstanceBuffer;
                    chunkDat.arrayOfInstanceVertex = arrayOfChunks[c].arrayOfInstanceVertex;
                    chunkDat.arrayOfInstancePos = arrayOfChunks[c].instances;
                    chunkDat.arrayOfInstanceIndices = arrayOfChunks[c].arrayOfInstanceIndices;
                    chunkDat.arrayOfInstanceNormals = arrayOfChunks[c].arrayOfInstanceNormals;
                    chunkDat.arrayOfInstanceTextureCoordinates = arrayOfChunks[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkDat.dVertexData = arrayOfChunks[c].arrayOfDVertex;

                    chunkDat.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkDat.worldMatrix = _worldMatrix;
                    chunkDat.viewMatrix = _viewMatrix;
                    chunkDat.projectionMatrix = _projectionMatrix;
                    chunkDat.chunkShader = arrayOfChunks[c].shaderOfChunk;
                    chunkDat.matrixBuffer = arrayOfChunks[c].arrayOfMatrixBuff;
                    chunkDat.vertBuffers = arrayOfChunks[c].vertBuffers;
                    chunkDat.colorBuffers = arrayOfChunks[c].colorBuffers;
                    chunkDat.indexBuffers = arrayOfChunks[c].indexBuffers;
                    chunkDat.instanceBuffers = arrayOfChunks[c].instanceBuffers;
                    chunkDat.dVertBuffers = arrayOfChunks[c].dVertBuffers;
                    chunkDat.texBuffers = arrayOfChunks[c].texBuffers;
                    chunkDat.normalBuffers = arrayOfChunks[c].normalBuffers;
                    chunkDat.lightBuffer = arrayOfChunks[c].lightBuffer;


                    arrayOfChunks[c].shaderOfChunk.Renderer(chunkDat);
                }
            }






            if (activatelevelgen == 1)
            {


                for (int c = 0; c < arrayoflevelchunkfloortiles.Length; c++)
                {

                    arrayoflevelchunkfloortiles[c].arrayOfMatrixBuff[0].world = _worldMatrix;
                    arrayoflevelchunkfloortiles[c].arrayOfMatrixBuff[0].view = _viewMatrix;
                    arrayoflevelchunkfloortiles[c].arrayOfMatrixBuff[0].proj = _projectionMatrix;

                    chunkdatlevel.instanceBuffer = arrayoflevelchunkfloortiles[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelchunkfloortiles[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelchunkfloortiles[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelchunkfloortiles[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelchunkfloortiles[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelchunkfloortiles[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelchunkfloortiles[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelchunkfloortiles[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelchunkfloortiles[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelchunkfloortiles[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelchunkfloortiles[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelchunkfloortiles[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelchunkfloortiles[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelchunkfloortiles[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelchunkfloortiles[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelchunkfloortiles[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelchunkfloortiles[c].lightBuffer;


                    arrayoflevelchunkfloortiles[c].shaderOfChunk.Renderer(chunkdatlevel);
                }




                for (int c = 0; c < arrayoflevelchunk.Length; c++)
                {

                    arrayoflevelchunk[c].arrayOfMatrixBuff[0].world = _worldMatrix;
                    arrayoflevelchunk[c].arrayOfMatrixBuff[0].view = _viewMatrix;
                    arrayoflevelchunk[c].arrayOfMatrixBuff[0].proj = _projectionMatrix;

                    chunkdatlevel.instanceBuffer = arrayoflevelchunk[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelchunk[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelchunk[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelchunk[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelchunk[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelchunk[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelchunk[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelchunk[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelchunk[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelchunk[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelchunk[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelchunk[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelchunk[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelchunk[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelchunk[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelchunk[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelchunk[c].lightBuffer;


                    arrayoflevelchunk[c].shaderOfChunk.Renderer(chunkdatlevel);
                }






                for (int c = 0; c < arrayoflevelfrontwall.Length; c++)
                {
                    arrayoflevelfrontwall[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelfrontwall[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelfrontwall[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelfrontwall[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelfrontwall[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelfrontwall[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelfrontwall[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelfrontwall[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelfrontwall[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelfrontwall[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelfrontwall[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelfrontwall[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelfrontwall[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelfrontwall[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelfrontwall[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelfrontwall[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelfrontwall[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelfrontwall[c].lightBuffer;


                    arrayoflevelfrontwall[c].shaderOfChunk.Renderer(chunkdatlevel);
                }



                for (int c = 0; c < arrayoflevelbackwall.Length; c++)
                {

                    arrayoflevelbackwall[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelbackwall[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelbackwall[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelbackwall[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelbackwall[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelbackwall[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelbackwall[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelbackwall[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelbackwall[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelbackwall[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelbackwall[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelbackwall[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelbackwall[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelbackwall[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelbackwall[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelbackwall[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelbackwall[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelbackwall[c].lightBuffer;


                    arrayoflevelbackwall[c].shaderOfChunk.Renderer(chunkdatlevel);
                }


                for (int c = 0; c < arrayoflevelrightwall.Length; c++)
                {

                    arrayoflevelrightwall[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelrightwall[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelrightwall[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelrightwall[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelrightwall[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelrightwall[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelrightwall[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelrightwall[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelrightwall[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelrightwall[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelrightwall[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelrightwall[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelrightwall[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelrightwall[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelrightwall[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelrightwall[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelrightwall[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelrightwall[c].lightBuffer;


                    arrayoflevelrightwall[c].shaderOfChunk.Renderer(chunkdatlevel);
                }


                for (int c = 0; c < arrayoflevelleftwall.Length; c++)
                {

                    arrayoflevelleftwall[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelleftwall[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelleftwall[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelleftwall[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelleftwall[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelleftwall[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelleftwall[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelleftwall[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelleftwall[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelleftwall[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelleftwall[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelleftwall[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelleftwall[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelleftwall[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelleftwall[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelleftwall[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelleftwall[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelleftwall[c].lightBuffer;


                    arrayoflevelleftwall[c].shaderOfChunk.Renderer(chunkdatlevel);
                }








                for (int c = 0; c < arrayoflevelleftfrontinsidecorner.Length; c++)
                {

                    arrayoflevelleftfrontinsidecorner[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelleftfrontinsidecorner[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelleftfrontinsidecorner[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelleftfrontinsidecorner[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelleftfrontinsidecorner[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelleftfrontinsidecorner[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelleftfrontinsidecorner[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelleftfrontinsidecorner[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelleftfrontinsidecorner[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelleftfrontinsidecorner[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelleftfrontinsidecorner[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelleftfrontinsidecorner[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelleftfrontinsidecorner[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelleftfrontinsidecorner[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelleftfrontinsidecorner[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelleftfrontinsidecorner[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelleftfrontinsidecorner[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelleftfrontinsidecorner[c].lightBuffer;


                    arrayoflevelleftfrontinsidecorner[c].shaderOfChunk.Renderer(chunkdatlevel);
                }




                for (int c = 0; c < arrayoflevelrightfrontinsidecorner.Length; c++)
                {

                    arrayoflevelrightfrontinsidecorner[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelrightfrontinsidecorner[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelrightfrontinsidecorner[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelrightfrontinsidecorner[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelrightfrontinsidecorner[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelrightfrontinsidecorner[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelrightfrontinsidecorner[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelrightfrontinsidecorner[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelrightfrontinsidecorner[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelrightfrontinsidecorner[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelrightfrontinsidecorner[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelrightfrontinsidecorner[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelrightfrontinsidecorner[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelrightfrontinsidecorner[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelrightfrontinsidecorner[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelrightfrontinsidecorner[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelrightfrontinsidecorner[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelrightfrontinsidecorner[c].lightBuffer;


                    arrayoflevelrightfrontinsidecorner[c].shaderOfChunk.Renderer(chunkdatlevel);
                }

                for (int c = 0; c < arrayoflevelleftbackinsidecorner.Length; c++)
                {

                    arrayoflevelleftbackinsidecorner[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelleftbackinsidecorner[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelleftbackinsidecorner[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelleftbackinsidecorner[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelleftbackinsidecorner[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelleftbackinsidecorner[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelleftbackinsidecorner[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelleftbackinsidecorner[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelleftbackinsidecorner[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelleftbackinsidecorner[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelleftbackinsidecorner[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelleftbackinsidecorner[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelleftbackinsidecorner[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelleftbackinsidecorner[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelleftbackinsidecorner[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelleftbackinsidecorner[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelleftbackinsidecorner[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelleftbackinsidecorner[c].lightBuffer;


                    arrayoflevelleftbackinsidecorner[c].shaderOfChunk.Renderer(chunkdatlevel);
                }



                for (int c = 0; c < arrayoflevelrightbackinsidecorner.Length; c++)
                {

                    arrayoflevelrightbackinsidecorner[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelrightbackinsidecorner[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelrightbackinsidecorner[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelrightbackinsidecorner[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelrightbackinsidecorner[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelrightbackinsidecorner[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelrightbackinsidecorner[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelrightbackinsidecorner[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelrightbackinsidecorner[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelrightbackinsidecorner[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelrightbackinsidecorner[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelrightbackinsidecorner[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelrightbackinsidecorner[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelrightbackinsidecorner[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelrightbackinsidecorner[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelrightbackinsidecorner[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelrightbackinsidecorner[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelrightbackinsidecorner[c].lightBuffer;


                    arrayoflevelrightbackinsidecorner[c].shaderOfChunk.Renderer(chunkdatlevel);
                }




                for (int c = 0; c < arrayoflevelleftfrontoutsidecorner.Length; c++)
                {

                    arrayoflevelleftfrontoutsidecorner[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelleftfrontoutsidecorner[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelleftfrontoutsidecorner[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelleftfrontoutsidecorner[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelleftfrontoutsidecorner[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelleftfrontoutsidecorner[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelleftfrontoutsidecorner[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelleftfrontoutsidecorner[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelleftfrontoutsidecorner[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelleftfrontoutsidecorner[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelleftfrontoutsidecorner[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelleftfrontoutsidecorner[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelleftfrontoutsidecorner[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelleftfrontoutsidecorner[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelleftfrontoutsidecorner[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelleftfrontoutsidecorner[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelleftfrontoutsidecorner[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelleftfrontoutsidecorner[c].lightBuffer;


                    arrayoflevelleftfrontoutsidecorner[c].shaderOfChunk.Renderer(chunkdatlevel);
                }





                for (int c = 0; c < arrayoflevelrightfrontoutsidecorner.Length; c++)
                {

                    arrayoflevelrightfrontoutsidecorner[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelrightfrontoutsidecorner[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelrightfrontoutsidecorner[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelrightfrontoutsidecorner[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelrightfrontoutsidecorner[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelrightfrontoutsidecorner[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelrightfrontoutsidecorner[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelrightfrontoutsidecorner[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelrightfrontoutsidecorner[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelrightfrontoutsidecorner[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelrightfrontoutsidecorner[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelrightfrontoutsidecorner[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelrightfrontoutsidecorner[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelrightfrontoutsidecorner[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelrightfrontoutsidecorner[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelrightfrontoutsidecorner[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelrightfrontoutsidecorner[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelrightfrontoutsidecorner[c].lightBuffer;


                    arrayoflevelrightfrontoutsidecorner[c].shaderOfChunk.Renderer(chunkdatlevel);
                }






                for (int c = 0; c < arrayoflevelleftbackoutsidecorner.Length; c++)
                {

                    arrayoflevelleftbackoutsidecorner[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelleftbackoutsidecorner[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelleftbackoutsidecorner[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelleftbackoutsidecorner[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelleftbackoutsidecorner[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelleftbackoutsidecorner[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelleftbackoutsidecorner[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelleftbackoutsidecorner[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelleftbackoutsidecorner[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelleftbackoutsidecorner[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelleftbackoutsidecorner[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelleftbackoutsidecorner[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelleftbackoutsidecorner[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelleftbackoutsidecorner[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelleftbackoutsidecorner[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelleftbackoutsidecorner[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelleftbackoutsidecorner[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelleftbackoutsidecorner[c].lightBuffer;


                    arrayoflevelleftbackoutsidecorner[c].shaderOfChunk.Renderer(chunkdatlevel);
                }







                for (int c = 0; c < arrayoflevelrightbackoutsidecorner.Length; c++)
                {

                    arrayoflevelrightbackoutsidecorner[c].arrayOfMatrixBuff[0] = new sclevelgenchunk.DMatrixBuffer()
                    {
                        world = _worldMatrix,
                        view = _viewMatrix,
                        proj = _projectionMatrix,
                    };



                    chunkdatlevel = new sclevelgenchunk.chunkData();
                    chunkdatlevel.instanceBuffer = arrayoflevelrightbackoutsidecorner[c].InstanceBuffer;
                    chunkdatlevel.arrayOfInstanceVertex = arrayoflevelrightbackoutsidecorner[c].arrayOfInstanceVertex;
                    chunkdatlevel.arrayOfInstancePos = arrayoflevelrightbackoutsidecorner[c].instances;
                    chunkdatlevel.arrayOfInstanceIndices = arrayoflevelrightbackoutsidecorner[c].arrayOfInstanceIndices;
                    chunkdatlevel.arrayOfInstanceNormals = arrayoflevelrightbackoutsidecorner[c].arrayOfInstanceNormals;
                    chunkdatlevel.arrayOfInstanceTextureCoordinates = arrayoflevelrightbackoutsidecorner[c].arrayOfInstanceTexturesCoordinates;
                    //arrayOfInstanceColors


                    chunkdatlevel.dVertexData = arrayoflevelrightbackoutsidecorner[c].arrayOfDVertex;

                    chunkdatlevel.Device = sccs.scgraphics.scdirectx.D3D.device;
                    chunkdatlevel.worldMatrix = _worldMatrix;
                    chunkdatlevel.viewMatrix = _viewMatrix;
                    chunkdatlevel.projectionMatrix = _projectionMatrix;
                    chunkdatlevel.chunkShader = arrayoflevelrightbackoutsidecorner[c].shaderOfChunk;
                    chunkdatlevel.matrixBuffer = arrayoflevelrightbackoutsidecorner[c].arrayOfMatrixBuff;
                    chunkdatlevel.vertBuffers = arrayoflevelrightbackoutsidecorner[c].vertBuffers;
                    chunkdatlevel.colorBuffers = arrayoflevelrightbackoutsidecorner[c].colorBuffers;
                    chunkdatlevel.indexBuffers = arrayoflevelrightbackoutsidecorner[c].indexBuffers;
                    chunkdatlevel.instanceBuffers = arrayoflevelrightbackoutsidecorner[c].instanceBuffers;
                    chunkdatlevel.dVertBuffers = arrayoflevelrightbackoutsidecorner[c].dVertBuffers;
                    chunkdatlevel.texBuffers = arrayoflevelrightbackoutsidecorner[c].texBuffers;
                    chunkdatlevel.normalBuffers = arrayoflevelrightbackoutsidecorner[c].normalBuffers;
                    chunkdatlevel.lightBuffer = arrayoflevelrightbackoutsidecorner[c].lightBuffer;


                    arrayoflevelrightbackoutsidecorner[c].shaderOfChunk.Renderer(chunkdatlevel);
                }
            }

            return scgraphicssecpackagemessage.scjittertasks;
        }
            */





        public scmessageobjectjitter[][] workonshaders(scgraphicssecpackage scgraphicssecpackagemessage)
        {
            //Program.MessageBox((IntPtr)0, "workonshaders", "scmsg", 0);

            _sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
            viewMatrix = scgraphicssecpackagemessage.viewMatrix;
            projectionMatrix = scgraphicssecpackagemessage.projectionMatrix; //_projectionMatrix;
            originRot = scgraphicssecpackagemessage.originRot; // originRot;
            rotatingMatrix = scgraphicssecpackagemessage.rotatingMatrix; //rotatingMatrix;
            hmdrotMatrix = scgraphicssecpackagemessage.hmdmatrixRot; //hmdmatrixRot;
            hmd_matrix = scgraphicssecpackagemessage.hmd_matrix; //hmd_matrix;
            rotatingMatrixForPelvis = scgraphicssecpackagemessage.rotatingMatrixForPelvis; //rotatingMatrixForPelvis;
            _rightTouchMatrix = scgraphicssecpackagemessage.rightTouchMatrix; //_rightTouchMatrix;
            _leftTouchMatrix = scgraphicssecpackagemessage.leftTouchMatrix; //_leftTouchMatrix;
            oriProjectionMatrix = scgraphicssecpackagemessage.oriProjectionMatrix; //oriProjectionMatrix;
            extramatrix = scgraphicssecpackagemessage.someextrapelvismatrix; //someextrapelvismatrix;
            OFFSETPOS = scgraphicssecpackagemessage.offsetpos; // OFFSETPOS;
            handPoseRight = scgraphicssecpackagemessage.handPoseRight; //handPoseRight;
            handPoseLeft = scgraphicssecpackagemessage.handPoseLeft; //handPoseLeft;

            _worldMatrix = sccs.scgraphics.scdirectx.WorldMatrix;
            _viewMatrix = viewMatrix;
            _projectionMatrix = oriProjectionMatrix;
            //_worldMatrix.Transpose();


            if (Program._useOculusRift == 1)
            {
                _projectionMatrix = oriProjectionMatrix;
                _viewMatrix.Transpose();
            }
            else
            {
                _viewMatrix.Transpose();
                _projectionMatrix = scgraphicssecpackagemessage.projectionMatrix;
                _projectionMatrix.Transpose();
            }





            /*
            for (int c = 0; c < arrayOfChunks.Length; c++)
            {


                chunkDat.instanceBuffer = arrayOfChunks[c].InstanceBuffer;
                chunkDat.arrayOfInstanceVertex = arrayOfChunks[c].arrayOfInstanceVertex;
                chunkDat.arrayOfInstancePos = arrayOfChunks[c].instances;//arrayOfChunks[c].instances;
                chunkDat.arrayOfInstanceIndices = arrayOfChunks[c].arrayOfInstanceIndices;
                chunkDat.arrayOfInstanceNormals = arrayOfChunks[c].arrayOfInstanceNormals;
                chunkDat.arrayOfInstanceTextureCoordinates = arrayOfChunks[c].arrayOfInstanceTexturesCoordinates;
                //arrayOfInstanceColors
                chunkDat.dVertexData = arrayOfChunks[c].arrayOfDVertex;

                chunkDat.Device = sccs.scgraphics.scdirectx.D3D.Device;
                chunkDat.worldMatrix = sccs.scgraphics.scdirectx.WorldMatrix;
                chunkDat.viewMatrix = _viewMatrix;
                chunkDat.projectionMatrix = _projectionMatrix;
                chunkDat.chunkShader = arrayOfChunks[c].shaderOfChunk;
                chunkDat.matrixBuffer = arrayOfChunks[c].arrayOfMatrixBuff;
                chunkDat.vertBuffers = arrayOfChunks[c].vertBuffers;
                chunkDat.colorBuffers = arrayOfChunks[c].colorBuffers;
                chunkDat.indexBuffers = arrayOfChunks[c].indexBuffers;
                chunkDat.instanceBuffers = arrayOfChunks[c].instanceBuffers;
                chunkDat.dVertBuffers = arrayOfChunks[c].dVertBuffers;
                chunkDat.texBuffers = arrayOfChunks[c].texBuffers;
                chunkDat.normalBuffers = arrayOfChunks[c].normalBuffers;
                chunkDat.lightBuffer = arrayOfChunks[c].lightBuffer;


                arrayOfChunks[c].shaderOfChunk.Renderer(chunkDat);


            }



            */






            /*
            //PHYSICS SCREENS
            _world_screen_list[0][0].Render(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext);
            sccs.scgraphics.scupdate._shaderManager.RenderInstancedObject(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext, _world_screen_list[0][0].IndexCount, _world_screen_list[0][0].InstanceCount, _world_screen_list[0][0]._POSITION, viewMatrix, projectionMatrix, _sc_jitter_tasks[0][0].shaderresource, _DLightBuffer_cube, _world_screen_list[0][0]);
            //END OF 

            //PHYSICS SCREEN ASSETS
            _world_screen_assets_list[0][0].Render(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext);
            sccs.scgraphics.scupdate._shaderManager.RenderInstancedObject(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext, _world_screen_assets_list[0][0].IndexCount, _world_screen_assets_list[0][0].InstanceCount, _world_screen_assets_list[0][0]._POSITION, viewMatrix, projectionMatrix, null, _DLightBuffer_cube, _world_screen_assets_list[0][0]);
            //END OF

            



            if (createikrig == 1)
            {



                for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                {
                    for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                    {
                        for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                        {
                            var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);

                            try
                            {

                                for (int x = 0; x < Program.worldwidth; x++)
                                {
                                    for (int y = 0; y < Program.worldheight; y++)
                                    {
                                        for (int z = 0; z < Program.worlddepth; z++)
                                        {
                                            var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);

                                            //Vector3 playerPos = new Vector3(_player_torso[0][0]._arrayOfInstances[0].current_pos.M41, _player_torso[0][0]._arrayOfInstances[0].current_pos.M42, _player_torso[0][0]._arrayOfInstances[0].current_pos.M43);


                                            /*
                                            //PHYSICS CUBES
                                            _world_cube_list[indexer00][indexer01].Render(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext);
                                            sccs.scgraphics.scupdate._shaderManager.RenderInstancedObject(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext, _world_cube_list[indexer00][indexer01].IndexCount, _world_cube_list[indexer00][indexer01].InstanceCount, _world_cube_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, sccs.scgraphics.scupdate._desktopFrame.ShaderResource, _DLightBuffer_cube, _world_cube_list[indexer00][indexer01]); // oculusRiftDir

                                            //PHYSICS CONES
                                            _world_cone_list[indexer00][indexer01].Render(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext);
                                            sccs.scgraphics.scupdate._shaderManager.RenderInstancedObject(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext, _world_cone_list[indexer00][indexer01].IndexCount, _world_cone_list[indexer00][indexer01].InstanceCount, _world_cone_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, sccs.scgraphics.scupdate._desktopFrame.ShaderResource, _DLightBuffer_cube, _world_cone_list[indexer00][indexer01]); // oculusRiftDir

                                            //PHYSICS CYLINDERS
                                            _world_cylinder_list[indexer00][indexer01].Render(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext);
                                            sccs.scgraphics.scupdate._shaderManager.RenderInstancedObject(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext, _world_cylinder_list[indexer00][indexer01].IndexCount, _world_cylinder_list[indexer00][indexer01].InstanceCount, _world_cylinder_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, sccs.scgraphics.scupdate._desktopFrame.ShaderResource, _DLightBuffer_cube, _world_cylinder_list[indexer00][indexer01]); // oculusRiftDir

                                            //PHYSICS CAPSULES
                                            _world_capsule_list[indexer00][indexer01].Render(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext);
                                            sccs.scgraphics.scupdate._shaderManager.RenderInstancedObject(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext, _world_capsule_list[indexer00][indexer01].IndexCount, _world_capsule_list[indexer00][indexer01].InstanceCount, _world_capsule_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, sccs.scgraphics.scupdate._desktopFrame.ShaderResource, _DLightBuffer_cube, _world_capsule_list[indexer00][indexer01]); // oculusRiftDir

                                            //PHYSICS SPHERES
                                            _world_sphere_list[indexer00][indexer01].Render(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext);
                                            sccs.scgraphics.scupdate._shaderManager.RenderInstancedObject(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext, _world_sphere_list[indexer00][indexer01].IndexCount, _world_sphere_list[indexer00][indexer01].InstanceCount, _world_sphere_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, sccs.scgraphics.scupdate._desktopFrame.ShaderResource, _DLightBuffer_cube, _world_sphere_list[indexer00][indexer01]); // oculusRiftDir
                                            

                                            //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);

                                            try
                                            {
                                                //PHYSICS VOXEL CUBES 
                                                //////////////////////about 100 ticks more per loop compared to simple physics cubes? will investigate later as when i do 
                                                //////////////////////simple cubes with the chunk it lags more even though the number of vertices are the same as the physics cube up above
                                                //////////////////////todo: culling of faces by distance from player. etc.

                                                worldvoxelcubelists[indexer00][indexer01].Render(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext);
                                                sccs.scgraphics.scupdate._shaderManager.RenderInstancedObjectsc_perko_voxel(sccs.scgraphics.scdirectx.D3D.device.ImmediateContext, worldvoxelcubelists[indexer00][indexer01].IndexCount, worldvoxelcubelists[indexer00][indexer01].InstanceCount, worldvoxelcubelists[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, _sc_jitter_tasks[0][0].shaderresource, _DLightBuffer_voxel_cube, worldvoxelcubelists[indexer00][indexer01]);
                                                ///Console.WriteLine(_SystemTickPerformance.ElapsedTicks);
                                            }
                                            catch (Exception ex)
                                            {
                                                Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                                            }


                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                            }
                        }
                    }
                }
            }

            */















            if (Program._useOculusRift == 1)
            {

                Matrix hmd_matrix_current = Matrix.Identity;
                Matrix hmdmatrixcurrentforpelvis = Matrix.Identity;
                //HEADSET POSITION
                displayMidpoint = sccs.scgraphics.scdirectx.D3D.OVR.GetPredictedDisplayTime(sccs.scgraphics.scdirectx.D3D.sessionPtr, 0);
                trackingState = sccs.scgraphics.scdirectx.D3D.OVR.GetTrackingState(sccs.scgraphics.scdirectx.D3D.sessionPtr, displayMidpoint, true);
                latencyMark = false;
                trackState = sccs.scgraphics.scdirectx.D3D.OVR.GetTrackingState(sccs.scgraphics.scdirectx.D3D.sessionPtr, 0.0f, latencyMark);
                poseStatefer = trackState.HeadPose;
                hmdPose = poseStatefer.ThePose;
                hmdRot = hmdPose.Orientation;
                _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);
                //SET CAMERA POSITION
                //Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
                oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);

                Matrix.RotationQuaternion(ref quatter, out hmd_matrix_current);

            }

            Matrix finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;


            /*
            if (createikrig == 1)
            {


                for (int xxx = 0; xxx < somechunkpriminstancesikvoxelbodywidthR; xxx++)
                {


                    float posX = (xxx);
                    float posY = (0);
                    float posZ = (0);

                    var xxi = xxx;
                    var yyi = 0;
                    var zzi = 0;

                    if (xxi < 0)
                    {
                        xxi *= -1;
                        xxi = (somechunkpriminstancesikvoxelbodywidthR) + xxi;
                    }
                    if (yyi < 0)
                    {
                        yyi *= -1;
                        yyi = (somechunkpriminstancesikvoxelbodyheightR) + yyi;
                    }
                    if (zzi < 0)
                    {
                        zzi *= -1;
                        zzi = (somechunkpriminstancesikvoxelbodydepthR) + zzi;
                    }

                    int somechunkpriminstanceikvoxelbodyindex = xxi;// + (somechunkpriminstancesikvoxelbodywidthL ) * (yyi + (somechunkpriminstancesikvoxelbodyheightL) * zzi);

                    _sc_jitter_tasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].setikbodytargetnlimbspositionsNrotations(_sc_jitter_tasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_pelvis[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_torso[0][0], lightpos, dirLight, finalRotationMatrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_pelvis[0][0], hmd_matrix_current, extramatrix, hmdmatrixcurrentforpelvis);
                    _sc_jitter_tasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].ikbodyrender(_sc_jitter_tasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);





                    /*
                    for (int yyy = -somechunkpriminstancesikvoxelbodyheightL; yyy <= somechunkpriminstancesikvoxelbodyheightR; yyy++)
                    {
                        for (int zzz = -somechunkpriminstancesikvoxelbodydepthL; zzz <= somechunkpriminstancesikvoxelbodydepthR; zzz++)
                        {

                        }
                    }
                }





                for (int xxx = 0; xxx < somechunkpriminstancesikarmvoxelwidthR; xxx++)
                {
                    for (int yyy = 0; yyy < somechunkpriminstancesikarmvoxelheightR; yyy++)
                    {

                        float posX = (xxx);
                        float posY = (yyy);
                        float posZ = (0);

                        var xxi = xxx;
                        var yyi = yyy;
                        var zzi = 0;

                        if (xxi < 0)
                        {
                            xxi *= -1;
                            xxi = (somechunkpriminstancesikarmvoxelwidthR) + xxi;
                        }
                        if (yyi < 0)
                        {
                            yyi *= -1;
                            yyi = (somechunkpriminstancesikarmvoxelheightR) + yyi;
                        }
                        if (zzi < 0)
                        {
                            zzi *= -1;
                            zzi = (somechunkpriminstancesikarmvoxeldepthR) + zzi;
                        }

                        int somechunkpriminstanceikarmvoxelindex = xxi + (yyi * (somechunkpriminstancesikarmvoxelheightR));


                        var sometest = ikarmvoxel[somechunkpriminstanceikarmvoxelindex];


                        _sc_jitter_tasks = ikarmvoxel[somechunkpriminstanceikarmvoxelindex].setiktargetnlimbspositionsNrotations(_sc_jitter_tasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikvoxelbody[0]._player_pelvis[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikvoxelbody[0]._player_torso[0][0], lightpos, dirLight, finalRotationMatrix, ikvoxelbody[0]._player_pelvis[0][0], ikvoxelbody[0], somechunkpriminstanceikarmvoxelindex, rotatingMatrixForPelvis, extramatrix, directionvectoroffsets, Vector3.Zero, Vector3.Zero);
                        _sc_jitter_tasks = ikarmvoxel[somechunkpriminstanceikarmvoxelindex].ikarmrender(_sc_jitter_tasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);

                    }
                }
            }*/

            float timeSinceStart = (float)(DateTime.Now - sccs.scgraphics.scupdate.startTime).TotalSeconds;
            //Matrix worldmatlightrot = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * lightrotationspeed) * Matrix.RotationY(timeSinceStart * 2 * lightrotationspeed) * Matrix.RotationZ(timeSinceStart * 3 * lightrotationspeed);
            Matrix worldmatlightrot = Matrix.Scaling(1.0f) * Matrix.RotationY(timeSinceStart * 2 * lightrotationspeed) * Matrix.RotationZ(timeSinceStart * 3 * lightrotationspeed);

            Quaternion worldmatlightquat;
            SharpDX.Quaternion.RotationMatrix(ref worldmatlightrot, out worldmatlightquat);
            dirLight = sc_maths._getDirection(Vector3.ForwardRH, worldmatlightquat); //new Vector3(0, -1, 0);// 
            ambientColor = new Vector4(0.35f, 0.35f, 0.35f, 1.0f);
            diffuseColour = new Vector4(1, 1, 1, 1);









            //sccs.scgraphics.scdirectx.D3D.TurnZBufferOff();
            //sccs.scgraphics.scdirectx.D3D.TurnOnAlphaBlending();


            //sccs.scgraphics.scdirectx.D3D.TurnZBufferOn();
            //sccs.scgraphics.scdirectx.D3D.TurnOffAlphaBlending();



            if (setoncevrdesktop == 0) //&& Program._useOculusRift == 1
            {

                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                for (int xxxx = 0; xxxx < somevoxelvirtualdesktopw; xxxx++)
                {
                    for (int yyyy = 0; yyyy < somevoxelvirtualdesktoph; yyyy++)
                    {
                        for (int zzzz = 0; zzzz < somevoxelvirtualdesktopd; zzzz++)
                        {
                            var somevoxelvirtualdesktopindex = xxxx + somevoxelvirtualdesktopw * (yyyy + somevoxelvirtualdesktoph * zzzz);


                            if (Program.exitedprogram == -1)
                            {


                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfMatrixBuff[0].world = _worldMatrix;
                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfMatrixBuff[0].view = _viewMatrix;
                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfMatrixBuff[0].proj = _projectionMatrix;

                                /*somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfMatrixBuff[0] = new SC_instancedChunkPrim.DMatrixBuffer()
                                {
                                    world = _worldMatrix,
                                    view = _viewMatrix,
                                    proj = _projectionMatrix,
                                };*/


                                /*somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].lightBufferInstChunk[0] = new SC_instancedChunkPrim.DLightBufferEr()
                                {
                                    ambientColor = ambientColor,
                                    diffuseColor = diffuseColour,
                                    lightDirection = rayDirFrontInstancesChunks,
                                    padding0 = 0,
                                    lightPosition = centerPosRighthandposRR,
                                    padding1 = 100
                                };*/

                                lightpos.X = sccs.scgraphics.scupdate._rightTouchMatrix.M41;
                                lightpos.Y = sccs.scgraphics.scupdate._rightTouchMatrix.M42;
                                lightpos.Z = sccs.scgraphics.scupdate._rightTouchMatrix.M43;


                                Quaternion somerighttouchquat;
                                Matrix somemat = sccs.scgraphics.scupdate._rightTouchMatrix;
                                Quaternion.RotationMatrix(ref somemat, out somerighttouchquat);

                                Vector3 forwarddir = sc_maths._getDirection(Vector3.ForwardRH, somerighttouchquat);

                                //ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
                                //ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
                                //diffuseColour = new Vector4(1, 1, 1, 1);
                                //lightDirection = new Vector3(0, -1, -1);

                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].lightBufferInstChunk[0].lightDirection = forwarddir;
                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].lightBufferInstChunk[0].lightPosition = lightpos;
                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].lightBufferInstChunk[0].ambientColor = ambientColor;
                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].lightBufferInstChunk[0].diffuseColor = diffuseColour;


                                for (int x = 0; x < somevoxelvirtualdesktopglobals.numberOfObjectInWidth; x++)
                                {
                                    for (int y = 0; y < somevoxelvirtualdesktopglobals.numberOfObjectInHeight; y++)
                                    {
                                        for (int z = 0; z < somevoxelvirtualdesktopglobals.numberOfObjectInDepth; z++)
                                        {
                                            int c = x + somevoxelvirtualdesktopglobals.numberOfObjectInWidth * (y + somevoxelvirtualdesktopglobals.numberOfObjectInHeight * z);

                                            /*
                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].someovrdir[0].ovrdirf = sccs.scgraphics.scupdate.dirikvoxelbodyInstanceForward0;
                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].someovrdir[0].ovrdirr = sccs.scgraphics.scupdate.dirikvoxelbodyInstanceRight0;
                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].someovrdir[0].ovrdiru = sccs.scgraphics.scupdate.dirikvoxelbodyInstanceUp0;
                                            //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].someovrdir[0].ovrpos = new Vector4(sccs.scgraphics.scupdate._hmdPoser.X, sccs.scgraphics.scupdate._hmdPoser.Y, sccs.scgraphics.scupdate._hmdPoser.Z,1);
                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].someovrdir[0].ovrpos = new Vector4(sccs.scgraphics.scupdate.OFFSETPOS.X, sccs.scgraphics.scupdate.OFFSETPOS.Y, sccs.scgraphics.scupdate.OFFSETPOS.Z, 1) + new Vector4(sccs.scgraphics.scupdate._hmdPoser.X, sccs.scgraphics.scupdate._hmdPoser.Y, sccs.scgraphics.scupdate._hmdPoser.Z, 1);
                                            */

                                            if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].switchForRender == 1)
                                            {





                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].lightBufferInstChunk = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].lightBuffer;
                                                //RENDERING THE INSTANCED CHUNK 
                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].matrixBuffer = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfMatrixBuff;








                                                var matrixBufferDescriptionVertex00 = new BufferDescription()
                                                {
                                                    Usage = ResourceUsage.Dynamic,
                                                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)) * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].instances.Length,
                                                    BindFlags = BindFlags.VertexBuffer,
                                                    CpuAccessFlags = CpuAccessFlags.Write,
                                                    OptionFlags = ResourceOptionFlags.None,
                                                    StructureByteStride = 0
                                                };

                                                var InstanceBuffer = new SharpDX.Direct3D11.Buffer(sccs.scgraphics.scdirectx.D3D.device, matrixBufferDescriptionVertex00);

                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].SC_instancedChunk_Instances = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instances;
                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].worldMatrix = _worldMatrix;
                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].viewMatrix = _viewMatrix;
                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].projectionMatrix = _projectionMatrix;

                                                //arrayOfChunkData[c].worldMatrix += arrayOfMatrixBuff[0].world;

                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].matrixBuffer = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfMatrixBuff;

                                                //arrayOfChunkData[c].lightBuffer = lightBufferInstChunk;

                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instanceBuffer = InstanceBuffer;
                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].arrayOfSomeMap = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayOfSomeMap;

                                                //arrayOfChunkData[c] = shaderOfChunk.Renderer(arrayOfChunkData[c], c);
                                                //Console.WriteLine("test");
                                            }
                                            else
                                            {


                                            }


                                            //var timeSinceStart = (float)(DateTime.Now - sccs.scgraphics.scupdate.startTime).TotalSeconds;
                                            //Matrix worldmatlightrot = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * disco_sphere_rot_speed) * Matrix.RotationY(timeSinceStart * 2 * disco_sphere_rot_speed) * Matrix.RotationZ(timeSinceStart * 3 * disco_sphere_rot_speed);


                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].lightBuffer[0].ambientColor = ambientColor;
                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].lightBuffer[0].diffuseColor = diffuseColour;
                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].lightBuffer[0].lightDirection = new Vector3(0, -1, 0); //dirLight
                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].lightBuffer[0].lightPosition = new Vector3(0, 3, 0); //sccs.scgraphics.scupdate.originPosScreen;// new Vector3(0, 0, 0);



                                            //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].lightBufferInstChunk = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].lightBuffer;

                                            //SENDING THE SCREENCAPTURE TO THE BUFFER FOR THE SHADER
                                            //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].shaderOfChunk.shaderResourceView2D = sccs.scgraphics.scupdate._desktopFrame.ShaderResource;// sccs.scgraphics.scupdate._desktopFrame.ShaderResourceArray;
                                            //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].chunkShader.shaderResourceView2D = sccs.scgraphics.scupdate._desktopFrame.ShaderResource;//
                                            //SENDING THE SCREENCAPTURE TO THE BUFFER FOR THE SHADER


                                            //RENDERING THE INSTANCED CHUNK 
                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].matrixBuffer = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfMatrixBuff;


                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c] = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].shaderOfChunk.Renderer(somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c], c, _sc_jitter_tasks[0][0].shaderresource, somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].voxeltype);
                                            //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c] = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].shaderOfChunk.Renderer(somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c], c, _sc_jitter_tasks[0][0].shaderresource, somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].voxeltype);

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
            }

            //sccs.scgraphics.scdirectx.D3D.TurnZBufferOn();
            //sccs.scgraphics.scdirectx.D3D.TurnOffAlphaBlending();

            //sccs.scgraphics.scdirectx.D3D.TurnZBufferOff();
            //sccs.scgraphics.scdirectx.D3D.TurnOnAlphaBlending();



            //sccs.scgraphics.scdirectx.D3D.DeviceContext.Rasterizer.State = sccs.scgraphics.scdirectx.D3D.RasterstateCullBack;
            //sccs.scgraphics.scdirectx.D3D.TurnOffAlphaBlending();
            //sccs.scgraphics.scdirectx.D3D.TurnZBufferOn();


            /*
            sccs.scgraphics.scdirectx.D3D.TurnOnAlphaBlending();
            sccs.scgraphics.scdirectx.D3D.TurnZBufferOff();

            sccs.scgraphics.scdirectx.D3D.TurnOffAlphaBlending();
            sccs.scgraphics.scdirectx.D3D.TurnZBufferOn();
            */
























            return scgraphicssecpackagemessage.scjittertasks; // scgraphicssecpackagemessage.scjittertasks;
        }
















        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        int frame_counter_4_buttonY = 45;
        int display_grid_type = 0;


        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        //TO STABILIZE THE MOUSE CURSOR ON THE SCREEN.
        const int _MaxArraySize0 = 15; //50
        const int _MaxArraySize1 = 14; //49
        //TO STABILIZE THE MOUSE CURSOR ON THE SCREEN.

        SharpDX.Matrix _oculusR_Cursor_matrix = SharpDX.Matrix.Identity;
        Stopwatch _updateFunctionStopwatchRightThumbstickGoRight = new Stopwatch();
        Stopwatch _updateFunctionStopwatchRightThumbstickGoLeft = new Stopwatch();
        Stopwatch _updateFunctionStopwatchLeftThumbstick = new Stopwatch();
        Stopwatch _updateFunctionStopwatchRight = new Stopwatch();

        int hasClickedBUTTONA = 0;
        int hasClickedBUTTONACounter = 0;

        bool _startOnce02 = true;
        bool _updateFunctionBoolRight = true;
        bool _updateFunctionBoolLeft = true;
        bool _updateFunctionBoolLeftThumbStickGoLeft = true;
        bool _updateFunctionBoolLeftThumbStickGoRight = true;
        bool _updateFunctionBoolRightThumbStickGoLeft = true;
        bool _updateFunctionBoolRightThumbStickGoRight = true;
        bool _updateFunctionBoolLeftHandTrigger = true;
        bool _updateFunctionBoolRightHandTrigger = true;
        bool _updateFunctionBoolLeftIndexTrigger = true;
        bool _updateFunctionBoolRightIndexTrigger = true;
        bool _updateFunctionBoolTouchRightButtonA = true;
        bool _updateFunctionBoolLeftThumbStick = true;
        int _frameCounterTouchRight = 0;

        Plane planer;

        Vector3 centerPosRight;
        SharpDX.Ray someRay;
        Vector3 intersectPointRight;
        bool intersecter;

        Vector3 centerPosLeft;
        Vector3 rayDirLeft;
        SharpDX.Ray someRayLeft;
        Vector3 intersectPointLeft;
        bool intersecterLeft;
        Vector3 stabilizedIntersectionPosLeft;
        Vector3 stabilizedIntersectionPosRight;



        int currentFrameLeft = 0;
        int currentFrameRight = 0;
        double averageXRight = 0;
        double averageYRight = 0;
        double averageZRight = 0;
        double lastRightHitPointXFrameOne = 0;
        double lastRightHitPointYFrameOne = 0;
        double lastRightHitPointZFrameOne = 0;
        double positionXRight = 0;
        double positionYRight = 0;
        double positionZRight = 0;
        double averageXLeft = 0;
        double averageYLeft = 0;
        double averageZLeft = 0;
        double lastLeftHitPointXFrameOne = 0;
        double lastLeftHitPointYFrameOne = 0;
        double lastLeftHitPointZFrameOne = 0;
        double positionXLeft = 0;
        double positionYLeft = 0;
        double positionZLeft = 0;
        double differenceX = 0;
        double differenceY = 0;
        double differenceZ = 0;
        double percentXLeft;
        double percentYLeft;

        float widthLength;
        float heightLength;
        double currentPosWidth;
        double currentPosHeight;

        double percentXRight;
        double percentYRight;

        double currentX;
        double currentY;
        double currentZ;

        int _has_init_ray;
        JMatrix _last_frame_rigid_grab_rot;
        Vector3 _last_frame_rigid_grab_pos;
        Vector3 _last_frame_handPos = Vector3.Zero;
        Vector3 _last_final_hand_pos_right;


        //HERE IS THE MOUSE STABILIZER ARRAYS - THE BIGGER THE ARRAYS THE SLOWER AND MORE STABLE THE MOUSE IS ON THE SCREEN.
        Vector3[] arrayOfStabilizerPosRight = new Vector3[_MaxArraySize0];
        double[] arrayOfStabilizerPosXRight = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceXRight = new double[_MaxArraySize1];
        double[] arrayOfStabilizerPosYRight = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceYRight = new double[_MaxArraySize1];

        double[] arrayOfStabilizerPosZRight = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceZRight = new double[_MaxArraySize1];



        Vector3[] arrayOfStabilizerPosLeft = new Vector3[_MaxArraySize0];
        double[] arrayOfStabilizerPosXLeft = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceXLeft = new double[_MaxArraySize1];
        double[] arrayOfStabilizerPosYLeft = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceYLeft = new double[_MaxArraySize1];

        double[] arrayOfStabilizerPosZLeft = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceZLeft = new double[_MaxArraySize1];

        //
        Vector3[] _arrayOfStabilizerPosRight = new Vector3[_MaxArraySize0];
        double[] _arrayOfStabilizerPosXRight = new double[_MaxArraySize0];
        double[] _arrayOfStabilizerPosDifferenceXRight = new double[_MaxArraySize1];
        double[] _arrayOfStabilizerPosYRight = new double[_MaxArraySize0];
        double[] _arrayOfStabilizerPosDifferenceYRight = new double[_MaxArraySize1];

        Vector3[] _arrayOfStabilizerPosLeft = new Vector3[_MaxArraySize0];
        double[] _arrayOfStabilizerPosXLeft = new double[_MaxArraySize0];
        double[] _arrayOfStabilizerPosDifferenceXLeft = new double[_MaxArraySize1];
        double[] _arrayOfStabilizerPosYLeft = new double[_MaxArraySize0];
        double[] _arrayOfStabilizerPosDifferenceYLeft = new double[_MaxArraySize1];

        int _hasLockedMouse = 0;
        Matrix hmd_matrix_current = Matrix.Identity;
        Matrix hmdmatrixcurrentforpelvis = Matrix.Identity;

        SharpDX.Matrix _intersectTouchRightMatrix = SharpDX.Matrix.Identity;
        SharpDX.Matrix _intersectTouchLeftMatrix = SharpDX.Matrix.Identity;

        Matrix final_hand_pos_right_locked;
        Matrix final_hand_pos_left_locked;
        Matrix tempMatrix = Matrix.Identity;// tempMatrix
        Matrix _last_screen_pos = Matrix.Identity;
        int had_locked_screen = -1;
        int _tier_logic_swtch_lock_screen = 0;
        Matrix _current_screen_pos = Matrix.Identity;

        //OCULUS TOUCH SETTINGS 
        Ab3d.OculusWrap.Result resultsRight;
        uint buttonPressedOculusTouchRight;
        Vector2f[] thumbStickRight;
        float[] handTriggerRight;
        float[] indexTriggerRight;

        float indexTriggerRightLastAbs;
        float indexTriggerLeftLastAbs;

        Ab3d.OculusWrap.Result resultsLeft;
        uint buttonPressedOculusTouchLeft;
        Vector2f[] thumbStickLeft;
        float[] handTriggerLeft;
        float[] indexTriggerLeft;
        //Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        //Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;
        //Matrix _leftTouchMatrix = Matrix.Identity;
        //Matrix _rightTouchMatrix = Matrix.Identity;
        //OCULUS TOUCH SETTINGS


        Matrix hmdmatrixRot_;
        Matrix OriginRot = Matrix.Identity;
        Matrix RotatingMatrix = Matrix.Identity;
        Matrix RotatingMatrixForPelvis = Matrix.Identity;
        Matrix viewMatrix_ = Matrix.Identity;



        uint lastbuttonPressedOculusTouchRight = 0;
        uint lastbuttonPressedOculusTouchLeft = 0;
        public int _indexMouseMove = 0;
        bool restartFrameCounterRight = false;

        int hasClickedHomeButtonTouchLeft = 0;
        int hasClickedHomeButtonTouchLeftCounter = 0;

        bool isHoldingBUTTONA = false;
        bool hasClickedBUTTONB = false;
        int hasClickedBUTTONX = 0;
        int hasClickedBUTTONY = 0;
        bool restartFrameCounterLeft = false;




        bool _canResetCounterTouchRightButtonA = false;
        bool _canResetCounterTouchRightButtonB = false;
        int _frameCounterTouchLeft = 0;
        bool _canResetCounterTouchLeftButtonA = false;
        bool _canResetCounterTouchLeftButtonB = false;
        bool _canResetCounterTouchLeftButtonX = false;
        bool _canResetCounterTouchLeftButtonY = false;
        bool hasUsedThumbStickLeftW = false;
        bool hasUsedThumbStickLeftS = false;
        bool hasUsedThumbStickLeftA = false;
        bool hasUsedThumbStickRightD = false;
        bool hasUsedThumbStickRightQ = false;
        bool hasUsedThumbStickRightE = false;
        bool lastHasUsedHandTriggerLeft = false;
        bool hasUsedHandTriggerLeft = false;


        int _out_of_bounds_oculus_rift = 0;
        int _out_of_bounds_right = 0;
        int _out_of_bounds_left = 0;
        uint _lastMousePosXRight = 9999;
        uint _lastMousePosYRight = 9999;



        //SPECTRUM
        //SPECTRUM
        //SPECTRUM
        const int _inst_spectrum_x = 42; // 36 // 210 //75 // 420
        const int _inst_spectrum_y = 1;
        const int _inst_spectrum_z = 21; // 36 // 210 //75 //5625 //210
        float _spectrum_size_x = 0.0015f; //0.001115f
        float _spectrum_size_y = 0.0015f;
        float _spectrum_size_z = 0.0015f;
        byte[] _sound_byte_array = new byte[_inst_spectrum_x * _inst_spectrum_z]; //44100
        byte[] _sound_byte_array_instant = new byte[_inst_spectrum_x * _inst_spectrum_z]; //44100 //176400
        int has_spoken_main = 0;
        int has_spoken_sec = 0;
        int has_spoken_tier = 0;
        int has_spoken_quart = 0;
        string last_xml_filepath = "";
        string last_wave_filepath = "";
        DateTime _time_of_recording_start = DateTime.Now;
        DateTime _time_of_recording_end = DateTime.Now;
        int sc_can_start_rec_counter = 0;
        int sc_can_start_rec_counter_before_add_index = 0;
        int sc_play_file = 0;
        int sc_play_file_counter = 0;
        int sc_save_file = 0;
        int sc_save_file_counter = 0;
        int sc_start_recording = 0;
        int sc_start_recording_counter = 0;
        string short_path = "";
        string instant_short_path = "";
        float spectrum_noise_value = 0;
        SoundPlayer _sound_player = new SoundPlayer();
        Matrix spectrum_mat = Matrix.Identity;
        static XmlTextWriter writer = new XmlTextWriter(Console.Out);
        string path;
        int _records_counter = 0;
        int _records_instant_counter = 0;
        int _frames_to_access_counter = 0;
        int _spectrum_work = 0;
        int _spectrum_work_counter = 0;
        int _has_recorded = 0;

        public static int GetSoundLength(string fileName)
        {
            StringBuilder lengthBuf = new StringBuilder(32);
            mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", fileName), null, 0, IntPtr.Zero);
            mciSendString("status wave length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);
            mciSendString("close wave", null, 0, IntPtr.Zero);
            int length = 0;
            int.TryParse(lengthBuf.ToString(), out length);
            return length;
        }
        int swtchinstantsound = -1;
        //END OF
        //END OF
        //END OF

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);





        public scmessageobjectjitter[][] scwritedirectiontobuffer(scmessageobjectjitter[][] _sc_jitter_tasks)
        {

            for (int xxx = 0; xxx < somevoxelvirtualdesktopw; xxx++)
            {
                for (int yyy = 0; yyy < somevoxelvirtualdesktoph; yyy++)
                {
                    for (int zzz = 0; zzz < somevoxelvirtualdesktopd; zzz++)
                    {
                        //somechunkmesh
                        var somevoxelvirtualdesktopindex = xxx + somevoxelvirtualdesktopw * (yyy + somevoxelvirtualdesktoph * zzz);


                        if (somevoxelvirtualdesktop != null && Program.exitedprogram != 1)
                        {
                            if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex] != null)
                            {
                                if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh != null)
                                {
                                    for (int x = 0; x < somevoxelvirtualdesktopglobals.numberOfObjectInWidth; x++)
                                    {
                                        for (int y = 0; y < somevoxelvirtualdesktopglobals.numberOfObjectInHeight; y++)
                                        {
                                            for (int z = 0; z < somevoxelvirtualdesktopglobals.numberOfObjectInDepth; z++)
                                            {
                                                int c = x + somevoxelvirtualdesktopglobals.numberOfObjectInWidth * (y + somevoxelvirtualdesktopglobals.numberOfObjectInHeight * z);


                                                if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c] != null)
                                                {
                                                    if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances != null)
                                                    {



                                                        for (int xx = 0; xx < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth; xx++)
                                                        {
                                                            for (int yy = 0; yy < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight; yy++)
                                                            {
                                                                for (int zz = 0; zz < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth; zz++)
                                                                {
                                                                    int cc = xx + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * (yy + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * zz);


                                                                    if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances[cc] != null)
                                                                    {



                                                                        Matrix worldMatrixChunkDroneInstance = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances[cc].current_pos;

                                                                        Matrix worldMatrixmain = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].worldmatofobj;


                                                                        Quaternion _testQuator;



                                                                        //RASTERTEK BILLBOARDING EXAMPLE
                                                                        /*// Calculate the rotation that needs to be applied to the billboard model to face the current camera position using the arc tangent function.
                                                                        double angle = Math.Atan2(modelPosition.X - cameraPosition.X, modelPosition.Z - cameraPosition.Z) * (180.0f / Math.PI);
                                                                        // Convert rotation into radians.
                                                                        float rotation = (float)angle * 0.0174532925f;
                                                                        // Setup the rotation the billboard at the origin using the world matrix.
                                                                        Matrix.RotationY(rotation, out worldMatrix);
                                                                        // Setup the translation matrix from the billboard model.
                                                                        Matrix translationMatrix = Matrix.Translation(modelPosition.X, modelPosition.Y, modelPosition.Z);
                                                                        // Finally combine the rotation and translation matrices to create the final world matrix for the billboard model.
                                                                        Matrix.Multiply(ref worldMatrix, ref translationMatrix, out worldMatrix);
                                                                        */

                                                                        /*
                                                                        Matrix worldMatrix;
                                                                        //RASTERTEK BILLBOARDING EXAMPLE
                                                                        // Calculate the rotation that needs to be applied to the billboard model to face the current camera position using the arc tangent function.
                                                                        double angle = Math.Atan2(worldMatrixChunkDroneInstance.M41 - sccs.scgraphics.scupdate.viewPosition.X, worldMatrixChunkDroneInstance.M43 - sccs.scgraphics.scupdate.viewPosition.Z) * (180.0f / Math.PI);
                                                                        // Convert rotation into radians.
                                                                        float rotation = (float)angle * 0.0174532925f;
                                                                        // Setup the rotation the billboard at the origin using the world matrix.
                                                                        Matrix.RotationY(rotation, out worldMatrix);
                                                                        // Setup the translation matrix from the billboard model.
                                                                        Matrix translationMatrix = Matrix.Translation(worldMatrixChunkDroneInstance.M41, worldMatrixChunkDroneInstance.M42, worldMatrixChunkDroneInstance.M43);
                                                                        // Finally combine the rotation and translation matrices to create the final world matrix for the billboard model.
                                                                        Matrix.Multiply(ref worldMatrix, ref translationMatrix, out worldMatrix);
                                                                        */
                                                                        //worldMatrixChunkDroneInstance = worldMatrix;
                                                                        //worldMatrixChunkDroneInstance = worldMatrixChunkDroneInstance * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].worldmatofobj;

                                                                        //TOREADD
                                                                        //TOREADD
                                                                        //TOREADD
                                                                        Quaternion.RotationMatrix(ref worldMatrixChunkDroneInstance, out _testQuator);
                                                                        if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD != null)
                                                                        {
                                                                            var dirInstanceForward = sc_maths._newgetdirforward(_testQuator);
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD[cc].instancePos.X = dirInstanceForward.X;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD[cc].instancePos.Y = dirInstanceForward.Y;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD[cc].instancePos.Z = dirInstanceForward.Z;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD[cc].instancePos.W = 1;
                                                                        }
                                                                        if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT != null)
                                                                        {
                                                                            //Quaternion.RotationMatrix(ref tempmat0, out quat_buffers);
                                                                            var dirInstanceRight = -sc_maths._newgetdirleft(_testQuator);
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT[cc].instancePos.X = dirInstanceRight.X;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT[cc].instancePos.Y = dirInstanceRight.Y;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT[cc].instancePos.Z = dirInstanceRight.Z;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT[cc].instancePos.W = 1;
                                                                        }
                                                                        if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP != null)
                                                                        {
                                                                            //Quaternion.RotationMatrix(ref tempmat0, out quat_buffers);    
                                                                            var dirInstanceUp = sc_maths._newgetdirup(_testQuator);
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP[cc].instancePos.X = dirInstanceUp.X;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP[cc].instancePos.Y = dirInstanceUp.Y;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP[cc].instancePos.Z = dirInstanceUp.Z;
                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP[cc].instancePos.W = 1;
                                                                        }
                                                                        //TOREADD
                                                                        //TOREADD
                                                                        //TOREADD
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return _sc_jitter_tasks;
        }





        public scmessageobjectjitter[][] scwriteinstancedVDbytestobuffer(scmessageobjectjitter[][] _sc_jitter_tasks)
        {





            //VIRTUAL DESKTOP VOXEL MESH BREAKING BUFFER MAP RESET
            //VIRTUAL DESKTOP VOXEL MESH BREAKING BUFFER MAP RESET
            //VIRTUAL DESKTOP VOXEL MESH BREAKING BUFFER MAP RESET

            for (int xxxx = 0; xxxx < somevoxelvirtualdesktopw; xxxx++)
            {
                for (int yyyy = 0; yyyy < somevoxelvirtualdesktoph; yyyy++)
                {
                    for (int zzzz = 0; zzzz < somevoxelvirtualdesktopd; zzzz++)
                    {
                        var somevoxelvirtualdesktopindex = xxxx + somevoxelvirtualdesktopw * (yyyy + somevoxelvirtualdesktoph * zzzz);

                        for (int x = 0; x < somevoxelvirtualdesktopglobals.numberOfObjectInWidth; x++)
                        {
                            for (int y = 0; y < somevoxelvirtualdesktopglobals.numberOfObjectInHeight; y++)
                            {
                                for (int z = 0; z < somevoxelvirtualdesktopglobals.numberOfObjectInDepth; z++)
                                {
                                    int c = x + somevoxelvirtualdesktopglobals.numberOfObjectInWidth * (y + somevoxelvirtualdesktopglobals.numberOfObjectInHeight * z);

                                    if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].switchForRender == 1)
                                    {

                                    }
                                    else
                                    {
                                        for (int xx = 0; xx < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth; xx++)
                                        {
                                            for (int yy = 0; yy < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight; yy++)
                                            {
                                                for (int zz = 0; zz < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth; zz++)
                                                {
                                                    int cc = xx + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * (yy + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * zz);

                                                    //if (sc_menu_scroller == 2 && somechunkplayerspatiallocationresetcounter < somevoxelvirtualdesktopw * somevoxelvirtualdesktoph * somevoxelvirtualdesktopd * somevoxelvirtualdesktopglobals.numberOfObjectInWidth * somevoxelvirtualdesktopglobals.numberOfObjectInHeight * somevoxelvirtualdesktopglobals.numberOfObjectInDepth * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth && resetvoxelladdercounter == 1)

                                                    if (sc_menu_scroller == 2 && resetvoxelladdercounter == 1)
                                                    {
                                                        //Program.MessageBox((IntPtr)0, "menu option 3" , "sccs", 0);
                                                        //Program.MessageBox((IntPtr)0, "menu option 2", "sccs", 0);

                                                        int voxelchunkinvertoption = -1; // Program.usetypeofvoxel;

                                                        //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].somechunk[cc].resetmap(out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, -1, voxelchunkinvertoption);
                                                        //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].somechunk[cc].setnewmap(out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, -1, voxelchunkinvertoption); //

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].somechunk[cc].resetmap(out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, -1, voxelchunkinvertoption
                                                        , out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b,
                                                    out m11c, out m12c, out m13c, out m14c, out m21c, out m22c, out m23c, out m24c, out m31c, out m32c, out m33c, out m34c, out m41c, out m42c, out m43c, out m44c,
                                                    out m11d, out m12d, out m13d, out m14d, out m21d, out m22d, out m23d, out m24d, out m31d, out m32d, out m33d, out m34d, out m41d, out m42d, out m43d, out m44d
);




                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M11 = (float)m11;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M12 = (float)m12;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M13 = (float)m13;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M14 = (float)m14;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M21 = (float)m21;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M22 = (float)m22;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M23 = (float)m23;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M24 = (float)m24;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M31 = (float)m31;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M32 = (float)m32;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M33 = (float)m33;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M34 = (float)m34;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M41 = (float)m41;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M42 = (float)m42;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M43 = (float)m43;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrix[cc].instancematrix.M44 = (float)m44;





                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M11 = (float)m11b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M12 = (float)m12b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M13 = (float)m13b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M14 = (float)m14b;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M21 = (float)m21b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M22 = (float)m22b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M23 = (float)m23b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M24 = (float)m24b;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M31 = (float)m31b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M32 = (float)m32b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M33 = (float)m33b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M34 = (float)m34b;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M41 = (float)m41b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M42 = (float)m42b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M43 = (float)m43b;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixb[cc].instancematrix.M44 = (float)m44b;






                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M11 = (float)m11c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M12 = (float)m12c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M13 = (float)m13c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M14 = (float)m14c;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M21 = (float)m21c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M22 = (float)m22c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M23 = (float)m23c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M24 = (float)m24c;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M31 = (float)m31c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M32 = (float)m32c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M33 = (float)m33c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M34 = (float)m34c;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M41 = (float)m41c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M42 = (float)m42c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M43 = (float)m43c;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixc[cc].instancematrix.M44 = (float)m44c;





                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M11 = (float)m11d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M12 = (float)m12d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M13 = (float)m13d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M14 = (float)m14d;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M21 = (float)m21d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M22 = (float)m22d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M23 = (float)m23d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M24 = (float)m24d;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M31 = (float)m31d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M32 = (float)m32d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M33 = (float)m33d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M34 = (float)m34d;

                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M41 = (float)m41d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M42 = (float)m42d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M43 = (float)m43d;
                                                        somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesmatrixd[cc].instancematrix.M44 = (float)m44d;
                                                        //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].copytobuffer = 1;


                                                    }
                                                }
                                            }
                                        }





                                    }
                                }
                            }
                        }
                    }
                }
            }
            somechunkplayerspatiallocationresetcounter++;



            for (int xxxx = 0; xxxx < somevoxelvirtualdesktopw; xxxx++)
            {
                for (int yyyy = 0; yyyy < somevoxelvirtualdesktoph; yyyy++)
                {
                    for (int zzzz = 0; zzzz < somevoxelvirtualdesktopd; zzzz++)
                    {
                        var somevoxelvirtualdesktopindex = xxxx + somevoxelvirtualdesktopw * (yyyy + somevoxelvirtualdesktoph * zzzz);

                        for (int x = 0; x < somevoxelvirtualdesktopglobals.numberOfObjectInWidth; x++)
                        {
                            for (int y = 0; y < somevoxelvirtualdesktopglobals.numberOfObjectInHeight; y++)
                            {
                                for (int z = 0; z < somevoxelvirtualdesktopglobals.numberOfObjectInDepth; z++)
                                {
                                    int c = x + somevoxelvirtualdesktopglobals.numberOfObjectInWidth * (y + somevoxelvirtualdesktopglobals.numberOfObjectInHeight * z);

                                    if (somevoxelvirtualdesktop != null)
                                    {
                                        if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex] != null)
                                        {
                                            if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh != null)
                                            {
                                                if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c] != null)
                                                {
                                                    if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances != null)
                                                    {


                                                        if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].switchForRender == 1)
                                                        {

                                                        }
                                                        else
                                                        {
                                                            for (int xx = 0; xx < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth; xx++)
                                                            {
                                                                for (int yy = 0; yy < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight; yy++)
                                                                {
                                                                    for (int zz = 0; zz < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth; zz++)
                                                                    {
                                                                        int cc = xx + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * (yy + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * zz);
                                                                        if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances[cc] != null)
                                                                        {
                                                                            //if (sc_menu_scroller == 2 && somechunkplayerspatiallocationresetcounter >= somevoxelvirtualdesktopw * somevoxelvirtualdesktoph * somevoxelvirtualdesktopd * somevoxelvirtualdesktopglobals.numberOfObjectInWidth * somevoxelvirtualdesktopglobals.numberOfObjectInHeight * somevoxelvirtualdesktopglobals.numberOfObjectInDepth * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth && resetvoxelladdercounter == 2)

                                                                            if (sc_menu_scroller == 2 && resetvoxelladdercounter == 1)
                                                                            {
                                                                                //Program.MessageBox((IntPtr)0, "menu option 2 again" , "sccs", 0);

                                                                                int voxelchunkinvertoption = Program.usetypeofvoxel;

                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].copytobuffer = 1;
                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].switchForRender = 1;
                                                                                somechunkplayerspatiallocationresetcounter = 0;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            resetvoxelladdercounter = 1;
            //VIRTUAL DESKTOP VOXEL MESH BREAKING BUFFER MAP RESET
            //VIRTUAL DESKTOP VOXEL MESH BREAKING BUFFER MAP RESET
            //VIRTUAL DESKTOP VOXEL MESH BREAKING BUFFER MAP RESET


            return _sc_jitter_tasks;
        }


        //byte[] _sc_jitter_tasks[0][0].frameByteArray;


        int divxadd = 0;
        int divyadd = 0;

        float someemptybytes = 00000000.0f;
        int pixelsize = 2;
        int somemindexy = 0;
        int somemindexx = 0;

        int somepixelsize = 4;
        int somepixelval = 4;
        int bytePoser = 0;

        float somemultiplier = 0; // -25 // -100
        int someresw = 0;//
        int someresh = 0;//
        int someindexmx = 1;
        int someindexmy = 1;


        public scmessageobjectjitter[][] scwriteheightmaptobuffer(scmessageobjectjitter[][] _sc_jitter_tasks)
        {










            if (activatevrheightmapfeature == 1)
            {

                //_sc_jitter_tasks[0][0].frameByteArray = _sc_jitter_tasks[0][0].frameByteArray;

                divxadd = 0;
                divyadd = 0;

                //var pixw = (resw / somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth) * pixnum; // 384 pixels per instance in width // 32 pixels per chunk
                //var pixh = (resh / somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight) * pixnum; // 432 pixels per instance in height // 32 pixels per chunk

                //divx = pixw;
                //divy = pixh;

                //var source_rect = new System.Drawing.Rectangle(0, 0, pixw, pixh);

                //int wid = pixw / pixnum; //96 pixels per byte
                //int hgt = pixh / pixnum; //108 pixels per byte

                //int num_rows = pixnum;// pixnum;
                //int num_cols = pixnum;// pixnum;

                //int xrowpix = 0;
                //int ycolpix = 0;


                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                try
                {


                    for (int xxx = 0; xxx < somevoxelvirtualdesktopw; xxx++)
                    {
                        for (int yyy = 0; yyy < somevoxelvirtualdesktoph; yyy++)
                        {
                            for (int zzz = 0; zzz < somevoxelvirtualdesktopd; zzz++)
                            {
                                //somechunkmesh
                                var somevoxelvirtualdesktopindex = xxx + somevoxelvirtualdesktopw * (yyy + somevoxelvirtualdesktoph * zzz);


                                if (somevoxelvirtualdesktop != null && Program.exitedprogram != 1)
                                {
                                    if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex] != null)
                                    {
                                        if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh != null)
                                        {
                                            for (int x = 0; x < somevoxelvirtualdesktopglobals.numberOfObjectInWidth; x++)
                                            {
                                                for (int y = 0; y < somevoxelvirtualdesktopglobals.numberOfObjectInHeight; y++)
                                                {
                                                    for (int z = 0; z < somevoxelvirtualdesktopglobals.numberOfObjectInDepth; z++)
                                                    {
                                                        int c = x + somevoxelvirtualdesktopglobals.numberOfObjectInWidth * (y + somevoxelvirtualdesktopglobals.numberOfObjectInHeight * z);


                                                        if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c] != null)
                                                        {
                                                            if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances != null)
                                                            {



                                                                /*for (int xx = 0; xx < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth; xx++)
                                                                {
                                                                    for (int yy = 0; yy < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight; yy++)
                                                                    {
                                                                        for (int zz = 0; zz < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth; zz++)
                                                                        {
                                                                            int cc = xx + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * (yy + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * zz);


                                                                            if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances[cc] != null)
                                                                            {



                                                                                Matrix worldMatrixChunkDroneInstance = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances[cc].current_pos;

                                                                                Matrix worldMatrixmain = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].worldmatofobj;


                                                                                Quaternion _testQuator;



                                                                                //RASTERTEK BILLBOARDING EXAMPLE
                                                                                // Calculate the rotation that needs to be applied to the billboard model to face the current camera position using the arc tangent function.
                                                                               // double angle = Math.Atan2(modelPosition.X - cameraPosition.X, modelPosition.Z - cameraPosition.Z) * (180.0f / Math.PI);
                                                                                // Convert rotation into radians.
                                                                                //float rotation = (float)angle * 0.0174532925f;
                                                                                // Setup the rotation the billboard at the origin using the world matrix.
                                                                                //Matrix.RotationY(rotation, out worldMatrix);
                                                                                // Setup the translation matrix from the billboard model.
                                                                                //Matrix translationMatrix = Matrix.Translation(modelPosition.X, modelPosition.Y, modelPosition.Z);
                                                                                // Finally combine the rotation and translation matrices to create the final world matrix for the billboard model.
                                                                                //Matrix.Multiply(ref worldMatrix, ref translationMatrix, out worldMatrix);
                                                                                

                                                                                /*
                                                                                Matrix worldMatrix;
                                                                                //RASTERTEK BILLBOARDING EXAMPLE
                                                                                // Calculate the rotation that needs to be applied to the billboard model to face the current camera position using the arc tangent function.
                                                                                double angle = Math.Atan2(worldMatrixChunkDroneInstance.M41 - sccs.scgraphics.scupdate.viewPosition.X, worldMatrixChunkDroneInstance.M43 - sccs.scgraphics.scupdate.viewPosition.Z) * (180.0f / Math.PI);
                                                                                // Convert rotation into radians.
                                                                                float rotation = (float)angle * 0.0174532925f;
                                                                                // Setup the rotation the billboard at the origin using the world matrix.
                                                                                Matrix.RotationY(rotation, out worldMatrix);
                                                                                // Setup the translation matrix from the billboard model.
                                                                                Matrix translationMatrix = Matrix.Translation(worldMatrixChunkDroneInstance.M41, worldMatrixChunkDroneInstance.M42, worldMatrixChunkDroneInstance.M43);
                                                                                // Finally combine the rotation and translation matrices to create the final world matrix for the billboard model.
                                                                                Matrix.Multiply(ref worldMatrix, ref translationMatrix, out worldMatrix);
                                                                                
                                                                                //worldMatrixChunkDroneInstance = worldMatrix;
                                                                                //worldMatrixChunkDroneInstance = worldMatrixChunkDroneInstance * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].worldmatofobj;

                                                                                //TOREADD
                                                                                //TOREADD
                                                                                //TOREADD
                                                                                Quaternion.RotationMatrix(ref worldMatrixChunkDroneInstance, out _testQuator);
                                                                                if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD != null)
                                                                                {
                                                                                    var dirInstanceForward = sc_maths._newgetdirforward(_testQuator);
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD[cc].instancePos.X = dirInstanceForward.X;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD[cc].instancePos.Y = dirInstanceForward.Y;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD[cc].instancePos.Z = dirInstanceForward.Z;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD[cc].instancePos.W = 1;
                                                                                }
                                                                                if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT != null)
                                                                                {
                                                                                    //Quaternion.RotationMatrix(ref tempmat0, out quat_buffers);
                                                                                    var dirInstanceRight = -sc_maths._newgetdirleft(_testQuator);
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT[cc].instancePos.X = dirInstanceRight.X;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT[cc].instancePos.Y = dirInstanceRight.Y;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT[cc].instancePos.Z = dirInstanceRight.Z;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT[cc].instancePos.W = 1;
                                                                                }
                                                                                if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP != null)
                                                                                {
                                                                                    //Quaternion.RotationMatrix(ref tempmat0, out quat_buffers);    
                                                                                    var dirInstanceUp = sc_maths._newgetdirup(_testQuator);
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP[cc].instancePos.X = dirInstanceUp.X;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP[cc].instancePos.Y = dirInstanceUp.Y;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP[cc].instancePos.Z = dirInstanceUp.Z;
                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP[cc].instancePos.W = 1;
                                                                                }
                                                                                //TOREADD
                                                                                //TOREADD
                                                                                //TOREADD
                                                                            }

                                                                        }
                                                                    }
                                                                }*/

                                                                //if (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].copytobuffer == 1)
                                                                {


                                                                    /*
                                                                    float staticPlaneSize = somevoxelvirtualdesktopglobals.planeSize;
                                                                    float planeSize0 = somevoxelvirtualdesktopglobals.planeSize;
                                                                    float alternateStaticPlaneSize = 0;

                                                                    if (staticPlaneSize == 1)
                                                                    {
                                                                        staticPlaneSize = planeSize0 * 0.1f;
                                                                        alternateStaticPlaneSize = planeSize0 * 0.1f;
                                                                    }
                                                                    else if (staticPlaneSize == 0.1f)
                                                                    {
                                                                        staticPlaneSize = planeSize0;
                                                                        alternateStaticPlaneSize = planeSize0 * 10;
                                                                    }
                                                                    else if (staticPlaneSize == 0.01f)
                                                                    {
                                                                        staticPlaneSize = planeSize0;
                                                                        alternateStaticPlaneSize = planeSize0 * 1000;
                                                                    }


                                                                    for (int xx = 0; xx < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth; xx++)
                                                                    {
                                                                        //Program.MessageBox((IntPtr)0, "" + noiseXZ, "sccs", 0);

                                                                        for (int yy = 0; yy < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight; yy++)
                                                                        {
                                                                            for (int zz = 0; zz < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth; zz++)
                                                                            {

                                                                                float seed = (float)sc_maths.getSomeRandNum(3420, 9999999); //3420;

                                                                                int cc = xx + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * (yy + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * zz);

                                                                                var somecountermax = (float)Math.Abs((somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.W - (float)Math.Floor(somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.W)) * 10); // 1000

                                                                                float noiseXZ = 100;
                                                                                float noiseYZ = 100;
                                                                                float noiseXY = 100;

                                                                                float detailScale1 = 7;
                                                                                float heightScale1 = 5;

                                                                                fastNoise = new FastNoise();

                                                                                noiseXZ *= (fastNoise.GetNoise((x * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.X + seed) / detailScale1, (z * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Z + seed) / detailScale1) * heightScale1);
                                                                                float size0 = (1 / planeSize0) * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Y;
                                                                                noiseXZ -= size0;

                                                                                //Program.MessageBox((IntPtr)0, "c:" + somecountermax, "sccs error", 0);
                                                                                noiseYZ *= (fastNoise.GetNoise((y * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Y + seed) / detailScale1, (z * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Z + seed) / detailScale1) * heightScale1);
                                                                                float size1 = (1 / planeSize0) * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.X;
                                                                                noiseYZ -= size1;


                                                                                noiseXY *= (fastNoise.GetNoise((x * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.X + seed) / detailScale1, (y * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Y + seed) / detailScale1) * heightScale1);
                                                                                float size2 = (1 / planeSize0) * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Z;
                                                                                noiseXY -= size2;

                                                                                //TOREADD FOR NOISE TEST ON COLOR

                                                                                //noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

                                                                                float someclampednoisexz = sc_maths.ClampValue(0.55f + (1 * noiseXZ), 0.35f, 0.75f);
                                                                                float someclampednoiseyz = sc_maths.ClampValue(0.55f + (1 * noiseYZ), 0.35f, 0.75f);
                                                                                float someclampednoisexy = sc_maths.ClampValue(0.55f + (1 * noiseXY), 0.35f, 0.75f);

                                                                                //Program.MessageBox((IntPtr)0, "someclampednoisexz:" + someclampednoisexz + "/someclampednoiseyz:" + someclampednoiseyz + "/someclampednoisexy:" + someclampednoisexy, "sccs", 0);
                                                                                //Program.MessageBox((IntPtr)0, "noiseXZ:" + noiseXZ + "/noiseYZ:" + noiseYZ + "/noiseXY:" + noiseXY, "sccs", 0);

                                                                                //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.X =Math.Abs(0.15f + noiseXZ);// someclampednoisexz;
                                                                                //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Y = Math.Abs(0.25f + noiseYZ);// someclampednoiseyz;
                                                                                //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Z = Math.Abs(0.15f + noiseXY);// someclampednoisexy;

                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.X = 0.35f + Math.Abs(someclampednoisexz);
                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Y = 0.35f + Math.Abs(someclampednoiseyz);
                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Z = 0.35f + Math.Abs(someclampednoisexy);

                                                                                //somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.W = 1.0f;

                                                                                //this can be swapped with heightmaps values with some modifications here and there to bring bytes within a range of 0 to 255 rgba values or from within a range of 0.0f to 1.0f values.
                                                                                if (Math.Truncate(somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.W) >= somecountermax)
                                                                                {

                                                                                    somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.W = 0 + somecountermax * 0.001f;
                                                                                }
                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.W += 1f;

                                                                                //TOREADD FOR NOISE TEST ON COLOR

                                                                                //this can be swapped with heightmaps values with some modifications here and there to bring bytes within a range of 0 to 255 rgba values or from within a range of 0.0f to 1.0f values.
                                                                            }
                                                                        }
                                                                    }*/












                                                                    /*
                                                                    int pixnum = somevoxelvirtualdesktopglobals.tinyChunkWidth;


                                                                    var resw = 1920;
                                                                    var resh = 1080;

                                                                    var pixw = (resw / somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth) * pixnum; // 384 pixels per instance in width // 
                                                                    var pixh = (resh / somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight) * pixnum; // 432 pixels per instance in height

                                                                    //var source_rect = new System.Drawing.Rectangle(0, 0, pixw, pixh);

                                                                    int wid = pixw / pixnum; //96 pixels per byte
                                                                    int hgt = pixh / pixnum; //108 pixels per byte

                                                                    int num_rows = pixnum;
                                                                    int num_cols = pixnum;*/

                                                                    for (int xx = 0; xx < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth; xx++)
                                                                    {
                                                                        //source_rect.X = 0;

                                                                        for (int yy = 0; yy < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight; yy++)
                                                                        {
                                                                            var somebytearrayindex = (((somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight - 1) - yy) * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth) + xx;

                                                                            // var _sc_jitter_tasks[0][0].frameByteArray = sccs.scgraphics.scupdate.screencaptureframe.arrayOfFRACSCREENSPECTRUMBytes[somebytearrayindex];

                                                                            //1920resolutionW / 20 instances = 96 res * 4 = 384 pixels width / xx byte square per instance side => xx pixels per face
                                                                            //1080resolutionH / 10 instances = 108 res * 4 = 432 pixels height / xx byte square per instance side => xx pixels per face                 

                                                                            /*for (int row = 0; row < num_rows; row++)
                                                                            {
                                                                                source_rect.X = 0;

                                                                                for (int col = 0; col < num_cols; col++)
                                                                                {
                                                                                    var mainArrayIndex = (row * num_cols) + col;

                                                                                    var byteindex = (source_rect.X * num_cols) + source_rect.Y;
                                                                                    var testv = _sc_jitter_tasks[0][0].frameByteArray[byteindex];
                                                                                    source_rect.X += wid;
                                                                                    //break;
                                                                                }
                                                                                source_rect.Y += hgt;
                                                                            }*/

                                                                            //var mainArrayIndex = (xx * num_cols) + yy;

                                                                            //var byteindex = (source_rect.X * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight) + source_rect.Y;
                                                                            //var testv = _sc_jitter_tasks[0][0].frameByteArray[byteindex];
                                                                            //source_rect.X += wid;


                                                                            for (int zz = 0; zz < somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth; zz++)
                                                                            {
                                                                                int cc = xx + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth * (yy + somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight * zz);

                                                                                //Matrix worldMatrixChunkDroneInstance = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].arrayofzeromeshinstances[cc].current_pos;
                                                                                //Quaternion _testQuator;

                                                                                //int maxval = somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth - 1;

                                                                                //var normz = sc_maths.LimitInclusiveInt(zz, 0, maxval);//(somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth - 1) - 
                                                                                //Program.MessageBox((IntPtr)0, normz + "", "sccs message", 0);
                                                                                //var somebyteindex0 = 0;// (0 * wid) + 0;

                                                                                //var bpix = 0;// _sc_jitter_tasks[0][0].frameByteArray[0 + somebyteindex0];
                                                                                //var gpix = 1;// _sc_jitter_tasks[0][0].frameByteArray[1 + somebyteindex0];
                                                                                //var rpix = 2;// _sc_jitter_tasks[0][0].frameByteArray[2 + somebyteindex0];
                                                                                //var apix = 3;// _sc_jitter_tasks[0][0].frameByteArray[3 + somebyteindex0];



                                                                                //float somefullbytes = 0;// _sc_jitter_tasks[0][0].frameByteArray[rpix + somebyteindex0];// 11111111.0f;
                                                                                someemptybytes = 00000000.0f;

                                                                                //HEIGHTMAPS START ADDED 31ST OCTOBER 2021 WIP
                                                                                //HEIGHTMAPS START ADDED 31ST OCTOBER 2021 WIP
                                                                                //HEIGHTMAPS START ADDED 31ST OCTOBER 2021 WIP
                                                                                //var somepixelsize = 4;
                                                                                //var someoffset = c;
                                                                                //var somevalpix = sc_maths.LimitInclusiveInt(_sc_jitter_tasks[0][0].frameByteArray[3 + somebyteindex0], 0, 9);

                                                                                //Program.MessageBox((IntPtr)0, somevalpix + "", "sccs message", 0);


                                                                                /*
                                                                                float seed = (float)sc_maths.getSomeRandNum(3420, 4820); //3420;

                                                                                float staticPlaneSize = somevoxelvirtualdesktopglobals.planeSize;
                                                                                float planeSize0 = somevoxelvirtualdesktopglobals.planeSize;
                                                                                float alternateStaticPlaneSize = 0;

                                                                                if (staticPlaneSize == 1)
                                                                                {
                                                                                    staticPlaneSize = planeSize0 * 0.1f;
                                                                                    alternateStaticPlaneSize = planeSize0 * 0.1f;
                                                                                }
                                                                                else if (staticPlaneSize == 0.1f)
                                                                                {
                                                                                    staticPlaneSize = planeSize0;
                                                                                    alternateStaticPlaneSize = planeSize0 * 10;
                                                                                }
                                                                                else if (staticPlaneSize == 0.01f)
                                                                                {
                                                                                    staticPlaneSize = planeSize0;
                                                                                    alternateStaticPlaneSize = planeSize0 * 1000;
                                                                                }
                                                                                //var somecountermax = (float)Math.Abs((somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M14 - (float)Math.Floor(somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.W)) * 10); // 1000
                                                                                var somecountermax = 1;


                                                                                float noiseXZ = 100;
                                                                                float noiseYZ = 100;
                                                                                float noiseXY = 100;

                                                                                float detailScale1 = 20; // 7
                                                                                float heightScale1 = 10; // 5

                                                                                //fastNoise = new FastNoise();

                                                                                noiseXZ *= (fastNoise.GetNoise((x * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M11 + seed) / detailScale1, (z * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M13 + seed) / detailScale1) * heightScale1);
                                                                                float size0 = (1 / planeSize0) * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M12;
                                                                                noiseXZ -= size0;

                                                                                //Program.MessageBox((IntPtr)0, "c:" + somecountermax, "sccs error", 0);
                                                                                noiseYZ *= (fastNoise.GetNoise((y * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M12 + seed) / detailScale1, (z * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M13 + seed) / detailScale1) * heightScale1);
                                                                                float size1 = (1 / planeSize0) * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M11;
                                                                                noiseYZ -= size1;


                                                                                noiseXY *= (fastNoise.GetNoise((x * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M11 + seed) / detailScale1, (y * planeSize0 + somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M12 + seed) / detailScale1) * heightScale1);
                                                                                float size2 = (1 / planeSize0) * somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix[cc].heightmapmat.M13;
                                                                                noiseXY -= size2;
                                                                                */
                                                                                //TOREADD FOR NOISE TEST ON COLOR

                                                                                //noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].colorsNFaces[cc].colorsNFaces.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

                                                                                //float someclampednoisexz = 1;// sc_maths.ClampValue(0.55f + (1 * noiseXZ), 0.35f, 0.75f);
                                                                                //float someclampednoiseyz = 1;// sc_maths.ClampValue(0.55f + (1 * noiseYZ), 0.35f, 0.75f);
                                                                                //float someclampednoisexy = 1;// sc_maths.ClampValue(0.55f + (1 * noiseXY), 0.35f, 0.75f);





                                                                                /*
                                                                                if (divxadd >= 1920)
                                                                                {
                                                                                    divxadd = 0;
                                                                                }

                                                                                if (divyadd >= 1080)
                                                                                {
                                                                                    divyadd = 0;
                                                                                }*/



                                                                                var somepixelvaly = 1;


                                                                                //divxadd = xrowpix;
                                                                                //divyadd = ycolpix;








                                                                                //var somebytearrayindex = (((somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight - 1) - yy) * somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth) + xx;








                                                                                //use 8 if 240 * 135 and 4*4*1
                                                                                //use 4 if 480 * 270 and 4*4*1

                                                                                //use 8 if 240 * 135 and 8*8*1






                                                                                 somepixelsize = 4;
                                                                                 somepixelval = 4;
                                                                                 bytePoser = 0;

                                                                                 somemultiplier = somevoxelvirtualdesktopglobals.planeSize * -10; // -25 // -100
                                                                                 someresw = 0;//
                                                                                 someresh = 0;//
                                                                                 someindexmx = 1;
                                                                                 someindexmy = 1;

                                                                                if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 4 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth == 240 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight == 135)
                                                                                {
                                                                                    someresw = 1920;//
                                                                                    someresh = 1080;//
                                                                                    divxadd = xx * 8;
                                                                                    divyadd = yy * 8;
                                                                                    if (Program._useOculusRift == 0)
                                                                                    {
                                                                                        divxadd = (1920 - divxadd);
                                                                                    }
                                                                                }
                                                                                else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth == 128 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight == 72 && somevoxelvirtualdesktopw == 2 && somevoxelvirtualdesktoph == 2)
                                                                                {
                                                                                     somemindexx = xxx;
                                                                                    if (somemindexx == 0)
                                                                                    {
                                                                                        somemindexx = 1;
                                                                                    }
                                                                                    else if (somemindexx == 1)
                                                                                    {
                                                                                        somemindexx = 2;
                                                                                    }

                                                                                     somemindexy = yyy;
                                                                                    if (somemindexy == 0)
                                                                                    {
                                                                                        somemindexy = 1;
                                                                                    }
                                                                                    else if (somemindexy == 1)
                                                                                    {
                                                                                        somemindexy = 2;
                                                                                    }


                                                                                    someresw = (1920 / 2);
                                                                                    someresh = (1080 / 2);

                                                                                     pixelsize = 8;


                                                                                    if (xxx == 0 && yyy == 0)
                                                                                    {
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;

                                                                                        someresw = 1920 / somemindexx;
                                                                                        someresh = 1080 / somemindexy;

                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }
                                                                                    }
                                                                                    else if (xxx == 0 && yyy == 1)
                                                                                    {
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;

                                                                                        someresw = 1920 / somemindexx;
                                                                                        someresh = 1080 / somemindexy;

                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }
                                                                                    }
                                                                                    else if (xxx == 1 && yyy == 0)
                                                                                    {
                                                                                        somemindexy = 2;
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;
                                                                                        //someresh = 1080 - someresh;
                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        //divxadd = (1920 - divxadd);
                                                                                        //divyadd = (1080 - divyadd);
                                                                                        //divyadd = 1080 - divyadd;
                                                                                        /*if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }*/
                                                                                        divxadd = ((1920 / 2) - divxadd);
                                                                                    }
                                                                                    else if (xxx == 1 && yyy == 1)
                                                                                    {
                                                                                        somemindexy = 1;
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;
                                                                                        //someresh = 1080 - someresh;
                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        //divxadd = (1920 - divxadd);

                                                                                        divxadd = ((1920 / 2) - divxadd);

                                                                                        //divyadd = 1080 - divyadd;
                                                                                        //divyadd = (1080 - divyadd);
                                                                                        /*if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }*/
                                                                                    }
                                                                                }
                                                                                else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 4 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth == 480 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight == 270)
                                                                                {
                                                                                    someresw = 1920;//
                                                                                    someresh = 1080;//
                                                                                    divxadd = xx * 4;
                                                                                    divyadd = yy * 4;
                                                                                    if (Program._useOculusRift == 0)
                                                                                    {
                                                                                        divxadd = (1920 - divxadd);
                                                                                    }
                                                                                }
                                                                                else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth == 240 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight == 135 && somevoxelvirtualdesktopw == 1 && somevoxelvirtualdesktoph == 1)
                                                                                {
                                                                                    someresw = 1920;//
                                                                                    someresh = 1080;//
                                                                                    divxadd = xx * 8;
                                                                                    divyadd = yy * 8;
                                                                                    if (Program._useOculusRift == 0)
                                                                                    {
                                                                                        divxadd = (1920 - divxadd);
                                                                                    }

                                                                                }
                                                                                else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth == 240 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight == 135 && somevoxelvirtualdesktopw == 2 && somevoxelvirtualdesktoph == 2)
                                                                                {
                                                                                     somemindexx = xxx;
                                                                                    if (somemindexx == 0)
                                                                                    {
                                                                                        somemindexx = 1;
                                                                                    }
                                                                                    else if (somemindexx == 1)
                                                                                    {
                                                                                        somemindexx = 2;
                                                                                    }

                                                                                     somemindexy = yyy;
                                                                                    if (somemindexy == 0)
                                                                                    {
                                                                                        somemindexy = 1;
                                                                                    }
                                                                                    else if (somemindexy == 1)
                                                                                    {
                                                                                        somemindexy = 2;
                                                                                    }


                                                                                    someresw = (1920 / 2);
                                                                                    someresh = (1080 / 2);

                                                                                     pixelsize = 4;


                                                                                    if (xxx == 0 && yyy == 0)
                                                                                    {
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;

                                                                                        someresw = 1920 / somemindexx;
                                                                                        someresh = 1080 / somemindexy;

                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }
                                                                                    }
                                                                                    else if (xxx == 0 && yyy == 1)
                                                                                    {
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;

                                                                                        someresw = 1920 / somemindexx;
                                                                                        someresh = 1080 / somemindexy;

                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }
                                                                                    }
                                                                                    else if (xxx == 1 && yyy == 0)
                                                                                    {
                                                                                        somemindexy = 2;
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;
                                                                                        //someresh = 1080 - someresh;
                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        //divxadd = (1920 - divxadd);
                                                                                        //divyadd = (1080 - divyadd);
                                                                                        //divyadd = 1080 - divyadd;
                                                                                        /*if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }*/
                                                                                        divxadd = ((1920 / 2) - divxadd);
                                                                                    }
                                                                                    else if (xxx == 1 && yyy == 1)
                                                                                    {
                                                                                        somemindexy = 1;
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;
                                                                                        //someresh = 1080 - someresh;
                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        //divxadd = (1920 - divxadd);

                                                                                        divxadd = ((1920 / 2) - divxadd);

                                                                                        //divyadd = 1080 - divyadd;
                                                                                        //divyadd = (1080 - divyadd);
                                                                                        /*if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }*/
                                                                                    }
                                                                                }
                                                                                else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth == 480 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight == 270 && somevoxelvirtualdesktopw == 1 && somevoxelvirtualdesktoph == 1)
                                                                                {
                                                                                    someresw = 1920;//
                                                                                    someresh = 1080;//
                                                                                    divxadd = xx * 4;
                                                                                    divyadd = yy * 4;
                                                                                    if (Program._useOculusRift == 0)
                                                                                    {
                                                                                        divxadd = (1920 - divxadd);
                                                                                    }
                                                                                }
                                                                                else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth == 480 && somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight == 270 && somevoxelvirtualdesktopw == 2 && somevoxelvirtualdesktoph == 2)
                                                                                {
                                                                                    somemindexx = xxx;
                                                                                    if (somemindexx == 0)
                                                                                    {
                                                                                        somemindexx = 1;
                                                                                    }
                                                                                    else if (somemindexx == 1)
                                                                                    {
                                                                                        somemindexx = 2;
                                                                                    }

                                                                                     somemindexy = yyy;
                                                                                    if (somemindexy == 0)
                                                                                    {
                                                                                        somemindexy = 1;
                                                                                    }
                                                                                    else if (somemindexy == 1)
                                                                                    {
                                                                                        somemindexy = 2;
                                                                                    }


                                                                                    someresw = (1920 / 2);
                                                                                    someresh = (1080 / 2);

                                                                                     pixelsize = 2;


                                                                                    if (xxx == 0 && yyy == 0)
                                                                                    {
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;

                                                                                        someresw = 1920 / somemindexx;
                                                                                        someresh = 1080 / somemindexy;

                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }
                                                                                    }
                                                                                    else if (xxx == 0 && yyy == 1)
                                                                                    {
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;

                                                                                        someresw = 1920 / somemindexx;
                                                                                        someresh = 1080 / somemindexy;

                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }
                                                                                    }
                                                                                    else if (xxx == 1 && yyy == 0)
                                                                                    {
                                                                                        somemindexy = 2;
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;
                                                                                        //someresh = 1080 - someresh;
                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        //divxadd = (1920 - divxadd);
                                                                                        //divyadd = (1080 - divyadd);
                                                                                        //divyadd = 1080 - divyadd;
                                                                                        /*if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }*/
                                                                                        divxadd = ((1920 / 2) - divxadd);
                                                                                    }
                                                                                    else if (xxx == 1 && yyy == 1)
                                                                                    {
                                                                                        somemindexy = 1;
                                                                                        someresw = someresw * somemindexx;
                                                                                        someresh = someresh * somemindexy;
                                                                                        //someresh = 1080 - someresh;
                                                                                        divxadd = xx * pixelsize;
                                                                                        divyadd = yy * pixelsize;
                                                                                        //divxadd = (1920 - divxadd);

                                                                                        divxadd = ((1920 / 2) - divxadd);

                                                                                        //divyadd = 1080 - divyadd;
                                                                                        //divyadd = (1080 - divyadd);
                                                                                        /*if (Program._useOculusRift == 0)
                                                                                        {
                                                                                            divxadd = (1920 - divxadd);
                                                                                        }*/
                                                                                    }


                                                                                }

                                                                            
                                                                                /*
                                                                                for (int yyyy = divyadd; yyyy < divyadd + divy; yyyy++)
                                                                                {
                                                                                    for (int xxxx = divxadd; xxxx < divxadd + divx; xxxx++)
                                                                                    {
                                                                                        var bytePoserer = ((yyyy * 1920) + xxxx) * 4;

                                                                                        if (bytePoserer <= 1920 * 1080 * 4)
                                                                                        {
                                                                                            _sc_jitter_tasks[0][0].frameByteArray[bytePoserer + 0] = 255;
                                                                                            _sc_jitter_tasks[0][0].frameByteArray[bytePoserer + 1] = 0;
                                                                                            _sc_jitter_tasks[0][0].frameByteArray[bytePoserer + 2] = 0;
                                                                                            _sc_jitter_tasks[0][0].frameByteArray[bytePoserer + 3] = 255;
                                                                                        }
                                                                                    }
                                                                                }*/
                                                                                


                                                                                














                                                                                //Console.WriteLine("" + somevoxelvirtualdesktopindex);




                                                                                //divxadd *= (xxx * somevoxelvirtualdesktopw);
                                                                                //divyadd *= (yyy * somevoxelvirtualdesktoph);

                                                                                if (_sc_jitter_tasks[0][0].frameByteArray != null && _sc_jitter_tasks[0][0].frameByteArray.Length > 0 && Program.exitedprogram == -1)
                                                                                {



                                                                                    if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 4) // || somevoxelvirtualdesktopglobals.tinyChunkWidth == 8
                                                                                    {




                                                                                        //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * 4) + somebyteindex0), "scmsg",0); // 8294400
                                                                                        bytePoser = (((1080 - (divyadd)) * 1920) + (divxadd + (0))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M11 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * 4) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M11 = someemptybytes;
                                                                                        }



                                                                                        bytePoser = (((1080 - (divyadd)) * 1920) + (divxadd + (1))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M12 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M12 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = (((1080 - (divyadd)) * 1920) + (divxadd + (2))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M13 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M13 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd)) * 1920) + (divxadd + (3))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M14 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M14 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (0))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M21 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M21 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (1))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M22 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M22 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (2))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M23 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M23 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (3))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M24 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M24 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 2)) * 1920) + (divxadd + (0))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M31 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M31 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = (((1080 - (divyadd + 2)) * 1920) + (divxadd + (1))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M32 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M32 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 2)) * 1920) + (divxadd + (2))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M33 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M33 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 2)) * 1920) + (divxadd + (3))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M34 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M34 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 3)) * 1920) + (divxadd + (0))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M41 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M41 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 3)) * 1920) + (divxadd + (1))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M42 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M42 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 3)) * 1920) + (divxadd + (2))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M43 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M43 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 3)) * 1920) + (divxadd + (3))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M44 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M44 = someemptybytes;
                                                                                        }
                                                                                    }
                                                                                    else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == -1)
                                                                                    {




                                                                                        //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * 4) + somebyteindex0), "scmsg",0); // 8294400
                                                                                        bytePoser = (((1080 - (divyadd + 0)) * 1920) + (divxadd + (0))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M11 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * 4) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M11 = someemptybytes;
                                                                                        }



                                                                                        bytePoser = (((1080 - (divyadd + 0)) * 1920) + (divxadd + (1))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M12 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M12 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = (((1080 - (divyadd + 0)) * 1920) + (divxadd + (2))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M13 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M13 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 0)) * 1920) + (divxadd + (3))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M14 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M14 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 0)) * 1920) + (divxadd + (4))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M21 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M21 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 0)) * 1920) + (divxadd + (5))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M22 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M22 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 0)) * 1920) + (divxadd + (6))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M23 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M23 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (7))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M24 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M24 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (0))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M31 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M31 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (1))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M32 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M32 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (2))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M33 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M33 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (3))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M34 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M34 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (4))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M41 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M41 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 1)) * 1920) + (divxadd + (5))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M42 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M42 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 3)) * 1920) + (divxadd + (6))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M43 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M43 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = (((1080 - (divyadd + 3)) * 1920) + (divxadd + (7))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M44 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix[cc].heightmapmat.M44 = someemptybytes;
                                                                                        }
                                                                                    }
                                                                                    else if (somevoxelvirtualdesktopglobals.tinyChunkWidth == 8)
                                                                                    {





                                                                                        ///8x8x8








                                                                                        //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser), "scmsg",0); // 8294400
                                                                                        //bytePoser = (rpix + bytePoser);
                                                                                        bytePoser = ((((someresh) - (divyadd + (0 * someindexmy))) * (someresw)) + (divxadd + (0 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M11 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M11 = someemptybytes;
                                                                                        }



                                                                                        bytePoser = ((((someresh) - (divyadd + (0 * someindexmy))) * (someresw)) + (divxadd + (1 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M12 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M12 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = ((((someresh) - (divyadd + (0 * someindexmy))) * (someresw)) + (divxadd + (2 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M13 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M13 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (0 * someindexmy))) * (someresw)) + (divxadd + (3 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M14 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M14 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (0 * someindexmy))) * (someresw)) + (divxadd + (4 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M21 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M21 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (0 * someindexmy))) * (someresw)) + (divxadd + (5 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M22 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M22 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (0 * someindexmy))) * (someresw)) + (divxadd + (6 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M23 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M23 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = ((((someresh) - (divyadd + (0 * someindexmy))) * (someresw)) + (divxadd + (7 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M24 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M24 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (1 * someindexmy))) * (someresw)) + (divxadd + (0 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M31 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M31 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (1 * someindexmy))) * (someresw)) + (divxadd + (1 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M32 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M32 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (1 * someindexmy))) * (someresw)) + (divxadd + (2 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M33 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M33 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (1 * someindexmy))) * (someresw)) + (divxadd + (3 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M34 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M34 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (1 * someindexmy))) * (someresw)) + (divxadd + (4 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M41 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M41 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (1 * someindexmy))) * (someresw)) + (divxadd + (5 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M42 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M42 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (1 * someindexmy))) * (someresw)) + (divxadd + (6 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M43 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M43 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (1 * someindexmy))) * (someresw)) + (divxadd + (7 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M44 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrix[cc].instancematrix.M44 = someemptybytes;
                                                                                        }






                                                                                        //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser), "scmsg",0); // 8294400
                                                                                        bytePoser = ((((someresh) - (divyadd + (2 * someindexmy))) * (someresw)) + (divxadd + (0 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M11 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M11 = someemptybytes;
                                                                                        }



                                                                                        bytePoser = ((((someresh) - (divyadd + (2 * someindexmy))) * (someresw)) + (divxadd + (1 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M12 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M12 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = ((((someresh) - (divyadd + (2 * someindexmy))) * (someresw)) + (divxadd + (2 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M13 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M13 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (2 * someindexmy))) * (someresw)) + (divxadd + (3 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M14 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M14 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (2 * someindexmy))) * (someresw)) + (divxadd + (4 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M21 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M21 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (2 * someindexmy))) * (someresw)) + (divxadd + (5 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M22 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M22 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (2 * someindexmy))) * (someresw)) + (divxadd + (6 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M23 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M23 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (2 * someindexmy))) * (someresw)) + (divxadd + (7 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M24 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M24 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (3 * someindexmy))) * (someresw)) + (divxadd + (0 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M31 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M31 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (3 * someindexmy))) * (someresw)) + (divxadd + (1 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M32 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M32 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (3 * someindexmy))) * (someresw)) + (divxadd + (2 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M33 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M33 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (3 * someindexmy))) * (someresw)) + (divxadd + (3 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M34 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M34 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (3 * someindexmy))) * (someresw)) + (divxadd + (4 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M41 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M41 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (3 * someindexmy))) * (someresw)) + (divxadd + (5 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M42 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M42 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (3 * someindexmy))) * (someresw)) + (divxadd + (6 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M43 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M43 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (3 * someindexmy))) * (someresw)) + (divxadd + (7 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M44 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixb[cc].instancematrix.M44 = someemptybytes;
                                                                                        }









                                                                                        //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser), "scmsg",0); // 8294400
                                                                                        bytePoser = ((((someresh) - (divyadd + (4 * someindexmy))) * (someresw)) + (divxadd + (0 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M11 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M11 = someemptybytes;
                                                                                        }



                                                                                        bytePoser = ((((someresh) - (divyadd + (4 * someindexmy))) * (someresw)) + (divxadd + (1 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M12 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M12 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = ((((someresh) - (divyadd + (4 * someindexmy))) * (someresw)) + (divxadd + (2 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M13 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M13 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (4 * someindexmy))) * (someresw)) + (divxadd + (3 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M14 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M14 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (4 * someindexmy))) * (someresw)) + (divxadd + (4 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M21 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M21 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (4 * someindexmy))) * (someresw)) + (divxadd + (5 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M22 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M22 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (4 * someindexmy))) * (someresw)) + (divxadd + (6 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M23 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M23 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (4 * someindexmy))) * (someresw)) + (divxadd + (7 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M24 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M24 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (5 * someindexmy))) * (someresw)) + (divxadd + (0 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M31 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M31 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (5 * someindexmy))) * (someresw)) + (divxadd + (1 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M32 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M32 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (5 * someindexmy))) * (someresw)) + (divxadd + (2 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M33 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M33 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (5 * someindexmy))) * (someresw)) + (divxadd + (3 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M34 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M34 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (5 * someindexmy))) * (someresw)) + (divxadd + (4 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M41 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M41 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (5 * someindexmy))) * (someresw)) + (divxadd + (5 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M42 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M42 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (5 * someindexmy))) * (someresw)) + (divxadd + (6 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M43 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M43 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (5 * someindexmy))) * (someresw)) + (divxadd + (7 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M44 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixc[cc].instancematrix.M44 = someemptybytes;
                                                                                        }









                                                                                        //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser), "scmsg",0); // 8294400
                                                                                        bytePoser = ((((someresh) - (divyadd + (6 * someindexmy))) * (someresw)) + (divxadd + (0 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M11 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M11 = someemptybytes;
                                                                                        }



                                                                                        bytePoser = ((((someresh) - (divyadd + (6 * someindexmy))) * (someresw)) + (divxadd + (1 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M12 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M12 = someemptybytes;
                                                                                        }


                                                                                        bytePoser = ((((someresh) - (divyadd + (6 * someindexmy))) * (someresw)) + (divxadd + (2 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M13 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M13 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (6 * someindexmy))) * (someresw)) + (divxadd + (3 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M14 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M14 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (6 * someindexmy))) * (someresw)) + (divxadd + (4 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M21 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M21 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (6 * someindexmy))) * (someresw)) + (divxadd + (5 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M22 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M22 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (6 * someindexmy))) * (someresw)) + (divxadd + (6 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M23 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M23 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (6 * someindexmy))) * (someresw)) + (divxadd + (7 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M24 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M24 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (7 * someindexmy))) * (someresw)) + (divxadd + (0 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M31 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M31 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (7 * someindexmy))) * (someresw)) + (divxadd + (1 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M32 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M32 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (7 * someindexmy))) * (someresw)) + (divxadd + (2 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M33 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M33 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (7 * someindexmy))) * (someresw)) + (divxadd + (3 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M34 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M34 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (7 * someindexmy))) * (someresw)) + (divxadd + (4 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M41 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M41 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (7 * someindexmy))) * (someresw)) + (divxadd + (5 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M42 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M42 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (7 * someindexmy))) * (someresw)) + (divxadd + (6 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M43 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M43 = someemptybytes;
                                                                                        }

                                                                                        bytePoser = ((((someresh) - (divyadd + (7 * someindexmy))) * (someresw)) + (divxadd + (7 * someindexmx))) * somepixelsize;
                                                                                        if (bytePoser >= 0 && bytePoser < _sc_jitter_tasks[0][0].frameByteArray.Length)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M44 = _sc_jitter_tasks[0][0].frameByteArray[bytePoser] * somemultiplier;
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                //Program.MessageBox((IntPtr)0, "" + (rpix + (0 * somepixelval) + bytePoser) + " " + _sc_jitter_tasks[0][0].frameByteArray.Length + " " + ex.ToString(), "scmsg", 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesmatrixd[cc].instancematrix.M44 = someemptybytes;
                                                                                        }


                                                                                        /*

                                                                                        xxx = somevoxelvirtualdesktopw ;
                                                                                        yyy = somevoxelvirtualdesktoph ;
                                                                                        zzz = somevoxelvirtualdesktopd ;
                                                                                        x = somevoxelvirtualdesktopglobals.numberOfObjectInWidth;
                                                                                        y = somevoxelvirtualdesktopglobals.numberOfObjectInHeight;
                                                                                        z = somevoxelvirtualdesktopglobals.numberOfObjectInDepth;

                                                                                        xx = somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInWidth;
                                                                                        yy = somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInHeight;
                                                                                        zz = somevoxelvirtualdesktopglobals.numberOfInstancesPerObjectInDepth;*/
                                                                                    }
                                                                                }

                                                                                //TO READD //removed 27 october 27
                                                                                //source_rect.X += wid;
                                                                            }
                                                                            //ycolpix += num_cols;
                                                                            //divyadd += divy;// 1080/ somevoxelvirtualdesktopglobals.tinyChunkHeight;

                                                                            //ycolpix++;
                                                                        }
                                                                        //xrowpix += num_rows;
                                                                        //divxadd += divx;// 1920 / somevoxelvirtualdesktopglobals.tinyChunkWidth;
                                                                    }
                                                                }
                                                                somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].copytobuffer = 0;
                                                            }

                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesLocationW = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesLocationW;
                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].instancesLocationH = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesLocationH;

                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].SC_instancedChunk_InstancesFORWARD = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataFORWARD;
                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].SC_instancedChunk_InstancesRIGHT = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataRIGHT;
                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].SC_instancedChunk_InstancesUP = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instancesDataUP;

                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].heightmapmatrix = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].heightmapmatrix;
                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].lightBuffer = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].lightBufferInstChunk;
                                                            somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayOfChunkData[c].SC_instancedChunk_Instances = somevoxelvirtualdesktop[somevoxelvirtualdesktopindex].arrayofindexzeromesh[c].instances;
                                                        }
                                                    }


                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP


                /*
                var _bitmap = new System.Drawing.Bitmap(1920, 1080, 1920 * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_sc_jitter_tasks[0][0].frameByteArray, 0));
                _bitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows.ToString("00") + columns.ToString("00") + ".png");
                bitmapcounter++;


                activatevrheightmapfeature = 0;*/
            }

            //VOXEL VIRTUAL DESKTOP
            //VOXEL VIRTUAL DESKTOP
            //VOXEL VIRTUAL DESKTOP
            return _sc_jitter_tasks;
        }







        private void _oculus_touch_controls(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft, double realMousePosX, double realMousePosY) //
        {
            if (Program.useArduinoOVRTouchKeymapper == 1)
            {
                var homebuttonLeftTouchControllerArduino = sccs.scgraphics.scupdate.arduinoDIYOculusTouchArrayOfData[5];

                //Console.WriteLine("homebuttonLeftTouchControllerArduino:" + homebuttonLeftTouchControllerArduino);
                if (homebuttonLeftTouchControllerArduino == 1)//buttonPressedOculusTouchLeft == 1048576)
                {
                    if (hasClickedHomeButtonTouchLeft == 0)
                    {
                        sccs.scgraphics.scdirectx.D3D.OVR.RecenterTrackingOrigin(sccs.scgraphics.scdirectx.D3D.sessionPtr);

                        //hmdrotMatrix

                        Quaternion currentRot;// = hmd_matrix_current;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out currentRot);

                        hmd_matrix_current = hmd_matrix_current * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_; //viewMatrix_;


                        hmdmatrixcurrentforpelvis = hmdmatrixcurrentforpelvis * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_;


                        //RotatingMatrixForPelvis = hmdmatrixRot_ * RotatingMatrixForPelvis;



                        //RotatingMatrixForPelvis = hmd_matrix_current * RotatingMatrixForPelvis;
                        //OriginRot = hmd_matrix_current * OriginRot;





                        //Quaternion currentRotAfter;
                        //Quaternion.RotationMatrix(ref hmd_matrix_current, out currentRotAfter);
                        //Quaternion.Lerp(ref currentRot, ref currentRotAfter, 0.001f, out currentRotAfter);
                        //Matrix.RotationQuaternion(ref currentRotAfter,out hmd_matrix_current);

                        //var timeSinceStart = (float)(DateTime.Now - sccs.scgraphics.scupdate.startTime).TotalSeconds;
                        //Matrix worldmatlightrot = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * disco_sphere_rot_speed) * Matrix.RotationY(timeSinceStart * 2 * disco_sphere_rot_speed) * Matrix.RotationZ(timeSinceStart * 3 * disco_sphere_rot_speed);



                        Quaternion _testQuator;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                        var xq = _testQuator.X;
                        var yq = _testQuator.Y;
                        var zq = _testQuator.Z;
                        var wq = _testQuator.W;

                        float roller = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcher = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));//
                        float yawer = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        //RotationY4Pelvis
                        //Matrix tempMat = RotatingMatrixForPelvis * hmdmatrixRot_;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                        xq = _testQuator.X;
                        yq = _testQuator.Y;
                        zq = _testQuator.Z;
                        wq = _testQuator.W;

                        float rollerPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcherPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));//
                        float yawerPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        //var pitch = (float)((Math.PI * pitcher + 45) / 180);
                        //var roll = (float)(0);
                        //var yaw = (float)(0);

                        sccs.scgraphics.scupdate.RotationX4Pelvis = pitcher;
                        sccs.scgraphics.scupdate.RotationY4Pelvis = 0;
                        sccs.scgraphics.scupdate.RotationZ4Pelvis = 0;

                        sccs.scgraphics.scupdate.rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(pitcher, 0, 0);
                        sccs.scgraphics.scupdate.hmdmatrixRot = SharpDX.Matrix.RotationYawPitchRoll(pitcher, 0, 0);

                        hasClickedHomeButtonTouchLeft = 1;
                    }
                }


                if (hasClickedHomeButtonTouchLeft == 1)
                {
                    if (hasClickedHomeButtonTouchLeftCounter > 20)
                    {
                        hasClickedHomeButtonTouchLeft = 0;
                        hasClickedHomeButtonTouchLeftCounter = 0;
                    }
                    hasClickedHomeButtonTouchLeftCounter++;
                }



                var XLeftTouchControllerArduino = sccs.scgraphics.scupdate.arduinoDIYOculusTouchArrayOfData[4];

                //Console.WriteLine("XLeftTouchControllerArduino:" + XLeftTouchControllerArduino);
                if (XLeftTouchControllerArduino == 1)
                {
                    if (sc_menu_scroller_counter >= 75)
                    {
                        if (sc_menu_scroller == 0)
                        {
                            sc_menu_scroller = 1;
                        }
                        else if (sc_menu_scroller == 1)
                        {
                            sc_menu_scroller = 2;
                        }
                        else if (sc_menu_scroller == 2)
                        {
                            sc_menu_scroller = 3;
                        }
                        else if (sc_menu_scroller == 3)
                        {
                            sc_menu_scroller = 4;
                        }
                        else if (sc_menu_scroller == 4)
                        {
                            sc_menu_scroller = 0;
                        }
                        sc_menu_scroller_counter = 0;
                    }
                }
                sc_menu_scroller_counter++;



                var YLeftTouchControllerArduino = sccs.scgraphics.scupdate.arduinoDIYOculusTouchArrayOfData[3];


                if (_has_locked_screen_pos_counter >= 50)
                {
                    if (YLeftTouchControllerArduino == 1)
                    {
                        if (sc_menu_scroller == 0)
                        {
                            if (frame_counter_4_buttonY >= 75)
                            {
                                if (display_grid_type == 0)
                                {
                                    display_grid_type = 1;
                                }
                                else if (display_grid_type == 1)
                                {
                                    display_grid_type = 2;
                                }
                                else if (display_grid_type == 2)
                                {
                                    display_grid_type = 3;
                                }
                                else if (display_grid_type == 3)
                                {
                                    display_grid_type = 0;
                                }
                                frame_counter_4_buttonY = 0;
                            }
                        }
                        else if (sc_menu_scroller == 1)
                        {
                            if (_has_locked_screen_pos == 0)
                            {
                                _has_locked_screen_pos_counter = 0;
                                _has_locked_screen_pos = 1;
                            }
                            else if (_has_locked_screen_pos == 1)
                            {
                                _has_locked_screen_pos_counter = 0;
                                _has_locked_screen_pos = 0;
                            }
                        }
                    }
                }
                frame_counter_4_buttonY++;
                _has_locked_screen_pos_counter++;






                if (sc_menu_scroller == 2)
                {
                    /////////////LEFT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXLeft >= 0 && percentXLeft <= 1920 && percentYLeft >= 0 && percentYLeft <= 1080)
                    {
                        var absoluteMoveX = Convert.ToUInt32((percentXLeft * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                        var absoluteMoveY = Convert.ToUInt32((percentYLeft * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                        if (percentXLeft >= 0 && percentXLeft < sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                        {

                        }
                        else
                        {
                            percentXLeft = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                            absoluteMoveX = Convert.ToUInt32((percentXLeft * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                        }

                        if (percentYLeft >= 0 && percentYLeft < sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                        {

                        }
                        else
                        {
                            percentYLeft = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                            absoluteMoveY = Convert.ToUInt32((percentYLeft * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);
                        }


                        //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                        //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                        //then disregard the first click location.
                        if (_frameCounterTouchLeft >= 50)
                        {

                            if (YLeftTouchControllerArduino == 1)
                            {
                                if (hasClickedBUTTONX == 0)
                                {
                                    absoluteMoveX = Convert.ToUInt32((percentXLeft * 65535 - 1) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                    absoluteMoveY = Convert.ToUInt32((percentYLeft * 65535 - 1) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                                    //if (_out_of_bounds_left == 0)
                                    //{
                                    //  
                                    //}

                                    SetCursorPos((int)percentXLeft, (int)percentYLeft);
                                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                    _frameCounterTouchLeft = 0;

                                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                    //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, absoluteMoveX, absoluteMoveY, 0, 0);


                                    Program.mousesim.LeftButtonDown();
                                    //Program.mousesim.MoveMouseToPositionOnVirtualDesktop();
                                    //Program.mousesim.LeftButtonClick();

                                    //_lastMousePosXLeft = absoluteMoveX;
                                    //_lastMousePosYLeft = absoluteMoveY;
                                    _canResetCounterTouchLeftButtonX = true;
                                    hasClickedBUTTONX = 1;
                                }
                            }
                            /*else if (YLeftTouchControllerArduino == 512)
                            {
                                if (hasClickedBUTTONY == 0)
                                {
                                    if (_out_of_bounds_right == 0)
                                    {
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535 - 1) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535 - 1) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);
                                    }
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    //_lastMousePosX = absoluteMoveX;
                                    //_lastMousePosY = absoluteMoveY;
                                    //_canResetCounterTouchRight = true;
                                    hasClickedBUTTONY = 1;
                                }
                            }*/


                        }
                        _out_of_bounds_left = 0;
                    }
                    else
                    {
                        _out_of_bounds_left = 1;
                    }
                    if (hasClickedBUTTONX == 1)
                    {
                        if (_frameCounterTouchLeft >= 10)
                        {
                            hasClickedBUTTONX = 0;
                        }
                    }
                }
                _frameCounterTouchLeft++;

                //TO READD WHEN I ORDER THE ARDUINO TRIGGERS OR NOT
                //TO READD WHEN I ORDER THE ARDUINO TRIGGERS OR NOT
                /*
                if (indexTriggerRight[1] > 0.0001f)
                {
                    if (heightmapscale > heightmapscaleMax)
                    {
                        heightmapscale = heightmapscaleMax;
                    }
                    else
                    {
                        heightmapscale += 0.00001f;
                    }
                }

                if (indexTriggerLeft[0] > 0.0001f)
                {
                    if (heightmapscale < heightmapscaleMin)
                    {
                        heightmapscale = heightmapscaleMin;
                    }
                    else
                    {
                        heightmapscale -= 0.00001f;
                    }
                }
                //if (Math.Abs(Math.Abs(indexTriggerRightLastAbs) - Math.Abs(indexTriggerRight[1])) > 0.0001f)
                //if (Math.Abs(Math.Abs(indexTriggerLeftLastAbs) - Math.Abs(indexTriggerLeft[0])) > 0.0001f)
                indexTriggerRightLastAbs = indexTriggerRight[1];
                indexTriggerLeftLastAbs = indexTriggerLeft[0];*/
                //TO READD WHEN I ORDER THE ARDUINO TRIGGERS OR NOT
                //TO READD WHEN I ORDER THE ARDUINO TRIGGERS OR NOT
            }
            else if (Program.useArduinoOVRTouchKeymapper == 0)
            {
                if (buttonPressedOculusTouchLeft == 1048576)
                {
                    if (hasClickedHomeButtonTouchLeft == 0)
                    {
                        sccs.scgraphics.scdirectx.D3D.OVR.RecenterTrackingOrigin(sccs.scgraphics.scdirectx.D3D.sessionPtr);

                        //hmdrotMatrix

                        Quaternion currentRot;// = hmd_matrix_current;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out currentRot);

                        hmd_matrix_current = hmd_matrix_current * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_; //viewMatrix_;


                        //Quaternion currentRotAfter;
                        //Quaternion.RotationMatrix(ref hmd_matrix_current, out currentRotAfter);
                        //Quaternion.Lerp(ref currentRot, ref currentRotAfter, 0.001f, out currentRotAfter);
                        //Matrix.RotationQuaternion(ref currentRotAfter,out hmd_matrix_current);

                        //var timeSinceStart = (float)(DateTime.Now - sccs.scgraphics.scupdate.startTime).TotalSeconds;
                        //Matrix worldmatlightrot = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * disco_sphere_rot_speed) * Matrix.RotationY(timeSinceStart * 2 * disco_sphere_rot_speed) * Matrix.RotationZ(timeSinceStart * 3 * disco_sphere_rot_speed);



                        Quaternion _testQuator;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                        var xq = _testQuator.X;
                        var yq = _testQuator.Y;
                        var zq = _testQuator.Z;
                        var wq = _testQuator.W;

                        float roller = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcher = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));//
                        float yawer = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        //RotationY4Pelvis
                        //Matrix tempMat = RotatingMatrixForPelvis * hmdmatrixRot_;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                        xq = _testQuator.X;
                        yq = _testQuator.Y;
                        zq = _testQuator.Z;
                        wq = _testQuator.W;

                        float rollerPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcherPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));//
                        float yawerPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        //var pitch = (float)((Math.PI * pitcher + 45) / 180);
                        //var roll = (float)(0);
                        //var yaw = (float)(0);

                        sccs.scgraphics.scupdate.RotationX4Pelvis = pitcher;
                        sccs.scgraphics.scupdate.RotationY4Pelvis = 0;
                        sccs.scgraphics.scupdate.RotationZ4Pelvis = 0;

                        sccs.scgraphics.scupdate.rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(pitcher, 0, 0);
                        sccs.scgraphics.scupdate.hmdmatrixRot = SharpDX.Matrix.RotationYawPitchRoll(pitcher, 0, 0);

                        hasClickedHomeButtonTouchLeft = 1;
                    }
                }

                if (hasClickedHomeButtonTouchLeft == 1)
                {
                    if (hasClickedHomeButtonTouchLeftCounter > 20)
                    {
                        hasClickedHomeButtonTouchLeft = 0;
                        hasClickedHomeButtonTouchLeftCounter = 0;
                    }
                    hasClickedHomeButtonTouchLeftCounter++;
                }

                if (buttonPressedOculusTouchLeft != 0)
                {
                    if (buttonPressedOculusTouchLeft == 512)
                    {
                        if (sc_menu_scroller_counter >= 75)
                        {
                            if (sc_menu_scroller == 0)
                            {
                                sc_menu_scroller = 1;
                            }
                            else if (sc_menu_scroller == 1)
                            {
                                sc_menu_scroller = 2;
                            }
                            else if (sc_menu_scroller == 2)
                            {
                                sc_menu_scroller = 0;
                            }
                            /*else if (sc_menu_scroller == 3)
                            {
                                sc_menu_scroller = 0;
                            }*/
                            /*else if (sc_menu_scroller == 4)
                            {
                                sc_menu_scroller = 0;
                            }*/
                            sc_menu_scroller_counter = 0;
                        }
                    }
                }
                sc_menu_scroller_counter++;

                if (_has_locked_screen_pos_counter >= 50)
                {
                    if (buttonPressedOculusTouchLeft == 256)
                    {
                        if (sc_menu_scroller == 0)
                        {
                            if (frame_counter_4_buttonY >= 75)
                            {
                                if (display_grid_type == 0)
                                {
                                    display_grid_type = 1;
                                }
                                else if (display_grid_type == 1)
                                {
                                    display_grid_type = 2;
                                }
                                else if (display_grid_type == 2)
                                {
                                    display_grid_type = 3;
                                }
                                else if (display_grid_type == 3)
                                {
                                    display_grid_type = 0;
                                }

                                frame_counter_4_buttonY = 0;
                            }
                        }
                        else if (sc_menu_scroller == 1)
                        {
                            if (_has_locked_screen_pos == 0)
                            {
                                _has_locked_screen_pos_counter = 0;
                                _has_locked_screen_pos = 1;
                            }
                            else if (_has_locked_screen_pos == 1)
                            {
                                _has_locked_screen_pos_counter = 0;
                                _has_locked_screen_pos = 0;
                            }
                        }
                        else if (sc_menu_scroller == 2)
                        {
                            if (frame_counter_4_buttonY >= 75)
                            {

                                if (resetvoxelladdercounter == 1)
                                {
                                    //5 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP)
                                    //4 full chunk cube all 0s for path tracing with path traced with bytes becoming 1s when the player moves around the invisible chunk.
                                    //3 full chunk cube all 0s for a way to visualize spatial location of objects in a 3d scene.
                                    //2 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP) - using random perlin WIP
                                    //1 WIP TRANSPARENCY GRID LIKE CHUNK WITH MY UPCOMING CODING CHALLENGE TO LEARN RASTERTEK C# TRANSPARENCY.

                                    if (resetvoxelladder == 0)
                                    {
                                        Program.usetypeofvoxel = 1; //5 // full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP)
                                        somechunkplayerspatiallocationresetcounter = 0;
                                        somechunkresetcounter = 0;
                                        resetvoxelladdercounter = 2;
                                        resetvoxelladder = 1;
                                    }
                                    /*else if (resetvoxelladder == 1)
                                    {
                                        Program.usetypeofvoxel = 4; // full chunk cube all 0s for path tracing with path traced with bytes becoming 1s when the player moves around the invisible chunk.
                                        somechunkplayerspatiallocationresetcounter = 0;
                                        somechunkresetcounter = 0;
                                        resetvoxelladdercounter = 2;
                                        resetvoxelladder = 2;
                                    }
                                    else if (resetvoxelladder == 2)
                                    {
                                        Program.usetypeofvoxel = 3; // full chunk cube all 0s for a way to visualize spatial location of objects in a 3d scene.
                                        somechunkplayerspatiallocationresetcounter = 0;
                                        somechunkresetcounter = 0;
                                        resetvoxelladdercounter = 2;
                                        resetvoxelladder = 3;
                                    }
                                    else if (resetvoxelladder == 3)
                                    {
                                        Program.usetypeofvoxel = 2; // full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP) - using random perlin WIP
                                        somechunkplayerspatiallocationresetcounter = 0;
                                        somechunkresetcounter = 0;
                                        resetvoxelladdercounter = 2;
                                        resetvoxelladder = 4;
                                    }*/
                                    /*else if (resetvoxelladder == 4)
                                    {
                                        Program.usetypeofvoxel = 1; // WIP TRANSPARENCY GRID LIKE CHUNK WITH MY UPCOMING CODING CHALLENGE TO LEARN RASTERTEK C# TRANSPARENCY.
                                        somecounterresetcounter = 0;
                                        resetvoxelladdercounter = 2;
                                        resetvoxelladder = 5;
                                    }*/
                                    else if (resetvoxelladder == 1)
                                    {
                                        resetvoxelladder = 2;
                                    }
                                    else if (resetvoxelladder == 2)
                                    {
                                        resetvoxelladder = 0;
                                    }
                                }
                                frame_counter_4_buttonY = 0;
                            }
                        }
                    }
                }
                frame_counter_4_buttonY++;
                _has_locked_screen_pos_counter++;






                if (sc_menu_scroller == 2)
                {
                    /////////////LEFT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXLeft >= 0 && percentXLeft <= 1920 && percentYLeft >= 0 && percentYLeft <= 1080)
                    {
                        var absoluteMoveX = Convert.ToUInt32((percentXLeft * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                        var absoluteMoveY = Convert.ToUInt32((percentYLeft * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                        if (percentXLeft >= 0 && percentXLeft < sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                        {

                        }
                        else
                        {
                            percentXLeft = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                            absoluteMoveX = Convert.ToUInt32((percentXLeft * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                        }

                        if (percentYLeft >= 0 && percentYLeft < sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                        {

                        }
                        else
                        {
                            percentYLeft = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                            absoluteMoveY = Convert.ToUInt32((percentYLeft * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);
                        }


                        //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                        //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                        //then disregard the first click location.
                        if (_frameCounterTouchLeft >= 50)
                        {
                            if (buttonPressedOculusTouchLeft != 0)
                            {
                                if (buttonPressedOculusTouchLeft == 256)
                                {
                                    if (hasClickedBUTTONX == 0)
                                    {
                                        absoluteMoveX = Convert.ToUInt32((percentXLeft * 65535 - 1) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                        absoluteMoveY = Convert.ToUInt32((percentYLeft * 65535 - 1) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                                        //if (_out_of_bounds_left == 0)
                                        //{
                                        //  
                                        //}

                                        SetCursorPos((int)percentXLeft, (int)percentYLeft);
                                        //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                        _frameCounterTouchLeft = 0;

                                        //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                        //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, absoluteMoveX, absoluteMoveY, 0, 0);


                                        Program.mousesim.LeftButtonDown();
                                        //Program.mousesim.MoveMouseToPositionOnVirtualDesktop();
                                        //Program.mousesim.LeftButtonClick();

                                        //_lastMousePosXLeft = absoluteMoveX;
                                        //_lastMousePosYLeft = absoluteMoveY;
                                        _canResetCounterTouchLeftButtonX = true;
                                        hasClickedBUTTONX = 1;
                                    }
                                }
                                /*else if (buttonPressedOculusTouchLeft == 512)
                                {
                                    if (hasClickedBUTTONY == 0)
                                    {
                                        if (_out_of_bounds_right == 0)
                                        {
                                            absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535 - 1) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                            absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535 - 1) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);
                                        }
                                        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                        //_lastMousePosX = absoluteMoveX;
                                        //_lastMousePosY = absoluteMoveY;
                                        //_canResetCounterTouchRight = true;
                                        hasClickedBUTTONY = 1;
                                    }
                                }*/

                            }
                        }
                        _out_of_bounds_left = 0;
                    }
                    else
                    {
                        _out_of_bounds_left = 1;
                    }
                    if (hasClickedBUTTONX == 1)
                    {
                        if (_frameCounterTouchLeft >= 10)
                        {
                            hasClickedBUTTONX = 0;
                        }
                    }
                }
                _frameCounterTouchLeft++;




                if (indexTriggerRight[1] > 0.0001f)
                {
                    if (heightmapscale > heightmapscaleMax)
                    {
                        heightmapscale = heightmapscaleMax;
                    }
                    else
                    {
                        heightmapscale += 0.00001f;
                    }
                }

                if (indexTriggerLeft[0] > 0.0001f)
                {
                    if (heightmapscale < heightmapscaleMin)
                    {
                        heightmapscale = heightmapscaleMin;
                    }
                    else
                    {
                        heightmapscale -= 0.00001f;
                    }
                }
                //if (Math.Abs(Math.Abs(indexTriggerRightLastAbs) - Math.Abs(indexTriggerRight[1])) > 0.0001f)
                //if (Math.Abs(Math.Abs(indexTriggerLeftLastAbs) - Math.Abs(indexTriggerLeft[0])) > 0.0001f)
                indexTriggerRightLastAbs = indexTriggerRight[1];
                indexTriggerLeftLastAbs = indexTriggerLeft[0];

















                if (buttonPressedOculusTouchLeft != 0)
                {
                    //Program.MessageBox((IntPtr)0, buttonPressedOculusTouchLeft + "", "sccs message", 0);

                    if (buttonPressedOculusTouchLeft == 1024 && hasClickedBUTTONX == 0)
                    {







                        /*string windowsKeyboard = "osk";

                        foreach (Process clsProcess in Process.GetProcesses())
                        {
                            if (clsProcess.ProcessName.ToLower().Contains(windowsKeyboard.ToLower()))
                            {
                                break;
                            }
                            else
                            {
                                Process proc = new Process();
                                proc.StartInfo.FileName = windowsKeyboard + ".exe";
                                proc.Start();
                                break;
                            }
                        }*/



                        /*string windir = Environment.GetEnvironmentVariable("windir");

                        Process p = new Process();
                        p.StartInfo.FileName = windir + @"\System32\cmd.exe";
                        p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.Start();
                        p.Dispose();*/



                        /*ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = false;
                        startInfo.UseShellExecute = true;
                        startInfo.WorkingDirectory = @"c:\WINDOWS\system32\";
                        startInfo.FileName = "osk.exe";
                        startInfo.Verb = "runas";
                        startInfo.WindowStyle = ProcessWindowStyle.Normal;

                        try
                        {
                            using (Process process = Process.Start(startInfo))
                            {
                                process.WaitForExit();
                            }
                        }
                        catch (Exception)
                        {
                            //throw;
                        }*/



                        //System.Diagnostics.Process.Start("osk.exe");
                        /*string windir = Environment.GetEnvironmentVariable("windir");

                        Process p = new Process();
                        p.StartInfo.FileName = windir + @"\System32\cmd.exe";
                        p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.Start();
                        p.Dispose();*/

                        //var path64 = @"c:\windows\sysnative\osk.exe"; //@"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
                        //var path32 = @"c:\windows\sysnative\osk.exe";// @"C:\windows\system32\osk.exe"; 
                        //var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;

                        //string somestr = getOskPath(@"c:\windows\system32\");




                        /*string[] dirs = Directory.GetFiles(@"c:\Windows\System32\", "c*");

                        Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);

                        foreach (string dir in dirs)
                        {

                            Console.WriteLine(dir);
                            if (dir == @"c:\Windows\System32\osk.exe")
                            {
                                Program.MessageBox((IntPtr)0, "", "sccs error", 0);
                                Console.WriteLine(dir);
                                string somestr = getOskPath(dir);

                                Program.MessageBox((IntPtr)0, "" + somestr, "sccs error", 0);
                            }
                        }*/





                        /*string somestr = @"c:\windows\system32\osk.exe";// getOskPath(@"c:\windows\system32\osk.exe");
                        var permissionSet = new PermissionSet(PermissionState.None);
                        var writePermission = new FileIOPermission(FileIOPermissionAccess.Read, somestr); //write
                        permissionSet.AddPermission(writePermission);

                        if (permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                        {
                            Program.MessageBox((IntPtr)0, "" + somestr, "sccs error", 0);

                            Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(OnScreenKeyboardExe));

                            if (p.Length == 0)
                            {
                                // we must start osk from an MTA thread
                                if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
                                {
                                    ThreadStart start = new ThreadStart(ShowKeyboard);// StartOsk);
                                    Thread thread = new Thread(start);
                                    thread.SetApartmentState(ApartmentState.MTA);
                                    thread.Start();
                                    thread.Join();
                                }
                                else
                                {
                                    ShowKeyboard();//StartOsk();
                                }
                            }
                            else
                            {
                                // there might be a race condition if the process terminated 
                                // meanwhile -> proper exception handling should be added
                                //
                                SendMessage(p[0].ProgramHandle, WM_SYSCOMMAND, new IntPtr(SC_RESTORE), new IntPtr(0)); //ProgramHandle
                            }
                        }
                        else
                        {
                          
                        }*/


                        string windowsKeyboard = "osk";

                        foreach (Process clsProcess in Process.GetProcesses())
                        {
                            if (clsProcess.ProcessName.ToLower().Contains(windowsKeyboard.ToLower()))
                            {
                                break;
                            }
                            else
                            {
                                // do your stuff

                                //bool sucessfullyDisabledWow64Redirect = false;

                                //string path = Path.Combine(dir, "osk.exe");



                                /*Process proc = new Process();
                                proc.StartInfo.FileName = windowsKeyboard + ".exe";
                                proc.Start();*/

                                ProcessStartInfo psi = new ProcessStartInfo();
                                //psi.FileName = OnScreenKeyboardExe;// path64;
                                string windir = Environment.GetEnvironmentVariable("windir");
                                psi.WorkingDirectory = @"c:\WINDOWS\system32\";
                                psi.FileName = windir + @"\System32\cmd.exe";
                                psi.Arguments = "/C " + windir + @"\System32\osk.exe";


                                psi.UseShellExecute = false;
                                psi.RedirectStandardOutput = true;
                                psi.RedirectStandardError = true;
                                psi.CreateNoWindow = false;
                                psi.Verb = "runas";

                                //Process p = System.Diagnostics.Process.Start(path);
                                Process proc = new Process();
                                proc.StartInfo = psi;
                                proc.Start();

                                bool sucessfullyDisabledWow64Redirect = false;
                                if (System.Environment.Is64BitOperatingSystem)
                                {
                                    IntPtr ptr = new IntPtr();
                                    sucessfullyDisabledWow64Redirect = Wow64DisableWow64FsRedirection(ref ptr);
                                    path = string.Empty;
                                }



                                //Process proc = new Process();
                                //proc.StartInfo = psi;
                                //proc.Start();





                                break;
                            }
                        }












                        //Program.MessageBox((IntPtr)0, "fuck you", "sccs message", 0);
                        /*string windowsKeyboard = "osk";

                        foreach (Process clsProcess in Process.GetProcesses())
                        {
                            if (clsProcess.ProcessName.ToLower().Contains(windowsKeyboard.ToLower()))
                            {
                                break;
                            }
                            else
                            {
                                Process proc = new Process();
                                proc.StartInfo.FileName = windowsKeyboard + ".exe";
                                proc.Start();
                                break;
                            }
                        }*/
                        //hasClickedBUTTONX = 1;
                    }
                }
                else if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft == 1 || buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
                {
                    if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft == 1)
                    {
                        hasClickedHomeButtonTouchLeft = 0;
                    }
                    else if (buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
                    {
                        hasClickedBUTTONX = 0;
                    }

                }



                /*if (buttonPressedOculusTouchLeft != 0)
                {
                    var yo = _updateFunctionStopwatchLeftThumbstick.Elapsed.Ticks;

                    if (yo >= 100)
                    {
                        if (buttonPressedOculusTouchLeft == 1024)
                        {

                            _updateFunctionBoolLeftThumbStick = true;
                        }
                    }
                }*/
            }

        }



        private void _MicrosoftWindowsMouseRight(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft, double realMousePosX, double realMousePosY) //, double realOculusRiftCursorPosX, double realOculusRiftCursorPosY
        {
            try
            {
                //MessageBox((IntPtr)0, "percentXRight: " + percentXRight + " percentYRight: " + percentYRight, "mouse move", 0);
                //Console.WriteLine("percentXRight: " + percentXRight + " percentYRight: " + percentYRight);

                if (_indexMouseMove == 0)
                {
                    //MessageBox((IntPtr)0, "test0", "mouse move", 0);
                    /////////////RIGHT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXRight >= 0 && percentXRight <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth && percentYRight >= 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight &&
                        realMousePosX >= 0 && realMousePosX <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth && realMousePosY >= 0 && realMousePosY <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                    {
                        //MessageBox((IntPtr)0, "test1", "mouse move", 0);

                        //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                        //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                        var yo = _updateFunctionStopwatchRight.Elapsed.Ticks;

                        if (_hasLockedMouse == 0)
                        {
                            if (yo >= 250)
                            {
                                var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                                if (realMousePosX >= 0 && realMousePosX < sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                                {

                                }
                                else
                                {
                                    realMousePosX = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                                    absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                }

                                if (realMousePosY >= 0 && realMousePosY < sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                                {

                                }
                                else
                                {
                                    realMousePosY = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                                    absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535 - 1)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);
                                }


                                //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                if (_frameCounterTouchRight <= 20)
                                {
                                    SetCursorPos((int)realMousePosX, (int)realMousePosY);
                                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                    _frameCounterTouchRight = 0;
                                }

                                _updateFunctionStopwatchRight.Stop();
                                _updateFunctionBoolRight = true;
                            }
                        }

                        //Console.WriteLine(percentXRight + "_" + percentYRight);
                        //MessageBox((IntPtr)0,  "test", "mouse move", 0);
                        //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                        //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                        //then disregard the first click location.

                        if (buttonPressedOculusTouchRight != 0)
                        {
                            if (buttonPressedOculusTouchRight == 1)
                            {
                                if (hasClickedBUTTONA == 0)
                                {
                                    var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                    var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                                    if (realMousePosX >= 0 && realMousePosX < sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosX = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                    }

                                    if (realMousePosY >= 0 && realMousePosY < sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosY = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);
                                    }


                                    if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                                    {
                                        SetCursorPos((int)realMousePosX, (int)realMousePosY);
                                        //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                        _frameCounterTouchRight = 0;
                                    }

                                    //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0); //| MOUSEEVENTF_ABSOLUTE

                                    Program.mousesim.LeftButtonDown();

                                    _lastMousePosXRight = absoluteMoveX;
                                    _lastMousePosYRight = absoluteMoveY;

                                    _canResetCounterTouchRightButtonA = true;
                                    hasClickedBUTTONA = 1;
                                }
                            }
                            else if (buttonPressedOculusTouchRight == 2)
                            {
                                if (hasClickedBUTTONB == false)
                                {
                                    var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                    var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                                    if (realMousePosX >= 0 && realMousePosX < sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosX = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                                    }

                                    if (realMousePosY >= 0 && realMousePosY < sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosY = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                                    }


                                    //mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    Program.mousesim.RightButtonDown();
                                    hasClickedBUTTONB = true;
                                }
                            }
                        }
                        _out_of_bounds_right = 0;
                    }
                    else
                    {
                        //Program.MessageBox((IntPtr)0, "out of bounds 0", "mouse move", 0);
                        //percentYRight >= 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight

                        var someorixright = percentXRight;
                        var someoriyright = percentYRight;

                        if (percentXRight < 0 && percentXRight <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                                percentXRight >= 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                                percentXRight < 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                        {
                            if (percentXRight < 0 && percentXRight <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                            {
                                percentXRight = 0;
                            }
                            else if (percentXRight >= 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                            {
                                percentXRight = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                            }
                        }

                        if (percentYRight < 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                           percentYRight >= 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                           percentYRight < 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                        {
                            if (percentYRight < 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                            {
                                percentYRight = 0;
                            }
                            else if (percentYRight >= 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                            {
                                percentYRight = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                            }
                        }


                        int somediv = 2;

                        if (someorixright < 0 && Math.Abs(someorixright) > sccs.scgraphics.scdirectx.D3D.SurfaceWidth / somediv ||
                            someoriyright < 0 && Math.Abs(someoriyright) > sccs.scgraphics.scdirectx.D3D.SurfaceHeight / somediv ||
                            someorixright >= 0 && (someorixright) > (sccs.scgraphics.scdirectx.D3D.SurfaceWidth) + sccs.scgraphics.scdirectx.D3D.SurfaceWidth / somediv ||
                            someoriyright >= 0 && (someoriyright) > (sccs.scgraphics.scdirectx.D3D.SurfaceHeight) + sccs.scgraphics.scdirectx.D3D.SurfaceHeight / somediv)
                        {

                        }
                        else
                        {

                            float dotres1;
                            var vert0 = new Vector3(_screenDirMatrix_correct_pos[0][0][0].M41, _screenDirMatrix_correct_pos[0][0][0].M42, _screenDirMatrix_correct_pos[0][0][0].M43);
                            var vert1 = new Vector3(_screenDirMatrix_correct_pos[0][0][1].M41, _screenDirMatrix_correct_pos[0][0][1].M42, _screenDirMatrix_correct_pos[0][0][1].M43);
                            var vert2 = new Vector3(_screenDirMatrix_correct_pos[0][0][2].M41, _screenDirMatrix_correct_pos[0][0][2].M42, _screenDirMatrix_correct_pos[0][0][2].M43);
                            var vert3 = new Vector3(_screenDirMatrix_correct_pos[0][0][3].M41, _screenDirMatrix_correct_pos[0][0][3].M42, _screenDirMatrix_correct_pos[0][0][3].M43);

                            Vector3 somevec0 = (vert2 - vert0);
                            Vector3 somevec1 = (vert1 - vert0);
                            //sc_maths.Dot(somevec0.X, somevec0.Y, somevec0.X, somevec0.Y);
                            //float dotres0;
                            //Vector3.Dot(ref somevec0, ref rayDirRighter, out dotres0);
                            Vector3.Dot(ref somevec1, ref rayDirRighter, out dotres1);
                            //Console.WriteLine(" " + " " + dotres1);
                            if (_frameCounterTouchRight <= 20 && dotres1 > -0.35f)
                            {
                                SetCursorPos((int)realMousePosX, (int)realMousePosY);
                                //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                _frameCounterTouchRight = 0;
                            }
                        }






                        _out_of_bounds_right = 1;
                    }

                    if (hasClickedBUTTONACounter >= 25)
                    {
                        //////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                        if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0 || hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                        {
                            if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0)
                            {
                                //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                Program.mousesim.LeftButtonUp();
                                hasClickedBUTTONACounter = 0;
                                hasClickedBUTTONA = 0;
                            }
                            else if (hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                            {
                                //mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                Program.mousesim.RightButtonUp();
                                hasClickedBUTTONACounter = 0;
                                hasClickedBUTTONB = false;
                            }
                        }
                    }
                    //if (_canResetCounterTouchRightButtonA)
                    //{
                    //  
                    //}
                    _frameCounterTouchRight++;
                    if (_frameCounterTouchRight >= 30)
                    {
                        _frameCounterTouchRight = 0;
                        _canResetCounterTouchRightButtonA = false;
                    }

                    if (_out_of_bounds_oculus_rift == 0)
                    {
                        long test = 80;
                        /////////RIGHT THUMBSTICK///////////
                        var yo6 = _updateFunctionStopwatchRightThumbstickGoLeft.Elapsed.Milliseconds;
                        if (yo6 >= 75)
                        {
                            /*if (thumbStickRight[1].Y <= -0.1f && hasUsedThumbStickRightE == false)
                            {
                                if (test < -0.99)
                                {
                                    test = (long)-0.99;
                                }

                                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -test, 0);
                                //Console.WriteLine("test");

                                hasUsedThumbStickRightE = true;
                            }
                            else if (hasUsedThumbStickRightE)
                            {
                                hasUsedThumbStickRightE = false;
                            }*/
                            _updateFunctionStopwatchRightThumbstickGoLeft.Stop();
                            _updateFunctionBoolRightThumbStickGoLeft = true;
                        }
                        ///////////////////////////////////////////////////////////////////////////

                        /////////RIGHT THUMBSTICK/////////////////////////////////////////////////////
                        var yo7 = _updateFunctionStopwatchRightThumbstickGoRight.Elapsed.Milliseconds;
                        if (yo7 >= 75)
                        {
                            /*if (thumbStickRight[1].Y >= 0.1f && hasUsedThumbStickRightQ == false)
                            {
                                if (test > 0.99f)
                                {
                                    test = (long)0.99;
                                }

                                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, test, 0);
                                hasUsedThumbStickRightQ = true;
                            }
                            else if (hasUsedThumbStickRightQ)
                            {
                                hasUsedThumbStickRightQ = false;
                            }*/
                            _updateFunctionStopwatchRightThumbstickGoRight.Stop();
                            _updateFunctionBoolRightThumbStickGoRight = true;
                        }
                    }























                    /*//////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                    if (hasClickedBUTTONX == 1 && buttonPressedOculusTouchLeft == 0 || hasClickedBUTTONY == 1 && buttonPressedOculusTouchLeft == 0)
                    {
                        if (hasClickedBUTTONX == 1 && buttonPressedOculusTouchLeft == 0)
                        {
                            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            hasClickedBUTTONX = 0;
                        }
                        else if (hasClickedBUTTONY == 1 && buttonPressedOculusTouchLeft == 0)
                        {
                            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                            hasClickedBUTTONY = 0;
                        }
                    }*/

                    //if (_canResetCounterTouchLeftButtonX)
                    //{
                    //
                    //}



                    //if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft)
                    //{
                    //    hasClickedHomeButtonTouchLeft = false;
                    //}
                }
                else if (_indexMouseMove == 100)
                {
                    /*/////////////LEFT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXLeft >= 0 && percentXLeft <= 1920 && percentYLeft >= 0 && percentYLeft <= 1080)
                    {
                        var absoluteMoveX = Convert.ToUInt32((percentXLeft * 65535) / 1920);
                        var absoluteMoveY = Convert.ToUInt32((percentYLeft * 65535) / 1080);

                        var yo = _updateFunctionStopwatchLeft.Elapsed.Milliseconds;

                        if (yo >= 10)
                        {
                            mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                            _updateFunctionStopwatchLeft.Stop();
                            _updateFunctionBoolLeft = true;
                        }

                        //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                        //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                        //then disregard the first click location.

                        if (buttonPressedOculusTouchLeft != 0)
                        {
                            if (buttonPressedOculusTouchLeft == 256)
                            {
                                if (hasClickedBUTTONX == 0)
                                {
                                    if (_frameCounterTouchLeft <= 20 && _canResetCounterTouchLeftButtonX == true)
                                    {
                                        mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXLeft, _lastMousePosYLeft, 0, 0);
                                        _frameCounterTouchLeft = 0;
                                    }

                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

                                    _lastMousePosXLeft = absoluteMoveX;
                                    _lastMousePosYLeft = absoluteMoveY;
                                    _canResetCounterTouchLeftButtonX = true;
                                    hasClickedBUTTONX = 1;
                                }
                            }
                            else if (buttonPressedOculusTouchLeft == 512)
                            {
                                if (hasClickedBUTTONY == 0)
                                {
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    //_lastMousePosX = absoluteMoveX;
                                    //_lastMousePosY = absoluteMoveY;
                                    //_canResetCounterTouchRight = true;
                                    hasClickedBUTTONY = 1;
                                }
                            }
                        }
                    }*/

                    /*//////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                    if (hasClickedBUTTONX == 1 && buttonPressedOculusTouchLeft == 0 || hasClickedBUTTONY == 1 && buttonPressedOculusTouchLeft == 0)
                    {
                        if (hasClickedBUTTONX == 1 && buttonPressedOculusTouchLeft == 0)
                        {
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            hasClickedBUTTONX = 0;
                        }
                        else if (hasClickedBUTTONY == 1 && buttonPressedOculusTouchLeft == 0)
                        {
                            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                            hasClickedBUTTONY = 0;
                        }
                    }

                    if (_canResetCounterTouchLeftButtonX)
                    {
                        _frameCounterTouchLeft++;
                    }

                    if (_frameCounterTouchLeft >= 30)
                    {
                        _frameCounterTouchLeft = 0;
                        _canResetCounterTouchLeftButtonX = false;
                    }*/



                    /////////////RIGHT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXRight >= 0 && percentXRight <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth && percentYRight >= 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                    {
                        var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                        var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                        /*var yo = _updateFunctionStopwatchRight.Elapsed.Milliseconds;

                        if (yo >= 10)
                        {
                            //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                            _updateFunctionStopwatchRight.Stop();
                            _updateFunctionBoolRight = true;
                        }*/

                        //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                        //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                        //then disregard the first click location.
                        if (buttonPressedOculusTouchRight != 0)
                        {
                            if (buttonPressedOculusTouchRight == 1)
                            {
                                if (hasClickedBUTTONA == 0)
                                {
                                    /*if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                                    {
                                        mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                        _frameCounterTouchRight = 0;
                                    }*/

                                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                    //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, absoluteMoveX, absoluteMoveY, 0, 0);

                                    SetCursorPos((int)absoluteMoveX, (int)absoluteMoveY);


                                    Program.mousesim.LeftButtonDown();
                                    Program.mousesim.LeftButtonUp();

                                    _lastMousePosXRight = absoluteMoveX;
                                    _lastMousePosYRight = absoluteMoveY;
                                    _canResetCounterTouchRightButtonA = true;
                                    hasClickedBUTTONA = 1;
                                }
                            }
                            else if (buttonPressedOculusTouchRight == 2)
                            {
                                if (hasClickedBUTTONB == false)
                                {
                                    //mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    Program.mousesim.RightButtonDown();
                                    hasClickedBUTTONB = true;
                                }
                            }
                        }
                    }
                    else //if ()
                    {
                        //Program.MessageBox((IntPtr)0, "out of bounds 1", "mouse move", 0);
                        /*if (percentXRight < 0 && percentXRight <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            percentXRight >= 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            percentXRight < 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            percentYRight < 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            percentYRight >= 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            percentYRight < 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                        {
                           Program.MessageBox((IntPtr)0, "out of bounds 0" , "mouse move", 0);
                        }
                        else if (realMousePosX < 0 && realMousePosX <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            realMousePosX >= 0 && realMousePosX > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            realMousePosX < 0 && realMousePosX > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            realMousePosY < 0 && realMousePosY <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            realMousePosY >= 0 && realMousePosY > sccs.scgraphics.scdirectx.D3D.SurfaceHeight||
                            realMousePosY < 0 && realMousePosY > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                        {
                            Program.MessageBox((IntPtr)0, "out of bounds 1", "mouse move", 0);
                        }*/

                        if (percentXRight < 0 && percentXRight <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            percentXRight >= 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            percentXRight < 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            percentYRight < 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            percentYRight >= 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            percentYRight < 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                        {
                            //Program.MessageBox((IntPtr)0, "out of bounds 0" , "mouse move", 0);

                            if (percentXRight < 0 && percentXRight <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                                percentXRight >= 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                                percentXRight < 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                            {
                                if (percentXRight < 0 && percentXRight <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                                {
                                    percentXRight = 0;
                                }
                                else if (percentXRight >= 0 && percentXRight > sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                                {
                                    percentXRight = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                                }
                            }

                            if (percentYRight < 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            percentYRight >= 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            percentYRight < 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                            {
                                if (percentYRight < 0 && percentYRight <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                                {
                                    percentYRight = 0;
                                }
                                else if (percentYRight >= 0 && percentYRight > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                                {
                                    percentYRight = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                                }
                            }



                            /*var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                            var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                            if (realMousePosX >= 0 && realMousePosX < sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                            {

                            }
                            else
                            {
                                realMousePosX = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                                absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                            }

                            if (realMousePosY >= 0 && realMousePosY < sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                            {

                            }
                            else
                            {
                                realMousePosY = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                                absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);
                            }*/


                            if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                            {
                                SetCursorPos((int)realMousePosX, (int)realMousePosY);
                                //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                _frameCounterTouchRight = 0;
                            }

                        }
                        else if (realMousePosX < 0 && realMousePosX <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            realMousePosX >= 0 && realMousePosX > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            realMousePosX < 0 && realMousePosX > sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                            realMousePosY < 0 && realMousePosY <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            realMousePosY >= 0 && realMousePosY > sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                            realMousePosY < 0 && realMousePosY > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                        {
                            //Program.MessageBox((IntPtr)0, "out of bounds 1", "mouse move", 0);

                            if (realMousePosX < 0 && realMousePosX <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                                realMousePosX >= 0 && realMousePosX <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth ||
                                realMousePosX < 0 && realMousePosX > sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                            {
                                if (realMousePosX < 0 && realMousePosX <= sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                                {
                                    realMousePosX = 0;
                                }
                                else if (realMousePosX >= 0 && realMousePosX > sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                                {
                                    realMousePosX = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                                }
                            }

                            if (realMousePosY < 0 && realMousePosY <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                                realMousePosY >= 0 && realMousePosY > sccs.scgraphics.scdirectx.D3D.SurfaceHeight ||
                                realMousePosY < 0 && realMousePosY > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                            {
                                if (realMousePosY < 0 && realMousePosY <= sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                                {
                                    realMousePosY = 0;
                                }
                                else if (realMousePosY >= 0 && realMousePosY > sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                                {
                                    realMousePosY = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                                }
                            }



                            /*var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                            var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);

                            if (realMousePosX >= 0 && realMousePosX < sccs.scgraphics.scdirectx.D3D.SurfaceWidth)
                            {

                            }
                            else
                            {
                                realMousePosX = sccs.scgraphics.scdirectx.D3D.SurfaceWidth;
                                absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceWidth);
                            }

                            if (realMousePosY >= 0 && realMousePosY < sccs.scgraphics.scdirectx.D3D.SurfaceHeight)
                            {

                            }
                            else
                            {
                                realMousePosY = sccs.scgraphics.scdirectx.D3D.SurfaceHeight;
                                absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / sccs.scgraphics.scdirectx.D3D.SurfaceHeight);
                            }*/


                            if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                            {
                                SetCursorPos((int)realMousePosX, (int)realMousePosY);
                                //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                _frameCounterTouchRight = 0;
                            }

                        }
                    }


                    //////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                    if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0 || hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                    {
                        if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0)
                        {
                            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            hasClickedBUTTONA = 0;
                        }
                        else if (hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                        {
                            //mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                            Program.mousesim.RightButtonUp();
                            hasClickedBUTTONB = false;
                        }
                    }

                    if (_canResetCounterTouchRightButtonA)
                    {
                        _frameCounterTouchRight++;
                    }

                    if (_frameCounterTouchRight >= 30)
                    {
                        _frameCounterTouchRight = 0;
                        _canResetCounterTouchRightButtonA = false;
                    }


                    /*//////////OCULUS TOUCH BUTTONS NOT PRESSED////////////////////////////////////////
                    long test = 80;
                    /////////RIGHT THUMBSTICK///////////
                    var yo6 = _updateFunctionStopwatchRightThumbstickGoLeft.Elapsed.Milliseconds;
                    if (yo6 >= 75)
                    {
                        if (thumbStickRight[1].Y <= -0.1f && hasUsedThumbStickRightE == false)
                        {
                            //Console.WriteLine("test");
                            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -test, 0);
                            hasUsedThumbStickRightE = true;
                        }
                        else if (hasUsedThumbStickRightE)
                        {
                            hasUsedThumbStickRightE = false;
                        }
                        _updateFunctionStopwatchRightThumbstickGoLeft.Stop();
                        _updateFunctionBoolRightThumbStickGoLeft = true;
                    }
                    ///////////////////////////////////////////////////////////////////////////

                    /////////RIGHT THUMBSTICK/////////////////////////////////////////////////////
                    var yo7 = _updateFunctionStopwatchRightThumbstickGoRight.Elapsed.Milliseconds;
                    if (yo7 >= 75)
                    {
                        if (thumbStickRight[1].Y >= 0.1f && hasUsedThumbStickRightQ == false)
                        {
                            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, test, 0);
                            //hasUsedThumbStickRightQ = true;
                        }
                        else if (hasUsedThumbStickRightQ)
                        {
                            hasUsedThumbStickRightQ = false;
                        }
                        _updateFunctionStopwatchRightThumbstickGoRight.Stop();
                        _updateFunctionBoolRightThumbStickGoRight = true;
                    }*/
                    /////////////RIGHT OCULUS TOUCH////////////////////////////////////////////
                }
            }
            catch (Exception ex)
            {
                //MessageBox((IntPtr)0, ex.ToString(), "mouse move", 0);
            }











            hasClickedBUTTONACounter++;
        }

    }
}