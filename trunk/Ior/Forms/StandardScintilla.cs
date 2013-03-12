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
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                //this.Focus();
                /*var charIndex = this.GetCharIndexFromPosition(e.Location);
                if (this.SelectionLength == 0) { //no selected text
                    this.SelectionStart = charIndex;
                } else if(charIndex < this.Selection.Start || charIndex > (this.Selection.Start + this.Selection.Length)) { // user clicks outside of selected text
                    this.NativeInterface.GotoLine(1);
                    //this.Select(charIndex, 0);
                }*/
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

        /*//credit to Schotime, see http://schotime.net/blog/index.php/2008/03/12/select-all-ctrla-for-textbox/
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A)) {
                this.NativeInterface.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }*/
    }
}
