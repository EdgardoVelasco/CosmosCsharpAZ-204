using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CosmosDBM4.entities
{
    class Producto
    {
        [JsonProperty(PropertyName ="nombre")]
        public string Nombre { get; set; }
        [JsonProperty(PropertyName ="precio")]
        public double Precio { get; set; }

        public override string ToString() => $"Nombre: {Nombre}, Precio:{Precio}";
    }
}
