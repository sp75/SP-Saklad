﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.Common
{
    public static class UserSession
    {
        public static Guid SessionId { get; set; }
        public static int UserId { get; set; }
    }
}
