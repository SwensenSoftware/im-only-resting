using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Swensen.Ior.Forms {

    //credit to Adam Robinson (http://stackoverflow.com/users/82187/adam-robinson) for the SendMessage, ResumeDrawing, and SuspendDrawing (non-lambda) methods: http://stackoverflow.com/a/778133/236255
    public static class ControlHelpers {
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void SuspendDrawing(this Control target) {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }
                                                                  
        public static void ResumeDrawing(this Control target) { ResumeDrawing(target, true); }
        public static void ResumeDrawing(this Control target, bool redraw) {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw) {
                target.Refresh();
            }
        }

        /// <summary>
        /// Suspend drawing while executing the action, resuming when done.
        /// </summary>
        public static void SuspendDrawing(this Control target, Action action) {
            target.SuspendDrawing();
            action();
            target.ResumeDrawing();
        }
    }
}
