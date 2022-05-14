using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;
using Jitter;

using Ab3d.OculusWrap;

using SC_console_directx = sccs.scgraphics.scdirectx;
using scupdate = sccs.scgraphics.scupdate;
using Jitter.LinearMath;

using sccs.scgraphics;

namespace sccs
{
    public class sccsikvoxellimbs : IDisposable
    {

        ~sccsikvoxellimbs()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // so that Dispose(false) isn't called later
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose all owned managed objects

                if (voxel_cuber_r_hand_grab != null)
                {
                    voxel_cuber_r_hand_grab.ShutDown();
                    voxel_cuber_r_hand_grab = null;
                }
                if (voxel_cuber_r_hnd != null)
                {
                    voxel_cuber_r_hnd.ShutDown();
                    voxel_cuber_r_hnd = null;
                }
                if (voxel_cuber_r_up_arm != null)
                {
                    voxel_cuber_r_up_arm.ShutDown();
                    voxel_cuber_r_up_arm = null;
                }
                if (voxel_cuber_r_low_arm != null)
                {
                    voxel_cuber_r_low_arm.ShutDown();
                    voxel_cuber_r_low_arm = null;
                }
                if (voxel_cuber_r_shld != null)
                {
                    voxel_cuber_r_shld.ShutDown();
                    voxel_cuber_r_shld = null;
                }
                if (voxel_cuber_r_targ != null)
                {
                    voxel_cuber_r_targ.ShutDown();
                    voxel_cuber_r_targ = null;
                }
                if (voxel_cuber_r_targ_two != null)
                {
                    voxel_cuber_r_targ_two.ShutDown();
                    voxel_cuber_r_targ_two = null;
                }


                if (_player_r_hand_grab != null)
                {


                    for (int i = 0; i < _player_r_hand_grab.Length; i++)
                    {
                        if (_player_r_hand_grab[i] != null)
                        {
                            for (int j = 0; j < _player_r_hand_grab[i].Length; j++)
                            {
                                if (_player_r_hand_grab[i][j] != null)
                                {
                                    _player_r_hand_grab[i][j].ShutDown();
                                    _player_r_hand_grab[i][j] = null;
                                }
                            }
                        }
                    }
                }

                if (_player_rght_hnd != null)
                {

                    for (int i = 0; i < _player_rght_hnd.Length; i++)
                    {
                        if (_player_rght_hnd[i] != null)
                        {
                            for (int j = 0; j < _player_rght_hnd[i].Length; j++)
                            {
                                if (_player_rght_hnd[i][j] != null)
                                {
                                    _player_rght_hnd[i][j].ShutDown();
                                    _player_rght_hnd[i][j] = null;
                                }
                            }
                        }
                    }
                }



                if (_player_rght_shldr != null)
                {
                    for (int i = 0; i < _player_rght_shldr.Length; i++)
                    {
                        if (_player_rght_shldr[i] != null)
                        {
                            for (int j = 0; j < _player_rght_shldr[i].Length; j++)
                            {
                                if (_player_rght_shldr[i][j] != null)
                                {
                                    _player_rght_shldr[i][j].ShutDown();
                                    _player_rght_shldr[i][j] = null;
                                }
                            }
                        }
                    }
                }


                if (_player_rght_elbow_target != null)
                {
                    for (int i = 0; i < _player_rght_elbow_target.Length; i++)
                    {
                        if (_player_rght_elbow_target[i] != null)
                        {
                            for (int j = 0; j < _player_rght_elbow_target[i].Length; j++)
                            {
                                if (_player_rght_elbow_target[i][j] != null)
                                {
                                    _player_rght_elbow_target[i][j].ShutDown();
                                    _player_rght_elbow_target[i][j] = null;
                                }
                            }
                        }
                    }
                }

                if (_player_rght_lower_arm != null)
                {
                    for (int i = 0; i < _player_rght_lower_arm.Length; i++)
                    {
                        if (_player_rght_lower_arm[i] != null)
                        {
                            for (int j = 0; j < _player_rght_lower_arm[i].Length; j++)
                            {
                                if (_player_rght_lower_arm[i][j] != null)
                                {
                                    _player_rght_lower_arm[i][j].ShutDown();
                                    _player_rght_lower_arm[i][j] = null;
                                }
                            }
                        }
                    }
                }


                if (_player_rght_upper_arm != null)
                {
                    for (int i = 0; i < _player_rght_upper_arm.Length; i++)
                    {
                        if (_player_rght_upper_arm[i] != null)
                        {
                            for (int j = 0; j < _player_rght_upper_arm[i].Length; j++)
                            {
                                if (_player_rght_upper_arm[i][j] != null)
                                {
                                    _player_rght_upper_arm[i][j].ShutDown();
                                    _player_rght_upper_arm[i][j] = null;
                                }
                            }
                        }
                    }
                }


                if (_player_rght_elbow_target_two != null)
                {
                    for (int i = 0; i < _player_rght_elbow_target_two.Length; i++)
                    {
                        if (_player_rght_elbow_target_two[i] != null)
                        {
                            for (int j = 0; j < _player_rght_elbow_target_two[i].Length; j++)
                            {
                                if (_player_rght_elbow_target_two[i][j] != null)
                                {
                                    _player_rght_elbow_target_two[i][j].ShutDown();
                                    _player_rght_elbow_target_two[i][j] = null;
                                }
                            }
                        }
                    }
                }



                if (_player_head != null)
                {
                    for (int i = 0; i < _player_head.Length; i++)
                    {
                        if (_player_head[i] != null)
                        {
                            for (int j = 0; j < _player_head[i].Length; j++)
                            {
                                if (_player_head[i][j] != null)
                                {
                                    _player_head[i][j].ShutDown();
                                    _player_head[i][j] = null;
                                }
                            }
                        }
                    }
                }

                if (_player_pelvis != null)
                {
                    for (int i = 0; i < _player_pelvis.Length; i++)
                    {
                        if (_player_pelvis[i] != null)
                        {
                            for (int j = 0; j < _player_pelvis[i].Length; j++)
                            {
                                if (_player_pelvis[i][j] != null)
                                {
                                    _player_pelvis[i][j].ShutDown();
                                    _player_pelvis[i][j] = null;
                                }
                            }
                        }
                    }
                }


                if (_player_torso != null)
                {
                    for (int i = 0; i < _player_torso.Length; i++)
                    {
                        if (_player_torso[i] != null)
                        {
                            for (int j = 0; j < _player_torso[i].Length; j++)
                            {
                                if (_player_torso[i][j] != null)
                                {
                                    _player_torso[i][j].ShutDown();
                                    _player_torso[i][j] = null;
                                }
                            }
                        }
                    }
                }



                if (voxel_cuber_head != null)
                {
                    voxel_cuber_head.ShutDown();
                    voxel_cuber_head = null;
                }



                if (voxel_cuber_pelvis != null)
                {
                    voxel_cuber_pelvis.ShutDown();
                    voxel_cuber_pelvis = null;
                }



                if (voxel_cuber_torso != null)
                {
                    voxel_cuber_torso.ShutDown();
                    voxel_cuber_torso = null;
                }


                if (parentobject != null)
                {
                    parentobject.Dispose();
                    parentobject = null;
                }


            }

