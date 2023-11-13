using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WApiPosExpress;
using WApiPosExpress.Datos;
using WApiPosExpress.Servicios.Interface;
using WApiPosExpress.Servicios.Servicio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMvc();

builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<ICodigoBarraServicio, CodigoBarraServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IVentaExpressServicio, VentaExpressServicio>();
//builder.Services.AddTransient<ICategoriaServicio, CategoriaServicio>();

builder.Services.AddDbContext<DbExpressContext>(options => {

    options.UseSqlServer(builder.Configuration.GetConnectionString("DBXpressConnection"));
});




var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<DbExpressContext>();
//    dataContext.Database.Migrate();
//}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
