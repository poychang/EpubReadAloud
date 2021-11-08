// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;
using System.Text;
using VersOne.Epub;

var filePath = Path.Combine(AppContext.BaseDirectory, "Epub\\sample.epub");
Console.WriteLine(filePath);


var book = EpubReader.ReadBook(filePath);
var bookcontent = new StringBuilder();
//foreach (var textContentFile in book.ReadingOrder.Take(3))
//{
//    var htmlDocument = new HtmlDocument();
//    htmlDocument.LoadHtml(textContentFile.Content);
//    var contentText = new StringBuilder();

//    foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//text()"))
//    {
//        contentText.AppendLine(node.InnerText.Trim());
//    }

//    bookcontent.Append(contentText.ToString());
//}

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

foreach (var textContentFile in book.ReadingOrder.Take(3))
{
    var htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(textContentFile.Content);
    var contentText = new StringBuilder();

    contentText.AppendLine(htmlDocument.DocumentNode.SelectNodes("/html/body/div[@class='main']").First().InnerText);

    bookcontent.Append(contentText.ToString());
}

Console.WriteLine(bookcontent.ToString());

