using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    [Authorize]
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        //public ViewResult List()
        //{
        //    var pieListViewModel = new PiesListViewModel(); //alternative new class
        //    pieListViewModel.Pies = _pieRepository.AllPies;
        //    pieListViewModel.CurrentCategory = "Cheese cakes";
        //    return View(pieListViewModel);

        //    //ViewBag.CurrentCategory = "Cheese cakes";
        //    //return View(_pieRepository.AllPies);
        //}

        public ViewResult List(string category)
        {
            var currentCategory = category;
            IEnumerable<Pie> pies;
            if (string.IsNullOrEmpty(category))
            {
                currentCategory = "All pies";
                pies = _pieRepository.AllPies.OrderBy(c => c.PieId);
            }
            else
            {
                var idOfCategory = _categoryRepository.AllCategories
                    .Where(c => c.CategoryName == category)
                    .Select(c => c.CategoryId)
                    .ToList();
                pies = _pieRepository.AllPies
                    .Where(c => c.CategoryId == idOfCategory[0])
                    .OrderBy(c=>c.PieId);
            }

            var pieListViewModel = new PiesListViewModel()
            {
                Pies = pies,
                CurrentCategory = currentCategory
            };
            return View(pieListViewModel);
        }

        public IActionResult Details(int id) // We used two different type of action. One of is View() and the other is NotFound() because of we use IActionResuult interface.
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }

    }
}