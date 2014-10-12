using System;
using NUnit.Framework;
using Swensen.Ior.Core;
using FluentAssertions;

namespace Tests.IorTests {
    public class IorContentTypeTests {
        [Test]
        public void ctor_forgives_illegal_comma_sep_content_type() {
            var ict = new IorContentType("text/xml, application/xml");
            ict.MediaType.Should().Be("text/xml");
        }

        [Test]
        public void default_ctor() {
            var ict = new IorContentType();
            ict.MediaType.Should().Be("application/octet-stream");
        }

        [Test]
        public void ctor_forgive_illegal_content_type_fallback() {
            var ict = new IorContentType("R#O@IJ@#R");
            ict.MediaType.Should().Be("application/octet-stream");
        }

        [Test]
        public void ctor_sets_category() {
            var ict = new IorContentType("text/xml");
            ict.MediaType.Should().Be("text/xml");
            ict.MediaTypeCategory.Should().Be(IorMediaTypeCategory.Xml);
        }

        [Test]
        public void parse_media_type_category() {
            IorContentType.GetMediaTypeCategory("text/xml").Should().Be(IorMediaTypeCategory.Xml);
            IorContentType.GetMediaTypeCategory("text/html").Should().Be(IorMediaTypeCategory.Html);
            IorContentType.GetMediaTypeCategory("text/json").Should().Be(IorMediaTypeCategory.Json);
            IorContentType.GetMediaTypeCategory("text/plain").Should().Be(IorMediaTypeCategory.Text);
            IorContentType.GetMediaTypeCategory("image/jpg").Should().Be(IorMediaTypeCategory.Application);
            IorContentType.GetMediaTypeCategory("application/octet-stream").Should().Be(IorMediaTypeCategory.Application);
        }

        [Test]
        public void get_file_ext() {
            var localhost = new Uri("http://localhost");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Xml, "text/xml", localhost).Should().Be("xml");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Html, "text/html", localhost).Should().Be("html");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Json, "text/json", localhost).Should().Be("json");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Text, "text/plain", localhost).Should().Be("txt");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Application, "image/jpg", localhost).Should().Be("jpg");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Other, "text/csv", localhost).Should().Be("csv");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Other, "text/css", localhost).Should().Be("css");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Other, "application/javascript", localhost).Should().Be("js");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Other, "application/python", localhost).Should().Be("");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Application, "application/zip", localhost).Should().Be("zip");

            var imgUri = new Uri("http://localhost/test.img");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Application, "application/octet-stream", imgUri).Should().Be("img");
        }

        [Test]
        public void pretty_print_xml_no_doctype() {
            var input = "<root><hello>world</hello></root>";
            var output = IorContentType.GetPrettyPrintedContent(IorMediaTypeCategory.Xml, input);
            output.Should().Be("<root>\r\n  <hello>world</hello>\r\n</root>");
        }

        [Test]
        public void pretty_print_xml_with_doctype() {
            var input = "<?xml version=\"1.0\"?><root><hello>world</hello></root>";
            var output = IorContentType.GetPrettyPrintedContent(IorMediaTypeCategory.Xml, input);
            output.Should().Be("<?xml version=\"1.0\"?>\r\n<root>\r\n  <hello>world</hello>\r\n</root>");
        }

        [Test]
        public void pretty_print_xml_malformed_forgives() {
            var input = "<root><hello>world</h></root>";
            var output = IorContentType.GetPrettyPrintedContent(IorMediaTypeCategory.Xml, input);
            output.Should().Be(input);
        }

        [Test]
        public void pretty_print_json() {
            var input = "{ x: 23 }";
            var output = IorContentType.GetPrettyPrintedContent(IorMediaTypeCategory.Json, input);
            output.Should().Be("{\r\n  \"x\": 23\r\n}");
        }

        [Test]
        public void pretty_print_json_malformed_forgives() {
            var input = "{ x: 23 + 3 }";
            var output = IorContentType.GetPrettyPrintedContent(IorMediaTypeCategory.Json, input);
            output.Should().Be(input);
        }

        [Test]
        public void pretty_print_html() {
            var input = "<html><body>hello world</body></html>";
            var output = IorContentType.GetPrettyPrintedContent(IorMediaTypeCategory.Html, input);
            output.Should().Be("<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 3.2//EN\">\r\n<html>\r\n  <head>\r\n    <title>\r\n    </title>\r\n  </head>\r\n  <body>\r\n    hello world\r\n  </body>\r\n</html>\r\n");
        }

        [Test]
        public void pretty_print_other() {
            var input = "hello world";
            var output = IorContentType.GetPrettyPrintedContent(IorMediaTypeCategory.Text, input);
            output.Should().Be(input);
        }

    }
}
