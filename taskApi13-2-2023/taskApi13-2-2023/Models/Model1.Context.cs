﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace taskApi13_2_2023.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class universityEntities : DbContext
    {
        public universityEntities()
            : base("name=universityEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<specialization> specializations { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}