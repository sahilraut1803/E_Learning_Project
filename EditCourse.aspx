<%@ Page Title="Edit Course" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="E_Learning_Project.EditCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="d-flex justify-content-center">
            <div class="w-100" style="max-width: 800px;">
                <h2 class="text-center">Edit Course</h2>
                <asp:Panel ID="pnlEdit" runat="server" CssClass="mt-4">

                    <asp:HiddenField ID="hfCourseID" runat="server" />

                    <div class="mb-3">
                        <label>Course Name</label>
                        <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label>Thumbnail URL</label>
                        <asp:TextBox ID="txtThumbnail" runat="server" CssClass="form-control" />
                    </div>

                    <asp:Repeater ID="rptSubCourses" runat="server">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfSubCourseID" runat="server" Value='<%# Eval("SubCourseID") %>' />
                            <h5 class="mt-4">SubCourse</h5>
                            <div class="mb-2">
                                <label>Name</label>
                                <asp:TextBox ID="txtSubCourseName" runat="server" CssClass="form-control" Text='<%# Eval("SubCourseName") %>' />
                            </div>
                            <div class="mb-2">
                                <label>Price</label>
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Text='<%# Eval("Price") %>' />
                            </div>
                            <div class="mb-2">
                                <label>Thumbnail</label>
                                <asp:TextBox ID="txtSubThumbnail" runat="server" CssClass="form-control" Text='<%# Eval("Thumbnail") %>' />
                            </div>

                            <asp:Repeater ID="rptTopics" runat="server" DataSource='<%# Eval("Topics") %>'>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfTopicID" runat="server" Value='<%# Eval("TopicID") %>' />
                                    <div class="mt-3 border p-2 rounded bg-light">
                                        <h6>Topic</h6>
                                        <div class="mb-2">
                                            <label>Name</label>
                                            <asp:TextBox ID="txtTopicName" runat="server" CssClass="form-control" Text='<%# Eval("TopicName") %>' />
                                        </div>
                                        <div class="mb-2">
                                            <label>Video URL</label>
                                            <asp:TextBox ID="txtVideoURL" runat="server" CssClass="form-control" Text='<%# Eval("VideoURL") %>' />
                                        </div>
                                        <div class="mb-2">
                                            <label>Duration (Seconds)</label>
                                            <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" Text='<%# Eval("DurationSeconds") %>' />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="d-flex justify-content-center mt-4">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success me-2" OnClick="btnUpdate_Click" />
                        <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="CourseList.aspx" CssClass="btn btn-secondary">Back to List</asp:HyperLink>
                    </div>

                    <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-3 d-block text-center" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
