using AutoMapper;
using HotelManagementProjectfeb.Model.Domain;
using HotelManagementProjectfeb.Model.DTO;
using HotelManagementProjectfeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace HotelManagementProjectfeb.Controllers
{

    [ApiController]

    [Route("Guest")]
    // [Authorize]
    public class GuestController : Controller
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper Mapper;

        //constructor
        public GuestController(IGuestRepository guestRepository, IMapper mapper)
        {
            this._guestRepository = guestRepository;

            this.Mapper = mapper;
        }


        //GetAll
        [HttpGet]
        // [Authorize]
        // [Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> GetAllGuestAsync()
        {
            var guest = await _guestRepository.GetAllAsync();

            //BY using Auto MApper
            //by 
            var guestsDTO = Mapper.Map<List<Model.DTO.Guest>>(guest);




            return Ok(guestsDTO);
        }


        //GetByID
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetGuestAsync")]
        //[Authorize]
        [Authorize(Roles = "receptionist")]
        public async Task<IActionResult> GetGuestAsync(Guid id)
        {
            var guestm = await _guestRepository.GetAsync(id);

            if (guestm == null)
            {
                return NotFound();
            }

            var guestDTO = Mapper.Map<Model.DTO.Guest>(guestm);

            return Ok(guestDTO);
        }


        //GetByID
        [HttpPost]
        //[Authorize]
        [Authorize(Roles = "receptionist")]
        public async Task<IActionResult> AddGuestAsync(Model.DTO.AddGuestRequest addguestRequest)
        {
            //adding Validation to the request
           //if(!(await ValidateAddGuestAsync(addguestRequest)))
           // {
           //     return BadRequest(ModelState);
           // }
            // first convert Request(DTO) to domain model
            var guest = new Model.Domain.Guest()
            {
                E_mail = addguestRequest.E_mail,
                Guest_Name = addguestRequest.Guest_Name,
                Gender = addguestRequest.Gender,
                Address = addguestRequest.Address,
                Phone_number = addguestRequest.Phone_number

            };

            //Pass details to Repository
            guest = await _guestRepository.AddAsync(guest);

            //Convert back to DTO

            var guestDTO = new Model.DTO.Guest
            {
                Guest_id = guest.Guest_id,
                E_mail = guest.E_mail,
                Guest_Name = guest.Guest_Name,
                Gender = guest.Gender,
                Address = guest.Address,
                Phone_number = guest.Phone_number

            };

            return CreatedAtAction(nameof(GetGuestAsync), new { id = guestDTO.Guest_id }, guestDTO);

        }


        //Delete
        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteGuestAsync")]
        //   [Authorize]
        //   [Authorize(Roles = "receptionist")]
        public async Task<IActionResult> DeleteGuestAsync(Guid id)
        {
            //Get region from database 

            var guest = await _guestRepository.DeleteAsync(id);

            //if null not found
            if (guest == null)
            {
                return NotFound();
            }
            //convert response back to DTO
            var guestDTO = new Model.DTO.Guest
            {
                Guest_id = guest.Guest_id,
                E_mail = guest.E_mail,
                Guest_Name = guest.Guest_Name,
                Gender = guest.Gender,
                Address = guest.Address,
                Phone_number = guest.Phone_number

            };

            //return Ok response
            return Ok(guestDTO);

        }


        //Update
        [HttpPut]
        [Route("{id:guid}")]
        [ActionName("UpdateGuestAsync")]
        // [Authorize]
        // [Authorize(Roles = "receptionist")]
        public async Task<IActionResult> UpdateGuestAsync([FromRoute] Guid id, 
            [FromBody] Model.DTO.UpdateGuestRequest updateguestRequest)
        {
            //validate the incoming Request
            //comment because no navigation property and fluent validation is applied
            //if(!(await ValidateUpdateGuestAsync(updateguestRequest)))
            //{
            //    return BadRequest(ModelState);
            //}
            var guest = new Model.Domain.Guest()
            {
                E_mail = updateguestRequest.E_mail,
                Guest_Name = updateguestRequest.Guest_Name,
                Gender = updateguestRequest.Gender,
                Address = updateguestRequest.Address,
                Phone_number = updateguestRequest.Phone_number
            };


            //update region using repository
            guest = await _guestRepository.UpdateAsync(id, guest);

            //if null not found
            if (guest == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var guestDTO = new Model.DTO.Guest
            {
                Guest_id = guest.Guest_id,
                E_mail = guest.E_mail,
                Guest_Name = guest.Guest_Name,
                Gender = guest.Gender,
                Address = guest.Address,
                Phone_number = guest.Phone_number

            };

            //Return OK Response

            return Ok(guestDTO);
        }


        //regions
        //#region Private methods
        //private async Task<bool> ValidateAddGuestAsync(Model.DTO.AddGuestRequest addguestRequest)
        //{
        //    // we also have to check Whole request should not be empty
        //    if(addguestRequest == null)
        //    {
        //        ModelState.AddModelError(nameof(addguestRequest),
        //         $"{nameof(addguestRequest)}Guest Data is Requried.");
        //        return false;  
        //    }
        //    if (string.IsNullOrWhiteSpace(addguestRequest.E_mail))
        //    {
        //        ModelState.AddModelError(nameof(addguestRequest.E_mail),
        //          $"{nameof(addguestRequest.E_mail)} E_mail Cannot be null or Empty or White Space.");
        //    }

        //    if (string.IsNullOrWhiteSpace(addguestRequest.Guest_Name))
        //    {
        //        ModelState.AddModelError(nameof(addguestRequest.Guest_Name),
        //          $"{nameof(addguestRequest.Guest_Name)} Guest_Name Cannot be null or Empty or White Space.");
        //    }

        //    if (string.IsNullOrWhiteSpace(addguestRequest.Gender))
        //    {
        //        ModelState.AddModelError(nameof(addguestRequest.Gender),
        //          $"{nameof(addguestRequest.Gender)}Gender Cannot be null or Empty or White Space.");
        //    }

        //    if (string.IsNullOrWhiteSpace(addguestRequest.Address))
        //    {
        //        ModelState.AddModelError(nameof(addguestRequest.Address),
        //          $"{nameof(addguestRequest.Address)} Address Cannot be null or Empty or White Space.");
        //    }

        //    if (addguestRequest.Phone_number<=0)
        //    {
        //        ModelState.AddModelError(nameof(addguestRequest.Phone_number),
        //          $"{nameof(addguestRequest.Phone_number)} Phone_number Cannot be less than Or equal To O.");
        //    }
        //    //we also can do individual in every model state but also at one place
        //    if(ModelState.ErrorCount>0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //private async Task<bool> ValidateUpdateGuestAsync(Model.DTO.UpdateGuestRequest updateGuestRequest)
        //{
        //    // we also have to check Whole request should not be empty
        //    if (updateGuestRequest == null)
        //    {
        //        ModelState.AddModelError(nameof(updateGuestRequest),
        //         $"{nameof(updateGuestRequest)}Guest Data is Requried.");
        //        return false;
        //    }
        //    if (string.IsNullOrWhiteSpace(updateGuestRequest.E_mail))
        //    {
        //        ModelState.AddModelError(nameof(updateGuestRequest.E_mail),
        //          $"{nameof(updateGuestRequest.E_mail)} E_mail Cannot be null or Empty or White Space.");
        //    }

        //    if (string.IsNullOrWhiteSpace(updateGuestRequest.Guest_Name))
        //    {
        //        ModelState.AddModelError(nameof(updateGuestRequest.Guest_Name),
        //          $"{nameof(updateGuestRequest.Guest_Name)} Guest_Name Cannot be null or Empty or White Space.");
        //    }

        //    if (string.IsNullOrWhiteSpace(updateGuestRequest.Gender))
        //    {
        //        ModelState.AddModelError(nameof(updateGuestRequest.Gender),
        //          $"{nameof(updateGuestRequest.Gender)}Gender Cannot be null or Empty or White Space.");
        //    }

        //    if (string.IsNullOrWhiteSpace(updateGuestRequest.Address))
        //    {
        //        ModelState.AddModelError(nameof(updateGuestRequest.Address),
        //          $"{nameof(updateGuestRequest.Address)} Address Cannot be null or Empty or White Space.");
        //    }

        //    if (updateGuestRequest.Phone_number <= 0)
        //    {
        //        ModelState.AddModelError(nameof(updateGuestRequest.Phone_number),
        //          $"{nameof(updateGuestRequest.Phone_number)} Phone_number Cannot be less than Or equal To O.");
        //    }
        //    //we also can do individual in every model state but also at one place
        //    if (ModelState.ErrorCount > 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //#endregion


    }
}


