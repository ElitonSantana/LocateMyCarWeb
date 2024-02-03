using LocateMyCarWeb.Controllers;
using LocateMyCarWeb.Models;
using LocateMyCarWeb.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json.Serialization;

namespace LocateMyCarWeb.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _BaseUrl;
        public VehicleService(IOptions<ServiceSettings> BaseUrl, ILogger<HomeController> logger)
        {
            _logger = logger;
            _BaseUrl = $"{BaseUrl.Value.BaseUrl}/api";
        }
        #region :: Private Methods ::
        private bool VehicleValidate(Vehicle vehicle)
        {
            if (vehicle == null ||
                string.IsNullOrEmpty(vehicle.Brand) ||
                string.IsNullOrEmpty(vehicle.Model) ||
                vehicle.Type == 0)
                return false;

            return true;
        }

        private bool VehicleDetailsValidate(VehicleDetail vehicleDetail)
        {
            if (vehicleDetail == null ||
                vehicleDetail.Year == 0 ||
                vehicleDetail.VehiclePower == 0 ||
                vehicleDetail.VehicleDoors == 0 ||
                vehicleDetail.Price == 0)
                return false;

            return true;
        }

        #endregion

        #region :: Vehicle ::

        /// <summary>
        /// Criação de novo veiculo
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public MessageResponse<Vehicle> Create(Vehicle vehicle)
        {
            if (!VehicleValidate(vehicle))
                return new MessageResponse<Vehicle> { Success = false, Message = "Veiculo inválido. Por favor, revisar o cadastro!" };

            var entity = new MessageResponse<Vehicle>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest("/vehicle", Method.Post);
                request.AddParameter("application/json", JsonConvert.SerializeObject(vehicle), ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<Vehicle>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(Create)} ao tentar cadastrar o veiculo {JsonConvert.SerializeObject(vehicle)} exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }
        /// <summary>
        /// Listagem de todos os veículos
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MessageResponse<List<Vehicle>> GetAll()
        {
            var entity = new MessageResponse<List<Vehicle>>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest("/vehicle", Method.Get);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<List<Vehicle>>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(GetAll)} ao tentar buscar os veiculos exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }
        /// <summary>
        /// Consulta de um veículo pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MessageResponse<Vehicle> GetById(int id)
        {
            var entity = new MessageResponse<Vehicle>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest($"/vehicle/{id}", Method.Get);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<Vehicle>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(GetById)} ao tentar buscar o veiculo exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }
        /// <summary>
        /// Atualizar informações um veiculo
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MessageResponse<Vehicle> Update(Vehicle vehicle)
        {
            if (!VehicleValidate(vehicle))
                return new MessageResponse<Vehicle> { Success = false, Message = "Veiculo inválido. Por favor, revisar o cadastro!" };

            var entity = new MessageResponse<Vehicle>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest($"/vehicle/{vehicle.VehicleId}", Method.Put);
                request.AddParameter("application/json", JsonConvert.SerializeObject(vehicle), ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<Vehicle>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(Update)} ao tentar atualizar o veiculo {JsonConvert.SerializeObject(vehicle)} exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }
        /// <summary>
        /// Remover um veiculo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MessageResponse<Vehicle> Delete(int id)
        {
            if (id == 0)
                return new MessageResponse<Vehicle> { Success = false, Message = "Veículo não encontrado para exclusão!" };

            var entity = new MessageResponse<Vehicle>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest($"/vehicle/{id}", Method.Delete);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<Vehicle>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(Delete)} ao tentar remover o veiculo id {id} exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }
        /// <summary>
        /// Listagem de TOP 3 veiculos mais antigos.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MessageResponse<List<VehicleVM>> GetTopVehiclesOlders()
        {
            var entity = new MessageResponse<List<VehicleVM>>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest("/vehicle/GetTopVehicles", Method.Get);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<List<VehicleVM>>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(GetTopVehiclesOlders)} ao tentar buscar o top 3 veiculos antigos exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }

        #endregion

        #region :: VehicleDetails ::

        public MessageResponse<VehicleDetail> GetVehicleDetailById(int id)
        {
            var entity = new MessageResponse<VehicleDetail>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest($"/vehicleDetails/{id}", Method.Get);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<VehicleDetail>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(GetVehicleDetailById)} ao tentar buscar versão do veiculo exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }

        public MessageResponse<VehicleDetail> CreateVehicleDetail(VehicleDetail vehicleDetail)
        {
            if (!VehicleDetailsValidate(vehicleDetail))
                return new MessageResponse<VehicleDetail> { Success = false, Message = "Versão de veiculo inválido. Por favor, revisar o cadastro!" };

            var entity = new MessageResponse<VehicleDetail>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest("/vehicleDetails", Method.Post);
                request.AddParameter("application/json", JsonConvert.SerializeObject(vehicleDetail), ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<VehicleDetail>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(CreateVehicleDetail)} ao tentar cadastrar nova versão de veiculo vehicleDetail {JsonConvert.SerializeObject(vehicleDetail)} exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }



        public MessageResponse<VehicleDetail> UpdateVehicleDetail(VehicleDetail vehicleDetail)
        {
            if (!VehicleDetailsValidate(vehicleDetail))
                return new MessageResponse<VehicleDetail> { Success = false, Message = "Versão de veiculo inválido. Por favor, revisar o cadastro!" };

            var entity = new MessageResponse<VehicleDetail>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest($"/vehicleDetails/{vehicleDetail.VehicleId}", Method.Put);
                request.AddParameter("application/json", JsonConvert.SerializeObject(vehicleDetail), ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<VehicleDetail>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(UpdateVehicleDetail)} ao tentar atualizar a versão do veiculo  vehicleDetail {JsonConvert.SerializeObject(vehicleDetail)} exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }

        public MessageResponse<VehicleDetail> DeleteVehicleDetail(int id)
        {
            if (id == 0)
                return new MessageResponse<VehicleDetail> { Success = false, Message = "Versão de veiculo não encontrada para exclusão!" };

            var entity = new MessageResponse<VehicleDetail>();

            try
            {
                var client = new RestClient(_BaseUrl);
                var request = new RestRequest($"/vehicleDetails/{id}", Method.Delete);

                var response = client.Execute(request);

                if (response == null)
                {
                    entity.Success = false;
                    entity.Message = $"Falha ao tentar se comunicar com o serviço!";
                }
                else
                    entity = JsonConvert.DeserializeObject<MessageResponse<VehicleDetail>>(response.Content);
            }
            catch (Exception ex)
            {
                entity.Success = false;
                entity.Message = $"Falha ao tentar se comunicar com o serviço! Entre em contato com o suporte!";
                _logger.LogError($"Erro catch Vehicle {nameof(DeleteVehicleDetail)} ao tentar remover a versão do veiculo id {id} exception {JsonConvert.SerializeObject(ex)}");
            }

            return entity;
        }

        #endregion
    }
}
