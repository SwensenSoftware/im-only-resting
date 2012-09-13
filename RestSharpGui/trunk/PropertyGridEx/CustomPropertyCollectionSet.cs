namespace PropertyGridEx
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class CustomPropertyCollectionSet : System.Collections.CollectionBase
	{
		
		
		public virtual int Add(CustomPropertyCollection value)
		{
			return base.List.Add(value);
		}
		
		public virtual int Add()
		{
			return base.List.Add(new CustomPropertyCollection());
		}
		
		public virtual CustomPropertyCollection this[int index]
		{
			get
			{
				return ((CustomPropertyCollection) base.List[index]);
			}
			set
			{
				base.List[index] = value;
			}
		}
		
		public virtual object ToArray()
		{
			ArrayList list = new ArrayList();
			list.AddRange(base.List);
			return list.ToArray(typeof(CustomPropertyCollection));
		}
		
	}
}
