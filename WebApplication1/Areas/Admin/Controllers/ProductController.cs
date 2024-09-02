using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductListPreview.DataAcsess.Repository.IRepository;
using ProductListPreview.Models.Models;

namespace ProductListPreview.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties: "category").ToList();

            return View(products);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            return View(new Product());
        }


        [HttpPost]
        public IActionResult Create(Product obj, IFormFile? file)
        {
            if (file == null)
            {
                ModelState.AddModelError("ImageaUrl", "Please upload an image file.");
            }

            if (obj.CategoryID == 0)
            {
                ModelState.AddModelError("CategoryID", "Please select a category.");
            }


            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\Product");

                if (!Directory.Exists(productPath))
                {
                    Directory.CreateDirectory(productPath);
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                obj.ImageaUrl = @"\images\Product\" + fileName;
            }

            _unitOfWork.ProductRepository.Add(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product Created Successfully";
            return RedirectToAction("Index");



            return View(obj);
        }



        public IActionResult Edit(int id)
        {
            Product product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product obj, IFormFile? file)
        {


            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\Product");

                if (Directory.Exists(productPath))
                {
                    Directory.CreateDirectory(productPath);
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                obj.ImageaUrl = @"\images\Product\" + fileName;
            }
            _unitOfWork.ProductRepository.Update(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product Updated Successfully";

            return RedirectToAction("Index");


        }
        public IActionResult Delete(int id)
        {
            Product product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Deletepost(int id)
        {
            Product product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            TempData["Success"] = "Product Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}
