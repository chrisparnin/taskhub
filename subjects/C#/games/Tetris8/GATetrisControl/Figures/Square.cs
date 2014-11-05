using System;
using System.Collections;
using System.Drawing;

namespace GATetrisControl
{
	internal class Square : Figure
	{

		public Square(TetrisGrid tg) : base(tg)
		{
			yPosition = 0;
			xPosition = (columns - 2) / 2;
			color = parent.settings.squareColor;
		}


		protected override ArrayList GetRectsIndexes()
		{
			ArrayList indexes = new ArrayList();
			int start = yPosition * columns + xPosition;
			indexes.Add(start);
			indexes.Add(start + 1);
			indexes.Add(start + columns);
			indexes.Add(start + columns + 1);

			width = 2;
			height = 2;

			return indexes;
		}

		internal override void DrawPreview(Graphics g)
		{
			int a = parent.settings.squareWidth;

			Rectangle r1 = new Rectangle(a * 2,a * 2,a,a);
			Rectangle r2 = new Rectangle(a * 3,a * 2,a,a);
			Rectangle r3 = new Rectangle(a * 2,a * 3,a,a);
			Rectangle r4 = new Rectangle(a * 3,a * 3,a,a);
			
			DrawPreviewSquare(r1,g, color);
			DrawPreviewSquare(r2,g, color);
			DrawPreviewSquare(r3,g, color);
			DrawPreviewSquare(r4,g, color);
		}

	}
}
