namespace Swensen.Ior.Forms
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class PropertyGridEx : PropertyGrid
	{			
		#region "Protected variables and objects"
		// CustomPropertyCollection assigned to MyBase.SelectedObject
		protected CustomPropertyCollection oCustomPropertyCollection;
		protected bool bShowCustomProperties;
		
		// CustomPropertyCollectionSet assigned to MyBase.SelectedObjects
		protected CustomPropertyCollectionSet oCustomPropertyCollectionSet;
		protected bool bShowCustomPropertiesSet;

		// Internal PropertyGrid Controls
		protected object oPropertyGridView;
		protected object oHotCommands;
		protected object oDocComment;
		protected ToolStrip oToolStrip;
		
		// Internal PropertyGrid Fields
		protected Label oDocCommentTitle;
		protected Label oDocCommentDescription;
		protected FieldInfo oPropertyGridEntries;

        // Properties variables
        protected bool bAutoSizeProperties;
        protected bool bDrawFlatToolbar;
        #endregion
		
		#region "Public Functions"
		public PropertyGridEx()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			
			// Add any initialization after the InitializeComponent() call.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			// Initialize collections
            oCustomPropertyCollection = new CustomPropertyCollection();
            oCustomPropertyCollectionSet = new CustomPropertyCollectionSet();
			
			// Attach internal controls
			oPropertyGridView = base.GetType().BaseType.InvokeMember("gridView", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this, null);
			oHotCommands = base.GetType().BaseType.InvokeMember("hotcommands", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this, null);
			oToolStrip = (ToolStrip) base.GetType().BaseType.InvokeMember("toolStrip", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this, null);
			oDocComment = base.GetType().BaseType.InvokeMember("doccomment", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this, null);
			
			// Attach DocComment internal fields
			if (oDocComment != null)
			{
				oDocCommentTitle = (Label)oDocComment.GetType().InvokeMember("m_labelTitle", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, oDocComment, null);
				oDocCommentDescription = (Label)oDocComment.GetType().InvokeMember("m_labelDesc", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, oDocComment, null);
			}
			
			// Attach PropertyGridView internal fields
			if (oPropertyGridView != null)
			{
				oPropertyGridEntries = oPropertyGridView.GetType().GetField("allGridEntries", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			}
			
			// Apply Toolstrip style
			if (oToolStrip != null)
			{
				ApplyToolStripRenderMode(bDrawFlatToolbar);
			}
			

        }
		
		public void MoveSplitterTo(int x)
		{
            oPropertyGridView.GetType().InvokeMember("MoveSplitterTo", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, oPropertyGridView, new object[] { x });
        }
		
		public override void Refresh()
		{
			if (bShowCustomPropertiesSet)
			{
				base.SelectedObjects =  (object[]) oCustomPropertyCollectionSet.ToArray();
			}
			base.Refresh();
			if (bAutoSizeProperties)
			{
				AutoSizeSplitter();
			}
		}
		
		public void SetComment(string title, string description)
		{
            MethodInfo method = oDocComment.GetType().GetMethod("SetComment");
            method.Invoke(oDocComment, new object[] { title, description });
			//oDocComment.SetComment(title, description);
		}
		
		#endregion
		
		#region "Protected Functions"
		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
			if (bAutoSizeProperties)
			{
				AutoSizeSplitter();
			}
		}
		
		protected void AutoSizeSplitter()
		{
            int RightMargin = 52;
			GridItemCollection oItemCollection =  (System.Windows.Forms.GridItemCollection) oPropertyGridEntries.GetValue(oPropertyGridView);
			if (oItemCollection == null)
			{
				return;
			}
			System.Drawing.Graphics oGraphics = System.Drawing.Graphics.FromHwnd(this.Handle);
			int CurWidth = 0;
			int MaxWidth = 0;
			
			foreach (GridItem oItem in oItemCollection)
			{
				if (oItem.GridItemType == GridItemType.Property)
				{
					CurWidth =  (int) oGraphics.MeasureString(oItem.Label, this.Font).Width + RightMargin;
					if (CurWidth > MaxWidth)
					{
						MaxWidth = CurWidth;
					}
				}
			}
			
			MoveSplitterTo(MaxWidth);
		}
		protected void ApplyToolStripRenderMode(bool value)
		{
			if (value)
			{
				oToolStrip.Renderer = new ToolStripSystemRenderer();
			}
			else
			{
                ToolStripProfessionalRenderer renderer = new ToolStripProfessionalRenderer(new CustomColorScheme());
                renderer.RoundedEdges = false;
				oToolStrip.Renderer = renderer;
			}
		}
		#endregion
		
		#region "Properties"

        [Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Set the collection of the CustomProperty. Set ShowCustomProperties to True to enable it."), RefreshProperties(RefreshProperties.Repaint)]public CustomPropertyCollection Item
        {
			get
			{
				return oCustomPropertyCollection;
			}
		}

		[Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Set the CustomPropertyCollectionSet. Set ShowCustomPropertiesSet to True to enable it."), RefreshProperties(RefreshProperties.Repaint)]public CustomPropertyCollectionSet ItemSet
        {
			get
			{
				return oCustomPropertyCollectionSet;
			}
		}

        [Category("Behavior"), DefaultValue(false), DescriptionAttribute("Move automatically the splitter to better fit all the properties shown.")]public bool AutoSizeProperties
        {
			get
			{
				return bAutoSizeProperties;
			}
			set
			{
				bAutoSizeProperties = value;
				if (value)
				{
					AutoSizeSplitter();
				}
			}
		}

        [Category("Behavior"), DefaultValue(false), DescriptionAttribute("Use the custom properties collection as SelectedObject."), RefreshProperties(RefreshProperties.All)]public bool ShowCustomProperties
        {
			get
			{
				return bShowCustomProperties;
			}
			set
			{
				if (value == true)
				{
					bShowCustomPropertiesSet = false;
					base.SelectedObject = oCustomPropertyCollection;
				}
				bShowCustomProperties = value;
			}
		}

        [Category("Behavior"), DefaultValue(false), DescriptionAttribute("Use the custom properties collections as SelectedObjects."), RefreshProperties(RefreshProperties.All)]public bool ShowCustomPropertiesSet
        {
			get
			{
				return bShowCustomPropertiesSet;
			}
			set
			{
				if (value == true)
				{
					bShowCustomProperties = false;
					base.SelectedObjects =  (object[]) oCustomPropertyCollectionSet.ToArray();
				}
				bShowCustomPropertiesSet = value;
			}
		}

		[Category("Appearance"), DefaultValue(false), DescriptionAttribute("Draw a flat toolbar")]public new bool DrawFlatToolbar
        {
			get
			{
				return bDrawFlatToolbar;
			}
			set
			{
				bDrawFlatToolbar = value;
				ApplyToolStripRenderMode(bDrawFlatToolbar);
			}
		}

        [Category("Appearance"), DisplayName("Toolstrip"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Toolbar object"), Browsable(true)]public ToolStrip ToolStrip
        {
			get
			{
				return oToolStrip;
			}
		}
		
		[Category("Appearance"), DisplayName("Help"), DescriptionAttribute("DocComment object. Represent the comments area of the PropertyGrid."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]public Control DocComment
		{
			get
			{
				return  (System.Windows.Forms.Control) oDocComment;
			}
		}
		
		[Category("Appearance"), DisplayName("HelpTitle"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Help Title Label."), Browsable(true)]public Label DocCommentTitle
		{
			get
			{
				return oDocCommentTitle;
			}
		}
		
		[Category("Appearance"), DisplayName("HelpDescription"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Help Description Label."), Browsable(true)]public Label DocCommentDescription
		{
			get
			{
				return oDocCommentDescription;
			}
		}
		
		[Category("Appearance"), DisplayName("HelpImageBackground"), DescriptionAttribute("Help Image Background.")]public Image DocCommentImage
		{
			get
			{
				return ((Control)oDocComment).BackgroundImage;
			}
			set
			{
                ((Control)oDocComment).BackgroundImage = value;
			}
		}
		
		#endregion
		
	}
	
}

