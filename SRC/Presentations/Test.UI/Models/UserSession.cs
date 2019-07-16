using Newtonsoft.Json;
using System.Collections.Generic;

namespace Test.UI.Models
{
    public class UserSession
    {
        public long SessionId { set; get; }

        public string AccessToken { set; get; }

        [JsonIgnore]
        public int LoginResult { set; get; }

        [JsonIgnore]
        public string LoginCaptionMessage { set; get; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsSuperAdmin { get; set; }

        public int RoleId { set; get; }

        public bool IsExternalUser { set; get; }
    }
}
