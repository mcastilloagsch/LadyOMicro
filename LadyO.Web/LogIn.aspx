<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="LadyO.Web.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AGSCH - Generación de Credenciales</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta charset="utf-8" />
    <meta name="google-signin-client_id" content="YOUR_CLIENT_ID.apps.googleusercontent.com"/>
    <link href="/CSS/LogIn.css" rel="stylesheet" type="text/css" />
    <link href="/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="icon" type="image/png" href="/Img/Logo.png" />
    <script>
        function onSuccess(googleUser) {
            console.log('Logged in as: ' + googleUser.getBasicProfile().getName());
        }
        function onFailure(error) {
            console.log(error);
        }
        function renderButton() {
            gapi.signin2.render('my-signin2', {
                'scope': 'profile email',
                'width': 320,
                'height': 50,
                'longtitle': true,
                'theme': 'dark',
                'onsuccess': onSuccess,
                'onfailure': onFailure
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <div class="formDiv">
            <div class="imgcontainer">
                <img src="/Img/Logo.png" alt="Avatar" class="avatar" />
            </div>
            <h1>Sistema de Registro Institucional</h1>
            <div id="my-signin2"></div>
            <script src="https://apis.google.com/js/platform.js?onload=renderButton" async defer></script>
        </div>
    </form>
</body>
</html>
