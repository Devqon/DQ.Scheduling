using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.UI.Admin;
using Orchard.Workflows.Services;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DQ.Scheduling.Controllers {
    [Admin]
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsAdminController : Controller {
        private readonly IContentManager _contentManager;
        private readonly IWorkflowManager _workflowManager;
        public NotificationsAdminController(IContentManager contentManager, IWorkflowManager workflowManager) {
            _contentManager = contentManager;
            _workflowManager = workflowManager;
        }
        
        [HttpPost]
        public ActionResult SendCustomNotification(int contentId, string message) {
            var result = "";
            var contentItem = _contentManager.Get(contentId);
            if (contentItem != null) {
                _workflowManager.TriggerEvent(Constants.EventCustomNotification, contentItem,
                    () => new Dictionary<string, object>{
                        { "Content", contentItem },
                        { "CustomMessage", message }
                    });
                Response.StatusCode = (int)HttpStatusCode.OK;
                result = "Success";
            }
            else {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = "Invalid Content Id";
            }
            
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}