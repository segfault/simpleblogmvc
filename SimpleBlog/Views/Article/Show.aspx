<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="SimpleBlog.Views.Article.Show" %>
<%@ Import Namespace="SimpleBlog.Models" %>
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
           <div id="commentPreview"></div>
           <div class="commentErrors"></div>
           <div id="newComment">
           <% using (Html.BeginForm("Comment", "Article", ViewData.Model.LinkDataCollection, FormMethod.Post, new { id="commentForm" }))
              {  %>
           <label>Name*</label><%= Html.TextBox("comment.author_name")%><br />
           <label>Email (not published)</label><%= Html.TextBox("comment.author_email")%><br />
           <label>Url</label><%= Html.TextBox("comment.author_url")%><br />
           <%= Html.TextArea("comment.body", new { cols = "50", rows = "10" })%><br />
           <%= Html.FormHelper().Submit()%>
           <% } %>
           </div>
           <script type="text/javascript">
           </script>
        </div>
   </div>
</asp:Content>
