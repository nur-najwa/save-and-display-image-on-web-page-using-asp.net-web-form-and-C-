<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            id:<asp:TextBox ID="txtcode" runat="server"></asp:TextBox>
            <asp:Button ID="btnsave" OnClick="btnsave_Click" runat="server" Text="Save" />
            <asp:Button ID="btnshow" OnClick="btnshow_Click" runat="server" Text="Show" />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Image ID="Image1" runat="server" />
        </div>
    </form>
</body>
</html>
