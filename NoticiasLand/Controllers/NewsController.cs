using NoticiasLand.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NoticiasLand.Controllers
{
    public class NewsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index()
        {
            ViewBag.isPublisher = "No";

            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Publisher") || User.IsInRole("Admin"))
                {
                    ViewBag.isPublisher = "Yes";
                }
                return View(db.News.ToList());
            }
            else
            {
                return View(db.News.ToList());
            }
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = db.News.FirstOrDefault(x => x.Id == id);

            if (res == null)
            {
                return HttpNotFound();
            }

            return View(res);
        }

        // GET: News/Create
        [Authorize(Roles = "Admin, Publisher"), HttpGet, ActionName("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [Authorize(Roles = "Admin, Publisher")]
        public ActionResult Create(NewsModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = User.Identity.Name;

                    NewsModels news = new NewsModels();
                    news.Title = model.Title;
                    news.Text = model.Text;
                    news.Publisher = User.Identity.Name;

                    db.News.Add(news);
                    db.SaveChanges();
                }

                // If we got this far, something failed, redisplay form 
                return RedirectToAction("Index", "News");
            }
            catch
            {
                return View();
            }
        }

        // GET: News/Edit/5
        [Authorize(Roles = "Admin, Publisher"), HttpGet, ActionName("Edit")]
        public ActionResult Edit(int? id)
        {
            var res = db.News.FirstOrDefault(x => x.Id == id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (res == null)
            {
                return HttpNotFound();
            }

            return View(res);
        }

        // POST: News/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin, Publisher"), ActionName("Edit")]
        public ActionResult Edit(int id, NewsModels model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (User.IsInRole("Admin") || model.Publisher == User.Identity.Name)
                    {
                        var user = User.Identity.Name;

                        NewsModels news = new NewsModels();
                        news.Id = model.Id;
                        news.Title = model.Title;
                        news.Text = model.Text;
                        news.Publisher = User.Identity.Name;

                        db.Entry(news).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    else
                    {
                        ViewBag.Error = "You cannot edit the News item because, either you are not the publisher or you are not Admin.";
                        return RedirectToAction("Index", "News");
                    }
                }
                return RedirectToAction("Details", "News", new { id = model.Id });

            }
            catch
            {
                ViewBag.Error = "An error occurred while trying to edit the News item.";

                return RedirectToAction("Index", "News");
            }
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            var res = db.News.FirstOrDefault(x => x.Id == id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (res == null)
            {
                return HttpNotFound();
            }

            return View(res);
        }

        // POST: News/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, NewsModels news)
        {
            try
            {

                if (User.IsInRole("Admin") || news.Publisher == User.Identity.Name)
                {
                    db.Entry(news).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Error = "You cannot delete this News item, either because you are not the publisher or you are not Admin.";
                }
                return RedirectToAction("Index", "News");
            }
            catch
            {
                ViewBag.Error = "An error occurred while trying to delete the News item.";

                return RedirectToAction("Index", "News");
            }
        }
    }
}
