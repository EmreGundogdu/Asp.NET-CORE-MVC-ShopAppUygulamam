﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApplication.EmailServices;
using ShopApplication.Identity;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopApplication.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager; ;
            _emailSender = emailSender;
        }
        public IActionResult Login(string returnUrl=null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var user = await _userManager.FindByIdAsync(model.UserName);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user==null)
            {
                ModelState.AddModelError("", "Bu Kullanıcı Adı İle Bir Kullanıcı Bulunamadı");
                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen Mail Hesabınıza Gelen Link İle Hesabınızı Onaylayınız");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user,model.Password,true,false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");
            }
            ModelState.AddModelError("", "Girilen Kullanıcı Adı Veya Parola Yanlış");
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail","Account",new
                {
                    userId = user.Id,
                    token = code
                });
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı Onaylayınız", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:44360{url}'>tıklayınız</a>");
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if (userId==null||token==null)
            {
                CreateMessage("Geçersiz Token", "danger");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    CreateMessage("Hesabınız Onaylandı", "success");
                    return View();
                }
            }
            CreateMessage("Hesabınız Onaylanmadı", "warning");
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user==null)
            {
                return View();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            await _emailSender.SendEmailAsync(Email,"ResetPassword", $"Parolanızı Yenilmek için linke <a href='https://localhost:44360{url}'>tıklayınız</a>");
            return View();
        }
        public IActionResult ResetPassword(string userId,string token)
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            return View();
        }
        private void CreateMessage(string message, string alertType)
        {
            var msg = new Messages()
            {
                Message = message,
                AlertType = alertType
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
        }
    }
}
