using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace GATetrisControl
{
	[ToolboxItem(false), EditorBrowsable(EditorBrowsableState.Never)]
	public class TetrisGrid : Control
	{

		//Occurs when new figure is about to fall and I use it for updating the score
		public event EventHandler NewFigure;
		//This is for drawing the preview from the TetrisGridContainer class
		public event EventHandler UpdateNextFigure;
		//occurs when the game starts
		public event EventHandler GameStart;
		//occurs when the game ends
		public event EventHandler GameEnd;
		//occurs when the game is paused
		public event EventHandler GamePaused;


		public TetrisGrid()
		{
			ResizeRedraw = true;
			BackColor = SystemColors.Window;

			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);

			score = 0;
			lines = 0;
			nextFig = 0;

			gameOver = false;
			gameStarted = false;
			newFigure = false;

            invalidRects = new ArrayList();

			timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += new EventHandler(TimerTick);
		}


		void TimerTick(object sender, EventArgs e)
		{
			currentFigure.Move(MoveDirection.Down);
			//if we need new figure
			if(newFigure)
			{
				int linesNum = 0;
				bool refresh = false;
				//first check for any completed lines
				for(int i=0; i < settings.rows; i++)
				{
					if(GetLine(i))
					{
						//clear the line
						ClearLine(i);
						linesNum++;
						lines++;
						//scroll the line
						ScrollLine(i);
						//set the refresh flag to true
						refresh = true;
					}
				}
				if(refresh)
					SmartRefresh();
				//adjust score
				if(linesNum==0)
					score += 0;
				if(linesNum==1)
					score += 100;
				if(linesNum==2)
					score += 300;
				if(linesNum==3)
					score += 600;
				if(linesNum==4)
					score += 1000;

				//fire the NewFigure event
				if(NewFigure != null)
					NewFigure(this, new EventArgs());

				InitFigure();
				InitNextFigure();
			}
		}



		protected override void Dispose( bool disposing )
		{
			timer.Stop();			
			base.Dispose(disposing);
		}

		protected override void OnResize(EventArgs e)
		{
			UpdateSize();
			base.OnResize (e);
		}


		protected override void OnPaint(PaintEventArgs e)
		{
			SolidBrush brush = new SolidBrush(BackColor);
			foreach(SingleSquare sq in squares)
			{
				//do not paint if not within the invalid rect
				if(!e.Graphics.ClipBounds.IntersectsWith(sq.rect))
					continue;
				//if filled - call the single square to draw itself on this graphics
				if(sq.filled)
					sq.Draw(e.Graphics);
				//otherwise just clear the square - fill it with its parent's back color
				else
					e.Graphics.FillRectangle(brush, sq.rect);
			}
			brush.Dispose();
		}

		protected override bool ProcessDialogKey(Keys key)
		{
			if(key == settings.pauseKey)
				return OnPause();

			if(!gameStarted || !timer.Enabled)
				return base.ProcessDialogChar((char)(int)key);

			bool processed = false;
			MoveDirection dir = new MoveDirection();

			if(key == settings.rotateKey)
			{
				dir = MoveDirection.Rotate;
				processed = true;
			}
			else if(key == settings.rightKey)
			{
				dir = MoveDirection.Right;
				processed = true;
			}
			else if(key == settings.leftKey)
			{
				dir = MoveDirection.Left;
				processed = true;
			}
			else if(key == settings.downKey)
			{
				dir = MoveDirection.Down;
				processed = true;
			}

			if(processed)
			{
				currentFigure.Move(dir);
				return true;
			}
			
			return base.ProcessDialogChar((char)(int)key);
		}



		internal Figure GetNextFigure()
		{
			switch(nextFig)
			{
				case 0:
					return new Line(this);
				case 1:
					return new Triangle(this);
				case 2:
					return new LThunder(this);
				case 3:
					return new RThunder(this);
				case 4:
					return new RightT(this);
				case 5:
					return new LeftT(this);
				case 6:
					return new LeftT(this);
				default:
					return null;
			}
		}
		void InitRectangles()
		{
			squares = new SingleSquare[settings.columns * settings.rows];

			int counter = 0;
			for(int i = 0; i < settings.rows * settings.squareWidth; i += settings.squareWidth)
			{
				for(int j = 0; j < settings.columns * settings.squareWidth; j += settings.squareWidth)
				{
					squares[counter] = new SingleSquare(this);
					squares[counter].rect = new Rectangle(j,i,settings.squareWidth,settings.squareWidth);
					counter++;
				}
			}
		}

		void InitFigure()
		{
			if(DesignMode)
				return;
			switch(nextFig)
			{
				case 0:
					currentFigure = new Line(this);
					break;
				case 1:
					currentFigure = new Triangle(this);
					break;
				case 2:
					currentFigure = new LThunder(this);
					break;
				case 3:
					currentFigure = new RThunder(this);
					break;
				case 4:
					currentFigure = new RightT(this);						
					break;
				case 5:
					currentFigure = new LeftT(this);
					break;
			}

			if(!currentFigure.CanDraw())
			{
				gameOver = true;
				InitGameOver();
				return;
			}

            currentFigure.direction = settings.rotateDirection;
			currentFigure.DrawFigure();
			SmartRefresh();
			newFigure = false;
		}

		void InitNextFigure()
		{
			Random r = new Random();
			nextFig = (byte)r.Next(0,6);
			if(UpdateNextFigure != null)
				UpdateNextFigure(this, new EventArgs());
		}

		public void InitNewGame()
		{
			Refresh();

			gameOver = false;
			gameStarted = true;			
			score = 0;
			lines = 0;

			InitRectangles();
			InitNextFigure();
			InitFigure();

			timer.Start();
			if(GameStart != null)
				GameStart(this, new EventArgs());
		}

		public void InitGameOver()
		{
			timer.Stop();			
			if(gameOver)
			{
				DialogResult dr = MessageBox.Show("The game is over!\nWould you like to play another game?",
					"GATetris",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
				switch(dr)
				{
					case DialogResult.Yes:
						if(this.GameEnd != null)
							GameEnd(this, new EventArgs());
						InitNewGame();
						return;
					case DialogResult.No:
						break;
				}
			}

			InitRectangles();
			Invalidate();
			invalidRects.Clear();

			gameStarted = false;
			if(GameEnd != null)
				GameEnd(this, new EventArgs());
		}


		internal bool OnPause()
		{
			if(!gameStarted)
				return false;
			if(timer.Enabled)
			{
				timer.Stop();				
			}
			else
			{
				timer.Start();				
			}
			if(GamePaused != null)
				GamePaused(this, new EventArgs());

			return true;
		}


		internal void UpdateSize()
		{
			if(settings != null)
			{
				Width = settings.columns * settings.squareWidth;
				Height = settings.rows * settings.squareWidth;
				InitRectangles();
			}
		}

		bool GetLine(int y)
		{
			if( y < 0 || y > settings.rows - 1)
				return false;
			bool flag = squares[y * settings.columns].filled;
			if(!flag)
				return false;
			for(int i = y * settings.columns + 1; i < y * settings.columns + settings.columns; i++)
			{
				if(!squares[i].filled)
					return false;
			}
			return true;
		}

		void ClearLine(int y)
		{
			if(y < 0 || y > settings.rows-1)
				return;
			for(int i=y * settings.columns; i < y * settings.columns + settings.columns; i++)
			{
                invalidRects.Add(squares[i]);
				squares[i].filled = false;
			}
		}

		void ScrollLine(int y)
		{
			for(int i = y * settings.columns + settings.columns - 1; i >= 0; i--)
			{
				if(squares[i].filled)
				{
					invalidRects.Add(squares[i]);
					squares[i].filled = false;
					squares[i + settings.columns].filled = true;
				}
			}
		}

		internal void SmartRefresh()
		{
			Rectangle r = new Rectangle();
			int counter = 0;

			//create a rectangle object that is union of all the invalid rects
			foreach(SingleSquare sq in invalidRects)
			{
				if(counter == 0)
					r = sq.rect;
				else
					r = Rectangle.Union(r, sq.rect);
				counter++;
			}
			//invalidate this rectangle
			Invalidate(r);
			//clear the invalid rects array
			invalidRects.Clear();
		}



        internal Settings settings;
		internal SingleSquare[] squares;
		internal Figure currentFigure;
		internal bool newFigure;
		internal ArrayList invalidRects;
		internal Timer timer;
		internal byte nextFig;
		internal bool gameOver;
		internal bool gameStarted;
		internal int score;
		internal int lines;

	}
}
