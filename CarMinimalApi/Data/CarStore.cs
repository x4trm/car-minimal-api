using CarMinimalApi.Models;

namespace CarMinimalApi.Data
{
    public static class CarStore
    {
        public static List<Car> Cars = new List<Car>()
        {
            new Car(){Id=1,Mark="Skoda Fabia",Color="Gray",Behavior=90192,YearOfProduction= new DateTime(2018,01,22)},
            new Car(){Id=1,Mark="Opel Astra",Color="White",Behavior=78121,YearOfProduction= new DateTime(2018,02,10)},
            new Car(){Id=1,Mark="Honda Jezz",Color="Yellow",Behavior=109224,YearOfProduction= new DateTime(2008,03,21)},
            new Car(){Id=1,Mark="Opel Vectra",Color = "Blue",Behavior=100229,YearOfProduction= new DateTime(2011,11,02)},
        };
    }
}
