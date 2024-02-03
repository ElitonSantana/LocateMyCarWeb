using LocateMyCarWeb.Models;
using LocateMyCarWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

namespace LocateMyCarWeb.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _BaseUrlImages;
        public VehicleController(IVehicleService vehicleService, ILogger<HomeController> logger, IOptions<ServiceSettings> BaseUrl, IWebHostEnvironment hostingEnvironment)
        {
            _vehicleService = vehicleService;
            _logger = logger;
            _BaseUrlImages = BaseUrl.Value.BaseUrlImages;
            _hostingEnvironment = hostingEnvironment;

        }

        #region :: Vehicle ::

        public ActionResult Index(string pFilter, bool btnFilterClick = false)
        {
            var response = new MessageResponse<List<Vehicle>>();

            if (string.IsNullOrEmpty(pFilter) && !btnFilterClick)
            {
                response = _vehicleService.GetAll();
                return View(response);
            }
            else if (string.IsNullOrEmpty(pFilter) && btnFilterClick)
            {
                response = _vehicleService.GetAll();
                return PartialView("List", response.Entity);
            }
            else
            {
                response = _vehicleService.GetAll();
                response.Entity = response.Entity.Where(x =>
                                                        x.Brand.ToLower().Contains(pFilter.ToLower()) ||
                                                        x.Model.ToLower().Contains(pFilter.ToLower())).ToList();
                return PartialView("List", response.Entity);
            }
        }

        public ActionResult Details(int id)
        {

            var response = _vehicleService.GetById(id);

            if (response.Success)
                return View(response.Entity);
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Create()
        {
            return View(new Vehicle());
        }

        [HttpPost]
        public ActionResult Create([Bind] Vehicle vehicle, IFormFile ImageFile)
        {
            try
            {
                ModelState.Remove(nameof(vehicle.ImageFile));
                if (string.IsNullOrEmpty(vehicle.ImageURL))
                    vehicle.ImageURL = string.Empty;

                if (ModelState.IsValid)
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var imageFolder = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");
                        string fileName = Path.GetFileName(ImageFile.FileName);

                        if (!Directory.Exists(imageFolder))
                            Directory.CreateDirectory(imageFolder);

                        string filePath = Path.Combine(imageFolder, fileName);

                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);
                        }

                        vehicle.ImageURL = $"{_BaseUrlImages}/Imagens/{fileName}";
                    }


                    var response = _vehicleService.Create(vehicle);

                    if (response.Success)
                    {
                        TempData["SuccessMessage"] = response.Message;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = response.Message;
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar veículo: {ex.Message}");

                TempData["ErrorMessage"] = "Ocorreu um erro ao criar o veículo. Por favor, tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Edit(int id)
        {
            var response = _vehicleService.GetById(id);

            if (response.Success)
                return View(response.Entity);
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public ActionResult Edit(Vehicle vehicle, IFormFile ImageFile)
        {
            try
            {
                ModelState.Remove(nameof(vehicle.ImageFile));
                if (string.IsNullOrEmpty(vehicle.ImageURL))
                    vehicle.ImageURL = string.Empty;

                if (ModelState.IsValid)
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var imageFolder = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");
                        string fileName = Path.GetFileName(ImageFile.FileName);

                        if (!Directory.Exists(imageFolder))
                            Directory.CreateDirectory(imageFolder);

                        string filePath = Path.Combine(imageFolder, fileName);

                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);
                        }

                        vehicle.ImageURL = $"{_BaseUrlImages}/Imagens/{fileName}";
                    }

                    var response = _vehicleService.Update(vehicle);

                    if (response.Success)
                    {
                        TempData["SuccessMessage"] = response.Message;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = response.Message;
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                    return View(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao editar veículo: {ex.Message}");

                TempData["ErrorMessage"] = "Ocorreu um erro ao editar o veículo. Por favor, tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var response = _vehicleService.Delete(id);

                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao remover o veículo: {ex.Message}");

                TempData["ErrorMessage"] = "Ocorreu um erro ao remover o veículo. Por favor, tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

        #region :: VehicleDetails ::
        public ActionResult CreateVehicleDetails(int pVehicleId)
        {
            var vehicleDetail = new VehicleDetail();
            vehicleDetail.VehicleId = pVehicleId;

            return PartialView(vehicleDetail);
        }

        [HttpPost]
        public ActionResult CreateVehicleDetails([Bind] VehicleDetail vehicleDetail, IFormFile ImageFile)
        {
            try
            {
                ModelState.Remove(nameof(vehicleDetail.ImageFile));
                ModelState.Remove(nameof(vehicleDetail.ImageURL));
                if (string.IsNullOrEmpty(vehicleDetail.ImageURL))
                    vehicleDetail.ImageURL = string.Empty;

                if (ModelState.IsValid)
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var imageFolder = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");
                        string fileName = Path.GetFileName(ImageFile.FileName);

                        if (!Directory.Exists(imageFolder))
                            Directory.CreateDirectory(imageFolder);

                        string filePath = Path.Combine(imageFolder, fileName);

                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);
                        }

                        vehicleDetail.ImageURL = $"{_BaseUrlImages}/Imagens/{fileName}";
                    }


                    var response = _vehicleService.CreateVehicleDetail(vehicleDetail);

                    if (response.Success)
                    {
                        TempData["SuccessMessage"] = response.Message;
                        return RedirectToAction("Details", new { id = vehicleDetail.VehicleId });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = response.Message;
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View(vehicleDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar veículo: {ex.Message}");

                TempData["ErrorMessage"] = "Ocorreu um erro ao criar o veículo. Por favor, tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult DetailsVehicleDetails(int VehicleDetailId, int VehicleId)
        {

            var response = _vehicleService.GetVehicleDetailById(VehicleDetailId);

            if (response.Success)
                return View(response.Entity);
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("Details", new { id = VehicleId });
            }
        }

        public ActionResult EditVehicleDetails(int VehicleDetailId, int VehicleId)
        {
            var response = _vehicleService.GetVehicleDetailById(VehicleDetailId);

            if (response.Success)
                return View(response.Entity);
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("Details", new { id = VehicleId });
            }
        }

        [HttpPost]
        public ActionResult EditVehicleDetails(VehicleDetail vehicleDetail, IFormFile ImageFile)
        {
            try
            {
                ModelState.Remove(nameof(vehicleDetail.ImageFile));
                ModelState.Remove(nameof(vehicleDetail.ImageURL));
                if (string.IsNullOrEmpty(vehicleDetail.ImageURL))
                    vehicleDetail.ImageURL = string.Empty;

                if (ModelState.IsValid)
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var imageFolder = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");
                        string fileName = Path.GetFileName(ImageFile.FileName);

                        if (!Directory.Exists(imageFolder))
                            Directory.CreateDirectory(imageFolder);

                        string filePath = Path.Combine(imageFolder, fileName);

                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);
                        }

                        vehicleDetail.ImageURL = $"{_BaseUrlImages}/Imagens/{fileName}";
                    }

                    var response = _vehicleService.UpdateVehicleDetail(vehicleDetail);

                    if (response.Success)
                    {
                        TempData["SuccessMessage"] = response.Message;
                        return RedirectToAction("Details", new { id = vehicleDetail.VehicleId });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = response.Message;
                        return RedirectToAction("Details", new { id = vehicleDetail.VehicleId });
                    }
                }
                else
                    return View(vehicleDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao editar veículo: {ex.Message}");
                TempData["ErrorMessage"] = "Ocorreu um erro ao editar a versão veículo. Por favor, tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult DeleteVehicleDetails(int VehicleDetailId, int VehicleId)
        {
            try
            {
                var response = _vehicleService.DeleteVehicleDetail(VehicleDetailId);

                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    return RedirectToAction("Details", new { id = VehicleId });
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                    return RedirectToAction("Details", new { id = VehicleId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao remover o veículo: {ex.Message}");

                TempData["ErrorMessage"] = "Ocorreu um erro ao remover a versão do veículo. Por favor, tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion
    }
}
