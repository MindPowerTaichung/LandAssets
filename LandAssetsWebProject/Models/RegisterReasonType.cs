//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LandAssetsWebProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RegisterReasonType
    {
        public RegisterReasonType()
        {
            this.LandsBefores = new HashSet<LandsBefore>();
        }
    
        public int Id { get; set; }
        public string Reason { get; set; }
        public byte[] Timestamp { get; set; }
    
        public virtual ICollection<LandsBefore> LandsBefores { get; set; }
    }
}
