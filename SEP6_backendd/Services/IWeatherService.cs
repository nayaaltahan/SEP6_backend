using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Services
{
    public interface IWeatherService
    {
        List<Weather> getWeathersInOrigins();

        List<TemperaturesOrigin> GetTemperatureAttributes();

        List<Temperature> GetTemperaturesJfk();

        List<Temperature> GetMeanTemperatureJfk();

        List<TemperaturesOrigin> GetMeanTemperatureOrigins();
    }
}
