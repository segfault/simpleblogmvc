<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="SimpleBlog.Views.Article.Show" %>
<%@ Import Namespace="MvcContrib.UI.Html" %>
<%@ Import Namespace="SimpleBlog.Models" %>
<%@ Import Namespace="System.Web.Mvc" %>
<asp:Content ID="showContent" ContentPlaceHolderID="MainContent" runat="server">
   <div id="<%= ViewData.Model.slug %>" class="articleContainer">
        <h2><%= Html.RouteLink(ViewData.Model.title, "ArticleLookup", ViewData.Model.LinkDataCollection)%></h2><br />
        <sub>posted on <%= ViewData.Model.created_on.Value.ToShortDateString()%> by <%= ViewData.Model.User.name%></sub>
        <div class="articleBody"><%= ViewData.Model.body%></div>
        <div id="comments" class="articleComments">
        <%foreach (Comment comment in ViewData.Model.Comments)
          { %>
          <div id="comment-<%= comment.id %>">
          <div><%= comment.author_name%></div>
          <%= comment.body %>
          </div>
        <% } %>
        </div>
   </div>
</asp:Content>
