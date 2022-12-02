using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanal_Login : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Load Event

    #region Button : Login
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        // Validate user Or not Validate user
        // UserName, Password
        #region Local Variable
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;

        #endregion Local Variable

        #region Server Side Validation
        String strErrorMessage = "";

        if (txtUserNameLogin.Text.Trim() == "")
        {
            strErrorMessage += "- Enter UserName <br/>";
        }

        if (txtPasswordLogin.Text.Trim() == "")
        {
            strErrorMessage += "- Enter Password <br/>";
        }

        if (strErrorMessage != "")
        {
            lblMessage.Text = "Kindly  <br/>" + strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        #region Assign the Value
        if (txtUserNameLogin.Text.Trim() != "")
        {
            strUserName = txtUserNameLogin.Text.Trim();
        }
        if (txtPasswordLogin.Text.Trim() != "")
        {
            strPassword = txtPasswordLogin.Text.Trim();
        }
        #endregion Assign the Value

        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            #region Connection
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #region Command Object
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_User_SelectByUserNamePassword]";
            #endregion Command Object

            #region Parameters
            objCmd.Parameters.AddWithValue("@UserName", strUserName);
            objCmd.Parameters.AddWithValue("@Password", strPassword);
            #endregion Parameters
            #region Read the data Value and set the Controls

            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                lblMessage.Text = "Valid User";

                while (objSDR.Read())
                {
                    if (!objSDR["UserID"].Equals(DBNull.Value))
                    {
                        Session["UserID"] = objSDR["UserID"].ToString().Trim();
                    }

                    if (!objSDR["DisplayName"].Equals(DBNull.Value))
                    {
                        Session["DisplayName"] = objSDR["DisplayName"].ToString().Trim();
                    }
                    break;
                }
                Response.Redirect("~/AdminPanal/Default.aspx");
            }
            #endregion Read the data Value and set the Controls
            else
            {
                lblMessage.Text = "Either UserName or Password is not valid, Try Again with difference UserName and Password";
            }

            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }

        #endregion Connection Establish

    }
    #endregion Button : Login
}