using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductListPreview.DataAcsess.Repository.IRepository;
using ProductListPreview.Models.Models;
using Utility;

namespace ProductListPreview.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.categoryRepository.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToAction("Index");

            }
            return View();  
        }
        public IActionResult Edit(int id)
        {
            Category category = _unitOfWork.categoryRepository.Get(u => u.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Update(category);
                _unitOfWork.Save();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index");

            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            Category category = _unitOfWork.categoryRepository.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Deletepost(int id)
        {
            Category category = _unitOfWork.categoryRepository.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.categoryRepository.Remove(category);
            _unitOfWork.Save();
            TempData["Success"] = "Category Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}
