using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public partial class Comment
    {
        public string[] ValidationErrors()
        {
            var ret = new List<string>();
            if (string.IsNullOrEmpty(this.author_name))
                ret.Add("The author name is required");

            if (string.IsNullOrEmpty(this.body))
                ret.Add("The comment body is required");

            return ret.ToArray();
        }
    }
}
