namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysMenuPermission")]
    public partial class SysMenuPermission
    {
        public int Id { get; set; }

        public int MenuId { get; set; }

        public int PermissionId { get; set; }

        public virtual SysMenu SysMenu { get; set; }

        public virtual SysPermission SysPermission { get; set; }
    }
}
