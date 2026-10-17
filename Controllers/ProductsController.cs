using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsValidation.Models;
using ProductsValidation.Services;

namespace ProductsValidation.Controllers
{
    
    public class ProductsController : Controller
    {
        private List<Product> myProducts;

        public ProductsController(Data data)
        {
            myProducts = data.Products;
        }
        
        public IActionResult Index(int filterId, string filtername)
        {
            return View(myProducts);
        }
        
        public IActionResult View(int id)
        {
            Product prod = myProducts.Find(prod => prod.Id == id);
            if (prod != null)
            {
                return View(prod);
            }

            return View("NotExists");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product prod = myProducts.Find(prod => prod.Id == id);
            if (prod != null)
            {
                return View(prod);
            }

            return View("NotExists");
        } 
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            myProducts[myProducts.FindIndex(prod => prod.Id == product.Id)] = product;
            return RedirectToAction("View", new { id = product.Id });
        }

        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);
            myProducts.Add(product);
            return RedirectToAction("View", new { id = product.Id });
        }

        public IActionResult Create()
        {
            return View(new Product(){Id = myProducts.Last().Id + 1});
        }

        public IActionResult Delete(int id)
        {
            myProducts.Remove( myProducts.Find(product => product.Id == id));
            return View("Index", myProducts);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult EditPrices(Product.Category category)
        {
            var model = myProducts
                .Where(p => p.Type == category)
                .Select(p => new ProductPriceViewModel { Id = p.Id, Name = p.Name, Price = p.Price })
                .ToList();

            ViewBag.Category = category;
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPrices(Product.Category category, List<ProductPriceViewModel> prices)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = category;
                return View(prices);
            }

            foreach (var item in prices)
            {
                var product = myProducts.Find(p => p.Id == item.Id);
                if (product != null)
                    product.Price = item.Price;
            }

            return RedirectToAction("Index");
        }
    }
}
