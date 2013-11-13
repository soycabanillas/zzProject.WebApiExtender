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
using Newtonsoft.Json.Bson;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace zzProject.WebApiExtender.MediaTypeFormatters
{
    public class CustomBSONMediaTypeFormatter : MediaTypeFormatter
    {
        public CustomBSONMediaTypeFormatter()
            : base()
        {
            //this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/cabanillas"));
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/bson"));
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/bson"));
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
                using (var reader = new BsonReader(readStream)) {
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
                using (var writer = new BsonWriter(writeStream)) {
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