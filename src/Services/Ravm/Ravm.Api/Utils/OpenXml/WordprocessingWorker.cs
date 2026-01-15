namespace Ravm.Api.Utils.OpenXml;

using DocumentFormat.OpenXml;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Ravm.Api.Extensions;
using System.Collections;

public class WordprocessingWorker
{

    #region Fields

    private WordprocessingDocument _document = null;
    private readonly Stream _stream;

    #endregion

    #region Properties
    public string TextColor { get; set; }
    public string OddRowColor { get; set; }
    public string EvenRowColor { get; set; }
    public string BorderColor { get; set; }

    #endregion

    #region Ctor

    private WordprocessingWorker(Stream stream, byte[] sourceBytes)
    {
        ArgumentNullException.ThrowIfNull(stream, "wordprocessingStream");

        if (sourceBytes != null)
            stream.Write(sourceBytes, 0, sourceBytes.Length);

        _stream = stream;
        _document = WordprocessingDocument.Open(_stream, true);
    }

    public WordprocessingWorker(byte[] sourceBytes)
        : this(new MemoryStream(), sourceBytes)
    { }

    public WordprocessingWorker(string sourceFileName)
        : this(File.ReadAllBytes(sourceFileName))
    {
    }

    #endregion



    #region Methods

    public void AddPropertyKeyValue(string key, string value)
    {
        _document.InsertText(key, value);
        _document.RemoveSdtBlock(key);
        _document.MainDocumentPart.Document.Save();
    }

    public void Save()
    {
        _document.Save();
    }

    public void Close()
    {
        _document.Close();
    }


    public Stream GetStream()
    {
        _stream.Seek(0, SeekOrigin.Begin);
        return _stream;
    }


    public void Dispose()
    {
        if (_document != null)
        {
            _document.Dispose();
            _document = null;
        }
    }

    public void AddPictureProperty(string pictureKey, string picturePath)
    {

        var cc = _document.MainDocumentPart.Document.Body.Descendants<SdtElement>()
            .FirstOrDefault(c =>
            {
                var p = c.Elements<SdtProperties>().FirstOrDefault();
                if (p != null)
                {
                    // Is it a picture content control?
                    var pict = p.Elements<SdtContentPicture>().FirstOrDefault();
                    if (pict != null && p.GetFirstChild<Tag>()?.Val == pictureKey)
                        return true;
                }
                return false;
            });
        string embed = null;
        if (cc != null)
        {
            var dr = cc.Descendants<Drawing>().FirstOrDefault();
            if (dr != null)
            {
                var blip = dr.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().FirstOrDefault();
                if (blip != null)
                    embed = blip.Embed;
            }
        }
        if (embed != null)
        {
            var idpp = _document.MainDocumentPart.Parts
                .Where(pa => pa.RelationshipId == embed).FirstOrDefault();
            if (idpp != null)
            {
                var ip = (ImagePart)idpp.OpenXmlPart;
                using (var fileStream =
                    File.Open(picturePath, FileMode.Open))
                    ip.FeedData(fileStream);
            }
        }

        _document.RemoveSdtBlock(pictureKey);
    }


    public void FillTable(IEnumerable source, string bookmarkStartName, bool setBackGroundToRow = false, bool setBorderColor = false)
    {
        if (source == null)
            throw new ArgumentNullException("source");

        BookmarkStart bookmarkStart = null;
        Table table = null;

        if ((bookmarkStart = _document.MainDocumentPart.RootElement.Descendants<BookmarkStart>().FirstOrDefault(a => a.Name == bookmarkStartName)) != null &&
            (table = bookmarkStart.Ancestors<Table>().FirstOrDefault()) != null)
        {
            var templateRow = bookmarkStart.Ancestors<TableRow>().FirstOrDefault();
            object firstElement = null;

            foreach (var item in source)
            {
                firstElement = item;
                break;
            }

            if (firstElement != null)
            {
                var templateRowClone = (TableRow)templateRow.Clone();

                foreach (var bookMarkStartInClone in templateRowClone.Descendants<BookmarkStart>())
                    bookMarkStartInClone.Remove();

                foreach (var paragraph in templateRowClone.Descendants<Paragraph>())
                    CollectTextsToFirstTextAndReturn(paragraph);

                var type = firstElement.GetType();
                var index = 0;
                IDictionary<int, string> mergeableCells = new Dictionary<int, string>();

                foreach (var item in source)
                {
                    var row = (TableRow)templateRowClone.Clone();
                    var cellIndex = 0;

                    foreach (var cell in row.Elements<TableCell>())
                    {

                        var tcp = cell.TableCellProperties;
                        if (setBackGroundToRow)
                        {

                            // Add cell shading.
                            var shading = new Shading()
                            {
                                Color = TextColor,
                                Fill = index % 2 == 0 ? EvenRowColor : OddRowColor,
                                Val = ShadingPatternValues.Clear,

                            };
                            tcp.Append(shading);
                        }
                        if (setBorderColor)
                        {
                            //// Create Table Borders
                            var tblBorders = new TableBorders();
                            var topBorder = new TopBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Thick),
                                Color = "CC0000"
                            };
                            tblBorders.AppendChild(topBorder);

                            var bottomBorder = new BottomBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Thick),
                                Color = "CC0000"
                            };
                            tblBorders.AppendChild(bottomBorder);

                            var rightBorder = new RightBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Thick),
                                Color = "CC0000"
                            };
                            tblBorders.AppendChild(rightBorder);

