using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBlog;
using SimpleBlog.Controllers;
using SimpleBlog.Models;

namespace SimpleBlog.Tests.Controllers
{
    /// <summary>
    /// Simple Unit tests of the article controller
    /// </summary>
    [TestClass]
    public class ArticleControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ArticleController controller = new ArticleController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            ViewDataDictionary viewData = result.ViewData;
            BlogDataContext db = new BlogDataContext();
            var featuredArticles = (from art in db.Articles orderby art.created_on descending select art).Take(5);
            Assert.AreEqual(featuredArticles, viewData["Articles"]);            
        }

    }
}
