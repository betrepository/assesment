//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BET.eCommerce.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Carts
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<bool> CheckedOut { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}