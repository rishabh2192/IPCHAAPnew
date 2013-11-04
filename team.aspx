<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="team.aspx.cs" Inherits="team" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Stylesheets/team.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="title_container">
            <span>Our Team Members</span>
        </div>
        <br />
        <div class="subtitle_container">
            <span>Founders</span>
        </div>
        <div>
            <ul class="ch-grid">
                <li>
                    <div class="ch-item ch-img-1">
                        <div class="ch-info">
                            <h3>Mr. Anurag Pandey</h3>
                            
                        </div>
                    </div>
                </li>
                <li>
                    <div class="ch-item ch-img-2">
                        <div class="ch-info">
                            <h3>Mr. Rishabh Malhotra</h3>
                            
                        </div>
                    </div>
                </li>
                <li>
                    <div class="ch-item ch-img-3">
                        <div class="ch-info">
                            <h3>Mr. Jatin Arora</h3>
                            
                        </div>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>

