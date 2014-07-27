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
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private List<LogMessageModel> logMessages = new List<LogMessageModel>();
        private string logFilePath = String.Format("logs{0}log.txt", Path.DirectorySeparatorChar);

        public LogViewer()
        {
            InitializeComponent();
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            txtLogViewer.DisableReplace();
            readLogLines();
            renderLogMessages();
            //txtLogViewer.Scrolling.ScrollToLine(txtLogViewer.Lines.Count-1);
        }

        private void readLogLines() {
            try { 
                logMessages = LogMessageModel.ParseAllMessages(logFilePath);
            }
            catch (Exception ex) {
                MessageBox.Show(String.Format("Could not load {0}, the file may not exist", logFilePath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
                this.Close();
            }
        }

        /// <summary>
        /// Render all log messages from newest to oldest.
        /// </summary>
        private void renderLogMessages() {
            txtLogViewer.SuspendReadonly(() => {
                foreach(var ll in logMessages) {
                    txtLogViewer.InsertText(0, ll.Message);
                    var marker = txtLogViewer.Lines[0].AddMarker(ll.Level.Ordinal);
                    marker.Marker.BackColor = getMarkerBackColor(ll.Level);
                }
            });
        }

        private static Color getMarkerBackColor(LogLevel ll) {
            if(ll == LogLevel.Fatal)
                return Color.Red;
            else if(ll == LogLevel.Error)
                return Color.Orange;
            else if(ll == LogLevel.Warn)
                return Color.Yellow;
            else if(ll == LogLevel.Info)
                return Color.Blue;
            else if (ll == LogLevel.Debug)
                return Color.Gray;
            else if (ll == LogLevel.Trace)
                return Color.Black;
            else
                return Color.White;
        }
    }
}
