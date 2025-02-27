using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Services;
using System.Reflection.Metadata.Ecma335;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFReportController : ControllerBase
    {
        private readonly IRenderPDFReport _renderPDF;
        public PDFReportController(IRenderPDFReport renderPDF)
        {
            _renderPDF = renderPDF;
        }

        [HttpPost("GeneratePdf")]
        public ActionResult<string> GeneratePDF(RenderPDFRequest request)
        {
            var pdf = _renderPDF.RednderProductionReport(request);

            return File(pdf, "application/pdf", $"report_day_{DateOnly.FromDateTime(DateTime.Now)}.pdf");
        }
    }


    public class RenderPDFRequest()
    {
        public List<ProductionDto> LineA { get; set; }
        public List<ProductionDto> LineB { get; set; }
        public List<ProductionDto> LineC { get; set; }

        public double TodayOutput { get; set; }
        public double WeekOutput { get; set; }

    }
}