                            var leftBorder = new LeftBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Thick),
                                Color = "CC0000"
                            };
                            tblBorders.AppendChild(leftBorder);

                            var insideHBorder = new InsideHorizontalBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Thick),
                                Color = "CC0000"
                            };
                            tblBorders.AppendChild(insideHBorder);

                            var insideVBorder = new InsideVerticalBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Thick),
                                Color = "CC0000"
                            };
                            tblBorders.AppendChild(insideVBorder);
                            tcp.AppendChild(tblBorders);

                        }
                        // cell.Append(tcp);
                        foreach (var paragraph in cell.Descendants<Paragraph>())
                        {
                            var text = paragraph.Descendants<Text>().FirstOrDefault();

                            if (text != null)
                            {
                                if (Regex.IsMatch(text.Text, Constants.INDEX_PATTERN))
                                    text.Text = Regex.Replace(text.Text, Constants.INDEX_PATTERN, (index + 1).ToString());

                                SetTextFromType(text, type, item);

                                if (Regex.IsMatch(text.Text, Constants.MERGEABLE_PATTERN) && !mergeableCells.ContainsKey(cellIndex))
                                {
                                    text.Text = Regex.Replace(text.Text, Constants.MERGEABLE_PATTERN, string.Empty);
                                    mergeableCells.Add(cellIndex, text.Text);
                                    cell.TableCellProperties.AppendChild(new VerticalMerge { Val = MergedCellValues.Restart });

                                    var templateCell = templateRowClone.Elements<TableCell>().ElementAt(cellIndex);
                                    foreach (var cellParagraph in templateCell.Descendants<Paragraph>())
                                        cellParagraph.RemoveAllChildren();
                                    templateCell.TableCellProperties.AppendChild(new VerticalMerge());
                                }

                            }
                        }
                        cellIndex++;
                    }

                    table.InsertBefore(row, templateRow);
                    index++;
                }

                mergeableCells.Clear();
                mergeableCells = null;
            }

            table.RemoveChild(templateRow);
        }

    }

    public void BindTable<TRow>(object tableObject, string bookmarkStartName, bool mergeRowDictionaryProperties = true)
    {
        var type = tableObject.GetType();
        var rowType = typeof(TRow);
        IDictionary<string, TRow> dictionary = new Dictionary<string, TRow>();

        if (mergeRowDictionaryProperties)
        {
            var rowDicType = typeof(IDictionary<string, TRow>);
            var stringType = typeof(string);
            Type[] genericArgs = null;

            foreach (var prop in OfficeUtils.GetProperties(type))
            {
                var propType = prop.PropertyType;

                if (propType == rowType)
                    dictionary.Add(prop.Name, (TRow)prop.GetValue(tableObject, null));
                else if ((propType.Name == rowDicType.Name || propType.GetInterface(rowDicType.Name) != null) && (genericArgs = propType.GetGenericArguments()).Length == 2 &&
                        genericArgs[0] == stringType && genericArgs[1] == rowType)
                    foreach (var item in (IDictionary<string, TRow>)prop.GetValue(tableObject, null))
                        dictionary.Add(item.Key, item.Value);
            }
        }
        else
            foreach (var prop in OfficeUtils.GetProperties(type).Where(a => a.PropertyType == rowType))
                dictionary.Add(prop.Name, (TRow)prop.GetValue(tableObject, null));

        BindTable(dictionary, bookmarkStartName);
    }

    public void BindTable<TRow>(IDictionary<string, TRow> rowDictionary, string bookmarkStartName)
    {
        if (rowDictionary == null)
            throw new ArgumentNullException("rowDictionary");
        if (rowDictionary.Count == 0)
            return;

        var type = typeof(TRow);
        BookmarkStart bookmarkStart = null;
        Table table = null;

        if ((bookmarkStart = _document.MainDocumentPart.RootElement.Descendants<BookmarkStart>().FirstOrDefault(a => a.Name == bookmarkStartName)) != null &&
            (table = bookmarkStart.Ancestors<Table>().FirstOrDefault()) != null)
        {
            var templateRow = bookmarkStart.Ancestors<TableRow>().FirstOrDefault();
            var columnPatterns = new Dictionary<int, string>();
            var index = 0;

            foreach (var cell in templateRow.Elements<TableCell>())
            {
                var paragraph = cell.Elements<Paragraph>().FirstOrDefault();
                var text = CollectTextsToFirstTextAndReturn(paragraph);

                if (!(text == null || string.IsNullOrEmpty(text.Text)))
                    columnPatterns.Add(index, text.Text);

                index++;
            }
            index = 0;

            var nextRow = templateRow.NextSibling<TableRow>();
            table.RemoveChild(templateRow);

            while (nextRow != null)
            {
                var firstCell = nextRow.Elements<TableCell>().FirstOrDefault();
                var firstCellParagraph = firstCell.Elements<Paragraph>().FirstOrDefault();
                var firstCellText = CollectTextsToFirstTextAndReturn(firstCellParagraph);
                MatchCollection matches = null;

                if (firstCellText != null && (matches = Regex.Matches(firstCellText.Text, @"(\$F)?\[[\w]+\]")).Count > 0)
                {
                    var rowPropertyPattern = matches[0].Value;
                    var isFormalRow = rowPropertyPattern.StartsWith(@"$F");
                    var rowKeyIndex = rowPropertyPattern.IndexOf('[');
                    var rowKey = rowPropertyPattern.Substring(rowKeyIndex + 1, rowPropertyPattern.Length - rowKeyIndex - 2);

                    if (isFormalRow)
                        firstCellText.Text = firstCellText.Text.Replace(rowPropertyPattern, Constants.DEFAULT_FORMAT);

                    if (rowDictionary.ContainsKey(rowKey))
                    {
                        object rowObject = rowDictionary[rowKey];

                        foreach (var cell in nextRow.Elements<TableCell>())
                        {
                            if (columnPatterns.ContainsKey(index))
                            {
                                var columnPattern = columnPatterns[index];
                                Text cellText = null;

                                if (isFormalRow)
                                {
                                    cell.RemoveAllChildren<Paragraph>();
                                    cell.Append(firstCellParagraph.Clone() as Paragraph);
                                    cellText = cell.Elements<Paragraph>()
                                                   .FirstOrDefault()
                                                   .Descendants<Text>()
                                                   .FirstOrDefault();
                                    cellText.Text = string.Format(firstCellText.Text, columnPattern);
                                }
                                else
                                {
                                    foreach (var item in cell.Descendants<Text>())
                                        item.Text = string.Empty;
                                    var paragraph = cell.Elements<Paragraph>().FirstOrDefault();
                                    cellText = paragraph.Descendants<Text>().FirstOrDefault();

                                    if (cellText == null)
                                    {
                                        if (paragraph.ParagraphProperties == null)
                                            paragraph.ParagraphProperties = new ParagraphProperties();
                                        var run = paragraph.AppendChild(new Run());
                                        run.RunProperties = new RunProperties(paragraph.ParagraphProperties.Elements().Select(a => a.Clone() as OpenXmlElement));
                                        cellText = run.AppendChild(new Text());
                                    }

                                    cellText.Text = columnPattern;
                                }

                                SetTextFromType(cellText, type, rowObject);
                            }

                            index++;
                        }
                        index = 0;
                    }
                }

                nextRow = nextRow.NextSibling<TableRow>();
            }

            #region Remove first column
            var gridColumn = table.Elements<TableGrid>().FirstOrDefault().Elements<GridColumn>().FirstOrDefault();
            var tableWidth = table.Elements<TableProperties>().FirstOrDefault().Elements<TableWidth>().FirstOrDefault();

            tableWidth.Width = (int.Parse(tableWidth.Width) - int.Parse(gridColumn.Width)).ToString();
            gridColumn.Remove();
            foreach (var row in table.Elements<TableRow>())
                row.Elements<TableCell>().FirstOrDefault().Remove();
            #endregion

        }
    }

    private static Text CollectTextsToFirstTextAndReturn(OpenXmlElement parentElement)
    {
        var texts = parentElement.Descendants<Text>();
        var firstText = texts.FirstOrDefault();

        if (firstText != null)
            firstText.Text = string.Concat(texts.Select(a =>
            {
                var str = a.Text;
                a.Text = string.Empty;
                return str;
            }));

        return firstText;
    }

    private static void SetTextFromType(Text text, Type type, object obj)
    {
        text.Text = Regex.Replace(text.Text, Constants.FIELD_PATTERN, match =>
        {
            var propertyName = match.Value.Substring(1, match.Value.Length - 2);

            return OfficeUtils.GetProperyValue(type, obj, propertyName);
        });
    }
    #endregion

}
