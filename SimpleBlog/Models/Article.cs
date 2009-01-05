using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public partial class Article // see BlogDataClasses for the linq 2 sql portion
    {
        public object LinkDataCollection
        {
            get
            {
                return new
                {
                    year = this.created_on.Value.Year,
                    month = this.created_on.Value.Month,
                    day = this.created_on.Value.Day,
                    slug = this.slug
                };
            }
        }
    }
}
