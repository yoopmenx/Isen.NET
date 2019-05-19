using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Isen.DotNet.Web.Controllers
{
    public abstract class BaseController<T, TRepo> : Controller
        where T : BaseModel<T>
        where TRepo : IBaseRepository<T>
    {
        #region Membres et constructeur

        protected readonly TRepo Repository;

        protected BaseController(TRepo repository)
        {
            Repository = repository;
        }

        #endregion

        #region Actions MVC

        public virtual IActionResult Index() => View(Repository.GetAll());

        [HttpGet] // facultatif car par défaut
        public virtual IActionResult Edit(int? id)
        {
            if (id == null) return View();
            return View(Repository.Single(id.Value));
        }

        [HttpPost]
        public virtual IActionResult Edit(int id, [Bind] T model)
        {
            Repository.Update(model);
            Repository.SaveChanges();
            return RedirectToAction("Index");
        }

        public virtual IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Repository.Delete(id.Value);
                Repository.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region API

        [HttpGet]
        [Route("api/[controller]/status")]
        public virtual JsonResult Status()
        {
            dynamic result = new ExpandoObject();
            result.serverTime = DateTime.Now;
            result.controller = typeof(T).Name;
            return Json(result);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public virtual JsonResult GetById(int id)
        {
            var entity = Repository.Single(id);
            if (entity == null) return Json(new {});
            return Json(entity.ToDynamic());
        }

        [HttpGet]
        [Route("api/[controller]")]
        public virtual JsonResult GetAll()
        {
            var all = Repository
                .GetAll()
                .Select(e => e.ToDynamic())
                .ToList();
            return Json(all);
        }

        #endregion
    }
}
