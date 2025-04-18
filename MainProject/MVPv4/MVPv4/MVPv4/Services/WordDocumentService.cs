//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;
//using System.Text;

//namespace MVPv4.Client.Services;

//public class WordDocumentService
//{
//    public string ReadWordDocument(byte[] fileContent)
//    {
//        using (var wordDocument = WordprocessingDocument.Open(new MemoryStream(fileContent), false))
//        {
//            var body = wordDocument.MainDocumentPart?.Document.Body;
//            if (body == null) return string.Empty;

//            return GetTextFromBody(body);
//        }
//    }

//    private string GetTextFromBody(Body body)
//    {
//        var text = new StringBuilder();

//        foreach (var paragraph in body.Elements<Paragraph>())
//        {
//            foreach (var run in paragraph.Elements<Run>())
//            {
//                foreach (var textElement in run.Elements<Text>())
//                {
//                    text.Append(textElement.Text);
//                }
//            }
//            text.AppendLine(); // Добавляем перенос строки после параграфа
//        }

//        return text.ToString();
//    }
//}
