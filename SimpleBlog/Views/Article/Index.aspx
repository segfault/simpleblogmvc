<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Views/Shared/Site.Master" CodeBehind="Index.aspx.cs" Inherits="SimpleBlog.Views.Article.Index" %>
<%@ Import Namespace="SimpleBlog.Models" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<% foreach (Article article in ViewData["Articles"] as IEnumerable<Article>)
   { %>
   <div id="<%= article.slug %>" class="articleContainer">
        <h2><%= Html.RouteLink(article.title, "ArticleLookup", article.LinkDataCollection )%></h2><br />
        <sub>posted on <%= article.created_on.Value.ToShortDateString() %> by <%= article.User.name %></sub>
        <div class="articleBody"><p><%= article.body %></p></div>
        <sub class="articleFooter"><%= article.Comments.Count %> comments</sub>
   </div>
<% } %>
</asp:Content>
