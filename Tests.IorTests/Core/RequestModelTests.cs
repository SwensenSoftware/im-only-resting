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
    public class RequestModelTests {
        [Test]
        public void TryCreate_url_required() {
            var rvm = new RequestViewModel {
                Body = "",
                Headers = new string[] { },
                Method = HttpMethod.Get.Method,
                Url = "   "
            };

            RequestModel rm = null;
            var errors = RequestModel.TryCreate(rvm, out rm);
            
            errors.Should().HaveCount(1);
            errors[0].Should().Be("Request URL may not be empty");
        }

        [Test]
        public void TryCreate_url_must_be_valid() {
            var rvm = new RequestViewModel {
                Body = "",
                Headers = new string[] { },
                Method = HttpMethod.Get.Method,
                Url = "invali$@#%$@#%%d"
            };

            RequestModel rm = null;
            var errors = RequestModel.TryCreate(rvm, out rm);

            errors.Should().HaveCount(1);
            errors[0].Should().Be("Request URL is invalid");
        }

        [Test]
        public void TryCreate_url_valid() {
            var rvm = new RequestViewModel {
                Body = "",
                Headers = new string[] { },
                Method = HttpMethod.Get.Method,
                Url = "http://www.google.com/"
            };

            RequestModel rm = null;
            var errors = RequestModel.TryCreate(rvm, out rm);

            errors.Should().HaveCount(0);
            rm.Should().NotBeNull();
            rm.Url.ToString().Should().Be("http://www.google.com/");
        }

        [Test]
        public void TryCreate_url_forgiving() {
            var rvm = new RequestViewModel {
                Body = "",
                Headers = new string[] { },
                Method = HttpMethod.Get.Method,
                Url = "www.google.com"
            };

            RequestModel rm = null;
            var errors = RequestModel.TryCreate(rvm, out rm);

            errors.Should().HaveCount(0);
            rm.Should().NotBeNull();
            rm.Url.ToString().Should().Be("http://www.google.com/");
        }
    }
}
