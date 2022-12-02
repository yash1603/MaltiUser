<%@ Page Title="" Language="C#" MasterPageFile="~/Contant/AddressBook.master" AutoEventWireup="true" CodeFile="ContactList.aspx.cs" Inherits="AdminPanal_Contact_ContactList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
     <div class="container">
        <div class="row">
            <div class="col-md-12" align="center">
                <h2>Contact List</h2>
                <hr />
                <br />
            </div>
            <div class="col-md-1">
                <asp:HyperLink runat="server" ID="hlContactAddEdit" Text="Add Contact" ToolTip="Contact -- Add" NavigateUrl="~/AdminPanal/Contact/ContactAddEdit.aspx" CssClass="btn btn-success btn-sm"></asp:HyperLink>
            </div>
        </div>
        <asp:Label runat="server" ID="lblMessage" Text="" EnableViewState="false"></asp:Label>
        <div class="row">
            <div class="col-md-12">
                <br />
                <asp:GridView ID="gvContact" runat="server" AutoGenerateColumns="false" OnRowCommand="gvContact_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ContactID" HeaderText="ID" />
                        <asp:BoundField DataField="CountryName" HeaderText="Country" />
                        <asp:BoundField DataField="StateName" HeaderText="State" />
                        <asp:BoundField DataField="CityName" HeaderText="City" />
                        <asp:BoundField DataField="ContactCategoryName" HeaderText="Category of Contact" />
                        <asp:BoundField DataField="ContactName" HeaderText="Name" />
                        <asp:BoundField DataField="ContactNo" HeaderText="Number" />
                        <asp:BoundField DataField="WhatsAppNo" HeaderText="WhatsApp Number" />
                        <asp:BoundField DataField="BirthDate" HeaderText="BirthDate" DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Email" HeaderText="Email ID" />
                        <asp:BoundField DataField="Age" HeaderText="Age" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group" />
                        <asp:BoundField DataField="FacebookID" HeaderText="FaceBook ID" />
                        <asp:BoundField DataField="LinkedINID" HeaderText="Linkedin ID" />
                       
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID").ToString() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="btnEdit" Text="Edit" CssClass="btn btn-info btn-sm" CommandName="EditRecord" NavigateUrl='<%#"~/AdminPanal/Contact/ContactAddEdit.aspx?ContactID=" +Eval("ContactID").ToString().Trim() %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

