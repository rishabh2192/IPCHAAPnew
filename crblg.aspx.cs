using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class crblg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string time = System.DateTime.Now.TimeOfDay.ToString();
        string filename = TextBox2.Text+time.Split(':')[0]+time.Split(':')[1]+".html";

        string filePath = "~/develop/Blog/" + filename ; 

        string contentOfPage = "<html><head><link href='../Stylesheets/Blog/blogarticle.css' rel='Stylesheet' type='text/css'/><title>"+TextBox2.Text+"</title></head><body><div class='container_header'><img src='../Images/logowork.png' /><span class='blogs'><span style='font-size:42px;'>|</span>blogs</span><a href='#'><span class='back'><< Back to more articles</span></a></div><br /><br /><div class='container_article'><div class='heading'><span>" + TextBox2.Text +"</span><br /><span class='author'>by Rishabh</span><hr /></div><div class='content'><span>"+TextBox1.Text +"</span></div></div><div class='container_footer'>2013 IP Chaap - Crafted By Rishabh Malhotra & Jatin Arora. All Rights Reserved.</div></body></html>";

        using (StreamWriter writer = new StreamWriter(Server.MapPath(filePath),true))
        {   
            writer.WriteLine(contentOfPage);
        }
    }
}