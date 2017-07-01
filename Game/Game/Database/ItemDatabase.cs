using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Game;

namespace Game
{
    public class ItemDatabase
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public ItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
        }

        // Query
        public Task<List<Item>> GetItemsAsync()
        {
            return database.Table<Item>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<Item>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Done] = 0");
        }
        // 10 nejlepších výsledků podle scóre 
        public Task<List<Item>> GetItemsByScore()
        {
            return database.QueryAsync<Item>("SELECT * FROM [Item] ORDER BY `Score` DESC LIMIT 10");
        }

        // Query using LINQ
        public Task<Item> GetItemAsync(int id)
        {
            return database.Table<Item>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            item.TimeStamp = DateTime.Now;

            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> FindByNO(int no_b, int no_id)
        {
            //return database.
            return null;
        }
    }
}
