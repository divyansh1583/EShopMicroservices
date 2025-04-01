using BuildingBlocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddCarter(new DependencyContextAssemblyCatalog([assembly]));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();
app.MapCarter();
app.Run();
