using Given.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface IInventoryRepository 
    {
        Task<string> SaveAsync(InventoryModel Inventory);
        Task DeleteInventoryAsync(InventoryModel Inventory);
        Task<IEnumerable<InventoryModel>> GetAllInventorysAsync();
        Task<InventoryModel> GetInventoryByIdAsync(int Inventoryid);
        Task<InventoryModel> GetInventoryByNameAsync(string name);
    }
}