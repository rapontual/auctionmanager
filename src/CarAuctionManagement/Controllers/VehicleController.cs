namespace CarAuctionManagement.API.Controllers
{
    using CarAuctionManagement.Model;
    using CarAuctionManagement.Model.Exceptions;
    using CarAuctionManagement.Service;
    using Microsoft.AspNetCore.Mvc;

    // I'm using the same model to API, but I usually create a separate request object, that would be mapped to domain when need, with AutoMapper e.g.
    // Also using simple string, but usually I create consts to keep the same instance, or even a translated resource when needed
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpPost("/vehicle")]
        public async Task<IResult> Add(Vehicle vehicle)
        {
            try
            {
                await this.vehicleService.AddVehicleAsync(vehicle);
            }
            catch (DuplicateEntityException dex)
            {
                return Results.BadRequest(dex.Message);
            }

            catch (Exception)
            {
                return Results.Problem("Error adding vehicle");
            }

            return Results.Created();
        }

        [HttpGet("/vehicle/search")]
        public async Task<IResult> Search(VehicleType? vehicleType, string? manufacturer, string? model, int? year)
        {
            try
            {
                var result = await this.vehicleService.SearchAsyc(vehicleType, manufacturer, model, year);
                return Results.Ok(result);
            }

            catch (Exception)
            {
                return Results.Problem("Error serching vehicles");
            }
        }
           
    }
}
