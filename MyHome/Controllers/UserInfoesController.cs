using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyHome.Models;
using MyHome.Tools;
using System.Data.Entity.Validation;
using MyHome.ViewModels;
using MyHome.Enums;
using System.Web.Security;

namespace MyHome.Controllers
{
    public class UserInfoesController : BaseController
    {
        [Authorize, AuthorizeRole(Role.超级管理员)]
        public ActionResult Index()
        {
            return View(Cacher.UserInfoes);
        }

        [Authorize, AuthorizeRole(Role.超级管理员, Role.会员)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = Cacher.UserInfoes.SingleOrDefault(t => t.ID == id.Value);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Create
        public ActionResult Create()
        {
            return View(new ViewUserInfo() { Sex = Sex.男 });
        }

        [HttpPost]
        public ActionResult Create(ViewUserInfo model)
        {
            if (ModelState.IsValid)
            {
                var userInfo = new UserInfo()
                {
                    UserName = model.UserName,
                    Password = Util.ToMD5(model.Password),
                    Role = Role.会员,
                    Sex = model.Sex
                };
                db.UserInfo.Add(userInfo);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.AddressInfoes, "UserInfoID", "Province", userInfo.ID);
            return View(userInfo);
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.AddressInfoes, "UserInfoID", "Province", userInfo.ID);
            return View(userInfo);
        }

        // GET: UserInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.UserInfo.Find(id);
            db.UserInfo.Remove(userInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ActionResult CheckUserName(string username)
        {
            var result = false;
            if (!Cacher.UserInfoes.Any(t => t.UserName == username))
                result = true;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(UserLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(User.Identity.Name))
                    return RedirectToAction("Index", "Home");
                var user = Cacher.UserInfoes.SingleOrDefault(it => string.Compare(it.UserName, model.UserName, true) == 0);
                if (user != null)
                {
                    if (user.Password != Util.ToMD5(model.Password))
                    {
                        ModelState.AddModelError("Password", "密码错误");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, model.RememberMe);

                        if (!string.IsNullOrEmpty(returnUrl))
                            return Redirect(returnUrl);
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "用户名不存在!");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
