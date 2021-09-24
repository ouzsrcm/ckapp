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
    
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> PersonId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> OrderStateId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string Address { get; set; }
        public string ModelDis { get; set; }
        public string ModelIc { get; set; }
        public string KasaRengiDis { get; set; }
        public string KasaRengiIc { get; set; }
        public string KaplamaDis { get; set; }
        public string KaplamaIc { get; set; }
        public string KapiYonu { get; set; }
        public string DuvarKalinligi { get; set; }
        public string KasaEni { get; set; }
        public string KDKasaBoyu { get; set; }
        public string KDPervazSag { get; set; }
        public string KDPervazSol { get; set; }
        public string KDPervazUst { get; set; }
        public string KIKasaBoyu { get; set; }
        public string KIPervazSag { get; set; }
        public string KIPervazSol { get; set; }
        public string KIPervazUst { get; set; }
        public string KapiMarkasi { get; set; }
        public string KanatTuru { get; set; }
        public string KasaTuru { get; set; }
        public string KiliDurumu { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Description { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual District District { get; set; }
        public virtual Order Order { get; set; }
        public virtual OrderState OrderState { get; set; }
        public virtual Person Person { get; set; }
    }
}