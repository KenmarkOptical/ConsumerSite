//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kenmark_Consumer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ARTerm
    {
        public string TermsID { get; set; }
        public string Description { get; set; }
        public string TermsType { get; set; }
        public Nullable<decimal> NumberPayments { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<decimal> FirstNetDueDate { get; set; }
        public Nullable<decimal> FirstDiscDueDate { get; set; }
        public Nullable<decimal> CutoffDay { get; set; }
        public Nullable<decimal> NextDueDate { get; set; }
        public Nullable<decimal> NextDiscDueDate { get; set; }
        public Nullable<System.DateTime> SpecificDueDate { get; set; }
        public Nullable<System.DateTime> SpecificDiscDueDate { get; set; }
    }
}
