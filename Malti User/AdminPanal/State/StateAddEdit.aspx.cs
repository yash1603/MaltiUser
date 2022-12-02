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

public partial class AdminPanal_State_StateAddEdit : System.Web.UI.Page
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
            FillDropDownList();
            #region Edit Mode
            if (Request.QueryString["StateID"] != null)
            {
                lblHeading.Text = "<h2>Edit Mode</h2>";
                FillControls(Convert.ToInt32(Request.QueryString["StateID"]));
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
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlString strStateName = SqlString.Null;
        SqlString strStateCode = SqlString.Null;
        SqlInt32 strUserID = SqlInt32.Null;
        #endregion Local Variable
        #region Try Block
        try
        {
            #region Server Side Validation
            String strErrorMessage = "";

            if (ddlCountryID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select Country <br/>";
            }

            if (txtStateName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter State Name <br/>";
            }

            if (strErrorMessage.Trim() != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation
            #region Gather Information
            //Gather the Information
            if (ddlCountryID.SelectedIndex > 0)
            {
                strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);

            }

            if (txtStateName.Text.Trim() != "")
            {
                strStateName = txtStateName.Text.Trim();
            }
            if (txtStateCode.Text.Trim() != "")
            {
                strStateCode = txtStateCode.Text.Trim();
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
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            #region Parameters
            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@StateName", strStateName);
            objCmd.Parameters.AddWithValue("@StateCode", strStateCode);
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            #endregion Parameters
            #region Edit Mode
            if (Request.QueryString["StateID"] != null)
            {
                //Edit Mode
                objCmd.Parameters.AddWithValue("@StateID", Request.QueryString["StateID"].ToString().Trim());
                objCmd.CommandText = "[dbo].[PR_State_UpdateByUserIDStateID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanal/State/StateList.aspx", true);

            }
            #endregion Edit Mode
            #region Add Mode
            else
            {
                //Add Mode
                objCmd.CommandText = "[dbo].[PR_State_Insert]";
                objCmd.ExecuteNonQuery();
                ddlCountryID.SelectedIndex = 0;
                txtStateName.Text = "";
                txtStateCode.Text = "";
                ddlCountryID.Focus();

                lblMessage.Text = "Data Inserted Successfully";
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

    #region Fill DropDown List
    private void FillDropDownList()
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
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
            objCmd.CommandText = "[dbo].[PR_Country_SelectForDropDownList]";
            if (Session["UserID"] != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows == true)
            {
                ddlCountryID.DataSource = objSDR;
                ddlCountryID.DataValueField = "CountryID";
                ddlCountryID.DataTextField = "CountryName";
                ddlCountryID.DataBind();
            }
            #endregion Read the Values and set the Controls
            ddlCountryID.Items.Insert(0, new ListItem("Select Country", "-1"));
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
    #endregion Fill DropDown List

    #region Fill Controls
    private void FillControls(SqlInt32 StateID)
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
            objCmd.CommandText = "[dbo].[PR_State_SelectByUserIDStateID]";
            #region Parameters
            objCmd.Parameters.AddWithValue("@StateID", StateID.ToString().Trim());
            #endregion Parameters
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #region Rows
            if (objSDR.HasRows)
            {
                #region Read
                while (objSDR.Read())
                {
                    if (objSDR["StateName"].Equals(DBNull.Value) != true)
                    {
                        txtStateName.Text = objSDR["StateName"].ToString().Trim();

                    }

                    if (objSDR["StateCode"].Equals(DBNull.Value) != true)
                    {
                        txtStateCode.Text = objSDR["StateCode"].ToString().Trim();
                    }

                    if (objSDR["CountryID"].Equals(DBNull.Value) != true)
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }
                    break;
                }
                #endregion Read
            }
            #endregion Rows
            #endregion Read the Values and set the Controls
            else
            {
                lblMessage.Text = "No Data available for the StateID = " + StateID.ToString();
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
        #endregion Connection Establish
    }
    #endregion Fill Controls

    #region Button : Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanal/State/StateList.aspx", true);
    }
    #endregion Button : Cancel
}