//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GlitterTweeting.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tweet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tweet()
        {
            this.LikeTweet = new HashSet<LikeTweet>();
            this.Tag = new HashSet<Tag>();
        }
    
        public System.Guid ID { get; set; }
        public System.Guid UserID { get; set; }
        public string Message { get; set; }
        public System.DateTime CreatedAt { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikeTweet> LikeTweet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tag { get; set; }
        public virtual User User { get; set; }
    }
}