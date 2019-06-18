namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysAccountRole")]
    public partial class SysAccountRole
    {
        public int Id { get; set; }

        public long AccountId { get; set; }

        public int RoleId { get; set; }

        public virtual SysAccount SysAccount { get; set; }

        public virtual SysRole SysRole { get; set; }
    }
}
