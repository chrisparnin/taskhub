using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GATetrisControl
{

	public enum RotateDirection
	{
		ClockWise,
		AntiClockWise
	}

	public enum RotateAngle
	{
		Deg0,
		Deg90,
		Deg180,
		Deg270
	}

	public enum MoveDirection
	{
		Left,
		Right,
		Down,
		Rotate
	}



	internal class Figure
	{

		public Figure(TetrisGrid grid)
		{
			parent = grid;
			columns = parent.settings.columns;
			rows = parent.settings.rows;

			angle = RotateAngle.Deg0;
			direction = RotateDirection.ClockWise;
		}



        /// <summary>
        /// Gets the indexes of the SingleSquare objects the figure has currently occupied.
        /// </summary>
        /// <returns></returns>
		protected virtual ArrayList GetRectsIndexes()
		{
			return null;
		}

        internal virtual void DrawPreview(Graphics g)
		{
		}




       	internal void Move(MoveDirection dir)
		{
			if(!CanMove(dir))
			{
				//if we tried to move figure down and we failed, so we need new figure
				if(dir == MoveDirection.Down)
					parent.newFigure = true;
				return;
			}
			//first clear previous location of the figure
			ClearFigure();
			switch(dir)
			{
				case MoveDirection.Left:
					xPosition -= 1;
					break;
				case MoveDirection.Right:
					xPosition += 1;
					break;
				case MoveDirection.Down:
					yPosition += 1;
					break;
				case MoveDirection.Rotate:
					ChangeRotateAngle();
					break;
			}
			//draw the figure at the new location
			DrawFigure();

			//refresh the tetris grid
			parent.SmartRefresh();
		}
		
		internal bool CanDraw()
		{
			//get the indexes of the figure and if twe have at least one which is filled
			//return false
			foreach(int i in GetRectsIndexes())
			{
				if(parent.squares[i].filled)
					return false;
			}

			return true;
		}



        internal void DrawFigure()
		{
			//get the indexes of the squares this figure occupies
			foreach(int index in GetRectsIndexes())
			{
				//add the rect in the invalid rects of the parent grid
				parent.invalidRects.Add(parent.squares[index]);
				//set the color for the square
				parent.squares[index].color = color;
				//set it to 'filled'
				parent.squares[index].filled = true;
			}
		}

		internal void DrawPreviewSquare(Rectangle r, Graphics g, Color c)
		{					
			r.Width -= 2;
			r.Height -= 2;

			g.FillRectangle(new SolidBrush(c),r);
			g.DrawLine(new Pen(parent.settings.lightBorder),r.Left,r.Bottom,r.Left,r.Top);
			g.DrawLine(new Pen(parent.settings.lightBorder),r.Left,r.Top,r.Right,r.Top);
			g.DrawLine(new Pen(parent.settings.darkBorder),r.Right,r.Top,r.Right,r.Bottom);
			g.DrawLine(new Pen(parent.settings.darkBorder),r.Right,r.Bottom,r.Left,r.Bottom);			
		}

		internal void ClearFigure()
		{
			//get the indexes of the squares thid figure occupies
			foreach(int index in GetRectsIndexes())
			{
				//add the square to the parent's invalid rects
				parent.invalidRects.Add(parent.squares[index]);
				//drop the flag 'filled'
				parent.squares[index].filled = false;
			}
		}


        /// <summary>
        /// changes the rotation angle according to the rotate direction
        /// </summary>
		private void ChangeRotateAngle()
		{
			switch(angle)
			{
				case RotateAngle.Deg0:
					switch(direction)
					{
						case RotateDirection.ClockWise:
							angle = RotateAngle.Deg90;
							break;
						case RotateDirection.AntiClockWise:
							angle = RotateAngle.Deg270;
							break;
					}
					break;
				case RotateAngle.Deg90:
					switch(direction)
					{
						case RotateDirection.ClockWise:
							angle = RotateAngle.Deg180;
							break;
						case RotateDirection.AntiClockWise:
							angle = RotateAngle.Deg0;
							break;
					}
					break;
				case RotateAngle.Deg180:
					switch(direction)
					{
						case RotateDirection.ClockWise:
							angle = RotateAngle.Deg270;
							break;
						case RotateDirection.AntiClockWise:
							angle = RotateAngle.Deg90;
							break;
					}
					break;
				case RotateAngle.Deg270:
					switch(direction)
					{
						case RotateDirection.ClockWise:
							angle = RotateAngle.Deg0;
							break;
						case RotateDirection.AntiClockWise:
							angle = RotateAngle.Deg180;
							break;
					}
					break;
			}
		}

		private ArrayList GetDifferentIndexes(ArrayList oldIndexes, ArrayList newIndexes)
		{
			//the different indexes
			ArrayList different = new ArrayList();
			foreach(int i in newIndexes)
			{
				if(oldIndexes.Contains(i))
					continue;
				different.Add(i);
			}

			return different;
		}

		private bool CanMove(MoveDirection dir)
		{
			ArrayList oldIndexes = GetRectsIndexes();
			ArrayList newIndexes = new ArrayList();

            switch(dir)
			{
				case MoveDirection.Down:
					//if we have reached the end of the rows - do nothing
					if(yPosition + height == rows)
						return false;
					//perform fake offset
					yPosition += 1;
					//get the indexes with the new offset
					newIndexes = GetRectsIndexes();
					//restore previous position
					yPosition -= 1;
					break;
				case MoveDirection.Left:
					//if we are at the left side of the grid - do nothing
					if(xPosition == 0)
						return false;
					//perform fake offset
					xPosition -= 1;
					//get the indexes with the new offset
					newIndexes = GetRectsIndexes();
					//restore previous location
					xPosition += 1;
					break;
				case MoveDirection.Right:
					//if we are at the right side of the grid - do nothing
					if(xPosition + width == columns)
						return false;
					xPosition += 1;
					newIndexes = GetRectsIndexes();
					xPosition -= 1;
					break;
				case MoveDirection.Rotate:
					RotateAngle oldAngle = angle;
					ChangeRotateAngle();
					newIndexes = GetRectsIndexes();
					angle = oldAngle;
					//do not allow rotating if we are going to exceed the bounds of the grid
					if(yPosition + height > rows || xPosition + width > columns)
						return false;
					break;
			}

            //get the different indexes
			ArrayList different = GetDifferentIndexes(oldIndexes, newIndexes);

			foreach(int i in different)
			{
				//if we have at least one filled square in the different indexes, so we cannot
				//move the figure in the desired location
				if(parent.squares[i].filled)
					return false;
			}

            //no other conditions, so return true
			return true;
		}


		internal int xPosition;
		internal int yPosition;
		internal int columns;
		internal int rows;
		internal TetrisGrid parent;
		internal Color color;

		internal int width;
		internal int height;
		internal RotateAngle angle;
		internal RotateDirection direction;

	}
}
