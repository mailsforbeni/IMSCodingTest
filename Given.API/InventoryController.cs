using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Given.Repositories.Generic;
using AutoMapper;
using Given.Models;
using System.Threading.Tasks;
using Given.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Given.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private IInventoryRepository _InventoryService;
        private IMapper _mapper;
        public InventoryController(
            IInventoryRepository InventoryService, IMapper mapper)
        {
            _InventoryService = InventoryService;
            _mapper = mapper;
        }

        [HttpGet("GetAllInventory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<InventoryModel>))]
        public async Task<IActionResult> GetAllInventory()
        {
            try
            {
                var model = await _InventoryService.GetAllInventorysAsync();
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }
        [HttpGet("GetInventoryById/{Inventoryid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<InventoryModel>))]
        public async Task<IActionResult> GetInventoryById(int Inventoryid)
        {                                                                     
            try
            {
                var model = await _InventoryService.GetInventoryByIdAsync(Inventoryid);   
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }
        [HttpGet("GetInventoryByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<InventoryModel>))]
        public async Task<IActionResult> GetInventoryByName(string name)
        {
            try
            {
                var model = await _InventoryService.GetInventoryByNameAsync(name);
                
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }
        /// <summary>
        /// Puts a Inventory.
        /// </summary>
        /// <param name="uuid">The UUID of the Inventory.</param>
        /// <param name="Inventory">The Inventory object.</param>
        /// <returns>204,400,404</returns>
        [HttpPut("PutInventory")]
        public async Task<IActionResult> Put(InventoryModel Inventory)
        {
            return await SaveInventory(Inventory);
        }

        /// <summary>
        /// Posts a Inventory.
        /// </summary>
        /// <param name="Inventory">The Inventory object.</param>
        /// <returns>201</returns>
        [HttpPost("PostInventory")]
        public async Task<IActionResult> Post(InventoryModel Inventory)
        {
            return await SaveInventory(Inventory);
        }

        private async Task<IActionResult> SaveInventory(InventoryModel Inventory)
        {
            var result = new ApiResult();
            try
            {
                var response = await _InventoryService.SaveAsync(Inventory);
                if (response == "200")
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Inventory saved", true);
                }
                else if (response == "2601")
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Inventory entered is already taken", false);
                else
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Error on save", false);
            }
            catch (DbUpdateConcurrencyException x)
            {
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Error on save", false);

            }
            return Ok(result);
        }

        /// <summary>
        /// Deletes a Inventory.
        /// </summary>
        /// <param name="id">The ID of the Inventory.</param>
        /// <returns>200,404</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ApiResult();
            var Inventory = await _InventoryService.GetInventoryByIdAsync(id);
            if (Inventory == null)
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "No Inventory with this Id", false);
            try
            {
                await _InventoryService.DeleteInventoryAsync(Inventory);
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Inventory deleted", true);
            }
            catch
            {
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Cannot delete due to internal error", false);
            }
            return Ok(result);
        }
    }
}