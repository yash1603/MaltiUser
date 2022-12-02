using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Upload_Fill_Upload_FillUploadDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //check for selection of file
        if (fuFile.HasFile)
        {
            // lblMessages.Text = "File is Selectes - " + fuFile.FileName.ToString().Trim();
            String FolderPath = "~/Upload/add/";
            String AbsolutePath = Server.MapPath(FolderPath);

            lblMessages.Text = "File will be Uploaded at the location " + AbsolutePath;

            if (!Directory.Exists(AbsolutePath))
                Directory.CreateDirectory(AbsolutePath);

            fuFile.SaveAs(AbsolutePath + fuFile.FileName.ToString().Trim()); //.Length.ToString()
        }
        else
        {
            lblMessages.Text = "You Have not Selected a File";
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        String FilePath = "~/Upload/add/WP-Lab Planning-2021-22.pdf";

        FileInfo file = new FileInfo(Server.MapPath(FilePath));

        if (file.Exists)
        {
            file.Delete();
        }
        else
        {
            lblMessages.Text = "File dose not Available";
        }
    }
}