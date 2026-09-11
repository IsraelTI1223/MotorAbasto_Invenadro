using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ApiPedidosController : Controller
    {
        // GET: ApiPedidos
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApiPedidos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApiPedidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiPedidos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ApiPedidos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApiPedidos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ApiPedidos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApiPedidos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
