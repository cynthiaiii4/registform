<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="addmember.aspx.cs" Inherits="RegistForm.addmember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <h3><i class="fa fa-angle-right"></i> 新增帳號</h3>
    <div class="cmxform form-horizontal style-form">
    <!-- /row -->
        <div class="row mt">
          <div class="col-lg-12">
              <div class="form-panel">
              <div class="form">
                <form class="cmxform form-horizontal style-form" id="signupForm" method="get" action="">
                  <div class="form-group ">
                    <label for="firstname" class="control-label col-lg-2">帳號</label>
                    <div class="col-lg-10">
                      <input class=" form-control" id="UserID" name="UserID" type="text" runat="server" required="" oninvalid="setCustomValidity('請填入帳號')" oninput="setCustomValidity('')"/>
                    </div>
                  </div>
                  <div class="form-group ">
                    <label for="lastname" class="control-label col-lg-2">密碼</label>
                    <div class="col-lg-10">
                      <input class=" form-control" id="Pwd"  type="password" runat="server" required="" oninvalid="setCustomValidity('請填入密碼')" oninput="setCustomValidity('')" ClientIDMode="Static"/>
                    </div>
                  </div>
                    <div class="form-group ">
                    <label for="lastname" class="control-label col-lg-2">請再次確認密碼</label>
                    <div class="col-lg-10">
                    <input class=" form-control" id="PwdCheck" name="PW" type="password" runat="server" required="" oninvalid="setCustomValidity('請再次確認密碼')" oninput="check(this)" />
                    </div>
                        <%--<asp:Label ID="PWmessage" runat="server" Text="123" ForeColor="red" CssClass="tip" >兩次輸入的密碼不一致</asp:Label>--%>
                        <%--<asp:Literal ID="PWmessage" runat="server"></asp:Literal>--%>
                        <script language='javascript' type='text/javascript'>
                            function check(input) {
                                console.log(document.getElementById('PW').value);
                                if (input.value != document.getElementById('PW').value) {
                                    input.setCustomValidity('請重新確認密碼.');
                                } else {
                                    // input is valid -- reset the error message
                                    input.setCustomValidity('');
                                }
                            }
                        </script>
                        
                  </div>
                    
                  <div class="form-group ">
                    <label for="username" class="control-label col-lg-2">姓名</label>
                    <div class="col-lg-10">
                      <input class="form-control " id="UserName" name="UserName" type="text" runat="server" required="" oninvalid="setCustomValidity('請填入姓名')" oninput="setCustomValidity('')"/>
                    </div>
                  </div>
                    <div class="form-group ">
                        <label for="Profile" class="control-label col-lg-2">照片</label>
                        <div class="col-lg-10">
                            <asp:FileUpload ID="RocketProfile" runat="server" class="form-control "/>
                            <asp:Label ID="Message" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="confirm_password" class="control-label col-lg-2">權限</label>
                        <div class="col-lg-10">
                            
                            <asp:CheckBoxList ID="CheckBoxList" runat="server">
                                <asp:ListItem Value="001">帳號權限</asp:ListItem>
                                <asp:ListItem Value="002">RegistrationList報名清單</asp:ListItem>
                                <asp:ListItem Value="003">Interview面試清單</asp:ListItem>
                                <asp:ListItem Value="004">AdmissionList錄取清單</asp:ListItem>

                            </asp:CheckBoxList>
                        </div>
                    </div>
                  
                  <div class="form-group">
                    <div class="col-lg-offset-2 col-lg-10">
                        <asp:Label ID="check" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Button CssClass="btn btn-theme" ID="save" runat="server" Text="新增" OnClick="save_Click" />
                        <asp:Button CssClass="btn btn-theme04" ID="cancel" runat="server" Text="取消" OnClick="cancel_Click"/>
                  
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
    </div>
    <script type="text/javascript">
        //嘗試一
        //function equalto() {
        //    PW1 = document.getElementById("PW");
        //    PW2 = document.getElementById("PWCheck");
        //    if (PW1.value != PW2.value) {
        //        document.getElementById("PWmessage").innerText = "密碼不一致";
        //    } else { document.getElementById("PWmessage").innerText = ""; }
        //}
        //嘗試二
        //var save = document.getElementById("save");
        //save.addEventListener("click", function() {
        //    PW1 = document.getElementById("PW");
        //    alert("11");
        //    PW2 = document.getElementById("PWcheck");
        //    if (PW1.value != PW2.value) {
        //        document.getElementById("PWmessage").innerText = "密碼不一致";
        //    } else { document.getElementById("PWmessage").innerText = ""; }
        //});
        //嘗試三
        //$(".tip").hide();
        //function checkpas1() {//當第一個密碼框失去焦點時，觸發checkpas1事件
        //    var pas1 = document.getElementById("password").value;
        //    var pas2 = document.getElementById("repassword").value;//獲取兩個密碼框的值
        //    if (pas1 != pas2 && pas2 != "")//此事件當兩個密碼不相等且第二個密碼是空的時候會顯示錯誤資訊
        //        $(".tip").show();
        //    else
        //        $(".tip").hide();//若兩次輸入的密碼相等且都不為空時，不顯示錯誤資訊。
        //}
        //function checkpas() {//當第一個密碼框失去焦點時，觸發checkpas2件
        //    var pas1 = document.getElementById("password").value;
        //    var pas2 = document.getElementById("repassword").value;//獲取兩個密碼框的值
        //    if (pas1 != pas2) {
        //        $(".tip").show();//當兩個密碼不相等時則顯示錯誤資訊
        //    } else {
        //        $(".tip").hide();
        //    }
        //}
    </script>
</asp:Content>
