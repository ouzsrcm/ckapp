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
    
    public partial class RoleModule
    {
        public int RoleModuleId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> ModuleId { get; set; }
        public Nullable<int> PersonId { get; set; }
    
        public virtual Module Module { get; set; }
        public virtual Person Person { get; set; }
        public virtual Role Role { get; set; }
    }
}
