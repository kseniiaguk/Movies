using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using Movies.Models.ViewModels;

namespace Movies.Controllers
{
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;
        public UsersController(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> PersonalAccount()
        {
            int? currentId = HttpContext.Session.GetInt32("user");
            if (currentId == null) // TODO: make _currentUser not null after redirection
            {
                return RedirectToAction("Index", "Home");
            }
            UserModel user = await _context.Users.Include(tempUser => tempUser.SpecifiedMovies).Where(item => item.Id == currentId).FirstOrDefaultAsync();
            //UserModel user = await _context.Users.FindAsync(currentId);
            IEnumerable<SpecifiedMovieModel> specifiedMovies = user?.SpecifiedMovies ?? Enumerable.Empty<SpecifiedMovieModel>();
            return View("PersonalAccount", specifiedMovies.Select(item => {
                                                                            _context.Entry(item).Reference("CommonMovie").Load();
                                                                            return (item.CommonMovie, item);
            }));
        }
        // GET: UserModels/Create
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<UserModel> matchedUsers = _context.Users.Where(user => user.Email == userModel.Email || user.Name == userModel.Name);
                if (!matchedUsers.Any())
                {
                    UserModel tempUser = userModel.CreateUserModel();
                    _context.Add(tempUser);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetInt32("user", tempUser.Id);
                    //await HttpContext.Session.CommitAsync();
                    return RedirectToAction(nameof(PersonalAccount));// TODO : redirect to personal account
                }
                else
                {
                    if (matchedUsers.Any(user => user.Email == userModel.Email))
                    {
                        ModelState.AddModelError("Email", "Пользователь с таким электронным адресом уже существует");
                    }
                    if (matchedUsers.Any(user => user.Name == userModel.Name))
                    {
                        ModelState.AddModelError("Name", "Пользователь с таким именем уже существует");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }
            return View(userModel);
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(string email, string password)
        {
            IEnumerable<UserModel> possibleUser = _context.Users.Where(user => user.Email == email);
            if (possibleUser.Any())
            {
                UserModel currentUser = possibleUser.FirstOrDefault(user => user.Password == password);
                if (currentUser != null)
                {
                    HttpContext.Session.SetInt32("user", currentUser.Id);
                    return RedirectToAction(nameof(PersonalAccount));
                }
                else
                {
                    ModelState.AddModelError("Password", "Пароль введён неправильно");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Нет пользователя с таким электронным адресом");
            }
            return View(new LoginViewModel { Email = email });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ClickingAddButton()
        {
            HttpContext.Items.Add("pressed", true);
            return View("AddingMovie");
        }
        public async Task<IActionResult> AddingMovie(AddedMovieViewModel movieInfo)
        {
            int? id = HttpContext.Session.GetInt32("user");
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    UserModel user = await _context.Users.FindAsync(id);
                    (CommonMovieModel commonMovie, SpecifiedMovieModel specifiedMovie) = movieInfo.CreateMovie(user);
                    _context.CommonMovies.Add(commonMovie);
                    _context.SpecifiedMovies.Add(specifiedMovie);
                    await _context.SaveChangesAsync();
                    return Json(new { IsValid = true, Redirect = true, Url = Url.Action("PersonalAccount") });
                }
                else
                {
                    ModelState.AddModelError("", "Проверьте, что все поля заполнены");
                }
            }
            else
            {
                return RedirectToAction("Login"); //TODO: saving changes while user is redirecting to Login
            }
            return Json(new { IsValid = false,
                              Redirect = false,
                              Html = Helper.RenderRazorViewToString(this, "AddingMovie", movieInfo) });
        }
    }
}
