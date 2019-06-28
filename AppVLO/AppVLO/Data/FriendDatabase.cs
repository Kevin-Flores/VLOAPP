using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppVLO.Model;

namespace AppVLO.Data
{
    public class FriendDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public FriendDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Model.Detalle>().Wait();
        }

        public async Task<List<Model.Detalle>> GetItemsAsync()
        {
            return await database.Table<Model.Detalle>().ToListAsync();
        }

        public Task<Model.Detalle> GetItemAsync(int id)
        {
            return database.Table<Model.Detalle>()
                .Where(i => i.IdDetalle == id)
                .FirstOrDefaultAsync();
        }

        public Task<List<Model.Detalle>> ObtenerPersonalizado(int idMesa)
        {
            return database.Table<Model.Detalle>()
                .Where(i => i.IdMesa == idMesa && i.Estado == 1)
                .ToListAsync();
                        //where q.IdMesa == idMesa && q.Estado == 1
                        //select q;             
        }

        public Task<int> SaveItemAsync(Model.Detalle item)
        {
            if (item.IdDetalle != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Model.Detalle item)
        {
            return database.DeleteAsync(item);
        }
    }
}
