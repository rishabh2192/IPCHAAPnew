using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Forum_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        generate_group_table();
    }

    protected void generate_group_table()
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT group_name,no_of_topics,group_descr,group_id FROM forum_group";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();

        Table group_table = new Table();
        group_table.CssClass = "table_forum";

        TableHeaderRow h_row = new TableHeaderRow();
        h_row.CssClass = "header_row";

        TableHeaderCell h_cell_forum_name = new TableHeaderCell();
        Label lbl_name = new Label();
        lbl_name.Text = "";

        h_cell_forum_name.Controls.Add(lbl_name);

        TableHeaderCell h_cell_forum_population = new TableHeaderCell();
        Label lbl_population = new Label();
        lbl_population.Text = "Topics";

        h_cell_forum_population.Controls.Add(lbl_population);

        TableHeaderCell h_cell_forum_last_activity = new TableHeaderCell();
        Label lbl_last_act = new Label();
        lbl_last_act.Text = "Latest Activity";

        h_cell_forum_last_activity.Controls.Add(lbl_last_act);

        h_row.Cells.Add(h_cell_forum_name);
        h_row.Cells.Add(h_cell_forum_population);
        h_row.Cells.Add(h_cell_forum_last_activity);

        group_table.Rows.Add(h_row);
        int i = 0;
        while (Reader.Read())
        {
            ++i;
            TableRow row_group = new TableRow();
            row_group.CssClass = "normal_table_forum_row";

            if (i % 2 == 0)
            {
                row_group.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#f5f5f5");
            }
            else
            {
                row_group.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#f9f9f9");
            }
            TableCell cell_title_group = new TableCell();
            cell_title_group.Style.Add(HtmlTextWriterStyle.Width, "70%");
            cell_title_group.Style.Add(HtmlTextWriterStyle.TextAlign, "Left");
            cell_title_group.Style.Add(HtmlTextWriterStyle.Padding, "10px 10px 10px 10px");

            TableCell cell_No_of_topics_group = new TableCell();
            cell_No_of_topics_group.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            cell_No_of_topics_group.Style.Add(HtmlTextWriterStyle.Padding, "10px 4px 10px 4px");

            TableCell cell_Last_topic_starter_group = new TableCell();
            cell_Last_topic_starter_group.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            cell_Last_topic_starter_group.Style.Add(HtmlTextWriterStyle.Padding, "10px 4px 10px 4px");

            HyperLink group_title = new HyperLink();
            group_title.CssClass = "heading_forum";
            group_title.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Reader[0].ToString().ToLower());
            group_title.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + Reader[3].ToString() + "&pg=1";
            cell_title_group.Controls.Add(group_title);

            Literal nl1 = new Literal();
            nl1.Text = "<br />";
            cell_title_group.Controls.Add(nl1);

            Label group_descr = new Label();
            group_descr.CssClass = "descr_forum";
            group_descr.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Reader[2].ToString().ToLower());
            cell_title_group.Controls.Add(group_descr);

            cell_No_of_topics_group.CssClass = "number";
            cell_No_of_topics_group.Text = get_no_of_topics(Reader[3].ToString());


            HyperLink last_act_topic = new HyperLink();
            last_act_topic.CssClass = "descr_forum";
            last_act_topic.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" + get_last_activity_topic_id(Reader[3].ToString());
            last_act_topic.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            if (!string.IsNullOrEmpty(get_last_activity_topic_name(Reader[3].ToString())))
            {
                last_act_topic.Text = get_last_activity_topic_name(Reader[3].ToString()) + "....";
            }

            Label last_act_user = new Label();
            last_act_user.CssClass = "descr_forum";
            if (!string.IsNullOrEmpty(get_last_activity_user_name(Reader[3].ToString())))
            {
                last_act_user.Text = "by " + get_last_activity_user_name(Reader[3].ToString());
            }
            Literal nl2 = new Literal();
            nl2.Text = "<br />";
            Literal nl3 = new Literal();
            nl3.Text = "<br />";

            Label last_act_datetime = new Label();
            last_act_datetime.CssClass = "descr_forum";
            last_act_datetime.Text = get_last_activity_date(Reader[3].ToString());


            cell_Last_topic_starter_group.Controls.Add(last_act_topic);
            cell_Last_topic_starter_group.Controls.Add(nl2);
            cell_Last_topic_starter_group.Controls.Add(last_act_user);
            cell_Last_topic_starter_group.Controls.Add(nl3);
            cell_Last_topic_starter_group.Controls.Add(last_act_datetime);

            row_group.Cells.Add(cell_title_group);
            row_group.Cells.Add(cell_No_of_topics_group);
            row_group.Cells.Add(cell_Last_topic_starter_group);

            group_table.Rows.Add(row_group);
        }

        table_group.Controls.Add(group_table);
    }

    protected Guid getcurrentuser()
    {
        // Get a reference to the currently logged on user
        MembershipUser currentUser = Membership.GetUser();

        // Determine the currently logged on user's UserId value
        Guid currentUserId = (Guid)currentUser.ProviderUserKey;

        return currentUserId;
    }

    protected string get_last_activity_topic_name(string group_id)
    {
        string name = null;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "Select topic_name from forum_topics where topic_id=(select TOP 1 topic_id from forum_comments WHERE topic_id IN (select topic_id from forum_topics where group_id=@p1) order by date_commented DESC)";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlDataReader Reader = null;

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = group_id;
        command.Parameters.Add(param1);

        command.Connection.Open();
        Reader = command.ExecuteReader();
        if (Reader.HasRows)
        {
            Reader.Read();
            name = (Reader[0].ToString()).Substring(0, ((Reader[0].ToString().Length) / 2)-1);
        }
        command.Connection.Close();

        return name;
    }

    protected string get_last_activity_user_name(string group_id)
    {
        string name = null;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "select firstname from userprofile where userid =(select TOP 1 forum_comments.commenter_user_id from forum_comments WHERE topic_id IN (select topic_id from forum_topics where group_id=@p1) order by date_commented DESC)";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlDataReader Reader = null;

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = group_id;
        command.Parameters.Add(param1);

        command.Connection.Open();
        Reader = command.ExecuteReader();
        if (Reader.HasRows)
        {
            Reader.Read();
            name = Reader[0].ToString();
        }
        command.Connection.Close();

        return name;
    }

    protected string get_last_activity_date(string group_id)
    {
        string date = null;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "select TOP 1 date_commented from forum_comments WHERE topic_id IN (select topic_id from forum_topics where group_id=@p1) order by date_commented DESC";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlDataReader Reader = null;

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = group_id;
        command.Parameters.Add(param1);

        command.Connection.Open();
        Reader = command.ExecuteReader();
        if (Reader.HasRows)
        {
            Reader.Read();
            date = ((DateTime)Reader[0]).ToShortDateString();
        }
        command.Connection.Close();

        return date;
    }

    protected string get_last_activity_topic_id(string group_id)
    {
        string id = null;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "Select topic_id from forum_topics where topic_id=(select TOP 1 topic_id from forum_comments WHERE topic_id IN (select topic_id from forum_topics where group_id=@p1) order by date_commented DESC)";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlDataReader Reader = null;

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = group_id;
        command.Parameters.Add(param1);

        command.Connection.Open();
        Reader = command.ExecuteReader();
        if (Reader.HasRows)
        {
            Reader.Read();
            id = Reader[0].ToString();
        }
        command.Connection.Close();

        return id;
    }

    protected string get_no_of_topics(string group_id)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "select count(*) from forum_topics where group_id=@p1";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlDataReader Reader = null;

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = group_id;
        command.Parameters.Add(param1);

        command.Connection.Open();
        Reader = command.ExecuteReader();
        Reader.Read();
        string no_of_topics = Reader[0].ToString();
        command.Connection.Close();

        return no_of_topics;
    }
}