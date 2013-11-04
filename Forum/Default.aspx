<%@ Page Title="" Language="C#" MasterPageFile="~/Forum/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Forum_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <table width="85%" style=" margin:5px 7% 8px 8%;">
            <tr>
                <td style="text-align:center;">
                    <div>
                        <span style="font-family:Lucida Handwriting; font-size:24px; color:#a1a1a1">DISCUSSION FORUMS</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="table_group" runat="server">
                        
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>

