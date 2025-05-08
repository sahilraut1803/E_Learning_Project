<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="E_Learning_Project.AddCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main-panel">
        <div class="container">
            <div class="page-inner">
                <div class="card shadow">
                    <div class="card-header">
                    </div>
                    <div class="card-body">
                        <div class="row-cols-2">
                            <div class="col">
                                <asp:Label ID="lblCourseName" runat="server">Course Name</asp:Label>
                                 
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="txtCourseName" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
