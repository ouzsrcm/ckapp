//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConversationMessage
    {
        public int ConversationMessageId { get; set; }
        public Nullable<int> ConversationId { get; set; }
        public Nullable<int> FromPersonId { get; set; }
        public Nullable<int> ToPersonId { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<bool> IsRead { get; set; }
    
        public virtual Conversation Conversation { get; set; }
    }
}
