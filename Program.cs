using Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();

app.ProductEndpoint();


app.Run();
