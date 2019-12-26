<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registtest.aspx.cs" Inherits="RegistForm.registtest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="">
    <title>Document</title>
    <script src='https://www.google.com/recaptcha/api.js?render=6Lf6pL0UAAAAAPo6jQwDUh0xSSafY0FPoTt86qRL'></script>
  
</head>
<body>

    <form id="form1" method="post" action="registform.ashx" enctype="multipart/form-data">
        <div>
            <div class="container">
        <div class="logo">
            <img src="img/logo.png" alt="">
        </div>
        <div class="form">
            <div class="form__group">
                <label for="">姓名</label>
                <input type="text" name="name">
            </div>
            <div class="form__group">
                <label for="">Email</label>
                <input type="email" name ="email">
            </div>
            <div class="form__group form__group--warning">
                <label for="">電話</label>
                <input type="text" name ="tel">
            </div>
            <div class="form__group">
                <label for="">上傳履歷（PDF）</label>
                <input type="file" name ="pdf">
            </div>
            <div class="form__group">
                <label for="">自我介紹&目前程式能力</label>
                <textarea name="introduction" id="" rows="3"></textarea>
            </div>
            <div class="form__group">
                <label for="">為什麼想嘗試程式開發？有無考慮其他職涯發展？</label>
                <textarea name="why" id="" rows="3"></textarea>
            </div>
            <div class="form__group">
                <label for="">是否已經嘗試在寫程式，目前有碰到什麼瓶頸？</label>
                <textarea name="essay" id="" rows="3"></textarea>
            </div>
            <div class="row">
                <div class="col-6 form__img">
                    <label class="title" for="">你想加入的組別</label>
                    <div class="form__group form__group--active">
                        <input type="radio" name="joingroup" checked value="前端">
                        <img src="img/class01.png" alt="" >
                        <label for="joingroup">前端組</label>
                    </div>
                    <div class="form__group">
                        <input type="radio" name="joingroup" value="後端">
                        <img src="img/class02.png" alt="">
                        <label for="joingroup">後端組</label>
                    </div>
                </div>
                <div class="col-6 form__img">
                    <label class="title" for="">你想參加的梯次（可複選）</label>
                    <div class="form__group form__group--active">
                        <input type="checkbox" name="batch" checked  value="1">
                        <img src="img/calendar01.png" alt="">
                        <label for="batch">第一梯</label>
                    </div>
                    <div class="form__group">
                        <input type="checkbox" name="batch"  value="2">
                        <img src="img/calendar02.png" alt="" >
                        <label for="batch">第二梯</label>
                    </div>
                </div>
            </div>
            <%--<div class="form__btn">GO!</div>--%>
            <%--<script>
                grecaptcha.ready(function () {
                    console.log('1. grecaptcha.ready');
                    console.log('2. grecaptcha.execute("6Lf6pL0UAAAAAPo6jQwDUh0xSSafY0FPoTt86qRL", { action: "@Url.Action("VerifyBot", "Account")" })');
                    grecaptcha.execute('6Lf6pL0UAAAAAPo6jQwDUh0xSSafY0FPoTt86qRL', { action: '@Url.Action("VerifyBot", "Account")' }).then((token) => {
                        console.log('3. Get token from reCAPTCHA service => ', token);
                        console.log('4. Verifying Bot...');
                        $.ajax({
                            type: 'POST',
                            url: '@Url.Action("VerifyBot", "Account")',
                            data: { token: token },
                            success: (res) => {
                                console.log('5. Return => ', res);
                            }
                        });
                    });
                });
            </script>--%>
           <%-- <script>
                grecaptcha.ready(function () {
                    grecaptcha.execute('reCAPTCHA_site_key', { action: 'homepage' });
                });
            </script>--%>
            <input type="hidden" name="hiddenToken" id="hiddenToken" value="" >
            <script src="https://www.google.com/recaptcha/api.js?render=6Lf6pL0UAAAAAPo6jQwDUh0xSSafY0FPoTt86qRL"></script>
            <script>
                grecaptcha.ready(function () {
                    grecaptcha.execute('6Lf6pL0UAAAAAPo6jQwDUh0xSSafY0FPoTt86qRL', { action: 'social' }).then(function (token) {
                        //console.log(token);
                        document.getElementById("hiddenToken").value = token;
                        const tokenId = document.getElementById("hiddenToken").value;
                        const xhr = new XMLHttpRequest();
                        xhr.open("POST", "verification.ashx");
                        // xhr.setRequestHeader("Content-Type", "application/json");
                        xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"); // 要用 x-www-form-urlencoded 方式傳送
                        xhr.onload = function () {
                           
                            const res = this.responseText;
                            console.log(this.responseText);
                        };
                        xhr.send(`hiddenToken=${tokenId}`);
                        
                    });
                });
            </script>
            <input type="submit" class="form__btn">GO!</input>
        </div>
        <div class="bgItme">
            <img class="man" src="img/man.png" alt="">
            <img class="moon" src="img/moon.png" alt="">
            <img class="rocket" src="img/rocket_cloud01.png" alt="">
        </div>
        <div id="particles-js"></div>
    </div>
        </div>
        
    </form>

</body>

</html>

<%-- 存在session/存在hidden欄位跟post一起送 --%>
