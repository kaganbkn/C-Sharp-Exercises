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
            var pieListViewModel=new PiesListViewModel(); //alternative new class
            pieListViewModel.Pies = _pieRepository.AllPies;
            pieListViewModel.CurrentCategory = "Cheese cakes";
            return View(pieListViewModel);

            //ViewBag.CurrentCategory = "Cheese cakes";
            //return View(_pieRepository.AllPies);
        }

    }
}