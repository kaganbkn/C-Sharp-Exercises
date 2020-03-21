using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
            var pieListViewModel = new PiesListViewModel(); //alternative new class
            pieListViewModel.Pies = _pieRepository.AllPies;
            pieListViewModel.CurrentCategory = "Cheese cakes";
            return View(pieListViewModel);

            //ViewBag.CurrentCategory = "Cheese cakes";
            //return View(_pieRepository.AllPies);
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