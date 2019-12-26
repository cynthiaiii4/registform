<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="candidate.aspx.cs" Inherits="RegistForm.candidate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<%--    <link rel="stylesheet" href="css/all.css">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="cmxform form-horizontal style-form">
    <!-- /row -->
        <div class="row mt">
          <div class="col-lg-12">
              <div class="form-panel">
              <div class="form">
                <form class="cmxform form-horizontal style-form" id="signupForm" method="get" action="">
                  <div class="form-group ">
                    <label for="firstname" class="control-label col-lg-2">姓名</label>
                    <div class="col-lg-10">
                        <asp:Label ID="name" runat="server" ></asp:Label>
                    </div>
                  </div>
                  <div class="form-group ">
                    <label for="lastname" class="control-label col-lg-2">Email</label>
                    <div class="col-lg-10">
                        <asp:Label ID="email" runat="server" ></asp:Label>
                    </div>
                  </div>
                  <div class="form-group ">
                    <label for="username" class="control-label col-lg-2">電話</label>
                    <div class="col-lg-10">
                        <asp:Label ID="tel" runat="server" ></asp:Label>
                    </div>
                  </div>
                    <div class="form-group ">
                        <label for="password" class="control-label col-lg-2">PDF履歷</label>
                        <div class="col-lg-10">
                            <a href="" ID="pdfa" runat="server" target="_blank">
                                <asp:Literal ID="pdf" runat="server"></asp:Literal></a>
                            
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">自我介紹&目前程式能力</label>
                        <div class="col-lg-10">
                            <asp:Label ID="introduction" runat="server" ></asp:Label>
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">為什麼想嘗試程式開發？有無考慮其他職涯發展？</label>
                        <div class="col-lg-10">
                            <asp:Label ID="why" runat="server" ></asp:Label>
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">是否已經嘗試在寫程式，目前有碰到什麼瓶頸？</label>
                        <div class="col-lg-10">
                            <asp:Label ID="essay" runat="server" ></asp:Label>
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">想加入的組別</label>
                        <div class="col-lg-10">
                            <asp:Label ID="joingroup" runat="server" ></asp:Label>
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">想參加的梯次 - 報到日</label>
                        <div class="col-lg-10">
                            <asp:Label ID="batch" runat="server" ></asp:Label>
                        </div>
                    </div>
                  <div class="form-group">
                    <div class="col-lg-offset-2 col-lg-10">
                        
                        <asp:Button ID="come" runat="server" Text="來面試吧!" CssClass="btn btn-success btn-xs" style="margin:20px" OnClick="come_Click"/>
                        <asp:Button ID="byebye" runat="server" Text="有緣再會" CssClass="btn btn-primary btn-xs" OnClick="byebye_Click"/>
                  
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
    </div>
</asp:Content>
