using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class AlertModels
    {
        public SensorData SensorData { get; set; }
        public bool TemperaturaAlert { get; set; }
        public bool UmidadeAlert { get; set; }
        public bool LevelCO2Alert { get; set; }


    }

    public class SensorData
    {
        public double Temperatura { get; set; }
        public double Umidade { get; set; }
        public double LevelCO2 { get; set; }
    }
}