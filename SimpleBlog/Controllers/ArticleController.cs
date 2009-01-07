using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SimpleBlog.Models;
using System.Configuration;

namespace SimpleBlog.Controllers
{
    public class ArticleController : Controller
    {
        protected const int DEFAULT_HOME_ARTICLE_COUNT = 5;
        protected static int _homeArticleCount = DEFAULT_HOME_ARTICLE_COUNT;
        protected static string _blogName;

        public ArticleController()
        {
            _blogName = ConfigurationManager.AppSettings["BlogName"];
            var articlePerPageSetting = ConfigurationManager.AppSettings["ArticlesOnHomepage"];
            if (!string.IsNullOrEmpty(articlePerPageSetting))
                int.TryParse(articlePerPageSetting, out _homeArticleCount);
        }

        public ActionResult Index()
        {
            ViewData["PageTitle"] = _blogName;
            ViewData["BlogTitle"] = _blogName;

            BlogDataContext db = new BlogDataContext();
            ViewData["Articles"] = (from art in db.Articles orderby art.created_on descending select art).Take(_homeArticleCount);

            return View();
        }

        protected Article _getArticle(BlogDataContext db, int year, int month, int day, string slug)
        {
            var article = from art in db.Articles
                          where art.slug == slug
                            && art.created_on.Value.Year == year
                            && art.created_on.Value.Month == month
                            && art.created_on.Value.Day == day
                          select art;

            return article.First();
        }


        public ActionResult Show(int year, int month, int day, string slug)
        {
            BlogDataContext db = new BlogDataContext();

            var article = _getArticle( db, year, month, day, slug );

            if (article == null)
                throw new HttpException(404, "Unable to find requested article");

            ViewData["PageTitle"] = string.Format("{0} - {1}", _blogName, article.title);
            ViewData["BlogTitle"] = _blogName;

            return View(article);
        }

        public ActionResult Comment(int year, int month, int day, string slug, [Bind(Prefix="comment")]Comment newComment)
        {
            BlogDataContext db = new BlogDataContext();

            var article = _getArticle(db, year, month, day, slug );
            if (article == null)
                throw new HttpException(404, "Unable to find requested article");

            if (newComment == null)
                throw new HttpException(500, "Unable to leave comment");

            var errs = newComment.ValidationErrors();
            if (errs.Count() > 0)
            {
                TempData["error"] = errs;
                return RedirectToAction("Show", article.LinkDataCollection);
            }

            newComment.created_on = DateTime.Now;
            article.Comments.Add(newComment);
            db.SubmitChanges();

            return RedirectToRoute("ArticleLookup", article.LinkDataCollection);
        }
    }
}
