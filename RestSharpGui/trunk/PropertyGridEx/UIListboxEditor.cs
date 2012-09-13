namespace PropertyGridEx
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
	
	public class UIListboxEditor : UITypeEditor
	{				
		private bool bIsDropDownResizable = false;
		private ListBox oList = new ListBox();
		private object oSelectedValue = null;
		private IWindowsFormsEditorService oEditorService;
		
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null&& context.Instance != null)
			{
				UIListboxIsDropDownResizable attribute =  (UIListboxIsDropDownResizable) context.PropertyDescriptor.Attributes[typeof(UIListboxIsDropDownResizable)];
				if (attribute != null)
				{
					bIsDropDownResizable = true;
				}
				return UITypeEditorEditStyle.DropDown;
			}
			return UITypeEditorEditStyle.None;
		}
		
		public override bool IsDropDownResizable
		{
			get
			{
				return bIsDropDownResizable;
			}
		}
		
		[RefreshProperties(RefreshProperties.All)]public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if (context == null || provider == null || context.Instance == null)
			{
				return base.EditValue(provider, value);
			}
			
			oEditorService =  (System.Windows.Forms.Design.IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
			if (oEditorService != null)
			{
				
				// Get the Back reference to the Custom Property
				CustomProperty.CustomPropertyDescriptor oDescriptor =  (CustomProperty.CustomPropertyDescriptor) context.PropertyDescriptor;
				CustomProperty cp =  (CustomProperty) oDescriptor.CustomProperty;
				
				// Declare attributes
				UIListboxDatasource datasource;
				UIListboxValueMember valuemember;
				UIListboxDisplayMember displaymember;
				
				// Get attributes
				datasource =  (UIListboxDatasource) context.PropertyDescriptor.Attributes[typeof(UIListboxDatasource)];
				valuemember =  (UIListboxValueMember) context.PropertyDescriptor.Attributes[typeof(UIListboxValueMember)];
				displaymember =  (UIListboxDisplayMember) context.PropertyDescriptor.Attributes[typeof(UIListboxDisplayMember)];
				
				oList.BorderStyle = BorderStyle.None;
				oList.IntegralHeight = true;
				
				if (datasource != null)
				{
					oList.DataSource = datasource.Value;
				}
				
				if (displaymember != null)
				{
					oList.DisplayMember = displaymember.Value;
				}
				
				if (valuemember != null)
				{
					oList.ValueMember = valuemember.Value;
				}
				
				if (value != null)
				{
					if (value.GetType().Name == "String")
					{
						oList.Text =  (string) value;
					}
					else
					{
						oList.SelectedItem = value;
					}
				}
				
				
				oList.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
				
				oEditorService.DropDownControl(oList);
				if (oList.SelectedIndices.Count == 1)
				{
					cp.SelectedItem = oList.SelectedItem;
					cp.SelectedValue = oSelectedValue;
					value = oList.Text;
				}
				oEditorService.CloseDropDown();
			}
			else
			{
				return base.EditValue(provider, value);
			}
			
			return value;
			
		}
		
		private void SelectedItem(object sender, EventArgs e)
		{
			if (oEditorService != null)
			{
				if (oList.SelectedValue != null)
				{
					oSelectedValue = oList.SelectedValue;
				}
				oEditorService.CloseDropDown();
			}
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UIListboxDatasource : Attribute
		{
			
			private object oDataSource;
			public UIListboxDatasource(ref object Datasource)
			{
				oDataSource = Datasource;
			}
			public object Value
			{
				get
				{
					return oDataSource;
				}
			}
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UIListboxValueMember : Attribute
		{
			
			private string sValueMember;
			public UIListboxValueMember(string ValueMember)
			{
				sValueMember = ValueMember;
			}
			public string Value
			{
				get
				{
					return sValueMember;
				}
				set
				{
					sValueMember = value;
				}
			}
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UIListboxDisplayMember : Attribute
		{
			
			private string sDisplayMember;
			public UIListboxDisplayMember(string DisplayMember)
			{
				sDisplayMember = DisplayMember;
			}
			public string Value
			{
				get
				{
					return sDisplayMember;
				}
				set
				{
					sDisplayMember = value;
				}
			}
			
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UIListboxIsDropDownResizable : Attribute
		{
			
		}	
	}	
}
