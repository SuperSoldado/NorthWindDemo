using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Navigator3.Core.Models;
using System.Security.Principal;
using Newtonsoft.Json;
using GreeNova.DGNB.SystemSoftware.SecurityProvider.Managers;
using Navigator3.Core.TransferObjects;
using Navigator3.Core.Managers;

namespace MyAppRESTLib.Helpers
{
    public class GenericResponse
    {
        public string Result { get; set; }
    }

    /// <summary>
    /// Represent a generic json response used in web api after inserting one register and returning the ID inserted.
    /// Most cases, after inser return "created" and {"id":"123-456-789"}
    /// </summary>
    public class GenericInsertOneResponse : GenericResponse
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Generic response to an update one request: {"Result":"Register updated"}
    /// </summary>
    public class GenericUpdateOneResponse : GenericResponse
    {
    }

    /// <summary>
    /// Generic response to an delete one request: {"Result":"Register deleted"}
    /// </summary>
    public class GenericDeleteOneResponse : GenericResponse
    {
    }

    public class GenericErrorResponse : GenericResponse
    {
        public string Exception { get; set; }
    }

    public class WebApiHelper
    {
        /// <summary>
        /// Return null if guid is valid.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public HttpResponseMessage GuidStringIsValid(string guid, out Guid? validGuid)
        {
            string error = null;

            Guid guidID;
            bool isValid = Guid.TryParse(guid, out guidID);
            if (isValid)
            {
                validGuid = guidID;
            }
            else
            {
                validGuid = null;
                error = "Could not recognize this guid.";
                HttpResponseMessage response = GetBadRequestResponse(error);
                return response;
            }

            return null;
        }

        private HttpRequestMessage request { get; set; }
        public WebApiHelper(HttpRequestMessage request)
        {
            this.request = request;
        }

