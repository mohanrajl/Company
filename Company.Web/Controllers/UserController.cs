using Company.Models;
using Company.Provider;
using Company.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Company.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            if (Session["IsAdmin"] == null)
            {
                return View("~/Views/Home.cshtml");
            }

            var userList = new UserProvider().GetUsers();

            if (userList != null && userList.Count > 0)
            {
                var userViewModelList = new List<UserViewModel>();

                foreach (var userItem in userList)
                {
                    var userViewModel = new UserViewModel();
                    userViewModel.Id = userItem.Id;
                    userViewModel.UserName = userItem.Name;
                    userViewModel.Password = userItem.Password;
                    userViewModel.Email = userItem.Email;
                    userViewModel.Admin = userItem.Admin;
                    userViewModel.Active = userItem.Active;
                    userViewModelList.Add(userViewModel);
                }

                return View(userViewModelList.AsEnumerable());
            }

            return View();
        }

        public ActionResult Create()
        {
            if (Session["IsAdmin"] == null)
            {
                return View("~/Views/Home.cshtml");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (Session["IsAdmin"] == null)
            {
                return View("~/Views/Home.cshtml");
            }

            if (userViewModel != null)
            {
                var userList = new UserProvider().GetUsers();

                if (userList != null && userList.Count > 0)
                {
                    if (!userList.Any(item => item.Name.Equals(userViewModel.UserName)))
                    {
                        var createUserModel = new User()
                        {
                            Name = userViewModel.UserName,
                            Password = userViewModel.Password,
                            Email = userViewModel.Email,
                            Admin = userViewModel.Admin,
                            Active = userViewModel.Active
                        };

                        var userId = new UserProvider().CreateUser(createUserModel);
                        if (!string.IsNullOrEmpty(userId))
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            if (Session["IsAdmin"] == null)
            {
                return View("~/Views/Home.cshtml");
            }

            if (id <= 0)
            {
                return RedirectToAction("Index");
            }

            var userList = new UserProvider().GetUsers();
            var user = userList.SingleOrDefault(item => item.Id == id);
            if (user != null)
            {
                var editUserViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    Admin = user.Admin,
                    Active = user.Active
                };

                return View(editUserViewModel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel editUserViewModel)
        {
            if (Session["IsAdmin"] == null)
            {
                return View("~/Views/Home.cshtml");
            }

            if (editUserViewModel != null)
            {
                var updateUserModel = new User()
                {
                    Id = editUserViewModel.Id,
                    Name = editUserViewModel.UserName,
                    Password = editUserViewModel.Password,
                    Email = editUserViewModel.Email,
                    Admin = editUserViewModel.Admin,
                    Active = editUserViewModel.Active
                };

                var user = new UserProvider().UpdateUser(updateUserModel);
                return RedirectToAction("Index");
            }
            
            return View();
        }
    }
}