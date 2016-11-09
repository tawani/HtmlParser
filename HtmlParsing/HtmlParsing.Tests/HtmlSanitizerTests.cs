namespace WhichMan.Utilities.HtmlParsing.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class HtmlSanitizerTests : BaseTest
    {
        [Test]
        public void Sanitize_removes_script_tags()
        {
            const string input = "<scriPt>alert(0)</Script>This is the game <SCRIPT>";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game ", output);
        }
        [Test]
        public void Sanitize_does_not_remove_form_tags()
        {
            const string input = "<form>Berije</form>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("<form>Berije</form>This is the game", output);
        }
        [Test]
        public void Sanitize_removes_applet_tags()
        {
            const string input = "<applet>alert(0)</applet>This is the game <APPLET>My APPlet</aPplet>";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game ", output);
        }
        [Test]
        public void Sanitize_removes_embed_tags()
        {
            const string input = "<emBed>alert(0)</Embed>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game", output);
        }
        [Test]
        public void Sanitize_removes_frame_tags()
        {
            const string input = "<fRame>alert(0)</framE>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game", output);
        }
        [Test]
        public void Sanitize_removes_frameset_tags()
        {
            const string input = "<fRameSet>alert(0)</framEset>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game", output);
        }
        [Test]
        public void Sanitize_removes_iframe_tags()
        {
            const string input = "<ifRame>alert(0)</IframE>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game", output);
        }
        [Test]
        public void Sanitize_removes_object_tags()
        {
            const string input = "<object>alert(0)</oBject>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game", output);
        }
        [Test]
        public void Sanitize_removes_layer_tags()
        {
            const string input = "<laYer>alert(0)</layeR>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game", output);
        }
        [Test]
        public void Sanitize_removes_ilayer_tags()
        {
            const string input = "<ilayer>alert(0)</ilayeR>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("This is the game", output);
        }
        [Test]
        public void Sanitize_removes_changes_submit_input_to_button()
        {
            const string input = "<form><input type=submit value=Submit>alert(0)</form>This is the game";
            var output = HtmlSanitizer.Sanitize(input);
            Assert.AreEqual("<form><input type=\"button\" value=\"Submit\"/>alert(0)</form>This is the game", output);
        }

        [Test]
        public void Sanitize_removes_processing_comments()
        {
            var input = ReadFile("sample.htm");
            var output = HtmlSanitizer.Sanitize(input);
            Assert.IsTrue(output.Contains("<html>"));
            Assert.IsFalse(output.Contains("<script"));
            Assert.IsFalse(output.Contains("<!--"));
        }

        [Test]
        public void Sanitize_removes_processing_instructions()
        {
            var input = ReadFile("sample.htm");
            var output = HtmlSanitizer.Sanitize(input);
            Assert.IsTrue(output.Contains("<html>"));
            Assert.IsFalse(output.Contains("<script"));
            Assert.IsFalse(output.Contains("<!DOCTYPE"));
        }
    }
}