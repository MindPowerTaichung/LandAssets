﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LandAssets.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LandAssetsEntities : DbContext
    {
        public LandAssetsEntities()
            : base("name=LandAssetsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LandsBefore> LandsBefores { get; set; }
        public virtual DbSet<RegisterReasonType> RegisterReasonTypes { get; set; }
        public virtual DbSet<LandType> LandTypes { get; set; }
    }
}