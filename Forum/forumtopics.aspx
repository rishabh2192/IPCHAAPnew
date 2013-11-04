<%@ Page Title="" Language="C#" MasterPageFile="~/Forum/MasterPage.master" AutoEventWireup="true" CodeFile="forumtopics.aspx.cs" Inherits="Forum_forumtopics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <table width="90%" style=" margin:5px 5% 8px 5%;">
            <tr>
                <td style="text-align:center;">
                    <div>
                        <span id="forum_name" runat="server" style="font-family:Lucida Handwriting; font-size:24px; color:#a1a1a1"></span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" style=" margin:2px 0% 3px 0%;">
                        <tr>
                            <td valign="middle">
                                <div id="where_are_u" style=" height:20px;" runat="server">
                        
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table width="100%">
                            <tr>
                                <td style=" width:80%"></td>
                                <td></td>
                                <td id="start_new_topic" align="right" runat="server">
                                    <asp:Button ID="Button1" CssClass="new_topic_button" runat="server" 
                                        Text="Start New Topic" onclick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
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
                    <div id="table_topics" runat="server">
                        
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="10%" align ="right"  style="">
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
    </div>
</asp:Content>

