using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocateMyCarWeb.Models
{
    public class Vehicle : BaseModel
    {
        [JsonProperty("vehicleId")]
        public int VehicleId { get; set; }
        /// <summary>
        /// Marca
        /// </summary>
        [JsonProperty("brand")]
        [Required(ErrorMessage = "O campo Marca é obrigatório.")]
        public string Brand { get; set; }

        /// <summary>
        /// Modelo
        /// </summary>
        [JsonProperty("model")]
        [Required(ErrorMessage = "O campo Modelo é obrigatório.")]
        public string Model { get; set; }

        /// <summary>
        /// Tipo de veiculo
        /// </summary>
        [JsonProperty("type")]
        [Required(ErrorMessage = "O campo Tipo de veiculo é obrigatório.")]
        public EVehicleType Type { get; set; }

        /// <summary>
        /// Imagem do veiculo
        /// </summary>
        [JsonProperty("imageURL")]
        public string? ImageURL { get; set; }

        /// <summary>
        /// Itens do veiculo
        /// Ano, Preço, Potência, Quantidade de portas e etc... 
        /// </summary>
        [JsonProperty("vehicleDetails")]
        public List<VehicleDetail>? VehicleDetails { get; set; }

        public IFormFile? ImageFile { get; set; }
    }

    public enum EVehicleType
    {
        Car = 1,
        Motorcycle = 2,
        Truck = 3,
        Bus = 4,
        Bicycle = 5
    }
}
