<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128547638/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4133)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomEvents.cs](./CS/WebSite/App_Code/CustomEvents.cs) (VB: [CustomEvents.vb](./VB/WebSite/App_Code/CustomEvents.vb))
* [Helpers.cs](./CS/WebSite/App_Code/Helpers.cs) (VB: [Helpers.vb](./VB/WebSite/App_Code/Helpers.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
* [NewPage.aspx](./CS/WebSite/NewPage.aspx) (VB: [NewPage.aspx](./VB/WebSite/NewPage.aspx))
* [NewPage.aspx.cs](./CS/WebSite/NewPage.aspx.cs) (VB: [NewPage.aspx.vb](./VB/WebSite/NewPage.aspx.vb))
<!-- default file list end -->
# How to redirect a user to a separate webpage, according to the selected appointment


<p>This example illustrates how to redirect an end user to a separate webpage when the custom appointment menu item is clicked. Note that the technique from the <a href="https://www.devexpress.com/Support/Center/p/E291">How to change default menu items and actions in the popup menu</a> code example is used to add a custom menu action in the following manner:<br />
</p>

```cs
    protected void ASPxScheduler1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {
        if (e.Menu.Id.Equals(SchedulerMenuItemId.AppointmentMenu)) {
            MenuItem item = new MenuItem("Redirect", "RedirectItem");

            e.Menu.Items.Insert(0, item);
            e.Menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ AppointmentMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName);
        }
    }

```



```js
        function AppointmentMenuHandler(scheduler, s, args) {
            if (args.item.GetItemCount() <= 0) {
                if (args.item.name == "RedirectItem")
                    window.location = 'NewPage.aspx?aptId=' + scheduler.GetSelectedAppointmentIds()[0];
                else
                    scheduler.RaiseCallback("MNUAPT|" + args.item.name);
            }
        }

```

<p>As you can see, the actual redirection is accomplished on the client side by modifying the <a href="http://www.w3schools.com/jsref/obj_location.asp"><u>window.location</u></a> attribute. The selected appointment Id is passed to a separate page via the <strong>aptId </strong>URL parameter.</p><p>In addition, page caching for the webpage with the ASPxScheduler control is disabled (see the <strong>DisablePageCaching</strong><strong>()</strong> method in the Default.aspx.cs code-behind file) because it might not operate correctly in some browsers when reloaded from the cache if you click the back button on the browser to return from the NewPage.aspx to the Default.aspx webpage.</p><p><strong>See Also:</strong><br />
<a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxSchedulerScriptsASPxClientSchedulerMembersTopicAll"><u>ASPxClientScheduler Members</u></a></p>

<br/>


