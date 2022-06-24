using App3.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Controllers
{
 
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
           this._roleManager = roleManager;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRoleViewModel.roleName
                };
                
                IdentityResult identityResult = await _roleManager.CreateAsync(identityRole);
                if(identityResult.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                foreach(IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(createRoleViewModel);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} not found";
                return View("NotFound");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            foreach (var user in _userManager.Users)
            {
                bool isUserInRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (isUserInRole)
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
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} not found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> models,
            string roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} not found";
                return View("NotFound");
            }
           for(int i =0; i<models.Count; i++)
            {
              var user = await   _userManager.FindByIdAsync(models[i].UserId);

                IdentityResult identityResult = null;
                if(models[i].IsSelected && !(await _userManager.IsInRoleAsync(user,role.Name)))
                {
                    identityResult = await  _userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!models[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if(identityResult.Succeeded)
                {
                    if(i<(models.Count -1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }
           
            return RedirectToAction("EditRole",new { Id = roleId});
        }


        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string id)
        {

            ViewBag.Id = id;

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} not found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    userName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                    userRoleViewModel.IsSelected = false;

                model.Add(userRoleViewModel);
            }
            return View(model);
        }





    }
}
