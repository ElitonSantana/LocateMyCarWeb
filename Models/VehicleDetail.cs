using LocateMyCarWeb.Models.Helpers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LocateMyCarWeb.Models
{
    public class VehicleDetail : BaseModel
    {

        [JsonProperty("vehicleDetailId")]
        public int VehicleDetailId { get; set; }

        /// <summary>
        /// Ano
        /// </summary>
        [JsonProperty("year")]
        [Required(ErrorMessage = "Necessário informar o ano.")]
        public int Year { get; set; }

        /// <summary>
        /// Potência
        /// </summary>
        [JsonProperty("vehiclePower")]
        [ValidationNumberGreaterThanZero(ErrorMessage ="O valor de Potência precisa ser maior do que 0.")]
        public decimal VehiclePower { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        [JsonProperty("price")]
        [ValidationNumberGreaterThanZero(ErrorMessage = "O valor de Preço precisa ser maior do que 0.")]
        public double Price { get; set; }

        /// <summary>
        /// Quantidade de portas
        /// </summary>
        [JsonProperty("vehicleDoors")]
        [ValidationNumberGreaterThanZero(ErrorMessage = "O valor de Quantidade de Portas precisa ser maior do que 0.")]
        public int VehicleDoors { get; set; }

        /// <summary>
        /// Imagem do veiculo
        /// </summary>
        [JsonProperty("imageURL")]
        public string ImageURL { get; set; }

        public IFormFile? ImageFile { get; set; }

        [JsonProperty("vehicleId")]
        public int VehicleId { get; set; }
    }
}
