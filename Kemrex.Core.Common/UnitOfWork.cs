using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Common.Modules;
using Kemrex.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        #region constructor
        private readonly mainContext db;
        public UnitOfWork(string constr)
        {
            db = new mainContext(constr);
        }
        public UnitOfWork(mainContext _db)
        {
            db = _db;
        }
        #endregion

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    db.Dispose();
                }
                disposedValue = true;
            }
        }
        private UowModules _Modules;
        public UowModules Modules => _Modules ?? (_Modules = new UowModules(db));

        public void Dispose()
        { Dispose(true); }
        #endregion

        #region Releated Classes
        public class UowModules
        {
            #region constructor
            private readonly mainContext db;
            public UowModules(mainContext _db)
            {
                db = _db;
            }
            #endregion
            private AccountModule _Account;
            private CustomerModule _Customer;
            private CustomerAddressModule _CustomerAddress;
            private CustomerGroupModule _CustomerGroup;
            private CustomerContactModule _CustomerContact;
            private DepartmentModule _Department;
            private EmployeeModule _Employee;
            private EnumModule _Enum;
            private InvoiceModule _Invoice;
            private PaymentConditionMudule _PaymentCondition;
            private PileModule _Pile;
            private PileSeriesModule _PileSeries;
            private PositionModule _Position;
            private ProductModule _Product;
            private ProductCategoryModule _ProductCategory;
            private ProductModelModule _ProductModel;
            private ProductTypeModule _ProductType;
            private QuotationModule _Quotation;
            private QuotationDetailModule _QuotationDetail;
            private RoleModule _Role;
            private RolePermissionModule _RolePermission;
            private SaleOrderModule _SaleOrder;
            private SaleOrderDetailModule _SaleOrderDetail;
            private SaleOrderAttachmentModule _SaleOrderAttachment;
            private SubDistrictModule _SubDistrict;
            private SystemModule _System;
            private TeamSaleModule _TeamSale;
            private TeamSaleDetailModule _TeamSaleDetail;
            private TeamOperationModule _TeamOperation;
            private TeamOperationDetailModule _TeamOperationDetail;
            private UnitModule _Unit;
            private AttachmentTypeModule _AttachmentTypeModule;
            private SysCategoryModule _SysCategoryModule;
            private JobOrderModule _JobOrderModule;
            private TransferModule _Transfer;
            private TransferStockModule _TransferStock;

            public AccountModule Account => _Account ?? (_Account = new AccountModule(db));
            public CustomerAddressModule CustomerAddress => _CustomerAddress ?? (_CustomerAddress = new CustomerAddressModule(db));
            public CustomerModule Customer => _Customer ?? (_Customer = new CustomerModule(db));
            public CustomerGroupModule CustomerGroup => _CustomerGroup ?? (_CustomerGroup = new CustomerGroupModule(db));
            public CustomerContactModule CustomerContact => _CustomerContact ?? (_CustomerContact = new CustomerContactModule(db));
            public DepartmentModule Department => _Department ?? (_Department = new DepartmentModule(db));
            public EmployeeModule Employee => _Employee ?? (_Employee = new EmployeeModule(db));
            public EnumModule Enum => _Enum ?? (_Enum = new EnumModule(db));
            public InvoiceModule Invoice => _Invoice ?? (_Invoice = new InvoiceModule(db));
            public PaymentConditionMudule PaymentCondition => _PaymentCondition ?? (_PaymentCondition = new PaymentConditionMudule(db));
            public PileModule Pile => _Pile ?? (_Pile = new PileModule(db));
            public PileSeriesModule PileSeries => _PileSeries ?? (_PileSeries = new PileSeriesModule(db));
            public PositionModule Position => _Position ?? (_Position = new PositionModule(db));
            public ProductModule Product => _Product ?? (_Product = new ProductModule(db));
            public ProductCategoryModule ProductCategory => _ProductCategory ?? (_ProductCategory = new ProductCategoryModule(db));
            public ProductModelModule ProductModel => _ProductModel ?? (_ProductModel = new ProductModelModule(db));
            public ProductTypeModule ProductType => _ProductType ?? (_ProductType = new ProductTypeModule(db));
            public QuotationModule Quotation => _Quotation ?? (_Quotation = new QuotationModule(db));
            public QuotationDetailModule QuotationDetail => _QuotationDetail ?? (_QuotationDetail = new QuotationDetailModule(db));
            public RoleModule Role => _Role ?? (_Role = new RoleModule(db));
            public RolePermissionModule RolePermission => _RolePermission ?? (_RolePermission = new RolePermissionModule(db));
            public SaleOrderModule SaleOrder => _SaleOrder ?? (_SaleOrder = new SaleOrderModule(db));
            public SaleOrderDetailModule SaleOrderDetail => _SaleOrderDetail ?? (_SaleOrderDetail = new SaleOrderDetailModule(db));
            public SaleOrderAttachmentModule SaleOrderAttachment => _SaleOrderAttachment ?? (_SaleOrderAttachment = new SaleOrderAttachmentModule(db));
            public SubDistrictModule SubDistrict => _SubDistrict ?? (_SubDistrict = new SubDistrictModule(db));
            public SystemModule System => _System ?? (_System = new SystemModule(db));
            public TeamSaleModule TeamSale => _TeamSale ?? (_TeamSale = new TeamSaleModule(db));
            public TeamSaleDetailModule TeamSaleDetail => _TeamSaleDetail ?? (_TeamSaleDetail = new TeamSaleDetailModule(db));
            public TeamOperationModule TeamOperation => _TeamOperation ?? (_TeamOperation = new TeamOperationModule(db));
            public TeamOperationDetailModule TeamOperationDetail => _TeamOperationDetail ?? (_TeamOperationDetail = new TeamOperationDetailModule(db));
            public UnitModule Unit => _Unit ?? (_Unit = new UnitModule(db));
            public AttachmentTypeModule AttachmentType => _AttachmentTypeModule ?? (_AttachmentTypeModule = new AttachmentTypeModule(db));
            public SysCategoryModule SysCategory => _SysCategoryModule ?? (_SysCategoryModule = new SysCategoryModule(db));
            public JobOrderModule JobOrder => _JobOrderModule ?? (_JobOrderModule = new JobOrderModule(db));
            public TransferModule Transfer => _Transfer ?? (_Transfer = new TransferModule(db));
            public TransferStockModule TransferStock => _TransferStock ?? (_TransferStock = new TransferStockModule(db));
        }
        #endregion
    }
}