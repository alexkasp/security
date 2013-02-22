using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SandBox.WebUi
{
    public class Data
    {
        public struct StrVmReady
        {
            public byte Id;
            public byte Type;
            public byte[] Addr;
        }

        public struct StrVmComplete
        {
            public byte Id;
            public byte Type;
        }
    }//end class
}//end namespace