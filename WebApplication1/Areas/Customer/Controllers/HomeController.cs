using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ProductListPreview.Models.Models;
using ProductListPreview.DataAcsess.Repository;
using ProductListPreview.DataAcsess.Repository.IRepository;

namespace ProductListPreview.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties: "category").ToList();

            return View(products);
        }
        public IActionResult Search(string searchTerm)
        {
            var lowerSearchTerm = searchTerm?.ToLower();

            IEnumerable<Product> products;

            if (string.IsNullOrEmpty(lowerSearchTerm))
            {
                products = _unitOfWork.ProductRepository.GetAll().ToList();
            }
            else
            {
                products = _unitOfWork.ProductRepository.GetAll()
                    .Where(p => p.Title.ToLower().Contains(lowerSearchTerm))
                    .ToList();
            }

            return PartialView("_ProductList", products);
        }

        public IActionResult Sort(string sortOption) { 
            IEnumerable<Product> products;
            products = _unitOfWork.ProductRepository.GetAll().ToList();
            switch (sortOption) 
            {
                case "A-Z":
                    products = products.OrderBy(p => p.Title).ToList();
                    break;
                case "Z-A":
                    products = products.OrderByDescending(p => p.Title).ToList();
                    break;
                case "L to H":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case "H to L":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;

            }
            return View("_ProductList", products);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}