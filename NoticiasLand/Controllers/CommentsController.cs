using Microsoft.AspNet.Identity;
using NoticiasLand.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NoticiasLand.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CommentsModels
        public ActionResult Index()
        {
            return PartialView("_Index", db.Comments.ToList());
        }

        //GET: CommentsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentsModel commentsModel = db.Comments.Find(id);
            if (commentsModel == null)
            {
                return HttpNotFound();
            }
            return View(commentsModel);
        }

        // GET: CommentsModels/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: CommentsModels/Create
        [HttpPost]
        public ActionResult Create(CommentsModel commentsModel, CommentsNews commentsNews)
        {

            if (ModelState.IsValid)
            {
                var user = User.Identity;


                CommentsModel comment = new CommentsModel();
                comment.Text = commentsModel.Text;
                comment.IdUser = user.GetUserId();
                comment.UserName = user.Name;
                comment.NewsId = commentsNews.NewsId;


                db.Comments.Add(comment);
                db.SaveChanges();

                var lastComment = db.Comments.Max(p => p.Id);

                CommentsNews commentNews = new CommentsNews();
                commentNews.CommentId = lastComment;
                commentNews.NewsId = commentsNews.NewsId;

                db.CommentsNews.Add(commentNews);
                db.SaveChanges();

                return RedirectToAction("Details", "News", new { id = commentsModel.NewsId });
            }

            return View(commentsModel);
        }

        // GET: CommentsModels/Edit/5
        [ActionName("Edit")]
        public ActionResult Edit(int? id)
        {
            var res = db.Comments.FirstOrDefault(x => x.Id == id);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (res == null)
            {
                return HttpNotFound();
            }
            return View("Edit", res);
        }

        // POST: CommentsModels/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Text,IdUser,NewsId,UserName")] CommentsModel commentsModel)
        {
            if (ModelState.IsValid)
            {
                if (commentsModel.UserName == User.Identity.Name)
                {

                    CommentsModel comment = new CommentsModel();
                    comment.Id = commentsModel.Id;
                    comment.IdUser = commentsModel.IdUser;
                    comment.Text = commentsModel.Text;
                    comment.UserName = commentsModel.UserName;
                    comment.NewsId = commentsModel.NewsId;


                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();


                    return RedirectToAction("Details", "News", new { id = commentsModel.NewsId });
                }
            }
            return View(commentsModel);
        }

        // GET: CommentsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = db.Comments.FirstOrDefault(x => x.Id == id);

            if (res == null || res.UserName != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", res);
        }

        // POST: CommentsModels/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {

            var res = db.Comments.FirstOrDefault(x => x.Id == id);
            if (res == null) { return HttpNotFound(); }

            //Apagar da bd dos comentários
            db.Comments.Remove(res);
            db.Entry(res).State = EntityState.Deleted;

            db.SaveChanges();

            //var commentNews = new CommentsNews { CommentId = res.Id };
            //if (commentNews == null) { return HttpNotFound(); }


            ////Apagar da bd de ligação dos comentários => noticias
            //db.CommentsNews.Attach(commentNews);
            //db.Entry(commentNews).State = EntityState.Deleted;
            ////db.SaveChanges();




            return RedirectToAction("Details", "News", new { id = res.NewsId });
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
