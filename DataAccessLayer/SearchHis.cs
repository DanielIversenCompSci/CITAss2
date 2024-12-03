﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SearchHis
    {
        public int UserId { get; set; }
        public string SearchQuery { get; set; }
        public DateTime SearchTimeStamp { get; set; }
        
        public int SearchId { get; set; }
        
        public Users User { get; set; }
    }
}
