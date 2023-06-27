<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="MemberTest.aspx.vb" Inherits="PassholderAdmin.MemberTest" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        #form1 {
            background: #959595;
        }

        .pagecontent {
            padding-top: 40px;
            width: 90%;
            margin-left: auto;
            margin-right: auto;
            background: white;
            max-width: 1050px;
            margin-left: auto;
            margin-right: auto;
            padding: 30px;
            box-shadow: 0px 0px 10px -4px #000;
            padding-left: 37px;
        }
        h1 {
            font-size: 20pt!important;
            margin-top: 30px!important;
        }

.displayPage table td label {
    font-size: 14pt!important;
    margin-bottom: 12px;
    display: inline-block;
    margin-left: 7px!important;
    background: #f4f4f4;
    padding: 4px!important;
    padding-bottom: 3px!important;
    padding-right: 10px!important;
}
        input#ContentPlaceHolder1_cmdCheckOrder,
        input#ContentPlaceHolder1_cmdLookupIP,
        input#ContentPlaceHolder1_cmdSubmit {
            border: none;
            padding: 13px;
            padding-left: 20px;
            padding-right: 20px;
            background: #0067b8;
            color: white;
            box-shadow: 0px 2px 8px -3px #000;
        }

        input#ContentPlaceHolder1_cmdSubmit:hover {
            box-shadow: 0px 2px 10px -3px #000;
            background: #005191;
        }

        input#ContentPlaceHolder1_cmdSubmit:active {
            box-shadow: none;
            background: #000000;
        }

        div#ContentPlaceHolder1_pnlResult {
            background: #fff400;
            padding: 20px;
            box-shadow: 0px 0px 10px -3px #000;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlForm" runat="server">
        <div class="pagecontent">
        <asp:Panel ID="pnlResult" runat="server" Visible="false">
            <asp:Label ID="lblResult" runat="server" Text="Guests associated with the IP address 111.111.111.111 will now see the $5 Promotional Credit offer after they clear their cookies."></asp:Label>
        </asp:Panel>

        <h1>Change Member Test Offer</h1>
        <p>The June 2019 Membership Test program has eight different offers which are randomly assigned to guests.
            To keep people from hacking offers and to ensure we can track who received each offer, each offer code is
            assigned based on the guests IP address.</p>
        <h3>If You Need to Change a Guest Offer</h3>
        <p>To change a guest offer, follow these instructions:</p>
        <ol><li><b>Have the geust give you their IP address.</b> This is easier than it sounds. Have the guest go into their browser 
            and type "My IP" in the address bar. The top of the screen will say "Your public IP address" and display a number.</li>
            <li>Enter their number in the form below, and select the offer you want them to have.</li>
            <li>Have them visit the page "https://sixflags.com/clearcookies". This will remove the cookies from their machine. </li>
            <li>Have the guest visit the sales page again.</li>
        </ol>

        <h3>What is the guest's IP Address?</h3>
        <p><asp:TextBox ID="fldIPAddress" runat="server"></asp:TextBox></p>

        <h3>Which offer should the guest have?</h3>
        <p><asp:RadioButtonList ID="fldBenefit" runat="server">
            <asp:ListItem Value="1">Golden Ticket</asp:ListItem>
            <asp:ListItem Value="2">Holiday Bundle</asp:ListItem>
            <asp:ListItem Value="3">No Fees</asp:ListItem>
            <asp:ListItem Value="4">Platinum Upgrade</asp:ListItem>
            <asp:ListItem Value="5">2019 Drink Bottle</asp:ListItem>
            <asp:ListItem Value="6">$5 promotional credit</asp:ListItem>
            <asp:ListItem Value="7">$10 promotional credit</asp:ListItem>
            <asp:ListItem Value="8">$20 promotional credit</asp:ListItem>
            </asp:RadioButtonList></p>

        <p><asp:Button ID="cmdSubmit" runat="server" Text="Submit Change" /></p>


        <h1>View Order Offer</h1>
            <p>If a guest has made a purchase you can use this tool to look up what their offer was by entering their order ID:</p>

        <h3>What is their Order ID?</h3>
        <p><asp:TextBox ID="fldAccessoOrderID" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="cmdCheckOrder" runat="server" Text="Check Offer" /></p>


        <h1>View IP Address Offer</h1>
            <p>You can use this to find out what offer was given to a particular IP address. You will need to
                get the guest's IP address -- usually they can find this by typing "My IP" into their address bar.
            </p>

        <h3>What is the guest's IP address</h3>
        <p><asp:TextBox ID="fldIPLookup" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="cmdLookupIP" runat="server" Text="Lookup IP" /></p>



        </div>
    </asp:Panel>
    <asp:Panel ID="pnlError" runat="server">
        <br />
        <br />
        <br />
        <p>You do not have access to this tool. Permission 515 required.</p>
    </asp:Panel>
</asp:Content>
