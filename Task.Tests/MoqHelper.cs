using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Moq;

namespace Task.Tests
{
    public static class MoqHelper
    {
        //public static HttpContextBase FakeHttpContext(HttpVerbs verbs, NameValueCollection nameValueCollection)
        //{
        //    var httpContext = new Mock<HttpContextBase>();

        //    var request = new Mock<HttpRequestBase>();
        //    request.Setup(c => c.Form).Returns(nameValueCollection);
        //    request.Setup(c => c.QueryString).Returns(nameValueCollection);

        //    var response = new Mock<HttpResponseBase>();
        //    var session = new Mock<HttpSessionStateBase>();
        //    var server = new Mock<HttpServerUtilityBase>();
        //    var cookies = new HttpCookieCollection();
        //    //var identity = new PMIdentity("test@test.com", "Test User", UserRole.Admin, 1);

        //    httpContext.Setup(c => c.Request).Returns(request.Object);
        //    //httpContext.Setup(c => c.User.Identity).Returns(identity);
        //    httpContext.Setup(c => c.Session).Returns(session.Object);

        //    request.Setup(c => c.RequestType).Returns(verbs.ToString().ToUpper());
        //    request.Setup(c => c.Cookies).Returns(cookies);

        //    httpContext.Setup(c => c.Response).Returns(response.Object);
        //    httpContext.Setup(c => c.Server).Returns(server.Object);
        //    //httpContext.Setup(c => c.User.Identity.Name).Returns("testclient");
        //    return httpContext.Object;
        //}
        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://google.com/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }
    }
}
