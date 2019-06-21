using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblCustomer
    {
        public TblCustomer()
        {
            TblCustomerAddress = new HashSet<TblCustomerAddress>();
            TblCustomerContact = new HashSet<TblCustomerContact>();
          /*  TblQuotation = new HashSet<TblQuotation>();
            TblSaleOrder = new HashSet<TblSaleOrder>();*/
        }

        public int CustomerId { get; set; }
        public int? PrefixId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAvatar { get; set; }
        public string CustomerTaxId { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerFax { get; set; }
        public string CustomerEmail { get; set; }
        public int CustomerType { get; set; }
        public int? GroupId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TblCustomerGroup Group { get; set; }
        public virtual EnmPrefix Prefix { get; set; }
        public virtual ICollection<TblCustomerAddress> TblCustomerAddress { get; set; }
        public virtual ICollection<TblCustomerContact> TblCustomerContact { get; set; }
      /*  public virtual ICollection<TblQuotation> TblQuotation { get; set; }
        public virtual ICollection<TblSaleOrder> TblSaleOrder { get; set; }*/
    }
}
