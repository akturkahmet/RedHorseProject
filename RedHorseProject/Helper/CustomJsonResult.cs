using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Web.Mvc;
using System.Web;
using System;

public class CustomJsonResult : JsonResult
{
    private const string _dateFormat = "dd-MM-yyyy HH:mm";

    // Varsayılan olarak DenyGet. Ama gerektikçe AllowGet yapılabilir.
    public JsonRequestBehavior JsonRequestBehavior { get; set; } = JsonRequestBehavior.AllowGet;

    public override void ExecuteResult(ControllerContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
            string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("GET requests are not allowed. To allow GET, set JsonRequestBehavior to AllowGet.");
        }

        HttpResponseBase response = context.HttpContext.Response;

        if (!string.IsNullOrEmpty(ContentType))
        {
            response.ContentType = ContentType;
        }
        else
        {
            response.ContentType = "application/json";
        }

        if (ContentEncoding != null)
        {
            response.ContentEncoding = ContentEncoding;
        }

        if (Data != null)
        {
            // JSON.NET serializer kullanımı
            var isoConvert = new IsoDateTimeConverter { DateTimeFormat = _dateFormat };
            response.Write(JsonConvert.SerializeObject(Data, isoConvert));
        }
    }
}
