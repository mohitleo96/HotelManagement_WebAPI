using AutoMapper;
using HotelManagementProjectfeb.Model.DTO;
using HotelManagementProjectfeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProjectfeb.Controllers
{
    

[ApiController]
[Route("Bill")]
  //[Authorize(Roles = "receptionist,manager,owner")]
    public class BillController : Controller
  {
        private readonly IBillRepository _billRepository;
        private readonly IMapper Mapper;
        private readonly IRoomRepository roomRepository;
        private readonly IReservationRepository reservationRepository;

        //constructor
        public BillController(IBillRepository billRepository, IMapper mapper, 
            IRoomRepository roomRepository, IReservationRepository reservationRepository)
    {
        this._billRepository = billRepository;

        this.Mapper = mapper;

        this.roomRepository = roomRepository;      
            
        this.reservationRepository = reservationRepository;
        }

        //GetAll
    [HttpGet]
        //[Authorize
       // [Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> GetAllBillAsync()
    {
        var bill = await _billRepository.GetAllAsync();

        //BY using Auto MApper

        var billsDTO = Mapper.Map<List<Model.DTO.Bill>>(bill);

        return Ok(billsDTO);
    }
         
        
        //GetByID
    [HttpGet]
    [Route("{id:guid}")]
    [ActionName("GetBillAsync")]
        //[Authorize]
       // [Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> GetBillAsync(Guid id)
    {
        var billm = await _billRepository.GetAsync(id);

        if (billm == null)
        {
            return NotFound();
        }

        var billDTO = Mapper.Map<Model.DTO.Bill>(billm);

        return Ok(billDTO);
    }


        //Add
    [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> AddBillAsync(Model.DTO.AddBillRequest addbillRequest)
    {
            //validate the incoming request manually
            //now we not comment because it Controller contains Navigation Property of RoomId And ReservationID
            //So we need to use manual as well as Fluent Validation
            //if (!(await ValidateAddBillAsync(addbillRequest)))
            //{
            //    return BadRequest(ModelState);
            //}


            // first convert Request(DTO) to domain mo del
            var bill = new Model.Domain.Bill()
        {

            stay_dates = addbillRequest.stay_dates,

            total_bill = addbillRequest.total_bill,

            Room_id = addbillRequest.Room_id,

            Reservation_id = addbillRequest.Reservation_id,

        };

        //Pass details to Repository
        bill = await _billRepository.AddAsync(bill);

        //Convert back to DTO

        var billDTO = new Model.DTO.Bill()
        {

            stay_dates = addbillRequest.stay_dates,

            total_bill = addbillRequest.total_bill,

            Room_id = addbillRequest.Room_id,

            Reservation_id = addbillRequest.Reservation_id,

        };

        return CreatedAtAction(nameof(GetBillAsync), new { id = billDTO.Bill_id }, billDTO);

    }


        //Delete
    [HttpDelete]
    [Route("{id:guid}")]
        //[Authorize(Roles = "receptionist,manager,owner")]
        public async Task<IActionResult> DeleteBillAsync(Guid id)
    {
        //Get region from database 

        var bill = await _billRepository.DeleteAsync(id);

        //if null not found
        if (bill == null)
        {
            return NotFound();
        }
        //convert response back to DTO
        var billDTO = new Model.DTO.Bill
        {
            stay_dates = bill.stay_dates,

            total_bill = bill.total_bill,

            Room_id = bill.Room_id,

            Reservation_id = bill.Reservation_id,
        };

        //return Ok response
        return Ok(billDTO);

    }


        //Update
    [HttpPut]
    [Route("{id:guid}")]
    [Authorize(Roles = "receptionist,manager,owner")]
    public async Task<IActionResult> UpdateBillAsync([FromRoute] Guid id, 
    [FromBody] Model.DTO.UpdateBillRequest updatebillRequest)
        {

            //validate the incoming request
            //if (!(await ValidateUpdateBillAsync(updatebillRequest)))
            //{
            //    return BadRequest(ModelState);
            //}

            var bill = new Model.Domain.Bill()
         {
            stay_dates = updatebillRequest.stay_dates,

            total_bill = updatebillRequest.total_bill,

            Room_id = updatebillRequest.Room_id,

            Reservation_id = updatebillRequest.Reservation_id,

        };


        //update region using repository
        bill = await _billRepository.UpdateAsync(id, bill);

        //if null not found
        if (bill == null)
        {
            return NotFound();
        }

        //Convert Domain back to DTO
        var billDTO = new Model.DTO.Bill
        {
            stay_dates = bill.stay_dates,

            total_bill = bill.total_bill,

            Room_id = bill.Room_id,

            Reservation_id = bill.Reservation_id

        };

        //Return OK Response

        return Ok(billDTO);
    }


        //regions
        #region private methods
        //private async Task<bool>  ValidateAddBillAsync(Model.DTO.AddBillRequest addbillRequest)
        //{
        //    if (addbillRequest == null)
        //    {
        //        ModelState.AddModelError(nameof(addbillRequest),
        //          $"{nameof(addbillRequest)}Bill Data is Requried.");
        //        return false;
        //    }

        //    if (addbillRequest.stay_dates <= 0)
        //    {
        //        ModelState.AddModelError(nameof(addbillRequest.stay_dates),
        //          $"{nameof(addbillRequest.stay_dates)} Stay Date not Should be negative.");
        //    }
        //    if (addbillRequest.total_bill <= 0)
        //    {
        //        ModelState.AddModelError(nameof(addbillRequest.total_bill),
        //          $"{nameof(addbillRequest.total_bill)} Total Bill not Should be negative.");
        //    }
        //    var room = await roomRepository.GetAsync(addbillRequest.Room_id);
        //    if(room == null)
        //    {
        //        ModelState.AddModelError(nameof(addbillRequest.Room_id),
        //          $"{nameof(addbillRequest.Room_id)} Room ID is invalid.");
        //    }
        //    var reservation =await reservationRepository.GetAsync(addbillRequest.Reservation_id);
        //    if(reservation == null)
        //    {
        //        ModelState.AddModelError(nameof(addbillRequest.Reservation_id),
        //          $"{nameof(addbillRequest.Reservation_id)} Reservation ID is invalid.");
        //    }

        //    if (ModelState.ErrorCount > 0)
        //    {
        //        return false;
        //    }
        //    return true;

        //}
        //private  async Task<bool> ValidateUpdateBillAsync(Model.DTO.UpdateBillRequest updatebillRequest)
        //{
        //    if (updatebillRequest == null)
        //    {
        //        ModelState.AddModelError(nameof(updatebillRequest),
        //          $"{nameof(updatebillRequest)}Bill Data is Requried.");
        //        return false;
        //    }

        //    if (updatebillRequest.stay_dates <= 0)
        //    {
        //        ModelState.AddModelError(nameof(updatebillRequest.stay_dates),
        //          $"{nameof(updatebillRequest.stay_dates)} Stay Date not Should be negative.");
        //    }
        //    if (updatebillRequest.total_bill <= 0)
        //    {
        //        ModelState.AddModelError(nameof(updatebillRequest.total_bill),
        //          $"{nameof(updatebillRequest.total_bill)} Total Bill not Should be negative.");
        //    }
        //    //navigation property because perosn should be add valid roomID from Room Table
        //    var room = await roomRepository.GetAsync(updatebillRequest.Room_id);
        //    if (room == null)
        //    {
        //        ModelState.AddModelError(nameof(updatebillRequest.Room_id),
        //          $"{nameof(updatebillRequest.Room_id)} Room ID is invalid.");
        //    }

        //    var reservation = await reservationRepository.GetAsync(updatebillRequest.Reservation_id);
        //    if (reservation == null)
        //    {
        //        ModelState.AddModelError(nameof(updatebillRequest.Reservation_id),
        //          $"{nameof(updatebillRequest.Reservation_id)} Reservation ID is invalid.");
        //    }
        //    if (ModelState.ErrorCount > 0)
        //    {
        //        return false;
        //    }
        //    return true;

        //}
        #endregion
    }

}
