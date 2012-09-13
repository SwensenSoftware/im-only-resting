namespace PropertyGridEx
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class BrowsableTypeConverter : ExpandableObjectConverter
	{			
		public enum LabelStyle
		{
			lsNormal,
			lsTypeName,
			lsEllipsis
		}
		
		public class BrowsableLabelStyleAttribute : Attribute
		{
			
			private LabelStyle eLabelStyle = LabelStyle.lsNormal;
			public BrowsableLabelStyleAttribute(LabelStyle LabelStyle)
			{
				eLabelStyle = LabelStyle;
			}
			public LabelStyle LabelStyle
			{
				get
				{
					return eLabelStyle;
				}
				set
				{
					eLabelStyle = value;
				}
			}
		}
		
		public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType)
		{
			return true;
		}
		
		public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
		{
            BrowsableLabelStyleAttribute attribute1 = (BrowsableLabelStyleAttribute)context.PropertyDescriptor.Attributes[typeof(BrowsableLabelStyleAttribute)];
            if (attribute1 != null)
            {
                switch (attribute1.LabelStyle)
                {
                    case LabelStyle.lsNormal:
                        {
                            return base.ConvertTo(context, culture, RuntimeHelpers.GetObjectValue(value), destinationType);
                        }
                    case LabelStyle.lsTypeName:
                        {
                            return ("(" + value.GetType().Name + ")");
                        }
                    case LabelStyle.lsEllipsis:
                        {
                            return "(...)";
                        }
                }
            }
            return base.ConvertTo(context, culture, RuntimeHelpers.GetObjectValue(value), destinationType);
        }
		
	}
}
