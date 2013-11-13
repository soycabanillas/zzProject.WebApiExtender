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

namespace zzProject.WebApiExtender.MediaTypeFormatters
{
    public class CustomXMLMediaTypeFormatter : XmlMediaTypeFormatter
    {
        public CustomXMLMediaTypeFormatter()
            : base()
        {
            //this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/cabanillas"));
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml"));
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml"));
        }

        //protected override bool OnCanReadType(Type type)
        //{
        //    return false;
        //}
        //protected override bool OnCanWriteType(Type type)
        //{
        //    return true;
        //}

        //protected override object OnReadFromStream(Type type, Stream stream, HttpContentHeaders contentHeaders)
        //{
        //    //var serializer = new JsonSerializer();
        //    //using (var reader = new BsonReader(stream))
        //    //{
        //    //    var result = serializer.Deserialize(reader, type);
        //    //    return result;
        //    //}
        //    return null;
        //}

        //protected override void OnWriteToStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext context)
        //{
        //    //var serializer = new JsonSerializer();
        //    //serializer.Converters.Add(new IsoDateTimeConverter());
        //    //using (var writer = new BsonWriter(stream))
        //    //{
        //    //    serializer.Serialize(writer, value);
        //    //}
        //}
    }
}