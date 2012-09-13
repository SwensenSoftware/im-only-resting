namespace PropertyGridEx
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;

    [Serializable()]public class CustomChoices : ArrayList
	{				
		public CustomChoices(ArrayList array, bool IsSorted)
		{
			this.AddRange(array);
			if (IsSorted)
			{
				this.Sort();
			}
		}
		
		public CustomChoices(ArrayList array)
		{
			this.AddRange(array);
		}
		
		public CustomChoices(string[] array, bool IsSorted)
		{
			this.AddRange(array);
			if (IsSorted)
			{
				this.Sort();
			}
		}
		
		public CustomChoices(string[] array)
		{
			this.AddRange(array);
		}
		
		public CustomChoices(int[] array, bool IsSorted)
		{
			this.AddRange(array);
			if (IsSorted)
			{
				this.Sort();
			}
		}
		
		public CustomChoices(int[] array)
		{
			this.AddRange(array);
		}
		
		public CustomChoices(double[] array, bool IsSorted)
		{
			this.AddRange(array);
			if (IsSorted)
			{
				this.Sort();
			}
		}
		
		public CustomChoices(double[] array)
		{
			this.AddRange(array);
		}
		
		public CustomChoices(object[] array, bool IsSorted)
		{
			this.AddRange(array);
			if (IsSorted)
			{
				this.Sort();
			}
		}
		
		public CustomChoices(object[] array)
		{
			this.AddRange(array);
		}
		
		public ArrayList Items
		{
			get
			{
				return this;
			}
		}
		
		public class CustomChoicesTypeConverter : TypeConverter
		{
			
			private CustomChoicesAttributeList oChoices = null;
			public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
			{
				bool returnValue;
				CustomChoicesAttributeList Choices =  (CustomChoicesAttributeList) context.PropertyDescriptor.Attributes[typeof(CustomChoicesAttributeList)];
				if (oChoices != null)
				{
					return true;
				}
				if (Choices != null)
				{
					oChoices = Choices;
					returnValue = true;
				}
				else
				{
					returnValue = false;
				}
				return returnValue;
			}
			public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
			{
				bool returnValue;
				CustomChoicesAttributeList Choices =  (CustomChoicesAttributeList) context.PropertyDescriptor.Attributes[typeof(CustomChoicesAttributeList)];
				if (oChoices != null)
				{
					return true;
				}
				if (Choices != null)
				{
					oChoices = Choices;
					returnValue = true;
				}
				else
				{
					returnValue = false;
				}
				return returnValue;
			}
			public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
			{
				CustomChoicesAttributeList Choices =  (CustomChoices.CustomChoicesAttributeList) context.PropertyDescriptor.Attributes[typeof(CustomChoicesAttributeList)];
				if (oChoices != null)
				{
					return oChoices.Values;
				}
				return base.GetStandardValues(context);
			}
		}
		
		public class CustomChoicesAttributeList : Attribute
		{
			
			private ArrayList oList = new ArrayList();
			
			public ArrayList Item
			{
				get
				{
					return this.oList;
				}
			}
			
			public TypeConverter.StandardValuesCollection Values
			{
				get
				{
					return new TypeConverter.StandardValuesCollection(this.oList);
				}
			}
			
			public CustomChoicesAttributeList(string[] List)
			{
				oList.AddRange(List);
			}
			
			public CustomChoicesAttributeList(ArrayList List)
			{
				oList.AddRange(List);
			}
			
			public CustomChoicesAttributeList(ListBox.ObjectCollection List)
			{
				oList.AddRange(List);
			}
		}
	}
}
