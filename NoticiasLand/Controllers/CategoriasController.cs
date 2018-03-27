using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NoticiasLand.Models;
using NoticiasLand.Models.Data;

namespace NoticiasLand.Controllers
{
    public class CategoriasController : Controller
    {
        private NoticiasLandDB db = new NoticiasLandDB();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(db.Categorias.ToList());
        }

        // GET: Categorias/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", new { msg = "Categoria Inválida", type = NotificationType.Error });
            }

            Categorias categorias = db.Categorias.Find(id);

            if (categorias == null)
            {
                return RedirectToAction("Index", new { msg = "A categoria submetida não existe.", type = NotificationType.Error });
            }
            return View(categorias);
        }

        // GET: Categorias/Criar
        public ActionResult Criar()
        {
            return View();
        }

        // POST: Categorias/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "CategoriaID,Nome,UrlSlug")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categorias);
                db.SaveChanges();
                return RedirectToAction("Index", new { msg = "Categoria criada com sucesso.", type = NotificationType.Success });
            }

            return View(categorias);
        }

        // GET: Categorias/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", new { msg = "Categoria Inválida", type = NotificationType.Error });
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return RedirectToAction("Index", new { msg = "A categoria submetida não existe.", type = NotificationType.Error });
            }
            return View(categorias);
        }

        // POST: Categorias/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "CategoriaID,Nome,UrlSlug")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { msg = "Categoria editada com sucesso.", type = NotificationType.Success });
            }
            return View(categorias);
        }

        // GET: Categorias/Apagar/5
        public ActionResult Apagar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return RedirectToAction("Index", new { msg = "A categoria submetida não existe.", type = NotificationType.Error });
            }
            return View(categorias);
        }

        // POST: Categorias/Apagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorias categorias = db.Categorias.Find(id);
            db.Categorias.Remove(categorias);
            db.SaveChanges();
            return RedirectToAction("Index");
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
