<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="crblg.aspx.cs" Inherits="crblg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Stylesheets/Blog/blogcreator.css" rel="Stylesheet" type="text/css"  />

    <script src="jquery-1.9.1.min.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="tinymce/tinymce.min.js"></script>
    
    <script type="text/javascript">
        tinymce.init({
            mode: "textareas",
            resize: false,
            statusbar: true,
            height: 300,
            plugins: ' autolink link wordcount textcolor emoticons tabfocus lists table paste charmap print preview'
        });
    </script>
    <script type="text/javascript">
        var qs = (function (a) {
            if (a == "") return {};
            var b = {};
            for (var i = 0; i < a.length; ++i) {
                var p = a[i].split('=');
                if (p.length != 2) continue;
                b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
            }
            return b;
        })(window.location.search.substr(1).split('&'));
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="heading">
            Create a Blog Article
        </div>
        <hr />
        <table class="main_table" cellpadding="5px;">
            <tr>
                <td width="15%">
                    <span class="label">
                        Blog Heading
                    </span>
                </td>
                <td width="85%">
                    <asp:TextBox ID="TextBox2" CssClass="text_boxes" placeholder="Type article's heading here (Note: Max Characters-60)" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <span class="label">
                        Blog Content
                    </span>
                </td>
                <td width="85%">
                    <asp:TextBox ID="TextBox1" TextMode="MultiLine"  runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>

        <div style="width:90%; margin:auto;" align="right" >
            <asp:Button ID="Button1" runat="server" CssClass="submit_button" Text="Submit Your Blog" onclick="Button1_Click" />
        </div>
    </div>    
</asp:Content>

