using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Connection
{
    public class ConnectionParams
    {
        //TODO: Ukoliko koristite SUBP u VM, a Visual Studio van VM promenite localhost sa IP adresom VM
        public static readonly string LOCAL_DATA_SOURCE = "//localhost:1521/xe";
        public static readonly string CLASSROOM_DATA_SOURCE = "//192.168.0.102:1522/db2016";

        //TODO: promeniti username i password
        public static readonly string USER_ID = "Student";
        public static readonly string PASSWORD = "ftn";
    }
}
