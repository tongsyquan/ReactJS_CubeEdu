//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FBPost
    {
        public string ID { get; set; }
        public string PageID { get; set; }
        public string ParentID { get; set; }
        public string Description { get; set; }
        public string PostContent { get; set; }
        public string FBLink { get; set; }
        public Nullable<System.DateTime> FBCreateTime { get; set; }
        public string ReplyHasPhone { get; set; }
        public string ReplyNoPhone { get; set; }
        public Nullable<bool> IsHidePhone { get; set; }
        public Nullable<bool> IsHideNoPhone { get; set; }
        public bool IsSkip { get; set; }
    }
}
