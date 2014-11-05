using System;
using System.Collections;
using System.Drawing;

namespace GATetrisControl
{
	internal class RightT : Figure
	{

		public RightT(TetrisGrid tg) : base(tg)
		{
			yPosition = 0;
			xPosition = (columns - 2)  /  2;
			color = parent.settings.rightTColor;
		}


		protected override ArrayList GetRectsIndexes()
		{
			ArrayList indexes = new ArrayList();
			int start = yPosition * columns + xPosition;
			switch(angle)
			{
				case RotateAngle.Deg0:
					indexes.Add(start);
					indexes.Add(start + 1);
					indexes.Add(start + columns);
					indexes.Add(start + columns * 2);

					width = 2;
					height = 3;
					break;
				case RotateAngle.Deg90:
					indexes.Add(start);
					indexes.Add(start + 1);
					indexes.Add(start + 2);
					indexes.Add(start + columns + 2);

					width = 3;
					height = 2;
					break;
				case RotateAngle.Deg180:
					indexes.Add(start + 1);
					indexes.Add(start + columns + 1);
					indexes.Add(start + columns * 2);
					indexes.Add(start + columns * 2 + 1);

					width = 2;
					height = 3;
					break;
				case RotateAngle.Deg270:
					indexes.Add(start);
					indexes.Add(start + columns);
					indexes.Add(start + columns + 1);
					indexes.Add(start + columns + 2);

					width = 3;
					height = 2;
					break;
			}

			return indexes;
		}

		internal override void DrawPreview(Graphics g)
		{
			int a = parent.settings.squareWidth;
			
			Rectangle r1 = new Rectangle(2  *  a,(3  *  a)  /  2,a,a);
			Rectangle r2 = new Rectangle(3  *  a,(3  *  a)  /  2,a,a);
			Rectangle r3 = new Rectangle(2  *  a,(3  *  a)  /  2  +  a,a,a);
			Rectangle r4 = new Rectangle(2  *  a,(3  *  a)  /  2  +  2  *  a,a,a);
			
			DrawPreviewSquare(r1,g, color);
			DrawPreviewSquare(r2,g, color);
			DrawPreviewSquare(r3,g, color);
			DrawPreviewSquare(r4,g, color);
		}

	}
}
