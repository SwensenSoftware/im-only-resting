using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Swensen.Ior.Core
{
    public class LogLineModel {
        public LogLineModel(string text) {
            this.Text = text;
            this.Level = LogLevel.Off;
        }

        public string Text {get; private set;}
        public LogLevel Level {get; private set;}
    }

}
