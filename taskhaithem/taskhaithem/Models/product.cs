//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace taskhaithem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productdescription { get; set; }
        public string productpic { get; set; }
        public Nullable<int> productprice { get; set; }
        public Nullable<int> categoryId { get; set; }
    
        public virtual Category Category { get; set; }
    }
}