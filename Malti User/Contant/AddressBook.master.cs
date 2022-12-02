using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contant_AddressBook : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region check Valid User
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/AdminPanel/Login.aspx", true);
        }
        #endregion check Valid User
        if (!Page.IsPostBack)
        {
            if (Session["DisplayName"] != null)
            {
                lblUserName.Text = "Hi " + Session["DisplayName"] + "!";
            }
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/AdminPanal/Login.aspx");
    }
}
