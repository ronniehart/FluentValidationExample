using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidationExample.Data;
using FluentValidationExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluentValidationExample.Controllers
{
    public class PeopleController : Controller
    {
        private readonly FluentValidationExampleContext _context;
        private readonly IValidator<Person> _validator;

        public PeopleController(FluentValidationExampleContext context, IValidator<Person> validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _context.Person.ToListAsync();
            return View(people);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            var validationResult = await _validator.ValidateAsync(person);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(person);
            }

            _context.Add(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, Person person)
        {
            var validationResult = await _validator.ValidateAsync(person);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(person);
            }

            if (id != person.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PersonExists(person.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(long id)
        {
            var personExists = _context.Person.Any(p => p.Id == id);
            return personExists;
        }
    }
}