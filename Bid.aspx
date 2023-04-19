<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bid.aspx.cs" Inherits="Lab4.Bid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .auto-style2 {
        width: 210px;
    }
    .auto-style7 {
        width: 961px;
        margin-right: 2px;
    }
    .auto-style12 {
        width: 210px;
        height: 29px;
    }
    .auto-style18 {
        width: 194px;
    }
    .auto-style19 {
        width: 194px;
        height: 29px;
    }
    .auto-style20 {
        width: 197px;
    }
    .auto-style21 {
        width: 197px;
        height: 29px;
    }
    .auto-style22 {
        width: 224px;
    }
    .auto-style23 {
        width: 224px;
        height: 29px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    <br />
            <table class="auto-style7">
                <tr>
                    <td class="auto-style22">Bidder:</td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="201px" >
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style18">
                        &nbsp;</td>
                    <td class="auto-style20">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style23">Item name:</td>
                    <td class="auto-style12">
                        <asp:DropDownList ID="DropDownList2" runat="server" Height="19px" Width="202px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style19"></td>
                    <td class="auto-style21">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style23">Item Description:</td>
                    <td class="auto-style12">
                        <asp:TextBox ID="TextBox1" runat="server" Width="203px"></asp:TextBox>
                    </td>
                    <td class="auto-style19">Seller:</td>
                    <td class="auto-style21">
                        <asp:TextBox ID="TextBox7" runat="server" Width="220px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style22">Current Price:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TextBox2" runat="server" Width="135px"></asp:TextBox>
                    </td>
                    <td class="auto-style18">Minimum Bid Increment:</td>
                    <td class="auto-style20">
                        <asp:TextBox ID="TextBox8" runat="server" Width="142px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style22">End date time:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TextBox3" runat="server" Width="206px"></asp:TextBox>
                    </td>
                    <td class="auto-style18">
                        &nbsp;</td>
                    <td class="auto-style20">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Bid Date Time:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TextBox4" runat="server" Width="205px"></asp:TextBox>
                    </td>
                    <td class="auto-style18">
                        &nbsp;</td>
                    <td class="auto-style20">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style23">Time Remaining (dd, hh:mm):</td>
                    <td class="auto-style12">
                        <asp:TextBox ID="TextBox5" runat="server" Width="204px"></asp:TextBox>
                    </td>
                    <td class="auto-style19">
                        &nbsp;</td>
                    <td class="auto-style21">
                    </td>
                </tr>
                <tr>
                    <td class="auto-style22">
                        Bid Price:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TextBox6" runat="server" Width="203px"></asp:TextBox>
                    </td>
                    <td class="auto-style18">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox6" ErrorMessage="Bid price must be a double!"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style20">
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox6" ErrorMessage="Bid price must be &gt; 0" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style22">
                        &nbsp;</td>
                    <td class="auto-style2">
                        <asp:Button ID="Button2" runat="server" Text="Bid" Width="103px" OnClick="Button2_Click" />
                    </td>
                    <td class="auto-style18">
                        <asp:Button ID="Button3" runat="server" Text="Cancel" Width="105px" OnClick="Button3_Click" />
                    </td>
                    <td class="auto-style20">&nbsp;</td>
                </tr>
            </table>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
</asp:Content>
