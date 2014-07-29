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
using Swensen.Ior.Properties;
using Swensen.Utils;

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
            bindCbGroupLogLevels();
            bindSettings();
            txtLogViewer.DisableReplace();
            readLogLines();
            renderLogMessages();
            //txtLogViewer.Scrolling.ScrollToLine(txtLogViewer.Lines.Count-1);
        }

        private IEnumerable<CheckBox> cbGrpLogLevels { get { return filterPanel.Controls.OfType<CheckBox>(); } }

        private void bindCbGroupLogLevels() {
            cbFatal.Tag = LogLevel.Fatal;
            cbError.Tag = LogLevel.Error;
            cbWarn.Tag = LogLevel.Warn;
            cbInfo.Tag = LogLevel.Info;
            cbDebug.Tag = LogLevel.Debug;
            cbTrace.Tag = LogLevel.Trace;
        }

        private void bindSettings() {
            var settings = Settings.Default;
            cbGrpLogLevels.Each(cb => {
                if(settings.LogViewerLevelFilter.Contains(cb.Tag.ToString()))
                    cb.Checked = true;
                else
                    cb.Checked = false;

            });
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
                txtLogViewer.Text = "";
                foreach(var lm in logMessages) {
                    if(!isCheckedLogLevel(lm.Level))
                        continue;

                    txtLogViewer.InsertText(0, lm.Message);
                    var marker = txtLogViewer.Lines[0].AddMarker(lm.Level.Ordinal);
                    marker.Marker.BackColor = getMarkerBackColor(lm.Level);
                }
            });
        }

        private bool isCheckedLogLevel(LogLevel ll) {
            if(ll == LogLevel.Fatal && cbFatal.Checked)
                return true;
            if(ll == LogLevel.Error && cbError.Checked)
                return true;
            if(ll == LogLevel.Warn && cbWarn.Checked)
                return true;
            if(ll == LogLevel.Info && cbInfo.Checked)
                return true;
            if(ll == LogLevel.Debug && cbDebug.Checked)
                return true;
            if(ll == LogLevel.Trace && cbTrace.Checked)
                return true;
            else
                return false;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            renderLogMessages();
            var checkedLogLevels = cbGrpLogLevels.Where(cb => cb.Checked).Select(cb => cb.Tag.ToString()); 
            Settings.Default.LogViewerLevelFilter = String.Join("|", checkedLogLevels);
            Settings.Default.Save();
        }
    }
}
