using System;
using System.Collections.Generic;
using System.Text;

using SharpDX;

using scmessageobject = sccs.scmessageobject.scmessageobject;
using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;

using Ab3d.OculusWrap;

namespace sccs
{
    public abstract class scgraphicsmain //: SC_Console_GRAPHICS
    {
        public scgraphicsmain()
        {

        }

        public abstract scmessageobjectjitter[][] sc_write_to_buffer(scmessageobjectjitter[][] _sc_jitter_tasks);
        public abstract scmessageobjectjitter[][] loop_worlds(scmessageobjectjitter[][] _sc_jitter_tasks);
        public abstract scmessageobjectjitter[][] workOnSomething(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft);
        public abstract scmessageobjectjitter[][] _sc_create_world_objects(scmessageobjectjitter[][] _sc_jitter_tasks);
    }
}
