using Microsoft.AspNetCore.Mvc;
using TP1.Models;
using System.Collections.Generic;

namespace TP1.Controllers
{
    [Route("Movie")]

    public class MovieController : Controller
    {

        List<Movie> movies = new List<Movie>
            {
                new Movie { Id = 1, Name = "The invisible guest" },
                new Movie { Id = 2, Name = "The fault in our stars " },
                new Movie { Id = 3, Name = "It" }
            };

        List<Customer> customers = new List<Customer>
        {
         new Customer { Id=1,Name="Customer 1"},
         new Customer { Id=2,Name="Customer 2"}

        };

        [HttpGet("")]
        public IActionResult Index()
        {
            return View(movies);
        }
        // Action pour éditer un film par son Id
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            return Content("Test Id: " + id);
        }
        //[Route("Movie/released/{year}/{month}")]
        // Action pour récupérer les films par mois et année de sortie
        [HttpGet("ByRelease/{year}/{month}")]
        public IActionResult ByRelease(int year, int month)
        {
            return Content($"Release Date: {month}/{year}");
        }

        // Action pour afficher les détails d'un film avec une liste de clients
        [HttpGet("Details")]
        public IActionResult Details()
        {
            var movie = new Movie { Id = 1, Name = "Inception" };

            var viewModel = new MovieCustomerViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }


        // Action pour afficher les détails d'un client par son Id
        [HttpGet("CustomerDetails/{id}")]
        public IActionResult CustomerDetails(int id)
        {
            var customer = new Customer { Id = id, Name = $"Customer {id}" };
            return Content($"Customer Details: {customer.Name}");
        }
        // Action pour éditer un film
        [HttpGet("Edit2/{id}")]
        public IActionResult Edit2(int id)
        {
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        [HttpPost("Edit2")]
        public IActionResult Edit2(Movie movie)
        {
            var existingMovie = movies.FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie == null)
                return NotFound();

            existingMovie.Name = movie.Name;
            return RedirectToAction("Index");
        }

        // Action pour supprimer un film
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }


        [HttpPost("Delete/{id}"), ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
                movies.Remove(movie);

            return RedirectToAction("Index");
        }
    }
}
