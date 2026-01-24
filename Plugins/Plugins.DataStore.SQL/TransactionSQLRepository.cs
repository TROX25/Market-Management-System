using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using UseCases.DataStorePluginInterfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Plugins.DataStore.SQL
{
    public class TransactionSQLRepository : ITransactionRepository
    {
        private readonly MarketContext db;

        public TransactionSQLRepository(MarketContext db)
        {
            this.db = db;
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            // Set the timestamp to now
            transaction.TimeStamp = DateTime.Now;
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
            {
                return db.Transactions
                    .Where(t => t.TimeStamp.Date == date.Date)
                    .ToList();
            }
            else
            {
                var startDate = date.Date;
                var endDate = startDate.AddDays(1);
                return db.Transactions
                .Where(t => t.CashierName == cashierName &&
                            t.TimeStamp >= startDate &&
                            t.TimeStamp < endDate)
                .ToList();

                // Alternative using EF.Functions.Like

                //return db.Transactions.Where(x =>
                //EF.Functions.Like(x.CashierName, $"%{cashierName}%") &&
                //x.TimeStamp.Date == date.Date).ToList();
            }

        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
            {
                return db.Transactions
                    .Where(t => t.TimeStamp.Date >= startDate && t.TimeStamp.Date <= endDate)
                    .ToList();
            }
            else
            {
                return db.Transactions.Where(t =>
                EF.Functions.Like(t.CashierName, $"%{cashierName}%") &&
                            t.TimeStamp >= startDate &&
                            t.TimeStamp <= endDate)
                .ToList();

                // Alternative using EF.Functions.Like

                //return db.Transactions.Where(x =>
                //EF.Functions.Like(x.CashierName, $"%{cashierName}%") &&
                //x.TimeStamp.Date == date.Date).ToList();
            }
        }
    }
}
