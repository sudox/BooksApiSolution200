using Microsoft.AspNetCore.Mvc;
using ReservationsApi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationsApi.Controllers
{
    public class ReservationsController : ControllerBase
    {
        private readonly IProcessOrders _orderProcessor;

        public ReservationsController(IProcessOrders orderProcessor)
        {
            _orderProcessor = orderProcessor;
        }

        // POST /reservations -- collection (plural)

        [HttpPost("")]
        public async Task<ActionResult> AddReservation([FromBody] ReservationRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // await Task.Delay(3000); // Simualting the delay of code to be written

            var response = new ReservationResponse
            {
                ReservationId = Guid.NewGuid(),
                For = request.For,
                Books = request.Books,
                Status = "Pending"
            };

            await _orderProcessor.Send(response);

            return Accepted(response);
            // Send me something
            // Validate it (if bad, send 400)
            // If it's good do whatever
            // Return a 201 created
            //  -- Since you created something send the name (location header)
            //  -- Return it

        }
    }


    public class ReservationRequest : IValidatableObject
    {
        [Required]
        public string For { get; set; }
        public string[] Books { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Books == null || Books?.Length == 0) { yield return new ValidationResult("You need some books"); }
        }
    }

    public class ReservationResponse
    {
        public Guid ReservationId { get; set; }
        public string For { get; set; }
        public string[] Books { get; set; }
        public string Status { get; set; }
    }

}
