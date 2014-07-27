using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.IO;
using System.Text.RegularExpressions;

namespace Swensen.Ior.Core
{
    public class LogMessageModel {
        public string Message {get; private set;}
        public LogLevel Level {get; private set;}

        public static List<LogMessageModel> ParseAllMessages(string logFilePath) {
            //can't use File.ReadAllText since file is locked by NLog
            using (var fileStream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                var logMessages = new List<LogMessageModel>();
                StringBuilder curBuilder = null;
                LogMessageModel curMessage = null;
                //commit current message, if any
                Action commitMessage = () => {
                    if(curBuilder != null && curMessage != null)
                        curMessage.Message = curBuilder.ToString();
                };
                while (!textReader.EndOfStream) {
                    var logLine = textReader.ReadLine();
                    var logLevel = parseLogLevel(logLine);
                    if (logLevel != null) { 
                        commitMessage();

                        //start new message
                        curMessage = new LogMessageModel { Level = logLevel };
                        curBuilder = new StringBuilder().AppendLine(logLine);
                        logMessages.Add(curMessage);
                    }
                    else if (curBuilder != null) { //curBuilder may be null if we have an empty file with only a newline char
                        curBuilder.AppendLine(logLine);
                    }
                }
                commitMessage();

                return logMessages;
            }
        }

        private static LogLevel parseLogLevel(string logLine) {
            var logLevelNames = new List<LogLevel> { 
                LogLevel.Debug,
                LogLevel.Error,
                LogLevel.Fatal,
                LogLevel.Info,
                LogLevel.Trace,
                LogLevel.Warn
            };

            var logLevelPattern = String.Join("|", logLevelNames);
            var match = Regex.Match(
                logLine, 
                @"\] \[(" + logLevelPattern + @")\] \{", 
                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant
            );

            if(match.Success)
                return LogLevel.FromString(match.Groups[1].Value);
            else
                return null;
        }
    }
}
