using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace sccs.sccore
{
    public class scglobalsaccessor : scicomponent, scglobals
    {
        public static scicomponent SCICOMPONENT;
        scglobals scicomponent.SCGlobals
        {
            get => SCGLOB;
        }
        public static scglobals SCGLOB;


        public virtual sccs.sccore.scconsolecore SCCONSOLECORE { get; set; }
        //public virtual sccs.scconsole.scconsolewriter SCCONSOLEWRITER { get; set; }
        //public virtual sccs.scconsole.scconsolereader SCCONSOLEREADER { get; set; }
        public virtual sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS { get; set; }

        public virtual int _Activate_Desktop_Image { get; set; }

        private scmessageobject.scmessageobject testingInit(scmessageobject.scmessageobject _main_object)
        {   
            return _main_object;
        }

        public static int _init_main_Task = 1;

        public scglobalsaccessor(scmessageobject.scmessageobject[] _main_object)
        {
            SCGLOB = this;
            SCICOMPONENT = this;

            SCCONSOLECORE = new sccore.scconsolecore(_main_object);
            //SCCONSOLEWRITER = new scconsole.scconsolewriter(_main_object);
            //SCCONSOLEREADER = new scconsole.scconsolereader(_main_object);

            SCGLOB.SCCONSOLECORE = SCCONSOLECORE;
            //SCGLOB.SCCONSOLEWRITER = SCCONSOLEWRITER;
            //SCGLOB.SCCONSOLEREADER = SCCONSOLEREADER;       
        }
    }
}
