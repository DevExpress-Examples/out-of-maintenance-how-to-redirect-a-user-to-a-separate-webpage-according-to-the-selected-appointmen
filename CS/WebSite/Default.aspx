<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v11.1.Core, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" ClientInstanceName="scheduler" 
                OnAppointmentInserting="ASPxScheduler1_AppointmentInserting" OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing">
            </dxwschs:ASPxScheduler>
        </div>
        <asp:ObjectDataSource ID="appointmentDataSource" runat="server" DataObjectTypeName="CustomEvent"
            TypeName="CustomEventDataSource" DeleteMethod="DeleteMethodHandler" SelectMethod="SelectMethodHandler"
            InsertMethod="InsertMethodHandler" UpdateMethod="UpdateMethodHandler" OnObjectCreated="appointmentsDataSource_ObjectCreated">
        </asp:ObjectDataSource>
    </form>

    <script type="text/javascript">
        function AppointmentMenuHandler(scheduler, s, args) {
            if (args.item.GetItemCount() <= 0) {
                if (args.item.name == "RedirectItem")
                    window.location = 'NewPage.aspx?aptId=' + scheduler.GetSelectedAppointmentIds()[0];
                else
                    scheduler.RaiseCallback("MNUAPT|" + args.item.name);
            }
        }
    </script>

</body>
</html>
