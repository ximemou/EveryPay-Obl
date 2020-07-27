using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<User> UserRepository { get;  }
        IRepository<SupplierField> SupplierFieldRepository { get; }
        ICustomSupplierFieldRepository CustomSupplierFieldRepository { get; }
        IRepository<Supplier> SupplierRepository { get; }
        IRepository<Bill> BillRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        IRepository<SpecificFieldValue> SpecificFieldValueRepository { get; }
        IRepository<BillSupplier> BillSupplierRepository { get; }
        IRepository<Token> TokenRepository { get;  }

        IRepository<Client> ClientRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Log> LogRepository { get; }
        IRepository<SystemSettings> SettingsRepository { get; }

        void Save();
    }
}
