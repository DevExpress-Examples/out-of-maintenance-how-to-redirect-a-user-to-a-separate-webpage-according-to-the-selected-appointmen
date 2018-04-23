using System;

public partial class NewPage : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        Label1.Text = "Appointment Id: " + Request.QueryString["aptId"];
    }
}