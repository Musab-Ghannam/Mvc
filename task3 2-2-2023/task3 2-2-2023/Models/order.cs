//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace task3_2_2_2023.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class order
    {
        public int orderId { get; set; }
       
        public Nullable<int> id { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; }
    
        public virtual information information { get; set; }
    }
}
