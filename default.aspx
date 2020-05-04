<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Based on Function(8) in the article, to calculate the probability(P) of being borrowed for individual item.</h2>
            Total Days of Being Checked-out (Tj)：<asp:TextBox ID="txt_tj" runat="server"></asp:TextBox><br />
            Times of the book being circulated (C)：<asp:TextBox ID="txt_c" runat="server"></asp:TextBox><br>
            Set period for the calculation (T0)：<asp:TextBox ID="txt_t0" runat="server" Text="365"></asp:TextBox><br />
            <asp:Button ID="Button1" runat="server" Text="Calculation" OnClick="Button1_Click" />
             <div>The result of P:<p id ="resultP" runat="server"></p></div>
        </div>

        <h2>Based on Function(11) in the article, to calculate the final assessment score for a collection. Note:Before upload the excel, please format the data as shown in the screenshot.</h2>
        <div style="background:url(pics.png) no-repeat; height:168px;"></div>
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <div style="height:32px; line-height:32px;">Select coefficient(1,10,100,1000,10000)：

        <select id="selectnum" runat="server">
            <option value="1">1</option>
            <option value="10">10</option>
            <option value="100">100</option>
            <option value="1000">1000</option>
            <option value="10000">10000</option>
        </select>;Set period(T<font style="font-size:8px;">0</font>) for calculation is 365 days.<br />
        </div>
        <asp:Button ID="Button2" runat="server" Text="Calculation" OnClick="Button2_Click" />
       <div style="height:32px; line-height:32px;"><br /></div>
        <table border="1">
            <tr>
                <td>Book Title</td>
                <td>Days to lend(Tj)</td>
                <td>Number of loans(C)</td>
                <td>Result(P)</td>
                <td>∑(Pi)</td>
            </tr>
            <asp:Repeater ID="rptlist" runat="server">
                <ItemTemplate>
                    <tbody id="re" runat="server">
                        <tr>
                            <td>{0}</td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                            <td>{4}</td>
                        </tr>
                    </tbody>
                </ItemTemplate>
            </asp:Repeater>
        </table>
           <div>The result of f(p):<p id ="resultfp" runat="server"></p></div>
           <div>The result of f(p) with selected coefficient:<p id ="resultfp2" runat="server"></p></div>
    </form>
</body>
</html>
