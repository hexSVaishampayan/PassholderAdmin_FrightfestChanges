<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="weatherforecast.aspx.vb" Inherits="PassholderAdmin.weatherforecast" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        img.wicon {
                width: 25px;
               }
            body {
                font-family:Arial, sans-serif;
            }
        table.weatherreport {
            border-collapse: collapse;
            font-family: sans-serif;
            font-size: 9pt;
            width: 100%;
            max-width: 500px;
            margin-left: auto;
            margin-right: auto;
        }

        td.parkname.wetd {
            min-width: 90px;
            white-space: nowrap;
            padding-left: 5px;
        }

        td.attclosed {
            opacity: .5;
            background: #e4e4e4;
            filter: saturate(0);
        }

            tr.werow {
            border-bottom: 1px solid #aaa;
        }
        .tempdesc {
            font-size: 6pt;
            margin: 0px;
            margin-top: -7px;
            padding: 3px;
            display: block;
            border-radius: 4px;
            margin: 0px;
            padding-bottom: 1px;
            padding-top: 1px;
            margin-top: -9px;
            width: 23px;
            margin-left: auto;
            margin-right: auto;
        }

        th {
            text-align: center;
            border-bottom: 1px solid black;
            background: #fff;
            padding-top: 3px;
            font-size: 8pt;
            padding-bottom: 3px;
        }


        td.werow {
            text-align: center;
            width: 37px;
            border-left: 1px solid #aaa;
            vertical-align: middle;
            padding-bottom: 4px;
            padding-top: 0px;
        }

        td.werow.attopen.lightrain {background: #d37272;}
        td.werow.attopen.rain {background: #950b0b;color: white;}
        td.werow.attopen.heavyrain {background: #000000;color: white;}

        td.werow.attopen.flurries {background: #faafaf;}
        td.werow.attopen.lightsnow {background: #d37272; color:white;}
        td.werow.attopen.snow {background: #950b0b;color: white;}
        td.werow.attopen.heavysnow {background: #000000;color: white;}

        .attopen .tempdesc.superfreezing {
            background: #0057d9;
            color: white;
        }
        .attopen .tempdesc.freezing {
            background: #5398fff2;
            color: white;
        }
        .attopen .tempdesc.verycold {
            background: #98c1ff;
            color: black;
        }
        .attopen .tempdesc.cold {
            background: #cde1ff;
            color: black;
        }
        .attopen .tempdesc.chilly {
            background: #e3eeff;
            color: black;
        }
        .attopen .tempdesc.veryhot {background: #0070f9b0;color: white;}
        .attopen .tempdesc.hot {background: #0070f9b0;color: white;}
        .attopen .tempdesc.prettyhot {background: #0070f9b0;color: white;}

        h1 {
            text-align: center;
            font-family: sans-serif;
            font-size: 17pt;
            margin-bottom: 6px;
        }
        h2 {
            text-align: center;
            margin-top: -4px;
            font-size: 12pt;
            font-weight: normal;
        }
        ul {
            max-width: 500px;
            margin-left: auto;
            margin-right: auto;
            font-family: sans-serif;
            font-size: 10pt;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="tooltipster/css/tooltipster.bundle.min.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.0.min.js"></script>
    <script type="text/javascript" src="tooltipster/js/tooltipster.bundle.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <h1>Weather Forecast</h1>
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </div>

    <div class="instructions">
        <ul>
            <li>Forecasts are updated at 12AM, 6AM, 12PM, and 6PM CST.</li>
            <li>Hover over or tap each cell for a summary.</li>
            <li>Gray cells indicate the park is scheduled to be closed.</li>
            <li>Red or black cells indicate precipitation severity.</li>
            <li>Extreme hot and cold temperatures are highlighted.</li>
            <li>Forecasts provided by DarkSky.</li>
        </ul>

    </div>
    <script>
        $(document).ready(function() {
            $('.tooltip').tooltipster({
                trigger: 'hover',
                theme: 'tooltipster-punk',
                contentAsHTML: true,
                animation: 'fade',
                delayTouch: [100, 5000],
                distance:0
                
            });
        });
    </script>
</asp:Content>

