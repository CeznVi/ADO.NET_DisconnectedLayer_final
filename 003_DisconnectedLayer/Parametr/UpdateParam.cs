using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _003_DisconnectedLayer.Parametr
{
    internal class UpdateParam
    {
        public string NameCol { get; set; }
        public DbParameter Parameter { get; set; }
    }
}
