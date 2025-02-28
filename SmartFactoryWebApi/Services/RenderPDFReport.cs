using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using SmartFactoryWebApi.Models;
using System.Text;
using SmartFactoryWebApi.Controllers;
using SmartFactoryWebApi.Dtos;
using System.Xml.Linq;

namespace SmartFactoryWebApi.Services
{
    public class RenderPDFReport : IRenderPDFReport
    {
        public RenderPDFReport()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }


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

        public byte[] RenderUserRegisterReport(User request)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    page.Header()
                        .AlignCenter()
                        .Column(column =>
                        {
                            column.Spacing(0);

                            //Logo
                            column.Item().Width(100).AlignCenter().Image("Images\\FactoryLogo.png");

                            //Company Name
                            //column.Item().AlignCenter().PaddingBottom(25).Text("Lend A Car")
                            //    .FontSize(16)
                            //    .Bold();

                        });


                    page.Content()
                        .AlignCenter()
                        .Column(column =>
                        {
                            column.Spacing(15);

                            column.Item().AlignCenter().PaddingBottom(10).Width(350).LineHorizontal(2);


                            column.Item().AlignCenter().PaddingBottom(20).Text($"Welcome to the company {request.FirstName}")
                                .Bold()
                                .FontSize(25);


                            column.Item().AlignCenter().PaddingBottom(20).Text("Employee System Access Data")
                                .SemiBold()
                                .FontSize(17);



                            column.Item().AlignCenter().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(120); //Email Column 
                                    columns.ConstantColumn(120); //Password Column 
                                });

                                //Table Header
                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.LightBlue.Darken2).Border(1).AlignCenter().Text("Email").Bold();
                                    header.Cell().Background(Colors.LightBlue.Darken2).Border(1).AlignCenter().Text("Password").Bold();
                                });

                                //Table Rows 
                                table.Cell().Border(1).AlignCenter().Text(request.Email);
                                table.Cell().Border(1).AlignCenter().Text(request.Password);
                            });

                            //Warning message
                            column.Item().PaddingTop(20).AlignCenter().Text("Note that this information is highly confidential, don't share it with anyone!").FontColor(Colors.Red.Medium)
                            .FontSize(10);
                        });

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

            return document.GeneratePdf();
        }
    }


}
