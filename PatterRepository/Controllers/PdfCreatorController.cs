﻿using System.IO;
using System.Linq;
using Contracts;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using PatterRepository.Utility;

namespace PatterRepository.Controllers
{
    [Route("api/PdfCreator")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {
        private IConverter _converter;
        private readonly TemplateGenerator _templateGenerator;

        //private readonly TemplateGenerator _templateGenerator;
        public PdfCreatorController(IConverter converter, TemplateGenerator templateGenerator)
        {
            _converter = converter;
            _templateGenerator = templateGenerator;
        }

        
        [HttpGet]
        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
               // Out = @"D:\PDFCreator\Employee_Report.pdf"  //USE THIS PROPERTY TO SAVE PDF TO A PROVIDED LOCATION
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = _templateGenerator.GetHTMLString(),
                //Page = "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            //_converter.Convert(pdf); IF WE USE Out PROPERTY IN THE GlobalSettings CLASS, THIS IS ENOUGH FOR CONVERSION
            var file = _converter.Convert(pdf);
            ////return Ok("Successfully created PDF document.");
            return File(file, "application/pdf", "EmployeeReport.pdf");
            //return Ok("file");
        }
    }
}