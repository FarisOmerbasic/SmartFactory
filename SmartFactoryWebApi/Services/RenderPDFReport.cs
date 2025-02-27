using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using SmartFactoryWebApi.Models;
using System.Text;
using SmartFactoryWebApi.Controllers;

namespace SmartFactoryWebApi.Services
{
    public class RenderPDFReport : IRenderPDFReport
    {
        public RenderPDFReport()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }

        //List<(int Id, string Name, string Category, double Price)> data = new List<(int Id, string Name, string Category, double Price)>
        //{
        //    (1, "Smart Sensor", "IoT", 199.99),
        //    (2, "Industrial Robot Arm", "Automation", 4999.50),
        //    (3, "Conveyor Belt", "Logistics", 1299.99),
        //    (4, "AI Vision System", "Quality Control", 2999.00),
        //};


        public byte[] RednderProductionReport(RenderPDFRequest request)
        {
           var document= Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    // Header Section
                    page.Header()
                        .AlignCenter()
                        .Column(column =>
                        {
                            column.Spacing(5);

                            // Logo
                            column.Item().Width(100).AlignCenter().Image("Images\\FactoryLogo.png");
                        });

                    // Content Section
                    page.Content()
                        .AlignCenter()
                        .Column(column =>
                        {
                            column.Spacing(15);

                            column.Item().AlignCenter().PaddingBottom(10).Width(450).LineHorizontal(2);

                            column.Item().AlignCenter().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(180); 
                                    columns.ConstantColumn(100); 
                                });

                                // Table Rows
                                table.Cell().Padding(2).Text("Today's project output:").Bold().FontSize(13);
                                table.Cell().Padding(2).Text($"{request.TodayOutput}").SemiBold(); 

                                table.Cell().Padding(2).Text("Week's projection:").Bold().FontSize(13);
                                table.Cell().Padding(2).Text($"{request.WeekOutput}").SemiBold(); 
                            });


                            column.Item().AlignCenter().PaddingBottom(10).Width(450).LineHorizontal(2);

                            column.Item().AlignCenter().PaddingBottom(20)
                                .Text("Factory Line List")
                                .Bold()
                                .FontSize(20);



                            column.Item().AlignCenter().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(50); 
                                    columns.RelativeColumn();    
                                    columns.RelativeColumn();    
                                });

                                // Table Header
                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.LightBlue.Darken2).Border(1).AlignCenter().Text("Line").Bold();
                                    header.Cell().Background(Colors.LightBlue.Darken2).Border(1).AlignCenter().Text("Machine").Bold();
                                    header.Cell().Background(Colors.LightBlue.Darken2).Border(1).AlignCenter().Text("Issue").Bold();
                                    //header.Cell().Background(Colors.LightBlue.Darken2).Border(1).AlignCenter().Text("Price ($)").Bold();
                                });

                                // Table Rows
                                foreach (var line in request.LineA)
                                {
                                    table.Cell().Border(1).AlignCenter().Text(line.Line);
                                    table.Cell().Border(1).AlignCenter().Text(line.Machine);
                                    table.Cell().Border(1).AlignCenter().Text(line.Issue);
                                    //table.Cell().Border(1).AlignCenter().Text(Price.ToString("F2"));
                                }
                                foreach (var line in request.LineB)
                                {
                                    table.Cell().Border(1).AlignCenter().Text(line.Line);
                                    table.Cell().Border(1).AlignCenter().Text(line.Machine);
                                    table.Cell().Border(1).AlignCenter().Text(line.Issue);
                                    //table.Cell().Border(1).AlignCenter().Text(Price.ToString("F2"));
                                }
                                foreach (var line in request.LineC)
                                {
                                    table.Cell().Border(1).AlignCenter().Text(line.Line);
                                    table.Cell().Border(1).AlignCenter().Text(line.Machine);
                                    table.Cell().Border(1).AlignCenter().Text(line.Issue);
                                    //table.Cell().Border(1).AlignCenter().Text(Price.ToString("F2"));
                                }
                            });

                        });

                    // Footer Section
                    page.Footer()
                        .Column(column =>
                        {
                            column.Item()
                                .PaddingVertical(10)
                                .Text(text =>
                                {
                                    text.Span("Page ");
                                    text.CurrentPageNumber();
                                    text.Span(" of ");
                                    text.TotalPages();
                                    text.AlignCenter();
                                });
                        });
                });
            });

            //document.ShowInCompanion();
            return document.GeneratePdf();
        }

    }


}
