namespace PropertyGridEx
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class UIFilenameEditor : System.Drawing.Design.UITypeEditor
	{				
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null&& context.Instance != null)
			{
				if (! context.PropertyDescriptor.IsReadOnly)
				{
					return UITypeEditorEditStyle.Modal;
				}
			}
			return UITypeEditorEditStyle.None;
		}
		
		[RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if (context == null || provider == null || context.Instance == null)
			{
				return base.EditValue(provider, value);
			}
			
			FileDialog fileDlg;
			if (context.PropertyDescriptor.Attributes[typeof(SaveFileAttribute)] == null)
			{
				fileDlg = new OpenFileDialog();
			}
			else
			{
				fileDlg = new SaveFileDialog();
			}
			fileDlg.Title = "Select " + context.PropertyDescriptor.DisplayName;
			fileDlg.FileName =  (string) value;
			
			FileDialogFilterAttribute filterAtt =  (FileDialogFilterAttribute) context.PropertyDescriptor.Attributes[typeof(FileDialogFilterAttribute)];
			if (filterAtt != null)
			{
				fileDlg.Filter = filterAtt.Filter;
			}
			if (fileDlg.ShowDialog() == DialogResult.OK)
			{
				value = fileDlg.FileName;
			}
			fileDlg.Dispose();
			return value;
		}
		
		[AttributeUsage(AttributeTargets.Property)]
        public class FileDialogFilterAttribute : Attribute
		{			
			private string _filter;
			
			public string Filter
			{
				get
				{
					return this._filter;
				}
			}
			
			public FileDialogFilterAttribute(string filter)
			{
				this._filter = filter;
			}
		}
		
		[AttributeUsage(AttributeTargets.Property)]
        public class SaveFileAttribute : Attribute
		{
			
		}
		
		public enum FileDialogType
		{
			LoadFileDialog,
			SaveFileDialog
		}
	}
}
