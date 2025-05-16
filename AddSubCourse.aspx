<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddSubCourse.aspx.cs" Inherits="E_Learning_Project.AddSubCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center" style="min-height: 80vh;">
        <div class="container w-50 border p-4 rounded shadow bg-light">
            <h2 class="text-center mb-4">Add Subcourse</h2>

            <div class="form-group">
                <asp:DropDownList ID="ddlMasterCourse" runat="server" CssClass="form-control">
                    <asp:ListItem Text="-- Select Master Course --" Value="" />
                </asp:DropDownList>
            </div>
            <br />

            <div class="form-group">
                <asp:TextBox ID="txtSubCourseName" runat="server" CssClass="form-control" placeholder="Subcourse Name" autocomplete="off" />
            </div>
            <br />

            <div class="form-group">
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Price" autocomplete="off" />
            </div>
            <br />

            <div class="form-group">
                <asp:FileUpload ID="fuSubThumbnail" runat="server" CssClass="form-control" />
            </div>
            <br />

            <div class="text-center">
                <asp:Button ID="btnAddSubCourse" runat="server" Text="Add Subcourse" OnClick="btnAddSubCourse_Click" CssClass="btn btn-success w-100" />
            </div>
        </div>
    </div>
</asp:Content>
