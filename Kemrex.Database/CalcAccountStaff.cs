namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CalcAccountStaff")]
    public partial class CalcAccountStaff
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccountId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long StaffId { get; set; }

        public string StaffRemark { get; set; }

        public virtual SysAccount SysAccount { get; set; }

        public virtual SysAccount SysAccount1 { get; set; }
    }
}
