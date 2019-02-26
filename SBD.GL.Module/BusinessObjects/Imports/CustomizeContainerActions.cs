 
//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Actions;
//using DevExpress.ExpressApp.SystemModule;
//using DevExpress.ExpressApp.Win.Templates.Bars.ActionControls;
//// ... 
//public class CustomizeContainerActionsController : Controller
//{

//    protected override void OnActivated()
//    {

//        base.OnActivated();

//        Frame.GetController<ActionControlsSiteController>().CustomizeContainerActions += OnCustomizeContainerActions;

//        Frame.GetController<FillActionContainersController>().CustomizeContainerActions += OnCustomizeContainerActions;

//    }

//    protected override void OnDeactivated()
//    {

//        Frame.GetController<ActionControlsSiteController>().CustomizeContainerActions -= OnCustomizeContainerActions;

//        Frame.GetController<FillActionContainersController>().CustomizeContainerActions -= OnCustomizeContainerActions;

//        base.OnDeactivated();

//    }

//    private void OnCustomizeContainerActions(object sender, CustomizeContainerActionsEventArgs e)
//    {

//        ActionBase actionToBeMoved = e.AllActions.Find("SaveAndNew");
        

        
//        if ((actionToBeMoved != null) && (e.Category == "Save") && (e.Container is BarLinkActionControlContainer))
//        {

//            e.ContainerActions.Remove(actionToBeMoved);

//        }

//        if ((actionToBeMoved != null) && (e.Category == "Edit"))
//        {

//            e.ContainerActions.Add(actionToBeMoved);

//        }

//    }

//}