<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Interview.aspx.cs" Inherits="RegistForm.Interview" %>
<%@ Register Src="~/PageControl.ascx" TagPrefix="uc1" TagName="PageControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="PageControl.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <%--<a href="AddORID.aspx" class="btn btn-success bg-theme05 mt">新增ORID</a>--%>
    <div class="row mt" id="table" runat="server">
        <div class="col-md-12">
            <div class="content-panel">
                <h4><i class="fa fa-angle-right"></i>面試名單</h4>
                <label style="margin: 0px 0px 0px 20px;">請選擇前後端</label>
                <asp:DropDownList ID="joingroup" runat="server">
                    <asp:ListItem Value="0">請選擇梯次</asp:ListItem>
                    <asp:ListItem Value="前端">前端</asp:ListItem>
                    <asp:ListItem Value="後端">後端</asp:ListItem>
                </asp:DropDownList>
                <label style="margin: 0px 0px 0px 20px;">請選擇梯次</label>
                <asp:DropDownList ID="batch" runat="server">
                    <asp:ListItem Value="0">請選擇梯次</asp:ListItem>
                    <asp:ListItem Value="1">第一梯</asp:ListItem>
                    <asp:ListItem Value="2">第二梯</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="search" runat="server" Text="搜尋" OnClick="search_Click" />
                <asp:Button ID="clear" runat="server" Text="清除搜尋條件" OnClick="clear_Click" />
                <asp:Button ID="sort" runat="server" Text="按照總分排序" OnClick="sort_Click"/>
                <hr/>

                <asp:GridView ID="GridView1" CssClass="table table-striped table-advance table-hover" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  GridLines="None" OnRowEditing="GridView1_RowEditing">
                    <Columns >
                       <%-- 表格ID--%>
                       <%-- <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />--%>
                       <%-- 表格內容--%>
                        
                        <asp:TemplateField HeaderText="Name" SortExpression="name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2"  ForeColor="red" runat="server" Text='<%# Bind("justin_love") %>'></asp:Label>
                                <asp:Label ID="Label3" ForeColor="red" runat="server" Text='<%# Bind("gonsakon_love") %>'></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="joingroup" HeaderText="組別" SortExpression="joingroup" />
                        <asp:BoundField DataField="batch" HeaderText="梯次" SortExpression="batch" />
                        <asp:BoundField DataField="gonsakon_score1" HeaderText="決心1" SortExpression="gonsakon_score1" />
                        <asp:BoundField DataField="gonsakon_score2" HeaderText="程式1" SortExpression="gonsakon_score2" />
                        <asp:BoundField DataField="gonsakon_score3" HeaderText="溝通1" SortExpression="gonsakon_score3" />
                        
                        <asp:BoundField DataField="justin_score1" HeaderText="決心2" SortExpression="justin_score1" />
                        <asp:BoundField DataField="justin_score2" HeaderText="程式2" SortExpression="justin_score2" />
                        <asp:BoundField DataField="justin_score3" HeaderText="溝通2" SortExpression="justin_score3" />
                        
                        <asp:BoundField DataField="memo" HeaderText="備註" SortExpression="memo" HtmlEncode="False" />
                        <asp:BoundField DataField="followup" HeaderText="後續" SortExpression="followup" />
                        <asp:BoundField DataField="total" HeaderText="總分" SortExpression="total" />
                        <asp:TemplateField>
                            
                            <ItemTemplate>

                                <asp:Button ID="Button1" CssClass="btn btn-success btn-xs" runat="server" CommandName="edit" Text="評分GO!" />
                                
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                        
                    </Columns>
                </asp:GridView>
            </div>
            <!-- /content-panel -->
        </div>
        <!-- /col-md-12 -->
    </div>
    <uc1:PageControl runat="server" ID="PageControl" />
    
    
</asp:Content>
