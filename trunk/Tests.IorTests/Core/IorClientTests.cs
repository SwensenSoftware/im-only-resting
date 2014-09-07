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
    public class IorClientTests {
        [Test]
        public void GetEncodedBytes_utf16() {
            //default big endian w/ bom
            var actual = IorClient.GetEncodedBytes("echo", "utf-16", false);
            var expected = new byte[] { 0xFE, 0xFF, 0x00, 0x65, 0x00, 0x63, 0x00, 0x68, 0x00, 0x6F }; 
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetEncodedBytes_utf16be() {
            //be no bom
            var actual = IorClient.GetEncodedBytes("echo", "utf-16be", false);
            var expected = new byte[] { 0x00, 0x65, 0x00, 0x63, 0x00, 0x68, 0x00, 0x6F };
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetEncodedBytes_utf16le() {
            //le no bom
            var actual = IorClient.GetEncodedBytes("echo", "utf-16le", false);
            var expected = new byte[] { 0x65, 0x00, 0x63, 0x00, 0x68, 0x00, 0x6F, 0x00 };
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetEncodedBytes_utf32() {
            //default big endian w/ bom
            var actual = IorClient.GetEncodedBytes("echo", "utf-32", false);
            var expected = new byte[] { 0x00, 0x00, 0xFE, 0xFF, 0x00, 0x00, 0x00, 0x65, 0x00, 0x00, 0x00, 0x63, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x00, 0x6F };
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetEncodedBytes_utf32be() {
            //be no bom
            var actual = IorClient.GetEncodedBytes("echo", "utf-32be", false);
            var expected = new byte[] { 0x00, 0x00, 0x00, 0x65, 0x00, 0x00, 0x00, 0x63, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x00, 0x6F };
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetEncodedBytes_utf32le() {
            //le no bom
            var actual = IorClient.GetEncodedBytes("echo", "utf-32le", false);
            var expected = new byte[] { 0x65, 0x00, 0x00, 0x00, 0x63, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x00, 0x6F, 0x00, 0x00, 0x00 };
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetEncodedBytes_utf8_no_bom() {
            var actual = IorClient.GetEncodedBytes("echo", "utf-8", false);
            var expected = new byte[] { 0x65, 0x63, 0x68, 0x6F };
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetEncodedBytes_utf8_bom() {
            var actual = IorClient.GetEncodedBytes("echo", "utf-8", true);
            var expected = new byte[] { 0xEF, 0xBB, 0xBF, 0x65, 0x63, 0x68, 0x6F };
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetEncodedBytes_default_ascii() {
            var actual = IorClient.GetEncodedBytes("echo", "", true);
            var expected = new byte[] { 0x65, 0x63, 0x68, 0x6F };
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void Base64EncodeUrlUserInfo_wikipedia_example_with_space() {
            var url = new Uri("http://Aladdin:open sesame@example.com");
            var actual = IorClient.Base64EncodeUrlUserInfo(url);
            var expected = "QWxhZGRpbjpvcGVuIHNlc2FtZQ==";
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
