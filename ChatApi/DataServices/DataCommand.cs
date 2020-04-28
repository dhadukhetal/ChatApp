using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChatApi.DataServices
{
    public class DataCommand
    {
        public string Command;
        public System.Data.CommandType Type;
        public List<SqlParameter> Parameters = new List<SqlParameter>();
    }
}