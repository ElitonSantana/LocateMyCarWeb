using LocateMyCarWeb.Models;

namespace LocateMyCarWeb.Services.Interfaces
{
    public interface IVehicleService
    {
        MessageResponse<List<Vehicle>> GetAll();
        MessageResponse<Vehicle> GetById(int id);
        MessageResponse<Vehicle> Create(Vehicle vehicle);
        MessageResponse<Vehicle> Update(Vehicle vehicle);
        MessageResponse<Vehicle> Delete(int id);
        MessageResponse<List<VehicleVM>> GetTopVehiclesOlders();

        MessageResponse<VehicleDetail> GetVehicleDetailById(int id);
        MessageResponse<VehicleDetail> CreateVehicleDetail(VehicleDetail vehicle);
        MessageResponse<VehicleDetail> UpdateVehicleDetail(VehicleDetail vehicle);
        MessageResponse<VehicleDetail> DeleteVehicleDetail(int id);
    }
}
