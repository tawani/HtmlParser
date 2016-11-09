namespace WhichMan.Utilities.HtmlParsing.Tests
{
    using HtmlParsing;
    using NUnit.Framework;

    [TestFixture]
    public class HtmlParserTests : BaseTest
    {
        [Test]
        public void Can_parse_html_string()
        {
            const string input = @"<h1>Table of Contents</h1>
            < !--This is a comment-->
            <ol style='color:blue;' align='left'>
                <li>The Great Gambler of <b>WhichMan</b>, INC. </li>
                <li checked >The Return of the Jedi</li>
                <li>Masters of the Universe</li>
            </ol>";

            var doc = HtmlParser.Parse(input);

            //System.IO.File.WriteAllText("C:\\temp\\tests\\parser_in.html", input);
            //System.IO.File.WriteAllText("C:\\temp\\tests\\parser_out.html", doc.ToString());
            //var html = doc.ToString();

            Assert.AreEqual(3, doc.ChildNodes.Count);

            var h1 = doc.ChildNodes[0] as HtmlElement;
            Assert.IsNotNull(h1);
            Assert.AreEqual(1, h1.ChildNodes.Count);

            var tn = h1.ChildNodes[0] as HtmlText;
            Assert.IsNotNull(tn);
            Assert.AreEqual("Table of Contents", tn.ToString());

            var c1 = doc.ChildNodes[1] as HtmlComment;
            Assert.IsNotNull(c1);

            var ol = doc.ChildNodes[2] as HtmlElement;
            Assert.IsNotNull(ol);
            Assert.AreEqual(2, ol.Attributes.Count);
            Assert.AreEqual(3, ol.ChildNodes.Count);

            var li = ol.ChildNodes[1] as HtmlElement;
            Assert.IsNotNull(li);
            Assert.AreEqual(1, li.Attributes.Count);
            Assert.AreEqual("checked", li.Attributes[0].Value);
        }

        [Test]
        public void Can_tidy_unquotted_attributes()
        {
            const string input = "<div style =   \"color:blue;\" align =right \r\n>The Game  of <b >Which</b > </div>";
            var output = HtmlParser.Tidy(input);
            Assert.AreEqual("<div style=\"color:blue;\" align=\"right\">The Game  of <b>Which</b></div>", output);
        }

        [Test]
        public void Parsing_removes_line_breaks_within_tags()
        {
            const string input = "<div style =   \"color:blue;\" \r\nonclick='alert(\"Hello O'Shea\")'>The \r\nGame  of <b >Which</b > </div>";
            //const string input = "<a on='a(\"H'S\")'>b</a>";
            var output = HtmlParser.Tidy(input);
            Assert.AreEqual("<div style=\"color:blue;\" onclick=\"alert(&#34;Hello O&#39; Shea&#34;)\">The \r\nGame  of <b>Which</b></div>", output);
        }

        [Test]
        public void Parsing_substitutes_attribute_single_quotes_for_double_quotes()
        {
            const string input = "<div style=\"color:blue;\" onclick='alert(\"Hello O'Shea\")'>The Game  of <b >Which</b > </div>";
            var output = HtmlParser.Parse(input).ToString();
            Assert.AreEqual("<div style=\"color:blue;\" onclick=\"alert(&#34;Hello O&#39; Shea&#34;)\">The Game  of <b>Which</b></div>", output);
        }

        [Test]
        public void Tidy_assigns_value_to_attribute_without_a_value()
        {
            const string input = "<button class='blue' disabled align='left'>The Great Gambler</button>";
            var output = HtmlParser.Tidy(input);
            Assert.AreEqual("<button class=\"blue\" disabled=\"disabled\" align=\"left\">The Great Gambler</button>", output);
        }

        [Test]
        public void Tidy_assigns_value_to_last_attribute_without_a_value()
        {
            const string input = "<input type=checkbox value=ON checked>";
            var output = HtmlParser.Tidy(input);
            Assert.AreEqual("<input type=\"checkbox\" value=\"ON\" checked=\"checked\"/>", output);
        }

        [Test]
        public void Tidy_adds_quotes_to_attribute_without_quotes()
        {
            const string input = "<p class =blue \r\nalign='left'>The Game  of <b >Which</b > </p>";
            var output = HtmlParser.Tidy(input);
            Assert.AreEqual("<p class=\"blue\" align=\"left\">The Game  of <b>Which</b></p>", output);
        }

        [Test]
        public void Tidy_adds_spaces_to_attributes_without_spaces()
        {
            const string input = "<img src=\"pic.jpg\"alt='My Pic'vspace=\"1\">";
            var output = HtmlParser.Tidy(input);
            Assert.AreEqual("<img src=\"pic.jpg\" alt=\"My Pic\" vspace=\"1\"/>", output);
        }

        [Test]
        public void Closing_tags_with_no_matching_opening_tags_are_removed()
        {
            const string input = "<p>The game of Chess <img src=\"pic.jpg\"alt='My Pic'vspace=\"1\"></ol></li> All frames</p>";
            var output = HtmlParser.Tidy(input);
            Assert.AreEqual("<p>The game of Chess <img src=\"pic.jpg\" alt=\"My Pic\" vspace=\"1\"/> All frames</p>", output);
        }

        [Test]
        public void Can_parse_html_document_with_processing_instructions()
        {

            var input = ReadFile("sample.htm");

            var doc = HtmlParser.Parse(input);

            Assert.AreEqual(2, doc.ChildNodes.Count);

            var el = doc.ChildNodes[0] as HtmlInstruction;
            Assert.IsNotNull(el);


            System.IO.File.WriteAllText(@"C:\temp\tests\sample.htm", doc.ToString());
        }
    }
}
