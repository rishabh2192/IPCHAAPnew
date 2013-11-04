<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="esol_serv.aspx.cs" Inherits="esol_serv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Stylesheets/esolserv.css" type="text/css" rel="Stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container_main" align="center">
        <div align="center">
            <span style="font-family:Corbel; font-size:34px; text-shadow:0px -1px 0px #00000 ; color:#a1a1a1;">
                E-Solutions Library Pricing
            </span>
        </div>
        <div id="container"  style="height:480px;">
    
            <div class="pricingtable">
              <div class="top">
                <h2>Regular</h2>
              </div>
              <ul>
                <li><strong>First Term</strong> Solutions</li>
                <li><strong>Second Term</strong> Solutions</li>
                <br /><br /><br />
              </ul>
    
              <hr />
      
              <h1><sup>₹</sup>Free</h1>
              <a href="free-sol.aspx">Start Trial</a>
            </div>
    
            <div class="pricingtable">
              <div class="top">
                <h2 style="color:Orange">Topper's</h2>
              </div>
              <ul>
                <li><strong>First Term</strong> Solutions</li>
                <li><strong>Second Term</strong> Solutions</li>
                <li><strong>End Term</strong> Solutions</li>
                <br /><br />
              </ul>
              <hr />
              <h1><sup>₹</sup>20</h1>
              <p>per Subject</p>
              <a href="#">Select Subjects</a>
            </div>
        </div>
    </div>
</asp:Content>