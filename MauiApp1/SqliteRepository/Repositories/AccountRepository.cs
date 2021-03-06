using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.SqliteRepository.Entities;
using SQLite;

namespace MauiApp1.SqliteRepository.Repositories
{
    //https://vladislavantonyuk.azurewebsites.net/articles/Adding-SQLite-to-the-.NET-MAUI-application?fbclid=IwAR2z3cc_CkedHxnjm9VWwPA8t8Y_T7711NvxdjF5iAkVQiMaTyhmkQG0IdE
    public class AccountRepository
    {
        private readonly SQLiteConnection _database;

        public AccountRepository(string dbName)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Account>();
        }


        public List<Account> GetAccounts()
        {
            return _database.Table<Account>().ToList();
        }

        public int CreateAccount(Account account)
        {
            return _database.Insert(account);
        }

        public int UpdateAccount(Account account)
        {
            return _database.Update(account);
        }

        public int DeleteAccount(Account account)
        {
            return _database.Delete(account);
        }


    }
}
