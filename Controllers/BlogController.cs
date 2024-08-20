using Microsoft.AspNetCore.Mvc;
using BlogPlatform.Models;
using Microsoft.Extensions.Logging;  // Import the ILogger namespace
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<BlogController> _logger;  // Add ILogger to the controller

        public BlogController(BlogDbContext context, ILogger<BlogController> logger)
        {
            _context = context;
            _logger = logger;  // Initialize ILogger
        }

        public IActionResult Index()
        {
            try
            {
                // Retrieve the UserID from TempData
                var userIdString = TempData["UserID"]?.ToString();
                TempData.Keep(); 

                if (string.IsNullOrEmpty(userIdString))
                {
                    return RedirectToAction("Login", "User");
                }

                var blogs = _context.Blogs.Where(b => b.UserID == userIdString).ToList();

                return View(blogs);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving blogs: {Exception}", ex);
                ModelState.AddModelError("", "An error occurred while retrieving blogs. Please try again later.");
                return View("Error");  // Make sure to create an Error view to display the message
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewBag.UserID = TempData["UserID"]?.ToString();
                TempData.Keep();

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while preparing the Create view: {Exception}", ex);
                ModelState.AddModelError("", "An error occurred while preparing the Create view. Please try again later.");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Blogs.Add(blog);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(blog);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("An error occurred while saving the blog: {Exception}", ex);
                ModelState.AddModelError("", "An error occurred while saving the blog. Please ensure all fields are correctly filled and try again.");
                return View(blog);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while saving the blog: {Exception}", ex);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(blog);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                var blog = _context.Blogs.Find(id);
                if (blog == null)
                {
                    return NotFound();
                }

                return View(blog);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving the blog for update: {Exception}", ex);
                ModelState.AddModelError("", "An error occurred while retrieving the blog. Please try again later.");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Update(Blog blog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingBlog = _context.Blogs.Find(blog.Id);
                    if (existingBlog != null)
                    {
                        existingBlog.Title = blog.Title;
                        existingBlog.Content = blog.Content;

                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Blog not found.");
                }
                return View(blog);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("An error occurred while updating the blog: {Exception}", ex);
                ModelState.AddModelError("", "An error occurred while updating the blog. Please ensure all fields are correctly filled and try again.");
                return View(blog);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while updating the blog: {Exception}", ex);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(blog);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var blog = _context.Blogs.Find(id);
                if (blog == null)
                {
                    return NotFound();
                }

                _context.Blogs.Remove(blog);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting the blog: {Exception}", ex);
                ModelState.AddModelError("", "An error occurred while deleting the blog. Please try again later.");
                return View("Error");
            }
        }
    }
}
