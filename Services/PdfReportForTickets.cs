using System.Collections.Generic;
using System.Diagnostics;
using CulturalSiberiaProject.Models;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace CulturalSiberiaProject.Services;

public class PdfReportForTickets
{
    public static void PrintPdfReport(List<Ticket> data)
    {
        string dest = "output.pdf";
        PdfWriter writer = new PdfWriter(dest);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);

        Table table = new Table(3);
        table.AddHeaderCell("Название");
        table.AddHeaderCell("Дата");
        table.AddHeaderCell("Цена");

        foreach (var ticket in data)
        {
            table.AddCell(ticket.Eventname);
            table.AddCell(ticket.Eventdate.ToString());
            table.AddCell(ticket.Price.ToString());
        }

        string fontDest = 
            @"C:\Users\Akame\RiderProjects\CulturalSiberiaProject\CulturalSiberiaProject\FontForPdfReports\pt-astra-serif_regular.ttf";
        PdfFont font = PdfFontFactory.CreateFont(fontDest, PdfEncodings.IDENTITY_H);

        document.SetFont(font);
        document.Add(table);
        document.Close();

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = dest,
            UseShellExecute = true
        };

        System.Diagnostics.Process.Start(startInfo);
    }
}