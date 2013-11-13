using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace zzProject.WebApiExtender.MediaTypeFormatters
{
    public class CustomJSONMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public CustomJSONMediaTypeFormatter()
            : base()
        {
            //this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/cabanillas"));
            //this.AddMediaRangeMapping("*/*", "text/html");
            //this.AddMediaRangeMapping("*/*", "*/*");
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/json"));
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

            //var requestHeaderMapping = new RequestHeaderMapping(
            //    "x-requested-with",
            //    "xmlhttprequest",
            //    StringComparison.OrdinalIgnoreCase,
            //    true,
            //    new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" });
            //this.MediaTypeMappings.Add(requestHeaderMapping);
        }

        //protected override bool OnCanReadType(Type type)
        //{
        //    return false;
        //}
        //protected override bool OnCanWriteType(Type type)
        //{
        //    return true;
        //}

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger) {
            var taskSource = new TaskCompletionSource<object>();
            try {
                var serializer = new JsonSerializer();
                object result;
                using (var sr = new StreamReader(readStream))
                using (var reader = new JsonTextReader(sr)) {
                    result = serializer.Deserialize(reader, type);
                }
                taskSource.SetResult(result);
            } catch (Exception e) {
                taskSource.SetException(e);
            }
            return taskSource.Task;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, TransportContext transportContext) {
            var taskSource = new TaskCompletionSource<object>();
            try {
                var serializer = new JsonSerializer();
                serializer.Converters.Add(new IsoDateTimeConverter());
                using (var sw = new StreamWriter(writeStream))
                using (var writer = new JsonTextWriter(sw)) {
                    serializer.Serialize(writer, value);
                }
                taskSource.SetResult(null);
            } catch (Exception e) {
                taskSource.SetException(e);
            }
            return taskSource.Task;
        }

        public override bool CanReadType(Type type) {
            return true;
        }

        public override bool CanWriteType(Type type) {
            return true;
        }
    }
}