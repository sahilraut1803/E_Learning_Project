<%@ Page Title="Course List" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CourseList.aspx.cs" Inherits="E_Learning_Project.CourseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center mt-4">
        <div class="w-100" style="max-width: 900px;">

            <!-- Search bar -->
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h2 class="mb-0">Course List</h2>
                <div class="d-flex">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control me-2" placeholder="Search by course..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>

            <asp:Repeater ID="rptCourses" runat="server">
                <ItemTemplate>
                    <div class="card my-4 shadow">
                        <div class="card-header d-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center">
                                <img src='<%# Eval("Thumbnail") %>' alt="Master Thumbnail" class="img-thumbnail me-3" style="max-height: 80px;" />
                                <h4 class="mb-0"><%# Eval("CourseName") %></h4>
                            </div>
                            <div>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-secondary me-2"
                                    CommandName="Edit" CommandArgument='<%# Eval("CourseID") %>' OnCommand="btnEdit_Command" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger"
                                    CommandName="Delete" CommandArgument='<%# Eval("CourseID") %>' OnCommand="btnDelete_Command" />
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Repeater ID="rptSubCourses" runat="server" DataSource='<%# Eval("SubCourses") %>'>
                                <ItemTemplate>
                                    <div class="mb-3 border p-3 rounded bg-light">
                                        <div class="d-flex align-items-center mb-2">
                                            <img src='<%# Eval("Thumbnail") %>' alt="Subcourse Thumbnail" class="img-thumbnail me-3" style="max-height: 60px;" />
                                            <h5 class="mb-0"><%# Eval("SubCourseName") %> - ₹<%# Eval("Price") %></h5>
                                        </div>
                                        <asp:Repeater ID="rptTopics" runat="server" DataSource='<%# Eval("Topics") %>'>
                                            <ItemTemplate>
                                                <div class="ms-4 mb-2">
                                                    <strong><%# Eval("TopicName") %></strong><br />
                                                    <div><%# Eval("VideoURL") %></div>
                                                    <div class="text-muted">Duration: <%# Eval("DurationFormatted") %></div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
