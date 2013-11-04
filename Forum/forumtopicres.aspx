<%@ Page Title="" Language="C#" MasterPageFile="~/Forum/MasterPage.master" AutoEventWireup="true" ValidateRequest = "false" CodeFile="forumtopicres.aspx.cs" Inherits="Forum_forumtopicres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../jquery-1.9.1.min.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="../Forum/tinymce/tinymce.min.js"></script>
    
    <script type="text/javascript">
        tinymce.init({
            mode: "textareas",
            resize: false,
            statusbar: true,
            height: 110,
            plugins: 'advlist autolink link wordcount textcolor emoticons tabfocus lists table paste charmap print preview'
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

        function Scrolldown() {
            $("html, body").animate({ scrollTop: $(document).height() - $(window).height() - '200' }, 900);
        }

        if (qs["scroll"] == "true") {
            window.onload = Scrolldown;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <table width="90%" style=" margin:5px 5% 8px 5%;">
            <tr>
                <td valign="middle">
                    <div id="where_are_u" runat="server">
                        
                    </div>
                </td>
            </tr>
        </table>
        <table width="90%" style=" margin:5px 5% 8px 5%;">
            <tr>
                <td>
                    <div>
                        <table width="100%">
                            <tr>
                                <td style=" width:75%"></td>
                                <td id="start_new_topic" align="right" style=" padding-right:10px;" runat="server">
                                    <asp:Button ID="strt_new_topic" CssClass="new_topic_button" runat="server" 
                                        Text="Start New Topic" onclick="str_new_topic" />
                                </td>
                                <td>                         
                                    <asp:Button ID="Cmnt_cur_topic" CssClass="new_topic_button" runat="server" 
                                        Text="Comment On This Topic" onclick="cmnt_on_this_topic" />                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">
                    <div>
                        <span id="forum_name" runat="server" style="font-family:Lucida Handwriting; font-size:24px; color:#a1a1a1"></span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="10%" align ="right"  style="">
                    <tr>
                        <td>
                            <div id="dv2" runat="server" style="width:50%;"></div>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
             <tr>
                <td>
                    <div id="table_topics" align="center" runat="server">
                        
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table  width="10%" align ="right"  style="">
                    <tr>
                        <td>
                            <div id="dv3" runat="server" style="width:50%;"></div>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
         </table>
         <br />
         <table id="tbl_post_comment_textbox" runat="server" width="90%" visible="false" style=" margin:5px 5% 8px 5%;">
            <tr class="normal_table_forum_row" style=" background:#f7f7f7;">
                <td style="padding:10px 10px 10px 10px;">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="comment_writing_user_name" runat="server" Text="Label"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img id="comment_writing_user_pic" alt="pic" style=" border:4px solid #d1d1d1" width="130" runat="server" class="forum_user_image" height="130"/>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:75%">
                    <asp:TextBox ID="TextBox2" TextMode="MultiLine" Width="80%" runat="server"></asp:TextBox>                        
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="Button1" runat="server" CssClass="new_topic_button" 
                        Text="Post Comment" onclick="Button1_Click" />
                </td>
            </tr>
         </table>
         <br />
    </div>
</asp:Content>

