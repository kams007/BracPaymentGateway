using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BracPGWApi.Controllers
{
    public class MakePaymentController : ApiController
    {
        [HttpPost]
        public async Task<string> process(requestVm model)
        {
            //using (var client = new HttpClient())
            //{
            //    //var data = new Dictionary<string, string>
            //    //        {
            //    //            {"amount", model.amount}
            //    //        };
            //    //var Json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            //    //{
            //    //    ContractResolver = new DefaultContractResolver
            //    //    {
            //    //        IgnoreSerializableAttribute = false
            //    //    }
            //    //});
            //    //HttpContent httpContent = new StringContent(Json, Encoding.UTF8, "application/json");
            //    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //    //var res = await client.PostAsync("http://localhost:7236/api/Brac/GetPayment", httpContent);
            //    //var x = await res.Content.ReadAsStringAsync();
            //};
            return "test url";
        }
        //[HttpPost]
        //public async void process_confirm(requestVm model)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var data = new Dictionary<string, string>
        //                {
        //                    {"amount", model.amount}
        //                };
        //        var Json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
        //        {
        //            ContractResolver = new DefaultContractResolver
        //            {
        //                IgnoreSerializableAttribute = false
        //            }
        //        });
        //        HttpContent httpContent = new StringContent(Json, Encoding.UTF8, "application/json");
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        //        var res = await client.PostAsync("http://localhost:7236/api/Brac/GetPayment", httpContent);
        //    };
        //}
    }
    public class requestVm
    {
        public string amount { get; set; }
    }
}
