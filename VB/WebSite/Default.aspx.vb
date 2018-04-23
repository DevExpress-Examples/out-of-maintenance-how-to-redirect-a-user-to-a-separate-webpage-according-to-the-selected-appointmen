Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxMenu

Partial Public Class [Default]
	Inherits System.Web.UI.Page
	Private ReadOnly Property Storage() As ASPxSchedulerStorage
		Get
			Return ASPxScheduler1.Storage
		End Get
	End Property

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		SetupMappings()
		ResourceFiller.FillResources(Me.ASPxScheduler1.Storage, 3)

		ASPxScheduler1.AppointmentDataSource = appointmentDataSource
		ASPxScheduler1.DataBind()

		ASPxScheduler1.GroupType = SchedulerGroupType.Resource

		DisablePageCaching()
	End Sub

	Protected Sub ASPxScheduler1_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
		If e.Menu.Id.Equals(SchedulerMenuItemId.AppointmentMenu) Then
			Dim item As New MenuItem("Redirect", "RedirectItem")

			e.Menu.Items.Insert(0, item)
			e.Menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ AppointmentMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName)
		End If
	End Sub

	Private Sub SetupMappings()
		Dim mappings As ASPxAppointmentMappingInfo = Storage.Appointments.Mappings
		Storage.BeginUpdate()
		Try
			mappings.AppointmentId = "Id"
			mappings.Start = "StartTime"
			mappings.End = "EndTime"
			mappings.Subject = "Subject"
			mappings.AllDay = "AllDay"
			mappings.Description = "Description"
			mappings.Label = "Label"
			mappings.Location = "Location"
			mappings.RecurrenceInfo = "RecurrenceInfo"
			mappings.ReminderInfo = "ReminderInfo"
			mappings.ResourceId = "OwnerId"
			mappings.Status = "Status"
			mappings.Type = "EventType"
		Finally
			Storage.EndUpdate()
		End Try
	End Sub
	'Initilazing ObjectDataSource
	Protected Sub appointmentsDataSource_ObjectCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceEventArgs)
		e.ObjectInstance = New CustomEventDataSource(GetCustomEvents())
	End Sub
	Private Function GetCustomEvents() As CustomEventList
		Dim events As CustomEventList = TryCast(Session("ListBoundModeObjects"), CustomEventList)
		If events Is Nothing Then
			events = GenerateCustomEventList()
			Session("ListBoundModeObjects") = events
		End If
		Return events
	End Function

	' User generated appointment id    
	Protected Sub ASPxScheduler1_AppointmentInserting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
		SetAppointmentId(sender, e)
	End Sub

	Private Sub SetAppointmentId(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
		Dim storage As ASPxSchedulerStorage = CType(sender, ASPxSchedulerStorage)
		Dim apt As Appointment = CType(e.Object, Appointment)
		storage.SetAppointmentId(apt, apt.GetHashCode())
	End Sub

	#Region "Random events generation"
	Private Function GenerateCustomEventList() As CustomEventList
		Dim eventList As New CustomEventList()
		Dim count As Integer = Storage.Resources.Count
		For i As Integer = 0 To count - 1
			Dim resource As Resource = Storage.Resources(i)
			Dim subjPrefix As String = resource.Caption & "'s "

			eventList.Add(CreateEvent(resource.Id, subjPrefix & "meeting", 2, 5))
			eventList.Add(CreateEvent(resource.Id, subjPrefix & "travel", 3, 6))
			eventList.Add(CreateEvent(resource.Id, subjPrefix & "phone call", 0, 10))
		Next i
		Return eventList
	End Function
	Private Function CreateEvent(ByVal resourceId As Object, ByVal subject As String, ByVal status As Integer, ByVal label As Integer) As CustomEvent
		Dim customEvent As New CustomEvent()
		customEvent.Subject = subject
		customEvent.OwnerId = resourceId
		Dim rnd As Random = rndInstance
		Dim rangeInHours As Integer = 48
		customEvent.StartTime = DateTime.Today + TimeSpan.FromHours(rnd.Next(0, rangeInHours))
		customEvent.EndTime = customEvent.StartTime + TimeSpan.FromHours(rnd.Next(0, rangeInHours \ 8))
		customEvent.Status = status
		customEvent.Label = label
		customEvent.Id = "ev" & customEvent.GetHashCode()
		Return customEvent
	End Function
	Private Shared rndInstance As New Random()
	#End Region

	Public Shared Sub DisablePageCaching()
		'Used for disabling page caching
		HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1))
		HttpContext.Current.Response.Cache.SetValidUntilExpires(False)
		HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches)
		HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
		HttpContext.Current.Response.Cache.SetNoStore()
	End Sub
End Class