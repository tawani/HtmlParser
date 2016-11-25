# HtmlParser
HTML Parsing and Sanitizing utility. Convert HTML to XHTML

e.g. 
<pre>
const string input = "&lt;div style=\"color:blue;\" align =right>The Game  of &lt;b >Which&lt;/b > &lt;/div>";
var output = HtmlParser.Tidy(input);
Assert.AreEqual("&lt;div style=\"color:blue;\" align=\"right\">The Game  of &lt;b>Which&lt;/b>&lt;/div>", output);
</pre>
or this
<pre>
var doc = HtmlParser.Parse(_html);
var el = doc.FindDescendants("ol").First();
var nodes = el.FindNodes("li").ToList();
Assert.AreEqual(3, nodes.Count);
</pre>
