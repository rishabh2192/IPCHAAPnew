using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Forum_forumtopicres : System.Web.UI.Page
{
    string tpc_id = "";
    string p_no = "";
    int pages;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tpc_id = Request.QueryString["tpc_id"].ToString();
            p_no = Request.QueryString["pg"].ToString();

            if (string.IsNullOrWhiteSpace(tpc_id))
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                if (valid_topic())
                {
                    if (!Page.IsPostBack)
                    {
                        inc_view_of_topic(tpc_id);
                    }
                    generate_topics_page(tpc_id);

                    try
                    {
                        getcurrentuser();
                        strt_new_topic.Visible = true;
                        Cmnt_cur_topic.Visible = true;
                    }
                    catch (Exception)
                    {
                        strt_new_topic.Visible = false;
                        Cmnt_cur_topic.Visible = false;

                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Default.aspx");
        }


        try
        {
            string comment = Request.QueryString["comment"].ToString();
            if (comment == "true" && p_no == pages.ToString())
            {
                tbl_post_comment_textbox.Visible = true;
                comment_writing_user_name.Text = get_user(getcurrentuser().ToString());
                comment_writing_user_pic.Src = "~/images/user_profile_pics/" + get_user_pic(getcurrentuser().ToString());
            }
        }
        catch (Exception)
        { }
    }

    protected bool valid_topic()
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT * FROM forum_topics where topic_id=@p1";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = Request.QueryString["tpc_id"].ToString();
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();
        bool result = Reader.HasRows;
        command.Connection.Close();

        return result;
    }

    protected Guid getcurrentuser()
    {
        // Get a reference to the currently logged on user
        MembershipUser currentUser = Membership.GetUser();

        // Determine the currently logged on user's UserId value
        Guid currentUserId = (Guid)currentUser.ProviderUserKey;

        return currentUserId;
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
                page_number.Style.Add(HtmlTextWriterStyle.Color, "Black");
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
                    page_number.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" + tpc_id + "&pg=" + next_ptr;
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
                    page_number.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" + tpc_id + "&pg=" + prev_ptr;
                }
            }

            else
            {
                page_number.Text = i.ToString();
                page_number.ID = i.ToString() + "Top";


                page_number.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" + tpc_id + "&pg=" + i.ToString();
            }

            page_number.BorderColor = System.Drawing.Color.Transparent;
            page_number.BorderStyle = BorderStyle.Ridge;
            page_number.BorderWidth = 1;
            page_number.Style.Add(HtmlTextWriterStyle.FontFamily, "Latha");
            page_number.Style.Add(HtmlTextWriterStyle.FontSize, "24px");
            page_number.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");
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
                page_number.Style.Add(HtmlTextWriterStyle.Color, "Black");
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
                    page_number.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" + tpc_id + "&pg=" + next_ptr;
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
                    page_number.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" + tpc_id + "&pg=" + prev_ptr;
                }
            }

            else
            {
                page_number.Text = i.ToString();
                page_number.ID = i.ToString() + "Bottom";

                page_number.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" + tpc_id + "&pg=" + i.ToString();
            }

            page_number.BorderColor = System.Drawing.Color.Transparent;
            page_number.BorderStyle = BorderStyle.Ridge;
            page_number.BorderWidth = 1;
            page_number.Style.Add(HtmlTextWriterStyle.FontFamily, "Latha");
            page_number.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");
            page_number.Style.Add(HtmlTextWriterStyle.FontSize, "24px");
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

    protected void generate_topics_page(string topic_id)
    {
        generate_topic_name(topic_id);

        if (p_no == "1")
        {
            generate_topic_desciption(topic_id);
        }

        generate_comments_table(topic_id);

        generate_bread_crumbs();

        create_page_numbers(pages);
    }

    protected string generate_topic_name(string topic_id)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT topic_name FROM forum_topics WHERE topic_id=@p1";

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
        string topicname = Reader[0].ToString();
        command.Connection.Close();

        forum_name.InnerText = topicname;
        return topicname;
    }

    protected void generate_topic_desciption(string topic_id)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT topic_id,starter_user_id,date_created,topic_description FROM forum_topics WHERE topic_id=@p1";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = topic_id;
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();

        Table comments_table = new Table();
        comments_table.CssClass = "table_forum";

        //TableHeaderRow h_row = new TableHeaderRow();
        //h_row.CssClass = "header_row";

        //TableHeaderCell h_cell_topic_name = new TableHeaderCell();


        //TableHeaderCell h_cell_topic_stats = new TableHeaderCell();
        //Label lbl_population = new Label();
        //lbl_population.Text = "";

        //h_cell_topic_stats.Controls.Add(lbl_population);

        //TableHeaderCell h_cell_topic_last_activity = new TableHeaderCell();
        //Label lbl_last_act = new Label();
        //lbl_last_act.Text = "";

        //h_cell_topic_last_activity.Controls.Add(lbl_last_act);

        //h_row.Cells.Add(h_cell_topic_name);
        //h_row.Cells.Add(h_cell_topic_stats);
        //h_row.Cells.Add(h_cell_topic_last_activity);

        //comments_table.Rows.Add(h_row);
        int i = 0;
        while (Reader.Read())
        {
            ++i;
            TableRow row_comment = new TableRow();
            row_comment.CssClass = "normal_table_forum_row";

            if (i % 2 == 0)
            {
                row_comment.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#f5f5f5");
            }
            else
            {
                row_comment.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#f9f9f9");
            }
            TableCell cell_title_user_pic = new TableCell();
            cell_title_user_pic.Style.Add(HtmlTextWriterStyle.Width, "25%");
            cell_title_user_pic.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Top");
            cell_title_user_pic.Style.Add(HtmlTextWriterStyle.TextAlign, "Left");

            cell_title_user_pic.Style.Add(HtmlTextWriterStyle.Padding, "10px 10px 10px 10px");

            TableCell cell_comments = new TableCell();
            cell_comments.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Top");
            cell_comments.Style.Add(HtmlTextWriterStyle.Padding, "10px 4px 10px 4px");




            Table tbl_commenter = new Table();
            TableRow row_commenter_name = new TableRow();
            TableCell cell_commenter_name = new TableCell();
            TableRow row_commenter_pic = new TableRow();
            TableCell cell_commenter_pic = new TableCell();

            tbl_commenter.Style.Add(HtmlTextWriterStyle.Width, "100%");
            cell_commenter_pic.HorizontalAlign = HorizontalAlign.Center;


            Label commenter_name = new Label();
            commenter_name.Text = get_user(Reader[1].ToString());
            cell_commenter_name.Controls.Add(commenter_name);

            Literal hr1 = new Literal();
            hr1.Text = "<hr style='width:100%;' />";
            cell_commenter_name.Controls.Add(hr1);

            Image img_commenter_pic = new Image();
            img_commenter_pic.ImageUrl = "~/images/user_profile_pics/" + get_user_pic(Reader[1].ToString());
            img_commenter_pic.Width = 100;
            img_commenter_pic.Height = 100;
            img_commenter_pic.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            img_commenter_pic.Style.Add(HtmlTextWriterStyle.BorderColor, "#d1d1d1");
            img_commenter_pic.Style.Add(HtmlTextWriterStyle.BorderWidth, "4px");
            img_commenter_pic.CssClass = "forum_user_image";
            cell_commenter_pic.Controls.Add(img_commenter_pic);


            row_commenter_name.Cells.Add(cell_commenter_name);
            row_commenter_pic.Cells.Add(cell_commenter_pic);

            tbl_commenter.Rows.Add(row_commenter_name);
            tbl_commenter.Rows.Add(row_commenter_pic);

            cell_title_user_pic.Controls.Add(tbl_commenter);





            Table tbl_comment = new Table();
            TableRow row_comment_date = new TableRow();
            TableCell cell_comment_date = new TableCell();
            TableRow row_comment_content = new TableRow();
            TableCell cell_comment_content = new TableCell();

            tbl_comment.Style.Add(HtmlTextWriterStyle.Width, "100%");
            cell_comment_date.HorizontalAlign = HorizontalAlign.Right;

            Label date_of_comment = new Label();
            DateTime date = new DateTime();
            date = (DateTime)Reader[2];
            date_of_comment.CssClass="date";
            date_of_comment.Text = date.ToString(" dd-MMM-yy , hh:mm");
            cell_comment_date.Controls.Add(date_of_comment);

            Literal hr2 = new Literal();
            hr2.Text = "<hr style='width:100%;' />";
            cell_comment_date.Controls.Add(hr2);

            Label lbl_comment = new Label();
            lbl_comment.CssClass = "comment";
            lbl_comment.Text = Reader[3].ToString();
            cell_comment_content.Controls.Add(lbl_comment);

            row_comment_date.Cells.Add(cell_comment_date);
            row_comment_content.Cells.Add(cell_comment_content);

            tbl_comment.Rows.Add(row_comment_date);
            tbl_comment.Rows.Add(row_comment_content);

            cell_comments.Controls.Add(tbl_comment);


            //Label last_act_datetime = new Label();
            //last_act_datetime.CssClass = "descr_forum";
            //if (get_last_user(Reader[0].ToString()) != "/")
            //{
            //    last_act_user.Text = "by " + get_last_user(Reader[0].ToString());
            //    last_act_datetime.Text = get_last_user_time(Reader[0].ToString());
            //}
            //else
            //{
            //    last_act_user.Text = "by " + get_starting_user(Reader[1].ToString());
            //    last_act_datetime.Text = (((DateTime)(Reader[3])).ToShortDateString());
            //}


            //cell_Last_comment_starter_group.Controls.Add(last_act_user);
            //cell_Last_comment_starter_group.Controls.Add(nl3);
            //cell_Last_comment_starter_group.Controls.Add(last_act_datetime);

            row_comment.Cells.Add(cell_title_user_pic);
            row_comment.Cells.Add(cell_comments);
            comments_table.Rows.Add(row_comment);
            comments_table.Style.Add(HtmlTextWriterStyle.Width, "105%");
            comments_table.Style.Add(HtmlTextWriterStyle.Margin,"0px 15px 0px -15px" );
        }

        table_topics.Controls.Add(comments_table);
        command.Connection.Close();
    }

    protected void generate_comments_table(string topic_id)
    {
        int start, stop;

        start = (10 * ((int.Parse(p_no)) - 1)) + 1;
        stop = 10 * (int.Parse(p_no));

        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT comment_id,commenter_user_id,date_commented,comment FROM (SELECT topic_id,comment_id,commenter_user_id,date_commented,comment,ROW_NUMBER() OVER (ORDER BY comment_id ASC) AS Row FROM forum_comments WHERE topic_id=@p1) tmp WHERE Row >=" + start.ToString() + " AND Row <= " + stop.ToString();

        pages = no_of_page("SELECT topic_id,comment_id,commenter_user_id,date_commented,comment FROM forum_comments WHERE topic_id=" + tpc_id);

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = topic_id;
        command.Parameters.Add(param1);

        SqlDataReader Reader = null;

        command.Connection.Open();
        Reader = command.ExecuteReader();

        Table comments_table = new Table();
        comments_table.CssClass = "table_forum";

        //TableHeaderRow h_row = new TableHeaderRow();
        //h_row.CssClass = "header_row";

        //TableHeaderCell h_cell_topic_name = new TableHeaderCell();


        //TableHeaderCell h_cell_topic_stats = new TableHeaderCell();
        //Label lbl_population = new Label();
        //lbl_population.Text = "";

        //h_cell_topic_stats.Controls.Add(lbl_population);

        //TableHeaderCell h_cell_topic_last_activity = new TableHeaderCell();
        //Label lbl_last_act = new Label();
        //lbl_last_act.Text = "";

        //h_cell_topic_last_activity.Controls.Add(lbl_last_act);

        //h_row.Cells.Add(h_cell_topic_name);
        //h_row.Cells.Add(h_cell_topic_stats);
        //h_row.Cells.Add(h_cell_topic_last_activity);

        //comments_table.Rows.Add(h_row);
        int i = 0;
        while (Reader.Read())
        {
            ++i;
            TableRow row_comment = new TableRow();
            row_comment.CssClass = "normal_table_forum_row";

            if (i % 2 == 0)
            {
                row_comment.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#fbfbfb");
            }
            else
            {
                row_comment.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#fcfcfc");
            }
            TableCell cell_title_user_pic = new TableCell();
            cell_title_user_pic.Style.Add(HtmlTextWriterStyle.Width, "25%");
            cell_title_user_pic.Style.Add(HtmlTextWriterStyle.TextAlign, "Left");
            cell_title_user_pic.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Top");
            cell_title_user_pic.Style.Add(HtmlTextWriterStyle.Padding, "10px 10px 10px 10px");

            TableCell cell_comments = new TableCell();
            cell_comments.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Top");
            cell_comments.Style.Add(HtmlTextWriterStyle.Padding, "10px 4px 10px 4px");




            Table tbl_commenter = new Table();
            TableRow row_commenter_name = new TableRow();
            TableCell cell_commenter_name = new TableCell();
            TableRow row_commenter_pic = new TableRow();
            TableCell cell_commenter_pic = new TableCell();

            tbl_commenter.Style.Add(HtmlTextWriterStyle.Width, "100%");
            cell_commenter_pic.HorizontalAlign = HorizontalAlign.Center;


            Label commenter_name = new Label();
            commenter_name.Text = get_user(Reader[1].ToString());
            cell_commenter_name.Controls.Add(commenter_name);

            Literal hr1 = new Literal();
            hr1.Text = "<hr style='width:100%;' />";
            
            cell_commenter_name.Controls.Add(hr1);

            Image img_commenter_pic = new Image();
            img_commenter_pic.ImageUrl = "~/images/user_profile_pics/" + get_user_pic(Reader[1].ToString());
            img_commenter_pic.Width = 100;
            img_commenter_pic.Height = 100;
            img_commenter_pic.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            img_commenter_pic.Style.Add(HtmlTextWriterStyle.BorderColor, "#d1d1d1");
            img_commenter_pic.Style.Add(HtmlTextWriterStyle.BorderWidth, "4px");
            img_commenter_pic.CssClass = "forum_user_image";
            cell_commenter_pic.Controls.Add(img_commenter_pic);


            row_commenter_name.Cells.Add(cell_commenter_name);
            row_commenter_pic.Cells.Add(cell_commenter_pic);

            tbl_commenter.Rows.Add(row_commenter_name);
            tbl_commenter.Rows.Add(row_commenter_pic);

            cell_title_user_pic.Controls.Add(tbl_commenter);





            Table tbl_comment = new Table();
            TableRow row_comment_date = new TableRow();
            TableCell cell_comment_date = new TableCell();
            TableRow row_comment_content = new TableRow();
            TableCell cell_comment_content = new TableCell();

            tbl_comment.Style.Add(HtmlTextWriterStyle.Width, "100%");
            cell_comment_date.HorizontalAlign = HorizontalAlign.Right;


            Label date_of_comment = new Label();
            DateTime date = new DateTime();
            date = (DateTime)Reader[2];
            date_of_comment.CssClass = "date";
            date_of_comment.Text = date.ToString(" dd-MMM-yy , hh:mm");
            cell_comment_date.Controls.Add(date_of_comment);

            Literal hr2 = new Literal();
            hr2.Text = "<hr style='width:100%;' />";
            cell_comment_date.Controls.Add(hr2);

            Label lbl_comment = new Label();
            lbl_comment.CssClass = "comment";
            lbl_comment.Text = Reader[3].ToString();
            cell_comment_content.Controls.Add(lbl_comment);

            row_comment_date.Cells.Add(cell_comment_date);
            row_comment_content.Cells.Add(cell_comment_content);

            tbl_comment.Rows.Add(row_comment_date);
            tbl_comment.Rows.Add(row_comment_content);

            cell_comments.Controls.Add(tbl_comment);


            //Label last_act_datetime = new Label();
            //last_act_datetime.CssClass = "descr_forum";
            //if (get_last_user(Reader[0].ToString()) != "/")
            //{
            //    last_act_user.Text = "by " + get_last_user(Reader[0].ToString());
            //    last_act_datetime.Text = get_last_user_time(Reader[0].ToString());
            //}
            //else
            //{
            //    last_act_user.Text = "by " + get_starting_user(Reader[1].ToString());
            //    last_act_datetime.Text = (((DateTime)(Reader[3])).ToShortDateString());
            //}


            //cell_Last_comment_starter_group.Controls.Add(last_act_user);
            //cell_Last_comment_starter_group.Controls.Add(nl3);
            //cell_Last_comment_starter_group.Controls.Add(last_act_datetime);

            row_comment.Cells.Add(cell_title_user_pic);
            row_comment.Cells.Add(cell_comments);
            comments_table.Rows.Add(row_comment);
        }

        table_topics.Controls.Add(comments_table);
        command.Connection.Close();
    }

    protected string get_user(string userid)
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
        string commenter = Reader[0].ToString();
        command.Connection.Close();

        return commenter;
    }

    protected string get_user_pic(string userid)
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT photo FROM userprofile where userid=@p1";

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
        string commenter_pic = Reader[0].ToString();
        command.Connection.Close();

        return commenter_pic;
    }

    protected void str_new_topic(object sender, EventArgs e)
    {
        string grp_id = get_group_id(tpc_id);

        Response.Redirect("../Forum/forumnewtopic.aspx?grp_id=" + grp_id);
    }

    protected void cmnt_on_this_topic(object sender, EventArgs e)
    {
        Response.Redirect("../Forum/forumtopicres.aspx?tpc_id=" + tpc_id + "&pg=" + pages + "&comment=true" + "&scroll=true");
    }

    protected string get_group_id(string topic_id)
    {
        string group_id = "";

        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "SELECT group_id FROM forum_topics where topic_id=@p1";

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

        group_id = Reader[0].ToString();

        command.Connection.Close();


        return group_id;
    }

    protected void post_new_comment()
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        SqlConnection connection = new SqlConnection(conn);

        string sqlqry = "INSERT INTO forum_comments values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)";

        SqlCommand command = new SqlCommand(sqlqry, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = inc_comment_id();
        command.Parameters.Add(param1);

        SqlParameter param2 = new SqlParameter();
        param2.SqlDbType = System.Data.SqlDbType.Int;
        param2.ParameterName = "@p2";
        param2.Value = tpc_id;
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

        SqlParameter param8 = new SqlParameter();
        param8.SqlDbType = System.Data.SqlDbType.Char;
        param8.ParameterName = "@p6";
        param8.Value = 0;
        command.Parameters.Add(param8);

        SqlParameter param6 = new SqlParameter();
        param6.SqlDbType = System.Data.SqlDbType.VarChar;
        param6.ParameterName = "@p7";
        param6.Value = TextBox2.Text;
        command.Parameters.Add(param6);

        command.Connection.Open();
        command.ExecuteNonQuery();
        command.Connection.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        post_new_comment();

        Response.Redirect("../Forum/forumtopicres.aspx?tpc_id=" + tpc_id + "&pg=" + pages);
    }

    protected int inc_comment_id()
    {
        //making connection string for connecting with database
        string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Query For Getting Order Number Of Last topic !
        string sqlqry_getprev_orderno = "SELECT TOP 1 comment_id FROM forum_comments ORDER BY comment_id DESC";

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

    protected void inc_view_of_topic(string topic_id)
    {
        //making connection string for connecting with database
        string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Query For Getting Order Number Of Last topic !
        string sqlqry_getprev_orderno = "UPDATE forum_topics SET views=views+1 WHERE topic_id=@p1";

        SqlConnection connection = new SqlConnection(conn_string);

        SqlCommand command_getprev_orderno = new SqlCommand(sqlqry_getprev_orderno, connection);

        SqlParameter param1 = new SqlParameter();
        param1.SqlDbType = System.Data.SqlDbType.Int;
        param1.ParameterName = "@p1";
        param1.Value = topic_id;
        command_getprev_orderno.Parameters.Add(param1);


        command_getprev_orderno.Connection.Open();
        command_getprev_orderno.ExecuteNonQuery();
        command_getprev_orderno.Connection.Close();
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
        link_group.NavigateUrl = "../Forum/forumtopics.aspx?grp_id=" + get_group_id(tpc_id)+"&pg=1";
        link_group.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(generate_forum_name(get_group_id(tpc_id)).ToLower());
        link_group.CssClass = "descr_forum";
        link_group.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");

        Label lbl2 = new Label();
        lbl2.Text = " >> ";
        lbl2.CssClass = "descr_forum";
        lbl2.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");

        HyperLink link_topic = new HyperLink();
        link_topic.NavigateUrl = "../Forum/forumtopicres.aspx?tpc_id=" +tpc_id+"&pg=1";
        link_topic.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(generate_topic_name(tpc_id).ToLower());
        link_topic.CssClass = "descr_forum";
        link_topic.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");

        where_are_u.Controls.Add(link_home);
        where_are_u.Controls.Add(lbl1);
        where_are_u.Controls.Add(link_group);
        where_are_u.Controls.Add(lbl2);
        where_are_u.Controls.Add(link_topic);
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
}