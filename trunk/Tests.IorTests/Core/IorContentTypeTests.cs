using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using Swensen.Ior.Core;
using Swensen.Utils;
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
            IorContentType.GetMediaTypeCategory("application/octet-stream").Should().Be(IorMediaTypeCategory.Other);
        }

        [Test]
        public void get_file_ext() {
            IorContentType.GetFileExtension(IorMediaTypeCategory.Xml, "text/xml").Should().Be("xml");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Html, "text/html").Should().Be("html");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Json, "text/json").Should().Be("json");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Text, "text/plain").Should().Be("txt");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Application, "image/jpg").Should().Be("jpg");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Other, "text/csv").Should().Be("csv");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Other, "text/css").Should().Be("css");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Other, "application/javascript").Should().Be("js");
            IorContentType.GetFileExtension(IorMediaTypeCategory.Other, "application/python").Should().Be("");
        }

    }
}
