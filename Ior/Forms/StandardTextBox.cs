using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Swensen.Ior.Forms {
    public class StandardTextBox : TextBox {
        MenuItem miCut;
        MenuItem miCopy;
        MenuItem miDelete;
        MenuItem miSelectAll;

        public StandardTextBox() : base() {
            initContextMenu();    
        }

        private void initContextMenu() {
            var cm = this.ContextMenu = new ContextMenu();
            cm.MenuItems.Add(new MenuItem("Undo", (s, ea) => this.Undo()));
            cm.MenuItems.Add(new MenuItem("-"));
            {
                this.miCut = new MenuItem("Cut", (s, ea) => this.Cut());
                cm.MenuItems.Add(miCut);
            }
            {
                this.miCopy = new MenuItem("Copy", (s, ea) => this.Copy());
                cm.MenuItems.Add(miCopy);
            }
            cm.MenuItems.Add(new MenuItem("Paste", (s, ea) => this.Paste()));
            {
                this.miDelete = new MenuItem("Delete", (s, ea) => this.SelectedText = "");
                cm.MenuItems.Add(miDelete);
            }
            cm.MenuItems.Add(new MenuItem("-"));
            {
                this.miSelectAll = new MenuItem("Select All", (s, ea) => this.SelectAll());
                cm.MenuItems.Add(miSelectAll);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                this.Focus();
                var charIndex = this.GetCharIndexFromPosition(e.Location);
                if (this.SelectionLength == 0) { //no selected text
                    this.SelectionStart = charIndex;
                } else if(charIndex < this.SelectionStart || charIndex > (this.SelectionStart + this.SelectionLength)) { // user clicks outside of selected text
                    this.Select(charIndex, 0);
                }
                miCut.Enabled = this.SelectionLength > 0;
                miCopy.Enabled = this.SelectionLength > 0;
                miDelete.Enabled = this.SelectionLength > 0;
                miSelectAll.Enabled = this.TextLength > 0 && this.TextLength != this.SelectionLength;
            } else
                base.OnMouseDown(e);
        }

        //credit to Schotime, see http://schotime.net/blog/index.php/2008/03/12/select-all-ctrla-for-textbox/
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A)) {
                this.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
