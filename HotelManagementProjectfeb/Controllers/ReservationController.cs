using AutoMapper;
using HotelManagementProjectfeb.Model.DTO;
using HotelManagementProjectfeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProjectfeb.Controllers
{

    [ApiController]
    [Route("Reservation")]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservatioRepository;
        private readonly IMapper Mapper;
        private readonly IGuestRepository guestRepository;
        private readonly IRoomRepository roomRepository;


        //constructor
        public ReservationController(IReservationRepository reservationRepository, IMapper mapper, 
            IGuestRepository guestRepository,IRoomRepository roomRepository)
        {
            this._reservatioRepository = reservationRepository;

            this.Mapper = mapper;
            this.guestRepository = guestRepository;
            this.roomRepository = roomRepository;
        }


        //GetAll
        [HttpGet]
        //[Authorize]
        [Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> GetAllReservationAsync()
        {
            var reservation = await _reservatioRepository.GetAllAsync();

            //BY using Auto MApper

            var reservationDTO = Mapper.Map<List<Model.DTO.Reservation>>(reservation);

            return Ok(reservationDTO);
        }


        //GetByID
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetReservationAsync")]
        //[Authorize]
        [Authorize(Roles = "receptionist,manager")]
        public async Task<IActionResult> GetReservationAsync(Guid id)
        {
            var reservation = await _reservatioRepository.GetAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDTO = Mapper.Map<Model.DTO.Reservation>(reservation);

            return Ok(reservationDTO);
        }

        //Add
        [HttpPost]
        //[Authorize]
       [Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> AddReservationAsync(Model.DTO.AddReservationRequest addReservationRequest)
        {
            //Apply Validation
            //if(!(await ValidateAddReservationAsync(addReservationRequest)))
            //{
            //    return BadRequest(ModelState);
            //}

            // first convert Request(DTO) to domain model
            var reservation = new Model.Domain.Reservation()
            {
                Check_in = addReservationRequest.Check_in,

                Check_out = addReservationRequest.Check_out,

                status = addReservationRequest.status,

                Guest_Id = addReservationRequest.Guest_Id,

                no_of_adults = addReservationRequest.no_of_adults,

                no_of_children = addReservationRequest.no_of_children,

                no_of_nights = addReservationRequest.no_of_nights,

                Room_id = addReservationRequest.Room_id

              };

            //Pass details to Repository
            reservation = await _reservatioRepository.AddAsync(reservation);

            //Convert back to DTO

            var reservationDTO = new Model.DTO.Reservation
            {
                reservation_id = reservation.reservation_id,

                no_of_adults = reservation.no_of_adults,

                no_of_children = reservation.no_of_children,

                Check_out = reservation.Check_out,

                Check_in = reservation.Check_in,

                status = reservation.status,

                no_of_nights = reservation.no_of_adults,

                Guest_Id = reservation.Guest_Id,

                Room_id = reservation.Room_id

                };

            return CreatedAtAction(nameof(GetReservationAsync), new { id = reservationDTO.reservation_id }, reservationDTO);

        }


        //Delete
        [HttpDelete]
        [Route("{id:guid}")]
        // [Authorize]
        [Authorize(Roles = "receptionist,manager")]
        public async Task<IActionResult> DeleteReservationAsync(Guid id)
        {
            //Get region from database 

            var reservation = await _reservatioRepository.DeleteAsync(id);


            //if null not found
            if (reservation == null)
            {
                return NotFound();
            }
            //convert response back to DTO
            var reservationDTO = new Model.DTO.Reservation
            {
                reservation_id = reservation.reservation_id,

                no_of_adults = reservation.no_of_adults,

                no_of_children = reservation.no_of_children,

                Check_out = reservation.Check_out,

                Check_in = reservation.Check_in,

                status = reservation.status,

                no_of_nights = reservation.no_of_adults,

                Guest_Id = reservation.Guest_Id,

                Room_id = reservation.Room_id
            };

            //return Ok response
            return Ok(reservationDTO);

        }


        //Update
        [HttpPut]
        [Route("{id:guid}")]
        // [Authorize]
          [Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> UpdateReservationAsync([FromRoute] Guid id, [FromBody] Model.DTO.UpdateReservationRequest updatereservationRequest)
        {
            //Apply Validation
            //if (!(await ValidateUpdateReservationAsync(updatereservationRequest)))
            //{
            //    return BadRequest(ModelState);
            //}

            var reservation = new Model.Domain.Reservation()
            {

                no_of_adults = updatereservationRequest.no_of_adults,

                no_of_children = updatereservationRequest.no_of_children,

                Check_out = updatereservationRequest.Check_out,

                Check_in = updatereservationRequest.Check_in,

                status = updatereservationRequest.status,

                no_of_nights = updatereservationRequest.no_of_nights,

                Guest_Id = updatereservationRequest.Guest_Id,

                Room_id = updatereservationRequest.Room_id,

              };


            //update region using repository
            reservation = await _reservatioRepository.UpdateAsync(id, reservation);

            //if null not found
            if (reservation == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var reservationDTO = new Model.DTO.Reservation
            {
                no_of_adults = reservation.no_of_adults,

                no_of_children = reservation.no_of_children,

                Check_out = reservation.Check_out,

                Check_in = reservation.Check_in,

                status = reservation.status,

                no_of_nights = reservation.no_of_nights,

                Guest_Id = reservation.Guest_Id,

                Room_id = reservation.Room_id

                 };

            //Return OK Response

            return Ok(reservationDTO);
        }


        //regions
        //#region Private property
        //public async Task<bool> ValidateAddReservationAsync(Model.DTO.AddReservationRequest addReservationRequest)
        //{
        //    if(addReservationRequest==null)
        //    {
        //        ModelState.AddModelError(nameof(addReservationRequest),
        //       $"{nameof(addReservationRequest)}Reservation Data is Requried.");
        //        return false;
        //    }
        //   if (addReservationRequest.no_of_adults<0)
        //        {
        //            ModelState.AddModelError(nameof(addReservationRequest.no_of_adults),
        //              $"{nameof(addReservationRequest.no_of_adults)} Number of Adults not be negative.");
        //        }
        //   if(addReservationRequest.no_of_children<0)
        //    {
        //        ModelState.AddModelError(nameof(addReservationRequest.no_of_children),
        //            $"{nameof(addReservationRequest.no_of_children)} Number of Childen not be negative.");
        //    }
        //    if (addReservationRequest.no_of_nights < 0)
        //    {
        //        ModelState.AddModelError(nameof(addReservationRequest.no_of_nights),
        //          $"{nameof(addReservationRequest.no_of_nights)} Number of Nights not be negative.");
        //    }
        //    var guest = await guestRepository.GetAsync(addReservationRequest.Guest_Id);
               
        //    if (guest == null)
        //    {
        //        ModelState.AddModelError(nameof(addReservationRequest.Guest_Id),
        //          $"{nameof(addReservationRequest.Guest_Id)} Guest ID is invalid.");
        //    }
        //    var room = await roomRepository.GetAsync(addReservationRequest.Room_id);

        //    if (room == null)
        //    {
        //        ModelState.AddModelError(nameof(addReservationRequest.Room_id),
        //          $"{nameof(addReservationRequest.Room_id)} Room ID is invalid.");
        //    }
        //    if(ModelState.ErrorCount > 0)
        //    {
        //        return false;
        //    }
        //    return true;

        //}
        //public async Task<bool> ValidateUpdateReservationAsync(Model.DTO.UpdateReservationRequest updateReservationRequest)
        //{
        //      if(updateReservationRequest == null)
        //    {
        //        ModelState.AddModelError(nameof(updateReservationRequest),
        //       $"{nameof(updateReservationRequest)}Reservation Data is Requried.");
        //        return false;
        //    }
        //   if (updateReservationRequest.no_of_adults < 0)
        //        {
        //            ModelState.AddModelError(nameof(updateReservationRequest.no_of_adults),
        //              $"{nameof(updateReservationRequest.no_of_adults)} Number of Adults not be negative.");
        //        }
        //   if(updateReservationRequest.no_of_children < 0)
        //    {
        //        ModelState.AddModelError(nameof(updateReservationRequest.no_of_children),
        //            $"{nameof(updateReservationRequest.no_of_children)} Number of Childen not be negative.");
        //    }
        //    if (updateReservationRequest.no_of_nights < 0)
        //    {
        //        ModelState.AddModelError(nameof(updateReservationRequest.no_of_nights),
        //          $"{nameof(updateReservationRequest.no_of_nights)} Number of Nights not be negative.");
        //    }
        //    var guest = await guestRepository.GetAsync(updateReservationRequest.Guest_Id);   
        //    if (guest == null)
        //    {
        //        ModelState.AddModelError(nameof(updateReservationRequest.Guest_Id),
        //          $"{nameof(updateReservationRequest.Guest_Id)} Guest ID is invalid.");
        //    }
        //    var room = await roomRepository.GetAsync(updateReservationRequest.Room_id);

        //    if (room == null)
        //    {
        //        ModelState.AddModelError(nameof(updateReservationRequest.Room_id),
        //          $"{nameof(updateReservationRequest.Room_id)} Room ID is invalid.");
        //    }
        //    if(ModelState.ErrorCount > 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //#endregion
    }
}

