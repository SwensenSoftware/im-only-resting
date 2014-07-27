using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NLog;
using Swensen.Ior.Core;

namespace Swensen.Ior.Forms
{
    public partial class LogViewer : Form
    {
        private List<LogLineModel> logLines = new List<LogLineModel>();
        private string logFilePath = String.Format("logs{0}log.txt", Path.DirectorySeparatorChar);

        public LogViewer()
        {
            InitializeComponent();
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            readLogLines();
            writeLogLines();
        }

        private void readLogLines() {
            try { 
                //can't use File.ReadAllText since file is locked by NLog
                using (var fileStream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var textReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    while (!textReader.EndOfStream) {
                        var content = textReader.ReadLine();
                        logLines.Add(new LogLineModel(content + Environment.NewLine));
                    }
                }
            }
            catch {
                MessageBox.Show(String.Format("Could not load {0}, the file may not exist", logFilePath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void writeLogLines() {
            txtLogViewer.SuspendReadonly(() => {
                foreach(var ll in logLines) {
                    txtLogViewer.InsertText(0, ll.Text);
                }
            });
        }
    }
}
