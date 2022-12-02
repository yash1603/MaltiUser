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

public partial class Upload_Contact_Fill_Upload_ContactList : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillGridView();
        }
    }
    #endregion Page Load

    #region Fill GridView
    private void FillGridView()
    {

        SqlConnection objConn = new SqlConnection();
        objConn.ConnectionString = "data source =LAPTOP-PT37LCJK;initial catalog=Yash;Integrated Security=True;"; 
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_Contact_SelecetAll]";
            #endregion Set Connection & Command Object

            #region Read the Value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                gvContactFileUpload.DataSource = objSDR;
                gvContactFileUpload.DataBind();
            }
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Read the Value and set the controls
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objConn.Close();
        }

    }
    #endregion Fill GridView


 
  

    #region Delete Contact File Upload
    private void DeleteContactFileUpload(SqlInt32 ContactFileUploadID)
    {
        SqlConnection objConn = new SqlConnection();
        objConn.ConnectionString = "data source =LAPTOP-PT37LCJK;initial catalog=Yash;Integrated Security=True;"; 
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_Contact_DeleteByPK]";
            objCmd.Parameters.AddWithValue("@ContactFileUploadID", ContactFileUploadID.ToString());
            objCmd.ExecuteNonQuery();

            lblMessage.Text = "Data deleted successful";
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Set Connection & Command Object
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objConn.Close();
        }

        FillGridView();
    }
    #endregion Delete Contact File Upload

    #region gvContactFileUpload : RowCommand
    protected void gvContactFileUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete Record
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteContactFileUpload(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
        #endregion Delete Record
    }

   
    #endregion gvContactFileUpload : RowCommand
}