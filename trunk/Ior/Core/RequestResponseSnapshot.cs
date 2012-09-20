using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swensen.Ior.Core {
    public class RequestResponseSnapshot {
        public RequestViewModel request { get; set; }
        public ResponseModel response { get; set; }

        public override string ToString() {
            return string.Format("[{0}] {1}", response.Start, request.Url);
        }
    }
}
