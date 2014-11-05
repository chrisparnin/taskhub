using System;
using System.Drawing;
using System.Diagnostics;

namespace GATetrisControl
{
	public class SingleSquare
	{

		public SingleSquare(TetrisGrid tg)
		{
			parent = tg;
			filled = false;
			rect = Rectangle.Empty;
		}



		internal void Draw(Graphics g)
		{
			Rectangle r = rect;
			r.Width -= 2;
			r.Height -= 2;

			SolidBrush brush = new SolidBrush(color);

			g.FillRectangle(brush,r.Left+1,r.Top+1,r.Width-1,r.Height-1);

			g.DrawLine(new Pen(parent.settings.lightBorder),r.Left,r.Bottom,r.Left,r.Top);
			g.DrawLine(new Pen(parent.settings.lightBorder),r.Left,r.Top,r.Right,r.Top);
			g.DrawLine(new Pen(parent.settings.darkBorder),r.Right,r.Top,r.Right,r.Bottom);
			g.DrawLine(new Pen(parent.settings.darkBorder),r.Right,r.Bottom,r.Left,r.Bottom);

			brush.Dispose();
		}


		internal Rectangle rect;
		internal bool filled;
		internal Color color;
		internal TetrisGrid parent;

	}
}
