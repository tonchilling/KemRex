namespace Kemrex.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbContext : DbContext
    {
        public dbContext()
            : base("name=dbContext")
        {
        }

        public virtual DbSet<CalcAccountStaff> CalcAccountStaff { get; set; }
        public virtual DbSet<EnmPaymentCondition> EnmPaymentCondition { get; set; }
        public virtual DbSet<EnmPaymentConditionTerm> EnmPaymentConditionTerm { get; set; }
        public virtual DbSet<EnmPrefix> EnmPrefix { get; set; }
        public virtual DbSet<EnmStatusEmployee> EnmStatusEmployee { get; set; }
        public virtual DbSet<EnmStatusQuotation> EnmStatusQuotation { get; set; }
        public virtual DbSet<SysAccount> SysAccount { get; set; }
        public virtual DbSet<SysAccountRole> SysAccountRole { get; set; }
        public virtual DbSet<SysDistrict> SysDistrict { get; set; }
        public virtual DbSet<SysGeography> SysGeography { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysMenuPermission> SysMenuPermission { get; set; }
        public virtual DbSet<SysParameter> SysParameter { get; set; }
        public virtual DbSet<SysPermission> SysPermission { get; set; }
        public virtual DbSet<SysPostcode> SysPostcode { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRolePermission> SysRolePermission { get; set; }
        public virtual DbSet<SysSite> SysSite { get; set; }
        public virtual DbSet<SysState> SysState { get; set; }
        public virtual DbSet<SysSubDistrict> SysSubDistrict { get; set; }
        public virtual DbSet<TblCalLoad> TblCalLoad { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<TblCustomerAddress> TblCustomerAddress { get; set; }
        public virtual DbSet<TblCustomerContact> TblCustomerContact { get; set; }
        public virtual DbSet<TblCustomerGroup> TblCustomerGroup { get; set; }
        public virtual DbSet<TblDepartment> TblDepartment { get; set; }
        public virtual DbSet<TblDepartmentPosition> TblDepartmentPosition { get; set; }
        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<TblInvoice> TblInvoice { get; set; }
        public virtual DbSet<TblKpt> TblKpt { get; set; }
        public virtual DbSet<TblKptAttachment> TblKptAttachment { get; set; }
        public virtual DbSet<TblKptDetail> TblKptDetail { get; set; }
        public virtual DbSet<TblKptStation> TblKptStation { get; set; }
        public virtual DbSet<TblKptStationAttachment> TblKptStationAttachment { get; set; }
        public virtual DbSet<TblKptStationDetail> TblKptStationDetail { get; set; }
        public virtual DbSet<TblModelInfo> TblModelInfo { get; set; }
        public virtual DbSet<TblModelSeries> TblModelSeries { get; set; }
        public virtual DbSet<TblPile> TblPile { get; set; }
        public virtual DbSet<TblPileSeries> TblPileSeries { get; set; }
        public virtual DbSet<TblPosition> TblPosition { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblProductCategory> TblProductCategory { get; set; }
        public virtual DbSet<TblProductModel> TblProductModel { get; set; }
        public virtual DbSet<TblProductType> TblProductType { get; set; }
        public virtual DbSet<TblQuotation> TblQuotation { get; set; }
        public virtual DbSet<TblQuotationDetail> TblQuotationDetail { get; set; }
        public virtual DbSet<TblSaleOrder> TblSaleOrder { get; set; }
        public virtual DbSet<TblSaleOrderAttachment> TblSaleOrderAttachment { get; set; }
        public virtual DbSet<TblSaleOrderDetail> TblSaleOrderDetail { get; set; }
        public virtual DbSet<TblUnit> TblUnit { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnmPaymentCondition>()
                .HasMany(e => e.EnmPaymentConditionTerm)
                .WithRequired(e => e.EnmPaymentCondition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnmStatusEmployee>()
                .HasMany(e => e.TblEmployee)
                .WithRequired(e => e.EnmStatusEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnmStatusQuotation>()
                .HasMany(e => e.TblQuotation)
                .WithRequired(e => e.EnmStatusQuotation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysAccount>()
                .HasMany(e => e.CalcAccountStaff)
                .WithRequired(e => e.SysAccount)
                .HasForeignKey(e => e.AccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysAccount>()
                .HasMany(e => e.CalcAccountStaff1)
                .WithRequired(e => e.SysAccount1)
                .HasForeignKey(e => e.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysAccount>()
                .HasMany(e => e.SysAccountRole)
                .WithRequired(e => e.SysAccount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysMenu>()
                .HasMany(e => e.SysMenu1)
                .WithOptional(e => e.SysMenu2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<SysMenu>()
                .HasMany(e => e.SysMenuPermission)
                .WithRequired(e => e.SysMenu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysMenu>()
                .HasMany(e => e.SysRolePermission)
                .WithRequired(e => e.SysMenu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysPermission>()
                .HasMany(e => e.SysMenuPermission)
                .WithRequired(e => e.SysPermission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysPermission>()
                .HasMany(e => e.SysRolePermission)
                .WithRequired(e => e.SysPermission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysRole>()
                .HasMany(e => e.SysAccountRole)
                .WithRequired(e => e.SysRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysRole>()
                .HasMany(e => e.SysRolePermission)
                .WithRequired(e => e.SysRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysSite>()
                .HasMany(e => e.SysMenu)
                .WithRequired(e => e.SysSite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysSite>()
                .HasMany(e => e.SysRole)
                .WithRequired(e => e.SysSite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblCalLoad>()
                .Property(e => e.InputC)
                .HasPrecision(12, 4);

            modelBuilder.Entity<TblCalLoad>()
                .Property(e => e.InputDegree)
                .HasPrecision(12, 4);

            modelBuilder.Entity<TblCalLoad>()
                .Property(e => e.InputSafeLoad)
                .HasPrecision(12, 4);

            modelBuilder.Entity<TblCustomer>()
                .HasMany(e => e.TblCustomerAddress)
                .WithRequired(e => e.TblCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblCustomer>()
                .HasMany(e => e.TblCustomerContact)
                .WithRequired(e => e.TblCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblDepartment>()
                .HasMany(e => e.TblDepartmentPosition)
                .WithRequired(e => e.TblDepartment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblEmployee>()
                .HasMany(e => e.TblEmployee1)
                .WithOptional(e => e.TblEmployee2)
                .HasForeignKey(e => e.LeadId);

            modelBuilder.Entity<TblEmployee>()
                .HasMany(e => e.TblQuotation)
                .WithRequired(e => e.TblEmployee)
                .HasForeignKey(e => e.SaleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblEmployee>()
                .HasMany(e => e.TblSaleOrder)
                .WithOptional(e => e.TblEmployee)
                .HasForeignKey(e => e.SaleId);

            modelBuilder.Entity<TblInvoice>()
                .Property(e => e.InvoiceAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblKpt>()
                .HasMany(e => e.TblKptAttachment)
                .WithRequired(e => e.TblKpt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblKpt>()
                .HasMany(e => e.TblKptDetail)
                .WithRequired(e => e.TblKpt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblKpt>()
                .HasMany(e => e.TblKptStation)
                .WithRequired(e => e.TblKpt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblKptStation>()
                .HasMany(e => e.TblKptStationAttachment)
                .WithRequired(e => e.TblKptStation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblKptStation>()
                .HasMany(e => e.TblKptStationDetail)
                .WithRequired(e => e.TblKptStation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblModelInfo>()
                .Property(e => e.ModelDia)
                .HasPrecision(5, 4);

            modelBuilder.Entity<TblModelInfo>()
                .Property(e => e.ModelBlade)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TblModelInfo>()
                .Property(e => e.ModelSpiralLength)
                .HasPrecision(5, 4);

            modelBuilder.Entity<TblModelInfo>()
                .Property(e => e.ModelSpiralDepth)
                .HasPrecision(5, 4);

            modelBuilder.Entity<TblModelInfo>()
                .Property(e => e.ModelFlangeWidth)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TblModelInfo>()
                .Property(e => e.ModelFlangeLength)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TblPile>()
                .Property(e => e.PileDia)
                .HasPrecision(5, 4);

            modelBuilder.Entity<TblPile>()
                .Property(e => e.PileBlade)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TblPile>()
                .Property(e => e.PileSpiralLength)
                .HasPrecision(5, 4);

            modelBuilder.Entity<TblPile>()
                .Property(e => e.PileSpiralDepth)
                .HasPrecision(5, 4);

            modelBuilder.Entity<TblPile>()
                .Property(e => e.PileFlangeWidth)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TblPile>()
                .Property(e => e.PileFlangeLength)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TblProduct>()
                .Property(e => e.CostNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblProduct>()
                .Property(e => e.CostVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblProduct>()
                .Property(e => e.CostTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblProduct>()
                .Property(e => e.PriceNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblProduct>()
                .Property(e => e.PriceVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblProduct>()
                .Property(e => e.PriceTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblProduct>()
                .HasMany(e => e.TblQuotationDetail)
                .WithRequired(e => e.TblProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblProduct>()
                .HasMany(e => e.TblSaleOrderDetail)
                .WithRequired(e => e.TblProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblProductCategory>()
                .HasMany(e => e.TblProduct)
                .WithRequired(e => e.TblProductCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblProductCategory>()
                .HasMany(e => e.TblProductModel)
                .WithRequired(e => e.TblProductCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.SubTotalNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.SubTotalVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.SubTotalTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.DiscountNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.DiscountVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.DiscountTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.DiscountCash)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.SummaryNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.SummaryVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .Property(e => e.SummaryTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotation>()
                .HasMany(e => e.TblQuotationDetail)
                .WithRequired(e => e.TblQuotation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.PriceNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.PriceVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.PriceTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.DiscountNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.DiscountVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.DiscountTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.TotalNet)
                .HasPrecision(13, 2);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.TotalVat)
                .HasPrecision(13, 2);

            modelBuilder.Entity<TblQuotationDetail>()
                .Property(e => e.TotalTot)
                .HasPrecision(13, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.SubTotalNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.SubTotalVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.SubTotalTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.DiscountNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.DiscountVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.DiscountTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.DiscountCash)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.SummaryNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.SummaryVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .Property(e => e.SummaryTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrder>()
                .HasMany(e => e.TblInvoice)
                .WithRequired(e => e.TblSaleOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblSaleOrder>()
                .HasMany(e => e.TblSaleOrderAttachment)
                .WithRequired(e => e.TblSaleOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblSaleOrder>()
                .HasMany(e => e.TblSaleOrderDetail)
                .WithRequired(e => e.TblSaleOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.PriceNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.PriceVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.PriceTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.DiscountNet)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.DiscountVat)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.DiscountTot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.TotalNet)
                .HasPrecision(13, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.TotalVat)
                .HasPrecision(13, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.TotalTot)
                .HasPrecision(13, 2);

            modelBuilder.Entity<TblSaleOrderDetail>()
                .Property(e => e.Remark)
                .IsFixedLength();

            modelBuilder.Entity<TblUnit>()
                .HasMany(e => e.TblProduct)
                .WithRequired(e => e.TblUnit)
                .WillCascadeOnDelete(false);
        }
    }
}
