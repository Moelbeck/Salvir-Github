﻿using depross.Common;
using depross.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace depross.Model.Log
{
    public class LogUserSaleListing : BaseLog
    {
        public int SaleListingID { get; set; }

        public eLogSaleListingType LogType{get;set;}
    }
}