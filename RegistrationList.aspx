<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="RegistrationList.aspx.cs" Inherits="RegistForm.login" %>
<%@ Register Src="~/PageControl.ascx" TagPrefix="uc1" TagName="PageControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="PageControl.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
   
    <div class="row mt" id="table" runat="server">
        <div class="col-md-12">
            <div class="content-panel">
                <h4><i class="fa fa-angle-right"></i>報名列表</h4>
                <label>起始時間</label>
                <input id="timeStart" class="input-medium default-date-picker" size="16" type="text" value="" placeholder="請輸入開始時間" runat="server" ClientIDMode="Static"/>
                <script >document.querySelector("#timeStart").setAttribute("type", "date")</script>
                <label>結束時間</label>
                <input id="timeEnd" class="input-medium default-date-picker" size="16" type="text" value="" placeholder="請輸入結束時間" runat="server" ClientIDMode="Static"/>
                <script >document.querySelector("#timeEnd").setAttribute("type", "date")
                </script>
                <label>前後端</label>
                <asp:DropDownList ID="joingroup" runat="server">
                    <asp:ListItem Value="0">請選擇前後端</asp:ListItem>
                    <asp:ListItem Value="前端">前端</asp:ListItem>
                    <asp:ListItem Value="後端">後端</asp:ListItem>
                </asp:DropDownList>
                <label>梯次</label>
                <asp:DropDownList ID="batch" runat="server">
                    <asp:ListItem Value="0">請選擇梯次</asp:ListItem>
                    <asp:ListItem Value="1">第一梯</asp:ListItem>
                    <asp:ListItem Value="2">第二梯</asp:ListItem>
                </asp:DropDownList>
                <label>關鍵字搜尋</label>
                <input type="text" placeholder="請輸入搜尋關鍵字" id="keyword" runat="server"/>
                <asp:Button ID="search" runat="server" Text="搜尋" OnClick="search_Click" />
                <asp:Button ID="clear" runat="server" Text="清除搜尋條件" OnClick="clear_Click" />
                
                <hr/>

                <asp:GridView ID="GridView1" CssClass="table table-striped table-advance table-hover" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" GridLines="None" OnRowDataBound="GridView1_RowDataBound">
                    <Columns >
                        <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                        <asp:BoundField DataField="joingroup" HeaderText="組別" SortExpression="joingroup" />
                        <asp:BoundField DataField="batch" HeaderText="梯次" SortExpression="batch" />
                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                        <asp:BoundField DataField="status" HeaderText="狀態" SortExpression="status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Button1" CssClass="btn btn-success btn-xs" runat="server" CommandName="edit" Text="詳細資料" />
                                <input type="button" value="寄信" Class="btn btn-primary btn-xs getEmail" id="sentEmail" runat="server"/>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("id") %>'/>
                                <asp:Label ID="getEmail" name="getEmail" runat="server" Text='<%# Eval("sent") %>' ></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                        
                    </Columns>
                </asp:GridView>
                <input type="button" id="Hi" runat="server"/>
            </div>
           
        </div>
       
    </div>
    <uc1:PageControl runat="server" ID="PageControl" />
    <script>
        var allbtn = document.querySelectorAll('.getEmail')
        for (let i = 0; i < allbtn.length; i++) {
            allbtn[i].addEventListener("click", function (e) {
                let thebtn=e.target
                let hiddeninput=e.target.nextElementSibling
                let spanformark = e.target.nextElementSibling.nextElementSibling
                console.log(hiddeninput.value)

            var data = `id=${hiddeninput.value}`;
            var xhr = new XMLHttpRequest();
            xhr.addEventListener("readystatechange", function () {
                if (this.readyState === 4) {
                    console.log(this.responseText)
                    spanformark.textContent = this.responseText
                    thebtn.style="display:none"
                }
            });
            xhr.open("POST", "sent.ashx");
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.send(data);

        })
        }
    </script>
    
</asp:Content>
