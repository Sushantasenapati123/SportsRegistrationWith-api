using Exam.Domain.Sports;
using Exam.Irepository.ISport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace SportsApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisController : ControllerBase
    {
        private readonly SpotInterface _SportService;
        public RegisController(SpotInterface prodService)
        {
            _SportService = prodService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spot>>> GetallProduct()
        {
            return await _SportService.GetAll( new Spot());
        }
        [HttpGet("{pid}")]
        public async Task<ActionResult<Spot>> GetallProductbyid(int pid)
        {
            return await _SportService.GetById(pid);
        }

        [HttpPut("{pid}")]
        public async Task<ActionResult<Spot>> PostProduct(int pid, Spot product)
        {
            if (pid != product.Registration_Id)
            {
                return BadRequest();
            }

            await _SportService.insert(product);

            return CreatedAtAction("GetallProduct", new { pid = product.Registration_Id }, product);
        }

        [HttpGet("generatepdf")]
        public async Task<IActionResult> GeneratePDF()
        {
            PdfSharpCore.Pdf.PdfDocument document = new PdfSharpCore.Pdf.PdfDocument();
           
            string[] copies = { "Employee copy"/*, "Comapny Copy" */};
            for (int i = 0; i < copies.Length; i++)
            {
                List<Spot> detail = await this._SportService.GetAll(new Spot());
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
            return File(response, "application/pdf", Filename);
        }

    }
}
