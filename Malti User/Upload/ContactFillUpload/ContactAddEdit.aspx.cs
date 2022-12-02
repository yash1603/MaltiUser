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

public partial class Upload_Contact_Fill_Upload_ContactAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        String ContactPhotoPath = "";
        if (fuContactPhotoPath.HasFile)
        {
            ContactPhotoPath = "~/Upload/add/" + fuContactPhotoPath.FileName.ToString().Trim();
            fuContactPhotoPath.SaveAs(Server.MapPath(ContactPhotoPath));
        }

        #region Local Variables
        SqlConnection objConn = new SqlConnection();
        objConn.ConnectionString = "data source =LAPTOP-PT37LCJK;initial catalog=Yash;Integrated Security=True;"; 
       
        SqlString strContactName = SqlString.Null;
        SqlString strContactPhotoPath = SqlString.Null;
        #endregion Local Variables

        try
        {

            #region Server Side Validation
            string strErrorMessge = "";

            if (strErrorMessge != "")
            {
                lblMessage.Text = strErrorMessge;
                return;
            }
            #endregion Server Side Validation

            #region Gather the Information
            /*if (txtContactPhotoPath.Text.Trim() != "")
            {
                strContactPhotoPath = txtContactPhotoPath.Text.Trim();            
            }*/
            if (txtContactName.Text.Trim() != "")
            {
                strContactName = txtContactName.Text.Trim();
            }
            #endregion Gather the Information

            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_Contact_Insert]";
            objCmd.Parameters.AddWithValue("@ContactName", txtContactName.Text.Trim());
            objCmd.Parameters.AddWithValue("@ContactPhotoPath", ContactPhotoPath);
            objCmd.ExecuteNonQuery();
            txtContactName.Text = "";
            lblMessage.Text = "Data Inserted Successfully";
            #endregion Set Connection & Command Object

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            Response.Redirect("~/Upload/ContactFillUpload/ContactList.aspx", true);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Button : Save
}