using Microsoft.AspNetCore.Mvc.Rendering;

namespace SoloCRM.Helper
{
    public static class MenuHelper
    {
        public static bool IsCustomerPage(ViewContext viewContext)
        {
            var currentPage = viewContext.RouteData.Values["page"]?.ToString();
            if (string.IsNullOrEmpty(currentPage))
                return false;

            // Check if it is a Customer related page
            return currentPage.Contains("/Customers/", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsCurrentPage(ViewContext viewContext, string pagePath)
        {
            var currentPage = viewContext.RouteData.Values["page"]?.ToString();
            if (string.IsNullOrEmpty(currentPage) || string.IsNullOrEmpty(pagePath))
                return false;

            return currentPage.StartsWith(pagePath, StringComparison.OrdinalIgnoreCase);
        }
    }
}
