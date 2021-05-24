﻿using ColmanAppStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ColmanAppStore.Data;



namespace ColmanAppStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
     //   private readonly ColmanAppStoreContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
     
        }

        public IActionResult Index()
        {
            /*
            List<string> titles = new List<string>();
            for(int i=0; i<5; i++)
            {
                
            }
            titles.Add("Top Rated");
            titles.Add("Popular");
            titles.Add("Last Updated");
            */
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CategoryInfo() //(int id) ->to add the type of the category
        {
            Category category = new Category();
            category.Name = "Sport bkabka";

            return View(category);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
