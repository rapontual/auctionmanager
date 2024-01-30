namespace CarAuctionManagement.API.Controllers
{
    using CarAuctionManagement.Model.Exceptions;
    using CarAuctionManagement.Service;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class AuctionController : Controller
    {
        private readonly IAuctionManager auctionManager;

        public AuctionController(IAuctionManager auctionManager)
        {
            this.auctionManager = auctionManager;
        }

        [HttpPost("/auction")]
        public async Task<IResult> Create(int vehicleId)
        {
            try
            {
                await auctionManager.StartAuctionAsync(vehicleId);
            }
            catch (InvalidAuctionException iaex)
            {
                return Results.BadRequest(iaex.Message);
            }

            catch (Exception)
            {
                return Results.Problem("Error creating auction");
            }


            return Results.Created();
        }

        [HttpPost("/auction/{id}/bid")]
        public async Task<IResult> Bid(int id, double bidAmount)
        {
            try
            {
                await auctionManager.BidAsync(id, bidAmount);
            }
            catch (InvalidBidException ibex)
            {
                return Results.BadRequest(ibex.Message);
            }
            catch (InvalidAuctionException iaex)
            {
                return Results.BadRequest(iaex.Message);
            }

            catch (Exception)
            {
                return Results.Problem("Error bidding");
            }


            return Results.Ok();
        }

        [HttpPost("/auction/{id}/finish")]
        public async Task<IResult> Finish(int id, double bidAmount)
        {
            try
            {
                await auctionManager.EndAuctionAsync(id);
            }
            catch (InvalidBidException ibex)
            {
                return Results.BadRequest(ibex.Message);
            }
            catch (InvalidAuctionException iaex)
            {
                return Results.BadRequest(iaex.Message);
            }

            catch (Exception)
            {
                return Results.Problem("Error bidding");
            }


            return Results.Ok();
        }
    }
}
