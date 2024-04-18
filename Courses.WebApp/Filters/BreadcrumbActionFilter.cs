using System.Reflection;
using Courses.WebApp.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Courses.WebApp.Filters;

public class BreadcrumbActionFilter(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
    : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var breadcrumbs = new Stack<Breadcrumb>();
        var controller = context.Controller as Controller;
        var currentAction = context.ActionDescriptor as ControllerActionDescriptor;
        var urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);

        while (true)
        {
            var breadcrumb = TryGetBreadcrumbAttribute(currentAction);
            if (breadcrumb == null)
            {
                break;
            }

            breadcrumbs.Push(new Breadcrumb(breadcrumb.Name,
                urlHelper.Action(currentAction.ActionName, currentAction.ControllerName)!));

            var parentController = TryFindParentController(breadcrumb.ParentController);
            var parentActionMethod = parentController?.GetMethods()
                .FirstOrDefault(method => method.Name == breadcrumb.ParentAction);
            if (parentActionMethod == null)
            {
                break;
            }

            var parentBreadcrumb = GetBreadcrumbAttribute(parentActionMethod);
            if (parentBreadcrumb == null)
            {
                break;
            }

            currentAction = CreateParentActionDescriptor(parentController, parentBreadcrumb);
        }

        controller.ViewBag.Breadcrumbs = breadcrumbs;
    }

    private static BreadcrumbAttribute? TryGetBreadcrumbAttribute(ControllerActionDescriptor? actionDescriptor)
    {
        return actionDescriptor?.FilterDescriptors
            .Select(filterDescriptor => filterDescriptor.Filter)
            .OfType<BreadcrumbAttribute>()
            .FirstOrDefault();
    }

    private static Type? TryFindParentController(string parentControllerName)
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t =>
                t.Name.Equals($"{parentControllerName}Controller", StringComparison.InvariantCultureIgnoreCase));
    }

    private static BreadcrumbAttribute? GetBreadcrumbAttribute(ICustomAttributeProvider memberInfo)
    {
        return memberInfo.GetCustomAttributes(typeof(BreadcrumbAttribute), true)
            .FirstOrDefault() as BreadcrumbAttribute;
    }

    private static ControllerActionDescriptor CreateParentActionDescriptor(Type parentController,
        BreadcrumbAttribute parentBreadcrumb)
    {
        return new ControllerActionDescriptor
        {
            ControllerTypeInfo = parentController.GetTypeInfo(),
            ActionName = parentBreadcrumb.ParentAction,
            FilterDescriptors = new List<FilterDescriptor>
            {
                new(parentBreadcrumb, FilterScope.Action)
            },
            ControllerName = parentBreadcrumb.ParentController
        };
    }
}

public class Breadcrumb(string name, string url)
{
    public string Name { get; set; } = name;
    public string Url { get; set; } = url;
}