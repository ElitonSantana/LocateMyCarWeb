using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocateMyCarWeb.Models.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumVehicleLabel(EVehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case EVehicleType.Car:
                    return "Carro";
                case EVehicleType.Motorcycle:
                    return "Motocicleta";
                case EVehicleType.Truck:
                    return "Caminhão";
                case EVehicleType.Bus:
                    return "Ônibus";
                case EVehicleType.Bicycle:
                    return "Bicicleta";
                default:
                    return string.Empty;
            }
        }
        public static IEnumerable<SelectListItem> GetYearList()
        {
            List<SelectListItem> years = new List<SelectListItem>();

            for (int year = 1950; year <= DateTime.Now.Year; year++)
            {
                years.Add(new SelectListItem
                {
                    Text = year.ToString(),
                    Value = year.ToString()
                });
            }

            return years;
        }

    }
}
