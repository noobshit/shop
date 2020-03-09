using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Shop.Services;
using Shop.ViewModels;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ShopContext _context;
        private readonly IImageManager _imageManager;
        private readonly IMapper _mapper;

        public ProductController(ShopContext context, IImageManager imageManager, IMapper mapper)
        {
            _context = context;
            _imageManager = imageManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var products = _mapper.ProjectTo<ProductViewModel>(_context.Products).ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            var viewModel = _mapper.Map<ProductViewModel>(product);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel model)
        {
            if( ModelState.IsValid )
            {
                string uniqueFilename = null;

                if (model.ImagePath != null)
                {
                    uniqueFilename = _imageManager.Upload(model.ImagePath, "products");
                }

                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    ImagePath = uniqueFilename,
                };

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if( product == null )
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ProductViewModel>(product);
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if( product != null )
            {
                if (product.ImagePath != null)
                {
                    _imageManager.Delete(product.ImagePath, "products");
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if( product == null )
            {
                return NotFound();
            }

            var model = new EditProductViewModel
            {
                Name = product.Name,
                Price = product.Price,
                ExistingPath = product.ImagePath,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel model)
        {
            if( ModelState.IsValid )
            {
                var entity = _context.Products.FirstOrDefault(p => p.Id == model.Id);

                if( entity != null )
                {
                    entity.Name = model.Name;
                    entity.Price = model.Price;

                    if (model.ImagePath != null)
                    {
                        if (entity.ImagePath != null)
                        {
                            _imageManager.Delete(entity.ImagePath);
                        }

                        entity.ImagePath = _imageManager.Upload(model.ImagePath, "products");
                    }
                    _context.Products.Update(entity);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Details), new { id = model.Id });
                }
            }

            return View();
        }
    }
}