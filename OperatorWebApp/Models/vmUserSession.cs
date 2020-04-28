using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OperatorWebApp.Models
{
    public class vmUserSession
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserToken { get; set; }
    }

    public class vmOperator
    { 
        public int OperatorId { get; set; }
        public string DisplayName { get; set; }
        public string OperatorMaxUserConnectionLimit { get; set; }
        public string OnHoldDefaultMessage { get; set; }
    }
}