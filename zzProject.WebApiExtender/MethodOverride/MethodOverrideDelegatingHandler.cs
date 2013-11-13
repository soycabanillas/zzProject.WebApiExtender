using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;

namespace zzProject.WebApiExtender.MethodOverride {
    
    //public enum MethodOverrideHeaderEnum
    //{
    //    X_HTTP_Method,
    //    X_HTTP_Method_Override,
    //    X_METHOD_OVERRIDE
    //}

    public class MethodOverrideDelegatingHandler : DelegatingHandler {

        private readonly string _header = "X-HTTP-Method-Override";

        public MethodOverrideDelegatingHandler()
        {
        }

        //X-HTTP-Method (Microsoft)
        //X-HTTP-Method-Override (Google/GData) <-- Default
        //X-METHOD-OVERRIDE (IBM)
        public MethodOverrideDelegatingHandler(string Header)
        {
            _header = Header;
        }
        
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) {
            if (request.Method == HttpMethod.Post) {

                //string XHttpMethod = 
                IEnumerable<string> Values;
                if (request.Headers.TryGetValues(_header, out Values))
                {
                    string XHttpMethod = Values.FirstOrDefault();
                    switch (XHttpMethod) {
                        case "PUT":
                            request.Method = HttpMethod.Put;
                            break;
                        case "DELETE":
                            request.Method = HttpMethod.Delete;
                            break;
                        case "POST":
                            request.Method = HttpMethod.Post;
                            break;
                    }
                }
            }
            return base.SendAsync(request, cancellationToken);
        }

        //public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext) {
        //    if (controllerContext.Request.Method == HttpMethod.Post) {
        //        //X-HTTP-Method (Microsoft)
        //        //X-HTTP-Method-Override (Google/GData)
        //        //X-METHOD-OVERRIDE (IBM)
        //        string XHttpMethod = controllerContext.Request.Headers.GetValues("X-HTTP-Method-Override").FirstOrDefault();

        //        switch (XHttpMethod) {
        //            case "PUT":
        //                controllerContext.Request.Method = HttpMethod.Put;
        //            case "MERGE":
        //                controllerContext.Request.Method = HttpMethod.P;
        //            case "DELETE":
        //                controllerContext.Request.Method = HttpMethod.Delete;
        //        }
        //        // This part feels like a dirty ugly hack to work around the shortcoming that looking at the request content
        //        // effectively consumes it. So I have to take it out, look at it, and put it back.
        //        // If you would like to decide upon an action based on something else, like a header setting or whatever
        //        // it can be removed.
        //        var ms = new MemoryStream();
        //        controllerContext.Request.Content.ReadAsStreamAsync().Result.CopyTo(ms);
        //        controllerContext.Request.Content.Dispose();

        //        ms.Seek(0, SeekOrigin.Begin);
        //        HttpContent newContent = new StreamContent(ms);

        //        // Copy the header values from the old content to the new content here

        //        controllerContext.Request.Content = newContent;

        //        var data = controllerContext.Request.Content.ReadAsAsync(GlobalConfiguration.Configuration.Formatters).Result;

        //        ms.Seek(0, SeekOrigin.Begin);
        //        // End hack

        //        if (data != null) {
        //            // Controller is already set based on routing.
        //            var controllerType = controllerContext.Controller.GetType();

        //            // Try to find a method on the controller's type based on some criteria (in this case post methods are named Post).
        //            // This is a very simple example, and you could easily implement a more advanced behaviour.
        //            // And to take a cue from the ApiControllerActionSelector, implement some caching since we are bypassing the
        //            // caching implemented by APiControllerActionSelector
        //            var method = controllerType.GetMethod("Post" + GetCorrectMethodFor(data));

        //            // Only override default behaviour if we find the method.
        //            if (method != null) {
        //                return new ReflectedHttpActionDescriptor(controllerContext.ControllerDescriptor, method);
        //            }
        //        }
        //    }

        //    // Delegate back to base class.
        //    return base.SelectAction(controllerContext);
        //}
    }
}
