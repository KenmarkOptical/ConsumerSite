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
    
    public partial class usp_survey_sel_by_customer_Result
    {
        public int customer_id { get; set; }
        public int survey_code { get; set; }
        public string previously_completed { get; set; }
        public Nullable<short> question_1 { get; set; }
        public Nullable<short> question_2 { get; set; }
        public Nullable<short> question_3 { get; set; }
        public Nullable<short> question_4 { get; set; }
        public Nullable<short> question_5 { get; set; }
        public Nullable<short> question_6 { get; set; }
        public string question_7 { get; set; }
        public string question_8 { get; set; }
        public string comments { get; set; }
        public short winning_code { get; set; }
        public System.DateTime survey_dt { get; set; }
    }
}
