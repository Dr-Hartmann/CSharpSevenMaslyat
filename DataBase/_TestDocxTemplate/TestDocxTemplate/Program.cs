using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using System.Reflection.Metadata;
using System.Linq;
using System;

namespace TestDocxTemplate
{
    internal class Program
    {
        static Dictionary<string, string> values = new() {
            {"{{institute}}", "Институт информационных технологий" },
            {"{{faculty}}", "МПО ЭВМ" },
            {"{{type}}", "КУРСОВАЯ РАБОТА" },
            { "{{class}}", "С#-программирование" },
            { "{{plot}}", "Blazor, Rest API, Entity Framework, PostrgreSQL, сериализация, работа с файлами"},
            { "{{group}}", "1ПИб-02-1оп-22" },
            {"{{code}}", "09.03.04" },
            {"{{specialization}}", "Программная инженерия" },
            {"{{fullname}}", "Микуцких Григорий Андреевич" },
            {"{{teacher}}", "Шаханов Н.И." },
            {"{{position}}", "доцент"},
            { "{{year}}", "2025"},
            { "{{annotation}}", $"Курсовая работа посвящена (SOME TEXT) разработке...\r\nВ ходе работы было ...\r\nВ работе присутствует введение в предметную область, …, сопровождение графическим материалом и диаграммами, код итоговой программы и результаты её тестирования."}
        };

        static void Main(string[] args)
        {
            File.Copy(@"./template.docx", @"./template1.docx", true);



            using (var doc = WordprocessingDocument.Open(@"./template1.docx", true))
            {
                var body = doc.MainDocumentPart!.Document.Body;

                foreach (var text in body!.Descendants<Text>())
                {
                    foreach (var replacement in values)
                    {
                        if (!text.InnerText.Contains(replacement.Key)) continue;
                        if (replacement.Value.Contains("\r\n"))
                        {
                            var run = (Run)text.Parent;
                            if (run == null) continue;
                            var paragraph = (Paragraph)run.Parent;
                            if (paragraph == null) continue;

                            var pPr = paragraph.ParagraphProperties;
                            var rPr = run.RunProperties;
                            int index = body.ToList().IndexOf(paragraph);
                            paragraph.Remove();

                            string[] newParagraphs
                                = replacement.Value.Split("\r\n", StringSplitOptions.None);

                            foreach (var newText in newParagraphs)
                            {
                                Paragraph newPar = new();
                                if (pPr != null)
                                {
                                    newPar.ParagraphProperties =
                                        (ParagraphProperties)pPr.CloneNode(true);
                                }

                                Run newRun = new();
                                if(rPr != null)
                                {
                                    newRun.RunProperties =
                                        (RunProperties)rPr.CloneNode(true);
                                }

                                newRun.Append(new Text(newText));
                                newPar.Append(newRun);
                                body.InsertAt(newPar, index++);
                            }
                        }
                        else
                        {
                            text.Text = text.Text.Replace(replacement.Key, replacement.Value);
                        }
                    }
                }

                doc.Save();
            }




            //using (var document = WordprocessingDocument.Open(@"./template1.docx", true))
            //{
            //    Body documentBody = document.MainDocumentPart!.Document.Body!;

            //    foreach (var text in documentBody.Descendants<Text>())
            //    {
            //        foreach (var pair in values)
            //        {
            //            if (text.Text.Contains(pair.Key))
            //            {
            //                text.Text = text.Text.Replace(pair.Key, pair.Value);
            //            }
            //        }
            //    }
            //    document.Save();
            //}




            //using (var document = WordprocessingDocument.Open(@"./template1.docx", true))
            //{
            //    Body documentBody = document.MainDocumentPart!.Document.Body!;
            //    List<Paragraph> paragraphsWithMarks = documentBody.Descendants<Paragraph>().ToList();

            //    foreach (Paragraph paragraph in paragraphsWithMarks)
            //    {
            //        foreach (var pair in values)
            //        {
            //            var newText = paragraph.InnerText.Replace(pair.Key, pair.Value);
            //            paragraph.RemoveAllChildren<Run>();
            //            paragraph.AppendChild<Run>(new Run(new Text(newText)));
            //        }
            //    }
            //}

            //if(doc. содержит Any Regex(@"[^{{\s*\S*}}$]")
        }
    }
}
