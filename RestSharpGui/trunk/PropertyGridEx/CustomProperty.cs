namespace PropertyGridEx
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Data;
    using System.Xml.Serialization;

    [Serializable(), XmlRootAttribute("CustomProperty")]
    public class CustomProperty
    {				
		#region "Private variables"
		
		// Common properties
		protected string sName = "";
		protected object oValue = null;
		protected bool bIsReadOnly = false;
		protected bool bVisible = true;
		protected string sDescription = "";
		protected string sCategory = "";
		protected bool bIsPassword = false;
		protected bool bIsPercentage = false;
		protected bool bParenthesize = false;
		
		// Filename editor properties
		protected string sFilter = null;
		protected UIFilenameEditor.FileDialogType eDialogType = UIFilenameEditor.FileDialogType.LoadFileDialog;
		protected bool bUseFileNameEditor = false;
		
		// Custom choices properties
		protected CustomChoices oChoices = null;
		
		// Browsable properties
		protected bool bIsBrowsable = false;
		protected BrowsableTypeConverter.LabelStyle eBrowsablePropertyLabel = BrowsableTypeConverter.LabelStyle.lsEllipsis;
		
		// Dynamic properties
		protected bool bRef = false;
		protected object oRef = null;
		protected string sProp = "";
		
		// Databinding properties
		protected object oDatasource = null;
		protected string sDisplayMember = null;
		protected string sValueMember = null;
		protected object oSelectedValue = null;
		protected object oSelectedItem = null;
		protected bool bIsDropdownResizable = false;
		
		// 3-dots button event handler
		protected UICustomEventEditor.OnClick MethodDelegate;
		
		// Extended Attributes
		[NonSerialized()]protected AttributeCollection oCustomAttributes = null;
		protected object oTag = null;
		protected object oDefaultValue = null;
		protected Type oDefaultType = null;
		
		// Custom Editor and Custom Type Converter
		[NonSerialized()]protected UITypeEditor oCustomEditor = null;
		[NonSerialized()]protected TypeConverter oCustomTypeConverter = null;
		
		#endregion
		
		#region "Public methods"
		
		public CustomProperty()
		{
			sName = "New Property";
			oValue = new string(' ',0);
		}
		
		public CustomProperty(string strName, object objValue, bool boolIsReadOnly, string strCategory, string strDescription, bool boolVisible)
		{
			sName = strName;
			oValue = objValue;
			bIsReadOnly = boolIsReadOnly;
			sDescription = strDescription;
			sCategory = strCategory;
			bVisible = boolVisible;
			if (oValue != null)
			{
				oDefaultValue = oValue;
			}
		}
		
		public CustomProperty(string strName, ref object objRef, string strProp, bool boolIsReadOnly, string strCategory, string strDescription, bool boolVisible)
		{
			sName = strName;
			bIsReadOnly = boolIsReadOnly;
			sDescription = strDescription;
			sCategory = strCategory;
			bVisible = boolVisible;
			bRef = true;
			oRef = objRef;
			sProp = strProp;
			if (Value != null)
			{
				oDefaultValue = Value;
			}
		}

        public void RebuildAttributes()
        {
            if (bUseFileNameEditor)
            {
                BuildAttributes_FilenameEditor();
            }
            else if (oChoices != null)
            {
                BuildAttributes_CustomChoices();
            }
            else if (oDatasource != null)
            {
                BuildAttributes_ListboxEditor();
            }
            else if (bIsBrowsable)
            {
                BuildAttributes_BrowsableProperty();
            }
        }
		
		#endregion
		
		#region "Private methods"
		
		private void BuildAttributes_FilenameEditor()
		{
			ArrayList attrs = new ArrayList();
			UIFilenameEditor.FileDialogFilterAttribute FilterAttribute = new UIFilenameEditor.FileDialogFilterAttribute(sFilter);
			UIFilenameEditor.SaveFileAttribute SaveDialogAttribute = new UIFilenameEditor.SaveFileAttribute();
			Attribute[] attrArray;
			attrs.Add(FilterAttribute);
			if (eDialogType == UIFilenameEditor.FileDialogType.SaveFileDialog)
			{
				attrs.Add(SaveDialogAttribute);
			}
			attrArray =  (System.Attribute[]) attrs.ToArray(typeof(Attribute));
			oCustomAttributes = new AttributeCollection(attrArray);
		}
		
		private void BuildAttributes_CustomChoices()
		{
			if (oChoices != null)
			{
				CustomChoices.CustomChoicesAttributeList list = new CustomChoices.CustomChoicesAttributeList(oChoices.Items);
				ArrayList attrs = new ArrayList();
				Attribute[] attrArray;
				attrs.Add(list);
				attrArray =  (System.Attribute[]) attrs.ToArray(typeof(Attribute));
				oCustomAttributes = new AttributeCollection(attrArray);
			}
		}
		
		private void BuildAttributes_ListboxEditor()
		{
			if (oDatasource != null)
			{
				UIListboxEditor.UIListboxDatasource ds = new UIListboxEditor.UIListboxDatasource(ref oDatasource);
				UIListboxEditor.UIListboxValueMember vm = new UIListboxEditor.UIListboxValueMember(sValueMember);
				UIListboxEditor.UIListboxDisplayMember dm = new UIListboxEditor.UIListboxDisplayMember(sDisplayMember);
				UIListboxEditor.UIListboxIsDropDownResizable ddr = null;
				ArrayList attrs = new ArrayList();
				attrs.Add(ds);
				attrs.Add(vm);
				attrs.Add(dm);
				if (bIsDropdownResizable)
				{
					ddr = new UIListboxEditor.UIListboxIsDropDownResizable();
					attrs.Add(ddr);
				}
				Attribute[] attrArray;
				attrArray =  (System.Attribute[]) attrs.ToArray(typeof(Attribute));
				oCustomAttributes = new AttributeCollection(attrArray);
			}
		}

        private void BuildAttributes_BrowsableProperty()
        {
            BrowsableTypeConverter.BrowsableLabelStyleAttribute style = new BrowsableTypeConverter.BrowsableLabelStyleAttribute(eBrowsablePropertyLabel);
            oCustomAttributes = new AttributeCollection(new Attribute[] { style });
        }
		
		private void BuildAttributes_CustomEventProperty()
		{
			UICustomEventEditor.DelegateAttribute attr = new UICustomEventEditor.DelegateAttribute(MethodDelegate);
			oCustomAttributes = new AttributeCollection(new Attribute[] {attr});
		}
		
		private object DataColumn
		{
			get
			{
				DataRow oRow = (System.Data.DataRow) oRef;
				if (oRow.RowState != DataRowState.Deleted)
				{
				if (oDatasource == null)
				{
					return oRow[sProp];
				}
				else
				{
					DataTable oLookupTable = oDatasource as DataTable;
					if (oLookupTable != null)
					{
						return oLookupTable.Select(sValueMember + "=" + oRow[sProp])[0][sDisplayMember];
					}
					else
					{
						Information.Err().Raise(Constants.vbObjectError + 513, null, "Bind of DataRow with a DataSource that is not a DataTable is not possible", null, null);
						return null;
					}
				}
			}
				else
				{
					return null;
				}
			}
			set
			{
				DataRow oRow = (System.Data.DataRow) oRef;
				if (oRow.RowState != DataRowState.Deleted)
				{
				if (oDatasource == null)
				{
					oRow[sProp] = value;
				}
				else
				{
					DataTable oLookupTable = oDatasource as DataTable;
					if (oLookupTable != null)
					{
						if (oLookupTable.Columns[sDisplayMember].DataType.Equals(System.Type.GetType("System.String")))
						{
							
							oRow[sProp] = oLookupTable.Select(oLookupTable.Columns[sDisplayMember].ColumnName + " = \'" + value + "\'")[0][sValueMember];
						}
						else
						{
							oRow[sProp] = oLookupTable.Select(oLookupTable.Columns[sDisplayMember].ColumnName + " = " + value)[0][sValueMember];
						}
					}
					else
					{
						Information.Err().Raise(Constants.vbObjectError + 514, null, "Bind of DataRow with a DataSource that is not a DataTable is impossible", null, null);
					}
				}
			}
		}
		}
		
		#endregion
		
		#region "Public properties"

        [Category("Appearance"), DisplayName("Name"), DescriptionAttribute("Display Name of the CustomProperty."), ParenthesizePropertyName(true), XmlElementAttribute("Name")]public string Name
        {
			get
			{
				return sName;
			}
			set
			{
				sName = value;
			}
		}

        [Category("Appearance"), DisplayName("ReadOnly"), DescriptionAttribute("Set read only attribute of the CustomProperty."), XmlElementAttribute("ReadOnly")]public bool IsReadOnly
        {
			get
			{
				return bIsReadOnly;
			}
			set
			{
				bIsReadOnly = value;
			}
		}

        [Category("Appearance"), DescriptionAttribute("Set visibility attribute of the CustomProperty.")]public bool Visible
        {
			get
			{
				return bVisible;
			}
			set
			{
				bVisible = value;
			}
		}

        [Category("Appearance"), DescriptionAttribute("Represent the Value of the CustomProperty.")]public object Value
        {
			get
			{
				if (bRef)
				{
                    if (oRef.GetType() == typeof(DataRow)  || oRef.GetType().IsSubclassOf(typeof(DataRow)))
						return this.DataColumn;
                    else
                        return Interaction.CallByName(oRef, sProp, CallType.Get, null);
				}
				else
				{
					return oValue;
				}
			}
			set
			{
				if (bRef)
				{
					if (oRef.GetType() == typeof(DataRow)  || oRef.GetType().IsSubclassOf(typeof(DataRow)))
							this.DataColumn = value;
                    else
                        Interaction.CallByName(oRef, sProp, CallType.Set, value);
				}
				else
				{
					oValue = value;
				}
			}
		}

        [Category("Appearance"), DescriptionAttribute("Set description associated with the CustomProperty.")]public string Description
        {
			get
			{
				return sDescription;
			}
			set
			{
				sDescription = value;
			}
		}

        [Category("Appearance"), DescriptionAttribute("Set category associated with the CustomProperty.")]public string Category
        {
			get
			{
				return sCategory;
			}
			set
			{
				sCategory = value;
			}
		}

        [XmlIgnore()]public System.Type Type
		{
			get
			{
				if (Value != null)
				{
					return Value.GetType();
				}
				else
				{
					if (oDefaultValue != null)
					{
						return oDefaultValue.GetType();
					}
					else
					{
						return oDefaultType;
					}
				}
            }
		}

        [XmlIgnore()]public AttributeCollection Attributes
		{
			get
			{
				return oCustomAttributes;
			}
			set
			{
				oCustomAttributes = value;
			}
		}

        [Category("Behavior"), DescriptionAttribute("Indicates if the property is browsable or not."), XmlElementAttribute(IsNullable = false)]public bool IsBrowsable
		{
			get
			{
				return bIsBrowsable;
			}
			set
			{
				bIsBrowsable = value;
				if (value == true)
				{
                    BuildAttributes_BrowsableProperty();
                }
			}
		}

		[Category("Appearance"), DisplayName("Parenthesize"), DescriptionAttribute("Indicates whether the name of the associated property is displayed with parentheses in the Properties window."), DefaultValue(false), XmlElementAttribute("Parenthesize")]public bool Parenthesize
		{
			get
			{
				return bParenthesize;
			}
			set
			{
				bParenthesize = value;
			}
		}

        [Category("Behavior"), DescriptionAttribute("Indicates the style of the label when a property is browsable."), XmlElementAttribute(IsNullable = false)]public BrowsableTypeConverter.LabelStyle BrowsableLabelStyle
        {
			get
			{
				return eBrowsablePropertyLabel;
			}
			set
			{
				bool Update = false;
				if (value != eBrowsablePropertyLabel)
				{
					Update = true;
				}
				eBrowsablePropertyLabel = value;
				if (Update)
				{
					BrowsableTypeConverter.BrowsableLabelStyleAttribute style = new BrowsableTypeConverter.BrowsableLabelStyleAttribute(value);
					oCustomAttributes = new AttributeCollection(new Attribute[] {style});
				}
			}
		}

        [Category("Behavior"), DescriptionAttribute("Indicates if the property is masked or not."), XmlElementAttribute(IsNullable = false)]public bool IsPassword
        {
			get
			{
				return bIsPassword;
			}
			set
			{
				bIsPassword = value;
			}
		}

        [Category("Behavior"), DescriptionAttribute("Indicates if the property represents a value in percentage."), XmlElementAttribute(IsNullable = false)]public bool IsPercentage
        {
			get
			{
				return bIsPercentage;
			}
			set
			{
				bIsPercentage = value;
			}
		}

        [Category("Behavior"), DescriptionAttribute("Indicates if the property uses a FileNameEditor converter."), XmlElementAttribute(IsNullable = false)]public bool UseFileNameEditor
        {
			get
			{
				return bUseFileNameEditor;
			}
			set
			{
				bUseFileNameEditor = value;
			}
		}

        [Category("Behavior"), DescriptionAttribute("Apply a filter to FileNameEditor converter."), XmlElementAttribute(IsNullable = false)]public string FileNameFilter
        {
			get
			{
				return sFilter;
			}
			set
			{
				bool UpdateAttributes = false;
				if (value != sFilter)
				{
					UpdateAttributes = true;
				}
				sFilter = value;
				if (UpdateAttributes)
				{
					BuildAttributes_FilenameEditor();
				}
			}
		}

        [Category("Behavior"), DescriptionAttribute("DialogType of the FileNameEditor."), XmlElementAttribute(IsNullable = false)]public UIFilenameEditor.FileDialogType FileNameDialogType
        {
			get
			{
				return eDialogType;
			}
			set
			{
				bool UpdateAttributes = false;
				if (value != eDialogType)
				{
					UpdateAttributes = true;
				}
				eDialogType = value;
				if (UpdateAttributes)
				{
					BuildAttributes_FilenameEditor();
				}
			}
		}

        [Category("Behavior"), DescriptionAttribute("Custom Choices list."), XmlIgnore()]public CustomChoices Choices
        {
			get
			{
				return oChoices;
			}
			set
			{
				oChoices = value;
				BuildAttributes_CustomChoices();
			}
		}

        [Category("Databinding"), XmlIgnore()]public object Datasource
        {
			get
			{
				return oDatasource;
			}
			set
			{
				oDatasource = value;
				BuildAttributes_ListboxEditor();
			}
		}

        [Category("Databinding"), XmlElementAttribute(IsNullable = false)]
        public string ValueMember
		{
			get
			{
				return sValueMember;
			}
			set
			{
				sValueMember = value;
				BuildAttributes_ListboxEditor();
			}
		}

        [Category("Databinding"), XmlElementAttribute(IsNullable = false)]
        public string DisplayMember
		{
			get
			{
				return sDisplayMember;
			}
			set
			{
				sDisplayMember = value;
				BuildAttributes_ListboxEditor();
			}
		}

        [Category("Databinding"), XmlElementAttribute(IsNullable = false)]
        public object SelectedValue
		{
			get
			{
				return oSelectedValue;
			}
			set
			{
				oSelectedValue = value;
			}
		}

        [Category("Databinding"), XmlElementAttribute(IsNullable = false)]
        public object SelectedItem
		{
			get
			{
				return oSelectedItem;
			}
			set
			{
				oSelectedItem = value;
			}
		}

        [Category("Databinding"), XmlElementAttribute(IsNullable = false)]
        public bool IsDropdownResizable
		{
			get
			{
				return bIsDropdownResizable;
			}
			set
			{
				bIsDropdownResizable = value;
				BuildAttributes_ListboxEditor();
			}
		}

        [XmlIgnore()]public UITypeEditor CustomEditor
		{
			get
			{
				return oCustomEditor;
			}
			set
			{
				oCustomEditor = value;
			}
		}

        [XmlIgnore()]public TypeConverter CustomTypeConverter
		{
			get
			{
				return oCustomTypeConverter;
			}
			set
			{
				oCustomTypeConverter = value;
			}
		}
				
		[XmlIgnore()]public object Tag
		{
			get
			{
				return oTag;
			}
			set
			{
				oTag = value;
			}
		}
		
		[XmlIgnore()]public object DefaultValue
		{
			get
			{
				return oDefaultValue;
			}
			set
			{
				oDefaultValue = value;
			}
		}
		
		[XmlIgnore()]public Type DefaultType
		{
			get
			{
				return oDefaultType;
			}
			set
			{
				oDefaultType = value;
			}
		}
		
		[XmlIgnore()]public UICustomEventEditor.OnClick OnClick
		{
			get
			{
				return MethodDelegate;
			}
			set
			{
				MethodDelegate = value;
				BuildAttributes_CustomEventProperty();
			}
		}
		
		#endregion
		
		#region "CustomPropertyDescriptor"
		public class CustomPropertyDescriptor : PropertyDescriptor
		{
			
			protected CustomProperty oCustomProperty;
			
			public CustomPropertyDescriptor(CustomProperty myProperty, Attribute[] attrs) : base(myProperty.Name, attrs)
			{
				if (myProperty == null)
				{
					oCustomProperty = null;
				}
				else
				{
					
					oCustomProperty = myProperty;
				}
			}
			
			public override bool CanResetValue(object component)
			{
				if ((oCustomProperty.DefaultValue != null)|| (oCustomProperty.DefaultType != null))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			
			public override System.Type ComponentType
			{
				get
				{
					return this.GetType();
				}
			}
			
			public override object GetValue(object component)
			{
				return oCustomProperty.Value;
			}
			
			public override bool IsReadOnly
			{
				get
				{
					return oCustomProperty.IsReadOnly;
				}
			}
			
			public override System.Type PropertyType
			{
				get
				{
					return oCustomProperty.Type;
				}
			}
			
			public override void ResetValue(object component)
			{
				oCustomProperty.Value = oCustomProperty.DefaultValue;
				this.OnValueChanged(component, EventArgs.Empty);
			}
			
			public override void SetValue(object component, object value)
			{
				oCustomProperty.Value = value;
				this.OnValueChanged(component, EventArgs.Empty);
			}
			
			public override bool ShouldSerializeValue(object component)
			{
				object oValue = oCustomProperty.Value;
				if ((oCustomProperty.DefaultValue != null)&& (oValue != null))
				{
					return ! oValue.Equals(oCustomProperty.DefaultValue);
				}
				else
				{
					return false;
				}
			}
			
			public override string Description
			{
				get
				{
					return oCustomProperty.Description;
				}
			}
			
			public override string Category
			{
				get
				{
					return oCustomProperty.Category;
				}
			}
			
			public override string DisplayName
			{
				get
				{
					return oCustomProperty.Name;
				}
			}
			
			public override bool IsBrowsable
			{
				get
				{
					return oCustomProperty.IsBrowsable;
				}
			}
			
			public object CustomProperty
			{
				get
				{
					return oCustomProperty;
				}
			}
		}
		#endregion		
	}	
}