<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Stylesheets/Login.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>  
    <table class="main_table" >
        <tr>
           <td valign="middle" style="width:40% ; border-right-color:#e8e8e8; border-right-style:solid; border-right-width:0px;">
                <div id="div1" align="center" runat="server" class="Login_Panel">
                    <asp:Panel ID="Panel1" DefaultButton="Login2$LoginButton" runat="server">
                        <asp:Login ID="Login2"  ForeColor="Gray" TextBoxStyle-CssClass="lgnpage_lgn_textbox" LabelStyle-CssClass="lgnpage_lgn_label"
                                                LoginButtonStyle-CssClass="login_button" UserNameLabelText="User ID "  TitleText= "LOG IN" 
                                                TitleTextStyle-CssClass="lgnpage_lgn_title" PasswordLabelText="Password " RememberMeSet="true" RememberMeText="Remember Me!"
                                                PasswordRecoveryText="I Forgot my Password !" PasswordRecoveryUrl="~/Password_Recovery.aspx"
                                                FailureTextStyle-Font-Size="14px" FailureText="Your login attempt was not successful."
                                                HyperLinkStyle-CssClass="lgnpage_lgn_hyper" runat="server">
                        </asp:Login>
                    </asp:Panel>
                </div>
            </td>
            <td valign="middle" style="width:20% ;text-align:center;">
                <%--<span class="OR">
                    OR
                </span>--%>
                <img src="Images/book-plant-pots.jpg" width="400px" height="250px" />
            </td>

            <td valign="middle" style=" width:40% ;text-align:center;" >
                
                <div id="div2" align="center"  runat="server" class="Register_Panel">
                <span class="lgnpage_lgn_title">REGISTER</span>
                    <asp:Panel ID="Panel2" DefaultButton="CreateUserWizard1$__CustomNav0$StepNextButtonButton" runat="server">
                    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
                                          CreateUserButtonText="Submit" ContinueButtonStyle-CssClass="login_button" 
                                          
                                          TextBoxStyle-CssClass="lgnpage_lgn_textbox" CreateUserButtonStyle-CssClass="login_button" 
                                          CancelButtonStyle-CssClass="button-change" LabelStyle-CssClass="lgnpage_lgn_label"
                                          UserNameLabelText="User ID" PasswordLabelText="Password" QuestionLabelText="Security Question"
                                          ContinueButtonText="Edit Profile" EmailLabelText="Email" ConfirmPasswordLabelText="Confirm Password" 
                                          AnswerLabelText="Security Answer" MailDefinition-BodyFileName="~/Registration_mail.txt" 
                                          ConfirmPasswordCompareErrorMessage="Passwords Does not match" MailDefinition-Subject="YOUR IPCHAAP ACCOUNT IS CREATED !" 
                                          InvalidPasswordErrorMessage="Password must contain atleast 1 special character." 
                                          ErrorMessageStyle-CssClass="lgnpage_lgn_instr" InstructionTextStyle-CssClass="lgnpage_lgn_instr">
                        <WizardSteps>
                            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" Title="">
                            </asp:CreateUserWizardStep>
                            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                            </asp:CompleteWizardStep>
                        </WizardSteps>
                     </asp:CreateUserWizard>
                    </asp:Panel>
                </div>
            </td>

            </tr>
        </table>
    </div>
</asp:Content>

