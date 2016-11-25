namespace WhichMan.Utilities.HtmlParsing.Tests
{
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class HtmlElementTests : BaseTest
    {
        private const string _html = @"<h1>Table of Contents</h1>
            < !--This is a comment-->
            <ol style='color:blue;' align='left'>
                <li>The Great Gambler of <b>WhichMan</b>, INC. </li>
                <li checked >The Return of the Jedi</li>
                <li>Masters of the Universe</li>
            </ol>";

        [Test]
        public void Can_find_node()
        {
            var doc = HtmlParser.Parse(_html);
            var el = doc.FindDescendants("li").First();
            var node = el.FindNode("b");
            Assert.IsNotNull(node);
            Assert.AreEqual("WhichMan", node.ChildNodes[0].ToString());
        }

        [Test]
        public void Can_find_all_nodes()
        {
            var doc = HtmlParser.Parse(_html);
            var el = doc.FindDescendants("ol").First();
            var nodes = el.FindNodes("li").ToList();
            Assert.IsNotNull(nodes);
            Assert.AreEqual(3, nodes.Count);
        }

        [Test]
        public void Can_find_ancestor()
        {
            var doc = HtmlParser.Parse(_html);
            var node = doc.FindDescendants("b").First().FindAncestor("li");
            Assert.IsNotNull(node);
            Assert.AreEqual("<li>The Great Gambler of <b>WhichMan</b>, INC. </li>", node.ToString());
        }

        [Test]
        public void Can_get_attribute_value()
        {
            var doc = HtmlParser.Parse(ReadFile("test.html"));
            var node = doc.FindDescendants("h2").First();
            Assert.IsNotNull(node);
            Assert.AreEqual("center", node.GetAttributeValue("align"));
        }

        [Test]
        public void Can_get_style_value()
        {
            var doc = HtmlParser.Parse(ReadFile("test.html"));
            var node = doc.FindDescendants("h2").First();
            Assert.IsNotNull(node);
            Assert.AreEqual("1px solid steelblue", node.GetStyleValue("border-bottom"));
        }

        [Test]
        public void Can_find_descendants()
        {
            var doc = HtmlParser.Parse(ReadFile("test.html"));
            var nodes = doc.FindDescendants("meta").ToList();
            Assert.AreEqual(4, nodes.Count);

            const string html = @"
<ol>
    <li>
        Item 1
        <ol>
            <li>Item 1.1</li>
            <li>Item 1.2</li>
            <li>Item 1.3</li>
        </ol>
    </li>
    <li>Item 2</li>
    <li>Item 3</li>
</ol>";
            doc = HtmlParser.Parse(html);
            nodes = doc.FindDescendants("ol").ToList();
            Assert.AreEqual(2, nodes.Count);
        }

        [Test]
        public void Can_find_styles()
        {
            var doc = HtmlParser.Parse(ReadFile("test.html"));
            var nodes = doc.FindDescendants("style").ToList();
            Assert.AreEqual(2, nodes.Count);

            var type = nodes[0].GetType();
            var style = nodes[0];
        }
    }
}