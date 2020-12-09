using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;
using SEP6_backendd.Repositories;

namespace SEP6_backendd.Services
{
    public class WeatherService : IWeatherService
    {
        private List<Weather> _weathers;

        private List<Temperature> _temperatures;

        private List<Temperature> _meanTemperatures;

        private List<TemperaturesOrigin> _temperaturesAtts;

        private List<TemperaturesOrigin> _meanTemperaturesOrigin;

        private readonly WeatherRepository _weatherRepository;

        public WeatherService()
        {
            _weatherRepository = new WeatherRepository();
        }

        public List<Weather> getWeathersInOrigins()
        {
            if (_weathers == null || _weathers.Count == 0)
            {
                _weathers = _weatherRepository.GetWeathersInOrigin();
            }

            return _weathers;
        }

        public List<TemperaturesOrigin> GetTemperatureAttributes()
        {
            if (_temperaturesAtts == null || _temperaturesAtts.Count == 0)
            {
                _temperaturesAtts = _weatherRepository.GetTemperatureAttributes();
            }

            return _temperaturesAtts;
        }

        public List<Temperature> GetTemperaturesJfk()
        {
            if (_temperatures == null || _temperatures.Count == 0)
            {
                _temperatures = _weatherRepository.GetTemperatureJfk();
            }

            return _temperatures;
        }

        public List<Temperature> GetMeanTemperatureJfk()
        {
            if (_meanTemperatures == null || _meanTemperatures.Count == 0)
            {
                _meanTemperatures = _weatherRepository.GetMeanTemperatureJfk();
            }

            return _meanTemperatures;
        }

        public List<TemperaturesOrigin> GetMeanTemperatureOrigins()
        {
            if (_meanTemperaturesOrigin == null || _meanTemperaturesOrigin.Count == 0)
            {
                _meanTemperaturesOrigin = _weatherRepository.GetMeanTemperatureOrigins();
            }

            return _meanTemperaturesOrigin;
        }
    }
}
