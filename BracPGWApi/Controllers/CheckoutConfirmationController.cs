using BracPGWApi.Models;
using com.fss.plugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;


namespace BracPGWApi.Controllers
{
    public class CheckoutConfirmationController : ApiController
    {
        [HttpPost]
        public  IHttpActionResult Process(QueryStringVm model)
        {
            try
            {
                String resourcePath = HttpContext.Current.Request.PhysicalApplicationPath+ "/Reference/Credendials/"+ ConfigurationManager.AppSettings["CGNFILE"].ToString() + "/cgn";   // folder location where the resource files present (Want to download from the merchant login portal)
                String aliasName = ConfigurationManager.AppSettings["AliasName"].ToString();    // Terminal Alias name (Want to get from the merchant portal)

                var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();

                var request = HttpContext.Current.Request.Params;

                string trandata = "", strval1 = "";

                strval1 = model.trandata;
                Console.WriteLine("Enc Response : " + strval1);
                if (trandata.Contains("ErrorText"))
                {
                    return Ok(new { IsSuccess = false, Message = "Failed"});
                }
                else
                {
                    iPayPipe pipe = new iPayPipe();
                    pipe.setKeystorePath(resourcePath);
                    pipe.setAlias(aliasName);
                    pipe.setResourcePath(resourcePath);
                    WriteLogFile("before result in bankhosted (result)file");
                    int result = pipe.parseEncryptedRequest(strval1);
                    WriteLogFile("after result trandata in bankhosted(result) file");
                    Console.WriteLine("Result");
                    ProcessResultVm processResult = new ProcessResultVm();
                    processResult.transactionStatus = pipe.getResult();
                    WriteLogFile("after trandata in bankhosted file--> " + pipe.getResult());
                    processResult.postDate = pipe.getDate();
                    processResult.transactioreference = pipe.getRef();
                    processResult.trackId = pipe.getTrackId();
                    processResult.transactionId = pipe.getTransId();
                    processResult.transactionAmt = pipe.getAmt();
                    processResult.paymentid = pipe.getPaymentId();
                    processResult.ECI = pipe.getEci();
                    processResult.cardNo = pipe.getCardNumber();
                    processResult.issuerRespCode = pipe.getAuthRespCode();
                    processResult.authCode = pipe.getAuth();

                    return Ok(new { IsSuccess = true, Message = "", Data = processResult });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { IsSuccess=false,Message= "Failed" + ex.Message });
            }

        }
        public void WriteLogFile(string data)
        {
            try
            {
                string CurrentDirectory = HttpContext.Current.Request.PhysicalApplicationPath + "Logs";
                string CurrentFile = HttpContext.Current.Request.PhysicalApplicationPath + "Logs\\Logs" + DateTime.Now.ToString("ddMMyyyy");
                if (Directory.Exists(CurrentDirectory) == false)
                {
                    Directory.CreateDirectory(CurrentDirectory);
                }
                else
                {
                    if (File.Exists(CurrentFile) == false)
                    {
                        FileInfo objfile = new FileInfo(CurrentFile);
                        StreamWriter Tex = objfile.CreateText();
                        string str = DateTime.Now.ToString() + ">>" + ">>" + data;
                        Tex.WriteLine(str);
                        Tex.Close();
                    }
                    else
                    {
                        FileInfo objfile = new FileInfo(CurrentFile);
                        StreamWriter Tex = objfile.AppendText();
                        Tex.WriteLine(DateTime.Now.ToString() + "==>>" + data);
                        Tex.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
    public class QueryStringVm
    {
        public string trandata { get; set; }    
    }
}