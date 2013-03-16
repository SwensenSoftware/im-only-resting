using System;
using System.Windows.Forms;

namespace Swensen.Ior.Forms {
    public class StandardScintilla : ScintillaNET.Scintilla {
        MenuItem miUndo;
        MenuItem miRedo;
        MenuItem miCut;
        MenuItem miCopy;
        MenuItem miDelete;
        MenuItem miSelectAll;

        public StandardScintilla() : base() {
            initContextMenu();

            this.NativeInterface.SetUseTabs(false); //use spaces for tabs
            this.NativeInterface.SetTabWidth(2);

            this.FindReplace.Window.FormBorderStyle = FormBorderStyle.FixedDialog; //so we can actually see the text title

            this.ConfigurationManager.Language = "js"; //not a bad default language
            this.ConfigurationManager.Configure();
        }

        private void initContextMenu() {
            var cm = this.ContextMenu = new ContextMenu();
            {
                this.miUndo = new MenuItem("Undo", (s, ea) => this.UndoRedo.Undo());
                cm.MenuItems.Add(this.miUndo);
            }
            {
                this.miRedo = new MenuItem("Redo", (s, ea) => this.UndoRedo.Redo());
                cm.MenuItems.Add(this.miRedo);
            }
            cm.MenuItems.Add(new MenuItem("-"));
            {
                this.miCut = new MenuItem("Cut", (s, ea) => this.Clipboard.Cut());
                cm.MenuItems.Add(miCut);
            }
            {
                this.miCopy = new MenuItem("Copy", (s, ea) => this.Clipboard.Copy());
                cm.MenuItems.Add(miCopy);
            }
            cm.MenuItems.Add(new MenuItem("Paste", (s, ea) => this.Clipboard.Paste()));
            {
                this.miDelete = new MenuItem("Delete", (s, ea) => this.NativeInterface.ReplaceSel(""));
                cm.MenuItems.Add(miDelete);
            }
            cm.MenuItems.Add(new MenuItem("-"));
            {
                this.miSelectAll = new MenuItem("Select All", (s, ea) => this.Selection.SelectAll());
                cm.MenuItems.Add(miSelectAll);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                miUndo.Enabled = this.UndoRedo.CanUndo;
                miRedo.Enabled = this.UndoRedo.CanRedo;
                miCut.Enabled = this.Clipboard.CanCut;
                miCopy.Enabled = this.Clipboard.CanCopy;
                miDelete.Enabled = this.Selection.Length > 0;
                miSelectAll.Enabled = this.TextLength > 0 && this.TextLength != this.Selection.Length;
            }
            else
                base.OnMouseDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            var form = this.FindForm();
            if (keyData == (Keys.Control | Keys.Tab)) {
                form.SelectNextControl(this, true, true, true, true);
                return true;
            } else if (keyData == (Keys.Control | Keys.Shift | Keys.Tab)) {
                form.SelectNextControl(this, false, true, true, true);
                return true;
            } else if (keyData == (Keys.Control | Keys.Enter)) {
                form.AcceptButton.PerformClick();
                return true;
            } else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        public void DisableReplace() {
            this.Commands.RemoveBinding(Keys.H, Keys.Control);
            ((TabControl)this.FindReplace.Window.Controls.Find("tabAll", true)[0]).TabPages.RemoveAt(1);
        }

        //scintilla does not allow programmatic write when set to readonly.
        public void SuspendReadonly(Action act) {
            if (this.IsReadOnly) {
                try {
                    this.IsReadOnly = false;
                    act();
                } finally {
                    this.IsReadOnly = true;
                }
            } else
                act();
        }

        public override string Text {
            get {
                return base.Text;
            }
            set {
                SuspendReadonly(() => base.Text = value);
            }
        }
    }
}
