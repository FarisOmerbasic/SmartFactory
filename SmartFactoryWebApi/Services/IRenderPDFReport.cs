using SmartFactoryWebApi.Controllers;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Models;

namespace SmartFactoryWebApi.Services
{
    public interface IRenderPDFReport
    {
        public byte[] RednderProductionReport(RenderPDFRequest request);
        public byte[] RenderUserRegisterReport(User request);
    }
}
