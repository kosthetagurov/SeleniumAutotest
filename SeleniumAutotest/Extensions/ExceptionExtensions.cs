using System.Text;

namespace SeleniumAutotest.Extensions
{
    public static class ExceptionExtensions
    {
        public static string Summary(this Exception exception)
        {
            var sb = new StringBuilder();

            sb.AppendLine(exception.GetType().FullName + " ");
            sb.AppendLine(exception.Message + " ");
            sb.AppendLine(exception.StackTrace + " ");

            if (exception.InnerException != null)
            {
                sb.AppendLine("Inner exception: " +exception.InnerException.GetType().FullName + " ");
                sb.AppendLine(exception.InnerException.Message + " ");
                sb.AppendLine(exception.InnerException.StackTrace + " ");
            }

            return sb.ToString();
        }

        public static string HtmlSummary(this Exception exception)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<h4>" + exception.GetType().FullName + "</h4>");
            sb.AppendLine("<p>" + exception.Message + "</p>");
            sb.AppendLine("<p>" + exception.StackTrace + "</p>");

            if (exception.InnerException != null)
            {
                sb.AppendLine("<h4>Inner exception: " + exception.InnerException.GetType().FullName + "</h4>");
                sb.AppendLine("<p>" + exception.InnerException.Message + "</p>");
                sb.AppendLine("<p>" + exception.InnerException.StackTrace + "</p>");
            }

            return sb.ToString();
        }
    }
}
