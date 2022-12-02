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

public partial class AdminPanal_Contact_ContactAddEdit : System.Web.UI.Page
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
            FillCountryDropDownList();
            FillContactCategoryDropDownList();
            #region Edit Mode
            if (Request.QueryString["ContactID"] != null)
            {
                lblHeading.Text = "<h2>Edit Mode</h2>";
                FillControls(Convert.ToInt32(Request.QueryString["ContactID"]));
                FillStateDropDownList();
                FillCityDropDownList();
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
        SqlInt32 strUserID = SqlInt32.Null;
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlInt32 strCityID = SqlInt32.Null;
        SqlInt32 strContactCategoryID = SqlInt32.Null;
        SqlString strContactName = SqlString.Null;
        SqlString strContactNo = SqlString.Null;
        SqlString strWhatsAppNo = SqlString.Null;
        SqlDateTime strBirthDate = SqlDateTime.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strAge = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strBloodGroup = SqlString.Null;
        SqlString strFaceBookID = SqlString.Null;
        SqlString strLinkedINID = SqlString.Null;
       
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

            if (ddlStateID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select State <br/>";
            }

            if (ddlCityID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select City <br/>";
            }

            if (ddlContactCategoryID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select ContactCategory <br/>";
            }

            if (txtContactName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Contact Name <br/>";
            }

            if (txtContactNo.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Contact Number <br/>";
            }

            if (txtEmail.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Email I'D <br/>";
            }

            if (txtAddress.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Your Address <br/>";
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
            if (ddlStateID.SelectedIndex > 0)
            {
                strStateID = Convert.ToInt32(ddlStateID.SelectedValue);

            }
            if (ddlCityID.SelectedIndex > 0)
            {
                strCityID = Convert.ToInt32(ddlCityID.SelectedValue);

            }

            if (ddlContactCategoryID.SelectedIndex > 0)
            {
                strContactCategoryID = Convert.ToInt32(ddlContactCategoryID.SelectedValue);

            }

            if (txtContactName.Text.Trim() != "")
            {
                strContactName = txtContactName.Text.Trim();
            }

            if (txtContactNo.Text.Trim() != "")
            {
                strContactNo = txtContactNo.Text.Trim();
            }

            if (txtEmail.Text.Trim() != "")
            {
                strEmail = txtEmail.Text.Trim();
            }

            if (txtAddress.Text.Trim() != "")
            {
                strAddress = txtAddress.Text.Trim();
            }
            if (txtWhatsAppNo.Text.Trim() != "")
            {
                strWhatsAppNo = txtWhatsAppNo.Text.Trim();
            }
            if (txtBirthDate.Text.Trim() != "")
            {
                strBirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim());
            }
            if (txtAge.Text.Trim() != "")
            {
                strAge = txtAge.Text.Trim();
            }

            if (txtBloodGroup.Text.Trim() != "")
            {
                strBloodGroup = txtBloodGroup.Text.Trim();
            }

            if (txtFacebookID.Text.Trim() != "")
            {
                strFaceBookID = txtFacebookID.Text.Trim();
            }


            if (txtLinkedINID.Text.Trim() != "")
            {
                strLinkedINID = txtLinkedINID.Text.Trim();
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
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CityID", strCityID);
            objCmd.Parameters.AddWithValue("@ContactCategoryID", strContactCategoryID);
            objCmd.Parameters.AddWithValue("@ContactName", strContactName);
            objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
            objCmd.Parameters.AddWithValue("@WhatsAppNo", strWhatsAppNo);
            objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
            objCmd.Parameters.AddWithValue("@Email", strEmail);
            objCmd.Parameters.AddWithValue("@Age", strAge);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.Parameters.AddWithValue("@BloodGroup", strBloodGroup);
            objCmd.Parameters.AddWithValue("@FaceBookID", strFaceBookID);
            objCmd.Parameters.AddWithValue("@LinkedINID", strLinkedINID);
           
            #endregion Parameters
            #region Edit Mode
            if (Request.QueryString["ContactID"] != null)
            {
                //Edit mode
                
                objCmd.CommandText = "[dbo].[PR_Contact_UpdateByUserIContactID]";
                objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanal/Contact/ContactList.aspx");
            }
            #endregion Edit Mode
            #region Add Mode
            else
            {
                //Add mode
                objCmd.CommandText = "[dbo].[PR_Contact_Insert]";
                objCmd.ExecuteNonQuery();
                ddlCountryID.SelectedIndex = 0;
                ddlStateID.SelectedIndex = 0;
                ddlCityID.SelectedIndex = 0;
                ddlContactCategoryID.SelectedIndex = 0;
                txtContactName.Text = "";
                txtContactNo.Text = "";
                txtWhatsAppNo.Text = "";
                txtBirthDate.Text = "";
                txtEmail.Text = "";
                txtAge.Text = "";
                txtAddress.Text = "";
                txtBloodGroup.Text = "";
                txtFacebookID.Text = "";
                txtLinkedINID.Text = "";
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

    #region Fill Country
    private void FillCountryDropDownList()
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
    #endregion Fill Country

    #region Fill State
    private void FillStateDropDownList()
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
            objCmd.CommandText = "[dbo].[PR_State_CityDropDownList]";
            objCmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddlCountryID.SelectedValue));
         
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlStateID.DataSource = objSDR;
                ddlStateID.DataValueField = "StateID";
                ddlStateID.DataTextField = "StateName";
                ddlStateID.DataBind();
            }
            #endregion Read the Values and set the Controls
            ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
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
    #endregion Fill State

    #region Fill City
    private void FillCityDropDownList()
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
            objCmd.CommandText = "[dbo].[PR_City_SelectForDropDownList]";
            objCmd.Parameters.AddWithValue("@StateID", Convert.ToInt32(ddlStateID.SelectedValue));
           
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlCityID.DataSource = objSDR;
                ddlCityID.DataValueField = "CityID";
                ddlCityID.DataTextField = "CityName";
                ddlCityID.DataBind();
            }
            #endregion Read the Values and set the Controls
            ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
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
    #endregion Fill City

    #region Fill Contact
    private void FillContactCategoryDropDownList()
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
            objCmd.CommandText = "[dbo].[PR_ContactCategory_SelectForDropDownList]";
            if (Session["UserID"] != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlContactCategoryID.DataSource = objSDR;
                ddlContactCategoryID.DataValueField = "ContactCategoryID";
                ddlContactCategoryID.DataTextField = "ContactCategoryName";
                ddlContactCategoryID.DataBind();
            }
            #endregion Read the Values and set the Controls
            ddlContactCategoryID.Items.Insert(0, new ListItem("Select ContactCategory", "-1"));
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
    #endregion Fill Contact

    #region Fill Controls
    private void FillControls(SqlInt32 ContactID)
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
            objCmd.CommandText = "[dbo].[PR_Contact_SelectByUserIContactID]";
            #region Parameters
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            #endregion Parameters
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #region Rows
            if (objSDR.HasRows)
            {
                #region Read
                while (objSDR.Read())
                {
                    if (objSDR["ContactName"].Equals(DBNull.Value) != true)
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();

                    }

                    if (objSDR["ContactNo"].Equals(DBNull.Value) != true)
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                    }

                    if (objSDR["WhatsAppNo"].Equals(DBNull.Value) != true)
                    {
                        txtWhatsAppNo.Text = objSDR["WhatsAppNo"].ToString().Trim();
                    }

                    if (objSDR["BirthDate"].Equals(DBNull.Value) != true)
                    {

                        txtBirthDate.Text = Convert.ToDateTime(objSDR["BirthDate"]).Date.ToString("yyyy-MM-dd");
                    }

                    if (objSDR["Email"].Equals(DBNull.Value) != true)
                    {
                        txtEmail.Text = objSDR["Email"].ToString().Trim();
                    }
                    if (objSDR["Age"].Equals(DBNull.Value) != true)
                    {
                        txtAge.Text = objSDR["Age"].ToString().Trim();
                    }
                    if (objSDR["Address"].Equals(DBNull.Value) != true)
                    {
                        txtAddress.Text = objSDR["Address"].ToString().Trim();
                    }
                    if (objSDR["BloodGroup"].Equals(DBNull.Value) != true)
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                    }
                    if (objSDR["FacebookID"].Equals(DBNull.Value) != true)
                    {
                        txtFacebookID.Text = objSDR["FacebookID"].ToString().Trim();
                    }
                    if (objSDR["LinkedINID"].Equals(DBNull.Value) != true)
                    {
                        txtLinkedINID.Text = objSDR["LinkedINID"].ToString().Trim();
                    }

                    if (objSDR["CountryID"].Equals(DBNull.Value) != true)
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }

                    if (objSDR["StateID"].Equals(DBNull.Value) != true)
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }

                    if (objSDR["CityID"].Equals(DBNull.Value) != true)
                    {
                        ddlCityID.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }

                    if (objSDR["ContactCategoryID"].Equals(DBNull.Value) != true)
                    {
                        ddlContactCategoryID.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                    }
                    break;
                }
                #endregion Read
            }
            #endregion Rows
            #endregion Read the Values and set the Controls
            else
            {
                lblMessage.Text = "No Data available for the ContactID = " + ContactID.ToString();
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
        Response.Redirect("~/AdminPanal/Contact/ContactList.aspx");
    }
    #endregion Button : Cancel

    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStateID.Items.Clear();
        FillStateDropDownList();
    }
    protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCityID.Items.Clear();
        FillCityDropDownList();
    }

    
}