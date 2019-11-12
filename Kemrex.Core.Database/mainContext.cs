using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace Kemrex.Core.Database
{
    public partial class mainContext : DbContext
    {
        private readonly string constr;
        private mainContext()
        {
        }

        public mainContext(string _constr)
        {
            constr = _constr;
        }

        public mainContext(DbContextOptions<mainContext> options) : base(options)
        {
        }

        public mainContext(string _constr, DbContextOptions<mainContext> options) : base(options)
        {
            constr = _constr;
        }

        public virtual DbSet<CalcAccountStaff> CalcAccountStaff { get; set; }
        public virtual DbSet<EnmPaymentCondition> EnmPaymentCondition { get; set; }
        public virtual DbSet<EnmPaymentConditionTerm> EnmPaymentConditionTerm { get; set; }
        public virtual DbSet<EnmPrefix> EnmPrefix { get; set; }
        public virtual DbSet<EnmStatusEmployee> EnmStatusEmployee { get; set; }
        public virtual DbSet<EnmStatusQuotation> EnmStatusQuotation { get; set; }
        public virtual DbSet<SysAccount> SysAccount { get; set; }
        public virtual DbSet<SysAccountRole> SysAccountRole { get; set; }
        public virtual DbSet<SysCategory> SysCategory { get; set; }
        public virtual DbSet<SysCategoryType> SysCategoryType { get; set; }
        public virtual DbSet<SysDistrict> SysDistrict { get; set; }
        public virtual DbSet<SysGeography> SysGeography { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysMenuActive> SysMenuActive { get; set; }
        public virtual DbSet<SysMenuPermission> SysMenuPermission { get; set; }
        public virtual DbSet<SysParameter> SysParameter { get; set; }
        public virtual DbSet<SysPermission> SysPermission { get; set; }
        public virtual DbSet<SysPostcode> SysPostcode { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRolePermission> SysRolePermission { get; set; }
        public virtual DbSet<SysSite> SysSite { get; set; }
        public virtual DbSet<SysState> SysState { get; set; }
        public virtual DbSet<SysSubDistrict> SysSubDistrict { get; set; }
        public virtual DbSet<TblAccountTeam> TblAccountTeam { get; set; }
        public virtual DbSet<TblAccountTeamMember> TblAccountTeamMember { get; set; }
        public virtual DbSet<TblCalLoad> TblCalLoad { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<TblCustomerAddress> TblCustomerAddress { get; set; }
        public virtual DbSet<TblCustomerContact> TblCustomerContact { get; set; }
        public virtual DbSet<TblCustomerGroup> TblCustomerGroup { get; set; }
        public virtual DbSet<TblDepartment> TblDepartment { get; set; }
        public virtual DbSet<TblDepartmentPosition> TblDepartmentPosition { get; set; }
        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<TblInvoice> TblInvoice { get; set; }
        public virtual DbSet<TblJobOrder> TblJobOrder { get; set; }
        public virtual DbSet<TblJobOrderAttachmentType> TblJobOrderAttachmentType { get; set; }
        public virtual DbSet<TblJobOrderDetail> TblJobOrderDetail { get; set; }
        public virtual DbSet<TblJobOrderEquipmentType> TblJobOrderEquipmentType { get; set; }
        public virtual DbSet<TblJobOrderLandType> TblJobOrderLandType { get; set; }
        public virtual DbSet<TblJobOrderObstructionType> TblJobOrderObstructionType { get; set; }
        public virtual DbSet<TblJobOrderProjectType> TblJobOrderProjectType { get; set; }
        public virtual DbSet<TblJobOrderProperties> TblJobOrderProperties { get; set; }
        public virtual DbSet<TblJobOrderUndergroundType> TblJobOrderUndergroundType { get; set; }
        public virtual DbSet<TblJobOrderSurveyDetail> TblJobOrderSurveyDetail { get; set; }
        public virtual DbSet<TblEmployeeUserPermission> TblEmployeeUserPermission { get; set; }

        public virtual DbSet<TblKpt> TblKpt { get; set; }
        public virtual DbSet<TblKptAttachment> TblKptAttachment { get; set; }
        public virtual DbSet<TblKptDetail> TblKptDetail { get; set; }
        public virtual DbSet<TblKptStation> TblKptStation { get; set; }
        public virtual DbSet<TblKptStationAttachment> TblKptStationAttachment { get; set; }
        public virtual DbSet<TblKptStationDetail> TblKptStationDetail { get; set; }
        public virtual DbSet<TblPile> TblPile { get; set; }
        public virtual DbSet<TblPileSeries> TblPileSeries { get; set; }
        public virtual DbSet<TblPosition> TblPosition { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblWareHouse> TblWareHouse { get; set; }
        public virtual DbSet<TblProductCategory> TblProductCategory { get; set; }
        public virtual DbSet<TblProductModel> TblProductModel { get; set; }
        public virtual DbSet<TblProductType> TblProductType { get; set; }
        public virtual DbSet<TblQuotation> TblQuotation { get; set; }
        public virtual DbSet<TblQuotationDetail> TblQuotationDetail { get; set; }
        public virtual DbSet<TblSaleOrder> TblSaleOrder { get; set; }
        public virtual DbSet<TblSaleOrderAttachment> TblSaleOrderAttachment { get; set; }
        public virtual DbSet<TblSaleOrderDetail> TblSaleOrderDetail { get; set; }
        public virtual DbSet<TblUnit> TblUnit { get; set; }
        public virtual DbSet<TeamOperation> TeamOperation { get; set; }
        public virtual DbSet<TeamOperationDetail> TeamOperationDetail { get; set; }
        public virtual DbSet<TeamSale> TeamSale { get; set; }
        public virtual DbSet<TeamSaleDetail> TeamSaleDetail { get; set; }
        public virtual DbSet<TransferDetail> TransferDetail { get; set; }
        public virtual DbSet<TransferHeader> TransferHeader { get; set; }
        public virtual DbSet<TransferStockDetail> TransferStockDetail { get; set; }
        public virtual DbSet<TransferStockHeader> TransferStockHeader { get; set; }
        public virtual DbSet<SysSurveyDetailTemplate> SysSurveyDetailTemplate { get; set; }
        public virtual DbSet<SysSurveyHeaderSubTemplate> SysSurveyHeaderSubTemplate { get; set; }
        public virtual DbSet<SysSurveyHeaderTemplate> SysSurveyHeaderTemplate { get; set; }
        public virtual DbSet<TblSurveyDetail> TblSurveyDetail { get; set; }
        public virtual DbSet<TblSurveyHeader> TblSurveyHeader { get; set; }
        public virtual DbSet<TransferHeaderReference> TransferHeaderReference { get; set; }
        public virtual DbSet<TblProductOfWareHouse> TblProductOfWareHouse { get; set; }

        public virtual DbSet<TblQuotationDetailTemplate> TblQuotationDetailTemplate { get; set; }
        public virtual DbSet<TblQuotationTemplate> TblQuotationTemplate { get; set; }
        public virtual DbSet<TblPayment> TblPayment { get; set; }
        public virtual DbSet<TblBankAccount> TblBankAccount { get; set; }


        // Unable to generate entity type for table 'dbo.Product_Temp'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            { optionsBuilder.UseSqlServer(constr); }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<CalcAccountStaff>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.StaffId });

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.CalcAccountStaffAccount)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalcAccountStaff_Account");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.CalcAccountStaffStaff)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalcAccountStaff_Staff");
            });

            modelBuilder.Entity<EnmPaymentCondition>(entity =>
            {
                entity.HasKey(e => e.ConditionId);

                entity.Property(e => e.ConditionId).ValueGeneratedNever();

                entity.Property(e => e.ConditionName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ConditionTerm).HasDefaultValueSql("((1))");

                entity.Property(e => e.FlagActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<EnmPaymentConditionTerm>(entity =>
            {
                entity.HasKey(e => new { e.ConditionId, e.TermNo });

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.EnmPaymentConditionTerm)
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnmPaymentConditionTerm_Id");
            });

            modelBuilder.Entity<EnmPrefix>(entity =>
            {
                entity.HasKey(e => e.PrefixId);

                entity.Property(e => e.PrefixId).ValueGeneratedNever();

                entity.Property(e => e.PrefixNameEn)
                    .IsRequired()
                    .HasColumnName("PrefixNameEN")
                    .HasMaxLength(100);

                entity.Property(e => e.PrefixNameTh)
                    .IsRequired()
                    .HasColumnName("PrefixNameTH")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EnmStatusEmployee>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.StatusId).ValueGeneratedNever();

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EnmStatusQuotation>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.StatusId).ValueGeneratedNever();

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SysAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.HasIndex(e => e.AccountEmail)
                    .HasName("IX_SysAccount_Email");

                entity.HasIndex(e => e.AccountUsername)
                    .HasName("IX_SysAccount_Username");

                entity.Property(e => e.AccountEmail)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.AccountFirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.AccountLastName).HasMaxLength(200);

                entity.Property(e => e.AccountPassword).HasMaxLength(100);

                entity.Property(e => e.AccountUsername)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlagStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SysAccountRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.SysAccountRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SysAccountRole_Role");
            });

            modelBuilder.Entity<SysCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__SysCateg__19093A0BF80B83F0");

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.HasOne(d => d.CategoryType)
                    .WithMany(p => p.SysCategory)
                    .HasForeignKey(d => d.CategoryTypeId)
                    .HasConstraintName("FK__SysCatego__Categ__19B5BC39");
            });

            modelBuilder.Entity<SysCategoryType>(entity =>
            {
                entity.HasKey(e => e.CategoryTypeId)
                    .HasName("PK__SysCateg__7B30E7A30A46A2FF");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<SysDistrict>(entity =>
            {
                entity.HasKey(e => e.DistrictId);

                entity.Property(e => e.DistrictCode).HasMaxLength(4);

                entity.Property(e => e.DistrictNameEn)
                    .HasColumnName("DistrictNameEN")
                    .HasMaxLength(256);

                entity.Property(e => e.DistrictNameTh)
                    .HasColumnName("DistrictNameTH")
                    .HasMaxLength(256);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.SysDistrict)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_SysDistrict_Province");
            });

            modelBuilder.Entity<SysGeography>(entity =>
            {
                entity.HasKey(e => e.GeoId);

                entity.Property(e => e.GeoNameEn)
                    .HasColumnName("GeoNameEN")
                    .HasMaxLength(256);

                entity.Property(e => e.GeoNameTh)
                    .HasColumnName("GeoNameTH")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.MenuId).ValueGeneratedNever();

                entity.Property(e => e.FlagActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuIcon).HasMaxLength(500);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MvcAction)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MvcArea)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MvcController)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_SysMenu_Parent");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SysMenu)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SysMenu_Site");
            });

            modelBuilder.Entity<SysMenuActive>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MvcAction)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MvcArea)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MvcController)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.SysMenuActive)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SysMenuActive_Id");
            });

            modelBuilder.Entity<SysMenuPermission>(entity =>
            {
                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.SysMenuPermission)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SysMenuPermission_Menu");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.SysMenuPermission)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SysMenuPermission_Permission");
            });

            modelBuilder.Entity<SysParameter>(entity =>
            {
                entity.HasKey(e => e.ParamName);

                entity.Property(e => e.ParamName)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.ParamType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParamValue).IsRequired();

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SysPermission>(entity =>
            {
                entity.HasKey(e => e.PermissionId);

                entity.Property(e => e.PermissionId).ValueGeneratedNever();

                entity.Property(e => e.PermissionName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SysPostcode>(entity =>
            {
                entity.HasKey(e => new { e.DistrictId, e.Postcode });

                entity.Property(e => e.Postcode).HasMaxLength(20);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.SysPostcode)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_SysPostcode_District");
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.HasIndex(e => e.SiteId)
                    .HasName("IX_SysRole_Site");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlagActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SysRole)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SysRole_Site");
            });

            modelBuilder.Entity<SysRolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.MenuId, e.PermissionId });
            });

            modelBuilder.Entity<SysSite>(entity =>
            {
                entity.HasKey(e => e.SiteId);

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SysState>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.Property(e => e.StateCode).HasMaxLength(2);

                entity.Property(e => e.StateNameEn)
                    .HasColumnName("StateNameEN")
                    .HasMaxLength(256);

                entity.Property(e => e.StateNameTh)
                    .HasColumnName("StateNameTH")
                    .HasMaxLength(256);

                entity.HasOne(d => d.Geo)
                    .WithMany(p => p.SysState)
                    .HasForeignKey(d => d.GeoId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_SysState_Geopraphy");
            });

            modelBuilder.Entity<SysSubDistrict>(entity =>
            {
                entity.HasKey(e => e.SubDistrictId);

                entity.Property(e => e.Postcode).HasMaxLength(10);

                entity.Property(e => e.SubDistrictCode).HasMaxLength(6);

                entity.Property(e => e.SubDistrictNameEn)
                    .HasColumnName("SubDistrictNameEN")
                    .HasMaxLength(256);

                entity.Property(e => e.SubDistrictNameTh)
                    .HasColumnName("SubDistrictNameTH")
                    .HasMaxLength(256);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.SysSubDistrict)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_SysSubDistrict_District");
            });

            modelBuilder.Entity<TblAccountTeam>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblAccountTeamMember>(entity =>
            {
                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TblAccountTeamMember)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblAccountTeamMember_Id");
            });

            modelBuilder.Entity<TblCalLoad>(entity =>
            {
                entity.HasKey(e => e.CalId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InputC).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.InputDegree).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.InputSafeLoad).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.ProjectName).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerAvatar).HasMaxLength(500);

                entity.Property(e => e.CustomerEmail).HasMaxLength(200);

                entity.Property(e => e.CustomerFax).HasMaxLength(50);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CustomerPhone).HasMaxLength(50);

                entity.Property(e => e.CustomerTaxId).HasMaxLength(20);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TblCustomer)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_TblCustomer_Group");

                entity.HasOne(d => d.Prefix)
                    .WithMany(p => p.TblCustomer)
                    .HasForeignKey(d => d.PrefixId)
                    .HasConstraintName("FK_TblCustomer_Prefix");
            });

            modelBuilder.Entity<TblCustomerAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.Property(e => e.AddressContact).HasMaxLength(500);

                entity.Property(e => e.AddressContactEmail).HasMaxLength(500);

                entity.Property(e => e.AddressContactPhone).HasMaxLength(500);

                entity.Property(e => e.AddressName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AddressPostcode).HasMaxLength(10);

                entity.Property(e => e.AddressValue)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblCustomerAddress)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblCustomerAddress_Id");

                entity.HasOne(d => d.SubDistrict)
                    .WithMany(p => p.TblCustomerAddress)
                    .HasForeignKey(d => d.SubDistrictId)
                    .HasConstraintName("FK_TblCustomerAddress_SubDistrict");
            });

            modelBuilder.Entity<TblCustomerContact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.Property(e => e.ContactEmail).HasMaxLength(500);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ContactPhone).HasMaxLength(50);

                entity.Property(e => e.ContactPosition).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblCustomerContact)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblCustomerContact_Id");
            });

            modelBuilder.Entity<TblCustomerGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.GroupOrder).HasDefaultValueSql("((9999))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblDepartmentPosition>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblDepartmentPosition)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblDepartmentPosition_Department");
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.HasIndex(e => e.EmpCode)
                    .HasName("UK_TblEmployee_Code")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmpAddress).HasMaxLength(500);

                entity.Property(e => e.EmpApplyDate).HasColumnType("datetime");

                entity.Property(e => e.EmpCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EmpEmail).HasMaxLength(250);

                entity.Property(e => e.EmpMobile)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.EmpNameEn)
                    .IsRequired()
                    .HasColumnName("EmpNameEN")
                    .HasMaxLength(500);

                entity.Property(e => e.EmpNameTh)
                    .IsRequired()
                    .HasColumnName("EmpNameTH")
                    .HasMaxLength(500);

                entity.Property(e => e.EmpPid)
                    .HasColumnName("EmpPID")
                    .HasMaxLength(13);

                entity.Property(e => e.EmpPostcode).HasMaxLength(10);

                entity.Property(e => e.EmpPromoteDate).HasColumnType("datetime");

                entity.Property(e => e.EmpSignature).HasMaxLength(300);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblEmployee)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_TblEmployee_Department");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.InverseLead)
                    .HasForeignKey(d => d.LeadId)
                    .HasConstraintName("FK_TblEmployee_Lead");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.TblEmployee)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_TblEmployee_Position");

                entity.HasOne(d => d.Prefix)
                    .WithMany(p => p.TblEmployee)
                    .HasForeignKey(d => d.PrefixId)
                    .HasConstraintName("FK_TblEmployee_Prefix");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblEmployee)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblEmployee_Status");
            });

            modelBuilder.Entity<TblInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.SaleOrder)
                    .WithMany(p => p.TblInvoice)
                    .HasForeignKey(d => d.SaleOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblInvoice_SaleOrder");
            });

            modelBuilder.Entity<TblJobOrder>(entity =>
            {
                entity.HasKey(e => e.JobOrderId)
                    .HasName("PK__TblJobOr__EACFC526C19E50C7");

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Adapter).HasMaxLength(100);

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerEmail).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.CustomerPhone).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EndWorkingTime).HasMaxLength(10);

                entity.Property(e => e.HouseNo).HasMaxLength(50);

                entity.Property(e => e.JobName).HasMaxLength(50);

                entity.Property(e => e.JobOrderNo).HasMaxLength(50);

                entity.Property(e => e.Other).HasMaxLength(100);

                entity.Property(e => e.ProductSaftyFactory).HasMaxLength(50);

                entity.Property(e => e.ProductWeight).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ProjectName).HasMaxLength(100);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.Road).HasMaxLength(100);

                entity.Property(e => e.Solution).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartWorkingTime).HasMaxLength(10);

                entity.Property(e => e.SurveyDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.VillageNo).HasMaxLength(50);
            });

            modelBuilder.Entity<TblJobOrderAttachmentType>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.AttachmentTypeId })
                    .HasName("PK__TblJobOr__BF09FF903427230F");

                entity.ToTable("TblJobOrder_AttachmentType");

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Caption).HasMaxLength(100);

                entity.HasOne(d => d.AttachmentType)
                    .WithMany(p => p.TblJobOrderAttachmentType)
                    .HasForeignKey(d => d.AttachmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblJobOrd__Attac__3C0AD43D");
            });

            modelBuilder.Entity<TblJobOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.No });

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");

                /*    entity.HasOne(d => d.JobOrder)
                      .WithMany(p => p.TblJobOrderDetail)
                      .HasForeignKey(d => d.JobOrderId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__TblJobOrd__JobOr__066DDD9B");*/
            });

            modelBuilder.Entity<TblJobOrderEquipmentType>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.EquipmentTypeId })
                    .HasName("PK__TblJobOr__3F9BB749D923A672");

                entity.ToTable("TblJobOrder_EquipmentType");

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Caption).HasMaxLength(100);

                entity.HasOne(d => d.EquipmentType)
                    .WithMany(p => p.TblJobOrderEquipmentType)
                    .HasForeignKey(d => d.EquipmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblJobOrd__Equip__392E6792");
            });

            modelBuilder.Entity<TblEmployeeUserPermission>(entity =>
            {
                entity.HasKey(e => new { e.EmpId, e.FunId, e.ViewEmpId });

                entity.ToTable("TblEmployee_UserPermission");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TblJobOrderLandType>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.LandTypeId })
                    .HasName("PK__TblJobOr__84B262F182E26026");

                entity.ToTable("TblJobOrder_LandType");

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Caption).HasMaxLength(100);

                entity.HasOne(d => d.LandType)
                    .WithMany(p => p.TblJobOrderLandType)
                    .HasForeignKey(d => d.LandTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblJobOrd__LandT__2DBCB4E6");
            });

            modelBuilder.Entity<TblJobOrderObstructionType>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.ObstructionTypeId })
                    .HasName("PK__TblJobOr__1B281B0443CD5E0F");

                entity.ToTable("TblJobOrder_ObstructionType");

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Caption).HasMaxLength(100);

                entity.HasOne(d => d.ObstructionType)
                    .WithMany(p => p.TblJobOrderObstructionType)
                    .HasForeignKey(d => d.ObstructionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblJobOrd__Obstr__3651FAE7");
            });

            modelBuilder.Entity<TblJobOrderProjectType>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.ProjectTypeId })
                    .HasName("PK__TblJobOr__CC3D80C0BC8E8C5D");

                entity.ToTable("TblJobOrder_ProjectType");

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Caption).HasMaxLength(100);

                entity.HasOne(d => d.ProjectType)
                    .WithMany(p => p.TblJobOrderProjectType)
                    .HasForeignKey(d => d.ProjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblJobOrd__Proje__2062B9C8");
            });

            modelBuilder.Entity<TblJobOrderProperties>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.No });

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");

            });

            modelBuilder.Entity<TblJobOrderUndergroundType>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.UndergroundTypeId })
                    .HasName("PK__TblJobOr__6DAF5D38B94FC145");

                entity.ToTable("TblJobOrder_UndergroundType");

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.Caption).HasMaxLength(100);

                entity.HasOne(d => d.UndergroundType)
                    .WithMany(p => p.TblJobOrderUndergroundType)
                    .HasForeignKey(d => d.UndergroundTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblJobOrd__Under__33758E3C");
            });


            modelBuilder.Entity<TblJobOrderSurveyDetail>(entity =>
            {
                entity.HasKey(e => new { e.JobOrderId, e.SurveyDetailId })
                    .HasName("PK__TblJobOrder_SurveyDetail");

                entity.ToTable("TblJobOrder_SurveyDetail");

                entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");

                entity.Property(e => e.SurveyDetailId)
                    .HasColumnName("SurveyDetailID")
                    .HasMaxLength(50);

                entity.Property(e => e.Desc).HasMaxLength(400);

                entity.HasOne(d => d.JobOrder)
                    .WithMany(p => p.SurveyDetail)
                    .HasForeignKey(d => d.JobOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblJobOrder_SurveyDetail_TblJobOrder");
            });


            modelBuilder.Entity<TblKpt>(entity =>
            {
                entity.HasKey(e => e.KptId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.KptDate).HasColumnType("datetime");

                entity.Property(e => e.KptLatitude).HasMaxLength(50);

                entity.Property(e => e.KptLocation).HasMaxLength(200);

                entity.Property(e => e.KptLongtitude).HasMaxLength(50);

                entity.Property(e => e.KptStation).HasMaxLength(100);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TestBy).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.SubDistrict)
                    .WithMany(p => p.TblKpt)
                    .HasForeignKey(d => d.SubDistrictId)
                    .HasConstraintName("FK_TblKpt_SubDistrict");
            });

            modelBuilder.Entity<TblKptAttachment>(entity =>
            {
                entity.HasKey(e => e.AttachId);

                entity.Property(e => e.AttachName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.AttachPath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Kpt)
                    .WithMany(p => p.TblKptAttachment)
                    .HasForeignKey(d => d.KptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblKptAttachment_Id");
            });

            modelBuilder.Entity<TblKptDetail>(entity =>
            {
                entity.HasOne(d => d.Kpt)
                    .WithMany(p => p.TblKptDetail)
                    .HasForeignKey(d => d.KptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblKptDetail_Id");
            });

            modelBuilder.Entity<TblKptStation>(entity =>
            {
                entity.HasKey(e => e.StationId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StationName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StationTestBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Kpt)
                    .WithMany(p => p.TblKptStation)
                    .HasForeignKey(d => d.KptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblKptStation_Id");
            });

            modelBuilder.Entity<TblKptStationAttachment>(entity =>
            {
                entity.HasKey(e => e.AttachId);

                entity.Property(e => e.AttachName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.AttachPath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.TblKptStationAttachment)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblKptStationAttachment_Id");
            });

            modelBuilder.Entity<TblKptStationDetail>(entity =>
            {
                entity.HasOne(d => d.Station)
                    .WithMany(p => p.TblKptStationDetail)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblKptStationDetail_Id");
            });

            modelBuilder.Entity<TblPile>(entity =>
            {
                entity.HasKey(e => e.PileId);

                entity.Property(e => e.PileBlade).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PileDia).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.PileFlangeLength).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PileFlangeWidth).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PileName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PileSpiralDepth).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.PileSpiralLength).HasColumnType("decimal(5, 4)");

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.TblPile)
                    .HasForeignKey(d => d.SeriesId)
                    .HasConstraintName("FK_TblPile_Series");
            });

            modelBuilder.Entity<TblPileSeries>(entity =>
            {
                entity.HasKey(e => e.SeriesId);

                entity.Property(e => e.SeriesImage)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SeriesName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblPosition>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.HasIndex(e => e.ProductCode)
                    .HasName("UK_TblProduct_Code")
                    .IsUnique();

                entity.Property(e => e.CostNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CostTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CostVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlagActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FlagVat)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PriceNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProductNameBilling)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProductNameTrade)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProduct)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblProduct_Category");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.TblProduct)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_TblProduct_Model");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TblProduct)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblProduct_Unit");
            });

            modelBuilder.Entity<TblProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlagActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblProductModel>(entity =>
            {
                entity.HasKey(e => e.ModelId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlagActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProductModel)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblProductModel_Category");
            });

            modelBuilder.Entity<TblProductType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlagActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TblProductOfWareHouse>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.Whid });

                entity.Property(e => e.Whid).HasColumnName("WHId");

                entity.Property(e => e.CostNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CostTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CostVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PriceNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblWareHouse>(entity =>
            {
                entity.HasKey(e => e.Whid)
                    .HasName("PK__TblWareHouse");

                entity.Property(e => e.Whid).HasColumnName("WHId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Whcode)
                    .HasColumnName("WHCode")
                    .HasMaxLength(100);

                entity.Property(e => e.Whname)
                    .HasColumnName("WHName")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblQuotation>(entity =>
            {
                entity.HasKey(e => e.QuotationId);

                entity.Property(e => e.ContractEmail).HasMaxLength(500);

                entity.Property(e => e.ContractName).HasMaxLength(500);

                entity.Property(e => e.ContractPhone).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(500);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountCash).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountNet)
                    .HasColumnType("decimal(12, 2)");
                //  .HasComputedColumnSql("([dbo].[cplQuotationDiscountNet]([QuotationId]))");

                entity.Property(e => e.DiscountTot)
                    .HasColumnType("decimal(12, 2)");
                // .HasComputedColumnSql("([dbo].[cplQuotationDiscountTot]([QuotationId]))");

                entity.Property(e => e.DiscountVat)
                    .HasColumnType("decimal(12, 2)");
                //  .HasComputedColumnSql("([dbo].[cplQuotationDiscountVat]([QuotationId]))");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.OperationEndDate).HasColumnType("datetime");

                entity.Property(e => e.OperationStartDate).HasColumnType("datetime");

                entity.Property(e => e.QuotationDate).HasColumnType("datetime");

                entity.Property(e => e.QuotationNo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.QuotationValidDay).HasDefaultValueSql("((30))");

                entity.Property(e => e.SaleName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SubTotalNet)
                    .HasColumnType("decimal(12, 2)");
                // .HasComputedColumnSql("([dbo].[cplQuotationPriceNet]([QuotationId]))");

                entity.Property(e => e.SubTotalTot)
                    .HasColumnType("decimal(12, 2)");
                //   .HasComputedColumnSql("([dbo].[cplQuotationPriceTot]([QuotationId]))");

                entity.Property(e => e.SubTotalVat)
                    .HasColumnType("decimal(12, 2)");
                // .HasComputedColumnSql("([dbo].[cplQuotationPriceVat]([QuotationId]))");

                entity.Property(e => e.SummaryNet)
                    .HasColumnType("decimal(12, 2)");
                //   .HasComputedColumnSql("([dbo].[cplQuotationSummaryNet]([QuotationId]))");

                entity.Property(e => e.SummaryTot)
                    .HasColumnType("decimal(12, 2)");
                //   .HasComputedColumnSql("([dbo].[cplQuotationSummaryTot]([QuotationId]))");

                entity.Property(e => e.SummaryVat)
                    .HasColumnType("decimal(12, 2)");
                //    .HasComputedColumnSql("([dbo].[cplQuotationSummaryVat]([QuotationId]))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                /*    entity.HasOne(d => d.Customer)
                      .WithMany(p => p.TblQuotation)
                      .HasForeignKey(d => d.CustomerId)
                      .HasConstraintName("FK_TblQuotation_Customer");*/

                entity.HasOne(d => d.Sale)
                    .WithMany(p => p.TblQuotation)
                    .HasForeignKey(d => d.SaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblQuotation_Sale");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblQuotation)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblQuotation_Status");
            });

            modelBuilder.Entity<TblQuotationDetail>(entity =>
            {
                entity.Property(e => e.DiscountNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TotalNet).HasColumnType("decimal(12, 2)");
                entity.Property(e => e.TotalTot).HasColumnType("decimal(12, 2)");
                entity.Property(e => e.TotalVat).HasColumnType("decimal(12, 2)");










                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblQuotationDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblQuotationDetail_Product");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.TblQuotationDetail)
                    .HasForeignKey(d => d.QuotationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblQuotationDetail_id");
            });

            modelBuilder.Entity<TblSaleOrder>(entity =>
            {
                entity.HasKey(e => e.SaleOrderId);

                entity.Property(e => e.ContractName).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(500);

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountCash).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountNet)
                    .HasColumnType("decimal(12, 2)");
                // .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'DiscountNet'))");

                entity.Property(e => e.DiscountTot)
                    .HasColumnType("decimal(12, 2)");
                // .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'DiscountTot'))");

                entity.Property(e => e.DiscountVat)
                    .HasColumnType("decimal(12, 2)");
                //  .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'DiscountVat'))");

                entity.Property(e => e.OperationEndDate).HasColumnType("datetime");

                entity.Property(e => e.OperationStartDate).HasColumnType("datetime");

                entity.Property(e => e.PoNo).HasMaxLength(50);

                entity.Property(e => e.QuotationNo).HasMaxLength(20);

                entity.Property(e => e.SaleName).HasMaxLength(500);

                entity.Property(e => e.SaleOrderDate).HasColumnType("datetime");

                entity.Property(e => e.SaleOrderNo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubTotalNet)
                    .HasColumnType("decimal(12, 2)");
                //  .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'PriceNet'))");

                entity.Property(e => e.SubTotalTot)
                    .HasColumnType("decimal(12, 2)");
                //  .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'PriceTot'))");

                entity.Property(e => e.SubTotalVat)
                    .HasColumnType("decimal(12, 2)");
                //  .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'PriceVat'))");

                entity.Property(e => e.SummaryNet)
                    .HasColumnType("decimal(12, 2)");
                //  .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'SummaryNet'))");

                entity.Property(e => e.SummaryTot)
                    .HasColumnType("decimal(12, 2)");
                // .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'SummaryTot'))");

                entity.Property(e => e.SummaryVat)
                    .HasColumnType("decimal(12, 2)");
                //  .HasComputedColumnSql("([dbo].[cplSaleOrderPrice]([SaleOrderId],'SummaryVat'))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                /*   entity.HasOne(d => d.Customer)
                     .WithMany(p => p.TblSaleOrder)
                     .HasForeignKey(d => d.CustomerId)
                     .HasConstraintName("FK_TblSaleOrder_Customer");*/
            });

            modelBuilder.Entity<TblSaleOrderAttachment>(entity =>
            {
                entity.Property(e => e.AttachmentOrder).HasDefaultValueSql("((9999))");

                entity.Property(e => e.AttachmentPath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.SaleOrder)
                    .WithMany(p => p.TblSaleOrderAttachment)
                    .HasForeignKey(d => d.SaleOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblSaleOrderAttachment_Id");
            });

            modelBuilder.Entity<TblSaleOrderDetail>(entity =>
            {
                entity.Property(e => e.DiscountNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Remark).HasMaxLength(10);

                entity.Property(e => e.TotalNet)
                    .HasColumnType("decimal(13, 2)");
                //   .HasComputedColumnSql("([PriceNet]-[DiscountNet])");

                entity.Property(e => e.TotalTot)
                    .HasColumnType("decimal(13, 2)");
                // .HasComputedColumnSql("([PriceTot]-[DiscountTot])");

                entity.Property(e => e.TotalVat)
                    .HasColumnType("decimal(13, 2)");
                /// .HasComputedColumnSql("([PriceVat]-[DiscountVat])");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblSaleOrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblSaleOrderDetail_Product");




                entity.HasOne(d => d.SaleOrder)
                    .WithMany(p => p.TblSaleOrderDetail)
                    .HasForeignKey(d => d.SaleOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblSaleOrderDetail_id");
            });

            modelBuilder.Entity<TblUnit>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlagActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TeamOperation>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.TeamOperation)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamOperation_Manager");
            });

            modelBuilder.Entity<TeamOperationDetail>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TeamOperationDetail)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamOperationDetail_Account");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamOperationDetail)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamOperationDetail_Id");
            });

            modelBuilder.Entity<TeamSale>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.TeamSale)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamSale_Manager");
            });

            modelBuilder.Entity<TeamSaleDetail>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TeamSaleDetail)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamSaleDetail_Account");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamSaleDetail)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamSaleDetail_Id");
            });

            modelBuilder.Entity<TransferDetail>(entity =>
            {
                entity.HasKey(e => new { e.TransferId, e.Seq })
                    .HasName("PK__Transfer__09E95DAB26E6103B");

                entity.Property(e => e.CurrentQty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnName("last_modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.RequestUnit)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RequestUnitFactor).HasColumnType("decimal(8, 3)");

                /*  entity.HasOne(d => d.Product)
                  .WithMany(p => p.TransferDetail)
                  .HasForeignKey(d => d.ProductId)
                  .HasConstraintName("FK__TransferD__Produ__2803DB90");

             entity.HasOne(d => d.Transfer)
                   .WithMany(p => p.TransferDetail)
                   .HasForeignKey(d => d.TransferId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_TransferDetail_TransferHeader");*/
            });

            modelBuilder.Entity<TransferHeader>(entity =>
            {
                entity.HasKey(e => e.TransferId)
                    .HasName("PK__Transfer__9548BE632F35F7F7");

                entity.Property(e => e.BillNo).HasMaxLength(50);

                entity.Property(e => e.CarBrand).HasMaxLength(10);

                entity.Property(e => e.CarNo).HasMaxLength(10);

                entity.Property(e => e.Company).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmpId).HasMaxLength(10);

                entity.Property(e => e.Note1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.ReceiveTo).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.TransferDate).HasColumnType("datetime");

                entity.Property(e => e.TransferNo)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.TransferTime).HasMaxLength(5);

                entity.Property(e => e.TransferType)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TransferStockDetail>(entity =>
            {
                entity.HasKey(e => new { e.TransferStockId, e.Seq })
                    .HasName("PK__TransferStockDetail");

                entity.Property(e => e.CurrentQty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnName("last_modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.RequestUnit)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RequestUnitFactor).HasColumnType("decimal(8, 3)");

                /*    entity.HasOne(d => d.Product)
                      .WithMany(p => p.TransferStockDetail)
                      .HasForeignKey(d => d.ProductId)
                      .HasConstraintName("FK__TransferStockDetail__Product");*/
            });

            modelBuilder.Entity<TransferStockHeader>(entity =>
            {

                entity.HasKey(e => e.TransferStockId)
                    .HasName("PK__Transfer__Stock");

                entity.Property(e => e.BillNo).HasMaxLength(50);

                entity.Property(e => e.CarBrand).HasMaxLength(10);

                entity.Property(e => e.CarNo).HasMaxLength(10);

                entity.Property(e => e.Company).HasMaxLength(100);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EmpId).HasMaxLength(10);

                entity.Property(e => e.Note1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.ReceiveTo).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.TransferDate).HasColumnType("datetime");

                entity.Property(e => e.TransferNo)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.TransferTime).HasMaxLength(5);

                entity.Property(e => e.TransferType)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SysSurveyDetailTemplate>(entity =>
            {
                entity.HasKey(e => new { e.SurveyId, e.No, e.SubSurveyId })
                    .HasName("PK__SysSurve__EDE39A6EB053E946");

                entity.ToTable("SysSurveyDetail_Template");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SubSurveyId).HasColumnName("SubSurveyID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Title).HasMaxLength(400);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SysSurveyHeaderSubTemplate>(entity =>
            {
                entity.HasKey(e => new { e.SurveyId, e.SubSurveyId })
                    .HasName("PK__SysSurve__1DE4940013534019");

                entity.ToTable("SysSurveyHeaderSub_Template");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SubSurveyId).HasColumnName("SubSurveyID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Desc).HasMaxLength(400);

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SysSurveyHeaderTemplate>(entity =>
            {
                entity.HasKey(e => e.SurveyId)
                    .HasName("PK__SysSurve__A5481F9D5844A79E");

                entity.ToTable("SysSurveyHeader_Template");

                entity.Property(e => e.SurveyId)
                    .HasColumnName("SurveyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Desc).HasMaxLength(400);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });



            modelBuilder.Entity<TblSurveyDetail>(entity =>
            {
                entity.HasKey(e => new { e.SurveyId, e.No })
                    .HasName("PK__TblSurve__366952D76CC502A5");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Title).HasMaxLength(400);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSurveyHeader>(entity =>
            {
                entity.HasKey(e => e.SurveyId)
                    .HasName("PK__TblSurve__A5481F9DAF20E4D4");

                entity.Property(e => e.SurveyId)
                    .HasColumnName("SurveyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Desc).HasMaxLength(400);

                entity.Property(e => e.RefSurveyId).HasColumnName("RefSurveyID");

                entity.Property(e => e.SurveyDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TransferHeaderReference>(entity =>
            {
                entity.HasKey(e => new { e.TransferId, e.RefTransferId });


            });


            modelBuilder.Entity<TblQuotationDetailTemplate>(entity =>
            {
                entity.ToTable("TblQuotationDetail_Template");

                entity.Property(e => e.Discount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceNet).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceTot).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PriceVat).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TotalNet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalTot).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Whid).HasColumnName("WHId");
            });

            modelBuilder.Entity<TblQuotationTemplate>(entity =>
            {
                entity.HasKey(e => e.TempQuotationId);

                entity.ToTable("TblQuotation_Template");

               // entity.Property(e => e.QuotationId).ValueGeneratedNever();

                entity.Property(e => e.ContractEmail).HasMaxLength(500);

                entity.Property(e => e.ContractName).HasMaxLength(500);

                entity.Property(e => e.ContractPhone).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(500);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountCash).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DiscountNet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountTot).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.OperationEndDate).HasColumnType("datetime");

                entity.Property(e => e.OperationStartDate).HasColumnType("datetime");

                entity.Property(e => e.QuotationDate).HasColumnType("datetime");

                entity.Property(e => e.QuotationNo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SaleName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SubTotalNet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SubTotalTot).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SubTotalVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SummaryNet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SummaryTot).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SummaryVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TempQuotationId).ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TblPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);
                    //.HasName("PK_Payment");

                entity.Property(e => e.BankPayFrom).HasMaxLength(200);

                entity.Property(e => e.BankPayFromBranch).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(500);

                entity.Property(e => e.InvoiceNo).HasMaxLength(50);

                entity.Property(e => e.PaySlipPath).HasMaxLength(300);

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TblBankAccount>(entity =>
            {
                entity.HasKey(e => e.AcctId);
                    //.HasName("PK_TblBankAccount_1");

                entity.Property(e => e.AcctName).HasMaxLength(200);

                entity.Property(e => e.AcctNo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Bank).HasMaxLength(200);

                entity.Property(e => e.Branch).HasMaxLength(200);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(20);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

        }
    }
}
