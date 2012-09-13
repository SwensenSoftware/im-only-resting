using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace PropertyGridEx
{
	public class UICustomEventEditor : System.Drawing.Design.UITypeEditor
	{				
		public delegate object OnClick(object sender, EventArgs e);
		protected UICustomEventEditor.OnClick m_MethodDelegate;
		protected CustomProperty.CustomPropertyDescriptor m_sender;
		
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
		
		[RefreshProperties(RefreshProperties.All)]public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if (context == null || provider == null || context.Instance == null)
			{
				return base.EditValue(provider, value);
			}
			if (m_MethodDelegate == null)
			{
				DelegateAttribute attr =  (DelegateAttribute) context.PropertyDescriptor.Attributes[typeof(DelegateAttribute)];
				m_MethodDelegate = attr.GetMethod;
			}
			if (m_sender == null)
			{
				m_sender = context.PropertyDescriptor as CustomProperty.CustomPropertyDescriptor;
			}
			return m_MethodDelegate.Invoke(m_sender, null);
		}
		
		[AttributeUsage(AttributeTargets.Property)]public class DelegateAttribute : Attribute
		{
			
			protected UICustomEventEditor.OnClick m_MethodDelegate;
			
			public UICustomEventEditor.OnClick GetMethod
			{
				get
				{
					return this.m_MethodDelegate;
				}
			}
			
			public DelegateAttribute(UICustomEventEditor.OnClick MethodDelegate)
			{
				this.m_MethodDelegate = MethodDelegate;
			}
		}		
	}
}

