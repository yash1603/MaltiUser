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

public partial class AdminPanal_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        #region check Valid User
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/AdminPanal/Login.aspx", true);
        }
        #endregion check Valid User
        if (!Page.IsPostBack)
        {
            #region Edit Mode
            if (Request.QueryString["ContactCategoryID"] != null)
            {
                lblHeading.Text = "<h2>Edit Mode</h2>";
                FillControls(Convert.ToInt32(Request.QueryString["ContactCategoryID"]));
            }
            #endregion Edit Mode
            #region Add Mode
            else
            {
                lblHeading.Text = "<h2>Add Mode</h2>";
            }
            #endregion Add Mode

        }
    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #region Local Variable
        //Declare Local Variable
        SqlString strContactCategoryName = SqlString.Null;
        SqlInt32 strUserID = SqlInt32.Null;
        #endregion Local Variable
        #region Try Block
        try
        {
            #region Server Side Validation
            //Validate the Data
            String strErrorMessage = "";

            if (txtContactCategoryName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Country Name <br/>";
            }


            if (strErrorMessage != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation
            #region Gather Information
            //Save the Country Data
            if (txtContactCategoryName.Text.Trim() != "")
            {
                strContactCategoryName = txtContactCategoryName.Text.Trim();
            }
            if (Session["UserID"] != null)
            {
                strUserID = Convert.ToInt32(Session["UserID"]);
            }
            #endregion Gather Information
            #region Connection
            //Establish a Connection
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #region Command Object
            //Prepare the Command
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            #region Parameters
            //Pass the Parameters in sp
            objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            #endregion Parameters
            #region Edit Mode
            if (Request.QueryString["ContactCategoryID"] != null)
            {
                //Edit mode
                objCmd.Parameters.AddWithValue("@ContactCategoryID", Request.QueryString["ContactCategoryID"].ToString().Trim());
            
                objCmd.CommandText = "[dbo].[PR_ContactCategory_UpdateByUserIDContactCategoryID]";
               
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanal/ContactCategory/ContactCategoryList.aspx", true);
            }
            #endregion Edit Mode
            #region Add Mode
            else
            {
                //Add mode
                objCmd.CommandText = "[dbo].[PR_ContactCategory_Insert]";
                objCmd.ExecuteNonQuery();
                lblMessage.Text = "Data Inserted Successfully";
                txtContactCategoryName.Text = "";
                txtContactCategoryName.Focus();
            }
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

    #region Fill Controls
    private void FillControls(SqlInt32 ContactCategoryID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #region Try Block
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
            objCmd.CommandText = "[dbo].[PR_ContactCategory_SelectByUserIDContactCategoryID]";
            #region Parameters
            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());
            #endregion Parameters
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #region Rows
            if (objSDR.HasRows)
            {
                #region Read
                while (objSDR.Read())
                {
                    if (objSDR["ContactCategoryName"].Equals(DBNull.Value) != true)
                    {
                        txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                    }
                    break;
                }
                #endregion Read

            }
            #endregion Rows
            #endregion Read the Values and set the Controls
            else
            {
                lblMessage.Text = "No Data available for the ContactCategoryID = " + ContactCategoryID.ToString();
            }
            #endregion Command Object
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
    #endregion Fill Controls

    #region Button : Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanal/ContactCategory/ContactCategoryList.aspx");
    }
    #endregion Button : Cancel
}