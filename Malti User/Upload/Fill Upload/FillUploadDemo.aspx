<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FillUploadDemo.aspx.cs" Inherits="Upload_Fill_Upload_FillUploadDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>

</head>
<body>
      <form id="form1" runat="server">
        <div class="container">
                <div>Selecet a File to Upload<br/>
                    <asp:FileUpload ID="fuFile" runat="server" /><br />
                    <asp:Button runat="server" ID="btnUpload" Text="Upload" OnClick="btnUpload_Click" /><br />
                    <asp:Label runat="server" ID="lblMessages" EnableViewState="false" ForeColor="Red"/> 
                    <br />
                    <asp:Button runat="server" ID="btnDelete" Text="Delete File" OnClick="btnDelete_Click" />
                </div>
        </div>
    </form>
</body>
</html>
