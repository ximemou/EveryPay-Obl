using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Data.DataAccess;


namespace EveryPay.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private EveryPayContext context;
       
        private GenericRepository<User> userRepository;
        private GenericRepository<SupplierField> supplierFieldRepository;
        private CustomSupplierFieldRepository customSupplierFieldRepository;

        private GenericRepository<Supplier> supplierRepository;
        private GenericRepository<Bill> billRepository;
        private GenericRepository<Transaction> transactionRepository;

        private GenericRepository<SpecificFieldValue> specificFieldValueRepository;

        private GenericRepository<BillSupplier> billSupplierRepository;

        private GenericRepository<Token> tokenRepository;

        private GenericRepository<Client> clientRepository;

        private GenericRepository<Product> productRepository;

        private GenericRepository<Log> logRepository;

        private GenericRepository<SystemSettings> settingsRepository;

        private bool disposed = false;


        public UnitOfWork(EveryPayContext aContext)
        {
            this.context = aContext;
        }

        public UnitOfWork()
        {
            this.context = new EveryPayContext();
        }

        public IRepository<User> UserRepository
        {
            get
            {
               if(this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public IRepository<Token> TokenRepository
        {
            get
            {
                if (this.tokenRepository == null)
                {
                    this.tokenRepository = new GenericRepository<Token>(context);
                }
                return tokenRepository;
            }
        }

        public IRepository<BillSupplier> BillSupplierRepository
        {
            get
            {
                if (this.billSupplierRepository == null)
                {
                    this.billSupplierRepository = new GenericRepository<BillSupplier>(context);

                }
                return billSupplierRepository;
            }
        }

        public IRepository<SupplierField> SupplierFieldRepository
        {
            get
            {
                if (this.supplierFieldRepository == null)
                {
                    this.supplierFieldRepository= new GenericRepository<SupplierField>(context);
                }
                return supplierFieldRepository;
            }
        }
        public ICustomSupplierFieldRepository CustomSupplierFieldRepository
        {
            get
            {
                if (this.customSupplierFieldRepository == null)
                {
                    this.customSupplierFieldRepository = new CustomSupplierFieldRepository(context);
                }
                return customSupplierFieldRepository;
            }
        }



        public IRepository<Supplier> SupplierRepository
        {
            get
            {
                if (this.supplierRepository == null)
                {
                    this.supplierRepository = new GenericRepository<Supplier>(context);
                }
                return supplierRepository;
            }
        }

        public IRepository<Bill> BillRepository
        {
            get
            {
                if (this.billRepository == null)
                {
                    this.billRepository = new GenericRepository<Bill>(context);
                }
                return billRepository;
            }
        }

        public IRepository<Transaction> TransactionRepository
        {
            get
            {
                if (this.transactionRepository == null)
                {
                    this.transactionRepository = new GenericRepository<Transaction>(context);
                }
                return transactionRepository;
            }
        }


        public IRepository<SpecificFieldValue> SpecificFieldValueRepository
        {
            get
            {
                if (this.specificFieldValueRepository == null)
                {
                    this.specificFieldValueRepository = new GenericRepository<SpecificFieldValue>(context);

                }
                return specificFieldValueRepository;
            }
        }

        public IRepository<Client> ClientRepository
        {
            get
            {
                if (this.clientRepository == null)
                {
                    this.clientRepository= new GenericRepository<Client>(context);

                }
                return clientRepository;
            }
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context);

                }
                return productRepository;
            }
        }

        public IRepository<Log> LogRepository
        {
            get
            {
                if (this.logRepository == null)
                {
                    this.logRepository = new GenericRepository<Log>(context);

                }
                return logRepository;
            }
        }

        public IRepository<SystemSettings> SettingsRepository
        {
            get
            {
                if (this.settingsRepository == null)
                {
                    this.settingsRepository = new GenericRepository<SystemSettings>(context);
                }
                return settingsRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
