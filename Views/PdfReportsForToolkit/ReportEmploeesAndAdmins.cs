using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using CulturalSiberiaProject.ViewModels;

namespace CulturalSiberiaProject.Views.PdfReportsForToolkit;

public class ReportEmploeesAndAdmins
{
    public static void PrintPdfReport(List<User> data)
    {
        string dest = "output.pdf";
        PdfWriter writer = new PdfWriter(dest);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);

        string adminRole = "Администратор";
        string employeeRole = "Сотрудник";
        string clientRole = "Пользователь";

        Table table = new Table(6);
        table.AddHeaderCell("Имя");
        table.AddHeaderCell("Фамилия");
        table.AddHeaderCell("Отчество");
        table.AddHeaderCell("Электронная почта");
        table.AddHeaderCell("Логин");
        table.AddHeaderCell("Роль");

        foreach (var user in data)
        {
            table.AddCell(user.Fname);
            table.AddCell(user.Lname);
            table.AddCell(string.IsNullOrWhiteSpace(user.Mname) ? "" : user.Mname);
            table.AddCell(string.IsNullOrWhiteSpace(user.Email) ? "" : user.Email);
            table.AddCell(user.Login); 
            if (user.Userrole == "Admin")
            {
                table.AddCell(adminRole);
            }
            else if (user.Userrole == "Employee")
            {
                table.AddCell(employeeRole);
            }
            else
            {
                table.AddCell(clientRole);
            }
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
