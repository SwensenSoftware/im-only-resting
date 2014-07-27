using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Swensen.Ior.Forms
{
    public partial class LogViewer : Form
    {
        public LogViewer()
        {
            InitializeComponent();
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            try { 
                //can't use File.ReadAllText since file is locked by NLog
                using (var fileStream = new FileStream(@"logs/log.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var textReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var content = textReader.ReadToEnd();
                    txtLogViewer.Text = content;
                }
            }
            catch {
                MessageBox.Show(@"Could not load logs\log.txt, the file may not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
