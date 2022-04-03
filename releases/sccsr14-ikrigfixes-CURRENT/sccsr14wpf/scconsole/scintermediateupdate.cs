using System;
using System.Collections.Generic;
using System.Text;
using SharpDX;
using scmessageobject = sccs.scmessageobject.scmessageobject;
using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;
using Ab3d.OculusWrap;

namespace sccs.scgraphics
{
    public abstract class scintermediateupdate : scdirectx
    {
        protected override void SC_Init_DirectX() //DSystemConfiguration configuration, IntPtr Hwnd, sc_console.sc_console_writer _writer
        {
            base.SC_Init_DirectX(); //configuration, Hwnd, _writer
        }

        public abstract scmessageobjectjitter[][] sc_write_to_buffer(scmessageobjectjitter[][] _sc_jitter_tasks);
        public abstract scmessageobjectjitter[][] loop_worlds(scmessageobjectjitter[][] _sc_jitter_tasks);
        public abstract scmessageobjectjitter[][] workOnSomething(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft);
        public abstract scmessageobjectjitter[][] _sc_create_world_objects(scmessageobjectjitter[][] _sc_jitter_tasks);

    }
}
