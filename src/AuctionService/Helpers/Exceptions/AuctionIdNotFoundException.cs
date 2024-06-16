namespace AuctionService.Helpers.Exceptions;

public class AuctionIdNotFoundException(Guid id) : ControllerException($"Auction with this id is not found {id}")
{
}
