using AutoMapper;
using HotelManagementProjectfeb.Model.DTO;
using HotelManagementProjectfeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProjectfeb.Controllers
{
   
    [ApiController]
    [Route("Inventory")]
   // [Authorize(Roles ="manager")]
    public class InventoryController : Controller
    {
        private readonly IInventoryRepositorycs _inventoryRepository;

        private readonly IMapper Mapper;

        //constructor
        public InventoryController(IInventoryRepositorycs inventoryRepository, IMapper mapper)
        {
            this._inventoryRepository = inventoryRepository;

            this.Mapper = mapper;
        }


        //GetAll
        [HttpGet]
        //[Authorize]
      //[Authorize(Roles = "manager,owner")]
        public async Task<IActionResult> GetAllInventoryAsync()
        {
            var inventory = await _inventoryRepository.GetAllAsync();

            //BY using Auto MApper

            var inventoryDTO = Mapper.Map<List<Model.DTO.Inventory>>(inventory);

            return Ok(inventoryDTO);
        }


        //GetByID
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetInventoryAsync")]
        //[Authorize]
       // [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetInventoryAsync(Guid id)
        {
            var inventorym = await _inventoryRepository.GetAsync(id);

            if (inventorym == null)
            {
                return NotFound();
            }

            var inventoryDTO = Mapper.Map<Model.DTO.Inventory>(inventorym);

            return Ok(inventoryDTO);
        }


        //Add
        [HttpPost]
        //[Authorize]
       //[Authorize(Roles = "manager")]
        public async Task<IActionResult> AddInventoryAsync(Model.DTO.AddInventoryRequest addinventoryRequest)
        {
            //apply validation
             
            //if (!(await ValidateAddInventoryAsync(addinventoryRequest)))
            //{
            //    return BadRequest(ModelState);
            //}

            // first convert Request(DTO) to domain model
            var inventory = new Model.Domain.Inventory()
            {
                Inventory_Name = addinventoryRequest.Inventory_Name,

                quantity = addinventoryRequest.quantity,

            };

            //Pass details to Repository
            inventory = await _inventoryRepository.AddAsync(inventory);

            //Convert back to DTO

            var inventoryDTO = new Model.DTO.Inventory()
            {

                Inventory_Name = inventory.Inventory_Name,

                quantity = inventory.quantity

            };

            return CreatedAtAction(nameof(GetInventoryAsync), new { id = inventoryDTO.Inventory_Id }, inventoryDTO);

        }


        //Delete
        [HttpDelete]
        [Route("{id:guid}")]
        //   [Authorize]
       //   [Authorize(Roles = "manager")]
        public async Task<IActionResult> DeleteInventoryAsync(Guid id)
        {
            //Get region from database 

            var inventory = await _inventoryRepository.DeleteAsync(id);

            //if null not found
            if (inventory == null)
            {
                return NotFound();
            }
            //convert response back to DTO
            var inventoryDTO = new Model.DTO.Inventory()
            {
                Inventory_Name = inventory.Inventory_Name,

                quantity = inventory.quantity


            };

            //return Ok response
            return Ok(inventoryDTO);

        }


        //Update
        [HttpPut]
        [Route("{id:guid}")]
        // [Authorize]
       //  [Authorize(Roles = "manager")]
        public async Task<IActionResult> UpdateInventoryAsync([FromRoute] Guid id, 
            [FromBody] Model.DTO.UpdateInventoryRequest updateInventoryRequest)
        {
            //apply validation  
            //if (!(await ValidateUpdateInventoryAsync(updateInventoryRequest)))
            //{
            //    return BadRequest(ModelState);
            //}

            var inventory = new Model.Domain.Inventory()
            {
                Inventory_Name = updateInventoryRequest.Inventory_Name,

                quantity = updateInventoryRequest.quantity,


            };


            //update region using repository
            inventory = await _inventoryRepository.UpdateAsync(id, inventory);

            //if null not found
            if (inventory == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var inventoryDTO = new Model.DTO.Inventory
            {
                Inventory_Name = inventory.Inventory_Name,

                quantity = inventory.quantity

            };

            //Return OK Response

            return Ok(inventoryDTO);
        }


        //regions
        //#region Private Property
        //public async Task<bool> ValidateAddInventoryAsync(Model.DTO.AddInventoryRequest addinventoryRequest)
        //{
        //    if(addinventoryRequest==null)
        //    {
        //        ModelState.AddModelError(nameof(addinventoryRequest),
        //        $"{nameof(addinventoryRequest)}Inventory Data is Requried.");
        //        return false;
        //    }
        //    if(string.IsNullOrEmpty(addinventoryRequest.Inventory_Name))
        //    {
        //        ModelState.AddModelError(nameof(addinventoryRequest),
        //            $"{nameof(addinventoryRequest)}Inventory Name Cannot be null or Empty or White Space");
        //    }
        //    if (addinventoryRequest.quantity < 0)
        //    {
        //        ModelState.AddModelError(nameof(addinventoryRequest),
        //            $"{nameof(addinventoryRequest)}Quantity Cannot be negative");
        //    }
        //    if(ModelState.ErrorCount> 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public async Task<bool> ValidateUpdateInventoryAsync(Model.DTO.UpdateInventoryRequest updateInventoryRequest)
        //{
        //    if (updateInventoryRequest == null)
        //    {
        //        ModelState.AddModelError(nameof(updateInventoryRequest),
        //        $"{nameof(updateInventoryRequest)}Inventory Data is Requried.");
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(updateInventoryRequest.Inventory_Name))
        //    {
        //        ModelState.AddModelError(nameof(updateInventoryRequest),
        //            $"{nameof(updateInventoryRequest)}Inventory Name Cannot be null or Empty or White Space");
        //    }
        //    if (updateInventoryRequest.quantity < 0)
        //    {
        //        ModelState.AddModelError(nameof(updateInventoryRequest),
        //            $"{nameof(updateInventoryRequest)}Quantity Cannot be negative");
        //    }
        //    if (ModelState.ErrorCount > 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
      //  #endregion
    }

}




