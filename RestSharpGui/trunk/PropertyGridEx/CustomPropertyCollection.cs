namespace PropertyGridEx
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;

	[Serializable()]public class CustomPropertyCollection : System.Collections.CollectionBase, ICustomTypeDescriptor
	{
		#region "Collection related methods"
	
		public virtual int Add(CustomProperty value)
		{
			return base.List.Add(value);
		}
		
		public virtual int Add(string strName, object objValue, bool boolIsReadOnly, string strCategory, string strDescription, bool boolVisible)
		{
			return base.List.Add(new CustomProperty(strName, objValue, boolIsReadOnly, strCategory, strDescription, boolVisible));
		}
		
		public virtual int Add(string strName, ref object objRef, string strProp, bool boolIsReadOnly, string strCategory, string strDescription, bool boolVisible)
		{
			return base.List.Add(new CustomProperty(strName, ref objRef, strProp, boolIsReadOnly, strCategory, strDescription, boolVisible));
		}
		
		public virtual CustomProperty this[int index]
		{
			get
			{
				return ((CustomProperty) base.List[index]);
			}
			set
			{
				base.List[index] = value;
			}
		}
		
		public virtual void Remove(string Name)
		{
			CustomProperty CustomProp;
			foreach (CustomProperty tempLoopVar_CustomProp in base.List)
			{
				CustomProp = tempLoopVar_CustomProp;
				if (CustomProp.Name == Name)
				{
					base.List.Remove(CustomProp);
					return;
				}
			}
		}

        #endregion
    		
	    #region "Implements ICustomTypeDescriptor"
		
		public System.ComponentModel.AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}
		
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}
		
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}
		
		public System.ComponentModel.TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}
		
		public System.ComponentModel.EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}
		
		public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}
		
		public object GetEditor(System.Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}
		
		public System.ComponentModel.EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}
		
		public System.ComponentModel.EventDescriptorCollection GetEvents(System.Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}
		
		public System.ComponentModel.PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}
		
		public System.ComponentModel.PropertyDescriptorCollection GetProperties(System.Attribute[] attributes)
		{
			PropertyDescriptorCollection Properties = new PropertyDescriptorCollection(null);
			CustomProperty CustomProp;
			foreach (CustomProperty tempLoopVar_CustomProp in base.List)
			{
				CustomProp = tempLoopVar_CustomProp;
				if (CustomProp.Visible)
				{
					ArrayList attrs = new ArrayList();
					
					// Expandable Object Converter
					if (CustomProp.IsBrowsable)
					{
						attrs.Add(new TypeConverterAttribute(typeof(BrowsableTypeConverter)));
					}
					
					// The Filename Editor
					if (CustomProp.UseFileNameEditor == true)
					{
						attrs.Add(new EditorAttribute(typeof(UIFilenameEditor), typeof(UITypeEditor)));
					}
					
					// Custom Choices Type Converter
					if (CustomProp.Choices != null)
					{
						attrs.Add(new TypeConverterAttribute(typeof(CustomChoices.CustomChoicesTypeConverter)));
					}
					
					// Password Property
					if (CustomProp.IsPassword)
					{
						attrs.Add(new PasswordPropertyTextAttribute(true));
					}
					
					// Parenthesize Property
					if (CustomProp.Parenthesize)
					{
						attrs.Add(new ParenthesizePropertyNameAttribute(true));
					}
					
					// Datasource
					if (CustomProp.Datasource != null)
					{
						attrs.Add(new EditorAttribute(typeof(UIListboxEditor), typeof(UITypeEditor)));
					}
					
					// Custom Editor
					if (CustomProp.CustomEditor != null)
					{
						attrs.Add(new EditorAttribute(CustomProp.CustomEditor.GetType(), typeof(UITypeEditor)));
					}
					
					// Custom Type Converter
					if (CustomProp.CustomTypeConverter != null)
					{
						attrs.Add(new TypeConverterAttribute(CustomProp.CustomTypeConverter.GetType()));
					}

                    // Is Percentage
                    if (CustomProp.IsPercentage)
                    {
                        attrs.Add(new TypeConverterAttribute(typeof(OpacityConverter)));
                    }
					
					// 3-dots button event delegate
					if (CustomProp.OnClick != null)
					{
						attrs.Add(new EditorAttribute(typeof(UICustomEventEditor), typeof(UITypeEditor)));
					}
					
					// Default value attribute
					if (CustomProp.DefaultValue != null)
					{
						attrs.Add(new DefaultValueAttribute(CustomProp.Type, CustomProp.Value.ToString()));
					}
					else
					{
						// Default type attribute
						if (CustomProp.DefaultType != null)
						{
							attrs.Add(new DefaultValueAttribute(CustomProp.DefaultType, null));
						}
					}
					
					// Extra Attributes
					if (CustomProp.Attributes != null)
					{
						attrs.AddRange(CustomProp.Attributes);
					}
					
					// Add my own attributes
					Attribute[] attrArray =  (System.Attribute[]) attrs.ToArray(typeof(Attribute));
					Properties.Add(new CustomProperty.CustomPropertyDescriptor(CustomProp, attrArray));
				}
			}
			return Properties;
		}
		
		public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
		{
			return this;
		}			

	#endregion

	    #region "Serialize & Deserialize related methods"
	
	    public void SaveXml(string filename)
	    {
		    XmlSerializer serializer = new XmlSerializer(typeof(CustomPropertyCollection));
		    FileStream writer = new FileStream(filename, FileMode.Create);
		    try
		    {
			    serializer.Serialize(writer, this);
		    }
		    catch (Exception ex)
		    {
			    MessageBox.Show(ex.InnerException.Message);
		    }
		    writer.Close();
	    }
    	
	    public bool LoadXml(string filename)
	    {
		    try
		    {
			    XmlSerializer serializer = new XmlSerializer(typeof(CustomPropertyCollection));
			    FileStream reader = new FileStream(filename, FileMode.Open);
    			
			    CustomPropertyCollection cpc = (CustomPropertyCollection) serializer.Deserialize(reader);
			    foreach (CustomProperty customprop in cpc)
			    {
				    customprop.RebuildAttributes();
				    this.Add(customprop);
			    }
			    cpc = null;
			    reader.Close();
			    return true;
    			
		    }
		    catch (Exception)
		    {
			    return false;
		    }
    		
	    }
    	
	    public void SaveBinary(string filename)
	    {
		    Stream stream = File.Create(filename);
		    BinaryFormatter serializer = new BinaryFormatter();
		    try
		    {
			    serializer.Serialize(stream, this);
		    }
		    catch (Exception ex)
		    {
			    MessageBox.Show(ex.InnerException.Message);
		    }
		    stream.Close();
	    }
    	
	    public bool LoadBinary(string filename)
	    {
		    try
		    {
			    Stream stream = File.Open(filename, FileMode.Open);
			    BinaryFormatter formatter = new BinaryFormatter();
			    if (stream.Length > 0)
			    {
				    CustomPropertyCollection cpc = (CustomPropertyCollection) formatter.Deserialize(stream);
				    foreach (CustomProperty customprop in cpc)
				    {
					    customprop.RebuildAttributes();
					    this.Add(customprop);
				    }
				    cpc = null;
				    stream.Close();
				    return true;
			    }
			    else
			    {
				    stream.Close();
				    return false;
			    }
    			
		    }
		    catch (Exception)
		    {
			    return false;
		    }
        }

        #endregion	    
    }
}
