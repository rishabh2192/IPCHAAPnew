<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="shipping_return.aspx.cs" Inherits="shipping_return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Stylesheets/shipping_return.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div style="text-align:center;">
            <span class="Heading" >Return and Shipping Policy</span><br /><br />
        </div>

        <span class="heading_policy_submain">1. Shipping</span>
        <br /><br />
        <div class="heading_policy_content">
            A shipping charge is applicable on all items unless specified otherwise. Most orders are shipped on next working day. We use reputed courier companies for Doorstep Deliveries. Most order shipped will arrive in 3 to 5 working days.
            <br /><br /><br />
        </div>

        <span class="heading_policy_submain">2. Packaging</span>
        <br /><br />
        <div class="heading_policy_content">
            We use the suitable appropriate material for packing. Depending upon the quantity of your order the sophistication level of packaging is decided.
            <br /><br /><br />
        </div>

        <span class="heading_policy_submain">3. Returns and Guarantee</span>
        <br /><br />
        <div class="heading_policy_content">
            We are committed for the satisfaction of our customers. We grant a 72 Hours guarantee Period for our Books quality (as mentioned on the books specification page in case of old Books) from the date of delivery to the customer.
            In case of a defect developing within the period of warranty, we will undertake to exchange the Book free of charge, provided the Book is returned to us through Reverse Logistics (It will be arranged by us). The Charge of the Logistical service has to beared by Customer. <br /><br />
            To Return a defected Book the customer needs to contact us through <a href="Contact.aspx">Contact Us Form</a> with his/her order number provided to him/her at the time of placing the order, and <span style=" font-weight:bold;">WITHOUT THE ORDER NUMBER NO RETURN QUERY WILL BE ENTERTAINED. </span>
            <br /><br /><br />
        </div>
        
        <span class="heading_policy_submain">4. Conditions For Initiating a Return</span>
        <br /><br />
        <div class="heading_policy_content">
            Damages due to misuse of product are not covered under the policy. Books with tampered Pages are likewise ineligible. Please return the Book in the actual condition you received it with all its original packaging and Billing Recipt.
            <br /><br /><br />
        </div>

        <span class="heading_policy_submain">5. Cancellation of an Order</span>
        <br /><br />
        <div class="heading_policy_content">
            You can cancel your order online before the product has been shipped. Your entire order amount will be refunded.
            Unfortunately, an order cannot be cancelled after the order is shipped.
            <br />
            To Cancel an order before it is shipped 
            <br /><br /><br />
        </div>


    </div>
</asp:Content>

