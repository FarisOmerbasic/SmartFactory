using SmartFactoryWebApi.Controllers;

namespace SmartFactoryWebApi.Services
{
    public interface IRenderPDFReport
    {
        public byte[] RednderProductionReport(RenderPDFRequest request);
    }
}
