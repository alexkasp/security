using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandBox.Data
{
    [Serializable]
    public class ManageStatusPacket
    {
        public Boolean ExternalClient;
        public Boolean InternalServer;
        public Boolean DataBase;
    }
}
