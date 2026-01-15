namespace Ravm.Api.Extensions;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public static class WordprocessingDocumentExtensions
{

    public static WordprocessingDocument InsertText(this WordprocessingDocument doc, string contentControlTag, string text)
    {
        SdtElement element = doc.MainDocumentPart.Document.Body.Descendants<SdtElement>()
          .FirstOrDefault(sdt => sdt.SdtProperties.GetFirstChild<Tag>()?.Val == contentControlTag);

        if (element == null)
            throw new ArgumentException($"ContentControlTag \"{contentControlTag}\" doesn't exist.");

        element.Descendants<Text>().First().Text = text;
        element.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
        return doc;
    }

    public static WordprocessingDocument RemoveSdtBlock(this WordprocessingDocument doc, string contentControlTag)
    {
        SdtElement element = doc.MainDocumentPart.Document.Body.Descendants<SdtElement>()
          .FirstOrDefault(sdt => sdt.SdtProperties.GetFirstChild<Tag>()?.Val == contentControlTag);

        if (element == null)
            return doc;

        IEnumerable<OpenXmlElement> elements = null;

        if (element is SdtBlock)
            elements = (element as SdtBlock).SdtContentBlock.Elements();
        else if (element is SdtCell)
            elements = (element as SdtCell).SdtContentCell.Elements();
        else if (element is SdtRun)
            elements = (element as SdtRun).SdtContentRun.Elements();

        foreach (var el in elements)
            element.InsertBeforeSelf(el.CloneNode(true));
        element.Remove();
        return doc;
    }
}

