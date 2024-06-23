using AuctionService.DTOs;
using AuctionService.Entities;
using AuctionService.Repository;
using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Extensions.handler;
public static class ProductExtension
{
    public static void ProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auctions/{date}", async (string date, [FromServices] IAuctionRepository repository) =>
        {
            var aution = await repository.GetAuctionsAsync(date);
            return Results.Ok(aution);
        });
        app.MapGet("/api/auctions/{id:Guid}", async (Guid id, [FromServices] IAuctionRepository repository) =>
        {
            var aution = await repository.GetAuctionByIdAsync(id);
            return Results.Ok(aution);
        });
        app.MapPost("api/auctions", async (
            [FromBody] CreateAuctionDto auctionDto,
            [FromServices] IAuctionRepository repository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint, CancellationToken token) =>
        {
            var auction = mapper.Map<Auction>(auctionDto);
            repository.AddAuction(auction);
            var newAuction = mapper.Map<AuctionDto>(auction);
            await publishEndpoint.Publish(mapper.Map<AuctionCreated>(newAuction), token);
            var result = await repository.SaveChangesAsync();

            if (!result) return Results.BadRequest();
            return Results.Ok(newAuction);
        });
        app.MapDelete("/api/auctions", async (Guid id, [FromServices] IAuctionRepository repository) =>
        {
            var entity = await repository.GetAuctionEntityById(id);
            if (entity == null) return Results.NotFound();
            repository.RemoveAuction(entity);
            var result = await repository.SaveChangesAsync();
            if (!result) return Results.BadRequest();
            return Results.NoContent();
        }).RequireAuthorization();

    }
}