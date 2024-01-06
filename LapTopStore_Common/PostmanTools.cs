using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Common
{
    public static class PostmanTools
    {
        public static string WebPost(string url, string baseUrl, string jsonData)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(baseUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", ".AspNetCore.Identity.Application=CfDJ8AdRK-PzapZBm4GqkviZiy6IOa_cjkgxOBpW_UkfMmBBCayYY1KwKSwBzGUQBCck51S3bSg0hpTxeyFp8k3yzYkpKnpojIMyzVeRNuw1s5Ncdynt7nSAPlFh-yxvrPsawN7end9UvGxbs_ufjETfI3EFTH46JKyAeixw-hAjr2j_rcIhT-Jd7HKn_2viOrqEIByO0IZxnr-OVZb-SAR_MkgSGu-Lq4wUeZEGP0X2yWIxIpkxgp8gEXCqmf7cYW5C0HnqlFskfbcIfl68FELJbYRsEWSTmiZBaiS-rdzF6u-T1O-LwE6lNF3mFg3fonIWmBNQAjDSO1Ca5ezHD6m_k49dOGXFk3Ep06w0IplC2kNiHh6N0oVN08P7b_aWtDe-4t8J1Kcen0Jg9a60A_9Fp2AsiO2XnjQ9C8MKMJIB24fNLNrn7NgtpKfyC7PHdz_auslGCV7gAwrn26uZhDRyUVGu_6h0Qiaf1vv2EAhEjr6VjjTrbOwz77pYaeEuGERA5BsBGMFsE7AqREtso6lqbrVwVNpWmHKjXgvkRfB8gI4yfuvj7bvMCq6gh-9AWmtq5rl8TrhKe1piGpZmW81PY_ftLxd11Kh020h9n3kJlqwWQM51V5txqXKS03zV6Cie9-pw4IfnOS8XzaaPNT_U58y0j-HPiw-11Wi_GbnyWROjOASMeNKmzh_3ilPQws8bdA");
            var body = jsonData;
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = client.Execute(request);

            return response.Content;
        }

        public static string WebGet(string url, string baseUrl)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(baseUrl, Method.Get);
            RestResponse response = client.Execute(request);

            return response.Content;
        }

        public static string WebGetDelete(string url, string baseUrl, string jsonData)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(baseUrl, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var body = jsonData;
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = client.Execute(request);

            return response.Content;
        }

        public static string WebPostImage(string url, string baseUrl, string jsonData)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(baseUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = jsonData;
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = client.Execute(request);

            return response.Content;
        }


    }
}
