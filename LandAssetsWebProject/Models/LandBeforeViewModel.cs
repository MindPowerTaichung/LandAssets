using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandAssetsWebProject.Models
{
    public class LandBeforeViewModel
    {
        public string NoA { get; set; }
        public string NoB { get; set; }
        public string NoC { get; set; }
        public string RegisterNo { get; set; }
        public Nullable<System.DateTime> RegisterDate { get; set; }
        public Nullable<int> RegisterReasonId { get; set; }
        public Nullable<int> LandTypeId { get; set; }
        public Nullable<double> LandArea { get; set; }
        public Nullable<int> CurrentValue { get; set; }
        public Nullable<int> AnnourceValue { get; set; }
        public string TimestampString { get; set; }

        public string RegisterReasonType { get; set; }
        public string LandType { get; set; }
    }
}