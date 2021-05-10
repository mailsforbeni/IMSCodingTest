using AutoMapper;
using Given.Models;
using Given.Repositories.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Given.DataContext.IMSEntities;
using Microsoft.Data.SqlClient;

namespace Given.Repositories
{
    public class InventoryRepository : RepositoryBase<InventoryManagementSystemContext, Inventory, InventoryModel>, IInventoryRepository
    {
        private IMapper _mapper;
        public InventoryRepository(InventoryManagementSystemContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _mapper = mapper;
        } 

        public async Task<string> SaveAsync(InventoryModel Inventory)
        {
            if (Inventory.Id == 0)
            {  
                Add(Inventory);
            }
            else
            {
                var thisInventory = await GetInventoryByIdAsync(Inventory.Id);
                  
                if (thisInventory != null)
                { 
                    thisInventory.Name = Inventory.Name;
                    thisInventory.Description = Inventory.Description;
                    thisInventory.Price = Inventory.Price;
                    thisInventory.SupplierName = Inventory.SupplierName;
                    thisInventory.UpdatedOn = DateTime.Now;                         
                    Update(thisInventory);                     
                } 
            }
            try
            {
                await SaveChangesAsync();
            }
            catch (Exception x)
            {
                SqlException innerException = x.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    return "2601";
                }
                else
                {
                    return "403";
                }
            }
            return "200";
        }
         

        public async Task DeleteInventoryAsync(InventoryModel Inventory)
        {
            Delete(Inventory);
            await SaveChangesAsync();
        }
        public async Task<IEnumerable<InventoryModel>> GetAllInventorysAsync()
        {
            return await GetAll()
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
        public async Task<InventoryModel> GetInventoryByIdAsync(int Inventoryid)
        {
            var Inventory = await Get(s => s.Id.Equals(Inventoryid)).SingleOrDefaultAsync();
            return Inventory;
        }
        public async Task<InventoryModel> GetInventoryByNameAsync(string name)
        {
            return await Get(s => s.Name.Equals(name)).SingleOrDefaultAsync();
        }
    }
}
