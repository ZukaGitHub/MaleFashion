﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplicationCrud.Data;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.Helpers;
using WebApplicationCrud.Models;
using WebApplicationCrud.Models.AdministrationModels;
using WebApplicationCrud.Models.Identity;
using WebApplicationCrud.ViewModels;
using WebApplicationCrud.ViewModels.AdministrationVms;
using WebApplicationCrud.ViewModels.PanelVMs;

namespace WebApplicationCrud.Controllers
{
    public class AdministrationController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private CRUDdbcontext _ctx;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public AdministrationController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            CRUDdbcontext ctx,
            RoleManager<IdentityRole> roleManager,
             IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _ctx = ctx;
            _roleManager = roleManager;
            _mapper = mapper; 
        }
        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole() {

                    Name = vm.RoleName

                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(vm);
        }
        [HttpGet]
        public IActionResult NewsLetter()
        {
            var newsLetters = _ctx.NewsLetters.ToList();
            if (newsLetters != null && newsLetters.Count() > 0)
            {
                return View(newsLetters);
            }
            return View(new List<NewsLetter>());
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {



            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // UserManager service GetClaimsAsync method gets all the current claims of the user
            var existingUserClaims = await _userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                UserId = userId
            };

            // Loop through each claim we have in our application
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                // If the user has the claim, set IsSelected property to true, so the checkbox
                // next to the claim is checked on the UI
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }

                model.Claims.Add(userClaim);
            }

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return View("NotFound");
            }

            // Get all the user existing claims and delete them
            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }

            // Add all the claims that are selected on the UI
            result = await _userManager.AddClaimsAsync(user,
                model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = model.UserId });

        }
      

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                var isinRole = await _userManager.IsInRoleAsync(user, role.Name);
              
                if (isinRole)
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

             return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>  ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var tempuserRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    tempuserRolesViewModel.IsSelected = true;
                }
                else
                {
                    tempuserRolesViewModel.IsSelected = false;
                }

                model.Add(tempuserRolesViewModel);
            }
            var editUserModel = new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles,
                Claims = userClaims.Select(c => c.Value).ToList()

            };

            return Json(new { isValid = true, html = RazorHelper.RenderRazorViewToString(this, "_RolesList", editUserModel.Roles) });
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Selected User Could Not Be Found";
                return View("NotFound");

            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var model = new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles,
                Claims = userClaims.Select(c => c.Value).ToList() 

            };

            return View(model);
                    
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} could not be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
             

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} could not be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("GetRoles");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AccountDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            var accountVm = _mapper.Map<AccountViewModel>(user);


            return View(accountVm);
        }
      

        [HttpPost]
        public async Task<IActionResult> AccountDetailsAsync(AccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
               
                user.Email = vm.Email;
                user.UserName = vm.UserName;
                user.PhoneNumber = vm.PhoneNumber;
                user.DeliveryInfo.AdditionalDescription = vm.AdditionalDescription;
                user.DeliveryInfo.Address = vm.Address;
                user.DeliveryInfo.Address2 = vm.Address2;
                user.DeliveryInfo.City = vm.City;
                user.DeliveryInfo.Country = vm.Country;
                user.DeliveryInfo.Street = vm.Street;
                await _userManager.UpdateAsync(user);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index", "Panel");
            }
            return View(vm);
          
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role is Not Found";
                return View("NotFound");        
            }
            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name

            };
            foreach(var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName); 
                }
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role is Not Found";
                return View("NotFound");
            }
            else {

                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");

                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }



                return View(model);
            }
          


           
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role with Specified Id cannot be found";
                return View("NotFound");

            }
            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersRole(List<UserRoleViewModel> model,string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            
            if (role == null)
            {
                ViewBag.ErrorMessage = "Specified Role could not be found";
                return View("NotFound");
            }
            for(int i = 0; i < model.Count; i++)
            {
                IdentityResult result = null;
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user,role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count-1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }
                
            return View();

        }

        public async Task<IActionResult> OrderHistory()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var orders = _ctx.Orders.Include(s => s.OrderDetails).Include(s => s.user).Where(s => s.user == user).Include(s=>s.user.DeliveryInfo).
                    ToList();
                return View(orders);
            }
         

            return View(new List<Order>());

        }
    }
}
