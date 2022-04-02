using SharpDX;
using Ab3d.OculusWrap;
using SharpDX.Direct3D11;

namespace sccs.scmessageobject
{
    public struct scmessageobject
    {
        public int _received_switch_in;
        public int _received_switch_out;
        public int _sending_switch_in;
        public int _sending_switch_out;
        //public List<int[]> _chain_Of_Tasks0;
        public int _timeOut0;
        public int _ParentTaskThreadID0;
        public int _main_cpu_count;
        public string _passTest;
        public int _welcomePackage;
        //public ManualResetEvent _reset_event;
        public int _work_done;
        public int _current_menu;
        public int _last_current_menu;
        public int _main_menu;
        public string _menuOption;
        public int _voRecSwtc;
        public string _voRecMsg;
        public object[] _world_data;
        public object[] _someData;

        //public World _world; //Jitter Physics
    }

    public struct scmessageobjectjitter
    {
        public int _sc_jitter_main;
        public int _sc_jitter_can_work;
        public float _world_step;
        public object[] _world_data;
        public int _work_index;
        //public int hasinit;
        public SharpDX.Direct3D11.Device device;
        public ShaderResourceView shaderresource;
        public byte[] frameByteArray;
        public sccsscreenframe theframe;
        public int swtcvirtualdesktoptypet0;
        public int swtcvirtualdesktoptypet1;
    }

    public struct scgraphicssecpackage
    {
        //_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, someextrapelvismatrix
        public scmessageobjectjitter[][] scjittertasks;
        public Matrix viewMatrix;
        public Matrix projectionMatrix;
        public Matrix originRot;
        public Matrix rotatingMatrix;
        public Matrix hmdmatrixRot;
        public Matrix hmd_matrix;
        public Matrix rotatingMatrixForPelvis;
        public Matrix rightTouchMatrix;
        public Matrix leftTouchMatrix;
        public Matrix oriProjectionMatrix;
        public Matrix someextrapelvismatrix;
        public Vector3 offsetpos;
        public Posef handPoseLeft;
        public Posef handPoseRight;
        public sccs.scgraphics.scgraphicssec scgraphicssec;


    }
}

