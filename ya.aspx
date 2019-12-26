<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ya.aspx.cs" Inherits="RegistForm.ya" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">

    <title>六角學院｜火箭隊軟體工程師培訓營 報名表單</title>

    <!-- <meta content="width=device-width, initial-scale=1.0" name="viewport"> -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">

    <meta property="og:title" content="六角學院｜火箭隊軟體工程師培訓營">
    <meta property="og:description" content="火箭隊軟體工程師培訓計畫｜與您一同搭造火箭，讓您的人生一飛沖天">
    <meta property="og:image" content="https://register.rocket-coding.com/img/fb.jpg">

    <meta property="fb:app_id" content="113926539963626">
    <meta property="og:site_name" content="六角學院｜火箭隊軟體工程師培訓營">
    <meta property="og:locale" content="zh_TW">
    <meta property="og:url" content="https://register.rocket-coding.com/">
    <meta property="og:type" content="website">
    <link rel="icon" href="img/favicon.png">

    <meta content="https://register.rocket-coding.com/img/fb.png" name="image">
    <meta content="火箭隊軟體工程師培訓計畫｜與您一同搭造火箭，讓您的人生一飛沖天" name="description">
    <meta content="六角學院,火箭隊培訓營, 網頁設計, 前端培訓營,後端培訓營, HTML, CSS, jQuery, javascript" name="keywords">
    <meta content="六角學院" name="author">

    <link rel="stylesheet" href="css/all.css?1081021_v5">
</head>
<body>
    <%--<div class="loader">
        <div class="loader_img">
            <img class="logo_img" src="img/logo01.png" alt="">
            <img class="logo_word" src="img/logo02.png" alt="">
        </div>
    </div>--%>
    <div class="container">
        <div class="logo">
            <img src="img/logo.png" alt="">
            <h1><span >｛ 培訓營</span><span style="color: aquamarine"> 目前報名人數</span> <span> ｝</span></h1>
        </div>
        <div class="bgItme">
            <img class="man" src="img/man.png" alt="">
            <!-- <img class="moon" src="img/moon.png" alt=""> -->
            <img class="rocket" src="img/rocket.png" alt="">
        </div>
        <div class="form">
            <form action="" method="post" target="hide_iframe">
                <div class="form__group">
                    <asp:Label ID="number" runat="server"  style="font-size: 110px; display: block; text-align: center; color: coral"></asp:Label><br>
                    <asp:Label ID="Label2" Text="Label" style="font-size: 50px; display: block; text-align: center;">位火箭人</asp:Label>
                </div>
               
            </form>
            <iframe id="hide_iframe" name="hide_iframe" style="display:none;"></iframe>
        </div>
        <div id="particles-js"></div>
    </div>

</body>
<script src="js/particles.min.js"></script>
<script src="js/star.js"></script>
<script src="js/script.js?1081021"></script>
</html>
