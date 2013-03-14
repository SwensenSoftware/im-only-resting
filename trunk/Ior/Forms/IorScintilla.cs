using System.Windows.Forms;

namespace Swensen.Ior.Forms {
    /// <summary>
    /// An StandardScintilla controll initialized with IOR defaults.
    /// </summary>
    public class IorScintilla : StandardScintilla {
        public IorScintilla() : base() {
            this.NativeInterface.SetUseTabs(false); //use spaces for tabs
            this.NativeInterface.SetTabWidth(2);

            this.FindReplace.Window.FormBorderStyle = FormBorderStyle.FixedDialog; //so we can actually see the text title

            this.ShowLineNumbers();

            this.ConfigurationManager.Language = "js"; //not a bad default language
            this.ConfigurationManager.Configure();
        }
    }
}