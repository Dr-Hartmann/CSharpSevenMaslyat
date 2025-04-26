using System;
using System.IO;

// У меня не сработало
// Install-Package DocumentFormat.OpenXml
// https://learn.microsoft.com/ru-ru/office/open-xml/open-xml-sdk
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace HtmlToDocx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateFolderInMyDocumentsAndGetPath(@"SevenMaslyat", out string rootFolderPath);

            string wordFileName = @"MyDocument.docx";
            string fullPath = Path.Combine(rootFolderPath, wordFileName);

            CreateDocxFile(ref fullPath); 
        }

        #region СОЗДАНИЕ ПАПКИ В МОИ ДОКУМЕНТЫ
        private static void CreateFolderInMyDocumentsAndGetPath(string newRootFolder, out string rootFolderPath)
        {
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            rootFolderPath = Path.Combine(myDocumentsPath, newRootFolder);
            if (!Directory.Exists(rootFolderPath))
            {
                Directory.CreateDirectory(rootFolderPath);
                Console.WriteLine($"Папка успешно создана: {rootFolderPath}");
            }
            else
            {
                Console.WriteLine($"Папка уже существует: {rootFolderPath}");
            }
        }
        #endregion

        #region Open XML SDK (не требует word)
        private static void CreateDocxFile(ref string path)
        {
            // Создаем новый документ
            using (WordprocessingDocument doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
            {
                // Добавляем основной документ (MainDocumentPart)
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();


                // Добавляем заголовок
                Paragraph para = new Paragraph();
                Run run = new Run();
                run.AppendChild(new Text("Привет, это мой документ!"));
                run.RunProperties = new RunProperties(new Bold());
                para.AppendChild(run);
                body.AppendChild(para);


                // Добавляем обычный текст
                para = new Paragraph();
                run = new Run();

                run.AppendChild(new Text("Это пример использования Open XML SDK.\n"));
                run.AppendChild(new Text("Этот текст добавлен программно."));
                para.AppendChild(run);
                body.AppendChild(para);


                // Создаем таблицу
                Table table = new Table();
                TableRow row = new TableRow();
                for (int i = 0; i < 3; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Append(
                        new Paragraph(
                            new Run(
                                new Text($"Ячейка {i + 1}")
                                )
                            )
                        );
                    row.Append(cell);
                }
                table.Append(row);


                // Сохраняем изменения
                // Добавляем таблицу в документ
                body.Append(table);
                mainPart.Document.AppendChild(body);
                mainPart.Document.Save();

                // Читаем текст из всех абзацев
                foreach (Paragraph item in body.Elements<Paragraph>())
                {
                    Console.WriteLine(item.InnerText);
                }
            }
            Console.WriteLine("Документ успешно создан!");
        }
        #endregion
    }
}