using AutomationTest.Controls;
using Common.Helpers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AutomationTest.Helpers
{
    public class WebHelper
    {
        public static async Task<T> HttpGet<T>(string baseUri, string path, string accessToken = "", string lang = "")
        {
            var httpRs = await CommonHelper.HttpGet(baseUri, path, accessToken, lang);

            if (httpRs.IsSuccess)
            {
                if (httpRs.Content != null)
                {
                    var rs = ToAPIDataResult<T>(httpRs.Content);
                    if (rs.Result == 0)
                    {
                        return rs.Data;
                    }
                    else if (rs.Result > 0)
                    {
                        throw new System.Exception(rs.ErrorMessage);
                    }
                    else
                    {
                        throw new System.Exception(rs.ErrorMessage);
                    }
                }
            }
            throw new System.Exception("APIGateway.FailedNetWork");
        }

        public static async Task<T> HttpPost<T>(string baseUri, string path, object data, string accessToken = "", string lang = "")
        {
            var httpRs = await CommonHelper.HttpPost(baseUri, path, data, accessToken, lang);

            if (httpRs.IsSuccess)
            {
                if (httpRs.Content != null)
                {
                    var rs = ToAPIDataResult<T>(httpRs.Content);
                    if (rs.Result == 0)
                    {
                        return rs.Data;
                    }
                    else if (rs.Result > 0)
                    {
                        throw new System.Exception(rs.ErrorMessage);
                    }
                    else
                    {
                        throw new System.Exception(rs.ErrorMessage);
                    }
                }
            }
            throw new System.Exception("APIGateway.FailedNetWork");
        }

        public static APIDataResult<T> ToAPIDataResult<T>(string content)
        {
            return JsonConvert.DeserializeObject<APIDataResult<T>>(content);
        }
        public static APIResult ToAPIResult(string content)
        {
            return JsonConvert.DeserializeObject<APIResult>(content);
        }
    }
}
