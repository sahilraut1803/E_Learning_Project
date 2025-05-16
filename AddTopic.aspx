<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddTopic.aspx.cs" Inherits="E_Learning_Project.AddTopic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center" style="min-height: 80vh;">
        <div class="container w-50 border p-4 rounded shadow bg-light">
            <h2 class="text-center mb-4">Add Topic</h2>

            <div class="form-group">
                <asp:DropDownList ID="ddlMasterCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterCourse_SelectedIndexChanged" CssClass="form-control">
                    <asp:ListItem Text="-- Select Master Course --" Value="" />
                </asp:DropDownList>
            </div>
            <br />

            <div class="form-group">
                <asp:DropDownList ID="ddlSubCourse" runat="server" CssClass="form-control">
                    <asp:ListItem Text="-- Select Subcourse --" Value="" />
                </asp:DropDownList>
            </div>
            <br />

            <div class="form-group">
                <asp:TextBox ID="txtTopicName" runat="server" CssClass="form-control" placeholder="Topic Name" autocomplete="off" />
            </div>
            <br />

            <div class="form-group">
                <asp:TextBox ID="txtVideoURL" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Paste YouTube embed code here (e.g. <iframe>)" autocomplete="off"></asp:TextBox>
            </div>
            <br />

            <div class="form-group">
                <asp:TextBox ID="txtDurationSeconds" runat="server" CssClass="form-control" placeholder="Duration in seconds" autocomplete="off" />
            </div>
            <br />

            <div class="text-center">
                <asp:Button ID="btnAddTopic" runat="server" Text="Add Topic" CssClass="btn btn-success w-100" OnClick="btnAddTopic_Click" />
            </div>
        </div>
    </div>
</asp:Content>