            // Release unmanaged resources
        }
        Vector3 somenewtargetlocation = Vector3.Zero;
        Quaternion _testQuater;
        Matrix matrixerer;

        float diffNormPosX;
        float diffNormPosY;
        float diffNormPosZ;
        Vector3 direction_head_forward;
        Vector3 direction_head_right;
        Vector3 direction_head_up;
        Vector3 torsooripos;
        Vector3 tempPoint;
        Vector3 dirToPoint;
        Vector3 realPosOfRS;
        Vector3 pivotOfHead;
        Matrix shoulderMatrix;
        Matrix theheadrotmatrix;

        Vector3 direction_feet_forward_ori;
        Vector3 direction_feet_right_ori;
        Vector3 direction_feet_up_ori;






        public sccsikvoxellimbs parentobject;

        sc_voxel voxel_cuber_r_hand_grab;
        sc_voxel voxel_cuber_r_hnd;
        sc_voxel voxel_cuber_r_up_arm;
        sc_voxel voxel_cuber_r_low_arm;
        sc_voxel voxel_cuber_r_shld;
        sc_voxel voxel_cuber_r_targ;
        sc_voxel voxel_cuber_r_targ_two;
        Matrix[] voxel_sometester_r_hand_grab;
        Matrix[] voxel_sometester_r_hnd;
        Matrix[] voxel_sometester_r_up_arm;
        Matrix[] voxel_sometester_r_low_arm;
        Matrix[] voxel_sometester_r_shld;
        Matrix[] voxel_sometester_r_targ;
        Matrix[] voxel_sometester_r_targ_two;


        sc_voxel.DLightBuffer[] _SC_modL_r_hand_grab_BUFFER = new sc_voxel.DLightBuffer[1];

        sc_voxel.DLightBuffer[] _SC_modL_rght_hnd_BUFFER = new sc_voxel.DLightBuffer[1];


        sc_voxel.DLightBuffer[] _SC_modL_rght_shldr_BUFFER = new sc_voxel.DLightBuffer[1];

        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_lower_arm_BUFFER = new sc_voxel.DLightBuffer[1];


        sc_voxel.DLightBuffer[] _SC_modL_rght_upper_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_two_BUFFER = new sc_voxel.DLightBuffer[1];


        public Matrix[][][] worldMatrix_instances_r_hand_grab;
        public Matrix[][][] worldMatrix_instances_r_elbow_target;
        public Matrix[][][] worldMatrix_instances_r_elbow_target_two;
        public Matrix[][][] worldMatrix_instances_r_elbow_target_three;



        public Matrix[][][] worldMatrix_instances_r_hand;
        public Matrix[][][] worldMatrix_instances_r_shoulder;

        public Matrix[][][] worldMatrix_instances_r_upperarm;
        public Matrix[][][] worldMatrix_instances_r_lowerarm;
        public sc_voxel[][] _player_r_hand_grab;

        public sc_voxel[][] _player_rght_hnd;

        public sc_voxel[][] _player_rght_shldr;
        public sc_voxel[][] _player_rght_elbow_target;
        public sc_voxel[][] _player_rght_lower_arm;
        public sc_voxel[][] _player_rght_upper_arm;
        public sc_voxel[][] _player_rght_elbow_target_two;
        public sc_voxel[][] _player_rght_elbow_target_three;
        //public Matrix worldmatofobj;

        public sccsikvoxellimbs()
        {



        }

        object _some_data0;
        World _thejitter_world;

        //lightpos = new Vector3(0, 100, 0);
        Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        Vector3 lightDirection = new Vector3(0, -1, -1);
        Vector3 dirLight = Vector3.Zero;
        Vector3 lightpos = Vector3.Zero;


        public Vector3 initchunkposition;



        Matrix[] voxel_sometester_head;
        sc_voxel voxel_cuber_head;
        sc_voxel voxel_cuber_pelvis;
        sc_voxel voxel_cuber_torso;
        Matrix[] voxel_sometester_pelvis;
        Matrix[] voxel_sometester_torso;
        sc_voxel.DLightBuffer[] _SC_modL_pelvis_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_head_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_torso_BUFFER = new sc_voxel.DLightBuffer[1];
        public Matrix[][][] worldMatrix_instances_head;
        public Matrix[][][] worldMatrix_instances_torso;
        public Matrix[][][] worldMatrix_instances_pelvis;


        public sc_voxel[][] _player_head;
        public sc_voxel[][] _player_pelvis;
        public sc_voxel[][] _player_torso;


        int _human_inst_rig_x = -1;
        int _human_inst_rig_y = -1;
        int _human_inst_rig_z = -1;

        int grabtargetitem = 0;

        public scmessageobjectjitter[][] createikbody(scmessageobjectjitter[][] _sc_jitter_tasks, int tempMultiInstancePhysicsTotal, Vector3 ikarmpivotinitposition, sccsikvoxellimbs parentobject_, Matrix worldmatofobj_, int human_inst_rig_x, int human_inst_rig_y, int human_inst_rig_z, int grabtargetitem_)
        {
            grabtargetitem = grabtargetitem_;

            _human_inst_rig_x = human_inst_rig_x;
            _human_inst_rig_y = human_inst_rig_y;
            _human_inst_rig_z = human_inst_rig_z;



            //worldmatofobj = worldmatofobj_;

            if (parentobject_ != null)
            {
                parentobject = parentobject_;
            }

            float voxel_general_size = voxeliknewsize;

            initchunkposition = ikarmpivotinitposition;

            if (Program.usejitterphysics == 1)
            {
                //SETTING UP SINGLE WORLD OBJECTS
                //END OF LOOP FOR PHYSICS ENGINE INSTANCES
                _some_data0 = (object)_sc_jitter_tasks[0][0]._world_data[0];
                //World[] _jitter_worlds0 = (World[])_some_data0;
                _thejitter_world = (World)_some_data0;
            }



            int _inst_p_torso_x = _human_inst_rig_x;
            int _inst_p_torso_y = _human_inst_rig_y;
            int _inst_p_torso_z = _human_inst_rig_z;
            int _inst_p_pelvis_x = _human_inst_rig_x;
            int _inst_p_pelvis_y = _human_inst_rig_y;
            int _inst_p_pelvis_z = _human_inst_rig_z;

            Matrix WorldMatrix = Matrix.Identity;
            float _dist_between = 0.25f;
            int tempIndex = 0;
            bool is_static = true;
            int _addToWorld = 0;
            int voxel_type = 2;

            _player_torso = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_pelvis = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_head = new sc_voxel[tempMultiInstancePhysicsTotal][];

            worldMatrix_instances_head = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_torso = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_pelvis = new Matrix[tempMultiInstancePhysicsTotal][][];

            _player_pelvis[0] = new sc_voxel[1];
            worldMatrix_instances_pelvis[0] = new Matrix[1][];

            _player_torso[0] = new sc_voxel[1];
            worldMatrix_instances_torso[0] = new Matrix[1][];

            _player_head[0] = new sc_voxel[1];
            worldMatrix_instances_head[0] = new Matrix[1][];





            int minx = 1;
            int miny = 1;
            int minz = 1;

            int diagmaxx = 1;
            int diagmaxy = 1;
            int diagmaxz = 1;

            int diagminx = 1;
            int diagminy = 1;
            int diagminz = 1;

            int chunkwidthl = 1;
            int chunkwidthr = 1;

            int chunkheightl = 1;
            int chunkheightr = 1;

            int chunkdepthl = 1;
            int chunkdepthr = 1;

            float distance = 1.0f;

            float sizelowerax = 0.0f;
            float sizeloweray = 0.0f;
            float sizeloweraz = 0.0f;


            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;
            //PELVIS
            float r = 0.19f;
            float g = 0.19f;
            float b = 0.19f;
            float a = 1;
            Matrix initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;
            initialmatrix.M41 = 0;
            initialmatrix.M42 = -0.38f + initchunkposition.Y; //-0.485f
            initialmatrix.M43 = 0;
            initialmatrix.M44 = 1;
            float offsetPosX = _dist_between * 2;
            float offsetPosY = _dist_between * 2;
            float offsetPosZ = _dist_between * 2;
            float _mass = 100;
            //_player_pelvis[0] = new sc_voxel();
            //_hasinit0 = _player_pelvis.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_pelvis[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerPelvis, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //float voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;



            minx = 9;
            miny = 9;
            minz = 9;

            diagmaxx = 9;
            diagmaxy = 9;
            diagmaxz = 9;

            diagminx = 9;
            diagminy = 9;
            diagminz = 9;

            chunkwidthl = 40;
            chunkwidthr = 39;

            chunkheightl = 12; //15
            chunkheightr = 11; //14

            chunkdepthl = 20;
            chunkdepthr = 19;
            distance = 15; // 10

            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;



            int _type_of_cube = 2;
            _player_pelvis[0][0] = new sc_voxel();
            //_player_pelvis[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerPelvis, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_pelvis[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz,
                new Vector4(r, g, b, a), _inst_p_pelvis_x, _inst_p_pelvis_y, _inst_p_pelvis_z,
                Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerPelvis,
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
                voxel_general_size, new Vector3(0, 0, 0), distance,
                vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_pelvis[0][0] = new Matrix[_inst_p_pelvis_x * _inst_p_pelvis_y * _inst_p_pelvis_z];
            for (int i = 0; i < worldMatrix_instances_pelvis[0][0].Length; i++)
            {
                worldMatrix_instances_pelvis[0][0][i] = _player_pelvis[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }




         








            //tempIndex = 0;

            //vertoffsetx = 0;
            //vertoffsety = 0;
            //vertoffsetz = -(16 + 15) * 0.015f;// - 0.25f;;

            ///_dist_between = 0.30f;

            ///////////////////////////////
            ///////////HUMAN RIG///////////
            ///////////////////////////////

            ////////////////////////////////////////////////
            //////////CONTAINMENT GRIDS RIGHT HAND//////////
            ////////////////////////////////////////////////

            /*
            _cubeGridFaceTop = new DObjectGrid();
            _cubeGridFaceTop.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, true, false, false, false, false);

            _cubeGridFaceBottom = new DObjectGrid();
            _cubeGridFaceBottom.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, true, false, false, false, false, false);


            _cubeGridFaceLeft = new DObjectGrid();
            _cubeGridFaceLeft.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, false, true, false, false, false);

            _cubeGridFaceRight = new DObjectGrid();
            _cubeGridFaceRight.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, false, false, true, false, false);

            _cubeGridFaceFront = new DObjectGrid();
            _cubeGridFaceFront.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, false, false, false, true, false);

            _cubeGridFaceBack = new DObjectGrid();
            _cubeGridFaceBack.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, false, false, false, false, true);*/

            //TORSO
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;
            initialmatrix.M41 = 0;
            initialmatrix.M42 = _player_pelvis[0][0]._arrayOfInstances[0].current_pos.M42 + (_player_pelvis[0][0]._total_torso_height * 3); // -0.1f
            initialmatrix.M43 = 0;
            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_torso[0] = new sc_voxel();
            //_hasinit0 = _player_torso.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f                                                                                                                                                                                                                                                                                        //_hasinit0 = _player_torso.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_player_torso[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerTorso, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;



            minx = 9;
            miny = 9;
            minz = 9;

            diagmaxx = 9;
            diagmaxy = 9;
            diagmaxz = 9;

            diagminx = 9;
            diagminy = 9;
            diagminz = 9;

            chunkwidthl = 40;
            chunkwidthr = 39;

            chunkheightl = 56;
            chunkheightr = 55;

            chunkdepthl = 12; //16
            chunkdepthr = 11; //15
            distance = 30;

            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;



            _type_of_cube = 2;
            _mass = 100;
            _player_torso[0][0] = new sc_voxel();
            _player_torso[0][0].Initialize(
                SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a),
                _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z,
                Program.consoleHandle, initialmatrix, _type_of_cube,
                offsetPosX, offsetPosY, offsetPosZ,
                _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerTorso,
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
                voxel_general_size,
                new Vector3(0, 0, 0), distance,
                vertoffsetx, vertoffsety, vertoffsetz,
                _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f



            //_player_torso[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerTorso, 2, 9, 2, 2, 2, 2, 45, 44, 60, 59, 10, 9, 0.0025f, new Vector3(0, 0, 0), 500); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_torso[0][0] = new Matrix[_inst_p_torso_x * _inst_p_torso_y * _inst_p_torso_z];
            for (int i = 0; i < worldMatrix_instances_torso[0][0].Length; i++)
            {
                worldMatrix_instances_torso[0][0][i] = _player_torso[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }


























            //THE HEAD IS AT ZERO
            /////////////////
            ///////HEAD//////
            /////////////////
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;
            initialmatrix.M41 = 0;
            //initialmatrix.M42 = _player_pelvis[0][0]._arrayOfInstances[0].current_pos.M42 + (_player_pelvis[0][0]._total_torso_height * 3); // 0.45f + initchunkposition.Y; // -0.1f
            initialmatrix.M42 = _player_torso[0][0]._arrayOfInstances[0].current_pos.M42 + (_player_torso[0][0]._total_torso_height * 3);
            initialmatrix.M43 = 0;
            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;



            minx = 10;
            miny = 10;
            minz = 10;

            diagmaxx = 10;
            diagmaxy = 10;
            diagmaxz = 10;

            diagminx = 10;
            diagminy = 10;
            diagminz = 10;

            chunkwidthl = 30;
            chunkwidthr = 29;

            chunkheightl = 30;
            chunkheightr = 29;

            chunkdepthl = 30;
            chunkdepthr = 29;
            distance = 30;

            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;

            _player_head[0][0] = new sc_voxel();
            _player_head[0][0].Initialize(
                SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz,
                new Vector4(r, g, b, a),
                _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z,
                Program.consoleHandle,
                initialmatrix,
                _type_of_cube,
                offsetPosX, offsetPosY, offsetPosZ,
                _thejitter_world,
                _mass,
                is_static,
                sccs.scgraphics.scdirectx.BodyTag.PlayerHead,
                //9, 9, 9, 9, 9, 15, 35, 20, 20, 40, 23, 22, BACKUP
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
                voxel_general_size,
                new Vector3(0, 0, 0),
                30,
                vertoffsetx, vertoffsety, vertoffsetz,
                _addToWorld,
                voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

            worldMatrix_instances_head[0][0] = new Matrix[_inst_p_torso_x * _inst_p_torso_y * _inst_p_torso_z];
            for (int i = 0; i < worldMatrix_instances_head[0][0].Length; i++)
            {
                worldMatrix_instances_head[0][0][i] = _player_head[0][0]._arrayOfInstances[i].current_pos;//initialmatrix;// Matrix.Identity;
            }
            /////////////////
            ///////HEAD//////
            /////////////////

            //chest looking like voxel settings 11, 12, 11, 12, 11, 14, 9, 9, 9, 35, 33, 35, 33, 35, 33,
            //same top japanese looking like voxel settings 11, 12, 11, 12, 11, 14, 9, 9, 9, 35, 33, 35, 33, 35, 33,
            //Elite Dangerous stations voxel type settings 9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 20, 19, 
            //9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 35, 34, 
            //RIDDICK 1 kinda monster head . id have to recheck the movie. 7, 9, 7, 30, 9, 40, 30, 9, 40, 30, 30, 30, 30, 30, 30,





            if (Program.usejitterphysics == 1)
            {


                for (int phys = 0; phys < Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez; phys++)
                {
                    for (int i = 0; i < Program.worldwidth * Program.worldheight * Program.worlddepth; i++)
                    {
                        object _some_dator = (object)_sc_jitter_tasks[phys][i]._world_data[0];
                        World _the_current_world = (World)_some_dator;

                        //_the_current_world.AddBody(_player_rght_upper_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        //_the_current_world.AddBody(_player_rght_lower_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_pelvis[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_torso[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        //_the_current_world.AddBody(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        //_the_current_world.AddBody(_player_rght_shldr[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                    }
                }
            }






            _SC_modL_head_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_torso_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_pelvis_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };










            return _sc_jitter_tasks;
        }


        public scmessageobjectjitter[][] setikbodytargetnlimbspositionsNrotations(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix, sc_voxel mainarmparentmeshobject, Vector3 lightpos_, Vector3 dirLight_, Matrix finalRotationMatrix_, sc_voxel mainarmparentmeshobjectmain, Matrix hmd_matrix_current, Matrix extramatrix, Matrix hmdmatrixcurrentforpelvis)
        {






            ////////////////////
            /////HUMAN RIG////// 
            ////////////////////
            for (int _iterator = 0; _iterator < 1; _iterator++) // _player_torso[0][0]._arrayOfInstances.Length
            {

                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                Matrix sometempmat2 = _player_head[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion somedirquat2;
                Quaternion.RotationMatrix(ref sometempmat2, out somedirquat2);
                var dirikvoxelbodyInstanceRight2 = -sc_maths._newgetdirleft(somedirquat2);
                var dirikvoxelbodyInstanceUp2 = sc_maths._newgetdirup(somedirquat2);
                var dirikvoxelbodyInstanceForward2 = sc_maths._newgetdirforward(somedirquat2);


                Vector3 tempOffset = OFFSETPOS;

                //int usethirdpersonview = 1;

                if (Program.usethirdpersonview == 0)
                {
                    tempOffset.X = scupdate.viewpositionorigin.X;
                    //tempOffset.Y = _player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                    tempOffset.Y = scupdate.viewpositionorigin.Y;
                    tempOffset.Z = scupdate.viewpositionorigin.Z;

                    //tempOffset.Y = tempOffset.Y - (dirikvoxelbodyInstanceUp2.Y * _player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42 * 0.75f);



                    //(_player_torso[0][0].ChunkHeight_L + _player_torso[0][0].ChunkHeight_R + 1) +
                    //tempOffset.Y = tempOffset.Y + (_player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42 * 0.75f);
                    //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (_player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42 * 0.75f));
                    //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (((_player_head[0][0].ChunkHeight_L + _player_head[0][0].ChunkHeight_R + 1)) * _player_head[0][0]._sizeY * (_player_head[0][0].planesize) * 4 * 10));


                    //tempOffset.Y = tempOffset.Y + ikvoxelbody._player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                    //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (ikvoxelbody._player_head[0][0]._total_torso_height * 0.5f));

                    //tempOffset.Y = tempOffset.Y + _player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                    //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (_player_head[0][0]._total_torso_height * 0.5f*10));


                    //tempOffset.Y = tempOffset.Y + _player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                    //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (_player_head[0][0]._total_torso_height * 0.5f * (_player_head[0][0].ChunkHeight_L + _player_head[0][0].ChunkHeight_R + 1) * 4 * (voxel_general_size * 10 * 4)));
                    //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (((_player_head[0][0].ChunkWidth_L + _player_head[0][0].ChunkWidth_R + 1 + _player_head[0][0].ChunkWidth_R + 1)) * _player_head[0][0]._sizeX * (_player_head[0][0].planesize)* 10 ));



                    //_total_torso_width = (((ChunkWidth_L + ChunkWidth_R + 1) * _sizeX * planesize * 4));
                }
                else if (Program.usethirdpersonview == 1)
                {

                    //OFFSETPOS.X = scupdate.viewPosition.X;
                    //OFFSETPOS.Y = scupdate.viewPosition.Y;
                    //OFFSETPOS.Z = scupdate.viewPosition.Z;

                    //OFFSETPOS = OFFSETPOS + (dirikvoxelbodyInstanceUp0 * -0.125f);
                    //scupdate.viewPosition = scupdate.viewPosition + (dirikvoxelbodyInstanceRight0 * -1.5f);





                    /*//tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    Quaternion quatt;
                    Quaternion.RotationMatrix(ref scupdate.tempmatter, out quatt);
                    // quatt.Invert();

                    //THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW
                    Vector3 forwardOVR = sc_maths._getDirection(Vector3.ForwardRH, quatt);
                    Vector3 upOVR = sc_maths._getDirection(Vector3.Up, quatt);
                    Vector3 rightOVR = sc_maths._getDirection(Vector3.Right, quatt);
                    upOVR.Normalize();
                    rightOVR.Normalize();
                    forwardOVR.Normalize();

                    forwardOVR *= -0.5f; // -1.0f

                    Vector3 thirdpersonview = OFFSETPOS + (-forwardOVR * 2.0f); //1.5f // + (upOVR * 0.25f)

                    OFFSETPOS.X = thirdpersonview.X;// scupdate.viewPosition.X;
                    OFFSETPOS.Y = thirdpersonview.Y;// scupdate.viewPosition.Y;
                    OFFSETPOS.Z = thirdpersonview.Z;// scupdate.viewPosition.Z;*/
                }



                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW












                _SC_modL_pelvis_BUFFER[0].lightPosition = lightpos;
                _SC_modL_pelvis_BUFFER[0].lightDirection = dirLight;

                _SC_modL_torso_BUFFER[0].lightPosition = lightpos;
                _SC_modL_torso_BUFFER[0].lightDirection = dirLight;

                _SC_modL_head_BUFFER[0].lightPosition = lightpos;
                _SC_modL_head_BUFFER[0].lightDirection = dirLight;



                Matrix finalRotationMatrix = finalRotationMatrix_;// originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;

                //finalRotationMatrix = hmd_matrix_current * finalRotationMatrix;




                var connectorOfUpperArmRightOffsetMul = 1.0f; //1.55f
                var connectorOfLowerArmRightOffsetMul = 1.0f; //0.70f
                var connectorOfHandOffsetMul = 1.00123f; // 1.00123f

                var connectorOfUpperLegOffsetMul = 1.0f;
                var connectorOfLowerLegOffsetMul = 1.0f;

                //THE CURRENT PIVOT POINT OF THE TORSO IS RIGHT IN THE MIDDLE OF THE TORSO ITSELF
                Vector3 MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                Matrix somerotationmatrix = _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION;// _player_torso[0][0]._ORIGINPOSITION;
                Quaternion somequatrot;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);

                //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                var direction_feet_forward_ori_torso = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                var direction_feet_right_ori_torso = sc_maths._getDirection(Vector3.Right, somequatrot);
                var direction_feet_up_ori_torso = sc_maths._getDirection(Vector3.Up, somequatrot);

                //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_player_torso[0][0]._total_torso_height * 0.5f));

                somerotationmatrix = _player_torso[0][0]._arrayOfInstances[_iterator].current_pos;// _player_torso[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                var direction_upper_torso = sc_maths._getDirection(Vector3.Up, somequatrot);

                //Vector3 NECKPIVOTORIGINAL = MOVINGPOINTER + (direction_upper_torso * (_player_torso[0][0]._total_torso_height * 0.5f));
                //NECKPIVOTORIGINAL = NECKPIVOTORIGINAL + (direction_upper_torso * (_player_head[0][0]._total_torso_height * 0.5f));
                //Vector3 NECKPIVOTORIGINALWITHROTATIONOFFSET = NECKPIVOTORIGINAL;

                //I AM USING THE SAME POINT THAT WAS DECLARED EARLIER TO SHRINK THE NUMBER OF VARIABLES CONTAINED IN THE SCRIPT, EVEN THOUGH THIS IS CURRENTLY ONLY A DRAFT PROJEKT.
                //I AM STARTING THE POSITION OF ALL OF THE FOLLOWING TRANSLATION TO BE ADDED TO THIS ONE. THIS IS SO MUCH EASIER TO COMPREHEND FOR ME THEN USING QUATERNIONS FOR OTHER THINGS.
                //I DO NOT HAVE THE ABILITY YET TO UNDERSTAND WHAT THE X AND Y AND Z AND W OF A QUATERNION IS UNLESS CONVERTED TO THE PITCH/YAW/ROLL OR CARTESIAN/POLAR COORDINATES.
                //MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                //I FOR SOME REASONS IS SAVING THE ORIGINAL TORSO POSITION INSIDE OF THAT MATRIX AGAIN. FOR SOME REASONS, I THOUGHT "REF" MEANT "FOR THE CURRENT VARIABLE TO ALSO BE MODIFIED AFTER THE FUNCTION HAS RUN".
                //DEFECT IN MY LEARNING PROCESS.
                //somerotationmatrix = _player_torso._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                //CALCULATED IT TWICE... NO NEED FOR THAT.

                //REMOVED THAT TOO... WTF AND WHY AM I REMOVING THE TOTAL HEIGHT OF THE TORSO INSTEAD OF JUST HALF IS.ive got no clue. removing it.
                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_player_torso._total_torso_height * 0.5f));

                //GETTING THE CURRENT ROTATION MATRIX OF THE PIVOT BOTTOM OF SPINE AREA.
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);

                //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);





                //I AM CALCULATING THE DIFFERENCE IN THE MOVEMENT FROM THE ORIGINAL POSITION TO THE CURRENT OFFSET AT THE BOTTOM OF THE SPINE WHERE I MOVED THAT POINT.
                var diffNormPosX = (MOVINGPOINTER.X) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41; //_player_torso[0][0]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42; //_player_torso[0][0]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43; //_player_torso[0][0]._ORIGINPOSITION.M43;

                //var pelvispos = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                //pelvispos = pelvispos + (direction_feet_up_torso* _player_torso[0][0]._total_torso_height * 0.35f);
                //MOVINGPOINTER = pelvispos;


                //I AM USING THE NEW PIVOT POINT AT THE BOTTOM OF THE SPINE AND ADDING THE FRONT/RIGHT/UP VECTOR OF THE ROTATION OF THAT SPINE AND THEN ADDING THE DIFFERENCE X/W/Z BETWEEN ORIGINAL POS AND THE NEW PIVOT POS
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right_torso * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_torso * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward_torso * (diffNormPosZ));





                //HEAD STUFF
                //HEAD STUFF
                //HEAD STUFF
                /*Matrix tempTorsoMat = finalRotationMatrix;// _player_torso[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref tempTorsoMat, out somequatrot);
                var direction_up_torso = sc_maths._getDirection(Vector3.Up, somequatrot);
                var direction_right_torso = sc_maths._getDirection(Vector3.Right, somequatrot);
                var headPoint = MOVINGPOINTER + (-direction_feet_up_torso * (diffNormPosY));// direction_up_torso * (_player_torso[0][0]._total_torso_height * 0.5f);
                var oriHeadPivot = headPoint;
                oriHeadPivot += OFFSETPOS;

                headPoint = headPoint + (direction_feet_up_torso * (_player_head[0][0]._total_torso_height * 1));
                headPoint = headPoint + (direction_feet_up_torso * 0.01f);
                headPoint += OFFSETPOS;*/
                //HEAD STUFF
                //HEAD STUFF
                //HEAD STUFF


                //MOVINGPOINTER.X += OFFSETPOS.X;
                //MOVINGPOINTER.Y += OFFSETPOS.Y;
                //MOVINGPOINTER.Z += OFFSETPOS.Z;

                var matrixerer = finalRotationMatrix;


                //MOVINGPOINTER += tempOffset;

                if (Program.usethirdpersonview == 0)
                {
                    MOVINGPOINTER += OFFSETPOS;
                    //MOVINGPOINTER.Y = tempOffset.Y;

                }
                else if (Program.usethirdpersonview == 1)
                {
                    MOVINGPOINTER += tempOffset;
                }




                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;


                //Matrix _body_pos = matrixerer;
                //Quaternion _quat;
                //Quaternion.RotationMatrix(ref _body_pos, out _quat);
                //JQuaternion _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                //var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                worldMatrix_instances_torso[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_torso[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                _player_torso[0][0]._arrayOfInstances[_iterator].forwarddirection = direction_feet_forward_torso;
                _player_torso[0][0]._arrayOfInstances[_iterator].updirection = direction_feet_up_torso;
                _player_torso[0][0]._arrayOfInstances[_iterator].rightdirection = direction_feet_right_torso;

                ///////////
                //SOMETESTS
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                var direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                Vector3 current_rotation_of_torso_pivot_forward = direction_feet_forward;
                Vector3 current_rotation_of_torso_pivot_right = direction_feet_right;
                Vector3 current_rotation_of_torso_pivot_up = direction_feet_up;
                //SOMETESTS
                ///////////





                //HEAD
                var finalHMDMatrix = hmd_matrix_current * finalRotationMatrix;
                Quaternion.RotationMatrix(ref hmd_matrix_current, out otherQuat);
                var direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_head_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_head[0][0]._POSITION.M41, _player_head[0][0]._POSITION.M42, _player_head[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                var torsooripos = MOVINGPOINTER;
                somerotationmatrix = _player_head[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);
                diffNormPosX = (MOVINGPOINTER.X) - _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var tempPoint = MOVINGPOINTER;
                //tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));
                var dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfHead = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_head[0][0]._total_torso_height * 1))));
                var pivotOfHead = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                //realPosOfHead.X += OFFSETPOS.X;
                //realPosOfHead.Y += OFFSETPOS.Y;
                //realPosOfHead.Z += OFFSETPOS.Z;

                finalHMDMatrix = hmd_matrix_current * finalRotationMatrix;
                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;







                Vector3 somepivotheadoffset = pivotOfHead;// + OFFSETPOS;




                matrixerer.M41 = somepivotheadoffset.X;
                matrixerer.M42 = somepivotheadoffset.Y;
                matrixerer.M43 = somepivotheadoffset.Z;
                _player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT = matrixerer;


                //_player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT.M41 = realPosOfHead.X;
                //_player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT.M42 = realPosOfHead.Y;
                //_player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT.M43 = realPosOfHead.Z;


                realPosOfHead = realPosOfHead + (direction_head_up * (_player_head[0][0]._total_torso_depth * 1));
                realPosOfHead = realPosOfHead + (-current_rotation_of_torso_pivot_up * (_player_head[0][0]._total_torso_depth * 1));



                if (Program.usethirdpersonview == 0)
                {

                    //realPosOfHead = OFFSETPOS;
                    realPosOfHead = tempOffset;
                    //realPosOfHead.X += tempOffset.X;
                    //realPosOfHead.Y += tempOffset.Y;
                    //realPosOfHead.Z += tempOffset.Z;
                    realPosOfHead.Y = OFFSETPOS.Y;
                    realPosOfHead.X += OFFSETPOS.X;
                    realPosOfHead.Z += OFFSETPOS.Z;

                }
                else if (Program.usethirdpersonview == 1)
                {
                    realPosOfHead += tempOffset;
                }




                matrixerer.M41 = realPosOfHead.X;
                matrixerer.M42 = realPosOfHead.Y;
                matrixerer.M43 = realPosOfHead.Z;
                worldMatrix_instances_head[0][0][_iterator] = matrixerer;
                _player_head[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




























                /*_SC_modL_pelvis_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis[0][0]._POSITION.M41, _player_pelvis[0][0]._POSITION.M42, _player_pelvis[0][0]._POSITION.M43),
                    padding1 = 100
                };
                //var _cuber_pelvis = _player_pelvis[0][0];
                var _spine_upper_body_pos = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                /*var MOVINGPOINTERPELVIS = _spine_upper_body_pos;

                MOVINGPOINTERPELVIS.X += OFFSETPOS.X;
                MOVINGPOINTERPELVIS.Y += OFFSETPOS.Y;
                MOVINGPOINTERPELVIS.Z += OFFSETPOS.Z;
                
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                somerotationmatrix = _player_pelvis[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);
                diffNormPosX = (MOVINGPOINTER.X) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));



                //finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;

                //matrixerer = finalRotationMatrix;
                // originRot * rotatingMatrixForPelvis * hmdrotMatrix;              
                //matrixerer = finalRotationMatrix;


                dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_pelvis[0][0]._total_torso_height * 2))));
                var pivotOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                realPosOfpelvis.X += OFFSETPOS.X;
                realPosOfpelvis.Y += OFFSETPOS.Y;
                realPosOfpelvis.Z += OFFSETPOS.Z;

                finalHMDMatrix = rotatingMatrixForPelvis; //finalRotationMatrix;

                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfpelvis, pivotOfpelvis + direction_head_forward, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;

                matrixerer.M44 = 1;

                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_pelvis[0][0][_iterator] = matrixerer;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;*/





                /*
                finalHMDMatrix = finalRotationMatrix;
                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_pelvis_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis[0][0]._POSITION.M41, _player_pelvis[0][0]._POSITION.M42, _player_pelvis[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                somerotationmatrix = _player_pelvis[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);
                diffNormPosX = (MOVINGPOINTER.X) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));
                dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_pelvis[0][0]._total_torso_height * 2))));
                var pivotOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                realPosOfpelvis.X += OFFSETPOS.X;
                realPosOfpelvis.Y += OFFSETPOS.Y;
                realPosOfpelvis.Z += OFFSETPOS.Z;
                finalHMDMatrix = finalRotationMatrix;
                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfpelvis, pivotOfpelvis + direction_head_forward, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;

                realPosOfpelvis = realPosOfpelvis + (direction_head_up * (_player_pelvis[0][0]._total_torso_depth * 2));
                //realPosOfpelvis = realPosOfpelvis + (-current_rotation_of_torso_pivot_up * (_player_pelvis[0][0]._total_torso_depth * 2));

                pivotOfpelvis.X += OFFSETPOS.X;
                pivotOfpelvis.Y += OFFSETPOS.Y;
                pivotOfpelvis.Z += OFFSETPOS.Z;



                matrixerer.M41 = pivotOfpelvis.X;
                matrixerer.M42 = pivotOfpelvis.Y;
                matrixerer.M43 = pivotOfpelvis.Z;
                worldMatrix_instances_pelvis[0][0][_iterator] = matrixerer;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                */













                finalHMDMatrix = originRot * rotatingMatrixForPelvis * hmdrotMatrix;//finalRotationMatrix = 



                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_pelvis_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis[0][0]._POSITION.M41, _player_pelvis[0][0]._POSITION.M42, _player_pelvis[0][0]._POSITION.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                torsooripos = MOVINGPOINTER;
                somerotationmatrix = _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);

                var lengthoftorso = _player_torso[0][0]._total_torso_height;

                //diffNormPosX = (MOVINGPOINTER.X) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                //diffNormPosY = (MOVINGPOINTER.Y) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                //diffNormPosZ = (MOVINGPOINTER.Z) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;




                var pivotOfpelvis = MOVINGPOINTER;
                pivotOfpelvis = pivotOfpelvis + (-direction_feet_up * lengthoftorso * 0.5f * 1.25f);



                //
                //tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                //tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                //tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));


                //dirToPoint = pivotOfpelvis - torsooripos;
                //dirToPoint.Normalize();
                //var realPosOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_pelvis[0][0]._total_torso_height * 2))));

                //MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                //MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                //var pivotOfpelvis = tempPoint; // + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                //realPosOfpelvis.X += OFFSETPOS.X;
                //realPosOfpelvis.Y += OFFSETPOS.Y;
                //realPosOfpelvis.Z += OFFSETPOS.Z;

                //finalHMDMatrix = hmdmatrixcurrentforpelvis * finalRotationMatrix;// originRot * rotatingMatrixForPelvis * hmdrotMatrix; ;// finalRotationMatrix; //rotatingMatrix

                finalHMDMatrix = extramatrix * hmdrotMatrix;


                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfpelvis, pivotOfpelvis + direction_head_forward, direction_head_up);
                theheadrotmatrix.Invert();


                matrixerer = theheadrotmatrix;
                //matrixerer = hmdmatrixcurrentforpelvis * matrixerer;




                //realPosOfpelvis = realPosOfpelvis + (direction_head_up * (_player_pelvis[0][0]._total_torso_depth * 2));
                //realPosOfpelvis = realPosOfpelvis + (current_rotation_of_torso_pivot_up * (_player_pelvis[0][0]._total_torso_depth * 2));

                //pivotOfpelvis.X += OFFSETPOS.X;
                //pivotOfpelvis.Y += OFFSETPOS.Y;
                //pivotOfpelvis.Z += OFFSETPOS.Z;
                //pivotOfpelvis += tempOffset;




                if (Program.usethirdpersonview == 0)
                {
                    //pivotOfpelvis += OFFSETPOS;
                    //pivotOfpelvis.Y = tempOffset.Y;

                }
                else if (Program.usethirdpersonview == 1)
                {
                    pivotOfpelvis += tempOffset;
                }


                //matrixerer = finalHMDMatrix;
                matrixerer.M41 = pivotOfpelvis.X;
                matrixerer.M42 = pivotOfpelvis.Y;
                matrixerer.M43 = pivotOfpelvis.Z;
                worldMatrix_instances_pelvis[0][0][_iterator] = matrixerer;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                ///////////
                //SOMETESTS

                Quaternion.RotationMatrix(ref matrixerer, out otherQuat);
                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                //SOMETESTS
                ///////////
                _player_pelvis[0][0]._arrayOfInstances[_iterator].forwarddirection = direction_feet_forward;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].updirection = direction_feet_up;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].rightdirection = direction_feet_right;


            }
            return _sc_jitter_tasks;
        }






        public scmessageobjectjitter[][] ikbodyrender(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {
            //#region
            //UPPERBODY
            var _cuber001 = _player_torso[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_torso_BUFFER, _cuber001);
            _cuber001 = _player_pelvis[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_pelvis(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_pelvis_BUFFER, _cuber001);

            //_player_head[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            //scupdate._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _player_head[0][0].IndexCount, _player_head[0][0].InstanceCount, _player_head[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_torso_BUFFER, _player_head[0][0]);

            return _sc_jitter_tasks;
        }

        public scmessageobjectjitter[][] writeikbodytobuffer(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {

            Matrix[] worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;


            //PHYSICS PELVIS
            _player_pelvis[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_pelvis[0][0];
            _player_pelvis[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS TORSO
            _player_torso[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_torso[0][0];
            _player_torso[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS TORSO
            _player_head[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_head[0][0];
            _player_head[0][0]._POSITION = worldMatrix_base[0];

            //tick_perf_counter.Stop();
            //tick_perf_counter.Reset();
            //tick_perf_counter.Restart();

            voxel_cuber_pelvis = _player_pelvis[0][0];
            voxel_sometester_pelvis = voxel_cuber_pelvis._WORLDMATRIXINSTANCES;

            voxel_cuber_torso = _player_torso[0][0];
            voxel_sometester_torso = voxel_cuber_torso._WORLDMATRIXINSTANCES;

            voxel_cuber_head = _player_head[0][0];
            voxel_sometester_head = voxel_cuber_head._WORLDMATRIXINSTANCES;
            for (int i = 0; i < voxel_cuber_pelvis.instances.Length; i++)
            {
                //voxel_cuber = _player_pelvis[tempIndex];
                //voxel_instancers = voxel_cuber.instances;
                //voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                var xxx = voxel_sometester_pelvis[i].M41;
                var yyy = voxel_sometester_pelvis[i].M42;
                var zzz = voxel_sometester_pelvis[i].M43;

                voxel_cuber_pelvis.instances[i].position.X = xxx;
                voxel_cuber_pelvis.instances[i].position.Y = yyy;
                voxel_cuber_pelvis.instances[i].position.Z = zzz;
                voxel_cuber_pelvis.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_pelvis[i], out _testQuater);

                var dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_pelvis.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_pelvis.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_pelvis.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_pelvis.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_pelvis.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_pelvis.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_pelvis.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_pelvis.instancesDataUP[i].rotation.W = 1;


                //voxel_cuber = _player_torso[tempIndex];
                //voxel_instancers = voxel_cuber.instances;
                //voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_torso[i].M41;
                yyy = voxel_sometester_torso[i].M42;
                zzz = voxel_sometester_torso[i].M43;

                voxel_cuber_torso.instances[i].position.X = xxx;
                voxel_cuber_torso.instances[i].position.Y = yyy;
                voxel_cuber_torso.instances[i].position.Z = zzz;
                voxel_cuber_torso.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_torso[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_torso.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_torso.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_torso.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_torso.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_torso.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_torso.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_torso.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_torso.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_torso.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_torso.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_torso.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_torso.instancesDataUP[i].rotation.W = 1;





                xxx = voxel_sometester_head[i].M41;
                yyy = voxel_sometester_head[i].M42;
                zzz = voxel_sometester_head[i].M43;

                voxel_cuber_head.instances[i].position.X = xxx;
                voxel_cuber_head.instances[i].position.Y = yyy;
                voxel_cuber_head.instances[i].position.Z = zzz;
                voxel_cuber_head.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_head[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_head.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_head.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_head.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_head.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_head.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_head.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_head.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_head.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_head.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_head.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_head.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_head.instancesDataUP[i].rotation.W = 1;








            }



            //PHYSICS CONTAINMENT GRID HANDRIGHT
            /*_world_containment_grid_list_RH[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[0][0];// worldMatrix_instances_containment_grid_RH[0][0];// _world_containment_grid_list_RH[0][0]._arrayOfInstances[_iterator].current_pos;// worldMatrix_instances_containment_grid_RH[0][0]; // 
            _world_containment_grid_list_RH[0][0]._POSITION = worldMatrix_base[0];

            var cuber_containment_grid_RH = _world_containment_grid_list_RH[0][0];
            var instancers_containment_grid_RH = cuber_containment_grid_RH.instances;
            var sometester_containment_grid_RH = cuber_containment_grid_RH._WORLDMATRIXINSTANCES;


            for (int i = 0; i < instancers_containment_grid_RH.Length; i++)
            {
                float xxx = sometester_containment_grid_RH[i].M41;
                float yyy = sometester_containment_grid_RH[i].M42;
                float zzz = sometester_containment_grid_RH[i].M43;

                cuber_containment_grid_RH.instances[i].position.X = xxx;
                cuber_containment_grid_RH.instances[i].position.Y = yyy;
                cuber_containment_grid_RH.instances[i].position.Z = zzz;
                cuber_containment_grid_RH.instances[i].position.W = 1;

                Quaternion.RotationMatrix(ref sometester_containment_grid_RH[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                //var dirInstance = _newgetdirforward(_testQuater);
                cuber_containment_grid_RH.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber_containment_grid_RH.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_RH.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_RH.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                //dirInstance = -_newgetdirleft(_testQuater);
                cuber_containment_grid_RH.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber_containment_grid_RH.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_RH.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_RH.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = sc_maths._newgetdirup(quat_buffers);
                //dirInstance = dirInstance = _newgetdirup(_testQuater);
                cuber_containment_grid_RH.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber_containment_grid_RH.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_RH.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_RH.instancesDataUP[i].rotation.W = 1;
            }
            //END OF*/
            return _sc_jitter_tasks;
        }







        int hasparentobject = -1;



        public float voxel_general_size = 0.0025f;

        public static float voxeliknewsize = 0.002f;
        public static float voxeliknewsizealt = 0.00015f;//0.00015f;
        public scmessageobjectjitter[][] createikarm(scmessageobjectjitter[][] _sc_jitter_tasks, int tempMultiInstancePhysicsTotal, Vector3 ikarmpivotinitposition, sccsikvoxellimbs parentobject_, int somechunkpriminstanceikarmvoxelindex, int human_inst_limbs_x, int human_inst_limbs_y, int human_inst_limbs_z, int grabtargetitem_, int typeoflimb, sccsikvoxellimbs ikvoxellimb)
        {
            float r = 0.19f;
            float g = 0.19f;
            float b = 0.19f;
            float a = 1;


            if (typeoflimb == 0)
            {
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                voxel_general_size = voxeliknewsize;
            }
            else if (typeoflimb == 1)
            {
                r = 0.99f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                voxel_general_size = voxeliknewsize;
            }

            grabtargetitem = grabtargetitem_;


            if (parentobject_ != null)
            {
                parentobject = parentobject_;
                hasparentobject = 1;
            }

            initchunkposition = ikarmpivotinitposition;

            if (Program.usejitterphysics == 1)
            {
                //SETTING UP SINGLE WORLD OBJECTS
                //END OF LOOP FOR PHYSICS ENGINE INSTANCES
                _some_data0 = (object)_sc_jitter_tasks[0][0]._world_data[0];
                //World[] _jitter_worlds0 = (World[])_some_data0;
                _thejitter_world = (World)_some_data0;
            }


            _human_inst_rig_x = human_inst_limbs_x;
            _human_inst_rig_y = human_inst_limbs_y;
            _human_inst_rig_z = human_inst_limbs_z;


            int _inst_p_r_hand_x = _human_inst_rig_x;
            int _inst_p_r_hand_y = _human_inst_rig_y;
            int _inst_p_r_hand_z = _human_inst_rig_z;
            int _inst_p_r_shoulder_x = _human_inst_rig_x;
            int _inst_p_r_shoulder_y = _human_inst_rig_y;
            int _inst_p_r_shoulder_z = _human_inst_rig_z;
            int _inst_p_r_upperarm_x = _human_inst_rig_x;
            int _inst_p_r_upperarm_y = _human_inst_rig_y;
            int _inst_p_r_upperarm_z = _human_inst_rig_z;
            int _inst_p_r_lowerarm_x = _human_inst_rig_x;
            int _inst_p_r_lowerarm_y = _human_inst_rig_y;
            int _inst_p_r_lowerarm_z = _human_inst_rig_z;

            int _type_of_cube = 2;
            bool is_static = false;


            var WorldMatrix = Matrix.Identity;
            float _dist_between = 0.25f;
            int _addToWorld = 0;

            int voxel_type = 2;






          


            _player_rght_elbow_target_three = new sc_voxel[tempMultiInstancePhysicsTotal][];
            worldMatrix_instances_r_elbow_target_three = new Matrix[tempMultiInstancePhysicsTotal][][];

            _player_rght_hnd = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_shldr = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_lower_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_upper_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target_two = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_r_hand_grab = new sc_voxel[tempMultiInstancePhysicsTotal][];
            worldMatrix_instances_r_hand_grab = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target_two = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_hand = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_shoulder = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_upperarm = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_lowerarm = new Matrix[tempMultiInstancePhysicsTotal][][];

            //UPPER BODY
            _player_r_hand_grab[0] = new sc_voxel[1];
            worldMatrix_instances_r_hand_grab[0] = new Matrix[1][];
            _player_rght_hnd[0] = new sc_voxel[1];
            worldMatrix_instances_r_hand[0] = new Matrix[1][];
            _player_rght_shldr[0] = new sc_voxel[1];
            worldMatrix_instances_r_shoulder[0] = new Matrix[1][];
            _player_rght_lower_arm[0] = new sc_voxel[1];
            worldMatrix_instances_r_lowerarm[0] = new Matrix[1][];
            _player_rght_upper_arm[0] = new sc_voxel[1];
            worldMatrix_instances_r_upperarm[0] = new Matrix[1][];
            _player_rght_elbow_target[0] = new sc_voxel[1];
            worldMatrix_instances_r_elbow_target[0] = new Matrix[1][];
            _player_rght_elbow_target_two[0] = new sc_voxel[1];
            worldMatrix_instances_r_elbow_target_two[0] = new Matrix[1][];



            _player_rght_elbow_target_three[0] = new sc_voxel[1];
            worldMatrix_instances_r_elbow_target_three[0] = new Matrix[1][];




            int minx = 1;
            int miny = 1;
            int minz = 1;

            int diagmaxx = 1;
            int diagmaxy = 1;
            int diagmaxz = 1;

            int diagminx = 1;
            int diagminy = 1;
            int diagminz = 1;

            int chunkwidthl = 1;
            int chunkwidthr = 1;

            int chunkheightl = 1;
            int chunkheightr = 1;

            int chunkdepthl = 1;
            int chunkdepthr = 1;

            float distance = 1.0f;








            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;
            //PLAYER RIGHT SHOULDER
            /*float r = 0.19f;
            float g = 0.19f;
            float b = 0.19f;
            float a = 1;*/




            Matrix initialmatrix =  WorldMatrix;

            

           
            //initialmatrix.M41 = 0;// parentobject.initchunkposition.X + ikarmpivotinitposition.X;
            //initialmatrix.M42 = 0;// parentobject.initchunkposition.Y + ikarmpivotinitposition.Y;create
            //initialmatrix.M43 = 0;
            //initialmatrix.M44 = 1;
            float offsetPosX = _dist_between * 2;
            float offsetPosY = _dist_between * 2;
            float offsetPosZ = _dist_between * 2;
            //_player_rght_shldr[0] = new sc_voxel();
            //_hasinit0 = _player_rght_shldr.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerShoulderRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            float _mass = 100;
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            _type_of_cube = 2;


            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 12;
            chunkwidthr = 11;

            chunkheightl = 12;
            chunkheightr = 11;

            chunkdepthl = 15;
            chunkdepthr = 14;
            distance = 35;

            float sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            float sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            float sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;



            if (somechunkpriminstanceikarmvoxelindex == 0 || somechunkpriminstanceikarmvoxelindex == 3)
            {
                initialmatrix.M42 = parentobject_._player_torso[0][0]._arrayOfInstances[0].current_pos.M42 + (parentobject_._player_torso[0][0]._total_torso_height * 0.5f);
            }
            else
            {
                initialmatrix.M42 = parentobject_._player_pelvis[0][0]._arrayOfInstances[0].current_pos.M42 - (parentobject_._player_pelvis[0][0]._total_torso_height * 0.5f);

            }

            if (somechunkpriminstanceikarmvoxelindex == 0)
            {
                var widthoftorso = parentobject_._player_torso[0][0]._total_torso_width + sizelowerax;
                initialmatrix.M41 -= widthoftorso * 0.5f;
            }
            else if (somechunkpriminstanceikarmvoxelindex == 1)
            {
                var widthofpelvis = parentobject_._player_pelvis[0][0]._total_torso_width;

                initialmatrix.M41 -= widthofpelvis * 0.5f;
            }
            else if (somechunkpriminstanceikarmvoxelindex == 2)
            {
                var widthofpelvis = parentobject_._player_pelvis[0][0]._total_torso_width;

                initialmatrix.M41 += widthofpelvis * 0.5f;
            }
            else if (somechunkpriminstanceikarmvoxelindex == 3)
            {
                var widthoftorso = parentobject_._player_torso[0][0]._total_torso_width + sizelowerax;

                initialmatrix.M41 += widthoftorso * 0.5f;
            }


            _player_rght_shldr[0][0] = new sc_voxel();
            //_player_rght_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerShoulderRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            //_player_rght_shldr[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerShoulderRight, 9, 9, 9, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, new Vector3(0, 0, 0), 17, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_shldr[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Program.consoleHandle,
                initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerShoulderRight,
                   //9, 9, 9, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, 
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
                voxel_general_size, new Vector3(0, 0, 0), 14, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_r_shoulder[0][0] = new Matrix[_inst_p_r_shoulder_x * _inst_p_r_shoulder_y * _inst_p_r_shoulder_z];
            for (int i = 0; i < worldMatrix_instances_r_shoulder[0][0].Length; i++)
            {
                worldMatrix_instances_r_shoulder[0][0][i] = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }





            //_player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M41 = 



            ////////////////////////////////////////////////
            //////////CONTAINMENT GRIDS RIGHT HAND//////////
            ////////////////////////////////////////////////
            /*r = 0.85f;
            g = 0.85f;
            b = 0.85f;
            a = 1;
            _object_worldmatrix = Matrix.Identity;
            //offsetPosX = _grid_size_x * 1.15f; //x between each world instance
            //offsetPosY = _grid_size_y * 1.15f; //y between each world instance
            //offsetPosZ = _grid_size_z * 1.15f; //z between each world instance
            _object_worldmatrix = WorldMatrix;
            _object_worldmatrix.M41 = 0;set
            _size_screen = 0.015f;
            _object_worldmatrix.M42 = _player_rght_hnd[0][0]._arrayOfInstances[0]._POSITION.M42 + (_player_rght_hnd[0][0]._total_torso_height) + (1 * 0.001f); //_terrain_size_y + (_terrain_size_y * 0.501f)-5 //_terrain[0][0]._arrayOfInstances[0]._POSITION.M42
            _object_worldmatrix.M43 = 0;
            _object_worldmatrix.M44 = 1;
            _type_of_cube = 0;
            _world_containment_grid_list_RH[0] = new sc_containment_grid[1];
            _world_containment_grid_list_RH[0][0] = new sc_containment_grid();
            _world_containment_grid_list_RH[0][0].Initialize(SC_console_directx.D3D, 10, 10, _size_screen, 10, 10, _grid_size_x, _grid_size_y, _grid_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, scupdate.HWND, _object_worldmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, sccs.scgraphics.scdirectx.BodyTag.sc_containment_grid, true, 0, 10, 0, 0, 0, 0, 0, 0, false, true, false, false, false, false); //, "terrainGrassDirt.bmp" //0.00035f
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    //_world_containment_grid_list_RH[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, scupdate.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, sccs.scgraphics.scdirectx.BodyTag.sc_containment_grid, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            worldMatrix_instances_containment_grid_RH[0] = new Matrix[1][];
            worldMatrix_instances_containment_grid_RH[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_containment_grid_RH[0][0].Length; i++)
            {
                worldMatrix_instances_containment_grid_RH[0][0][i] = _player_rght_hnd[0][0]._arrayOfInstances[i].current_pos;
            }*/










            //RIGHT UPPER ARM
            /*r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;*/
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;


            //initialmatrix.M41 = 0;
            //initialmatrix.M42 = 0;// 0 + -0.375f;
            //initialmatrix.M43 = 0;
            //initialmatrix.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_shldr[0][0]._total_torso_depth * 0.5f);


          

            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;

            //offsetPosX = 0;
            //offsetPosY = 0;
            //offsetPosZ = 0;
            //_player_rght_upper_arm[0] = new sc_voxel();
            //_hasinit0 = _player_rght_upper_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_upperarm_x, _inst_p_r_upperarm_y, _inst_p_r_upperarm_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerUpperArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            //13, 12, 40, 39, 13, 12,


            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            //vertoffsetx = -13 * voxel_general_size;
            //vertoffsety = -40 * voxel_general_size ;
            //vertoffsetz = -13 * voxel_general_size;

            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 7;
            chunkwidthr = 6;

            chunkheightl = 7;
            chunkheightr = 6;

            chunkdepthl = 31;
            chunkdepthr = 30;
            distance = 63;

            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;


            initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);


            initialmatrix.M41 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
            initialmatrix.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_shldr[0][0]._total_torso_height * 0.5f);
            initialmatrix.M43 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M43;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);


            initialmatrix.M44 = 1;
            //_player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos
            initialmatrix.M42 -= (sizeloweraz * 0.5f);
            //_player_rght_upper_arm[0][0]._POSITION = initialmatrix;
            //_player_rght_upper_arm[0][0]._ORIGINPOSITION = initialmatrix;
            //_player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos = initialmatrix;




            _type_of_cube = 2;
            _player_rght_upper_arm[0][0] = new sc_voxel();//0.035f, 0.1055f, 0.035f
            //_player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerUpperArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_upper_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a),
            _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
            sccs.scgraphics.scdirectx.BodyTag.PlayerUpperArmRight,
            //9, 9, 9, 14, 14, 14, 14, 14, 14, 13, 12, 40, 39, 13, 12,
            minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
            voxel_general_size, new Vector3(0, 0, 0), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

            
            worldMatrix_instances_r_upperarm[0][0] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
            for (int i = 0; i < worldMatrix_instances_r_upperarm[0][0].Length; i++)
            {
                worldMatrix_instances_r_upperarm[0][0][i] = _player_rght_upper_arm[0][0]._arrayOfInstances[i].current_pos;//Matrix.Identity;
            }





            /*
            Vector3 shoulderposition = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M43);
            var somerotationmatrix = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos;
            Quaternion somequatrot;
            Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
            direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
            direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
            direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);

            shoulderposition = shoulderposition + (-direction_feet_right_ori);
            shoulderposition = shoulderposition + (new Vector3(_player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M43) * -direction_feet_up_ori);

            _player_rght_upper_arm[0][0]._arrayOfInstances[0].virtualelbowdirrightpoint = shoulderposition;
            */









            //RIGHT UPPER ARM
            /*r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;*/
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;


            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0;// 0 + -0.375f;
            initialmatrix.M43 = 0;

            initialmatrix.M44 = 1;

            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;

            //offsetPosX = 0;
            //offsetPosY = 0;
            //offsetPosZ = 0;
            //_player_rght_upper_arm[0] = new sc_voxel();
            //_hasinit0 = _player_rght_upper_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_upperarm_x, _inst_p_r_upperarm_y, _inst_p_r_upperarm_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerUpperArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            //13, 12, 40, 39, 13, 12,



            /*
            if (typeoflimb == 0)
            {
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                voxel_general_size = voxeliknewsize;
            }
            else if (typeoflimb == 1)
            {
                r = 0.99f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                voxel_general_size = voxeliknewsize;
            }*/

            /*
            float voxel_general_size = 0.0025f;
            float voxeliknewsize = 0.002f;
            float voxeliknewsizealt = 0.00015f;
            */

            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            //vertoffsetx = -13 * voxel_general_size;
            //vertoffsety = -40 * voxel_general_size ;
            //vertoffsetz = -13 * voxel_general_size;


            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 7;
            chunkwidthr = 6;

            chunkheightl = 7;
            chunkheightr = 6;

            chunkdepthl = 31;
            chunkdepthr = 30;
            distance = 63;

            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;

            initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
            //initialmatrix.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_shldr[0][0]._total_torso_depth * 0.5f);
            initialmatrix.M41 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
            initialmatrix.M42 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f) -(sizeloweraz * 0.5f);
            initialmatrix.M43 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M43;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);



            _type_of_cube = 2;
            _player_rght_lower_arm[0][0] = new sc_voxel();//0.035f, 0.1055f, 0.035f
            //_player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerUpperArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_lower_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a),
            _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
            sccs.scgraphics.scdirectx.BodyTag.PlayerLowerArmLeft,
            //9, 9, 9, 14, 14, 14, 14, 14, 14, 13, 12, 40, 39, 13, 12,
            minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
            voxel_general_size, new Vector3(0, 0, 0), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_lowerarm[0][0] = new Matrix[_inst_p_r_lowerarm_x * _inst_p_r_lowerarm_y * _inst_p_r_lowerarm_z];
            for (int i = 0; i < worldMatrix_instances_r_lowerarm[0][0].Length; i++)
            {
                worldMatrix_instances_r_lowerarm[0][0][i] = _player_rght_lower_arm[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }










            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 1;
            chunkwidthr = 0;

            chunkheightl = 1;
            chunkheightr = 0;

            chunkdepthl = 1;
            chunkdepthr = 0;
            distance = 5;


            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;

            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            //RIGHT ELBOW TARGET
            r = 0.19f;
            g = 0.99f;
            b = 0.19f;
            a = 1;
            //initialmatrix.M41 = -0.25f; /
            //initialmatrix.M42 = -0.2f;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0 - (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) - 0.45f);
            initialmatrix.M43 = 0 + 3;

            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_elbow_target[0] = new sc_voxel();
            //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            _type_of_cube = 2;
            _player_rght_elbow_target[0][0] = new sc_voxel();
            _player_rght_elbow_target[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle,
                initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTarget,
                      minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr, 
                      voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_elbow_target[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target[0][0].Length; i++)
            {
                worldMatrix_instances_r_elbow_target[0][0][i] = _player_rght_elbow_target[0][0]._arrayOfInstances[i].current_pos;//Matrix.Identity;
            }






            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 1;
            chunkwidthr = 0;

            chunkheightl = 1;
            chunkheightr = 0;

            chunkdepthl = 1;
            chunkdepthr = 0;
            distance = 5;


            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;


            //RIGHT ELBOW TARGET TWO
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.19f;
            g = 0.19f;
            b = 0.99f;
            a = 1;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0 + (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) + 1);
            initialmatrix.M43 = 0 + 0;


            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_elbow_target_two[0] = new sc_voxel();
            //_hasinit0 = _player_rght_elbow_target_two.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target_two[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            _type_of_cube = 2;
            _player_rght_elbow_target_two[0][0] = new sc_voxel();
            _player_rght_elbow_target_two[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz,
                new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ,
                _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTargettwo,
                      minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_elbow_target_two[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target_two[0][0].Length; i++)
            {
                worldMatrix_instances_r_elbow_target_two[0][0][i] = _player_rght_elbow_target_two[0][0]._arrayOfInstances[i].current_pos;//Matrix.Identity;
            }







            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 1;
            chunkwidthr = 0;

            chunkheightl = 1;
            chunkheightr = 0;

            chunkdepthl = 1;
            chunkdepthr = 0;
            distance = 5;


            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;


            //RIGHT ELBOW TARGET THREE
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.99f;
            g = 0.15f;
            b = 0.15f;
            a = 1;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0 + (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) + 1);
            initialmatrix.M43 = 0 + 0;



            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_elbow_target_two[0] = new sc_voxel();
            //_hasinit0 = _player_rght_elbow_target_two.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target_two[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            _type_of_cube = 2;
            _player_rght_elbow_target_three[0][0] = new sc_voxel();
            _player_rght_elbow_target_three[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, 
                new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, 
                _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTargettwo,
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
                voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_elbow_target_three[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target_three[0][0].Length; i++)
            {
                worldMatrix_instances_r_elbow_target_three[0][0][i] = _player_rght_elbow_target_three[0][0]._arrayOfInstances[i].current_pos;//Matrix.Identity;
            }









            if (grabtargetitem == 0)
            {



                /*
                _inst_p_r_hand_x = 2;
                _inst_p_r_hand_y = 1;
                _inst_p_r_hand_z = 1;
                */

                //PLAYER RIGHT HAND GRAB
                //voxel_type = 2;
                /*r = 0.19f;
                 g = 0.19f;
                 b = 0.19f;
                 a = 1;*/
                //instX = 1;
                //instY = 1;
                //instZ = 1;
                initialmatrix = Matrix.Identity;
                initialmatrix = WorldMatrix;
                initialmatrix.M41 = 0;
                initialmatrix.M42 = 0;// + -(_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
                initialmatrix.M43 = 0;
                initialmatrix.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                _mass = 100;
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;









                initialmatrix.M41 = 0;
                initialmatrix.M42 = -1;// + -(_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
                initialmatrix.M43 = 0;
                voxel_type = 2;

                minx = 1;
                miny = 1;
                minz = 1;

                diagmaxx = 1;
                diagmaxy = 1;
                diagmaxz = 1;

                diagminx = 1;
                diagminy = 1;
                diagminz = 1;

                chunkwidthl = 1;
                chunkwidthr = 1;

                chunkheightl = 1;
                chunkheightr = 1;

                chunkdepthl = 1;
                chunkdepthr = 1;
                distance = 35;


                /*if (somechunkpriminstanceikarmvoxelindex == 1 || somechunkpriminstanceikarmvoxelindex == 2)
                {
                    initialmatrix.M41 = 0;
                    initialmatrix.M42 = -1;// + -(_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
                    initialmatrix.M43 = 0;
                    voxel_type = 2;

                    minx = 1;
                    miny = 1;
                    minz = 1;

                    diagmaxx = 1;
                    diagmaxy = 1;
                    diagmaxz = 1;

                    diagminx = 1;
                    diagminy = 1;
                    diagminz = 1;

                    chunkwidthl = 1;
                    chunkwidthr = 1;

                    chunkheightl = 1;
                    chunkheightr = 1;

                    chunkdepthl = 1;
                    chunkdepthr = 1;
                    distance = 35;
                }
                else
                {
                    voxel_type = 3;
                }*/

                /*if (voxel_type == 0)
                {
                    vertoffsetz = -13 * 0.075f;
                }
                else
                {
                    vertoffsetz = -13;
                }*/
                //voxel_general_size =  voxeliknewsize;
                //voxel_type = 2; // 3 for pickaxe
                _type_of_cube = 2;
                is_static = true;

                //covid19/snowflake/mine init = 9, 9, 9, 9, 9, 9, 9, 9, 9, 60, 60, 60, 60, 60, 60,



                //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr
                //3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 3, 65, 64, 40, 39


                distance = 10;
                _player_r_hand_grab[0][0] = new sc_voxel();
                _player_r_hand_grab[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightHandGrabTarget, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, voxel_general_size, new Vector3(0, 0, -0.1f), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                //_player_r_hand_grab[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightHandGrabTarget, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 3, 65, 64, 40, 39, voxel_general_size, new Vector3(0, 0, -0.1f), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_r_hand_grab[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_hand_grab[0][0].Length; i++)
                {
                    worldMatrix_instances_r_hand_grab[0][0][i] = _player_r_hand_grab[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
                }

            }












            //9, 9, 9, 18, 9, 9, 9, 9, 9, 4, 3, 13, 12, 18, 17
            if (somechunkpriminstanceikarmvoxelindex == 0)
            {
                minx = 7;
                miny = 7;
                minz = 7;

                diagmaxx = 7;
                diagmaxy = 7;
                diagmaxz = 7;

                diagminx = 7;
                diagminy = 7;
                diagminz = 7;

                chunkwidthl = 6;
                chunkwidthr = 5;

                chunkheightl = 2;
                chunkheightr = 1;

                chunkdepthl = 12;
                chunkdepthr = 11;
                distance = 25;
            }
            else if (somechunkpriminstanceikarmvoxelindex == 1)
            {

                minx = 7;
                miny = 7;
                minz = 7;

                diagmaxx = 7;
                diagmaxy = 7;
                diagmaxz = 7;

                diagminx = 7;
                diagminy = 7;
                diagminz = 7;

                chunkwidthl = 9;
                chunkwidthr = 8;

                chunkheightl = 2;
                chunkheightr = 1;

                chunkdepthl = 12;
                chunkdepthr = 11;
                distance = 20;
            }
            else if (somechunkpriminstanceikarmvoxelindex == 2)
            {
                minx = 7;
                miny = 7;
                minz = 7;

                diagmaxx = 7;
                diagmaxy = 7;
                diagmaxz = 7;

                diagminx = 7;
                diagminy = 7;
                diagminz = 7;

                chunkwidthl = 9;
                chunkwidthr = 8;

                chunkheightl = 2;
                chunkheightr = 1;

                chunkdepthl = 12;
                chunkdepthr = 11;
                distance = 20;
            }
            else if (somechunkpriminstanceikarmvoxelindex == 3)
            {
                minx = 7;
                miny = 7;
                minz = 7;

                diagmaxx = 7;
                diagmaxy = 7;
                diagmaxz = 7;

                diagminx = 7;
                diagminy = 7;
                diagminz = 7;

                chunkwidthl = 6;
                chunkwidthr = 5;

                chunkheightl = 2;
                chunkheightr = 1;

                chunkdepthl = 12;
                chunkdepthr = 11;
                distance = 25;
            }





            //PLAYER RIGHT HAND
            voxel_type = 2;
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            //instX = 1;
            //instY = 1;
            //instZ = 1;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            if (somechunkpriminstanceikarmvoxelindex == 0 || somechunkpriminstanceikarmvoxelindex == 3)
            {
                initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
            }
            else if (somechunkpriminstanceikarmvoxelindex == 1 || somechunkpriminstanceikarmvoxelindex == 2)
            {
                //initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
            }

            //initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
            initialmatrix.M41 = 0;
            initialmatrix.M42 = -0.65f;// - (_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
            initialmatrix.M43 = 0;

            initialmatrix.M41 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);
            initialmatrix.M42 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);
            initialmatrix.M43 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M43;// - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);




            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            _mass = 100;
            /*if (voxel_type == 0)
            {
                vertoffsetz = -10 * 0.075f;
            }
            else
            {
                vertoffsetz = -10;
            }*/
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;


            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;




            _player_rght_hnd[0][0] = new sc_voxel();
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            _type_of_cube = 2;
            is_static = true;
            //9, 9, 9, 18, 9, 9, 9, 9, 9, 4, 3, 13, 12, 18, 17 //0.0125f //0.035f //0.055f
            _player_rght_hnd[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle,
                initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerHandRight,
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr, voxel_general_size, new Vector3(0, 0, -0.1f),
                distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_r_hand[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_hand[0][0].Length; i++)
            {
                worldMatrix_instances_r_hand[0][0][i] = _player_rght_hnd[0][0]._arrayOfInstances[i].current_pos;//Matrix.Identity;
            }








            Vector3 posupperarm = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

            var somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);
            var dirShoulderToHand = somePosOfRightHand - posupperarm;



            var lengthOfLowerArmRight = _player_rght_lower_arm[0][0]._total_torso_depth * 1.0f;
            var lengthOfUpperArmRight = _player_rght_upper_arm[0][0]._total_torso_depth * 1.0f;
            var lengthOfHandRight = _player_rght_hnd[0][0]._total_torso_depth * 1.0f;
            var totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;



            Vector3 someshoulderpos = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M43);

            Quaternion somequatrot;
            var somerotationmatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos;
            Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
            direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
            direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
            direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);

 

            //somenewtargetlocation = someshoulderpos + (dirShoulderToHand * totalArmLengthRight * 2);
            somenewtargetlocation = someshoulderpos + (-direction_feet_forward_ori * lengthOfUpperArmRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 2);
            somenewtargetlocation = somenewtargetlocation + (-direction_feet_right_ori * lengthOfUpperArmRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 4);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_forward_ori * totalArmLengthRight * 2);
            Matrix somematrix = Matrix.Identity;
            somematrix.M41 = somenewtargetlocation.X;
            somematrix.M42 = somenewtargetlocation.Y;
            somematrix.M43 = somenewtargetlocation.Z;

            _player_rght_elbow_target_two[0][0]._arrayOfInstances[0].current_pos = somematrix;
            worldMatrix_instances_r_elbow_target_two[0][0][0] = somematrix;







            somenewtargetlocation = someshoulderpos + (dirShoulderToHand * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (-direction_feet_forward_ori * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 2);
            somenewtargetlocation = somenewtargetlocation + (-direction_feet_right_ori * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 4);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_forward_ori * totalArmLengthRight * 2);
            somematrix = Matrix.Identity;
            somematrix.M41 = somenewtargetlocation.X;
            somematrix.M42 = somenewtargetlocation.Y;
            somematrix.M43 = somenewtargetlocation.Z;

            _player_rght_elbow_target[0][0]._arrayOfInstances[0].current_pos = somematrix;
            worldMatrix_instances_r_elbow_target[0][0][0] = somematrix;

















            if (Program.usejitterphysics == 1)
            {
                for (int phys = 0; phys < Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez; phys++)
                {
                    for (int i = 0; i < Program.worldwidth * Program.worldheight * Program.worlddepth; i++)
                    {
                        object _some_dator = (object)_sc_jitter_tasks[phys][i]._world_data[0];
                        World _the_current_world = (World)_some_dator;

                        _the_current_world.AddBody(_player_rght_upper_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_lower_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_shldr[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                    }
                }
            }
















            _SC_modL_rght_shldr_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_rght_elbow_target_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };

            _SC_modL_rght_elbow_target_two_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_rght_upper_arm_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            }; _SC_modL_rght_lower_arm_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };


            _SC_modL_rght_hnd_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };




            _SC_modL_r_hand_grab_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };







            return _sc_jitter_tasks;
        }




        public scmessageobjectjitter[][] createikfingers(scmessageobjectjitter[][] _sc_jitter_tasks, int tempMultiInstancePhysicsTotal, Vector3 ikarmpivotinitposition, sccsikvoxellimbs parentobject_, int somechunkpriminstanceikarmvoxelindex, int human_inst_limbs_x, int human_inst_limbs_y, int human_inst_limbs_z, int grabtargetitem_, int typeoflimb, sccsikvoxellimbs ikvoxellimb, int somechunkpriminstanceikfingervoxelindex)
        {


            float r = 0.19f;
            float g = 0.19f;
            float b = 0.19f;
            float a = 1;


            if (typeoflimb == 0)
            {
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                voxel_general_size = voxeliknewsize;
            }
            else if (typeoflimb == 1)
            {
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                voxel_general_size = voxeliknewsizealt;
            }




            int minx = 1;
            int miny = 1;
            int minz = 1;

            int diagmaxx = 1;
            int diagmaxy = 1;
            int diagmaxz = 1;

            int diagminx = 1;
            int diagminy = 1;
            int diagminz = 1;

            int chunkwidthl = 1;
            int chunkwidthr = 1;

            int chunkheightl = 1;
            int chunkheightr = 1;

            int chunkdepthl = 1;
            int chunkdepthr = 1;

            float distance = 1.0f;

            float sizelowerax = 0.0f;
            float sizeloweray = 0.0f;
            float sizeloweraz = 0.0f;






            grabtargetitem = grabtargetitem_;


            if (parentobject_ != null)
            {
                parentobject = parentobject_;
                hasparentobject = 1;
            }

            initchunkposition = ikarmpivotinitposition;

            if (Program.usejitterphysics == 1)
            {
                //SETTING UP SINGLE WORLD OBJECTS
                //END OF LOOP FOR PHYSICS ENGINE INSTANCES
                _some_data0 = (object)_sc_jitter_tasks[0][0]._world_data[0];
                //World[] _jitter_worlds0 = (World[])_some_data0;
                _thejitter_world = (World)_some_data0;
            }


            _human_inst_rig_x = human_inst_limbs_x;
            _human_inst_rig_y = human_inst_limbs_y;
            _human_inst_rig_z = human_inst_limbs_z;


            int _inst_p_r_hand_x = _human_inst_rig_x;
            int _inst_p_r_hand_y = _human_inst_rig_y;
            int _inst_p_r_hand_z = _human_inst_rig_z;
            int _inst_p_r_shoulder_x = _human_inst_rig_x;
            int _inst_p_r_shoulder_y = _human_inst_rig_y;
            int _inst_p_r_shoulder_z = _human_inst_rig_z;
            int _inst_p_r_upperarm_x = _human_inst_rig_x;
            int _inst_p_r_upperarm_y = _human_inst_rig_y;
            int _inst_p_r_upperarm_z = _human_inst_rig_z;
            int _inst_p_r_lowerarm_x = _human_inst_rig_x;
            int _inst_p_r_lowerarm_y = _human_inst_rig_y;
            int _inst_p_r_lowerarm_z = _human_inst_rig_z;

            int _type_of_cube = 2;
            bool is_static = false;


            var WorldMatrix = Matrix.Identity;
            float _dist_between = 0.25f;
            int _addToWorld = 0;

            int voxel_type = 2;


            _player_rght_hnd = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_shldr = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_lower_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_upper_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target_two = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target_three = new sc_voxel[tempMultiInstancePhysicsTotal][];


            _player_r_hand_grab = new sc_voxel[tempMultiInstancePhysicsTotal][];
            worldMatrix_instances_r_hand_grab = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target_two = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target_three = new Matrix[tempMultiInstancePhysicsTotal][][];

            worldMatrix_instances_r_hand = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_shoulder = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_upperarm = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_lowerarm = new Matrix[tempMultiInstancePhysicsTotal][][];

            //UPPER BODY
            _player_r_hand_grab[0] = new sc_voxel[1];
            worldMatrix_instances_r_hand_grab[0] = new Matrix[1][];
            _player_rght_hnd[0] = new sc_voxel[1];
            worldMatrix_instances_r_hand[0] = new Matrix[1][];
            _player_rght_shldr[0] = new sc_voxel[1];
            worldMatrix_instances_r_shoulder[0] = new Matrix[1][];
            _player_rght_lower_arm[0] = new sc_voxel[1];
            worldMatrix_instances_r_lowerarm[0] = new Matrix[1][];
            _player_rght_upper_arm[0] = new sc_voxel[1];
            worldMatrix_instances_r_upperarm[0] = new Matrix[1][];
            _player_rght_elbow_target[0] = new sc_voxel[1];
            worldMatrix_instances_r_elbow_target[0] = new Matrix[1][];


            _player_rght_elbow_target_two[0] = new sc_voxel[1];
            worldMatrix_instances_r_elbow_target_two[0] = new Matrix[1][];
            _player_rght_elbow_target_three[0] = new sc_voxel[1];
            worldMatrix_instances_r_elbow_target_three[0] = new Matrix[1][];


            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;
            //PLAYER RIGHT SHOULDER
            /*float r = 0.19f;
            float g = 0.19f;
            float b = 0.19f;
            float a = 1;*/

            Matrix initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;
            initialmatrix.M41 = 0;// parentobject.initchunkposition.X + ikarmpivotinitposition.X;
            initialmatrix.M42 = 0;// parentobject.initchunkposition.Y + ikarmpivotinitposition.Y;
            initialmatrix.M43 = 0;
            initialmatrix.M44 = 1;
            float offsetPosX = _dist_between * 2;
            float offsetPosY = _dist_between * 2;
            float offsetPosZ = _dist_between * 2;
            //_player_rght_shldr[0] = new sc_voxel();
            //_hasinit0 = _player_rght_shldr.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerShoulderRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            float _mass = 100;
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            _type_of_cube = 2;

            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            // sizelowerax, sizeloweray, sizeloweraz


            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 10;
            chunkwidthr = 9;

            chunkheightl = 10;
            chunkheightr = 9;

            chunkdepthl = 10;
            chunkdepthr = 9;

            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;

            distance = 25;



            //KNUCKLE SECTION
            Matrix somerotmatrix = ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
            Quaternion otherQuat;
            Quaternion.RotationMatrix(ref somerotmatrix, out otherQuat);
            var dirhandforward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
            var dirhandright = sc_maths._getDirection(Vector3.Right, otherQuat);
            var dirhandup = sc_maths._getDirection(Vector3.Up, otherQuat);

            Vector3 POSITIONOFHAND = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

            var somefingerpos = POSITIONOFHAND + (dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f));
            //somefingerpos = somefingerpos + (dirhandforward * (_player_rght_shldr[0][0]._total_torso_depth));

            if (somechunkpriminstanceikarmvoxelindex == 0)
            {
                if (somechunkpriminstanceikfingervoxelindex == 0)
                {
                    somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.35f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 1)
                {
                    somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.15f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 2)
                {
                    somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.05f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 3)
                {
                    somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.25f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 4)
                {
                    somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.55f));
                    somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f * 0.45f));
                }
            }
            else if (somechunkpriminstanceikarmvoxelindex == 3)
            {
                if (somechunkpriminstanceikfingervoxelindex == 0)
                {
                    somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.35f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 1)
                {
                    somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.15f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 2)
                {
                    somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.05f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 3)
                {
                    somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.25f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 4)
                {
                    somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.55f));
                    somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f * 0.45f));
                }
            }
            else
            {
                if (somechunkpriminstanceikfingervoxelindex == 0)
                {
                    somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.35f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 1)
                {
                    somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.15f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 2)
                {
                    somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.05f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 3)
                {
                    somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.25f));
                }
                else if (somechunkpriminstanceikfingervoxelindex == 4)
                {
                    somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.45f));
                    //somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f * 0.45f));
                }
            }


            initialmatrix.M41 = somefingerpos.X;
            initialmatrix.M42 = somefingerpos.Y;
            initialmatrix.M43 = somefingerpos.Z;




            _player_rght_shldr[0][0] = new sc_voxel();
            //_player_rght_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerShoulderRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            //_player_rght_shldr[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerShoulderRight, 9, 9, 9, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, new Vector3(0, 0, 0), 17, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_shldr[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Program.consoleHandle,
                initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerShoulderRight,
                //9, 9, 9, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, 
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr, voxel_general_size,
                new Vector3(0, 0, 0), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_r_shoulder[0][0] = new Matrix[_inst_p_r_shoulder_x * _inst_p_r_shoulder_y * _inst_p_r_shoulder_z];
            for (int i = 0; i < worldMatrix_instances_r_shoulder[0][0].Length; i++)
            {
                worldMatrix_instances_r_shoulder[0][0][i] = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }





            //_player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M41 = 



            ////////////////////////////////////////////////
            //////////CONTAINMENT GRIDS RIGHT HAND//////////
            ////////////////////////////////////////////////
            /*r = 0.85f;
            g = 0.85f;
            b = 0.85f;
            a = 1;
            _object_worldmatrix = Matrix.Identity;
            //offsetPosX = _grid_size_x * 1.15f; //x between each world instance
            //offsetPosY = _grid_size_y * 1.15f; //y between each world instance
            //offsetPosZ = _grid_size_z * 1.15f; //z between each world instance
            _object_worldmatrix = WorldMatrix;
            _object_worldmatrix.M41 = 0;set
            _size_screen = 0.015f;
            _object_worldmatrix.M42 = _player_rght_hnd[0][0]._arrayOfInstances[0]._POSITION.M42 + (_player_rght_hnd[0][0]._total_torso_height) + (1 * 0.001f); //_terrain_size_y + (_terrain_size_y * 0.501f)-5 //_terrain[0][0]._arrayOfInstances[0]._POSITION.M42
            _object_worldmatrix.M43 = 0;
            _object_worldmatrix.M44 = 1;
            _type_of_cube = 0;
            _world_containment_grid_list_RH[0] = new sc_containment_grid[1];
            _world_containment_grid_list_RH[0][0] = new sc_containment_grid();
            _world_containment_grid_list_RH[0][0].Initialize(SC_console_directx.D3D, 10, 10, _size_screen, 10, 10, _grid_size_x, _grid_size_y, _grid_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, scupdate.HWND, _object_worldmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, sccs.scgraphics.scdirectx.BodyTag.sc_containment_grid, true, 0, 10, 0, 0, 0, 0, 0, 0, false, true, false, false, false, false); //, "terrainGrassDirt.bmp" //0.00035f
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    //_world_containment_grid_list_RH[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, scupdate.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, sccs.scgraphics.scdirectx.BodyTag.sc_containment_grid, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            worldMatrix_instances_containment_grid_RH[0] = new Matrix[1][];
            worldMatrix_instances_containment_grid_RH[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_containment_grid_RH[0][0].Length; i++)
            {
                worldMatrix_instances_containment_grid_RH[0][0][i] = _player_rght_hnd[0][0]._arrayOfInstances[i].current_pos;
            }*/



            //RIGHT UPPER ARM
            /*r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;*/
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;


            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0;// 0 + -0.375f;
            initialmatrix.M43 = 0;

            initialmatrix.M44 = 1;

            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;

            //offsetPosX = 0;
            //offsetPosY = 0;
            //offsetPosZ = 0;
            //_player_rght_upper_arm[0] = new sc_voxel();
            //_hasinit0 = _player_rght_upper_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_upperarm_x, _inst_p_r_upperarm_y, _inst_p_r_upperarm_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerUpperArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            //13, 12, 40, 39, 13, 12,


            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            //vertoffsetx = -13 * voxel_general_size;
            //vertoffsety = -40 * voxel_general_size ;
            //vertoffsetz = -13 * voxel_general_size;
            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            // sizelowerax, sizeloweray, sizeloweraz

            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 10;
            chunkwidthr = 9;

            chunkheightl = 10;
            chunkheightr = 9;

            chunkdepthl = 30;
            chunkdepthr = 29;



            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;

            if (somechunkpriminstanceikarmvoxelindex == 0 || somechunkpriminstanceikarmvoxelindex == 3)
            {
                initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
                initialmatrix.M41 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
                initialmatrix.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_shldr[0][0]._total_torso_depth * 0.05f) - (sizeloweraz * 0.5f * 0.05f);
                initialmatrix.M43 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M43;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);

            }
            else if (somechunkpriminstanceikarmvoxelindex == 1 || somechunkpriminstanceikarmvoxelindex == 2)
            {
                //initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
                initialmatrix.M41 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
                initialmatrix.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42 ;
                initialmatrix.M43 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M43 - (_player_rght_shldr[0][0]._total_torso_depth * 0.05f) - (sizeloweraz * 0.5f * 0.05f);// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);

            }

            _type_of_cube = 2;
            _player_rght_upper_arm[0][0] = new sc_voxel();
            //_player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerUpperArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_upper_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a),
            _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
            sccs.scgraphics.scdirectx.BodyTag.PlayerUpperArmRight,
            //9, 9, 9, 14, 14, 14, 14, 14, 14, 13, 12, 40, 39, 13, 12,
           minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
            voxel_general_size, new Vector3(0, 0, 0), 30, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_upperarm[0][0] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
            for (int i = 0; i < worldMatrix_instances_r_upperarm[0][0].Length; i++)
            {
                worldMatrix_instances_r_upperarm[0][0][i] = _player_rght_upper_arm[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }







            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;

            //RIGHT LOWER ARM
            /*r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;*/
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0;//  0 + -0.15f;
            initialmatrix.M43 = 0;


            /*initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
            //initialmatrix.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_shldr[0][0]._total_torso_depth * 0.5f);
            initialmatrix.M41 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
            initialmatrix.M42 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
            initialmatrix.M43 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M43;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
            */
      
     

            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;

            //_player_lft_lower_arm[0] = new sc_voxel();
            //_hasinit0 = _player_lft_lower_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_l_lowerarm_x, _inst_p_l_lowerarm_y, _inst_p_l_lowerarm_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerLowerArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            _type_of_cube = 2;
            
            
            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            // sizelowerax, sizeloweray, sizeloweraz

            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 9;
            chunkwidthr = 8;

            chunkheightl = 9;
            chunkheightr = 8;

            chunkdepthl = 28;
            chunkdepthr = 27;


            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;

            if (somechunkpriminstanceikarmvoxelindex == 0 || somechunkpriminstanceikarmvoxelindex == 3)
            {
                initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
                //initialmatrix.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_shldr[0][0]._total_torso_depth * 0.5f);
                initialmatrix.M41 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
                initialmatrix.M42 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f * 0.05f) - sizeloweraz * 0.5f * 0.05f;
                initialmatrix.M43 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M43;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);

            }
            else if (somechunkpriminstanceikarmvoxelindex == 1 || somechunkpriminstanceikarmvoxelindex == 2)
            {
                //initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
                //initialmatrix.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_shldr[0][0]._total_torso_depth * 0.5f);
                initialmatrix.M41 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
                initialmatrix.M42 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M42 ;
                initialmatrix.M43 = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos.M43 - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f * 0.05f) - sizeloweraz * 0.5f * 0.05f;// - (_player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);

            }


            _player_rght_lower_arm[0][0] = new sc_voxel();
            // _player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerLowerArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_lower_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a),
                _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                sccs.scgraphics.scdirectx.BodyTag.PlayerLowerArmRight,
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
                voxel_general_size, new Vector3(0, 0, 0), 30, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                                                                                                                               //_player_rght_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerLowerArmRight, 7, 7, 9, 18, 21, 25, 10, 9, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 72, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

            //FOR VOXEL ARROW
            //_player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, sccs.scgraphics.scdirectx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 17, 100, 3, 10, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 70, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

            worldMatrix_instances_r_lowerarm[0][0] = new Matrix[_inst_p_r_lowerarm_x * _inst_p_r_lowerarm_y * _inst_p_r_lowerarm_z];
            for (int i = 0; i < worldMatrix_instances_r_lowerarm[0][0].Length; i++)
            {
                worldMatrix_instances_r_lowerarm[0][0][i] = _player_rght_lower_arm[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }
















            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            //RIGHT ELBOW TARGET
            r = 0.19f;
            g = 0.99f;
            b = 0.19f;
            a = 1;
            //initialmatrix.M41 = -0.25f; /
            //initialmatrix.M42 = -0.2f;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0 - (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) - 0.45f);
            initialmatrix.M43 = 0 + 3;




            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_elbow_target[0] = new sc_voxel();
            //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;
            _type_of_cube = 2;

            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            // sizelowerax, sizeloweray, sizeloweraz

            minx = 2;
            miny = 2;
            minz = 2;

            diagmaxx = 2;
            diagmaxy = 2;
            diagmaxz = 2;

            diagminx = 9;
            diagminy = 9;
            diagminz = 9;

            chunkwidthl = 10;
            chunkwidthr = 9;

            chunkheightl = 10;
            chunkheightr = 9;

            chunkdepthl = 10;
            chunkdepthr = 9;


            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;


            _player_rght_elbow_target[0][0] = new sc_voxel();
            _player_rght_elbow_target[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1,
               sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle,
               initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTarget,
              minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,
               voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_elbow_target[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target[0][0].Length; i++)
            {
                worldMatrix_instances_r_elbow_target[0][0][i] = _player_rght_elbow_target[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }








            //RIGHT ELBOW TARGET TWO
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.19f;
            g = 0.19f;
            b = 0.99f;
            a = 1;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0 + (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) + 1);
            initialmatrix.M43 = 0 + 0;


            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_elbow_target_two[0] = new sc_voxel();
            //_hasinit0 = _player_rght_elbow_target_two.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target_two[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;

            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            // sizelowerax, sizeloweray, sizeloweraz

            minx = 2;
            miny = 2;
            minz = 2;

            diagmaxx = 2;
            diagmaxy = 2;
            diagmaxz = 2;

            diagminx = 2;
            diagminy = 2;
            diagminz = 2;

            chunkwidthl = 10;
            chunkwidthr = 9;

            chunkheightl = 10;
            chunkheightr = 9;

            chunkdepthl = 10;
            chunkdepthr = 9;

            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;



            _type_of_cube = 2;
            _player_rght_elbow_target_two[0][0] = new sc_voxel();
            _player_rght_elbow_target_two[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1,
                 sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle,
                initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, 
                sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTargettwo,
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_elbow_target_two[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target_two[0][0].Length; i++)
            {
                worldMatrix_instances_r_elbow_target_two[0][0][i] = _player_rght_elbow_target_two[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }



            //RIGHT ELBOW TARGET THREE
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.99f;
            g = 0.50f;
            b = 0.99f;
            a = 1;
            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0 + (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) + 1);
            initialmatrix.M43 = 0 + 0;


            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_elbow_target_two[0] = new sc_voxel();
            //_hasinit0 = _player_rght_elbow_target_two.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, initialmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target_two[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, initialmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size =  voxeliknewsize;
            //voxel_type = 0;


            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            // sizelowerax, sizeloweray, sizeloweraz

            minx = 2;
            miny = 2;
            minz = 2;

            diagmaxx = 2;
            diagmaxy = 2;
            diagmaxz = 2;

            diagminx = 2;
            diagminy = 2;
            diagminz = 2;

            chunkwidthl = 10;
            chunkwidthr = 9;

            chunkheightl = 10;
            chunkheightr = 9;

            chunkdepthl = 10;
            chunkdepthr = 9;

            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;


            _type_of_cube = 2;
            _player_rght_elbow_target_three[0][0] = new sc_voxel();
            _player_rght_elbow_target_three[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle,
                initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightElbowTargettwo,
               minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

                voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_elbow_target_three[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target_three[0][0].Length; i++)
            {
                worldMatrix_instances_r_elbow_target_three[0][0][i] = _player_rght_elbow_target_three[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }





            if (typeoflimb == 0)
            {
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
            }
            else if (typeoflimb == 1)
            {
                r = 0.99f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
            }






             minx = 1;
             miny = 1;
             minz = 1;

             diagmaxx = 1;
             diagmaxy = 1;
             diagmaxz = 1;

             diagminx = 1;
             diagminy = 1;
             diagminz = 1;

             chunkwidthl = 1;
             chunkwidthr = 1;

             chunkheightl = 1;
             chunkheightr = 1;

             chunkdepthl = 1;
             chunkdepthr = 1;

            distance = 1.0f;


            if (grabtargetitem == 0)
            {



                /*
                _inst_p_r_hand_x = 2;
                _inst_p_r_hand_y = 1;
                _inst_p_r_hand_z = 1;
                */

                //PLAYER RIGHT HAND GRAB
                //voxel_type = 2;
                /*r = 0.19f;
                 g = 0.19f;
                 b = 0.19f;
                 a = 1;*/
                //instX = 1;
                //instY = 1;
                //instZ = 1;
                initialmatrix = Matrix.Identity;
                initialmatrix = WorldMatrix;
                initialmatrix.M41 = 0;
                initialmatrix.M42 = 0;// + -(_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
                initialmatrix.M43 = 0;
                initialmatrix.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                _mass = 100;
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;









                /*initialmatrix.M41 = 0;
                initialmatrix.M42 = -1;// + -(_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
                initialmatrix.M43 = 0;*/


                initialmatrix.M41 = 0;
                initialmatrix.M42 = 0;// + -(_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
                initialmatrix.M43 = -1;

                voxel_type = 2;

                minx = 1;
                miny = 1;
                minz = 1;

                diagmaxx = 1;
                diagmaxy = 1;
                diagmaxz = 1;

                diagminx = 1;
                diagminy = 1;
                diagminz = 1;

                chunkwidthl = 1;
                chunkwidthr = 1;

                chunkheightl = 1;
                chunkheightr = 1;

                chunkdepthl = 1;
                chunkdepthr = 1;
                distance = 35;


                /*if (somechunkpriminstanceikarmvoxelindex == 1 || somechunkpriminstanceikarmvoxelindex == 2)
                {
                    initialmatrix.M41 = 0;
                    initialmatrix.M42 = -1;// + -(_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
                    initialmatrix.M43 = 0;
                    voxel_type = 2;

                    minx = 1;
                    miny = 1;
                    minz = 1;

                    diagmaxx = 1;
                    diagmaxy = 1;
                    diagmaxz = 1;

                    diagminx = 1;
                    diagminy = 1;
                    diagminz = 1;

                    chunkwidthl = 1;
                    chunkwidthr = 1;

                    chunkheightl = 1;
                    chunkheightr = 1;

                    chunkdepthl = 1;
                    chunkdepthr = 1;
                    distance = 35;
                }
                else
                {
                    voxel_type = 3;
                }*/

                /*if (voxel_type == 0)
                {
                    vertoffsetz = -13 * 0.075f;
                }
                else
                {
                    vertoffsetz = -13;
                }*/
                //voxel_general_size =  voxeliknewsize;
                //voxel_type = 2; // 3 for pickaxe
                _type_of_cube = 2;
                is_static = true;

                //covid19/snowflake/mine init = 9, 9, 9, 9, 9, 9, 9, 9, 9, 60, 60, 60, 60, 60, 60,
                r = 0.99f;
                g = 0.19f;
                b = 0.99f;
                a = 1;


                //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr
                //3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 3, 65, 64, 40, 39


                distance = 10;
                _player_r_hand_grab[0][0] = new sc_voxel();
                _player_r_hand_grab[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightHandGrabTarget, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, voxel_general_size, new Vector3(0, 0, -0.1f), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                //_player_r_hand_grab[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerRightHandGrabTarget, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 3, 65, 64, 40, 39, voxel_general_size, new Vector3(0, 0, -0.1f), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_r_hand_grab[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_hand_grab[0][0].Length; i++)
                {
                    worldMatrix_instances_r_hand_grab[0][0][i] = _player_r_hand_grab[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
                }
            }












            //9, 9, 9, 18, 9, 9, 9, 9, 9, 4, 3, 13, 12, 18, 17
            /*if (somechunkpriminstanceikarmvoxelindex == 0)
            {

                minx = 7;
                miny = 7;
                minz = 7;

                diagmaxx = 7;
                diagmaxy = 7;
                diagmaxz = 7;

                diagminx = 7;
                diagminy = 7;
                diagminz = 7;

                chunkwidthl = 15;
                chunkwidthr = 14;

                chunkheightl = 15;
                chunkheightr = 14;

                chunkdepthl = 20;
                chunkdepthr = 19;
                distance = 25;

            }
            else if (somechunkpriminstanceikarmvoxelindex == 1)
            {
                minx = 7;
                miny = 7;
                minz = 7;

                diagmaxx = 7;
                diagmaxy = 7;
                diagmaxz = 7;

                diagminx = 7;
                diagminy = 7;
                diagminz = 7;

                chunkwidthl = 15;
                chunkwidthr = 14;

                chunkheightl = 15;
                chunkheightr = 14;

                chunkdepthl = 20;
                chunkdepthr = 19;
                distance = 25;
            }
            else if (somechunkpriminstanceikarmvoxelindex == 2)
            {
                minx = 7;
                miny = 7;
                minz = 7;

                diagmaxx = 7;
                diagmaxy = 7;
                diagmaxz = 7;

                diagminx = 7;
                diagminy = 7;
                diagminz = 7;

                chunkwidthl = 15;
                chunkwidthr = 14;

                chunkheightl = 15;
                chunkheightr = 14;

                chunkdepthl = 20;
                chunkdepthr = 19;
                distance = 25;
            }
            else if (somechunkpriminstanceikarmvoxelindex == 3)
            {

                minx = 7;
                miny = 7;
                minz = 7;

                diagmaxx = 7;
                diagmaxy = 7;
                diagmaxz = 7;

                diagminx = 7;
                diagminy = 7;
                diagminz = 7;

                chunkwidthl = 15;
                chunkwidthr = 14;

                chunkheightl = 15;
                chunkheightr = 14;

                chunkdepthl = 20;
                chunkdepthr = 19;
                distance = 25;

            }*/

            //7, 7, 7, 7, 7, 7, 7, 7, 7, 15, 14, 15, 14, 28, 27,
            minx = 7;
            miny = 7;
            minz = 7;

            diagmaxx = 7;
            diagmaxy = 7;
            diagmaxz = 7;

            diagminx = 7;
            diagminy = 7;
            diagminz = 7;

            chunkwidthl = 8;
            chunkwidthr = 7;

            chunkheightl = 8;
            chunkheightr = 7;

            chunkdepthl = 20;
            chunkdepthr = 19;
            distance = 25;



            if (typeoflimb == 0)
            {
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
            }
            else if (typeoflimb == 1)
            {
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
            }






            //PLAYER RIGHT HAND
            voxel_type = 2;
            /*r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;*/
            //instX = 1;
            //instY = 1;
            //instZ = 1;

            initialmatrix = Matrix.Identity;
            initialmatrix = WorldMatrix;

            /*initialmatrix.M41 = 0;
            initialmatrix.M42 = -0.65f;// - (_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
            initialmatrix.M43 = 0;
            */

            initialmatrix.M41 = 0;
            initialmatrix.M42 = 0.0f;// - (_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
            initialmatrix.M43 = 0.65f;

             if (somechunkpriminstanceikarmvoxelindex == 0 || somechunkpriminstanceikarmvoxelindex == 3)
            {
                initialmatrix = SharpDX.Matrix.RotationYawPitchRoll(0, sc_maths.DegreeToRadian(-90), 0);
                initialmatrix.M41 = 0;
                initialmatrix.M42 = -0.65f;// - (_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
                initialmatrix.M43 = 0;

                initialmatrix.M41 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);
                initialmatrix.M42 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M42 - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f * 0.05f);
                initialmatrix.M43 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M43;// - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);

            }
             else if (somechunkpriminstanceikarmvoxelindex == 1 || somechunkpriminstanceikarmvoxelindex == 2)
            {

                initialmatrix.M41 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M41;// - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);
                initialmatrix.M42 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M42 ;
                initialmatrix.M43 = _player_rght_lower_arm[0][0]._arrayOfInstances[0].current_pos.M43 - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f * 0.05f);// - (_player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);
            }


            initialmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            _mass = 100;
            vertoffsetx = 0;
            vertoffsety = 0;
            if (voxel_type == 0)
            {
                vertoffsetz = -18 * 0.075f;
            }
            else
            {
                vertoffsetz = -18;
            }
            _player_rght_hnd[0][0] = new sc_voxel();
            //voxel_general_size =  voxeliknewsize;

            voxel_general_size = voxeliknewsizealt;
            //voxel_type = 0;
            _type_of_cube = 2;
            is_static = true;


            //minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr,

            // sizelowerax, sizeloweray, sizeloweraz


            sizelowerax = (chunkwidthl + chunkwidthr) * voxeliknewsize;
            sizeloweray = (chunkheightl + chunkheightr) * voxeliknewsize;
            sizeloweraz = (chunkdepthl + chunkdepthr) * voxeliknewsize;


            //9, 9, 9, 18, 9, 9, 9, 9, 9, 4, 3, 13, 12, 18, 17
            _player_rght_hnd[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, sizelowerax, sizeloweray, sizeloweraz, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle,
                initialmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, sccs.scgraphics.scdirectx.BodyTag.PlayerHandRight,
                minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr, voxel_general_size,
                new Vector3(0, 0, -0.1f), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_hand[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_hand[0][0].Length; i++)
            {
                worldMatrix_instances_r_hand[0][0][i] = _player_rght_hnd[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }






            Vector3 posupperarm = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

            var somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);
            var dirShoulderToHand = somePosOfRightHand - posupperarm;



            var lengthOfLowerArmRight = _player_rght_lower_arm[0][0]._total_torso_depth * 1.0f;
            var lengthOfUpperArmRight = _player_rght_upper_arm[0][0]._total_torso_depth * 1.0f;
            var lengthOfHandRight = _player_rght_hnd[0][0]._total_torso_depth * 1.0f;
            var totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;



            Vector3 someshoulderpos = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[0].current_pos.M43);
            
            
            Quaternion somequatrot;
            var somerotationmatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[0].current_pos;
            Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
            direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
            direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
            direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);



            //somenewtargetlocation = someshoulderpos + (dirShoulderToHand * totalArmLengthRight * 2);
            somenewtargetlocation = someshoulderpos + (-direction_feet_forward_ori * lengthOfUpperArmRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 2);
            somenewtargetlocation = somenewtargetlocation + (-direction_feet_right_ori * lengthOfUpperArmRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 4);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_forward_ori * totalArmLengthRight * 2);
            Matrix somematrix = Matrix.Identity;
            somematrix.M41 = somenewtargetlocation.X;
            somematrix.M42 = somenewtargetlocation.Y;
            somematrix.M43 = somenewtargetlocation.Z;

            _player_rght_elbow_target_two[0][0]._arrayOfInstances[0].current_pos = somematrix;
            worldMatrix_instances_r_elbow_target_two[0][0][0] = somematrix;







            somenewtargetlocation = someshoulderpos + (dirShoulderToHand * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (-direction_feet_forward_ori * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 2);
            somenewtargetlocation = somenewtargetlocation + (-direction_feet_right_ori * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 4);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 2);
            //somenewtargetlocation = somenewtargetlocation + (direction_feet_forward_ori * totalArmLengthRight * 2);
            somematrix = Matrix.Identity;
            somematrix.M41 = somenewtargetlocation.X;
            somematrix.M42 = somenewtargetlocation.Y;
            somematrix.M43 = somenewtargetlocation.Z;

            _player_rght_elbow_target[0][0]._arrayOfInstances[0].current_pos = somematrix;
            worldMatrix_instances_r_elbow_target[0][0][0] = somematrix;




















            if (Program.usejitterphysics == 1)
            {
                for (int phys = 0; phys < Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez; phys++)
                {
                    for (int i = 0; i < Program.worldwidth * Program.worldheight * Program.worlddepth; i++)
                    {
                        object _some_dator = (object)_sc_jitter_tasks[phys][i]._world_data[0];
                        World _the_current_world = (World)_some_dator;

                        _the_current_world.AddBody(_player_rght_upper_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_lower_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_shldr[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                    }
                }
            }
















            _SC_modL_rght_shldr_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_rght_elbow_target_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };

            _SC_modL_rght_elbow_target_two_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_rght_upper_arm_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            }; _SC_modL_rght_lower_arm_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };


            _SC_modL_rght_hnd_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };




            _SC_modL_r_hand_grab_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };





            return _sc_jitter_tasks;
        }


        Vector3[] directionvectoroffsets;

        public scmessageobjectjitter[][] setiktargetnlimbspositionsNrotations(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix, sc_voxel mainarmparentmeshobject, Vector3 lightpos_, Vector3 dirLight_, Matrix finalRotationMatrix_, sc_voxel mainarmparentmeshobjectmain, sccsikvoxellimbs ikvoxelbody, int somechunkpriminstanceikarmvoxelindex, Matrix hmd_matrix_current, Matrix extramatrix, Vector3[] directionvectoroffsets_, Vector3 targetfootleft, Vector3 targetfootright, int typeoflimb)
        {




            directionvectoroffsets = directionvectoroffsets_;


            lightpos = lightpos_;
            dirLight = dirLight_;



            _SC_modL_r_hand_grab_BUFFER[0].lightPosition = lightpos;
            _SC_modL_r_hand_grab_BUFFER[0].lightDirection = dirLight;

            _SC_modL_rght_hnd_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_hnd_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_shldr_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_shldr_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_elbow_target_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_elbow_target_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_elbow_target_two_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_elbow_target_two_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_upper_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_upper_arm_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_lower_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_lower_arm_BUFFER[0].lightDirection = dirLight;





            Matrix finalRotationMatrix = finalRotationMatrix_;//originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;

            //finalRotationMatrix = finalRotationMatrix * hmdrotMatrix;

            //finalRotationMatrix = hmd_matrix_current * finalRotationMatrix;


            finalRotationMatrix.M41 = 0;
            finalRotationMatrix.M42 = 0;
            finalRotationMatrix.M43 = 0;

            ////////////////////
            /////HUMAN RIG////// 
            ////////////////////
            for (int _iterator = 0; _iterator < 1; _iterator++) // //_player_rght_hnd[0][0]._arrayOfInstances.Length
            {




                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                Matrix sometempmat2 = ikvoxelbody._player_head[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion somedirquat2;
                Quaternion.RotationMatrix(ref sometempmat2, out somedirquat2);
                var dirikvoxelbodyInstanceRight2 = -sc_maths._newgetdirleft(somedirquat2);
                var dirikvoxelbodyInstanceUp2 = sc_maths._newgetdirup(somedirquat2);
                var dirikvoxelbodyInstanceForward2 = sc_maths._newgetdirforward(somedirquat2);


                Vector3 tempOffset = OFFSETPOS;

                //int usethirdpersonview = 1;

                if (Program.usethirdpersonview == 0)
                {
                    tempOffset.X = scupdate.viewPosition.X;
                    tempOffset.Y = scupdate.viewPosition.Y;
                    tempOffset.Z = scupdate.viewPosition.Z;
                    /*
                    tempOffset.Y = tempOffset.Y + ikvoxelbody._player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT.M42;
                    tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (ikvoxelbody._player_head[0][0]._total_torso_height * 0.5f));*/
                }
                else if (Program.usethirdpersonview == 1)
                {
                    //OFFSETPOS.X = scupdate.viewPosition.X;
                    //OFFSETPOS.Y = scupdate.viewPosition.Y;
                    //OFFSETPOS.Z = scupdate.viewPosition.Z;

                    //OFFSETPOS = OFFSETPOS + (dirikvoxelbodyInstanceUp0 * -0.125f);
                    //scupdate.viewPosition = scupdate.viewPosition + (dirikvoxelbodyInstanceRight0 * -1.5f);

                    /*//tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    Quaternion quatt;
                    Quaternion.RotationMatrix(ref scupdate.tempmatter, out quatt);
                    // quatt.Invert();

                    //THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW
                    Vector3 forwardOVR = sc_maths._getDirection(Vector3.ForwardRH, quatt);
                    Vector3 upOVR = sc_maths._getDirection(Vector3.Up, quatt);
                    Vector3 rightOVR = sc_maths._getDirection(Vector3.Right, quatt);
                    upOVR.Normalize();
                    rightOVR.Normalize();
                    forwardOVR.Normalize();

                    forwardOVR *= -0.5f; // -1.0f

                    Vector3 thirdpersonview = OFFSETPOS + (-forwardOVR * 2.0f); //1.5f // + (upOVR * 0.25f)

                    OFFSETPOS.X = thirdpersonview.X;// scupdate.viewPosition.X;
                    OFFSETPOS.Y = thirdpersonview.Y;// scupdate.viewPosition.Y;
                    OFFSETPOS.Z = thirdpersonview.Z;// scupdate.viewPosition.Z;*/
                }



                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW


















                var lengthOfLowerArmRight = _player_rght_lower_arm[0][0]._total_torso_depth * 1.0f;
                var lengthOfUpperArmRight = _player_rght_upper_arm[0][0]._total_torso_depth * 1.0f;
                var lengthOfHandRight = _player_rght_hnd[0][0]._total_torso_depth * 1.0f;
                var totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                var connectorOfUpperArmRightOffsetMul = 1.0f; //1.55f
                var connectorOfLowerArmRightOffsetMul = 1.0f; //0.70f
                var connectorOfHandOffsetMul = 1.00123f; // 1.00123f

                var connectorOfUpperLegOffsetMul = 1.0f;
                var connectorOfLowerLegOffsetMul = 1.0f;

                //lightpos = new Vector3(0, 100, 0);
                ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
                diffuseColour = new Vector4(1, 1, 1, 1);
                lightDirection = new Vector3(0, -1, -1);
                dirLight = Vector3.Zero;
                lightpos = Vector3.Zero;
                Quaternion otherQuat;


                ///////////
                //SOMETESTS

                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                var direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var current_rotation_of_torso_pivot_forward = direction_feet_forward;
                var current_rotation_of_torso_pivot_right = direction_feet_right;
                var current_rotation_of_torso_pivot_up = direction_feet_up;
                //SOMETESTS
                ///////////




                Vector3 MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                Matrix somerotationmatrix = mainarmparentmeshobject._arrayOfInstances[_iterator]._ORIGINPOSITION;// _player_torso[0][0]._ORIGINPOSITION;
                Quaternion somequatrot;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);

                //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                var direction_feet_forward_ori_torso = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                var direction_feet_right_ori_torso = sc_maths._getDirection(Vector3.Right, somequatrot);
                var direction_feet_up_ori_torso = sc_maths._getDirection(Vector3.Up, somequatrot);

                //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (mainarmparentmeshobject._total_torso_height * 0.5f));



                //RIGHT SHOULDER
                somerotationmatrix = _player_rght_shldr[0][0]._ORIGINPOSITION;


                /*Quaternion.RotationMatrix(ref somerotationmatrix, out otherQuat);
                var direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);*/
                _SC_modL_rght_shldr_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_shldr[0][0]._POSITION.M41, _player_rght_shldr[0][0]._POSITION.M42, _player_rght_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                var torsooripos = MOVINGPOINTER;
                somerotationmatrix = ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos;// _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);
                var diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var tempPoint = MOVINGPOINTER;


                direction_feet_right.Normalize();
                tempPoint = tempPoint + (direction_feet_up * ikvoxelbody._player_torso[0][0]._total_torso_height);

                //tempPoint = tempPoint + -(direction_feet_right * ((ikvoxelbody._player_torso[0][0]._total_torso_width + (_player_rght_shldr[0][0]._total_torso_width*4)))); // (diffNormPosX)
                //tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));


                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    finalRotationMatrix.M41 = 0;
                    finalRotationMatrix.M42 = 0;
                    finalRotationMatrix.M43 = 0;

                    somerotationmatrix = finalRotationMatrix;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    extramatrix.M41 = 0;
                    extramatrix.M42 = 0;
                    extramatrix.M43 = 0;
                    somerotationmatrix = extramatrix * hmdrotMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    extramatrix.M41 = 0;
                    extramatrix.M42 = 0;
                    extramatrix.M43 = 0;
                    somerotationmatrix = extramatrix * hmdrotMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    finalRotationMatrix.M41 = 0;
                    finalRotationMatrix.M42 = 0;
                    finalRotationMatrix.M43 = 0;

                    somerotationmatrix = finalRotationMatrix;

                }





                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);

                var dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                realPosOfRS = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                var pivotOfHead = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));

                //realPosOfRS.X += OFFSETPOS.X;
                //realPosOfRS.Y += OFFSETPOS.Y;
                //realPosOfRS.Z += OFFSETPOS.Z;



                /*
                Vector3 shoulderposition0 = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfRightHand0 = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somedirshoulderpivottohand0 = somePosOfRightHand0 - shoulderposition0;
                var lengthOfDirFromPivotUpperToHand0 = somedirshoulderpivottohand0.Length();
                somedirshoulderpivottohand0.Normalize();
                var theshoulderrot = Matrix.LookAtRH(shoulderposition0, shoulderposition0 + somedirshoulderpivottohand0, direction_head_up);
                theshoulderrot.Invert();
                matrixerer = theshoulderrot;*/







                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    /*MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                     realPosOfRS = MOVINGPOINTER + (direction_feet_right_ori * (ikvoxelbody._player_pelvis[0][0]._total_torso_width* 0.5f));
                     realPosOfRS = realPosOfRS + (-direction_feet_up_ori * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                     //realPosOfRS = realPosOfRS + (direction_feet_up_ori * -((ikvoxelbody._player_torso[0][0]._total_torso_height*2) + (ikvoxelbody._player_pelvis[0][0]._total_torso_height)));
                     matrixerer  = extramatrix;// ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;  //matrixerer = extramatrix;
                     //matrixerer = Matrix.Identity;
                     matrixerer.M41 = realPosOfRS.X;
                     matrixerer.M42 = realPosOfRS.Y;
                     matrixerer.M43 = realPosOfRS.Z;*/


                    //MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    var torsooriginpos = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);



                    /*
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (ikvoxelbody._player_torso[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * 0.5f));
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 0.5f));
                    */

                    var originposshoulder = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);


                    diffNormPosX = (torsooriginpos.X) - originposshoulder.X;
                    diffNormPosY = (torsooriginpos.Y) - originposshoulder.Y;
                    diffNormPosZ = (torsooriginpos.Z) - originposshoulder.Z;

                    var shoulderMatrix = ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    var dirtorsof = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirtorsor = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirtorsou = sc_maths._getDirection(Vector3.Up, otherQuat);

                    var torsocurrentpos = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    var thetorsorotation = Matrix.LookAtRH(torsocurrentpos, torsocurrentpos + dirtorsof, dirtorsou);
                    thetorsorotation.Invert();



                    var posshoulder = torsocurrentpos + (dirtorsou * ikvoxelbody._player_torso[0][0]._total_torso_height * 0.5f);
                    posshoulder = posshoulder + (dirtorsor * diffNormPosX);



                    /*
                    var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                    theheadrotmatrix.Invert();*/


                    //Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    //var xqu = otherQuat.X;
                    //var yqu = otherQuat.Y;
                    //var zqu = otherQuat.Z;
                    //var wqu = otherQuat.W;

                    //var pitch0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var yaw0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var roll0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);

                    // var pitchdegree = (float)(pitch0 * (180.0f / Math.PI));// (float)((Math.PI * pitch0) / 180);

                    //if (pitchdegree < -90)
                    // {
                    //    pitchdegree = -90;
                    //}
                    //else if (pitchdegree > 90)
                    //{
                    //    pitchdegree = 90;
                    //}
                    //else
                    //{
                    //
                    //}

                    //pitchdegree = (float)((Math.PI * pitchdegree) / 180);

                    //theheadrotmatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw0, pitchdegree, roll0);
                    //var upperarmrot = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer = thetorsorotation;
                    //matrixerer = thetorsorotation;

                    //realPosOfRS += tempOffset;
                    realPosOfRS += OFFSETPOS;

                    //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                    //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                    matrixerer.M41 = posshoulder.X;
                    matrixerer.M42 = posshoulder.Y;
                    matrixerer.M43 = posshoulder.Z;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    /*MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    realPosOfRS = MOVINGPOINTER + (direction_feet_right_ori * (ikvoxelbody._player_pelvis[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    //realPosOfRS = realPosOfRS + (direction_feet_up_ori * -((ikvoxelbody._player_torso[0][0]._total_torso_height*2) + (ikvoxelbody._player_pelvis[0][0]._total_torso_height)));
                    matrixerer  = extramatrix;// ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;  //matrixerer = extramatrix;
                    //matrixerer = Matrix.Identity;
                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;*/


                    //MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    var torsooriginpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);



                    /*
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (ikvoxelbody._player_torso[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * 0.5f));
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 0.5f));
                    */

                    var originposshoulder = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);


                    diffNormPosX = (torsooriginpos.X) - originposshoulder.X;
                    diffNormPosY = (torsooriginpos.Y) - originposshoulder.Y;
                    diffNormPosZ = (torsooriginpos.Z) - originposshoulder.Z;

                    var shoulderMatrix = ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    var dirtorsof = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirtorsor = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirtorsou = sc_maths._getDirection(Vector3.Up, otherQuat);

                    var torsocurrentpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    var thetorsorotation = Matrix.LookAtRH(torsocurrentpos, torsocurrentpos + dirtorsof, dirtorsou);
                    thetorsorotation.Invert();



                    var posshoulder = torsocurrentpos + (-dirtorsou * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    posshoulder = posshoulder + (-dirtorsor * diffNormPosX);



                    /*
                    var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                    theheadrotmatrix.Invert();*/


                    //Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    //var xqu = otherQuat.X;
                    //var yqu = otherQuat.Y;
                    //var zqu = otherQuat.Z;
                    //var wqu = otherQuat.W;

                    //var pitch0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var yaw0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var roll0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);

                    // var pitchdegree = (float)(pitch0 * (180.0f / Math.PI));// (float)((Math.PI * pitch0) / 180);

                    //if (pitchdegree < -90)
                    // {
                    //    pitchdegree = -90;
                    //}
                    //else if (pitchdegree > 90)
                    //{
                    //    pitchdegree = 90;
                    //}
                    //else
                    //{
                    //
                    //}

                    //pitchdegree = (float)((Math.PI * pitchdegree) / 180);

                    //theheadrotmatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw0, pitchdegree, roll0);

                    //var upperarmrot = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;

                    //matrixerer = upperarmrot;


                    matrixerer = thetorsorotation;

                    //realPosOfRS += tempOffset;
                    realPosOfRS += OFFSETPOS;

                    //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                    //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                    matrixerer.M41 = posshoulder.X;
                    matrixerer.M42 = posshoulder.Y;
                    matrixerer.M43 = posshoulder.Z;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    /*MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    realPosOfRS = MOVINGPOINTER + (direction_feet_right_ori * (ikvoxelbody._player_pelvis[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    //realPosOfRS = realPosOfRS + (direction_feet_up_ori * -((ikvoxelbody._player_torso[0][0]._total_torso_height*2) + (ikvoxelbody._player_pelvis[0][0]._total_torso_height)));
                    matrixerer  = extramatrix;// ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;  //matrixerer = extramatrix;
                    //matrixerer = Matrix.Identity;
                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;*/


                    //MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    var torsooriginpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);



                    /*
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (ikvoxelbody._player_torso[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * 0.5f));
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 0.5f));
                    */

                    var originposshoulder = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);


                    diffNormPosX = (torsooriginpos.X) - originposshoulder.X;
                    diffNormPosY = (torsooriginpos.Y) - originposshoulder.Y;
                    diffNormPosZ = (torsooriginpos.Z) - originposshoulder.Z;

                    var shoulderMatrix = ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    var dirtorsof = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirtorsor = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirtorsou = sc_maths._getDirection(Vector3.Up, otherQuat);

                    var torsocurrentpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    var thetorsorotation = Matrix.LookAtRH(torsocurrentpos, torsocurrentpos + dirtorsof, dirtorsou);
                    thetorsorotation.Invert();



                    var posshoulder = torsocurrentpos + (-dirtorsou * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    posshoulder = posshoulder + (-dirtorsor * diffNormPosX);



                    /*
                    var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                    theheadrotmatrix.Invert();*/


                    //Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    //var xqu = otherQuat.X;
                    //var yqu = otherQuat.Y;
                    //var zqu = otherQuat.Z;
                    //var wqu = otherQuat.W;

                    //var pitch0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var yaw0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var roll0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);

                    // var pitchdegree = (float)(pitch0 * (180.0f / Math.PI));// (float)((Math.PI * pitch0) / 180);

                    //if (pitchdegree < -90)
                    // {
                    //    pitchdegree = -90;
                    //}
                    //else if (pitchdegree > 90)
                    //{
                    //    pitchdegree = 90;
                    //}
                    //else
                    //{
                    //
                    //}

                    //pitchdegree = (float)((Math.PI * pitchdegree) / 180);

                    //theheadrotmatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw0, pitchdegree, roll0);

                    matrixerer = thetorsorotation;

                    //realPosOfRS += tempOffset;
                    //realPosOfRS += OFFSETPOS;

                    //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                    //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                    matrixerer.M41 = posshoulder.X;
                    matrixerer.M42 = posshoulder.Y;
                    matrixerer.M43 = posshoulder.Z;




                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {

                    /*MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    realPosOfRS = MOVINGPOINTER + (direction_feet_right_ori * (ikvoxelbody._player_pelvis[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    //realPosOfRS = realPosOfRS + (direction_feet_up_ori * -((ikvoxelbody._player_torso[0][0]._total_torso_height*2) + (ikvoxelbody._player_pelvis[0][0]._total_torso_height)));
                    matrixerer  = extramatrix;// ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;  //matrixerer = extramatrix;
                    //matrixerer = Matrix.Identity;
                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;*/


                    //MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    var torsooriginpos = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);



                    /*
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (ikvoxelbody._player_torso[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * 0.5f));
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 0.5f));
                    */

                    var originposshoulder = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);


                    diffNormPosX = (torsooriginpos.X) - originposshoulder.X;
                    diffNormPosY = (torsooriginpos.Y) - originposshoulder.Y;
                    diffNormPosZ = (torsooriginpos.Z) - originposshoulder.Z;

                    var shoulderMatrix = ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    var dirtorsof = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirtorsor = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirtorsou = sc_maths._getDirection(Vector3.Up, otherQuat);

                    var torsocurrentpos = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    var thetorsorotation = Matrix.LookAtRH(torsocurrentpos, torsocurrentpos + dirtorsof, dirtorsou);
                    thetorsorotation.Invert();



                    var posshoulder = torsocurrentpos + (dirtorsou * ikvoxelbody._player_torso[0][0]._total_torso_height * 0.5f);
                    posshoulder = posshoulder + (dirtorsor * diffNormPosX);



                    /*
                    var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                    theheadrotmatrix.Invert();*/


                    //Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    //var xqu = otherQuat.X;
                    //var yqu = otherQuat.Y;
                    //var zqu = otherQuat.Z;
                    //var wqu = otherQuat.W;

                    //var pitch0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var yaw0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var roll0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);

                    // var pitchdegree = (float)(pitch0 * (180.0f / Math.PI));// (float)((Math.PI * pitch0) / 180);

                    //if (pitchdegree < -90)
                    // {
                    //    pitchdegree = -90;
                    //}
                    //else if (pitchdegree > 90)
                    //{
                    //    pitchdegree = 90;
                    //}
                    //else
                    //{
                    //
                    //}

                    //pitchdegree = (float)((Math.PI * pitchdegree) / 180);

                    //theheadrotmatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw0, pitchdegree, roll0);


                    matrixerer = thetorsorotation;

                    //realPosOfRS += tempOffset;
                    realPosOfRS += OFFSETPOS;

                    //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                    //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                    matrixerer.M41 = posshoulder.X;
                    matrixerer.M42 = posshoulder.Y;
                    matrixerer.M43 = posshoulder.Z;


                }






                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




















                //START OF RIGHT ARM IK
                //START OF RIGHT ARM IK
                //START OF RIGHT ARM IK
                /////////////////////
                //////HANDRIGHT//////

                Matrix someMatRight = mainarmparentmeshobject._arrayOfInstances[_iterator].current_pos;///_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos; //Matrix.Identity;

                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    someMatRight = _rightTouchMatrix;
                    someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                    someMatRight.M42 = handPoseRight.Position.Y + MOVINGPOINTER.Y;
                    someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
                    diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                    diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                    diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;

                    MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                    MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                    MOVINGPOINTER = MOVINGPOINTER + (mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));

                    //MOVINGPOINTER.X += OFFSETPOS.X;
                    //MOVINGPOINTER.Y += OFFSETPOS.Y;
                    //MOVINGPOINTER.Z += OFFSETPOS.Z;

                    //someMatRight.M41 += OFFSETPOS.X;
                    //someMatRight.M42 += OFFSETPOS.Y;
                    //someMatRight.M43 += OFFSETPOS.Z;

                    matrixerer = someMatRight * finalRotationMatrix;
                    matrixerer.M41 = MOVINGPOINTER.X;
                    matrixerer.M42 = MOVINGPOINTER.Y;
                    matrixerer.M43 = MOVINGPOINTER.Z;
                    matrixerer.M44 = 1;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    /*MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    realPosOfRS = MOVINGPOINTER + (direction_feet_right_ori * (ikvoxelbody._player_pelvis[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    //realPosOfRS = realPosOfRS + (direction_feet_up_ori * -((ikvoxelbody._player_torso[0][0]._total_torso_height*2) + (ikvoxelbody._player_pelvis[0][0]._total_torso_height)));
                    matrixerer  = extramatrix;// ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;  //matrixerer = extramatrix;
                    //matrixerer = Matrix.Identity;
                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;*/


                    //MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    var torsooriginpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);



                    /*
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (ikvoxelbody._player_torso[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * 0.5f));
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 0.5f));
                    */

                    var originposshoulder = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);


                    diffNormPosX = (torsooriginpos.X) - originposshoulder.X;
                    diffNormPosY = (torsooriginpos.Y) - originposshoulder.Y;
                    diffNormPosZ = (torsooriginpos.Z) - originposshoulder.Z;

                    var shoulderMatrix = ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    var dirtorsof = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirtorsor = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirtorsou = sc_maths._getDirection(Vector3.Up, otherQuat);

                    //var torsocurrentpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    var torsocurrentpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var thetorsorotation = Matrix.LookAtRH(torsocurrentpos, torsocurrentpos + dirtorsof, dirtorsou);
                    thetorsorotation.Invert();


                    var somelengthOfshoulder = _player_rght_shldr[0][0]._total_torso_height * 0.5f;
                    var somewidthofpelvis = ikvoxelbody._player_pelvis[0][0]._total_torso_width * 0.5f;

                    //var posshoulder = torsocurrentpos + (-dirtorsou * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    var posshoulder = torsocurrentpos + (-dirtorsou * lengthOfLowerArmRight * 0.75f);
                    posshoulder = posshoulder + (-dirtorsou * lengthOfUpperArmRight * 0.75f);
                    //posshoulder = posshoulder + (-dirtorsor * diffNormPosX);
                    posshoulder = posshoulder + (-dirtorsor * (somewidthofpelvis));

                    //Console.WriteLine(" " + somewidthofpelvis + " " + lengthOfLowerArmRight + " " + lengthOfUpperArmRight);


                    /*
                    var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                    theheadrotmatrix.Invert();*/


                    //Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    //var xqu = otherQuat.X;
                    //var yqu = otherQuat.Y;
                    //var zqu = otherQuat.Z;
                    //var wqu = otherQuat.W;

                    //var pitch0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var yaw0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var roll0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);

                    // var pitchdegree = (float)(pitch0 * (180.0f / Math.PI));// (float)((Math.PI * pitch0) / 180);

                    //if (pitchdegree < -90)
                    // {
                    //    pitchdegree = -90;
                    //}
                    //else if (pitchdegree > 90)
                    //{
                    //    pitchdegree = 90;
                    //}
                    //else
                    //{
                    //
                    //}

                    //pitchdegree = (float)((Math.PI * pitchdegree) / 180);

                    //theheadrotmatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw0, pitchdegree, roll0);

                    matrixerer = thetorsorotation;

                    //realPosOfRS += tempOffset;
                    posshoulder += OFFSETPOS;

                    //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                    //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                    matrixerer.M41 = posshoulder.X;
                    matrixerer.M42 = posshoulder.Y;
                    matrixerer.M43 = posshoulder.Z;
                    matrixerer.M44 = 1.0f;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    /*MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    realPosOfRS = MOVINGPOINTER + (direction_feet_right_ori * (ikvoxelbody._player_pelvis[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    //realPosOfRS = realPosOfRS + (direction_feet_up_ori * -((ikvoxelbody._player_torso[0][0]._total_torso_height*2) + (ikvoxelbody._player_pelvis[0][0]._total_torso_height)));
                    matrixerer  = extramatrix;// ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;  //matrixerer = extramatrix;
                    //matrixerer = Matrix.Identity;
                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;*/


                    //MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    var torsooriginpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);



                    /*
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (ikvoxelbody._player_torso[0][0]._total_torso_width* 0.5f));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * 0.5f));
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 0.5f));
                    */

                    var originposshoulder = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);


                    diffNormPosX = (torsooriginpos.X) - originposshoulder.X;
                    diffNormPosY = (torsooriginpos.Y) - originposshoulder.Y;
                    diffNormPosZ = (torsooriginpos.Z) - originposshoulder.Z;

                    var shoulderMatrix = ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    var dirtorsof = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirtorsor = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirtorsou = sc_maths._getDirection(Vector3.Up, otherQuat);

                    //var torsocurrentpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    var torsocurrentpos = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var thetorsorotation = Matrix.LookAtRH(torsocurrentpos, torsocurrentpos + dirtorsof, dirtorsou);
                    thetorsorotation.Invert();


                    var somelengthOfshoulder = _player_rght_shldr[0][0]._total_torso_height * 0.5f;
                    var somewidthofpelvis = ikvoxelbody._player_pelvis[0][0]._total_torso_width * 0.5f;

                    //var posshoulder = torsocurrentpos + (-dirtorsou * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 0.5f);
                    var posshoulder = torsocurrentpos + (-dirtorsou * lengthOfLowerArmRight * 0.75f);
                    posshoulder = posshoulder + (-dirtorsou * lengthOfUpperArmRight * 0.75f);
                    //posshoulder = posshoulder + (-dirtorsor * diffNormPosX);
                    posshoulder = posshoulder + (dirtorsor * (somewidthofpelvis));

                    //Console.WriteLine(" " + somewidthofpelvis + " " + lengthOfLowerArmRight + " " + lengthOfUpperArmRight);


                    /*
                    var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                    theheadrotmatrix.Invert();*/


                    //Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    //var xqu = otherQuat.X;
                    //var yqu = otherQuat.Y;
                    //var zqu = otherQuat.Z;
                    //var wqu = otherQuat.W;

                    //var pitch0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var yaw0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    //var roll0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);

                    // var pitchdegree = (float)(pitch0 * (180.0f / Math.PI));// (float)((Math.PI * pitch0) / 180);

                    //if (pitchdegree < -90)
                    // {
                    //    pitchdegree = -90;
                    //}
                    //else if (pitchdegree > 90)
                    //{
                    //    pitchdegree = 90;
                    //}
                    //else
                    //{
                    //
                    //}

                    //pitchdegree = (float)((Math.PI * pitchdegree) / 180);

                    //theheadrotmatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw0, pitchdegree, roll0);

                    matrixerer = thetorsorotation;

                    //realPosOfRS += tempOffset;
                    posshoulder += OFFSETPOS;

                    //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                    //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                    matrixerer.M41 = posshoulder.X;
                    matrixerer.M42 = posshoulder.Y;
                    matrixerer.M43 = posshoulder.Z;
                    matrixerer.M44 = 1.0f;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {

                    MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    someMatRight = _leftTouchMatrix;
                    someMatRight.M41 = handPoseLeft.Position.X + MOVINGPOINTER.X;
                    someMatRight.M42 = handPoseLeft.Position.Y + MOVINGPOINTER.Y;
                    someMatRight.M43 = handPoseLeft.Position.Z + MOVINGPOINTER.Z;
                    
                    
                    diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                    diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                    diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;

                    MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                    MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                    MOVINGPOINTER = MOVINGPOINTER + (mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));

                    //MOVINGPOINTER.X += OFFSETPOS.X;
                    //MOVINGPOINTER.Y += OFFSETPOS.Y;
                    //MOVINGPOINTER.Z += OFFSETPOS.Z;


                    matrixerer = someMatRight * finalRotationMatrix;
                    matrixerer.M41 = MOVINGPOINTER.X;
                    matrixerer.M42 = MOVINGPOINTER.Y;
                    matrixerer.M43 = MOVINGPOINTER.Z;
                    matrixerer.M44 = 1;
                }



                //Vector3 somegrabposition = MOVINGPOINTER + (mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ * 0.5f));


                var posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                Vector3 tempDir = posRHand - somePosOfSHLDR;



                if (somechunkpriminstanceikarmvoxelindex == 0)//right hand
                {
                    if (tempDir.Length() > totalArmLengthRight * 1.00923f)
                    {
                        //Console.WriteLine("> tempDir.Length " + somechunkpriminstanceikarmvoxelindex);
                        //Program.MessageBox((IntPtr)0, "" + tempDir.Length(), "sc core systems message", 0);
                        tempDir.Normalize();
                        Vector3 tempVect = somePosOfSHLDR + (tempDir * (totalArmLengthRight * 1.00923f));
                        MOVINGPOINTER.X = tempVect.X;
                        MOVINGPOINTER.Y = tempVect.Y;
                        MOVINGPOINTER.Z = tempVect.Z;

                        matrixerer.M41 = MOVINGPOINTER.X;
                        matrixerer.M42 = MOVINGPOINTER.Y;
                        matrixerer.M43 = MOVINGPOINTER.Z;
                        matrixerer.M44 = 1;
                    }
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)//lefthand
                {
                    if (tempDir.Length() > totalArmLengthRight * 1.00923f)
                    {
                        //Console.WriteLine("> tempDir.Length " + somechunkpriminstanceikarmvoxelindex);
                        //Program.MessageBox((IntPtr)0, "" + tempDir.Length(), "sc core systems message", 0);
                        tempDir.Normalize();
                        Vector3 tempVect = somePosOfSHLDR + (tempDir * (totalArmLengthRight * 1.00923f));
                        MOVINGPOINTER.X = tempVect.X;
                        MOVINGPOINTER.Y = tempVect.Y;
                        MOVINGPOINTER.Z = tempVect.Z;

                        matrixerer.M41 = MOVINGPOINTER.X;
                        matrixerer.M42 = MOVINGPOINTER.Y;
                        matrixerer.M43 = MOVINGPOINTER.Z;
                        matrixerer.M44 = 1;
                    }



                }



                /*
                if (somechunkpriminstanceikarmvoxelindex == 0)//right hand
                {


                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)//lefthand
                {

                }*/





                /*
                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    matrixerer = someMatRight * finalRotationMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    matrixerer = someMatRight;//finalRotationMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    matrixerer = someMatRight;//finalRotationMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    matrixerer = someMatRight * finalRotationMatrix;
                }*/

                /*someMatRight.M41 += OFFSETPOS.X;
                someMatRight.M42 += OFFSETPOS.Y;
                someMatRight.M43 += OFFSETPOS.Z;*/

                /*matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;*/

                //Vector3 realposhand0 = MOVINGPOINTER;

                worldMatrix_instances_r_hand[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                //_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;
                //_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatRight;
                //_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION = someMatRight;
                //if (swtch_for_last_pos[0][0][_iterator] > 0)
                //{
                //    _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITIONFORPHYSICS = matrixerer;
                //}




















                //Console.WriteLine("x:" + _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41 + " y:" + _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42 + " z:" + _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                
                Vector3 shoulderposition = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                somerotationmatrix = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);

                //shoulderposition = shoulderposition + (direction_feet_up_ori * _player_rght_shldr[0][0]._total_torso_height);


                shoulderposition = shoulderposition + (-direction_feet_up_ori * _player_rght_shldr[0][0]._total_torso_height * 0.5f);


                var somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetTwo = new Vector3(_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetOne = new Vector3(_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                //var someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;
                //var someDirFromElbowTargetOneToRghtHand = somePosOfRightHand - somePosOfUpperElbowTargetOne;
                //var someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;


                var dirshoulderpivottohand = somePosOfRightHand - shoulderposition;
                var lengthOfDirFromPivotUpperToHand = dirshoulderpivottohand.Length();
                dirshoulderpivottohand.Normalize();

                var dirshouldertotargetone =  somePosOfUpperElbowTargetOne - shoulderposition;
                dirshouldertotargetone.Normalize();


                lengthOfLowerArmRight = _player_rght_lower_arm[0][0]._total_torso_depth;
                lengthOfUpperArmRight = _player_rght_upper_arm[0][0]._total_torso_depth;
                totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthRight - totalArmLengthRight * 0.001f);
                var upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                var adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);

                //c2 = a2+b2
                float hypothenuseforupperarmsquared = (lengthOfUpperArmRight * lengthOfUpperArmRight) - (adjacentSolvingForX * adjacentSolvingForX);

                var oppositeSolvingForHalfA = (float)Math.Sqrt(hypothenuseforupperarmsquared);
                oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);

                


                Vector3 elbowpositionrighttowardshandsolvingforxpoint = shoulderposition + (dirshoulderpivottohand * adjacentSolvingForX);

                Vector3 direlbowtoshoulder = elbowpositionrighttowardshandsolvingforxpoint - shoulderposition;
                direlbowtoshoulder.Normalize();

                Vector3 direlbowreferencetotargettwo = somePosOfUpperElbowTargetTwo - elbowpositionrighttowardshandsolvingforxpoint;
                direlbowreferencetotargettwo.Normalize();


                //somerotationmatrix = [0][0]._arrayOfInstances[_iterator].current_pos;

                Matrix elbowtargetonedirmatrix = _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref elbowtargetonedirmatrix, out somequatrot);
                var direlbowtargetonef = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                var direlbowtargetoner = sc_maths._getDirection(Vector3.Right, somequatrot);
                var direlbowtargetoneu = sc_maths._getDirection(Vector3.Up, somequatrot);


                var targettwotoshoulder = shoulderposition - somePosOfUpperElbowTargetTwo;
                var targetonetoshoulder = shoulderposition - somePosOfUpperElbowTargetOne;
                Vector3 dirrightforelbowcross;
                Vector3.Cross(ref targettwotoshoulder, ref targetonetoshoulder, out dirrightforelbowcross);
                dirrightforelbowcross.Normalize();



                //Vector3 upvectorreferencetocreateothervec = Vector3.Up;// direlbowtargetoneu;// Vector3.Up;

                //upvectorreferencetocreateothervec.X = elbowpositionrighttowardshandsolvingforxpoint.X;
                //upvectorreferencetocreateothervec.Z = elbowpositionrighttowardshandsolvingforxpoint.Z;

                //if (upvectorreferencetocreateothervec.X == dirshoulderpivottohand.X && upvectorreferencetocreateothervec.Y == dirshoulderpivottohand.Y && upvectorreferencetocreateothervec.Z == dirshoulderpivottohand.Z )
                //{
                //    upvectorreferencetocreateothervec = Vector3.Right;
                //}

                //Vector3 crossforpointofreferencefortargettwodirright;
                //Vector3.Cross(ref upvectorreferencetocreateothervec, ref dirshoulderpivottohand, out crossforpointofreferencefortargettwodirright);
                //crossforpointofreferencefortargettwodirright.Normalize();

                Vector3 targettwopointA = elbowpositionrighttowardshandsolvingforxpoint + (dirrightforelbowcross);

                Vector3 shouldertopointadir = targettwopointA - shoulderposition;
                shouldertopointadir.Normalize();

                Vector3 crosstogetupvec;
                Vector3.Cross(ref shouldertopointadir, ref dirshoulderpivottohand, out crosstogetupvec);
                crosstogetupvec.Normalize();

                Vector3 elbowposition = elbowpositionrighttowardshandsolvingforxpoint + (crosstogetupvec * oppositeSolvingForHalfA);


                Vector3 dirshoulderelbow = elbowposition - shoulderposition;
                dirshoulderelbow.Normalize();

                Vector3 realmiddlepivotupperarm = shoulderposition + (dirshoulderelbow * lengthOfUpperArmRight * 0.5f);    
                //realmiddlepivotupperarm = realmiddlepivotupperarm + (-dirshoulderelbow * _player_rght_shldr[0][0]._total_torso_depth * 0.5f);

                //Vector3 fakeoriginpointlowerarmduetowrongsizesofabove = realmiddlepivotupperarm + (dirshoulderelbow * _player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
                Vector3 fakeelbowtohanddir = somePosOfRightHand - elbowposition;
                fakeelbowtohanddir.Normalize();

                Vector3 currentposlowerarm = elbowposition + (fakeelbowtohanddir * _player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);// + (dirshoulderelbow * _player_rght_shldr[0][0]._total_torso_depth * 0.5f);
                

                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = dirrightforelbowcross;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowposition;// fakeoriginpointlowerarmduetowrongsizesofabove;// elbowposition;

                Vector3 otherpointrightofrealelbow = elbowposition + (dirrightforelbowcross);

                Vector3 dirshouldertootherpointrightofelbow = otherpointrightofrealelbow - shoulderposition;
                dirshouldertootherpointrightofelbow.Normalize();

                Vector3 crosstogetupvectorofupperarm;
                Vector3.Cross(ref dirshouldertootherpointrightofelbow, ref dirshoulderelbow, out crosstogetupvectorofupperarm);
                crosstogetupvectorofupperarm.Normalize();

                var shoulderRotationMatrixRight = Matrix.LookAtRH(shoulderposition, shoulderposition + dirshoulderelbow, crosstogetupvectorofupperarm);
                shoulderRotationMatrixRight.Invert();
                
            
                matrixerer = shoulderRotationMatrixRight;
                matrixerer.M41 = realmiddlepivotupperarm.X;
                matrixerer.M42 = realmiddlepivotupperarm.Y;
                matrixerer.M43 = realmiddlepivotupperarm.Z;
                matrixerer.M44 = 1;

                worldMatrix_instances_r_upperarm[0][0][_iterator] = matrixerer;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;



                matrixerer = Matrix.Identity;// _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                matrixerer.M41 = elbowposition.X;
                matrixerer.M42 = elbowposition.Y;
                matrixerer.M43 = elbowposition.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_elbow_target_three[0][0][_iterator] = matrixerer;
                _player_rght_elbow_target_three[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;



                








                /*
                //////////////////
                //UPPER ARM RIGHT
                Vector3 shoulderposition = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;

                somerotationmatrix = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;

                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);

                var test = MOVINGPOINTER + OFFSETPOS;
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                diffNormPosX = (test.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                diffNormPosY = (test.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                diffNormPosZ = (test.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43;
                var realPIVOTOfUpperArm = MOVINGPOINTER;
                var realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * connectorOfUpperArmRightOffsetMul));

                realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;
                Vector3 currentFINALPIVOTUPPERARM = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43) + (direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * connectorOfUpperArmRightOffsetMul));// realPIVOTOfUpperArm;
                realPIVOTOfUpperArm = currentFINALPIVOTUPPERARM;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT = currentFINALPIVOTUPPERARM;

                //WAYPOINT
                var somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetTwo = new Vector3(_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetOne = new Vector3(_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;
                var someDirFromElbowTargetOneToRghtHand = somePosOfRightHand - somePosOfUpperElbowTargetOne;
                var targettwotoshoulder = shoulderposition - somePosOfUpperElbowTargetTwo;
                var targetonetoshoulder = shoulderposition - somePosOfUpperElbowTargetOne;


                Vector3 crossRes;
                Vector3.Cross(ref targettwotoshoulder, ref targetonetoshoulder, out crossRes);
                crossRes.Normalize();

                var targettwopointA = realPIVOTOfUpperArm + (-crossRes);

                var dirshoulderpivottohand = somePosOfRightHand - shoulderposition;
                var lengthOfDirFromPivotUpperToHand = dirshoulderpivottohand.Length();
                dirshoulderpivottohand.Normalize();

                var upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                var adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);
                adjacentSolvingForX = Math.Min(adjacentSolvingForX, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);
                var resulter = Math.Pow(lengthOfUpperArmRight, 2) - Math.Pow(adjacentSolvingForX, 2);
                if (resulter < 0)
                {
                    resulter *= -1;
                }
                var oppositeSolvingForHalfA = (float)Math.Sqrt(resulter);
                oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);
                if (oppositeSolvingForHalfA < 0)
                {
                    oppositeSolvingForHalfA *= -1;
                }

                Vector3 crossforpointofreferencefortargettwodirright;
                Vector3.Cross(ref crossRes, ref dirshoulderpivottohand, out crossforpointofreferencefortargettwodirright);
                crossforpointofreferencefortargettwodirright.Normalize();

                Vector3 elbowpositionrighttowardshandsolvingforxpoint = shoulderposition + (dirshoulderpivottohand * adjacentSolvingForX);

                Vector3 shouldertopointadir = targettwopointA - shoulderposition;
                shouldertopointadir.Normalize();

                Vector3 crosstogetupvec;
                Vector3.Cross(ref shouldertopointadir, ref dirshoulderpivottohand, out crosstogetupvec);
                crosstogetupvec.Normalize();

                Vector3 elbowposition = elbowpositionrighttowardshandsolvingforxpoint + (crosstogetupvec * oppositeSolvingForHalfA);


                Vector3 dirshoulderelbow = elbowposition - shoulderposition;
                dirshoulderelbow.Normalize();

                Vector3 realmiddlepivotupperarm = shoulderposition + (dirshoulderelbow * lengthOfUpperArmRight * 0.5f);
                //realmiddlepivotupperarm = realmiddlepivotupperarm + (-dirshoulderelbow * _player_rght_shldr[0][0]._total_torso_depth * 0.5f);

                //Vector3 fakeoriginpointlowerarmduetowrongsizesofabove = realmiddlepivotupperarm + (dirshoulderelbow * _player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
                Vector3 fakeelbowtohanddir = somePosOfRightHand - elbowposition;
                fakeelbowtohanddir.Normalize();

                Vector3 currentposlowerarm = elbowposition;// + (dirshoulderelbow * _player_rght_shldr[0][0]._total_torso_depth * 0.5f);
                currentposlowerarm = currentposlowerarm + (fakeelbowtohanddir * _player_rght_lower_arm[0][0]._total_torso_depth * 0.5f);

                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = crossforpointofreferencefortargettwodirright;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowposition;// fakeoriginpointlowerarmduetowrongsizesofabove;// elbowposition;

                Vector3 otherpointrightofrealelbow = elbowposition + (crossforpointofreferencefortargettwodirright);

                Vector3 dirshouldertootherpointrightofelbow = otherpointrightofrealelbow - shoulderposition;
                dirshouldertootherpointrightofelbow.Normalize();

                Vector3 crosstogetupvectorofupperarm;
                Vector3.Cross(ref dirshouldertootherpointrightofelbow, ref dirshoulderelbow, out crosstogetupvectorofupperarm);
                crosstogetupvectorofupperarm.Normalize();

                var shoulderRotationMatrixRight = Matrix.LookAtRH(shoulderposition, shoulderposition + dirshoulderelbow, crosstogetupvectorofupperarm);
                shoulderRotationMatrixRight.Invert();


                matrixerer = shoulderRotationMatrixRight;
                matrixerer.M41 = realmiddlepivotupperarm.X;
                matrixerer.M42 = realmiddlepivotupperarm.Y;
                matrixerer.M43 = realmiddlepivotupperarm.Z;
                matrixerer.M44 = 1;

                worldMatrix_instances_r_upperarm[0][0][_iterator] = matrixerer;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                */
















                //var rotationshouldertohand = Matrix.LookAtRH(shoulderposition, shoulderposition + dirshoulderpivottohand, crosstogetupvec);
                //rotationshouldertohand.Invert();

                //somerotationmatrix = [0][0]._arrayOfInstances[_iterator].current_pos;
                /*Quaternion.RotationMatrix(ref rotationshouldertohand, out somequatrot);
                var dirshouldertohandf = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                var dirshouldertohandr = sc_maths._getDirection(Vector3.Right, somequatrot);
                var dirshouldertohandu = sc_maths._getDirection(Vector3.Up, somequatrot);*/

                var dirshouldertohand = somePosOfRightHand - shoulderposition;

                if (somechunkpriminstanceikarmvoxelindex == 0)//right hand
                {
                    //dirshoulderpivottohand.X *= -1;
                    Vector3 elbowtargetonepos = shoulderposition - (dirshouldertohand * 0.25f);
                    elbowtargetonepos = elbowtargetonepos - (direction_feet_forward_ori * 0.5f);
                    /*Vector3 upvecmod = Vector3.Up;
                    upvecmod.X = elbowtargetonepos.X;
                    upvecmod.Y = upvecmod.Y * 0.25f;
                    upvecmod.Z = elbowtargetonepos.Z;
                    //upvecmod *= crosstogetupvec;

                    elbowtargetonepos = elbowtargetonepos;// + (upvecmod);
                    */


                    //elbowtargetonepos = elbowtargetonepos - (dirshouldertohandr * 0.15f);
                    //elbowtargetonepos = elbowtargetonepos - (dirshouldertohandu * (lengthOfHandRight + lengthOfLowerArmRight + lengthOfUpperArmRight));
                    //_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;

                    matrixerer = Matrix.Identity;// _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer.M41 = elbowtargetonepos.X;
                    matrixerer.M42 = elbowtargetonepos.Y;
                    matrixerer.M43 = elbowtargetonepos.Z;
                    matrixerer.M44 = 1;
                    worldMatrix_instances_r_elbow_target[0][0][_iterator] = matrixerer;
                    _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;


                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {

                    Vector3 elbowtargetonepos = shoulderposition - (dirshouldertohand * 0.25f);

                    /*Vector3 upvecmod = Vector3.ForwardRH;
                    upvecmod.X = elbowtargetonepos.X;
                    upvecmod.Y = elbowtargetonepos.Y;
                    upvecmod.Z = upvecmod.Z * 0.25f;

                    elbowtargetonepos = elbowtargetonepos + (upvecmod);
                    */


                    //elbowtargetonepos = elbowtargetonepos - (dirshouldertohandr * 0.15f);
                    //elbowtargetonepos = elbowtargetonepos - (dirshouldertohandu * (lengthOfHandRight + lengthOfLowerArmRight + lengthOfUpperArmRight));
                    //_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;

                    matrixerer = Matrix.Identity;// _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer.M41 = elbowtargetonepos.X;
                    matrixerer.M42 = elbowtargetonepos.Y;
                    matrixerer.M43 = elbowtargetonepos.Z;
                    matrixerer.M44 = 1;
                    worldMatrix_instances_r_elbow_target[0][0][_iterator] = matrixerer;
                    _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    Vector3 elbowtargetonepos = shoulderposition - (dirshouldertohand * 0.25f);

                    /*Vector3 upvecmod = Vector3.ForwardRH;
                    upvecmod.X = elbowtargetonepos.X;
                    upvecmod.Y = elbowtargetonepos.Y;
                    upvecmod.Z = upvecmod.Z * 0.25f;

                    elbowtargetonepos = elbowtargetonepos;// + (upvecmod);
                    */


                    //elbowtargetonepos = elbowtargetonepos - (dirshouldertohandr * 0.15f);
                    //elbowtargetonepos = elbowtargetonepos - (dirshouldertohandu * (lengthOfHandRight + lengthOfLowerArmRight + lengthOfUpperArmRight));
                    //_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;

                    matrixerer = Matrix.Identity;// _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer.M41 = elbowtargetonepos.X;
                    matrixerer.M42 = elbowtargetonepos.Y;
                    matrixerer.M43 = elbowtargetonepos.Z;
                    matrixerer.M44 = 1;
                    worldMatrix_instances_r_elbow_target[0][0][_iterator] = matrixerer;
                    _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)//lefthand
                {
                    //dirshoulderpivottohand.X *= -1;
                    Vector3 elbowtargetonepos = shoulderposition - (dirshouldertohand * 0.25f);
                    elbowtargetonepos = elbowtargetonepos - (direction_feet_forward_ori * 0.5f);

                    /*Vector3 upvecmod = Vector3.Up;
                    upvecmod.X = elbowtargetonepos.X;
                    upvecmod.Y = upvecmod.Y * 0.25f;
                    upvecmod.Z = elbowtargetonepos.Z;
                    //upvecmod *= crosstogetupvec;

                    elbowtargetonepos = elbowtargetonepos;// + (upvecmod);
                    */

                    //elbowtargetonepos = elbowtargetonepos - (dirshouldertohandr * 0.15f);
                    //elbowtargetonepos = elbowtargetonepos - (dirshouldertohandu * (lengthOfHandRight + lengthOfLowerArmRight + lengthOfUpperArmRight));
                    //_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;

                    matrixerer = Matrix.Identity;// _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer.M41 = elbowtargetonepos.X;
                    matrixerer.M42 = elbowtargetonepos.Y;
                    matrixerer.M43 = elbowtargetonepos.Z;
                    matrixerer.M44 = 1;
                    worldMatrix_instances_r_elbow_target[0][0][_iterator] = matrixerer;
                    _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;


                }







                if (somechunkpriminstanceikarmvoxelindex == 0)//right hand
                {

                    var elbowtargetonepos1 = somePosOfRightHand - (direction_feet_up_ori * 0.25f);


                    //crosstogetupvectorofupperarm *= -1.5f;

                    //Vector3 moreupdirtoelbow = elbowtargetonepos1 + (-crosstogetupvectorofupperarm);

                    //otherpointrightofrealelbow.X += OFFSETPOS.X;
                    //otherpointrightofrealelbow.Y += OFFSETPOS.Y;
                    //otherpointrightofrealelbow.Z += OFFSETPOS.Z;

                    matrixerer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer.M41 = elbowtargetonepos1.X;// moreupdirtoelbow.X;
                    matrixerer.M42 = elbowtargetonepos1.Y;// moreupdirtoelbow.Y;
                    matrixerer.M43 = elbowtargetonepos1.Z;// moreupdirtoelbow.Z;
                    matrixerer.M44 = 1;
                    worldMatrix_instances_r_elbow_target_two[0][0][_iterator] = matrixerer;
                    _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                }
                else  if (somechunkpriminstanceikarmvoxelindex == 1)//right hand
                {
                    var elbowtargetonepos1 = somePosOfRightHand + (direction_feet_forward_ori * 0.25f);


                    //crosstogetupvectorofupperarm *= -1.5f;

                    //Vector3 moreupdirtoelbow = elbowtargetonepos1 + (-crosstogetupvectorofupperarm);

                    //otherpointrightofrealelbow.X += OFFSETPOS.X;
                    //otherpointrightofrealelbow.Y += OFFSETPOS.Y;
                    //otherpointrightofrealelbow.Z += OFFSETPOS.Z;

                    matrixerer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer.M41 = elbowtargetonepos1.X;// moreupdirtoelbow.X;
                    matrixerer.M42 = elbowtargetonepos1.Y;// moreupdirtoelbow.Y;
                    matrixerer.M43 = elbowtargetonepos1.Z;// moreupdirtoelbow.Z;
                    matrixerer.M44 = 1;
                    worldMatrix_instances_r_elbow_target_two[0][0][_iterator] = matrixerer;
                    _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)//right hand
                {
                    var elbowtargetonepos1 = somePosOfRightHand + (direction_feet_forward_ori * 0.25f);


                    //crosstogetupvectorofupperarm *= -1.5f;

                    //Vector3 moreupdirtoelbow = elbowtargetonepos1 + (-crosstogetupvectorofupperarm);

                    //otherpointrightofrealelbow.X += OFFSETPOS.X;
                    //otherpointrightofrealelbow.Y += OFFSETPOS.Y;
                    //otherpointrightofrealelbow.Z += OFFSETPOS.Z;

                    matrixerer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer.M41 = elbowtargetonepos1.X;// moreupdirtoelbow.X;
                    matrixerer.M42 = elbowtargetonepos1.Y;// moreupdirtoelbow.Y;
                    matrixerer.M43 = elbowtargetonepos1.Z;// moreupdirtoelbow.Z;
                    matrixerer.M44 = 1;
                    worldMatrix_instances_r_elbow_target_two[0][0][_iterator] = matrixerer;
                    _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)//right hand
                {
                    var elbowtargetonepos1 = somePosOfRightHand - (direction_feet_up_ori * 0.25f);


                    //crosstogetupvectorofupperarm *= -1.5f;

                    //Vector3 moreupdirtoelbow = elbowtargetonepos1 + (-crosstogetupvectorofupperarm);

                    //otherpointrightofrealelbow.X += OFFSETPOS.X;
                    //otherpointrightofrealelbow.Y += OFFSETPOS.Y;
                    //otherpointrightofrealelbow.Z += OFFSETPOS.Z;

                    matrixerer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    matrixerer.M41 = elbowtargetonepos1.X;// moreupdirtoelbow.X;
                    matrixerer.M42 = elbowtargetonepos1.Y;// moreupdirtoelbow.Y;
                    matrixerer.M43 = elbowtargetonepos1.Z;// moreupdirtoelbow.Z;
                    matrixerer.M44 = 1;
                    worldMatrix_instances_r_elbow_target_two[0][0][_iterator] = matrixerer;
                    _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                }







                Vector3 positionhand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                Vector3 dirhandtoelbow = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION - positionhand;
                dirhandtoelbow.Normalize();

                Vector3 pivotlower = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                //Vector3 crossvecrefelbow = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;
                Vector3 direlbowshoulder = realmiddlepivotupperarm - pivotlower;
                direlbowshoulder.Normalize();


                Vector3 vectorupdirlower;
                Vector3.Cross(ref dirhandtoelbow, ref direlbowshoulder, out vectorupdirlower);
                vectorupdirlower.Normalize();

                Matrix theLowerArmRotationMatrix = Matrix.LookAtRH(currentposlowerarm, currentposlowerarm + dirhandtoelbow, vectorupdirlower);
                theLowerArmRotationMatrix.Invert();

                matrixerer = theLowerArmRotationMatrix; //_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;//  
                matrixerer.M41 = currentposlowerarm.X;
                matrixerer.M42 = currentposlowerarm.Y;
                matrixerer.M43 = currentposlowerarm.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_lowerarm[0][0][_iterator] = matrixerer;
                _player_rght_lower_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                //RIGHT LOWER ARM
                //////////////////


            }

            return _sc_jitter_tasks;
        }


        public scmessageobjectjitter[][] setiktargetnfingerspositionsNrotations(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix, sc_voxel mainarmparentmeshobject, Vector3 lightpos_, Vector3 dirLight_, Matrix finalRotationMatrix_, sc_voxel mainarmparentmeshobjectmain, sccsikvoxellimbs ikvoxelbody, int somechunkpriminstanceikarmvoxelindex, Matrix hmd_matrix_current, Matrix extramatrix, Vector3[] directionvectoroffsets_, Vector3 targetfootleft, Vector3 targetfootright, sccsikvoxellimbs ikvoxellimb, int somechunkpriminstanceikfingervoxelindex)
        {
            directionvectoroffsets = directionvectoroffsets_;

            lightpos = lightpos_;
            dirLight = dirLight_;

            _SC_modL_r_hand_grab_BUFFER[0].lightPosition = lightpos;
            _SC_modL_r_hand_grab_BUFFER[0].lightDirection = dirLight;

            _SC_modL_rght_hnd_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_hnd_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_shldr_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_shldr_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_elbow_target_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_elbow_target_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_elbow_target_two_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_elbow_target_two_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_upper_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_upper_arm_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_lower_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_lower_arm_BUFFER[0].lightDirection = dirLight;


            Matrix finalRotationMatrix = finalRotationMatrix_;//originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;

            //finalRotationMatrix = finalRotationMatrix * hmdrotMatrix;

            //finalRotationMatrix = hmd_matrix_current * finalRotationMatrix;


            finalRotationMatrix.M41 = 0;
            finalRotationMatrix.M42 = 0;
            finalRotationMatrix.M43 = 0;




            ////////////////////
            /////HUMAN RIG////// 
            ////////////////////
            for (int _iterator = 0; _iterator < 1; _iterator++) // //_player_rght_hnd[0][0]._arrayOfInstances.Length
            {




                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
    
                Vector3 tempOffset = OFFSETPOS;

                //int usethirdpersonview = 1;

                if (Program.usethirdpersonview == 0)
                {
                    tempOffset.X = scupdate.viewPosition.X;
                    tempOffset.Y = scupdate.viewPosition.Y;
                    tempOffset.Z = scupdate.viewPosition.Z;
                    /*
                    tempOffset.Y = tempOffset.Y + ikvoxelbody._player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT.M42;
                    tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (ikvoxelbody._player_head[0][0]._total_torso_height * 0.5f));*/
                }
                else if (Program.usethirdpersonview == 1)
                {
                    //OFFSETPOS.X = scupdate.viewPosition.X;
                    //OFFSETPOS.Y = scupdate.viewPosition.Y;
                    //OFFSETPOS.Z = scupdate.viewPosition.Z;

                    //OFFSETPOS = OFFSETPOS + (dirikvoxelbodyInstanceUp0 * -0.125f);
                    //scupdate.viewPosition = scupdate.viewPosition + (dirikvoxelbodyInstanceRight0 * -1.5f);

                    /*//tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    Quaternion quatt;
                    Quaternion.RotationMatrix(ref scupdate.tempmatter, out quatt);
                    // quatt.Invert();

                    //THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW
                    Vector3 forwardOVR = sc_maths._getDirection(Vector3.ForwardRH, quatt);
                    Vector3 upOVR = sc_maths._getDirection(Vector3.Up, quatt);
                    Vector3 rightOVR = sc_maths._getDirection(Vector3.Right, quatt);
                    upOVR.Normalize();
                    rightOVR.Normalize();
                    forwardOVR.Normalize();

                    forwardOVR *= -0.5f; // -1.0f

                    Vector3 thirdpersonview = OFFSETPOS + (-forwardOVR * 2.0f); //1.5f // + (upOVR * 0.25f)

                    OFFSETPOS.X = thirdpersonview.X;// scupdate.viewPosition.X;
                    OFFSETPOS.Y = thirdpersonview.Y;// scupdate.viewPosition.Y;
                    OFFSETPOS.Z = thirdpersonview.Z;// scupdate.viewPosition.Z;*/
                }



                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW


















                var lengthOfLowerArmRight = _player_rght_lower_arm[0][0]._total_torso_depth * 1.0f;
                var lengthOfUpperArmRight = _player_rght_upper_arm[0][0]._total_torso_depth * 1.0f;
                var lengthOfHandRight = _player_rght_hnd[0][0]._total_torso_depth * 1.0f;
                var totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                var connectorOfHandOffsetMul = 1.00123f; // 1.00123f

                //lightpos = new Vector3(0, 100, 0);
                ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
                diffuseColour = new Vector4(1, 1, 1, 1);
                lightDirection = new Vector3(0, -1, -1);
                dirLight = Vector3.Zero;
                lightpos = Vector3.Zero;
                Quaternion otherQuat;


                ///////////
                //SOMETESTS

                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                var direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var current_rotation_of_torso_pivot_forward = direction_feet_forward;
                var current_rotation_of_torso_pivot_right = direction_feet_right;
                var current_rotation_of_torso_pivot_up = direction_feet_up;
                //SOMETESTS
                ///////////




                Vector3 MOVINGPOINTER = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                /*Matrix somerotationmatrix = mainarmparentmeshobject._arrayOfInstances[_iterator]._ORIGINPOSITION;// _player_torso[0][0]._ORIGINPOSITION;
                Quaternion somequatrot;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                */
                //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                /*var direction_feet_forward_ori_torso = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                var direction_feet_right_ori_torso = sc_maths._getDirection(Vector3.Right, somequatrot);
                var direction_feet_up_ori_torso = sc_maths._getDirection(Vector3.Up, somequatrot);

                //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (mainarmparentmeshobject._total_torso_height * 0.5f));
                */

                Matrix somerotationmatrix = Matrix.Identity;
                Quaternion somequatrot;






                //RIGHT SHOULDER
                somerotationmatrix = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;


                _SC_modL_rght_shldr_BUFFER[0] = new sccs.scgraphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_shldr[0][0]._POSITION.M41, _player_rght_shldr[0][0]._POSITION.M42, _player_rght_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var torsooripos = MOVINGPOINTER;
                somerotationmatrix = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;// _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);
                var diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var tempPoint = MOVINGPOINTER;


                direction_feet_right.Normalize();
                tempPoint = tempPoint + (direction_feet_up * ikvoxellimb._player_rght_hnd[0][0]._total_torso_height);

                //tempPoint = tempPoint + -(direction_feet_right * ((ikvoxelbody._player_torso[0][0]._total_torso_width + (_player_rght_shldr[0][0]._total_torso_width*4)))); // (diffNormPosX)
                //tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));


                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    finalRotationMatrix.M41 = 0;
                    finalRotationMatrix.M42 = 0;
                    finalRotationMatrix.M43 = 0;

                    somerotationmatrix = finalRotationMatrix;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    extramatrix.M41 = 0;
                    extramatrix.M42 = 0;
                    extramatrix.M43 = 0;
                    somerotationmatrix = extramatrix * hmdrotMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    extramatrix.M41 = 0;
                    extramatrix.M42 = 0;
                    extramatrix.M43 = 0;
                    somerotationmatrix = extramatrix * hmdrotMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    finalRotationMatrix.M41 = 0;
                    finalRotationMatrix.M42 = 0;
                    finalRotationMatrix.M43 = 0;

                    somerotationmatrix = finalRotationMatrix;

                }





                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);

                var dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                Vector3 TORSOPIVOT = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                realPosOfRS = TORSOPIVOT + (dirToPoint * ((ikvoxellimb._player_rght_hnd[0][0]._total_torso_height)));
                var pivotOfHead = TORSOPIVOT + (dirToPoint * ((ikvoxellimb._player_rght_hnd[0][0]._total_torso_height)));

               

                //KNUCKLE SECTION
                Matrix somerotmatrix = ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref somerotmatrix, out otherQuat);
                var dirhandforward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var dirhandright = sc_maths._getDirection(Vector3.Right, otherQuat);
                var dirhandup = sc_maths._getDirection(Vector3.Up, otherQuat);

                Vector3 POSITIONOFHAND = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                var somefingerpos = POSITIONOFHAND + (dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f));
                //somefingerpos = somefingerpos + (dirhandforward * (_player_rght_shldr[0][0]._total_torso_depth));

                /*if (somechunkpriminstanceikarmvoxelindex == 0 || somechunkpriminstanceikarmvoxelindex == 3)
                {
                    if (somechunkpriminstanceikfingervoxelindex == 0)
                    {
                        somefingerpos = somefingerpos + (-dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 0.35f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 1)
                    {
                        somefingerpos = somefingerpos + (-dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height  * 0.15f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 2)
                    {
                        somefingerpos = somefingerpos + (dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height  * 0.05f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 3)
                    {
                        somefingerpos = somefingerpos + (dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height  * 0.25f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 4)
                    {
                        somefingerpos = somefingerpos + (dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height  * 0.55f));
                        somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth  * 0.45f));
                    }
                }
                else
                {
                    /*if (somechunkpriminstanceikfingervoxelindex == 0)
                    {
                        somefingerpos = somefingerpos + (-dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 0.35f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 1)
                    {
                        somefingerpos = somefingerpos + (-dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 0.15f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 2)
                    {
                        somefingerpos = somefingerpos + (dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 0.05f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 3)
                    {
                        somefingerpos = somefingerpos + (dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 0.25f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 4)
                    {
                        somefingerpos = somefingerpos + (dirhandup * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 0.55f));
                        somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.45f));
                    }

                    somefingerpos = POSITIONOFHAND + (dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f));

                    //somefingerpos = somefingerpos + (dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.45f));
                    if (somechunkpriminstanceikfingervoxelindex == 0)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width  * 0.35f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 1)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width  * 0.15f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 2)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width  * 0.05f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 3)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width  * 0.25f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 4)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width  * 0.45f));
                        //somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 6 * 0.45f));
                    }
                }*/
                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    if (somechunkpriminstanceikfingervoxelindex == 0)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.35f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 1)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.15f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 2)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.05f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 3)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.25f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 4)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.55f));
                        somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f * 0.45f));
                    }
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    if (somechunkpriminstanceikfingervoxelindex == 0)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.35f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 1)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.15f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 2)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.05f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 3)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.25f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 4)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.55f));
                        somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f * 0.45f));
                    }
                }
                else
                {
                    if (somechunkpriminstanceikfingervoxelindex == 0)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.35f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 1)
                    {
                        somefingerpos = somefingerpos + (dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.15f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 2)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.05f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 3)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.25f));
                    }
                    else if (somechunkpriminstanceikfingervoxelindex == 4)
                    {
                        somefingerpos = somefingerpos + (-dirhandright * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_width * 0.45f));
                        //somefingerpos = somefingerpos + (-dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f * 0.45f));
                    }
                }

                matrixerer = somerotmatrix;
                matrixerer.M41 = somefingerpos.X;
                matrixerer.M42 = somefingerpos.Y;
                matrixerer.M43 = somefingerpos.Z;


                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;















                //GRAB SECTION



                if (somechunkpriminstanceikarmvoxelindex == 0)
                {


                    Matrix somerotmatrixshoulder0 = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref somerotmatrixshoulder0, out otherQuat);
                    var dirshoulderforward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirshoulderright0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirshoulderup0 = sc_maths._getDirection(Vector3.Up, otherQuat);

                    //Vector3 posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                    //Vector3 posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var lengthOfHandheight = ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 2;//_player_rght_hnd[0][0]._total_torso_width;

                    var lengthOfshoulder = _player_rght_shldr[0][0]._total_torso_height;

                    Vector3 somevec0 = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    //Vector3 somevec0 = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);


                    //POSITIONOFHAND = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    somevec0 = somevec0 + (dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f));


                    if (sccs.scgraphics.scupdate.typeofsensortouchR != 9999999)
                    {
                        if (somechunkpriminstanceikfingervoxelindex != 3)
                        {
                            //Vector3 tempVect = Vector3.Zero;
                            if (sccs.scgraphics.scupdate.handTriggerRight[1] > 0.00000001f)
                            {
                                somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * sccs.scgraphics.scupdate.handTriggerRight[1] * 0.5f)));
                            }
                            else
                            {
                                somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * 0.05f * 1.0923f)));
                            }

                            //tempVect = (tempVect) + (direction_head_forward0 * 0.05f * 1.0923f);

                            if (sccs.scgraphics.scupdate.handTriggerRight[1] > 0.00000001f) ///when over 0.52f
                            {
                                somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * (1 - sccs.scgraphics.scupdate.handTriggerRight[1]) * 0.5f)));
                            }
                            else
                            {
                                somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.5f * 1.0923f)));
                            }
                        }
                        else
                        {
                            somevec0 = somevec0 + (dirshoulderforward0 * lengthOfHandheight * 0.5f);
                            //somevec0 = (somevec0) + (-dirshoulderright0 * ((lengthOfHandheight * 0.025f * 1.0923f)));
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.015f * 1.0923f)));
                        }

                        //somevec0 = somevec0 + (dirshoulderforward0 * lengthOfHandheight * 0.5f);




                        MOVINGPOINTER = somevec0;
                    }
                    else
                    {
                        if (sccs.scgraphics.scupdate.handTriggerRight[1] > 0.00000001f)
                        {
                            somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * sccs.scgraphics.scupdate.handTriggerRight[1] * 0.5f)));
                        }
                        else
                        {
                            somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * 0.05f * 1.0923f)));
                        }

                        //tempVect = (tempVect) + (direction_head_forward0 * 0.05f * 1.0923f);

                        if (sccs.scgraphics.scupdate.handTriggerRight[1] > 0.00000001f) ///when over 0.52f
                        {
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * (1 - sccs.scgraphics.scupdate.handTriggerRight[1]) * 0.5f)));
                        }
                        else
                        {
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.5f * 1.0923f)));
                        }


                        MOVINGPOINTER = somevec0;
                    }


                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    Matrix somerotmatrixshoulder0 = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref somerotmatrixshoulder0, out otherQuat);
                    var dirshoulderforward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirshoulderright0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirshoulderup0 = sc_maths._getDirection(Vector3.Up, otherQuat);

                    //Vector3 posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                    //Vector3 posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var lengthOfHandheight = ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 2;//_player_rght_hnd[0][0]._total_torso_width;

                    var lengthOfshoulder = _player_rght_shldr[0][0]._total_torso_height;

                    Vector3 somevec0 = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    //Vector3 somevec0 = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);


                    //POSITIONOFHAND = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    somevec0 = somevec0 + (dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_depth * 0.5f));




                    if (sccs.scgraphics.scupdate.typeofsensortouchL != 9999999)
                    {
                        if (somechunkpriminstanceikfingervoxelindex != 3)
                        {
                            //Vector3 tempVect = Vector3.Zero;
                            if (sccs.scgraphics.scupdate.handTriggerLeft[0] > 0.00000001f)
                            {
                                somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * sccs.scgraphics.scupdate.handTriggerLeft[0] * 0.5f)));
                            }
                            else
                            {
                                somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * 0.05f * 1.0923f)));
                            }

                            //tempVect = (tempVect) + (direction_head_forward0 * 0.05f * 1.0923f);

                            if (sccs.scgraphics.scupdate.handTriggerLeft[0] > 0.00000001f) ///when over 0.52f
                            {
                                somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * (1 - sccs.scgraphics.scupdate.handTriggerLeft[0]) * 0.5f)));
                            }
                            else
                            {
                                somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.5f * 1.0923f)));
                            }
                        }
                        else
                        {
                            somevec0 = somevec0 + (dirshoulderforward0 * lengthOfHandheight * 0.5f);
                            //somevec0 = (somevec0) + (dirshoulderright0 * ((lengthOfHandheight * 0.025f * 1.0923f)));
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.015f * 1.0923f)));
                        }

                        //somevec0 = somevec0 + (dirshoulderforward0 * lengthOfHandheight * 0.5f);

                        MOVINGPOINTER = somevec0;
                    }
                    else
                    {
                        if (sccs.scgraphics.scupdate.handTriggerLeft[0] > 0.00000001f)
                        {
                            somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * sccs.scgraphics.scupdate.handTriggerLeft[0] * 0.5f)));
                        }
                        else
                        {
                            somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * 0.05f * 1.0923f)));
                        }

                        //tempVect = (tempVect) + (direction_head_forward0 * 0.05f * 1.0923f);

                        if (sccs.scgraphics.scupdate.handTriggerLeft[0] > 0.00000001f) ///when over 0.52f
                        {
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * (1 - sccs.scgraphics.scupdate.handTriggerLeft[0]) * 0.5f)));
                        }
                        else
                        {
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.5f * 1.0923f)));
                        }


                        MOVINGPOINTER = somevec0;
                    }
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    Matrix somerotmatrixshoulder0 = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref somerotmatrixshoulder0, out otherQuat);
                    var dirshoulderforward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirshoulderright0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirshoulderup0 = sc_maths._getDirection(Vector3.Up, otherQuat);

                    //Vector3 posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                    //Vector3 posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var lengthOfHandheight = ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 2;//_player_rght_hnd[0][0]._total_torso_width;

                    var lengthOfshoulder = _player_rght_shldr[0][0]._total_torso_height;

                    Vector3 somevec0 = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    //Vector3 somevec0 = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);


                    //POSITIONOFHAND = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    somevec0 = somevec0 + (dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 0.5f));


                    if (sccs.scgraphics.scupdate.typeofsensortouchR != 9999999)
                    {
                        if (somechunkpriminstanceikfingervoxelindex != 1)
                        {
                            //Vector3 tempVect = Vector3.Zero;
                            if (sccs.scgraphics.scupdate.handTriggerRight[1] > 0.00000001f)
                            {
                                somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * sccs.scgraphics.scupdate.handTriggerRight[1] * 0.5f)));
                            }
                            else
                            {
                                somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * 0.05f * 1.0923f)));
                            }

                            //tempVect = (tempVect) + (direction_head_forward0 * 0.05f * 1.0923f);

                            if (sccs.scgraphics.scupdate.handTriggerRight[1] > 0.00000001f) ///when over 0.52f
                            {
                                somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * (1 - sccs.scgraphics.scupdate.handTriggerRight[1]) * 0.5f)));
                            }
                            else
                            {
                                somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.5f * 1.0923f)));
                            }
                        }
                        else
                        {
                            somevec0 = somevec0 + (dirshoulderforward0 * lengthOfHandheight * 0.5f);
                            //somevec0 = (somevec0) + (-dirshoulderright0 * ((lengthOfHandheight * 0.025f * 1.0923f)));
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.015f * 1.0923f)));
                        }

                        //somevec0 = somevec0 + (dirshoulderforward0 * lengthOfHandheight * 0.5f);




                        MOVINGPOINTER = somevec0;
                    }
                    else
                    {
                        if (sccs.scgraphics.scupdate.handTriggerRight[1] > 0.00000001f)
                        {
                            somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * sccs.scgraphics.scupdate.handTriggerRight[1] * 0.5f)));
                        }
                        else
                        {
                            somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * 0.05f * 1.0923f)));
                        }

                        //tempVect = (tempVect) + (direction_head_forward0 * 0.05f * 1.0923f);

                        if (sccs.scgraphics.scupdate.handTriggerRight[1] > 0.00000001f) ///when over 0.52f
                        {
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * (1 - sccs.scgraphics.scupdate.handTriggerRight[1]) * 0.5f)));
                        }
                        else
                        {
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.5f * 1.0923f)));
                        }


                        MOVINGPOINTER = somevec0;
                    }
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {



                    Matrix somerotmatrixshoulder0 = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                    Quaternion.RotationMatrix(ref somerotmatrixshoulder0, out otherQuat);
                    var dirshoulderforward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var dirshoulderright0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var dirshoulderup0 = sc_maths._getDirection(Vector3.Up, otherQuat);

                    //Vector3 posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                    //Vector3 posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var lengthOfHandheight = ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 2;//_player_rght_hnd[0][0]._total_torso_width;

                    var lengthOfshoulder = _player_rght_shldr[0][0]._total_torso_height;

                    Vector3 somevec0 = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    //Vector3 somevec0 = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);


                    //POSITIONOFHAND = new Vector3(ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxellimb._player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    somevec0 = somevec0 + (dirhandforward * (ikvoxellimb._player_rght_hnd[0][0]._total_torso_height * 0.5f));




                    if (sccs.scgraphics.scupdate.typeofsensortouchL != 9999999)
                    {
                        if (somechunkpriminstanceikfingervoxelindex != 3)
                        {
                            //Vector3 tempVect = Vector3.Zero;
                            if (sccs.scgraphics.scupdate.handTriggerLeft[0] > 0.00000001f)
                            {
                                somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * sccs.scgraphics.scupdate.handTriggerLeft[0] * 0.5f)));
                            }
                            else
                            {
                                somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * 0.05f * 1.0923f)));
                            }

                            //tempVect = (tempVect) + (direction_head_forward0 * 0.05f * 1.0923f);

                            if (sccs.scgraphics.scupdate.handTriggerLeft[0] > 0.00000001f) ///when over 0.52f
                            {
                                somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * (1 - sccs.scgraphics.scupdate.handTriggerLeft[0]) * 0.5f)));
                            }
                            else
                            {
                                somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.5f * 1.0923f)));
                            }
                        }
                        else
                        {
                            somevec0 = somevec0 + (dirshoulderforward0 * lengthOfHandheight * 0.5f);
                            //somevec0 = (somevec0) + (dirshoulderright0 * ((lengthOfHandheight * 0.025f * 1.0923f)));
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.015f * 1.0923f)));
                        }

                        //somevec0 = somevec0 + (dirshoulderforward0 * lengthOfHandheight * 0.5f);




                        MOVINGPOINTER = somevec0;
                    }
                    else
                    {
                        if (sccs.scgraphics.scupdate.handTriggerLeft[0] > 0.00000001f)
                        {
                            somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * sccs.scgraphics.scupdate.handTriggerLeft[0] * 0.5f)));
                        }
                        else
                        {
                            somevec0 = (somevec0) + (-dirshoulderup0 * ((lengthOfHandheight * 0.05f * 1.0923f)));
                        }

                        //tempVect = (tempVect) + (direction_head_forward0 * 0.05f * 1.0923f);

                        if (sccs.scgraphics.scupdate.handTriggerLeft[0] > 0.00000001f) ///when over 0.52f
                        {
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * (1 - sccs.scgraphics.scupdate.handTriggerLeft[0]) * 0.5f)));
                        }
                        else
                        {
                            somevec0 = (somevec0) + (dirshoulderforward0 * ((lengthOfHandheight * 0.5f * 1.0923f)));
                        }


                        MOVINGPOINTER = somevec0;
                    }



                }


                matrixerer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;

                worldMatrix_instances_r_hand_grab[0][0][_iterator] = matrixerer;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;
                //_player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatRight;
                //_player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION = someMatRight;
                //GRAB SECTION








































                //FINGER PHALANX SEGMENT OR FINGER TIP SEGMENT
                var positionofshouldermovingpointer = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somerotmatrixshoulder = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref somerotmatrixshoulder, out otherQuat);
                var dirshoulderforward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var dirshoulderright = sc_maths._getDirection(Vector3.Right, otherQuat);
                var dirshoulderup = sc_maths._getDirection(Vector3.Up, otherQuat);

                positionofshouldermovingpointer = positionofshouldermovingpointer + (dirshoulderforward * _player_rght_shldr[0][0]._total_torso_depth * 1 * 0.5f * 0.05f);
                positionofshouldermovingpointer = positionofshouldermovingpointer + (dirshoulderforward * _player_rght_upper_arm[0][0]._total_torso_depth * 1 * 0.05f);
                positionofshouldermovingpointer = positionofshouldermovingpointer + (dirshoulderforward * _player_rght_lower_arm[0][0]._total_torso_depth * 1 * 0.05f);
                //var positionofuppermovingpointer = new Vector3(_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                //var rotmatrixupper = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                //Quaternion.RotationMatrix(ref rotmatrixupper, out otherQuat);
                //var dirupperforward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                //var dirupperright = sc_maths._getDirection(Vector3.Right, otherQuat);
                //var dirupperup = sc_maths._getDirection(Vector3.Up, otherQuat);

                //positionofuppermovingpointer = positionofshouldermovingpointer + (dirshoulderforward * _player_rght_upper_arm[0][0]._total_torso_depth * 6 * 0.5f);

                //var positionoflowermovingpointer = positionofuppermovingpointer + (dirshoulderforward * _player_rght_lower_arm[0][0]._total_torso_depth * 6 * 0.5f);

                //var somePosOfRightHandd = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                //Vector3 positionoflowerfingersegment = positionoflowermovingpointer + (dirshoulderforward * _player_rght_lower_arm[0][0]._total_torso_depth * 6 * 0.5f);
                //Vector3 positionoflowerfingersegment = new Vector3(_player_rght_lower_arm[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_lower_arm[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_lower_arm[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                //Matrix rotmatrixlower = _player_rght_lower_arm[0][0]._arrayOfInstances[_iterator].current_pos;
                //Quaternion.RotationMatrix(ref rotmatrixlower, out otherQuat);
                //Vector3 dirlowerforward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                //Vector3 dirlowerright = sc_maths._getDirection(Vector3.Right, otherQuat);
                //Vector3 dirlowerup = sc_maths._getDirection(Vector3.Up, otherQuat);

                //positionoflowerfingersegment = positionoflowermovingpointer + (dirshoulderforward * _player_rght_lower_arm[0][0]._total_torso_depth * 6 * 0.5f);

                //_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = positionoflowerfingersegment;

                //Vector3 positionoffingertipmovingpointer = positionoflowerfingersegment + (dirlowerforward * _player_rght_hnd[0][0]._total_torso_depth * 6 * 0.5f);

                //positionofshouldermovingpointer = positionofshouldermovingpointer + (dirlowerforward * _player_rght_upper_arm[0][0]._total_torso_depth * 6 * 0.5f);
                //positionoflowerfingersegment = positionofshouldermovingpointer + (dirupperforward * _player_rght_lower_arm[0][0]._total_torso_depth * 6 * 0.5f);

                //Vector3 lowerarmendpos = new Vector3(_player_rght_lower_arm[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION.M41, _player_rght_lower_arm[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION.M42, _player_rght_lower_arm[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION.M43);  //_player_rght_lower_arm[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION;
                Vector3 handgrabpos = new Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                Vector3 grabtolowerfingersegmentendpos = handgrabpos - positionofshouldermovingpointer;
                float lengthofdistance = grabtolowerfingersegmentendpos.Length();
                grabtolowerfingersegmentendpos.Normalize();



                var somePosOfUpperElbowTargetTwo1 = new Vector3(_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetOne1 = new Vector3(_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var someDirFromElbowTargetOneToTwo1 = somePosOfUpperElbowTargetTwo1 - somePosOfUpperElbowTargetOne1;
                var someDirFromElbowTargetOneToRghtHand1 = positionofshouldermovingpointer - somePosOfUpperElbowTargetTwo1;

                Vector3 crossRes1;
                Vector3.Cross(ref someDirFromElbowTargetOneToTwo1, ref someDirFromElbowTargetOneToRghtHand1, out crossRes1);
                crossRes1.Normalize();

                var pointA1 = positionofshouldermovingpointer + (-crossRes1);
                //var someDirFromPivotUpperToHand1 = handgrabpos - positionoflowerfingersegment;
                //var lengthOfDirFromPivotUpperToHand1 = someDirFromPivotUpperToHand1.Length();
                //someDirFromPivotUpperToHand1.Normalize();
                var someDirFromPivothandtograb = pointA1 - positionofshouldermovingpointer;
                someDirFromPivothandtograb.Normalize();

                Vector3 crossRes111;
                Vector3.Cross(ref someDirFromPivothandtograb, ref grabtolowerfingersegmentendpos, out crossRes111);
                crossRes111.Normalize();
                //crossRes111 *= -1;

                Vector3 pointtofingertippivot = handgrabpos + (-grabtolowerfingersegmentendpos * _player_rght_hnd[0][0]._total_torso_depth * 0.05f * 2.0f);

                _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = pointtofingertippivot;// handgrabpos + (-grabtolowerfingersegmentendpos * _player_rght_hnd[0][0]._total_torso_depth * 6 * 1.5f);



                var thehandmatrix = Matrix.LookAtRH(positionofshouldermovingpointer, positionofshouldermovingpointer + grabtolowerfingersegmentendpos, crossRes111);
                thehandmatrix.Invert();

                Vector3 posshoulder = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                Vector3 gragtoshoulder = handgrabpos - posshoulder;
                float lengthmax = gragtoshoulder.Length();
                gragtoshoulder.Normalize();


                if (somechunkpriminstanceikarmvoxelindex == 0 || somechunkpriminstanceikarmvoxelindex == 3)
                {
                    //tempDirr.Length() > (lengthOfLowerArmRight + lengthOfUpperArmRight * 0.1f) * connectorOfHandOffsetMul && (lengthOfLowerArmRight + lengthOfUpperArmRight * 0.1f) != 0
                    if (lengthmax > (totalArmLengthRight * 0.05f))
                    //if (lengthmax > ((lengthOfLowerArmRight * 1.0f) + (lengthOfUpperArmRight * 1.0f)) && (lengthOfLowerArmRight + lengthOfUpperArmRight) != 0)
                    //if (tempDirr.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                    {
                        //Console.WriteLine("far");
                        Vector3 dirfingertiptoshoulder = posshoulder - pointtofingertippivot;
                        float dirfingertiptoshoulderlength = dirfingertiptoshoulder.Length();
                        dirfingertiptoshoulder.Normalize();

                        //Vector3 tempVect = pointtofingertippivot + (dirfingertiptoshoulder * (dirfingertiptoshoulderlength - (totalArmLengthRight * 1.0f)));

                        Vector3 grabtolowerfingersegmentendpos11 = handgrabpos - posshoulder;
                        //float lengthofdistance = grabtolowerfingersegmentendpos11.Length();
                        grabtolowerfingersegmentendpos11.Normalize();


                        //Console.WriteLine("> tempDir.Length " + somechunkpriminstanceikarmvoxelindex);
                        //Program.MessageBox((IntPtr)0, "" + tempDir.Length(), "sc core systems message", 0);
                        //grabtolowerfingersegmentendpos.Normalize();
                        Vector3 tempVector = posshoulder + (grabtolowerfingersegmentendpos11 * (totalArmLengthRight * 0.05f));
                        //MOVINGPOINTER.X = tempVect.X;
                        //MOVINGPOINTER.Y = tempVect.Y;
                        //MOVINGPOINTER.Z = tempVect.Z;
                        matrixerer = thehandmatrix;
                        matrixerer.M41 = tempVector.X;
                        matrixerer.M42 = tempVector.Y;
                        matrixerer.M43 = tempVector.Z;


                        //MOVINGPOINTER.X += OFFSETPOS.X;
                        //MOVINGPOINTER.Y += OFFSETPOS.Y;
                        //MOVINGPOINTER.Z += OFFSETPOS.Z;
                        _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = tempVector;// handgrabpos + (-grabtolowerfingersegmentendpos * _player_rght_hnd[0][0]._total_torso_depth * 6 * 1.5f);
                    }
                    else
                    {
                        //tempDirr.Normalize();
                        //Vector3 tempVect = positionofshouldermovingpointer + (grabtolowerfingersegmentendpos * lengthofdistance);
                        //MOVINGPOINTER.X = tempVect.X;
                        //MOVINGPOINTER.Y = tempVect.Y;
                        //MOVINGPOINTER.Z = tempVect.Z;

                        matrixerer = thehandmatrix;
                        matrixerer.M41 = pointtofingertippivot.X;
                        matrixerer.M42 = pointtofingertippivot.Y;
                        matrixerer.M43 = pointtofingertippivot.Z;

                        //_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = pointtofingertippivot;// handgrabpos + (-grabtolowerfingersegmentendpos * _player_rght_hnd[0][0]._total_torso_depth * 6 * 1.5f);
                    }
                }
                else
                {
                    //tempDirr.Length() > (lengthOfLowerArmRight + lengthOfUpperArmRight * 0.1f) * connectorOfHandOffsetMul && (lengthOfLowerArmRight + lengthOfUpperArmRight * 0.1f) != 0
                    if (lengthmax > (totalArmLengthRight * 0.05f))
                    //if (lengthmax > ((lengthOfLowerArmRight * 1.0f) + (lengthOfUpperArmRight * 1.0f)) && (lengthOfLowerArmRight + lengthOfUpperArmRight) != 0)
                    //if (tempDirr.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                    {
                        //Console.WriteLine("far");
                        Vector3 dirfingertiptoshoulder = posshoulder - pointtofingertippivot;
                        float dirfingertiptoshoulderlength = dirfingertiptoshoulder.Length();
                        dirfingertiptoshoulder.Normalize();

                        //Vector3 tempVect = pointtofingertippivot + (dirfingertiptoshoulder * (dirfingertiptoshoulderlength - (totalArmLengthRight * 1.0f)));

                        Vector3 grabtolowerfingersegmentendpos11 = handgrabpos - posshoulder;
                        //float lengthofdistance = grabtolowerfingersegmentendpos11.Length();
                        grabtolowerfingersegmentendpos11.Normalize();


                        //Console.WriteLine("> tempDir.Length " + somechunkpriminstanceikarmvoxelindex);
                        //Program.MessageBox((IntPtr)0, "" + tempDir.Length(), "sc core systems message", 0);
                        //grabtolowerfingersegmentendpos.Normalize();
                        Vector3 tempVector = posshoulder + (grabtolowerfingersegmentendpos11 * (totalArmLengthRight * 0.05f));
                        //MOVINGPOINTER.X = tempVect.X;
                        //MOVINGPOINTER.Y = tempVect.Y;
                        //MOVINGPOINTER.Z = tempVect.Z;
                        matrixerer = thehandmatrix;
                        matrixerer.M41 = tempVector.X;
                        matrixerer.M42 = tempVector.Y;
                        matrixerer.M43 = tempVector.Z;


                        //MOVINGPOINTER.X += OFFSETPOS.X;
                        //MOVINGPOINTER.Y += OFFSETPOS.Y;
                        //MOVINGPOINTER.Z += OFFSETPOS.Z;
                        _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = tempVector;// handgrabpos + (-grabtolowerfingersegmentendpos * _player_rght_hnd[0][0]._total_torso_depth * 6 * 1.5f);
                    }
                    else
                    {
                        //tempDirr.Normalize();
                        //Vector3 tempVect = positionofshouldermovingpointer + (grabtolowerfingersegmentendpos * lengthofdistance);
                        //MOVINGPOINTER.X = tempVect.X;
                        //MOVINGPOINTER.Y = tempVect.Y;
                        //MOVINGPOINTER.Z = tempVect.Z;

                        matrixerer = thehandmatrix;
                        matrixerer.M41 = pointtofingertippivot.X;
                        matrixerer.M42 = pointtofingertippivot.Y;
                        matrixerer.M43 = pointtofingertippivot.Z;

                        //_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = pointtofingertippivot;// handgrabpos + (-grabtolowerfingersegmentendpos * _player_rght_hnd[0][0]._total_torso_depth * 6 * 1.5f);
                    }

                }


    

                worldMatrix_instances_r_hand[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;
                //FINGER PHALANX SEGMENT OR FINGER TIP SEGMENT







































                Vector3 shoulderposition = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                //shoulderposition = shoulderposition + (direction_feet_up_ori * _player_rght_shldr[0][0]._total_torso_height);



                somerotationmatrix = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref somerotationmatrix, out somequatrot);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequatrot);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequatrot);




                var somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetTwo = new Vector3(_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetOne = new Vector3(_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                //var someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;
                //var someDirFromElbowTargetOneToRghtHand = somePosOfRightHand - somePosOfUpperElbowTargetOne;
                //var someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;


                var dirshoulderpivottohand = somePosOfRightHand - shoulderposition;
                var lengthOfDirFromPivotUpperToHand = dirshoulderpivottohand.Length();
                dirshoulderpivottohand.Normalize();


                lengthOfLowerArmRight = _player_rght_lower_arm[0][0]._total_torso_depth* 0.05f;
                lengthOfUpperArmRight = _player_rght_upper_arm[0][0]._total_torso_depth * 0.05f;
                totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthRight - totalArmLengthRight * 0.001f);
                var upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                var adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);
                //adjacentSolvingForX = Math.Min(adjacentSolvingForX, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);

                /*if (adjacentSolvingForX < 0)
                {
                    adjacentSolvingForX *= -1;
                }*/
                //c2 = a2+b2
                float hypothenuseforupperarmsquared = (lengthOfUpperArmRight * lengthOfUpperArmRight) - (adjacentSolvingForX * adjacentSolvingForX);
                /*var hypothenuseforupperarmsquared = Math.Pow(lengthOfUpperArmRight, 2) - Math.Pow(adjacentSolvingForX, 2);
                if (hypothenuseforupperarmsquared < 0)
                {
                    hypothenuseforupperarmsquared *= -1;
                }*/
                /*if (hypothenuseforupperarmsquared < 0)
                {
                    hypothenuseforupperarmsquared *= -1;
                }*/
                //var oppositeSolvingForHalfA = (float)Math.Sqrt(hypothenuseforupperarmsquared);
                /*if (oppositeSolvingForHalfA < 0)
                {
                    oppositeSolvingForHalfA *= -1;
                }*/
                var oppositeSolvingForHalfA = (float)Math.Sqrt(hypothenuseforupperarmsquared);
                oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);

                /*
                Vector3 upvectorreferencetocreateothervec = Vector3.Right;

                Vector3 crossforpointofreferencefortargettwodirup;
                Vector3.Cross(ref upvectorreferencetocreateothervec, ref dirshoulderpivottohand, out crossforpointofreferencefortargettwodirup);
                crossforpointofreferencefortargettwodirup.Normalize();


                Vector3 elbowpositionrighttowardshandsolvingforxpoint = shoulderposition + (dirshoulderpivottohand * adjacentSolvingForX);


                //Vector3 targettwopointA = elbowpositionrighttowardshandsolvingforxpoint + (crossforpointofreferencefortargettwodirup);
                Vector3 elbowposition = elbowpositionrighttowardshandsolvingforxpoint - (crossforpointofreferencefortargettwodirup * oppositeSolvingForHalfA);

                Vector3 dirshoulderelbow = elbowposition - shoulderposition;
                dirshoulderelbow.Normalize();

                Vector3 fakeelbowduetowrongsizes = elbowposition + (dirshoulderelbow * _player_rght_upper_arm[0][0]._total_torso_depth);
                Vector3 fakeelbowtohanddir = somePosOfRightHand - fakeelbowduetowrongsizes;
                fakeelbowtohanddir.Normalize();
                Vector3 fakeoriginpointlowerarmduetowrongsizesofabove = fakeelbowduetowrongsizes + (fakeelbowtohanddir * _player_rght_lower_arm[0][0]._total_torso_depth);
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = crossforpointofreferencefortargettwodirup;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = fakeelbowduetowrongsizes;// elbowposition;

                Vector3 otherpointrightofrealelbow = elbowposition + (crossforpointofreferencefortargettwodirup);

                Vector3 dirshouldertootherpointrightofelbow = otherpointrightofrealelbow - shoulderposition;
                dirshouldertootherpointrightofelbow.Normalize();

                Vector3 crosstogetupvectorofupperarm;
                Vector3.Cross(ref dirshouldertootherpointrightofelbow, ref dirshoulderelbow, out crosstogetupvectorofupperarm);
                crosstogetupvectorofupperarm.Normalize();

                var shoulderRotationMatrixRight = Matrix.LookAtRH(shoulderposition, shoulderposition + dirshoulderelbow, crosstogetupvectorofupperarm);
                shoulderRotationMatrixRight.Invert();


                matrixerer = shoulderRotationMatrixRight;
                matrixerer.M41 = elbowposition.X;
                matrixerer.M42 = elbowposition.Y;
                matrixerer.M43 = elbowposition.Z;
                matrixerer.M44 = 1;

                worldMatrix_instances_r_upperarm[0][0][_iterator] = matrixerer;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                */



                Vector3 elbowpositionrighttowardshandsolvingforxpoint = shoulderposition + (dirshoulderpivottohand * adjacentSolvingForX);

                Vector3 direlbowtoshoulder = elbowpositionrighttowardshandsolvingforxpoint - shoulderposition;
                direlbowtoshoulder.Normalize();

                Vector3 direlbowreferencetotargettwo = somePosOfUpperElbowTargetTwo - elbowpositionrighttowardshandsolvingforxpoint;
                direlbowreferencetotargettwo.Normalize();

                Vector3 upvectorreferencetocreateothervec = Vector3.Up;

                Vector3 crossforpointofreferencefortargettwodirright;
                Vector3.Cross(ref upvectorreferencetocreateothervec, ref dirshoulderpivottohand, out crossforpointofreferencefortargettwodirright);
                crossforpointofreferencefortargettwodirright.Normalize();

                Vector3 targettwopointA = elbowpositionrighttowardshandsolvingforxpoint + (crossforpointofreferencefortargettwodirright);

                Vector3 shouldertopointadir = targettwopointA - shoulderposition;
                shouldertopointadir.Normalize();

                Vector3 crosstogetupvec;
                Vector3.Cross(ref shouldertopointadir, ref dirshoulderpivottohand, out crosstogetupvec);
                crosstogetupvec.Normalize();

                Vector3 elbowposition = elbowpositionrighttowardshandsolvingforxpoint + (crosstogetupvec * oppositeSolvingForHalfA);

                Vector3 dirshoulderelbow = elbowposition - shoulderposition;
                dirshoulderelbow.Normalize();

                Vector3 realmiddlepivotupperarm = shoulderposition + (dirshoulderelbow * lengthOfUpperArmRight * 0.5f);
                realmiddlepivotupperarm = realmiddlepivotupperarm + (dirshoulderelbow * _player_rght_shldr[0][0]._total_torso_depth * 0.5f * 0.05f);



                //Vector3 fakeelbowduetowrongsizes = elbowposition;// + (dirshoulderelbow * _player_rght_upper_arm[0][0]._total_torso_depth * 0.5f);
                //Vector3 fakeelbowtohanddir = somePosOfRightHand - fakeelbowduetowrongsizes;
                //fakeelbowtohanddir.Normalize();

                Vector3 fakeoriginpointlowerarmduetowrongsizesofabove = realmiddlepivotupperarm + (dirshoulderelbow * lengthOfLowerArmRight * 0.5f);

                Vector3 fakeelbowtohanddir = somePosOfRightHand - fakeoriginpointlowerarmduetowrongsizesofabove;
                fakeelbowtohanddir.Normalize();


                Vector3 currentposlowerarm = fakeoriginpointlowerarmduetowrongsizesofabove;// + (dirshoulderelbow * _player_rght_shldr[0][0]._total_torso_depth * 0.5f);
                currentposlowerarm = currentposlowerarm + (fakeelbowtohanddir * _player_rght_lower_arm[0][0]._total_torso_depth * 0.5f * 0.05f);






                /*Vector3 fakeelbowduetowrongsizes = elbowposition + (dirshoulderelbow * _player_rght_upper_arm[0][0]._total_torso_depth);
                Vector3 fakeelbowtohanddir = somePosOfRightHand - fakeelbowduetowrongsizes;
                fakeelbowtohanddir.Normalize();
                Vector3 fakeoriginpointlowerarmduetowrongsizesofabove = fakeelbowduetowrongsizes + (fakeelbowtohanddir * _player_rght_lower_arm[0][0]._total_torso_depth);
                */
                //Vector3 crossotherpointtorightofelbow;
                //Vector3.Cross(ref dirshoulderelbow, ref somedirshoulderpivottohand, out crossotherpointtorightofelbow);
                //crossotherpointtorightofelbow.Normalize();

                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = crossforpointofreferencefortargettwodirright;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = fakeoriginpointlowerarmduetowrongsizesofabove;// elbowposition;

                Vector3 otherpointrightofrealelbow = elbowposition + (crossforpointofreferencefortargettwodirright);

                Vector3 dirshouldertootherpointrightofelbow = otherpointrightofrealelbow - shoulderposition;
                dirshouldertootherpointrightofelbow.Normalize();

                Vector3 crosstogetupvectorofupperarm;
                Vector3.Cross(ref dirshouldertootherpointrightofelbow, ref dirshoulderelbow, out crosstogetupvectorofupperarm);
                crosstogetupvectorofupperarm.Normalize();




                var shoulderRotationMatrixRight = Matrix.LookAtRH(shoulderposition, shoulderposition + dirshoulderelbow, crosstogetupvectorofupperarm);
                shoulderRotationMatrixRight.Invert();


                matrixerer = shoulderRotationMatrixRight;
                matrixerer.M41 = realmiddlepivotupperarm.X;
                matrixerer.M42 = realmiddlepivotupperarm.Y;
                matrixerer.M43 = realmiddlepivotupperarm.Z;
                matrixerer.M44 = 1;

                worldMatrix_instances_r_upperarm[0][0][_iterator] = matrixerer;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

















                /*Vector3 elbowpositionrighttowardshandsolvingforxpoint = shoulderposition + (dirshoulderpivottohand * adjacentSolvingForX);

                Vector3 direlbowtoshoulder = elbowpositionrighttowardshandsolvingforxpoint - shoulderposition;
                direlbowtoshoulder.Normalize();

                Vector3 direlbowreferencetotargettwo = somePosOfUpperElbowTargetTwo - elbowpositionrighttowardshandsolvingforxpoint;
                direlbowreferencetotargettwo.Normalize();

                Vector3 upvectorreferencetocreateothervec = Vector3.Up;

                Vector3 crossforpointofreferencefortargettwodirright;
                Vector3.Cross(ref upvectorreferencetocreateothervec, ref dirshoulderpivottohand, out crossforpointofreferencefortargettwodirright);
                crossforpointofreferencefortargettwodirright.Normalize();

                Vector3 targettwopointA = elbowpositionrighttowardshandsolvingforxpoint + (crossforpointofreferencefortargettwodirright);

                Vector3 shouldertopointadir = targettwopointA - shoulderposition;
                shouldertopointadir.Normalize();

                Vector3 crosstogetupvec;
                Vector3.Cross(ref shouldertopointadir, ref dirshoulderpivottohand, out crosstogetupvec);
                crosstogetupvec.Normalize();

                Vector3 elbowposition = elbowpositionrighttowardshandsolvingforxpoint + (crosstogetupvec * oppositeSolvingForHalfA);

                Vector3 dirshoulderelbow = elbowposition - shoulderposition;
                dirshoulderelbow.Normalize();

                Vector3 fakeelbowduetowrongsizes = elbowposition + (dirshoulderelbow * _player_rght_upper_arm[0][0]._total_torso_depth);
                Vector3 fakeelbowtohanddir = somePosOfRightHand - fakeelbowduetowrongsizes;
                fakeelbowtohanddir.Normalize();
                Vector3 fakeoriginpointlowerarmduetowrongsizesofabove = fakeelbowduetowrongsizes + (fakeelbowtohanddir * _player_rght_lower_arm[0][0]._total_torso_depth);

                //Vector3 crossotherpointtorightofelbow;
                //Vector3.Cross(ref dirshoulderelbow, ref somedirshoulderpivottohand, out crossotherpointtorightofelbow);
                //crossotherpointtorightofelbow.Normalize();

                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = crossforpointofreferencefortargettwodirright;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = fakeelbowduetowrongsizes;// elbowposition;

                Vector3 otherpointrightofrealelbow = elbowposition + (crossforpointofreferencefortargettwodirright);

                Vector3 dirshouldertootherpointrightofelbow = otherpointrightofrealelbow - shoulderposition;
                dirshouldertootherpointrightofelbow.Normalize();

                Vector3 crosstogetupvectorofupperarm;
                Vector3.Cross(ref dirshouldertootherpointrightofelbow, ref dirshoulderelbow, out crosstogetupvectorofupperarm);
                crosstogetupvectorofupperarm.Normalize();

                var shoulderRotationMatrixRight = Matrix.LookAtRH(shoulderposition, shoulderposition + dirshoulderelbow, crosstogetupvectorofupperarm);
                shoulderRotationMatrixRight.Invert();

            
                matrixerer = shoulderRotationMatrixRight;
                matrixerer.M41 = elbowposition.X;
                matrixerer.M42 = elbowposition.Y;
                matrixerer.M43 = elbowposition.Z;
                matrixerer.M44 = 1;

                worldMatrix_instances_r_upperarm[0][0][_iterator] = matrixerer;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                
                */

                Vector3 moreupdirtoelbow = otherpointrightofrealelbow + (-crosstogetupvectorofupperarm);



                matrixerer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                matrixerer.M41 = moreupdirtoelbow.X;
                matrixerer.M42 = moreupdirtoelbow.Y;
                matrixerer.M43 = moreupdirtoelbow.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_elbow_target_two[0][0][_iterator] = matrixerer;
                _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;







                Vector3 positionhand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                Vector3 dirhandtoelbow = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION - positionhand;
                dirhandtoelbow.Normalize();

                Vector3 pivotlower = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                Vector3 crossvecrefelbow = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;

                Vector3 vectorupdirlower;
                Vector3.Cross(ref dirhandtoelbow, ref crossvecrefelbow, out vectorupdirlower);
                vectorupdirlower.Normalize();

                Matrix theLowerArmRotationMatrix = Matrix.LookAtRH(currentposlowerarm, currentposlowerarm + dirhandtoelbow, vectorupdirlower);
                theLowerArmRotationMatrix.Invert();

                matrixerer = theLowerArmRotationMatrix; //_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;//  
                matrixerer.M41 = currentposlowerarm.X;
                matrixerer.M42 = currentposlowerarm.Y;
                matrixerer.M43 = currentposlowerarm.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_lowerarm[0][0][_iterator] = matrixerer;
                _player_rght_lower_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                //RIGHT LOWER ARM
                //////////////////




            }



            return _sc_jitter_tasks;
        }

        public scmessageobjectjitter[][] ikarmrender(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {

            _player_rght_hnd[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_hnd(SC_console_directx.D3D.device.ImmediateContext, _player_rght_hnd[0][0].IndexCount, _player_rght_hnd[0][0].InstanceCount, _player_rght_hnd[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _player_rght_hnd[0][0]);
            var _cuber001 = _player_rght_shldr[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_shldr(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_shldr_BUFFER, _cuber001);
            _cuber001 = _player_rght_lower_arm[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_lower_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_lower_arm_BUFFER, _cuber001);
            _cuber001 = _player_rght_upper_arm[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_upper_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_upper_arm_BUFFER, _cuber001);
            //IK TARGETS UPPER BODY.


            /*
            var _cuber01 = _player_rght_elbow_target[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_elbow_targ(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _cuber01);

            _cuber01 = _player_rght_elbow_target_two[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _cuber01);
            _cuber01 = _player_r_hand_grab[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_hnd(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_r_hand_grab_BUFFER, _cuber01);
            _cuber01 = _player_rght_elbow_target_three[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _cuber01);
            */


            return _sc_jitter_tasks;
        }








        public scmessageobjectjitter[][] ikfingerrender(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {

            _player_rght_hnd[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_hnd(SC_console_directx.D3D.device.ImmediateContext, _player_rght_hnd[0][0].IndexCount, _player_rght_hnd[0][0].InstanceCount, _player_rght_hnd[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _player_rght_hnd[0][0]);
            var _cuber001 = _player_rght_shldr[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_shldr(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_shldr_BUFFER, _cuber001);
            _cuber001 = _player_rght_lower_arm[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_lower_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_lower_arm_BUFFER, _cuber001);
            _cuber001 = _player_rght_upper_arm[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_upper_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_upper_arm_BUFFER, _cuber001);
            //IK TARGETS UPPER BODY.



            
            var _cuber01 = _player_rght_elbow_target[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_elbow_targ(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _cuber01);

            _cuber01 = _player_rght_elbow_target_two[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _cuber01);
            _cuber01 = _player_r_hand_grab[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_hnd(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_r_hand_grab_BUFFER, _cuber01);

            _cuber01 = _player_rght_elbow_target_three[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            scupdate._shaderManager._rend_rgt_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _cuber01);
            



            return _sc_jitter_tasks;
        }










        public scmessageobjectjitter[][] writeikarmtobuffer(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {




            Matrix[] worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;



            //PHYSICS HAND RIGHT GRAB
            _player_r_hand_grab[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand_grab[0][0];
            _player_r_hand_grab[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS HAND RIGHT
            _player_rght_hnd[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[0][0];
            _player_rght_hnd[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER ARM RIGHT
            _player_rght_lower_arm[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_lowerarm[0][0];
            _player_rght_lower_arm[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS UPPER ARM RIGHT
            _player_rght_upper_arm[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_upperarm[0][0];
            _player_rght_upper_arm[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS  RIGHT ELBOWTARGET
            _player_rght_elbow_target[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target[0][0];
            _player_rght_elbow_target[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS RIGHT ELBOWTARGET TWO
            _player_rght_elbow_target_two[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target_two[0][0];
            _player_rght_elbow_target_two[0][0]._POSITION = worldMatrix_base[0];



            //PHYSICS RIGHT SHOULDER
            _player_rght_shldr[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_shoulder[0][0];
            _player_rght_shldr[0][0]._POSITION = worldMatrix_base[0];




            //PHYSICS RIGHT ELBOWTARGET TWO
            _player_rght_elbow_target_three[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target_three[0][0];
            _player_rght_elbow_target_three[0][0]._POSITION = worldMatrix_base[0];


            var voxel_cuber_r_targ_three = _player_rght_elbow_target_three[0][0];
            var voxel_sometester_r_targ_three = voxel_cuber_r_targ_three._WORLDMATRIXINSTANCES;


            //UPPER BODY

            voxel_cuber_r_shld = _player_rght_shldr[0][0];
            voxel_sometester_r_shld = voxel_cuber_r_shld._WORLDMATRIXINSTANCES;
            voxel_cuber_r_up_arm = _player_rght_upper_arm[0][0];
            voxel_sometester_r_up_arm = voxel_cuber_r_up_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ = _player_rght_elbow_target[0][0];
            voxel_sometester_r_targ = voxel_cuber_r_targ._WORLDMATRIXINSTANCES;
            voxel_cuber_r_low_arm = _player_rght_lower_arm[0][0];
            voxel_sometester_r_low_arm = voxel_cuber_r_low_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_r_hnd = _player_rght_hnd[0][0];
            voxel_sometester_r_hnd = voxel_cuber_r_hnd._WORLDMATRIXINSTANCES;
            voxel_cuber_r_hand_grab = _player_r_hand_grab[0][0];
            voxel_sometester_r_hand_grab = voxel_cuber_r_hand_grab._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ_two = _player_rght_elbow_target_two[0][0];
            voxel_sometester_r_targ_two = voxel_cuber_r_targ_two._WORLDMATRIXINSTANCES;

            Quaternion _testQuater;


            for (int i = 0; i < voxel_cuber_r_shld.instances.Length; i++)
            {













                //RIGHT HAND
                var xxx = voxel_sometester_r_hnd[i].M41;
                var yyy = voxel_sometester_r_hnd[i].M42;
                var zzz = voxel_sometester_r_hnd[i].M43;
                voxel_cuber_r_hnd.instances[i].position.X = xxx;
                voxel_cuber_r_hnd.instances[i].position.Y = yyy;
                voxel_cuber_r_hnd.instances[i].position.Z = zzz;
                voxel_cuber_r_hnd.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_hnd[i], out _testQuater);
                var dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.W = 1;




                //RIGHT HAND GRAB
                xxx = voxel_sometester_r_hand_grab[i].M41;
                yyy = voxel_sometester_r_hand_grab[i].M42;
                zzz = voxel_sometester_r_hand_grab[i].M43;
                voxel_cuber_r_hand_grab.instances[i].position.X = xxx;
                voxel_cuber_r_hand_grab.instances[i].position.Y = yyy;
                voxel_cuber_r_hand_grab.instances[i].position.Z = zzz;
                voxel_cuber_r_hand_grab.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_hand_grab[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.W = 1;




                //voxel_cuber_r_low_arm = _player_rght_lower_arm[tempIndex];
                //voxel_instancers_r_low_arm = voxel_instancers_r_low_arm.instances;
                //voxel_sometester_r_low_arm = voxel_instancers_r_low_arm._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_low_arm[i].M41;
                yyy = voxel_sometester_r_low_arm[i].M42;
                zzz = voxel_sometester_r_low_arm[i].M43;

                voxel_cuber_r_low_arm.instances[i].position.X = xxx;
                voxel_cuber_r_low_arm.instances[i].position.Y = yyy;
                voxel_cuber_r_low_arm.instances[i].position.Z = zzz;
                voxel_cuber_r_low_arm.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_low_arm[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.W = 1;





                xxx = voxel_sometester_r_up_arm[i].M41;
                yyy = voxel_sometester_r_up_arm[i].M42;
                zzz = voxel_sometester_r_up_arm[i].M43;

                voxel_cuber_r_up_arm.instances[i].position.X = xxx;
                voxel_cuber_r_up_arm.instances[i].position.Y = yyy;
                voxel_cuber_r_up_arm.instances[i].position.Z = zzz;
                voxel_cuber_r_up_arm.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_up_arm[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.W = 1;






                //voxel_cuber_r_targ = _player_rght_elbow_target[tempIndex];
                //voxel_instancers = voxel_cuber_r_targ.instances;
                //voxel_sometester_r_targ = voxel_cuber_r_targ._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_targ[i].M41;
                yyy = voxel_sometester_r_targ[i].M42;
                zzz = voxel_sometester_r_targ[i].M43;

                voxel_cuber_r_targ.instances[i].position.X = xxx;
                voxel_cuber_r_targ.instances[i].position.Y = yyy;
                voxel_cuber_r_targ.instances[i].position.Z = zzz;
                voxel_cuber_r_targ.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.W = 1;





                //voxel_cuber_r_targ_two = _player_rght_elbow_target_two[tempIndex];
                //voxel_instancers = voxel_cuber_r_targ_two.instances;
                //voxel_sometester_r_targ_two = voxel_cuber_r_targ_two._WORLDMATRIXINSTANCES;


                xxx = voxel_sometester_r_targ_two[i].M41;
                yyy = voxel_sometester_r_targ_two[i].M42;
                zzz = voxel_sometester_r_targ_two[i].M43;

                voxel_cuber_r_targ_two.instances[i].position.X = xxx;
                voxel_cuber_r_targ_two.instances[i].position.Y = yyy;
                voxel_cuber_r_targ_two.instances[i].position.Z = zzz;
                voxel_cuber_r_targ_two.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ_two[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.W = 1;


                xxx = voxel_sometester_r_targ_three[i].M41;
                yyy = voxel_sometester_r_targ_three[i].M42;
                zzz = voxel_sometester_r_targ_three[i].M43;

                voxel_cuber_r_targ_three.instances[i].position.X = xxx;
                voxel_cuber_r_targ_three.instances[i].position.Y = yyy;
                voxel_cuber_r_targ_three.instances[i].position.Z = zzz;
                voxel_cuber_r_targ_three.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ_three[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ_three.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_three.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_three.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_three.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ_three.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_three.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_three.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_three.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ_three.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_three.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_three.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_three.instancesDataUP[i].rotation.W = 1;


                //voxel_cuber = _player_rght_shldr[tempIndex];
                //voxel_instancers = voxel_cuber.instances;
                //voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_shld[i].M41;
                yyy = voxel_sometester_r_shld[i].M42;
                zzz = voxel_sometester_r_shld[i].M43;

                voxel_cuber_r_shld.instances[i].position.X = xxx;
                voxel_cuber_r_shld.instances[i].position.Y = yyy;
                voxel_cuber_r_shld.instances[i].position.Z = zzz;
                voxel_cuber_r_shld.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_shld[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_shld.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_shld.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.W = 1;

            }

            return _sc_jitter_tasks;
        }







        public scmessageobjectjitter[][] writeikfingertobuffer(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {




            Matrix[] worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;



            //PHYSICS HAND RIGHT GRAB
            _player_r_hand_grab[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand_grab[0][0];
            _player_r_hand_grab[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS HAND RIGHT
            _player_rght_hnd[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[0][0];
            _player_rght_hnd[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER ARM RIGHT
            _player_rght_lower_arm[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_lowerarm[0][0];
            _player_rght_lower_arm[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS UPPER ARM RIGHT
            _player_rght_upper_arm[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_upperarm[0][0];
            _player_rght_upper_arm[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS  RIGHT ELBOWTARGET
            _player_rght_elbow_target[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target[0][0];
            _player_rght_elbow_target[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS RIGHT ELBOWTARGET TWO
            _player_rght_elbow_target_two[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target_two[0][0];
            _player_rght_elbow_target_two[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS RIGHT ELBOWTARGET TWO
            _player_rght_elbow_target_three[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target_three[0][0];
            _player_rght_elbow_target_three[0][0]._POSITION = worldMatrix_base[0];




            //PHYSICS RIGHT SHOULDER
            _player_rght_shldr[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_shoulder[0][0];
            _player_rght_shldr[0][0]._POSITION = worldMatrix_base[0];






            //UPPER BODY

            voxel_cuber_r_shld = _player_rght_shldr[0][0];
            voxel_sometester_r_shld = voxel_cuber_r_shld._WORLDMATRIXINSTANCES;
            voxel_cuber_r_up_arm = _player_rght_upper_arm[0][0];
            voxel_sometester_r_up_arm = voxel_cuber_r_up_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ = _player_rght_elbow_target[0][0];
            voxel_sometester_r_targ = voxel_cuber_r_targ._WORLDMATRIXINSTANCES;
            voxel_cuber_r_low_arm = _player_rght_lower_arm[0][0];
            voxel_sometester_r_low_arm = voxel_cuber_r_low_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_r_hnd = _player_rght_hnd[0][0];
            voxel_sometester_r_hnd = voxel_cuber_r_hnd._WORLDMATRIXINSTANCES;
            voxel_cuber_r_hand_grab = _player_r_hand_grab[0][0];
            voxel_sometester_r_hand_grab = voxel_cuber_r_hand_grab._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ_two = _player_rght_elbow_target_two[0][0];
            voxel_sometester_r_targ_two = voxel_cuber_r_targ_two._WORLDMATRIXINSTANCES;

            var voxel_cuber_r_targ_three = _player_rght_elbow_target_three[0][0];
            var voxel_sometester_r_targ_three = voxel_cuber_r_targ_three._WORLDMATRIXINSTANCES;


            Quaternion _testQuater;


            for (int i = 0; i < voxel_cuber_r_shld.instances.Length; i++)
            {













                //RIGHT HAND
                var xxx = voxel_sometester_r_hnd[i].M41;
                var yyy = voxel_sometester_r_hnd[i].M42;
                var zzz = voxel_sometester_r_hnd[i].M43;
                voxel_cuber_r_hnd.instances[i].position.X = xxx;
                voxel_cuber_r_hnd.instances[i].position.Y = yyy;
                voxel_cuber_r_hnd.instances[i].position.Z = zzz;
                voxel_cuber_r_hnd.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_hnd[i], out _testQuater);
                var dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.W = 1;




                //RIGHT HAND GRAB
                xxx = voxel_sometester_r_hand_grab[i].M41;
                yyy = voxel_sometester_r_hand_grab[i].M42;
                zzz = voxel_sometester_r_hand_grab[i].M43;
                voxel_cuber_r_hand_grab.instances[i].position.X = xxx;
                voxel_cuber_r_hand_grab.instances[i].position.Y = yyy;
                voxel_cuber_r_hand_grab.instances[i].position.Z = zzz;
                voxel_cuber_r_hand_grab.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_hand_grab[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.W = 1;




                //voxel_cuber_r_low_arm = _player_rght_lower_arm[tempIndex];
                //voxel_instancers_r_low_arm = voxel_instancers_r_low_arm.instances;
                //voxel_sometester_r_low_arm = voxel_instancers_r_low_arm._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_low_arm[i].M41;
                yyy = voxel_sometester_r_low_arm[i].M42;
                zzz = voxel_sometester_r_low_arm[i].M43;

                voxel_cuber_r_low_arm.instances[i].position.X = xxx;
                voxel_cuber_r_low_arm.instances[i].position.Y = yyy;
                voxel_cuber_r_low_arm.instances[i].position.Z = zzz;
                voxel_cuber_r_low_arm.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_low_arm[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.W = 1;





                xxx = voxel_sometester_r_up_arm[i].M41;
                yyy = voxel_sometester_r_up_arm[i].M42;
                zzz = voxel_sometester_r_up_arm[i].M43;

                voxel_cuber_r_up_arm.instances[i].position.X = xxx;
                voxel_cuber_r_up_arm.instances[i].position.Y = yyy;
                voxel_cuber_r_up_arm.instances[i].position.Z = zzz;
                voxel_cuber_r_up_arm.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_up_arm[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.W = 1;






                //voxel_cuber_r_targ = _player_rght_elbow_target[tempIndex];
                //voxel_instancers = voxel_cuber_r_targ.instances;
                //voxel_sometester_r_targ = voxel_cuber_r_targ._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_targ[i].M41;
                yyy = voxel_sometester_r_targ[i].M42;
                zzz = voxel_sometester_r_targ[i].M43;

                voxel_cuber_r_targ.instances[i].position.X = xxx;
                voxel_cuber_r_targ.instances[i].position.Y = yyy;
                voxel_cuber_r_targ.instances[i].position.Z = zzz;
                voxel_cuber_r_targ.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.W = 1;





                //voxel_cuber_r_targ_two = _player_rght_elbow_target_two[tempIndex];
                //voxel_instancers = voxel_cuber_r_targ_two.instances;
                //voxel_sometester_r_targ_two = voxel_cuber_r_targ_two._WORLDMATRIXINSTANCES;


                xxx = voxel_sometester_r_targ_two[i].M41;
                yyy = voxel_sometester_r_targ_two[i].M42;
                zzz = voxel_sometester_r_targ_two[i].M43;

                voxel_cuber_r_targ_two.instances[i].position.X = xxx;
                voxel_cuber_r_targ_two.instances[i].position.Y = yyy;
                voxel_cuber_r_targ_two.instances[i].position.Z = zzz;
                voxel_cuber_r_targ_two.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ_two[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.W = 1;




                xxx = voxel_sometester_r_targ_three[i].M41;
                yyy = voxel_sometester_r_targ_three[i].M42;
                zzz = voxel_sometester_r_targ_three[i].M43;

                voxel_cuber_r_targ_three.instances[i].position.X = xxx;
                voxel_cuber_r_targ_three.instances[i].position.Y = yyy;
                voxel_cuber_r_targ_three.instances[i].position.Z = zzz;
                voxel_cuber_r_targ_three.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ_three[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ_three.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_three.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_three.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_three.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ_three.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_three.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_three.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_three.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ_three.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_three.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_three.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_three.instancesDataUP[i].rotation.W = 1;













                //voxel_cuber = _player_rght_shldr[tempIndex];
                //voxel_instancers = voxel_cuber.instances;
                //voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_shld[i].M41;
                yyy = voxel_sometester_r_shld[i].M42;
                zzz = voxel_sometester_r_shld[i].M43;

                voxel_cuber_r_shld.instances[i].position.X = xxx;
                voxel_cuber_r_shld.instances[i].position.Y = yyy;
                voxel_cuber_r_shld.instances[i].position.Z = zzz;
                voxel_cuber_r_shld.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_shld[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_shld.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_shld.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.W = 1;

            }

            return _sc_jitter_tasks;
        }



    }
}




/*posRHand = new Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

tempDir.Normalize();
var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f) + (lengthOfHandRight * 1.0923f)));

MOVINGPOINTER.X = tempVect.X;
MOVINGPOINTER.Y = tempVect.Y;
MOVINGPOINTER.Z = tempVect.Z;*/
/*if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
{
    tempDir.Normalize();
    var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
    Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
    MOVINGPOINTER.X = tempVect.X;
    MOVINGPOINTER.Y = tempVect.Y;
    MOVINGPOINTER.Z = tempVect.Z;
}*/





/*Matrix handmatrix = someMatRight * finalRotationMatrix;// _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos;

Quaternion.RotationMatrix(ref handmatrix, out somequatrot);
Vector3 handgrabdirf = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
Vector3 handgrabdirr = sc_maths._getDirection(Vector3.Right, somequatrot);
Vector3 handgrabdiru = sc_maths._getDirection(Vector3.Up, somequatrot);

//Vector3 somevec0 = MOVINGPOINTER;// new Vector3(MOVINGPOINTER.X, someMatRight.M42, someMatRight.M43);

Vector3 somevec0 = directionvectoroffsets[3];*/
//somevec0 = somevec0 + (handgrabdirf  *);


//MOVINGPOINTER.X += somevec0.X;
//MOVINGPOINTER.Y += somevec0.Y;
//MOVINGPOINTER.Z += somevec0.Z;


//MOVINGPOINTER.Z = MOVINGPOINTER.Z + (handgrabdirf.Z * somevec0.Z);//



/*Matrix handmatrix = someMatRight * finalRotationMatrix;// _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos;

Quaternion.RotationMatrix(ref handmatrix, out somequatrot);
Vector3 handgrabdirf = sc_maths._getDirection(Vector3.ForwardRH, somequatrot);
Vector3 handgrabdirr = sc_maths._getDirection(Vector3.Right, somequatrot);
Vector3 handgrabdiru = sc_maths._getDirection(Vector3.Up, somequatrot);

MOVINGPOINTER = MOVINGPOINTER + (handgrabdirf * (0.1f));*/
