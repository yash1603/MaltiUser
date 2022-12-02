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

public partial class AdminPanal_Country_CountryAddEdit : System.Web.UI.Page
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
            if (Request.QueryString["CountryID"] != null)
            {
                lblHeading.Text = "<h2>Edit Mode</h2>";
                FillControls(Convert.ToInt32(Request.QueryString["CountryID"]));
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
        //Establish a Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #region Local Variable
        //Declare Local Variable
        SqlString strCountryName = SqlString.Null;
        SqlString strCountryCode = SqlString.Null;
        SqlInt32 strUserID = SqlInt32.Null;
        #endregion Local Variable

        #region Try Block
        try
        {
            #region Server Side Validation
            //Validate the Data
            String strErrorMessage = "";

            if (txtCountryName.Text.Trim() == "")
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
            if (txtCountryName.Text.Trim() != "")
            {
                strCountryName = txtCountryName.Text.Trim();
            }
            if (txtCountryCode.Text.Trim() != "")
            {
                strCountryCode = txtCountryCode.Text.Trim();
            }
            if (Session["UserID"] != null)
            {
                strUserID = Convert.ToInt32(Session["UserID"]);
            }
            #endregion Gather Information
            #region Connection
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
            objCmd.Parameters.AddWithValue("@CountryName", strCountryName);
            objCmd.Parameters.AddWithValue("@CountryCode", strCountryCode);
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            #endregion Parameters

            #region Edit Mode
            if (Request.QueryString["CountryID"] != null)
            {
                //Edit mode
                objCmd.Parameters.AddWithValue("@CountryID ", Request.QueryString["CountryID"].ToString().Trim());
                objCmd.CommandText = "[dbo].[PR_Country_UpdateByUserIDCountryID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanal/Country/CountryList.aspx", true);
            }
            #endregion Edit Mode
            #region Add Mode
            else
            {
                //Add mode
                objCmd.CommandText = "[dbo].[PR_Country_Insert]";
                objCmd.ExecuteNonQuery();
                lblMessage.Text = "Data Inserted Successfully";
                txtCountryName.Text = "";
                txtCountryCode.Text = "";
                txtCountryName.Focus();
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
    private void FillControls(SqlInt32 CountryID)
    {
        #region Connnection Establish
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
            objCmd.CommandText = "[dbo].[PR_Country_SelectByUserIDCountryID]";
           
            #region Parameters
            objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());
          
            #endregion Parameters
           
            #region Read the Values and set the Controls
           
            SqlDataReader objSDR = objCmd.ExecuteReader();
           
            #region Rows
            if (objSDR.HasRows)
            {
                #region Read
                while (objSDR.Read())
                {
                    if (objSDR["CountryName"].Equals(DBNull.Value) != true)
                    {
                        txtCountryName.Text = objSDR["CountryName"].ToString().Trim();

                    }

                    if (objSDR["CountryCode"].Equals(DBNull.Value) != true)
                    {
                        txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                    }
                    break;
                }
                #endregion Read
            }
            #endregion Rows
            #endregion Read the Values and set the Controls
            else
            {
                lblMessage.Text = "No Data available for the CountryID = " + CountryID.ToString();
            }
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
        #endregion Connnection Establish
    }
    #endregion Fill Controls

    #region Button : Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/AdminPanal/Country/CountryList.aspx", true);
    }
    #endregion Button : Cancel
}