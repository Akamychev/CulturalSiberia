using System.Collections.Generic;
using System.Diagnostics;
using CulturalSiberiaProject.Models;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace CulturalSiberiaProject.Views.PdfReportsForToolkit;

public class ReportShowPiece
{
    public static void PrintPdfReport(List<Showpiece> data)
    {
        string dest = "output.pdf";
        PdfWriter writer = new PdfWriter(dest);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);
        
        string yesForTrue = "Да";
        string noForFalse = "Нет";
    
        Table table = new Table(6);
        table.AddHeaderCell("Название");
        table.AddHeaderCell("Оригинальность");
        table.AddHeaderCell("Тема");
        table.AddHeaderCell("Цена в $");
        table.AddHeaderCell("Дата создания");
        table.AddHeaderCell("История");
    
        foreach (var showPiece in data)
        {
            table.AddCell(showPiece.Nameing);
            
            if (showPiece.Originality.HasValue)
            {
                table.AddCell(showPiece.Originality.Value ? yesForTrue : noForFalse);
            }
            else
                table.AddCell("");
            
            table.AddCell(showPiece.Subject);
            table.AddCell(showPiece.Price.ToString());
            table.AddCell(showPiece.Borndate.ToString());
            table.AddCell(showPiece.History);
        }
        
        string fontDest = @"C:\Users\Akame\RiderProjects\CulturalSiberiaProject\CulturalSiberiaProject\FontForPdfReports\pt-astra-serif_regular.ttf";
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