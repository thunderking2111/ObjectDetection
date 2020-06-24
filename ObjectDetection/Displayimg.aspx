<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Displayimg.aspx.cs" Inherits="ObjectDetection.Displayimg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select the Objects</title>
    <link href="css/jquery.Jcrop.css" rel="stylesheet" />  
    <link href="css/stylesheet.css" rel="stylesheet" type="text/css" />
<script src="js/jquery.min.js"></script>  
<script src="js/jquery.Jcrop.js"></script>  
<script type="text/javascript">  
    var height = 1;
    var width = 2;
    $(document).ready(function () {
        <%--$('#<%=GridView1.ClientID%>').Jcrop({
            onSelect: SelectCropArea
        });--%>
        
        var $target = $('#DispImage');
        $target.on('load', function () {
            height = $(this).height();
            width = $(this).width();
            '<%Session["height"] = "' + height + '"; %>';
            '<%Session["width"] = "'+width+'";%>';
            alert('<%=Session["height"]%>');
        });

        $('#<%=DispImage.ClientID%>').Jcrop({
            onSelect: SelectCropArea
        });
        

    });
    
    $('#btn_add_another').click(function () {
        $('#<%=DispImage.ClientID%>').Jcrop({
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <div class="getImage">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Show Image" OnClick="Button1_Click" />
            </div>
            <div class="navigation_Buttons">
                 <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>--%>
                            <asp:Button runat="server" CssClass="nav_buttons" ID="btn_previous" Text="Previous" OnClick="Load_Previous_Image"></asp:Button>
                            <asp:Button runat="server" CssClass="nav_buttons" ID="btn_add_another" Text="Add One More" OnClick="Add_Another_Data"></asp:Button>
                            <asp:Button runat="server" CssClass="nav_buttons" ID="btn_done" Text="Done" OnClick="Add_Data_To_Database"></asp:Button>
                            <asp:Button runat="server" CssClass="nav_buttons" ID="btn_next" Text="Next" OnClick="Load_Next_Image" onMouseDown="SelectCropArea"></asp:Button> 
                            <asp:Button runat="server" CssClass="nav_buttons" ID="Button2" Text="Next" OnClick="Button2_Click" ></asp:Button> 
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
            </div>
            <br />
            <br />
            <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                <div class="imagebox">
                        <asp:Image ID="DispImage" CssClass="center-fit" runat="server" />
                </div>
                <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_done" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btn_previous" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btn_add_another" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btn_next" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>--%>
            
            
                 <table > 

                    <tr>
                       <td> <asp:HiddenField ID="X" runat="server" /> 
                            <asp:HiddenField ID="Y" runat="server" />  
                            <asp:HiddenField ID="W" runat="server" />  
                            <asp:HiddenField ID="H" runat="server" />
                       </td> 
                    </tr>
                 </table>
        </div>
        
    <p>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="0">Select Lable for Object</asp:ListItem>
                    <asp:ListItem Value="1">Cat</asp:ListItem>
                    <asp:ListItem Value="2">Dog</asp:ListItem>
                </asp:DropDownList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="NewLabelAdd" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="NewLabelBox" runat="server" placeholder="Add New Label"></asp:TextBox>
                <asp:Button ID="NewLabelAdd" runat="server" Text="ADD" OnClick="Add_New_Label" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
       
    </p>


</form>
    
</body>
</html>
