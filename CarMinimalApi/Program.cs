using AutoMapper;
using CarMinimalApi;
using CarMinimalApi.Data;
using CarMinimalApi.Models;
using CarMinimalApi.Models.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig)); // add AutoMapper
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

var cars = app.MapGroup("/api");

cars.MapGet("/",GetAllCars).Produces<APIResponse>(200);
cars.MapGet("/{id:int}",GetCar).Produces<APIResponse>(200);
cars.MapPost("/", AddCar).Accepts<CarAddDTO>("application/json").Produces<APIResponse>(201).Produces(400);
cars.MapPut("/",UpdateCar).Accepts<CarUpdateDTO>("application/json").Produces<APIResponse>(200).Produces(400);
cars.MapDelete("/{id:int}",DeleteCar);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

static IResult GetAllCars(ILogger<Program> _logger)
{
    APIResponse response = new();
    _logger.Log(LogLevel.Information, "Getting all cars");
    response.Result = CarStore.Cars;
    response.IsSuccess= true;
    response.StatusCode = System.Net.HttpStatusCode.OK;
    return Results.Ok(response);
}
static IResult GetCar(ILogger<Program> _logger,int id)
{
    APIResponse response = new()
    {
        IsSuccess = false,
        StatusCode = System.Net.HttpStatusCode.BadRequest
    };
    Car _car = CarStore.Cars.FirstOrDefault(x => x.Id == id);
    if (_car != null)
    {
        response.Result = _car;
        response.IsSuccess = true;
        response.StatusCode = System.Net.HttpStatusCode.OK;
        _logger.Log(LogLevel.Information, $"Get One Car. Car Id: {id}");
        return Results.Ok(response);
    }
    else
    {
        response.ErrorMessages.Add("Invalid Id");
        _logger.Log(LogLevel.Information, $"Get One Care. Invalid Id");
        return Results.BadRequest(response);
    }
}

static async Task<IResult> AddCar(ILogger<Program> _logger,
    IMapper _mapper,
    IValidator<CarAddDTO> _validator,
    [FromBody] CarAddDTO _carAddDTO)
{
    APIResponse response = new()
    {
        IsSuccess = false,
        StatusCode = System.Net.HttpStatusCode.BadRequest
    };
    var validationResult = await _validator.ValidateAsync(_carAddDTO);
    if(!validationResult.IsValid)
    {
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    if(CarStore.Cars.FirstOrDefault(x=>x.RegistrationNumber.ToLower() == _carAddDTO.RegistrationNumber.ToLower()) != null)
    {
        response.ErrorMessages.Add("Car Registration Number already Exists");
        return Results.BadRequest(response);
    }
    Car car = _mapper.Map<Car>(_carAddDTO);
    car.Id= CarStore.Cars.OrderByDescending(x=>x.Id).FirstOrDefault().Id+1;
    CarStore.Cars.Add(car);
    response.Result = car;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.Created;
    _logger.Log(LogLevel.Information, $"Add New Car | Car ID: {car.Id} | Car Registration Number: {car.RegistrationNumber}");
    return Results.Ok(response);
}
static async Task<IResult> UpdateCar(ILogger<Program> _logger,
    IMapper _mapper,
    IValidator<CarUpdateDTO> _validator,
    [FromBody] CarUpdateDTO _carUpdateDTO)
{
    APIResponse response = new()
    {
        IsSuccess = false,
        StatusCode = System.Net.HttpStatusCode.BadRequest
    };
    var validationResult = await _validator.ValidateAsync(_carUpdateDTO);
    if (!validationResult.IsValid)
    {
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    Car _car = CarStore.Cars.FirstOrDefault(x => x.Id == _carUpdateDTO.Id);
    _car.RegistrationNumber = _carUpdateDTO.RegistrationNumber;
    _car.Color = _carUpdateDTO.Color;
    _car.Behavior = _carUpdateDTO.Behavior;
    _car.Description = _carUpdateDTO.Description;
    response.Result = _car;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;
    return Results.Ok(response);
}

static IResult DeleteCar(ILogger<Program> _logger,int id)
{
    APIResponse response = new()
    {
        IsSuccess = false,
        StatusCode = System.Net.HttpStatusCode.BadRequest
    };
    Car _car = CarStore.Cars.FirstOrDefault(x => x.Id == id);
    if(_car != null)
    {
        CarStore.Cars.Remove(_car);
        response.IsSuccess = true;
        response.StatusCode = System.Net.HttpStatusCode.OK;
        _logger.Log(LogLevel.Information, $"Delete Car. Car Id: {id}");
        return Results.Ok(response);
    }
    else
    {
        response.ErrorMessages.Add("Invalid Id");
        return Results.BadRequest(response);
    }
}
app.UseHttpsRedirection();


app.Run();

