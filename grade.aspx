<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="grade.aspx.cs" Inherits="RegistForm.grade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">評分區</label>
                        <div class="col-lg-10">
                            <asp:Label ID="Label1" runat="server" >決心(0~10)</asp:Label>
                            <input ID="score1" type="number" runat="server" min="0" max="10" step="1"/>
                            <asp:Label ID="Label3" runat="server" >程式能力(0~2)</asp:Label>
                            <input ID="score2" type="number" runat="server" min="0" max="2" step="1"/>
                            <asp:Label ID="Label4" runat="server" >溝通力(0~10)</asp:Label>
                            <input ID="score3" type="number" runat="server" min="0" max="10" step="1"/>
                            <asp:Label ID="Label2" runat="server" >Special Person!</asp:Label>
                            <asp:CheckBox ID="Special" runat="server" />
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">Memo區</label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="memo" runat="server" Height="94px"  TextMode="MultiLine" Width="538px"></asp:TextBox>
                           
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">後續處理紀錄</label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="followup" runat="server" Height="94px"  TextMode="MultiLine" Width="538px"></asp:TextBox>
                           
                        </div>
                    </div>
                  <div class="form-group">
                    <div class="col-lg-offset-2 col-lg-10">
                        
                        <asp:Button ID="tograde" runat="server" Text="完成評分" CssClass="btn btn-success btn-xs" style="margin:20px" OnClick="come_Click"/>

                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
    </div>
</asp:Content>
