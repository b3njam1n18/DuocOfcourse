using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Api.Core.DTO;
using System;

namespace Api.Service.Documents
{
    public class CourseCertificateDocument : IDocument
    {
        public CourseCertificateData Data { get; }

        public CourseCertificateDocument(CourseCertificateData data)
        {
            Data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));

                page.Content().Column(col =>
                {
                    col.Spacing(20);

                    col.Item().Text("INSTITUTO PROFESIONAL DUOC UC")
                        .SemiBold().FontSize(16);

                    col.Item().AlignCenter().Text("CERTIFICADO DE APROBACIÓN DE CURSO")
                        .Bold().FontSize(18).FontColor(Colors.Blue.Medium);

                    col.Item().Text(txt =>
                    {
                        txt.Span("Se certifica que ").NormalWeight();
                        txt.Span(Data.StudentFullName.ToUpperInvariant())
                           .Bold();

                        txt.Span(" ha aprobado el curso ").NormalWeight();
                        txt.Span(Data.CourseName.ToUpperInvariant())
                           .Bold();

                        txt.Span($" con nota final ").NormalWeight();
                        txt.Span(Data.FinalGrade.ToString("0.0")).Bold();
                        txt.Span(" (escala de 1.0 a 7.0).");
                    });

                    col.Item().Text($"Estado: {Data.StatusText}")
                        .Bold();

                    col.Item().Text(txt =>
                    {
                        txt.Span("Profesor responsable: ");
                        txt.Span(string.IsNullOrWhiteSpace(Data.TeacherName)
                            ? "Profesor/a"
                            : Data.TeacherName);
                    });

                    col.Item().Text(
                        $"Fecha de emisión: {Data.IssuedAt:dd 'de' MMMM 'de' yyyy}"
                    );

                    col.Item().Text($"Código de verificación: {Data.CertificateCode}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken2);

                    col.Item().PaddingTop(60).Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("_____________________________");
                            c.Item().Text("FIRMA DIRECTOR").SemiBold();
                            c.Item().Text("Duoc UC").FontSize(10);
                        });
                    });
                });
            });
        }
    }
}
