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

public partial class AdminPanal_CreateAccount : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #region Local Variable
        SqlInt32 strUserID = SqlInt32.Null;
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        SqlString strName = SqlString.Null;
       
        SqlString strMobile = SqlString.Null;
        
        #endregion Local Variable

        #region Try Block
        try
        {
            #region Server Side Validation
            String strErrorMessage = "";
            if (txtUserName.Text.Trim() == "")
            {
                strErrorMessage += "- Kindly Enter Unique Name <br/>";
            }

            if (txtPassword.Text.Trim() == "")
            {
                strErrorMessage += "- Kindly Enter Password <br/>";
            }

            if (txtName.Text.Trim() == "")
            {
                strErrorMessage += "- Kindly Enter Your Name <br/>";
            }

           
          
            if (txtMobile.Text.Trim() == "")
            {
                strErrorMessage += "- Kindly Enter Your Mobile Number <br/>";
            }

            if (strErrorMessage.Trim() != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation
            #region Gather Information
            //Gather the Information

            if (txtUserName.Text.Trim() != "")
            {
                strUserName = txtUserName.Text.Trim();
            }

            if (txtPassword.Text.Trim() != "")
            {
                strPassword = txtPassword.Text.Trim();
            }

            if (txtName.Text.Trim() != "")
            {
                strName = txtName.Text.Trim();
            }

          
           
            
            if (txtMobile.Text.Trim() != "")
            {
                strMobile = txtMobile.Text.Trim();
            }

            #endregion Gather Information
            #region Connection
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #region Command Object
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            #region Parameters
            objCmd.Parameters.AddWithValue("@UserName", strUserName);
          
            objCmd.Parameters.AddWithValue("@Password", strPassword);
            objCmd.Parameters.AddWithValue("@DisplayName", strName);
            
           
            objCmd.Parameters.AddWithValue("@MobileNo", strMobile);
            
            #endregion Parameters
            #region Add Mode
            //Add mode
            objCmd.CommandText = "[dbo].[PR_User_Insert]";
            objCmd.ExecuteNonQuery();
            txtUserName.Text = "";
           
            txtPassword.Text = "";
            txtName.Text = "";
           
            
            txtMobile.Text = "";
            txtUserName.Focus();
            lblMessage.Text = "Data Inserted Successfully";
            #endregion Add Mode
            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection

        }
        #endregion Try Block
        #region Catch Block
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        #endregion Catch Block
        #region Finally Block
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        #endregion Finally Block
        #endregion Connection Establish
    }
    #endregion Button : Save

    #region Button : Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanal/Login.aspx", true);
    }
    #endregion Button : Cancel
}