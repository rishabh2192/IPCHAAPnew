using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

public partial class Forum_forumnewtopic : System.Web.UI.Page
{
    string grp_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            grp_id = Request.QueryString["grp_id"].ToString();

            if (string.IsNullOrWhiteSpace(grp_id))
            {
                Response.Redirect("~/forum.aspx");
            }
            else
            {
                if (valid_group())
                {
                    new_topic.InnerText = "Posting a New Topic in " + generate_forum_name(grp_id) + " Discussion";
                    try
                    {
                        getcurrentuser();
                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/Forum.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/forum.aspx");
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/forum.aspx");
        }
    }

    protected bool valid_group()
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT * FROM forum_group where group_id=@p1";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = Request.QueryString["grp_id"].ToString();
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();
        bool result = Reader.HasRows;
        command.Connection.Close();

        return result;
    }

    protected string generate_forum_name(string group_id)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT group_name FROM forum_group WHERE group_id=@p1";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = group_id;
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();
        Reader.Read();
        string name = Reader[0].ToString();
        command.Connection.Close();

        return name;
    }

    protected void post_new_topic()
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "INSERT INTO forum_topics values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = inc_topic_id();
        command.Parameters.Add(param1);

        SqlParameter param2 = new SqlParameter();
        param2.SqlDbType = System.Data.SqlDbType.Int;
        param2.ParameterName = "@p2";
        param2.Value = grp_id;
        command.Parameters.Add(param2);

        SqlParameter param3 = new SqlParameter();
        param3.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
        param3.ParameterName = "@p3";
        param3.Value = getcurrentuser();
        command.Parameters.Add(param3);

        SqlParameter param4 = new SqlParameter();
        param4.SqlDbType = System.Data.SqlDbType.Int;
        param4.ParameterName = "@p4";
        param4.Value = 0;
        command.Parameters.Add(param4);

        SqlParameter param5 = new SqlParameter();
        param5.SqlDbType = System.Data.SqlDbType.DateTime2;
        param5.ParameterName = "@p5";
        param5.Value = System.DateTime.Now;
        command.Parameters.Add(param5);

        SqlParameter param6 = new SqlParameter();
        param6.SqlDbType = System.Data.SqlDbType.VarChar;
        param6.ParameterName = "@p6";
        param6.Value = TextBox1.Text;
        command.Parameters.Add(param6);

        SqlParameter param7 = new SqlParameter();
        param7.SqlDbType = System.Data.SqlDbType.VarChar;
        param7.ParameterName = "@p7";
        param7.Value = TextBox2.Text;
        command.Parameters.Add(param7);

        SqlParameter param8 = new SqlParameter();
        param8.SqlDbType = System.Data.SqlDbType.Char;
        param8.ParameterName = "@p8";
        param8.Value = 0;
        command.Parameters.Add(param8);

        SqlParameter param9 = new SqlParameter();
        param9.SqlDbType = System.Data.SqlDbType.Int;
        param9.ParameterName = "@p9";
        param9.Value = 0;
        command.Parameters.Add(param9);

        command.Connection.Open();
        command.ExecuteNonQuery();
        command.Connection.Close();
    }

    protected Guid getcurrentuser()
    {
        // Get a reference to the currently logged on user
        MembershipUser currentUser = Membership.GetUser();

        // Determine the currently logged on user's UserId value
        Guid currentUserId = (Guid)currentUser.ProviderUserKey;

        return currentUserId;
    }

    protected int inc_topic_id()
    {
        //making connection string for connecting with database
        string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Query For Getting Order Number Of Last topic !
        string sqlqry_getprev_orderno = "SELECT TOP 1 topic_id FROM forum_topics ORDER BY topic_id DESC";

        SqlConnection connection = new SqlConnection(conn_string);

        SqlCommand command_getprev_orderno = new SqlCommand(sqlqry_getprev_orderno, connection);

        SqlDataReader Reader = null;

        command_getprev_orderno.Connection.Open();
        Reader = command_getprev_orderno.ExecuteReader();
        Reader.Read();

        int neworder_no = int.Parse(Reader[0].ToString()) + 1;

        command_getprev_orderno.Connection.Close();

        return neworder_no;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        post_new_topic();
    }
}