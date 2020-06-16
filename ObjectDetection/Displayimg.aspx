<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Displayimg.aspx.cs" Inherits="ObjectDetection.Displayimg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="css/jquery.Jcrop.css" rel="stylesheet" />  
<script src="js/jquery.min.js"></script>  
<script src="js/jquery.Jcrop.js"></script>  
<script type="text/javascript">  
    $(document).ready(function () {
        $('#<%=GridView1.ClientID%>').Jcrop({
            onSelect: SelectCropArea
        });
    });

    function SelectCropArea(c) {
        $('#<%=X.ClientID%>').val(parseInt(c.x));
        $('#<%=Y.ClientID%>').val(parseInt(c.y));
        $('#<%=W.ClientID%>').val(parseInt(c.w));
        $('#<%=H.ClientID%>').val(parseInt(c.h));
    }  
</script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
             <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Show Image" OnClick="Button1_Click" />
            
             
          <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="true">
              
            
           
                    
              <Columns>
                      
                  <asp:ImageField DataImageUrlField="ImgPath"  ControlStyle-Height="300" ControlStyle-Width="200" ></asp:ImageField>
            
                 
              </Columns>
                
          

          </asp:GridView>
             <table > 
                <tr>
                   <td><asp:HiddenField ID="X" runat="server" /> 
                    <asp:HiddenField ID="Y" runat="server" />  
                    <asp:HiddenField ID="W" runat="server" />  
                    <asp:HiddenField ID="H" runat="server" />
                       </td> 
                    </tr>
             </table>
           
        </div>
   
     
    <p>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="0">Select Lable for Image</asp:ListItem>
          
        </asp:DropDownList>
        <br />
        <br />
       Enter text to add:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <br />

        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
        <br />
        <br />
        <asp:Button runat="server" Text="Done" OnClick="Unnamed1_Click"></asp:Button>
    </p>


</form>
    
</body>
</html>
