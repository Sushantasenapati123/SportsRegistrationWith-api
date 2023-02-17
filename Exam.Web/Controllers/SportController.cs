using Exam.Domain.Sports;
using Exam.Irepository.ISport;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Exam.Web.Controllers
{//Sport_Registration
    public class SportController : Controller
    {

        private readonly IWebHostEnvironment _environment;
        Uri baseadd = new Uri("http://localhost:1688/api");
        HttpClient client;
        //private readonly SpotInterface log;
        public SportController(IWebHostEnvironment environment/*, SpotInterface _log*/)
        {
            _environment = environment;
            client = new HttpClient();
            client.BaseAddress = baseadd;
            //log = _log;
        }
        [HttpGet]
        public async Task<IActionResult> Downloadd(Spot clubid)
        {

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Regis/generatepdf").Result;
            List<SelectListItem> scatlist = new List<SelectListItem>();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var lstcat = JsonConvert.DeserializeObject<List<Spot>>(data);
                foreach (Spot dr in lstcat)
                {
                    scatlist.Add(new SelectListItem { Text = dr.sprot_name, Value = dr.sport_Id.ToString() });
                }
            }
            var jsonres = JsonConvert.SerializeObject(scatlist);
            return Json(jsonres);

        }

        public IActionResult Download()
        {
            List<Spot> pc = new List<Spot>();
            HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "/Regis").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data1 = response1.Content.ReadAsStringAsync().Result;
                pc = JsonConvert.DeserializeObject<List<Spot>>(data1);
            }
            PdfSharpCore.Pdf.PdfDocument document = new PdfSharpCore.Pdf.PdfDocument();

            string[] copies = { "Employee copy"/*, "Comapny Copy" */};
            for (int i = 0; i < copies.Length; i++)
            {
                List<Spot> detail = pc;
                string htmlcontent = "<div style='width:100%; text-align:center'>";
                htmlcontent += "<h2>" + copies[i] + "</h2>";
                htmlcontent += "<h2>Employee Data</h2>";
                htmlcontent += "<hr>";
                htmlcontent += "<br>";
                htmlcontent += "<table style ='width:100%; border: 1px solid #000'>";
                htmlcontent += "<thead style='font-weight:bold'>";
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border:1px solid #000'> Registration_Id </td>";
                htmlcontent += "<td style='border:1px solid #000'> Applicant_name </td>";
                htmlcontent += "<td style='border:1px solid #000'> club_name </td>";
                htmlcontent += "<td style='border:1px solid #000'> sprot_name </td >";

                htmlcontent += "</tr>";
                htmlcontent += "</thead >";

                htmlcontent += "<tbody>";
                if (detail != null && detail.Count > 0)
                {
                    detail.ForEach(item =>
                    {
                        htmlcontent += "<tr>";
                        htmlcontent += "<td style='border:1px solid #000'>" + item.Registration_Id + "</td>";
                        htmlcontent += "<td style='border:1px solid #000'>" + item.Applicant_name + "</td>";
                        htmlcontent += "<td style='border:1px solid #000'>" + item.club_name + "</td >";
                        htmlcontent += "<td style='border:1px solid #000'>" + item.sprot_name + "</td>";

                        htmlcontent += "</tr>";
                    });
                }
                htmlcontent += "</tbody>";

                htmlcontent += "</table>";
                htmlcontent += "</div>";

                htmlcontent += "</div>";

                PdfGenerator.AddPdfPages(document, htmlcontent, (PdfSharpCore.PageSize)PdfSharp.PageSize.A4);
            }
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            { 
                document.Save(ms);
                response = ms.ToArray();
            }
            string Filename = "Employee" + ".pdf";
            var stream = new FileStream(Filename, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
           // return File(response, "application/pdf", Filename);
            //string Filename = "Employee" + ".pdf";
            //string path = Request.GetDisplayUrl()+"/"+ Filename;
            //var stream = new FileStream(@"https://localhost:44328/Sport/Download/Employee.pdf", FileMode.Open);
            //return new FileStreamResult(stream, "application/pdf");

            // Response...
          

            // return File(System.IO.File.ReadAllBytes(file), "application/pdf");
            //return File(Filename, "application/pdf");
            //return File(response, "application/pdf", Filename);



            //string URL = "http://localhost:1688/api/Regis/generatepdf";
            //URL = URL + "&SRNo=122&rs:Command=Render&rs:Format=pdf";
            //System.Net.HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);

            ////Req.Method = "GET";
            ////string path = @ "E:\New folder\Test.pdf";
            //System.Net.WebResponse objResponse = Req.GetResponse();
            //System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
            //System.IO.Stream stream = objResponse.GetResponseStream();
            //byte[] buf = new byte[1024];
            //int len = stream.Read(buf, 0, 1024);

            //while (len > 0)
            //{
            //    fs.Write(buf, 0, len);
            //    len = stream.Read(buf, 0, 1024);
            //}
            //stream.Close();
            //fs.Close();

          
        }

        public async Task<IActionResult> Sport_Registration()
        {
            List<Spot> pc = new List<Spot>();
            List<Spot> pc1 = new List<Spot>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Club").Result;
            HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "/Regis").Result;

            if (response.IsSuccessStatusCode && response1.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                string data1 = response1.Content.ReadAsStringAsync().Result;
                pc1 = JsonConvert.DeserializeObject<List<Spot>>(data1);
                pc = JsonConvert.DeserializeObject<List<Spot>>(data);
                pc.Insert(0, new Spot { club_id = 0, club_name = "Select One" });
            }
          
            ViewBag.UnitName = pc;

            ViewBag.Result =pc1; 

            return View();
        }
        [HttpPost]
        public IActionResult UploadImage(IFormFile MyUploader)
        {
            if (MyUploader != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "prodimage");
                string filePath = Path.Combine(uploadsFolder, MyUploader.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    MyUploader.CopyTo(fileStream);
                }
                return new ObjectResult(new { status = "success" });
            }
            return new ObjectResult(new { status = "fail" });

        }
        [HttpPost]
        public IActionResult Add(Spot entity)
        {
            string[] files = entity.image_path.Split('\\');
            entity.image_path = "prodimage/" + files[files.Length - 1];
            string data = JsonConvert.SerializeObject(entity);
            HttpResponseMessage response;
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");


            response = client.PutAsync(client.BaseAddress + "/Regis/" + entity.Registration_Id, content).Result;
            if (response.IsSuccessStatusCode)
            {  if(entity.Registration_Id==0)
                 return Json("Record Saved Successfully");
               else
                   return Json("Record Update Successfully");
            }
            return View();
        }
        [HttpPost]
      

        [HttpGet]
        public async Task<IActionResult> GetSubCatByCId(int clubid)
        {
           
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Sports/" + clubid).Result;
            List<SelectListItem> scatlist = new List<SelectListItem>();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var lstcat = JsonConvert.DeserializeObject<List<Spot>>(data);
                foreach (Spot dr in lstcat)
                {
                    scatlist.Add(new SelectListItem { Text = dr.sprot_name, Value = dr.sport_Id.ToString() });
                }
            }
            var jsonres = JsonConvert.SerializeObject(scatlist);
            return Json(jsonres);

        }


        [HttpGet]
        public IActionResult MedicineGetById(int id)
        {

            Spot prod = new Spot();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Regis/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                prod = JsonConvert.DeserializeObject<Spot>(data);
            }
            //return Ok(JsonConvert.SerializeObject(prod));

            //var Doctors = log.GetById(Convert.ToInt32(id)).Result;



            return Ok(JsonConvert.SerializeObject(prod));
        }

    }
}
