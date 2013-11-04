<%@ Page Title="" Language="C#" MasterPageFile="~/Forum/MasterPage.master" AutoEventWireup="true" CodeFile="forumnewtopic.aspx.cs" Inherits="Forum_forumnewtopic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <table width="90%" style=" margin:5px 5% 8px 5%;">
            <tr>
                <td style="text-align:center;">
                    <div>
                        <span id="new_topic" runat="server" style="font-family:Lucida Handwriting; font-size:24px; color:#a1a1a1"></span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="New_Topic_Forum" runat="server">
                        <table width="100%">
                            <tr>
                                <td valign="bottom">
                                    <span style="font-family:Californian FB; font-size:18px; font-weight:bold;">Topic Title</span>
                                </td>
                                <td valign="middle" style="width:85%;">
                                    <asp:TextBox ID="TextBox1" CssClass="lgnpage_lgn_textbox" Width="100%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr>
                                <td valign="Top">
                                    <span style="font-family:Californian FB; font-size:18px; font-weight:bold;">Topic Description</span>
                                </td>
                                <td valign="middle" style="width:84%;">
                                    <asp:TextBox ID="TextBox2" TextMode="MultiLine" Width="80%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%">
                            <tr>
                                <td valign="Top" ></td>
                                <td align="right" valign="middle" style="width:80%" >
                                    <asp:Button ID="Button1" runat="server" CssClass="new_topic_button" 
                                        Text="Post New Topic" onclick="Button1_Click" />                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>

