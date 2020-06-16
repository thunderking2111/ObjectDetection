 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageUpload.aspx.cs" Inherits="ObjectDetection.ImageUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" />

        </div>
        <p>
            <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Save" Width="59px" />
        </p>
        <p>
            &nbsp;</p>
        <br />
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <img src= 'data/<%#Eval("name") %>' alt="" height="150" />
            </ItemTemplate>
          
        </asp:Repeater>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" />
          <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="true">
           

          </asp:GridView>
        

    </form>
</body>
</html>
