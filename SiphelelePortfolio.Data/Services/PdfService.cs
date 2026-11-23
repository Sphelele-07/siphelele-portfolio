using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SiphelelePortfolio.Data.Models.Domain;

namespace SiphelelePortfolio.Data.Services;

public class PdfService : IPdfService
{
    private const string Gold = "#b8942e";
    private const string DarkGreen = "#1e3a2f";

    public PdfService() { QuestPDF.Settings.License = LicenseType.Community; }

    public byte[] GenerateRequirementsPdf(Project project)
    {
        bool isMouzie = project.Title.Contains("Mouzie", StringComparison.OrdinalIgnoreCase);
        bool isMerrydale = project.Title.Contains("Merrydale", StringComparison.OrdinalIgnoreCase);

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Column(col =>
                {
                    col.Item().Text("Siphelele Ndzundzu").FontSize(20).Bold().FontColor(DarkGreen);
                    col.Item().Text("Software Requirements & Test Guide").FontSize(12).FontColor(Colors.Grey.Darken1);
                    col.Item().PaddingTop(8).LineHorizontal(1).LineColor(Gold);
                });

                page.Content().PaddingTop(16).Column(col =>
                {
                    col.Spacing(12);
                    col.Item().Text($"Project: {project.Title}").FontSize(16).Bold().FontColor(DarkGreen);
                    col.Item().Text(project.Type).FontSize(11).Italic().FontColor(Colors.Grey.Darken1);

                    col.Item().PaddingTop(8).Text("1. Overview").Bold().FontColor(Gold);
                    col.Item().Text(project.Solution);
                    col.Item().PaddingTop(8).Text("2. Problem Statement").Bold().FontColor(Gold);
                    col.Item().Text(project.Problem);
                    col.Item().PaddingTop(8).Text("3. Expected Result").Bold().FontColor(Gold);
                    col.Item().Text(project.Result);
                    col.Item().PaddingTop(8).Text("4. Tech Stack").Bold().FontColor(Gold);
                    col.Item().Text(project.Tags);

                    col.Item().PaddingTop(16).Text("5. Test Credentials").Bold().FontColor(Gold).FontSize(13);
                    col.Item().PaddingTop(6).Background(Colors.Grey.Lighten4).Padding(10).Column(c =>
                    {
                        if (isMouzie)
                        {
                            c.Item().Text("Admin Login").Bold();
                            c.Item().Text("Email: ta.shoes@mouzie.co.za").FontSize(10);
                            c.Item().Text("Password: Mouzie2026!").FontSize(10);
                            c.Item().PaddingTop(6).Text("Guest Booking").Bold();
                            c.Item().Text("Use SA ID: 8501015009087 (test)").FontSize(10);
                        }
                        else if (isMerrydale)
                        {
                            c.Item().Text("Admin Login").Bold();
                            c.Item().Text("Email: admin@merrydale.co.za").FontSize(10);
                            c.Item().Text("Password: Admin2026!").FontSize(10);
                            c.Item().PaddingTop(6).Text("Pharmacist Login").Bold();
                            c.Item().Text("Email: pharmacist@merrydale.co.za").FontSize(10);
                            c.Item().Text("Password: Pharm2026!").FontSize(10);
                        }
                        else
                        {
                            c.Item().Text("Credentials provided on request.").FontSize(10).Italic();
                        }
                    });

                    col.Item().PaddingTop(16).Text("6. Manual Test Cases").Bold().FontColor(Gold).FontSize(13);
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(30); c.RelativeColumn(3);
                            c.RelativeColumn(3); c.RelativeColumn(2);
                        });
                        table.Header(h =>
                        {
                            h.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("#").Bold().FontSize(9);
                            h.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Action").Bold().FontSize(9);
                            h.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Expected").Bold().FontSize(9);
                            h.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Result").Bold().FontSize(9);
                        });

                        var tests = isMouzie
                            ? new[] {
                                ("1","Open home","2 room cards visible",""),
                                ("2","Click 'Book Now'","Booking form opens",""),
                                ("3","Submit empty form","Validation errors",""),
                                ("4","Enter valid booking","Success with MZ code",""),
                                ("5","Check inbox","Guest + Admin emails",""),
                                ("6","Admin login","Dashboard loads",""),
                                ("7","Approve booking","Invoice emailed",""),
                                ("8","Check-in guest","Room = Occupied",""),
                                ("9","Check-out guest","Room = Available",""),
                                ("10","Damage report","Appears on /Admin/Damages","")
                            }
                            : isMerrydale
                            ? new[] {
                                ("1","Pharmacist login","Dashboard loads",""),
                                ("2","Add medicine","Shows in inventory",""),
                                ("3","Duplicate medicine","Rejected",""),
                                ("4","Add patient","Shows in list",""),
                                ("5","Create prescription","Stock reduces",""),
                                ("6","Admin login","Admin dashboard loads",""),
                                ("7","Generate report","PDF downloads",""),
                                ("8","Delete a user","Cannot log in",""),
                                ("9","Schedule 5 sale","Requires prescription",""),
                                ("10","Low stock","Red alert","")
                            }
                            : new[] { ("1", "Open demo", "App loads", "") };

                        foreach (var (n, a, e, r) in tests)
                        {
                            table.Cell().BorderBottom(0.5f).Padding(4).Text(n).FontSize(9);
                            table.Cell().BorderBottom(0.5f).Padding(4).Text(a).FontSize(9);
                            table.Cell().BorderBottom(0.5f).Padding(4).Text(e).FontSize(9);
                            table.Cell().BorderBottom(0.5f).Padding(4).Text(r).FontSize(9);
                        }
                    });

                    col.Item().PaddingTop(20).Text("Prepared by Siphelele Ndzundzu").FontSize(10).Italic();
                });

                page.Footer().AlignCenter().Text(t =>
                {
                    t.Span("Siphelele Portfolio · ").FontSize(8).FontColor(Colors.Grey.Medium);
                    t.Span(project.Title).FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        }).GeneratePdf();
    }
}