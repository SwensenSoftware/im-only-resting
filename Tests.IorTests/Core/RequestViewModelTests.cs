using System.Net.Http;
using NUnit.Framework;
using Swensen.Ior.Core;
using Swensen.Utils;
using FluentAssertions;

namespace Tests.IorTests {
    public class RequestViewModelTests {
        [Test]
        public void serializeAndDeserialize() {
            var rvm = new RequestViewModel {
                Body = "body",
                Headers = new[] { "h1", "h2" },
                Method = HttpMethod.Post.Method,
                Url = "url"
            };

            var filepath = FileUtils.CreateTempFilePath("xml");
            rvm.Save(filepath);

            var rvm_deserialized = RequestViewModel.Load(filepath);

            rvm.ShouldBeEquivalentTo(rvm_deserialized);
        }
    }
}
