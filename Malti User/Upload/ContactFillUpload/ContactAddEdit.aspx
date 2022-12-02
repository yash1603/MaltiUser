<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactAddEdit.aspx.cs" Inherits="Upload_Contact_Fill_Upload_ContactAddEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
  <form id="form1" runat="server">
       <div class="row">
        <div class="col-md-12">
            <h2>Contact File Upload Add Edit Page</h2>
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            Contact Name
        </div>
        <div class="col-md-8">
            <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control" />
            
                <asp:RequiredFieldValidator id="rfvContactName" runat="server"
                ControlToValidate="txtContactName"
                ErrorMessage="Please field Country name."
                ForeColor="Red">
                </asp:RequiredFieldValidator>
        </div>
    </div>
    
    <br />
    <div class="row">
        <div class="col-md-4">
            Upload Photo
        </div>
        <div class="col-md-8">
            <asp:FileUpload ID="fuContactPhotoPath" runat="server" /> 
            <!-- <asp:TextBox runat="server" ID="txtContactPhotoPath" CssClass="form-control" />
            
                <asp:RequiredFieldValidator id="rfvContactPhotoPath" runat="server"
                ControlToValidate="txtContactPhotoPath"
                ErrorMessage="Please field Contact Photo."
                ForeColor="Red">
                </asp:RequiredFieldValidator>-->
        </div>
    </div> 
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-8">
            <asp:Label runat="server" CssClass="btn-outline-danger" ID="lblMessage" EnableViewState="false" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-8">
            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-dark btn-sm" OnClick="btnSave_Click"/>       
            <asp:HyperLink runat="server" ID="hlCancel" Text="Cancel" CssClass="btn btn-danger btn-sm" NavigateUrl="~/Upload/ContactFileUpload/ContactFileLists.aspx" />
        </div>
    </div>
  </form>
</body>
</html>
