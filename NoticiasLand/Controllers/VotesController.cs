using Microsoft.AspNet.Identity;
using NoticiasLand.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace NoticiasLand.Controllers
{
    public class VotesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Votes
        public ActionResult IndexPositive(NewsModels news)
        {


            return PartialView("_PositiveVote", news);
        }

        public ActionResult IndexNegative(NewsModels news)
        {


            return PartialView("_NegativeVote", news);
        }



        [Authorize(Roles = "User,Publisher,Admin")]
        public ActionResult PositiveVoteIndex(int? id)
        {

            if (id != null && id != 0)
            {
                var res = db.News.FirstOrDefault(x => x.Id == id);
                if (res == null) { HttpNotFound(); }

                var user = User.Identity;
                var resV = db.Votes.Where(u => u.UserName == user.Name).Where(v => v.IdNews == id);
                if (resV.Count() > 0) { return RedirectToAction("Index", "News"); }


                VotesModels vote = new VotesModels();
                vote.IdUser = user.GetUserId();
                vote.Vote = true;
                vote.IdNews = res.Id;
                vote.UserName = user.Name;

                db.Votes.Add(vote);
                db.SaveChanges();

                res.Votes = res.Votes + 1;


                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "News");
            }
            else
            {
                return HttpNotFound();
            }


        }


        [Authorize(Roles = "User,Publisher,Admin")]
        public ActionResult NegativeVoteIndex(int? id)
        {
            if (id != null && id != 0)
            {


                var res = db.News.FirstOrDefault(x => x.Id == id);
                if (res == null) { HttpNotFound(); }

                var user = User.Identity;
                var resV = db.Votes.Where(u => u.UserName == user.Name).Where(v => v.IdNews == id);
                if (resV.Count() > 0) { return RedirectToAction("Index", "News"); }

                VotesModels vote = new VotesModels();
                vote.IdUser = user.GetUserId();
                vote.Vote = false;
                vote.IdNews = res.Id;
                vote.UserName = user.Name;

                db.Votes.Add(vote);
                db.SaveChanges();

                res.Votes = res.Votes - 1;


                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "News");
            }
            else
            {
                return HttpNotFound();
            }
        }


        [Authorize(Roles = "User,Publisher,Admin")]
        public ActionResult NegativeVoteDetails(int? id)
        {
            if (id != null && id != 0)
            {


                var res = db.News.FirstOrDefault(x => x.Id == id);
                if (res == null) { HttpNotFound(); }

                var user = User.Identity;
                var resV = db.Votes.Where(u => u.UserName == user.Name).Where(v => v.IdNews == id);
                if (resV.Count() > 0) { return RedirectToAction("Details", "News", new { id = id }); }

                VotesModels vote = new VotesModels();
                vote.IdUser = user.GetUserId();
                vote.Vote = false;
                vote.IdNews = res.Id;
                vote.UserName = user.Name;

                db.Votes.Add(vote);
                db.SaveChanges();

                res.Votes = res.Votes - 1;


                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "News", new { id = id });

            }
            else
            {
                return HttpNotFound();
            }

        }


        [Authorize(Roles = "User,Publisher,Admin")]
        public ActionResult PositiveVoteDetails(int? id)
        {
            if (id != null && id != 0)
            {


                var res = db.News.FirstOrDefault(x => x.Id == id);
                if (res == null) { HttpNotFound(); }

                var user = User.Identity;
                var resV = db.Votes.Where(u => u.UserName == user.Name).Where(v => v.IdNews == id);
                if (resV.Count() > 0) { return RedirectToAction("Details", "News", new { id = id }); }

                VotesModels vote = new VotesModels();
                vote.IdUser = user.GetUserId();
                vote.Vote = true;
                vote.IdNews = res.Id;
                vote.UserName = user.Name;

                db.Votes.Add(vote);
                db.SaveChanges();

                res.Votes = res.Votes + 1;


                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "News", new { id = id });
            }
            else
            {
                return HttpNotFound();
            }

        }

    }
}