Imports Microsoft.VisualBasic
Imports System

Partial Public Class NewPage
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		Label1.Text = "Appointment Id: " & Request.QueryString("aptId")
	End Sub
End Class