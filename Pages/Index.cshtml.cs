using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FinalGian.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Vehicle> Vehicles { get; set; }

        [BindProperty]
        public SearchParameters SearchParams { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            SearchParams = new SearchParameters();
        }

        public void OnGet(string? keyword = "", string? sortBy = "VehicleId", bool sortAsc = true, int pageSize = 5, int pageIndex = 1)
        {
            var allVehicles = new List<Vehicle>
            {
                new Vehicle() { VehicleId = 1, Make = "Toyota", Model = "Corolla", Year = 2019, Mileage = 15000 },
                new Vehicle() { VehicleId = 2, Make = "Ford", Model = "Mustang", Year = 2018, Mileage = 25000 },
                new Vehicle() { VehicleId = 3, Make = "Honda", Model = "Civic", Year = 2020, Mileage = 12000 },
                new Vehicle() { VehicleId = 4, Make = "Chevrolet", Model = "Malibu", Year = 2017, Mileage = 35000 },
                new Vehicle() { VehicleId = 5, Make = "Nissan", Model = "Altima", Year = 2021, Mileage = 8000 },
                new Vehicle() { VehicleId = 6, Make = "Hyundai", Model = "Sonata", Year = 2019, Mileage = 18000 },
                new Vehicle() { VehicleId = 7, Make = "Mazda", Model = "CX-5", Year = 2018, Mileage = 22000 },
                new Vehicle() { VehicleId = 8, Make = "Toyota", Model = "Camry", Year = 2020, Mileage = 10000 },
                new Vehicle() { VehicleId = 9, Make = "Ford", Model = "Fusion", Year = 2016, Mileage = 45000 },
                new Vehicle() { VehicleId = 10, Make = "Kia", Model = "Optima", Year = 2021, Mileage = 5000 },
                new Vehicle() { VehicleId = 11, Make = "BMW", Model = "X5", Year = 2019, Mileage = 17000 },
                new Vehicle() { VehicleId = 12, Make = "Mercedes-Benz", Model = "E-Class", Year = 2018, Mileage = 28000 },
                new Vehicle() { VehicleId = 13, Make = "Audi", Model = "A4", Year = 2020, Mileage = 14000 },
                new Vehicle() { VehicleId = 14, Make = "Chevrolet", Model = "Impala", Year = 2017, Mileage = 32000 },
                new Vehicle() { VehicleId = 15, Make = "Ford", Model = "Explorer", Year = 2020, Mileage = 11000 },
                new Vehicle() { VehicleId = 16, Make = "Toyota", Model = "Highlander", Year = 2021, Mileage = 6000 },
                new Vehicle() { VehicleId = 17, Make = "Subaru", Model = "Outback", Year = 2018, Mileage = 25000 },
                new Vehicle() { VehicleId = 18, Make = "Honda", Model = "Accord", Year = 2020, Mileage = 15000 },
                new Vehicle() { VehicleId = 19, Make = "Nissan", Model = "Maxima", Year = 2019, Mileage = 13000 },
                new Vehicle() { VehicleId = 20, Make = "Mazda", Model = "Mazda3", Year = 2021, Mileage = 4000 },
                new Vehicle() { VehicleId = 21, Make = "Hyundai", Model = "Elantra", Year = 2017, Mileage = 37000 },
                new Vehicle() { VehicleId = 22, Make = "Kia", Model = "Soul", Year = 2019, Mileage = 22000 },
                new Vehicle() { VehicleId = 23, Make = "Ford", Model = "Escape", Year = 2020, Mileage = 9000 },
                new Vehicle() { VehicleId = 24, Make = "Chevrolet", Model = "Equinox", Year = 2021, Mileage = 7000 },
                new Vehicle() { VehicleId = 25, Make = "Toyota", Model = "Rav4", Year = 2021, Mileage = 8000 }
            };

            // Search
            if (!string.IsNullOrEmpty(keyword))
            {
                allVehicles = allVehicles.Where(v => v.Make.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                                      v.Model.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                                      v.Year.ToString().Contains(keyword) ||
                                                      v.Mileage.ToString().Contains(keyword)).ToList();
            }

            // Sorting
            if (sortBy == "VehicleId")
            {
                allVehicles = sortAsc ? allVehicles.OrderBy(v => v.VehicleId).ToList() : allVehicles.OrderByDescending(v => v.VehicleId).ToList();
            }
            else if (sortBy == "Make")
            {
                allVehicles = sortAsc ? allVehicles.OrderBy(v => v.Make).ToList() : allVehicles.OrderByDescending(v => v.Make).ToList();
            }
            else if (sortBy == "Model")
            {
                allVehicles = sortAsc ? allVehicles.OrderBy(v => v.Model).ToList() : allVehicles.OrderByDescending(v => v.Model).ToList();
            }
            else if (sortBy == "Year")
            {
                allVehicles = sortAsc ? allVehicles.OrderBy(v => v.Year).ToList() : allVehicles.OrderByDescending(v => v.Year).ToList();
            }
            else if (sortBy == "Mileage")
            {
                allVehicles = sortAsc ? allVehicles.OrderBy(v => v.Mileage).ToList() : allVehicles.OrderByDescending(v => v.Mileage).ToList();
            }

            // Pagination
            int totalVehicles = allVehicles.Count;
            int skip = (pageIndex - 1) * pageSize;
            Vehicles = allVehicles.Skip(skip).Take(pageSize).ToList();

            SearchParams = new SearchParameters
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                PageCount = (int)Math.Ceiling((double)totalVehicles / pageSize),
                SearchCount = totalVehicles,
                SortBy = sortBy,
                SortAsc = sortAsc,
                Keyword = keyword
            };
        }

        public class SearchParameters
        {
            public string? SortBy { get; set; }
            public bool SortAsc { get; set; } = true;
            public string? Keyword { get; set; }
            public int PageSize { get; set; } = 5;
            public int PageIndex { get; set; } = 1;
            public int? PageCount { get; set; }
            public int? SearchCount { get; set; }
        }

        public class Vehicle
        {
            public int VehicleId { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public int Year { get; set; }
            public int Mileage { get; set; }
        }
    }
}
