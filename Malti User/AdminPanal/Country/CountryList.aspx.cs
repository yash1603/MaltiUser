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

public partial class AdminPanal_Country_CountryList : System.Web.UI.Page
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

            FillGridView();
        }
    }
    #endregion Load Event

    #region Fill Grid View
    private void FillGridView()
    {
        #region Connection Establish
        //Establish the Connection
        //Create empty connection object
        //Read the connection string from web.config file
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            #region Connection
            //DESKTOP-TH5FB1M\\SQLEXPRESS ; This is source of data or Server name
            //AddressBook ; This is database name
            //Integrated Security ; if it is true then it is windows authentication and if it is false then it is sql authentication so we put the User id and password 
            //User Id = ****
            //Password = ****
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open(); //Close the Connection
            }
            #region Command Object
            //Do Your Work
            //Step 2 Prepare the Command  object
            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            //objCmd.CommandType = CommandType.Text;
            //objCmd.CommandType = CommandType.TableDirect;
            objCmd.CommandText = "[dbo].[PR_Country_SelectByUserID]";
            if (Session["UserID"] != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }

            //objCmd.ExecuteNonQuery();   //Insert/Update/Delete
            //objCmd.ExecuteReader();     //Select
            //objCmd.ExecuteScalar();     //Only One scalar value for example count function
            //objCmd.ExecuteXmlReader();  //xml type of data
            #endregion Command Object
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvCountry.DataSource = objSDR;
            gvCountry.DataBind();
            #endregion Read the Values and set the Controls
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close(); //Close the Connection
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
                objConn.Close(); //Close the Connection
            }
        }
        #endregion Connection Establish
    }
    #endregion Fill Grid View

    #region gvCountry : RowCommand
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        // Which command is clicked | e.CommandName
        // Which Row is clicked | Get the ID of that Row | e.CommandArgument
        if (e.CommandName == "DeleteRecord")
        {

            if (e.CommandArgument.ToString() != "")
            {
                DeleteCountry(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                lblMessage.Text = "Deleted Successfully";
            }
        }


    }
    #endregion gvCountry : RowCommand

    #region Delete Country
    private void DeleteCountry(SqlInt32 CountryID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        try
        {
            #region Connection
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #region Command Object
            SqlInt32 strUserID = SqlInt32.Null;

            if (Session["UserID"] != null)
            {
                strUserID = Convert.ToInt32(Session["UserID"]);
            }
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_Country_DeleteByUserIDCountryID]";
            objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString());
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.ExecuteNonQuery();
            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection
            FillGridView();
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
    #endregion Delete Country
}