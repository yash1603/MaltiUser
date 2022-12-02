<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="AdminPanal_CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Contant/css/bootstrap-theme.min.css" rel="stylesheet" />

    <link href="../Contant/css/bootstrap.min.css" rel="stylesheet" />

    <script src="../Contant/js/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row" align="justify">
            <h2 align="center">Create Your Account</h2>
            <hr />
            <div class="row">
                <div class="col-md-7">
                    <asp:Label runat="server" ID="lblHeading"></asp:Label>
                </div>
            </div>
            <asp:Label runat="server" ID="lblMessage" Text="" EnableViewState="false"></asp:Label>
            <br />
            <div class="row">
                <div class="col-md-4">
                    <asp:Label runat="server" Text="User Name" ID="lblUserName"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><br />
                </div>
                <div class="col-md-2">

                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Enter Unique Name" ForeColor="Red" ToolTip="UserName - Error" ValidationGroup="Save"></asp:RequiredFieldValidator>

                </div>
            </div>
                <br />
                <br />
            <div class="row">
                <div class="col-md-4">
                    <asp:Label runat="server" Text="Password" ID="lblPassword"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox><br />
                </div>
                <div class="col-md-2">

                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Enter Your Password" ForeColor="Red" ToolTip="Password - Error" ValidationGroup="Save"></asp:RequiredFieldValidator>

                </div>
            </div><br /><br />
            <div class="row">
                <div class="col-md-4">
                    <asp:Label runat="server" Text="Name" ID="lblName"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
                </div>
                <div class="col-md-2">

                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Enter Your Name" ForeColor="Red" ToolTip="Name - Error" ValidationGroup="Save"></asp:RequiredFieldValidator>

                </div>
            </div>
            
           <br /><br />
           
            <div class="row">
                <div class="col-md-4">
                    <asp:Label runat="server" Text="Mobile" ID="lblMobile"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox><br />
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="Enter Your Mobile Number" ForeColor="Red" ToolTip="Mobile Number - Error" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
            </div>
                <br /><br />
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-8">
                    <asp:Button runat="server" ID="btnSave" ToolTip="Submit" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success" ValidationGroup="Save" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnCancel" ToolTip="Cancel" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
