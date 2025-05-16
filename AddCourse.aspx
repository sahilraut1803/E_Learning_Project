<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="E_Learning_Project.AddCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center" style="min-height: 80vh;">
        <div class="container w-50 border p-4 rounded shadow bg-light">
            <h2 class="text-center mb-4">Add Master Course</h2>

            <div class="form-group">
                <asp:TextBox ID="txtMasterCourse" runat="server" CssClass="form-control" placeholder="Course Name" autocomplete="off" />
            </div>
            <br />

            <div class="form-group">
                <asp:FileUpload ID="fuThumbnail" runat="server" CssClass="form-control" />
            </div>
            <br />

            <div class="text-center">
                <asp:Button ID="btnAddMasterCourse" runat="server" Text="Add" OnClick="btnAddMasterCourse_Click" CssClass="btn btn-primary w-100" />
            </div>
        </div>
    </div>
</asp:Content>
