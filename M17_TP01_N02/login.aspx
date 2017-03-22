<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="M17_TP01_N02.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>OPCS &gt; Venda de Equipamentos Informáticos</title>
    <script src="js/jquery-3.1.1.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="http://getbootstrap.com/examples/carousel/carousel.css" />
    <link rel="stylesheet" href="css/style.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body id="login">
    <form id="form1" runat="server">
        <div id="elements">
            <!--<div class="alert-info col-md-4" style="padding: 10px">
	    		<span class="glyphicon glyphicon-home"></span>
	    	</div>
	    	<div class="alert-danger col-md-4" style="padding: 10px">
	    		<span class="glyphicon glyphicon-home"></span>
	    	</div>
	    	<div class="alert-warning col-md-4" style="padding: 10px">
	    		<span class="glyphicon glyphicon-home"></span>
	    	</div>-->
            <div id="header">
                <img src="images/logo.png" height="154" alt="OPCS" />
            </div>
            <div id="body">
                <div class="form-group">
                    <label for="txtUsername">Username ou E-mail</label>
                    <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Email ou Username" TabIndex="1"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassword">Password</label><label class="pull-right"><a href="#">Esqueci-me da senha</a></label>
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" placeholder="Password" TextMode="Password" TabIndex="2"></asp:TextBox>
                </div>
                <div class="checkbox">
                    <label>
                        <input type="checkbox" />
                        Manter sessão iniciada
                    </label>
                </div>
                <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-default pull-right" Text="Login" OnClick="btnLogin_OnClick" />
                <br />
                <br />
                <br />
                <asp:Label runat="server" ID="lblError" CssClass="col-md-12"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
