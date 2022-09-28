using BracPGWApi.Models;
using com.fss.plugin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BracPGWApi.Controllers
{
    public class CheckoutController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Process(CheckoutVm model)
        {
            try
            {
                String resourcePath = HttpContext.Current.Request.PhysicalApplicationPath + "/Reference/Credendials/" + ConfigurationManager.AppSettings["CGNFILE"].ToString()+ "/cgn";   // folder location where the resource files present (Want to download from the merchant login portal)
                String aliasName = ConfigurationManager.AppSettings["AliasName"].ToString();      // Terminal Alias name (Want to get from the merchant portal)
                String currency = ConfigurationManager.AppSettings["Currency"].ToString();    // Oman Currency
                String language = ConfigurationManager.AppSettings["Language"].ToString();    // Language it should be ENG
               //String bankHostedUrl = ConfigurationManager.AppSettings["bankHostedUrl"].ToString();    // Language it should be ENG
                WriteLogFile("before creating object in bankhosted class");
                iPayPipe ipayPipeOb = new iPayPipe();
                //WriteLogFile("after creating object in bankhosted class");
                ipayPipeOb.setResourcePath(resourcePath);
                ipayPipeOb.setKeystorePath(resourcePath);
                ipayPipeOb.setAlias(aliasName);
                ipayPipeOb.setAction(model.action);
                ipayPipeOb.setAmt(model.amount); 
                ipayPipeOb.setCurrency(currency);
                ipayPipeOb.setLanguage(language);
                //ipayPipeOb.setResponseURL(bankHostedUrl);
                //ipayPipeOb.setErrorURL(bankHostedUrl);
                ipayPipeOb.setResponseURL(model.returnUrl);
                ipayPipeOb.setErrorURL(model.returnUrl);
                ipayPipeOb.setTrackId(model.trackId);
                //ipayPipeOb.setTrackId("2487294248767824");
                ipayPipeOb.setUdf1(model.udf1??"");
                ipayPipeOb.setUdf2(model.udf2??"");
                ipayPipeOb.setUdf3(model.udf3??"");
                ipayPipeOb.setUdf4(model.udf4??"");
                ipayPipeOb.setUdf5(model.udf5??"");
                WriteLogFile("before http bankhosted method");
                int result = ipayPipeOb.performPaymentInitializationHTTP();
                WriteLogFile("after http bankhosted method");
                var bracUrl = ipayPipeOb.getWebAddress();
                if (result != 0)
                {
                    Ok( new {IsSuccess=true,Message="1",TrackId=model.trackId,Data= bracUrl });
                }
                return Ok(new { IsSuccess = true, Message = "2", Data = bracUrl });
            }
            catch (Exception ex)
            {
                WriteLogFile("excep os " + ex.InnerException);
                WriteLogFile("error exception is  " + ex.ToString());
                Console.WriteLine(ex);
               return Ok(new { IsSuccess = true, Message = "Failed"});
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
    
    
}
