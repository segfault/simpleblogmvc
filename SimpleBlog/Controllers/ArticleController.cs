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


        public ActionResult Show(int year, int month, int day, string slug)
        {

            BlogDataContext db = new BlogDataContext();
            
            var article = from art in db.Articles
                          where art.slug == slug
                            && art.created_on.Value.Year == year
                            && art.created_on.Value.Month == month
                            && art.created_on.Value.Day == day
                          select art;
            if (article == null)
                throw new HttpException(404, "Unable to find requested article");

            ViewData["PageTitle"] = string.Format("{0} - {1}", _blogName, article.First().title);
            ViewData["BlogTitle"] = _blogName;

            return View(article.First());
        }
    }
}