        public HttpResponseMessage GetGenerictTextResponse(string output, HttpStatusCode httpStatusCode)
        {
            var methodResponse = this.request.CreateResponse(httpStatusCode);
            methodResponse.Content = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        /// <summary>
        /// Generic response to one insert command. Returns 201
        /// </summary>
        /// <param name="idInserted"></param>
        /// <returns></returns>
        public HttpResponseMessage GetInsertResponse(Guid idInserted, string text)
        {
            GenericInsertOneResponse response = new GenericInsertOneResponse();
            response.Id = idInserted;
            response.Result = text;
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.Created);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetCustomInsertResponse(Core.TransferObjects.IStandartResponse objectToRespond)
        {
            if (string.IsNullOrEmpty(objectToRespond.Result))
            {
                objectToRespond.Result = "Inserted";
            }
            string yourJson = JsonConvert.SerializeObject(objectToRespond);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.Created);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetInsertResponse(object objectInserted)
        {
            string yourJson = JsonConvert.SerializeObject(objectInserted);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.Created);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetInsertResponse(Guid idInserted)
        {
            return GetInsertResponse(idInserted, "Inserted");
        }

        public HttpResponseMessage GetDeleteResponse()
        {
            GenericDeleteOneResponse response = new GenericDeleteOneResponse();
            response.Result = "Deleted";
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.OK);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetUpdateOneResponse()
        {
            GenericUpdateOneResponse response = new GenericUpdateOneResponse();
            response.Result = "Updated";
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.OK);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetNotFoundResponse()
        {
            GenericResponse response = new GenericResponse();
            response.Result = "Not found";
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.NotFound);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetNotFoundResponse(string customReturnText)
        {
            GenericResponse response = new GenericResponse();
            response.Result = customReturnText;
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.NotFound);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetPreConditionFailed(string customReturnText)
        {
            GenericResponse response = new GenericResponse();
            response.Result = customReturnText;
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.PreconditionFailed);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetForbidden(string customReturnText)
        {
            GenericResponse response = new GenericResponse();
            response.Result = customReturnText;
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.Forbidden);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetUnauthorized(string customReturnText)
        {
            GenericResponse response = new GenericResponse();
            response.Result = customReturnText;
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.Unauthorized);//401
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetBadRequestResponse(string message)
        {
            GenericResponse response = new GenericResponse();
            response.Result = message;
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.BadRequest);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetExceptionResponse(string error, string exception, HttpStatusCode errorType)
        {
            GenericErrorResponse response = new GenericErrorResponse();
            response.Result = error;
            response.Exception = exception;
            string yourJson = JsonConvert.SerializeObject(response);
            var methodResponse = this.request.CreateResponse(errorType);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        public HttpResponseMessage GetSuccessGetResponse(object myData)
        {
            string yourJson = JsonConvert.SerializeObject(myData);
            var methodResponse = this.request.CreateResponse(HttpStatusCode.OK);
            methodResponse.Content = new StringContent(yourJson, System.Text.Encoding.UTF8, "application/json");
            return methodResponse;
        }

        /// <summary>
        /// Get logged user. Example: 
        /// ApplicationUser loggedUser = helper.GetLoggedUser(RequestContext.Principal.Identity, out error);
        /// </summary>
        /// <param name="principal">use "RequestContext.Principal.Identity"</param>
        /// <returns></returns>
        public ApplicationUser GetLoggedUser(IIdentity principal, out string error)
        {
            error = null;
            try
            {
                UserManager UserManager = new GreeNova.DGNB.SystemSoftware.SecurityProvider.Managers.UserManager();
                if (principal.Name == null)
                {
                    return null;
                }
                GreeNova.DGNB.SystemSoftware.SecurityProvider.Database.ApplicationUser loggedUser = UserManager.FindByName(principal.Name);
                ApplicationUser usr = new ApplicationUser();
                usr.Id = loggedUser.Id;
                return usr;
            }
            catch (Exception ex)
            {
                error = "Could not retrive 'GetLoggedUser'. Error:" + ex.Message + Environment.NewLine + ex.StackTrace;
                return null;
            }
        }


        /// <summary>
        /// Ultimate place to get logged info data
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public LoggedUserCompleteInfo GetLoggedUserAndLocalDBUser(IIdentity principal, bool loadRoles, out string error)
        {
            error = null;
            try
            {
                UserManager UserManager = new GreeNova.DGNB.SystemSoftware.SecurityProvider.Managers.UserManager();
                if (principal.Name == null)
                {
                    return null;
                }
                GreeNova.DGNB.SystemSoftware.SecurityProvider.Database.ApplicationUser loggedUser = UserManager.FindByName(principal.Name);

                LoggedUserCompleteInfo loggedUserInfo = new LoggedUserCompleteInfo();
                loggedUserInfo.MainRole = "";
                loggedUserInfo.GreenNovaLoggedUser.UserName = loggedUser.UserName;
                if (loadRoles)
                {
                    List<string> roles = UserManager.GetRoles(loggedUser.Id).ToList();
                    loggedUserInfo.GreenNovaLoggedUser.Roles = roles;

                    PermissionManager permissionManager = new PermissionManager();
                    if (roles != null)
                    {
                        if (roles.Count > 0)
                        {
                            loggedUserInfo.MainRole = permissionManager.GetMainUserRole(roles);
                        }
                    }
                }

                loggedUserInfo.LoadLocalDBUser();

                return loggedUserInfo;
            }
            catch (Exception ex)
            {
                error = "Could not retrive 'GetLoggedUser'. Error:" + ex.Message + Environment.NewLine + ex.StackTrace;
                return null;
            }
        }

        /// <summary>
        /// Get logged user. Example: 
        /// Guid loggedUserGuid = helper.GetLoggedUserGuid(RequestContext.Principal.Identity, out error);
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Guid? GetLoggedUserGuid(IIdentity principal, out string error)
        {
            var user = GetLoggedUser(principal, out error);
            if (user == null)
            {
                return null;
            }

            return Guid.Parse(user.Id);
        }

        public GreeNova.DGNB.SystemSoftware.SecurityProvider.Database.ApplicationUser GetUserByID(string userID, out string error)
        {
            error = null;
            try
            {
                UserManager UserManager = new GreeNova.DGNB.SystemSoftware.SecurityProvider.Managers.UserManager();

                GreeNova.DGNB.SystemSoftware.SecurityProvider.Database.ApplicationUser user = UserManager.FindById(userID);
                return user;
            }
            catch (Exception ex)
            {
                error = "Could not retrive 'User'. Error:" + ex.Message + Environment.NewLine + ex.StackTrace;
                return null;
            }
        }

        public GreeNova.DGNB.SystemSoftware.SecurityProvider.Database.ApplicationUser GetUser(string name, out string error)
        {
            error = null;
            try
            {
                if (name == null)
                {
                    name = "ozzy@osbourne.com";
                }
                UserManager UserManager = new GreeNova.DGNB.SystemSoftware.SecurityProvider.Managers.UserManager();

                GreeNova.DGNB.SystemSoftware.SecurityProvider.Database.ApplicationUser user = UserManager.FindByName(name);
                return user;
            }
            catch (Exception ex)
            {
                error = "Could not retrive 'GetLoggedUser'. Error:" + ex.Message + Environment.NewLine + ex.StackTrace;
                return null;
            }
        }

        public ApplicationUser GetUserFromLocalDB(string name, out string error)
        {
            error = null;
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    ApplicationUser result = db.Users.Where(x => x.UserName == name).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                error = "Could not retrive 'GetLoggedUser'. Error:" + ex.Message + Environment.NewLine + ex.StackTrace;
                return null;
            }
        }

        /// <summary>
        /// Get roles from user. Use "GetLoggedUser()" to get "ApplicationUser"
        /// </summary>
        /// <param name="userID">Send "ApplicationUser.id" from method "GetLoggerUser()"</param>
        /// <returns></returns>
        public List<string> GetRolesFromUser(string userID, out string error)
        {
            error = null;
            try
            {
                UserManager UserManager = new GreeNova.DGNB.SystemSoftware.SecurityProvider.Managers.UserManager();
                var roles = UserManager.GetRoles(userID);
                return roles.ToList();
            }
            catch (Exception ex)
            {
                error = "Could not retrive 'GetRolesFromUser'. Error:" + ex.Message + Environment.NewLine + ex.StackTrace;
                return null;
            }
        }

        /// <summary>
        /// Get one header value if exists or return null
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public string GetSingleHeader(HttpRequestMessage request, string headerName)
        {
            IEnumerable<string> headers = new List<string>();
            request.Headers.TryGetValues(headerName, out headers);
            if ((headers == null) || (headers.Count() == 0))
            {
                return null;
            }
            string headerContent = headers.First();
            return headerContent;
        }
    }
}