using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CosmosDBM4.entities
{
    class Venta
    {
        [JsonProperty(propertyName:"id")]
        public string Id { get; set; }
        [JsonProperty(propertyName:"email")]
        public string Email { get; set; }
        [JsonProperty(propertyName:"nombre")]
        public string Nombre { get; set; }

        [JsonProperty(propertyName:"productos")]
        public List<Producto> Productos { get; set; }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append("ID:").Append(Id).Append("\n");
            build.Append("Email:").Append(Email).Append("\n");
            build.Append("Nombre: ").Append(Nombre);
            build.Append("-.-.-.-.productos-.-.-.-.-.\n");
            foreach (var pr in Productos) {
                build.Append(pr.ToString()).Append("\n");
            }

            return build.ToString();
        }
    }
}
