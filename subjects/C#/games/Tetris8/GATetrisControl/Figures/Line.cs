using System;
using System.Collections;
using System.Drawing;

namespace GATetrisControl
{
	internal class Line : Figure
	{

		public Line(TetrisGrid tg) : base(tg)
		{
			yPosition = 0;
			xPosition = (columns - 1)  /  2;
			color = parent.settings.lineColor;
		}


		protected override ArrayList GetRectsIndexes()
		{
			ArrayList indexes = new ArrayList();
			int start = yPosition * columns + xPosition;

			switch(angle)
			{
				case RotateAngle.Deg90:
				case RotateAngle.Deg270:
					indexes.Add(start);
					indexes.Add(start + 1);
					indexes.Add(start + 2);
					indexes.Add(start + 3);

					width = 4;
					height = 1;
					break;
				case RotateAngle.Deg0:
				case RotateAngle.Deg180:
					indexes.Add(start);
					indexes.Add(start + columns);
					indexes.Add(start + columns * 2);
					indexes.Add(start + columns * 3);

					width = 1;
					height = 4;
					break;
			}

			return indexes;
		}

		internal override void DrawPreview(Graphics g)
		{
			int a = parent.settings.squareWidth;
			
			Rectangle r1 = new Rectangle((5 * a) / 2,a,a,a);
			Rectangle r2 = new Rectangle((5 * a) / 2,a * 2,a,a);
			Rectangle r3 = new Rectangle((5 * a) / 2,a * 3,a,a);
			Rectangle r4 = new Rectangle((5 * a) / 2,a * 4,a,a);
			
			DrawPreviewSquare(r1,g, color);
			DrawPreviewSquare(r2,g, color);
			DrawPreviewSquare(r3,g, color);
			DrawPreviewSquare(r4,g, color);
		}

		
	}
}
