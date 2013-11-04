using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Forum_forumtopics : System.Web.UI.Page
{
    string grp_id, p_no;
    int pages;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            grp_id = Request.QueryString["grp_id"].ToString();
            p_no = Request.QueryString["pg"].ToString();

            if (string.IsNullOrWhiteSpace(grp_id))
            {
                Response.Redirect("../Forum/Default.aspx");
            }
            else
            {
                if (valid_group())
                {
                    generate_topics_page(grp_id);

                    try
                    {
                        getcurrentuser();
                        Button1.Visible = true;
                    }
                    catch (Exception)
                    {
                        Button1.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("../Forum/Default.aspx");
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Forum/Default.aspx");
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

    protected void generate_topics_page(string group_id)
    {
        generate_forum_name(group_id);

        generate_topic_table(group_id);

        generate_bread_crumbs();

        create_page_numbers(pages);
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
        string forumname = Reader[0].ToString();
        command.Connection.Close();

        forum_name.InnerText = forumname;

        return forumname;
    }

    protected void generate_topic_table(string group_id)
    {
        int start, stop;

        start = (10 * ((int.Parse(p_no)) - 1)) + 1;
        stop = 10 * (int.Parse(p_no));


        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT topic_id,starter_user_id,no_of_likes,date_created,views,topic_name,topic_description FROM (SELECT group_id,topic_id,starter_user_id,no_of_likes,date_created,views,topic_name,topic_description,ROW_NUMBER() OVER (ORDER BY topic_id ASC) AS Row FROM forum_topics) tmp WHERE tmp.group_id=@p1 AND Row >=" + start.ToString() + " AND Row <= " + stop.ToString();

        pages = no_of_page("SELECT group_id,topic_id,starter_user_id,no_of_likes,date_created,views,topic_name,topic_description FROM forum_topics");

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = group_id;
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();

        Table topic_table = new Table();
        topic_table.CssClass = "table_forum";

        TableHeaderRow h_row = new TableHeaderRow();
        h_row.CssClass = "header_row";

        TableHeaderCell h_cell_topic_name = new TableHeaderCell();


        TableHeaderCell h_cell_topic_stats = new TableHeaderCell();
        Label lbl_population = new Label();
        lbl_population.Text = "Topic Stats";

        h_cell_topic_stats.Controls.Add(lbl_population);

        TableHeaderCell h_cell_topic_last_activity = new TableHeaderCell();
        Label lbl_last_act = new Label();
        lbl_last_act.Text = "Latest Activity";

        h_cell_topic_last_activity.Controls.Add(lbl_last_act);

        h_row.Cells.Add(h_cell_topic_name);
        h_row.Cells.Add(h_cell_topic_stats);
        h_row.Cells.Add(h_cell_topic_last_activity);

        topic_table.Rows.Add(h_row);
        int i = 0;
        while (Reader.Read())
        {
            ++i;
            TableRow row_topic = new TableRow();
            row_topic.CssClass = "normal_table_forum_row";

            if (i % 2 == 0)
            {
                row_topic.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#f5f5f5");
            }
            else
            {
                row_topic.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#f9f9f9");
            }
            TableCell cell_title_topic = new TableCell();
            cell_title_topic.Style.Add(HtmlTextWriterStyle.Width, "70%");
            cell_title_topic.Style.Add(HtmlTextWriterStyle.TextAlign, "Left");
            cell_title_topic.Style.Add(HtmlTextWriterStyle.Padding, "10px 10px 10px 10px");

            TableCell cell_No_of_comments_topic = new TableCell();
            cell_No_of_comments_topic.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            cell_No_of_comments_topic.Style.Add(HtmlTextWriterStyle.Padding, "10px 4px 10px 4px");

            TableCell cell_Last_comment_starter_group = new TableCell();
            cell_Last_comment_starter_group.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            cell_Last_comment_starter_group.Style.Add(HtmlTextWriterStyle.Padding, "10px 4px 10px 4px");





            HyperLink topic_title = new HyperLink();
            topic_title.CssClass = "heading_forum";
            topic_title.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Reader[5].ToString().ToLower());
            topic_title.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" + Reader[0].ToString() + "&pg=1";

            Literal nl1 = new Literal();
            nl1.Text = "<br />";

            Label topic_starter = new Label();
            topic_starter.CssClass = "descr_forum";
            topic_starter.Text = "Started by " + get_starting_user(Reader[1].ToString());

            cell_title_topic.Controls.Add(topic_title);
            cell_title_topic.Controls.Add(nl1);
            cell_title_topic.Controls.Add(topic_starter);






            Label total_comments = new Label();
            total_comments.CssClass = "descr_forum";
            total_comments.Text = calculate_comments(Reader[0].ToString()) + " Comments";

            Literal nl5 = new Literal();
            nl5.Text = "<br />";

            Label total_views = new Label();
            total_views.CssClass = "number";
            total_views.Text = (Reader[4].ToString()) + " Views";


            cell_No_of_comments_topic.Controls.Add(total_comments);
            cell_No_of_comments_topic.Controls.Add(nl5);
            cell_No_of_comments_topic.Controls.Add(total_views);






            Label last_act_user = new Label();
            last_act_user.CssClass = "descr_forum";


            Literal nl4 = new Literal();
            nl4.Text = "<br />";
            Literal nl3 = new Literal();
            nl3.Text = "<br />";

            Label last_act_datetime = new Label();
            last_act_datetime.CssClass = "descr_forum";
            if (get_last_user(Reader[0].ToString()) != "/")
            {
                last_act_user.Text = "by " + get_last_user(Reader[0].ToString());
                last_act_datetime.Text = get_last_user_time(Reader[0].ToString());
            }
            else
            {
                last_act_user.Text = "by " + get_starting_user(Reader[1].ToString());
                last_act_datetime.Text = (((DateTime)(Reader[3])).ToShortDateString());
            }





            cell_Last_comment_starter_group.Controls.Add(last_act_user);
            cell_Last_comment_starter_group.Controls.Add(nl3);
            cell_Last_comment_starter_group.Controls.Add(last_act_datetime);

            row_topic.Cells.Add(cell_title_topic);
            row_topic.Cells.Add(cell_No_of_comments_topic);
            row_topic.Cells.Add(cell_Last_comment_starter_group);

            topic_table.Rows.Add(row_topic);
        }

        table_topics.Controls.Add(topic_table);
        command.Connection.Close();
    }

    protected void create_page_numbers(int no_of_page_reqs)
    {
        Panel panel = new Panel();

        //LAYOUT SHOWING PAGE NUMBERS AT BOTTOM
        Table table_top_pagenumbers = new Table();
        table_top_pagenumbers.Style.Add(HtmlTextWriterStyle.Width, "20%");

        //table.Style.Add(HtmlTextWriterStyle.MarginRight, "10%");
        TableRow row_top_pagenumbers = new TableRow();
        row_top_pagenumbers.Style.Add(HtmlTextWriterStyle.Width, "10%");

        for (int i = 0; i <= no_of_page_reqs + 1; i++)
        {
            TableCell cell = new TableCell();
            HyperLink page_number = new HyperLink();

            if (int.Parse(Request.QueryString["pg"].ToString()) == i)
            {
                page_number.BackColor = System.Drawing.Color.Black;
                page_number.ForeColor = System.Drawing.Color.LemonChiffon;
            }
            else
            {
                page_number.BackColor = System.Drawing.Color.Transparent;
            }

            //NEXT POINTER
            if (i == no_of_page_reqs + 1)
            {
                string page_sel = Request.QueryString["pg"].ToString();
                string next_ptr = (int.Parse(page_sel) + 1).ToString();


                if (int.Parse(next_ptr) != no_of_page_reqs + 1)
                {
                    page_number.Text = "Next";
                    page_number.ID = i.ToString() + "Top";
                    page_number.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + grp_id + "&pg=" + next_ptr;
                }
            }

            //PREV POINTER
            else if (i == 0)
            {
                string page_sel = Request.QueryString["pg"].ToString();
                string prev_ptr = (int.Parse(page_sel) - 1).ToString();



                if (int.Parse(prev_ptr) != 0)
                {
                    page_number.Text = "Previous";
                    page_number.ID = i.ToString() + "Top";
                    page_number.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + grp_id + "&pg=" + prev_ptr;
                }
            }

            else
            {
                page_number.Text = i.ToString();
                page_number.ID = i.ToString() + "Top";


                page_number.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + grp_id + "&pg=" + i.ToString();
            }

            page_number.BorderColor = System.Drawing.Color.Transparent;
            page_number.BorderStyle = BorderStyle.Ridge;
            page_number.BorderWidth = 1;
            page_number.Style.Add(HtmlTextWriterStyle.FontSize, "22px");
            page_number.Style.Add(HtmlTextWriterStyle.PaddingLeft, "4px");
            page_number.Style.Add(HtmlTextWriterStyle.PaddingRight, "4px");

            cell.Controls.Add(page_number);
            cell.Style.Add(HtmlTextWriterStyle.Width, "10px");
            row_top_pagenumbers.Cells.Add(cell);
        }
        table_top_pagenumbers.Rows.Add(row_top_pagenumbers);


        //LAYOUT SHOWING PAGE NUMBERS AT BOTTOM
        Table table_Bottom_pagenumbers = new Table();
        table_Bottom_pagenumbers.Style.Add(HtmlTextWriterStyle.Width, "20%");

        //table.Style.Add(HtmlTextWriterStyle.MarginRight, "10%");
        TableRow row_Bottom_pagenumbers = new TableRow();
        row_Bottom_pagenumbers.Style.Add(HtmlTextWriterStyle.Width, "10%");

        for (int i = 0; i <= no_of_page_reqs + 1; i++)
        {
            TableCell cell = new TableCell();
            HyperLink page_number = new HyperLink();

            if (int.Parse(Request.QueryString["pg"].ToString()) == i)
            {
                page_number.BackColor = System.Drawing.Color.Black;
                page_number.ForeColor = System.Drawing.Color.LemonChiffon;
            }
            else
            {
                page_number.BackColor = System.Drawing.Color.Transparent;
            }

            //NEXT POINTER
            if (i == no_of_page_reqs + 1)
            {
                string page_sel = Request.QueryString["pg"].ToString();
                string next_ptr = (int.Parse(page_sel) + 1).ToString();

                if (int.Parse(next_ptr) != no_of_page_reqs + 1)
                {
                    page_number.Text = "Next";
                    page_number.ID = i.ToString() + "Bottom";
                    page_number.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + grp_id + "&pg=" + next_ptr;
                }
            }

            //PREV POINTER
            else if (i == 0)
            {
                string page_sel = Request.QueryString["pg"].ToString();
                string prev_ptr = (int.Parse(page_sel) - 1).ToString();


                if (int.Parse(prev_ptr) != 0)
                {
                    page_number.Text = "Previous";
                    page_number.ID = i.ToString() + "Bottom";
                    page_number.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + grp_id + "&pg=" + prev_ptr;
                }
            }

            else
            {
                page_number.Text = i.ToString();
                page_number.ID = i.ToString() + "Bottom";

                page_number.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + grp_id + "&pg=" + i.ToString();
            }

            page_number.BorderColor = System.Drawing.Color.Transparent;
            page_number.BorderStyle = BorderStyle.Ridge;
            page_number.BorderWidth = 1;
            page_number.Style.Add(HtmlTextWriterStyle.FontSize, "22px");
            page_number.Style.Add(HtmlTextWriterStyle.PaddingLeft, "4px");
            page_number.Style.Add(HtmlTextWriterStyle.PaddingRight, "4px");

            cell.Controls.Add(page_number);
            cell.Style.Add(HtmlTextWriterStyle.Width, "10px");
            row_Bottom_pagenumbers.Cells.Add(cell);
        }
        table_Bottom_pagenumbers.Rows.Add(row_Bottom_pagenumbers);

        //ADDING BOTH TOP & BOTTOM LAYOUTS TO SPECIFIED div's
        dv2.Controls.Add(table_top_pagenumbers);
        dv2.Style.Add(HtmlTextWriterStyle.Width, "100%");
        dv3.Controls.Add(table_Bottom_pagenumbers);
        dv3.Style.Add(HtmlTextWriterStyle.Width, "100%");
        table_top_pagenumbers.Style.Add(HtmlTextWriterStyle.Width, "10%");
        table_Bottom_pagenumbers.Style.Add(HtmlTextWriterStyle.Width, "10%");
    }

    protected int no_of_page(string sqlqry)
    {
        float d_no_of_page_req = -1;

        string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn_string);

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlDataReader Reader = null;

        command.Connection.Open();

        Reader = command.ExecuteReader();

        while (Reader.Read())
        {
            ++d_no_of_page_req;
        }
        int temp;

        temp = (int)(d_no_of_page_req / 10);

        int no_of_page_req = temp + 1;
        command.Connection.Close();

        if (no_of_page_req == 1)
        {
            dv2.Visible = false;
            dv3.Visible = false;
        }

        return no_of_page_req;
    }

    protected string get_starting_user(string userid)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT Firstname FROM userprofile where userid=@p1";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        Guid user = new Guid(userid);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
        param1.ParameterName = "@p1";
        param1.Value = user;
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();
        Reader.Read();
        string starter = Reader[0].ToString();
        command.Connection.Close();

        return starter;
    }

    protected string calculate_comments(string topic_id)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT count(*) FROM forum_comments WHERE topic_id=@p1";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = topic_id;
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();
        Reader.Read();
        string no_of_comments = Reader[0].ToString();
        command.Connection.Close();

        return no_of_comments;
    }

    protected string get_last_user(string topic_id)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT Firstname FROM userprofile WHERE userid=(SELECT TOP 1 commenter_user_id from forum_comments where topic_id=@p1 order by date_commented DESC)";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = topic_id;
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        string User;

        command.Connection.Open();
        Reader = command.ExecuteReader();
        if (Reader.HasRows)
        {
            Reader.Read();
            User = Reader[0].ToString();
        }
        else
        {
            User = "/";
        }

        command.Connection.Close();

        return User;
    }

    protected string get_last_user_time(string topic_id)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT TOP 1 date_commented from forum_comments where topic_id=@p1 order by date_commented DESC";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = topic_id;
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();
        Reader.Read();
        string date = (((DateTime)(Reader[0])).ToShortDateString()).ToString();
        command.Connection.Close();

        return date;
    }

    //not used any where in this page
    protected string checkid()
    {
        //FUNCTION FOR DETERMINING ID OF BOOK WHOSE "ADD TO CART" BUTTON WAS CLICKED ! (Actually we Are Determining the id of the button which is book id !) 

        string controlID = Page.Request.Params["__EVENTTARGET"];
        Control postbackControl = null;
        if (controlID != null && controlID != String.Empty)
        {
            postbackControl = Page.FindControl(controlID);
        }
        else
        {
            foreach (string ctrl in Page.Request.Form)
            {    //Check if Image Button
                if (ctrl.EndsWith(".x") || ctrl.EndsWith(".y"))
                {
                    postbackControl = Page.FindControl(ctrl.Substring(0, ctrl.Length - 2));
                    break;
                }
                else
                {
                    postbackControl = Page.FindControl(ctrl);
                    //Check if Button control     
                    if (postbackControl is Button)
                    {
                        break;
                    }
                }

            }
        }
        string str = postbackControl.ID;
        return str;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Forum/forumnewtopic.aspx?grp_id=" + grp_id);
    }

    protected Guid getcurrentuser()
    {
        // Get a reference to the currently logged on user
        MembershipUser currentUser = Membership.GetUser();

        // Determine the currently logged on user's UserId value
        Guid currentUserId = (Guid)currentUser.ProviderUserKey;

        return currentUserId;
    }

    protected void generate_bread_crumbs()
    {
        HyperLink link_home = new HyperLink();
        link_home.NavigateUrl = "../Forum/Default.aspx";
        link_home.Text = "Home";
        link_home.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");
        link_home.CssClass = "descr_forum";

        Label lbl1 = new Label();
        lbl1.CssClass = "descr_forum";
        lbl1.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");
        lbl1.Text = " >> ";

        HyperLink link_group = new HyperLink();
        link_group.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + grp_id+"&pg=1";
        link_group.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(generate_forum_name(grp_id).ToLower());
        link_group.CssClass = "descr_forum";
        link_group.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");

        where_are_u.Controls.Add(link_home);
        where_are_u.Controls.Add(lbl1);
        where_are_u.Controls.Add(link_group);
    }
}