using AutoMapper;
using HotelManagementProjectfeb.Model.DTO;
using HotelManagementProjectfeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProjectfeb.Controllers
{

    [ApiController]
    [Route("Room")]
    public class RoomController : Controller

    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper Mapper;

        //constructor
        public RoomController(IRoomRepository roomRepository, IMapper mapper)
        {
            this._roomRepository = roomRepository;

            this.Mapper = mapper;
        }


        //GetAll
        [HttpGet]
        //[Authorize]
         [Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> GetAllRoomAsync()
        {
            var room = await _roomRepository.GetAllAsync();

            //BY using Auto MApper

            var roomDTO = Mapper.Map<List<Model.DTO.Room>>(room);

            return Ok(roomDTO);
        }


        //GetByID
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRoomAsync")]
        //[Authorize]
        [Authorize(Roles = "manager,owner")]
        public async Task<IActionResult> GetRoomAsync(Guid id)
        {
            var room = await _roomRepository.GetAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            var roomDTO = Mapper.Map<Model.DTO.Room>(room);

            return Ok(roomDTO);
        }

        //Add
        [HttpPost]
        //[Authorize]
        [Authorize(Roles = "manager,owner")]
        public async Task<IActionResult> AddRoomAsync(Model.DTO.AddRoomRequest addRoomRequest)
        {
            //if(!(await ValidateAddRoomAsync(addRoomRequest)))
            //{
            //    return BadRequest(ModelState);
            //}

            // first convert Request(DTO) to domain model
            var room = new Model.Domain.Room()
            {
                room_rate = addRoomRequest.room_rate,

                room_status = addRoomRequest.room_status,

            };

            //Pass details to Repository
            room = await _roomRepository.AddAsync(room);

            //Convert back to DTO

            var roomDTO = new Model.DTO.Room
            {
                room_rate = room.room_rate,

                room_status = room.room_status,

            };

            return CreatedAtAction(nameof(GetRoomAsync), new { id = roomDTO.room_id }, roomDTO);

        }


        //Delete
        [HttpDelete]
        [Route("{id:guid}")]
        //   [Authorize]
        [Authorize(Roles = "manager,owner")]
        public async Task<IActionResult> DeleteRoomAsync(Guid id)
        {
            //Get region from database 

            var room = await _roomRepository.DeleteAsync(id);


            //if null not found
            if (room == null)
            {
                return NotFound();
            }
            //convert response back to DTO
            var roomDTO = new Model.DTO.Room
            {
                room_rate = room.room_rate,

                room_status = room.room_status

            };

            //return Ok response
            return Ok(roomDTO);

        }


        //Update
        [HttpPut]
        [Route("{id:guid}")]
        // [Authorize]
        [Authorize(Roles = "manager,owner")]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute] Guid id, [FromBody] Model.DTO.UpdateRoomRequest updateroomRequest)
        {
            //add validation
            //if(!(await ValidateUpdateRoomAsync(updateroomRequest)))
            //{
            //    return BadRequest(ModelState);
            //}

            var room = new Model.Domain.Room()
            {
                room_rate = updateroomRequest.room_rate,

                room_status = updateroomRequest.room_status

            };


            //update region using repository
            room = await _roomRepository.UpdateAsync(id, room);

            //if null not found
            if (room == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var roomDTO = new Model.DTO.Room
            {
                room_rate = room.room_rate,

                room_status = room.room_status

            };

            //Return OK Response

            return Ok(roomDTO);
        }


        //regions
        //#region Private Property
        //public async Task<bool> ValidateAddRoomAsync(Model.DTO.AddRoomRequest addRoomRequest)
        //{
        //    if(addRoomRequest==null)
        //    {
        //        ModelState.AddModelError(nameof(addRoomRequest),
        //       $"{nameof(addRoomRequest)}Room Data is Requried.");
        //        return false;
        //    }
        //    if (addRoomRequest.room_rate<0)
        //    {
        //        ModelState.AddModelError(nameof(addRoomRequest.room_rate),
        //       $"{nameof(addRoomRequest.room_rate)}Room Rate not be nagative.");
                
        //    }
        //    if(ModelState.ErrorCount>0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public async Task<bool> ValidateUpdateRoomAsync(Model.DTO.UpdateRoomRequest updateroomRequest)
        //{
        //    if (updateroomRequest == null)
        //    {
        //        ModelState.AddModelError(nameof(updateroomRequest),
        //       $"{nameof(updateroomRequest)}Room Data is Requried.");
        //        return false;
        //    }
        //    if (updateroomRequest.room_rate < 0)
        //    {
        //        ModelState.AddModelError(nameof(updateroomRequest.room_rate),
        //       $"{nameof(updateroomRequest.room_rate)}Room Rate not be nagative.");

        //    }
        //    if (ModelState.ErrorCount > 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //#endregion
    }
}
