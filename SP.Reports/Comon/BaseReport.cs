﻿using SP.Base;
using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Comon
{
    public class BaseReport
    {
        public SPBaseModel _db { get; set; }

        public BaseReport()
        {
            _db = Database.SPBase();
        }


        public string GetTemlate(int rep_id)
        {
            return _db.Reports.FirstOrDefault(w => w.RepId == rep_id).TemlateName;
        }
    }
}
