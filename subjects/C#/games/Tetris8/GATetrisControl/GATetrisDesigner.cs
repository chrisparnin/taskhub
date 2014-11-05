using System;
using System.Reflection;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace GATetrisControl
{
	public class TetrisGridContainerDesigner : ParentControlDesigner
	{

		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize (component);
			tetris = component as GATetris;			
			
			Settings sett = new Settings();
			tetris.grid.settings = sett;
			sett.container = tetris;
			sett.ApplySettings();
		}

		protected override void PreFilterProperties(System.Collections.IDictionary props)
		{
			props.Remove("Size");
			props.Remove("Font");
			props.Remove("DockPadding");
			props.Remove("Dock");
			props.Remove("Anchor");
			props.Remove("BackColor");
			props.Remove("ForeColor");
			props.Remove("BackgroundImage");
			props.Remove("AutoScroll");
			props.Remove("AutoScrollMargin");
			props.Remove("AutoScrollMinSize");

			base.PreFilterProperties (props);
		}


		protected override bool DrawGrid
		{
			get
			{
				return false;
			}
			set
			{
				return;
			}
		}



		internal GATetris tetris;

	}
}
