using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;


namespace GATetrisControl
{
	[Designer(typeof(TetrisGridContainerDesigner))]
	public sealed class GATetris : UserControl
	{

		public GATetris()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);

			grid = new TetrisGrid();			
			grid.Parent = gridPanel;
			grid.Dock = DockStyle.Fill;

			grid.NewFigure += new EventHandler(UpdateScore);
			grid.UpdateNextFigure += new EventHandler(UpdateNextFigure);
			grid.GameStart += new EventHandler(NewGame);
			grid.GameEnd += new EventHandler(EndGame);
			grid.GamePaused += new EventHandler(PauseGame);

			int sc = grid.score;
			score.Text = sc.ToString();

			InitContextMenu();
		}


		public void ShowOptions()
		{
			if(grid.gameStarted)
				return;
			Options o = new Options(this);
			if(o.ShowDialog() == DialogResult.OK)
			{
				grid.settings = o.settings;
				grid.settings.ApplySettings();
			}
		}


		public void ShowAbout()
		{
			About a = new About();
			a.ShowDialog();
		}

		public void ShowBestPlayers(Player select)
		{
			//create the form and populate the listview
			BestPlayers players = new BestPlayers();
			GetBestPlayers().PopulateListView(players.players, select);
			players.ShowDialog();
		}


		Player AddToBestPlayers(BestPlayersCollection coll)
		{
			Player added = null;

			//create a player from the current score and lines
			Player current = new Player();
			current.score = grid.score;
			current.lines = grid.lines;

            //sort the collection
			coll.Sort();

			for(int i = coll.Count - 1; i >= 0; i--)
			{
				Player p = coll[i];
				//check if the current player has better results
				if(current.CompareTo(p) == 1)
				{
					EnterName en = new EnterName();
					en.ShowDialog();
					current.name = en.name.Text;

					//remove the first(lowest) player
					coll.RemoveAt(0);

					//add the current player
					coll.Add(current);

                    //sort the collection again
					coll.Sort();

					added = current;
					break;
				}
			}

			return added;
		}

		BestPlayersCollection GetBestPlayers()
		{
			//try to get an existing collection
			BestPlayersCollection coll = BestPlayersCollection.Restore("Top10.dat");
			if(coll == null)
			{
				//create default collection and add 10 players
				coll = new BestPlayersCollection();
			}

			return coll;
		}


		internal void UpdateSize()
		{
			grid.UpdateSize();
			gridPanel.Width = grid.Width+2;
			gridPanel.Height = grid.Height+2;
			controlPanel.Width = 190;
			preview.Height = grid.settings.squareWidth*6;
			preview.Width = grid.settings.squareWidth*6;
			paused.Location = new Point(8,preview.Bottom);			
			Width = gridPanel.Width + controlPanel.Width + 4;
			Height = gridPanel.Height;

			Refresh();
		}

		void InitContextMenu()
		{
			ContextMenu menu = new ContextMenu();

			MenuItem mi;
			EventHandler eh = new EventHandler(ContextMenuClicked);

			mi = new MenuItem("&New Game", eh, Shortcut.F2);
			menu.MenuItems.Add(mi);

			mi = new MenuItem("&End Game", eh, Shortcut.F3);
			menu.MenuItems.Add(mi);

			mi = new MenuItem("&Pause", eh);
			menu.MenuItems.Add(mi);

			mi = new MenuItem("-");
			menu.MenuItems.Add(mi);

			mi = new MenuItem("&Settings...", eh, Shortcut.CtrlO);
			menu.MenuItems.Add(mi);

			mi = new MenuItem("-");
			menu.MenuItems.Add(mi);

			mi = new MenuItem("&About...", eh);
			menu.MenuItems.Add(mi);

			mi = new MenuItem("&Hall Of Fame", eh);
			menu.MenuItems.Add(mi);

			ContextMenu = menu;
		}


		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
				if(!DesignMode)
				{
					if(grid.settings != null)
						grid.settings.Save();
				}
			}
			base.Dispose( disposing );
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(SystemPens.ControlDarkDark,0,0,Width-1,Height-1);
		}

		protected override void OnResize(EventArgs e)
		{
			Width = gridPanel.Width + controlPanel.Width + 4;
			Height = gridPanel.Height + 4;

			if(Parent != null)
				Parent.ClientSize = new Size(Width, Height);

			base.OnResize(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			if(!DesignMode)
			{
				grid.settings = Settings.Restore();
				if(grid.settings == null)
					grid.settings = new Settings();

				grid.settings.container = this;
				UpdateSize();
			}

			base.OnLoad (e);
		}


		void DrawPreview(Figure figure, Graphics g)
		{
			figure.DrawPreview(g);
		}


		void ContextMenuClicked(object sender, EventArgs e)
		{
			MenuItem mi = sender as MenuItem;
			switch(mi.Text)
			{
				case "&New Game":
					grid.InitNewGame();
					break;
				case "&End Game":
					grid.InitGameOver();
					break;
				case "&Settings...":
					ShowOptions();
					break;
				case "&About...":
					ShowAbout();
					break;
				case "&Hall Of Fame":
					ShowBestPlayers(null);
					break;
				case "&Pause":
					grid.OnPause();
					break;
			}
		}

		private void gridPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(SystemPens.ControlDark,0,0,gridPanel.Width-1,
				gridPanel.Height-1);
		}

		private void controlPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(SystemPens.ControlDark,0,0,controlPanel.Width-1,0);
			e.Graphics.DrawLine(SystemPens.ControlDark,controlPanel.Width-1,0,
				controlPanel.Width-1,controlPanel.Height-1);
			e.Graphics.DrawLine(SystemPens.ControlDark,0,controlPanel.Height-1,
				controlPanel.ClientRectangle.Width-1,controlPanel.Height-1);
		}

		private void gridPanel_Resize(object sender, System.EventArgs e)
		{
			gridPanel.Width = grid.Width+2;
			gridPanel.Height = grid.Height+2;
		}

		private void controlPanel_Resize(object sender, System.EventArgs e)
		{
			int width = Math.Max(6 * grid.settings.squareWidth + 18,130);
			controlPanel.Width = width;
		}

		private void preview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(!grid.gameOver && grid.gameStarted)
				DrawPreview(grid.GetNextFigure(), e.Graphics);
			else
				e.Graphics.Clear(controlPanel.BackColor);
		}

		void UpdateScore(object sender, EventArgs e)
		{
			int sc = grid.score;
			int lin = grid.lines;
			score.Text = sc.ToString();
			lines.Text = lin.ToString();
		}

		void UpdateNextFigure(object sender, EventArgs e)
		{
			preview.Invalidate();			
		}

		void NewGame(object sender, EventArgs e)
		{
			paused.Text = "Playing";
			score.Text = "0";
			lines.Text = "0";
			grid.Focus();
		}

		void EndGame(object sender, EventArgs e)
		{
			paused.Text = "";
			preview.Invalidate();
			score.Text = "0";
			lines.Text = "0";

            //check if we have to add this player to the hall of fame
			BestPlayersCollection coll = GetBestPlayers();
			Player select = AddToBestPlayers(coll);

			if(select != null)
			{
				//serialize and show top 10 players
				coll.Save("Top10.dat");
				ShowBestPlayers(select);
			}

			grid.lines = 0;
			grid.score = 0;
		}

		void PauseGame(object sender, EventArgs e)
		{
			if(grid.timer.Enabled)
			{
				paused.Text = "Playing";
			}
			else
			{
				paused.Text = "Paused";
			}
		}


        [Browsable(false)]
		public TetrisGrid TetrisGrid
		{
			get{return grid;}
		}



		#region Generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gridPanel = new System.Windows.Forms.Panel();
			this.controlPanel = new System.Windows.Forms.Panel();
			this.lines = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.paused = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.preview = new System.Windows.Forms.Label();
			this.sc = new System.Windows.Forms.Label();
			this.score = new System.Windows.Forms.Label();
			this.controlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridPanel
			// 
			this.gridPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.gridPanel.DockPadding.All = 1;
			this.gridPanel.Location = new System.Drawing.Point(2, 2);
			this.gridPanel.Name = "gridPanel";
			this.gridPanel.Size = new System.Drawing.Size(238, 468);
			this.gridPanel.TabIndex = 0;
			this.gridPanel.Resize += new System.EventHandler(this.gridPanel_Resize);
			this.gridPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gridPanel_Paint);
			// 
			// controlPanel
			// 
			this.controlPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
																					   this.lines,
																					   this.label1,
																					   this.paused,
																					   this.label2,
																					   this.preview,
																					   this.sc,
																					   this.score});
			this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlPanel.DockPadding.All = 1;
			this.controlPanel.Location = new System.Drawing.Point(240, 2);
			this.controlPanel.Name = "controlPanel";
			this.controlPanel.Size = new System.Drawing.Size(262, 468);
			this.controlPanel.TabIndex = 1;
			this.controlPanel.Resize += new System.EventHandler(this.controlPanel_Resize);
			this.controlPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.controlPanel_Paint);
			// 
			// lines
			// 
			this.lines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.lines.Location = new System.Drawing.Point(8, 56);
			this.lines.Name = "lines";
			this.lines.Size = new System.Drawing.Size(112, 16);
			this.lines.TabIndex = 7;
			this.lines.Text = "0";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Lines:";
			// 
			// paused
			// 
			this.paused.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.paused.Location = new System.Drawing.Point(8, 192);
			this.paused.Name = "paused";
			this.paused.Size = new System.Drawing.Size(64, 23);
			this.paused.TabIndex = 5;
			this.paused.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "Next Figure:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// preview
			// 
			this.preview.BackColor = System.Drawing.SystemColors.Control;
			this.preview.Location = new System.Drawing.Point(8, 96);
			this.preview.Name = "preview";
			this.preview.Size = new System.Drawing.Size(112, 96);
			this.preview.TabIndex = 2;
			this.preview.Paint += new System.Windows.Forms.PaintEventHandler(this.preview_Paint);
			// 
			// sc
			// 
			this.sc.BackColor = System.Drawing.SystemColors.Control;
			this.sc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.sc.Location = new System.Drawing.Point(8, 8);
			this.sc.Name = "sc";
			this.sc.Size = new System.Drawing.Size(48, 16);
			this.sc.TabIndex = 1;
			this.sc.Text = "Score:";
			this.sc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// score
			// 
			this.score.BackColor = System.Drawing.SystemColors.Control;
			this.score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.score.Location = new System.Drawing.Point(8, 24);
			this.score.Name = "score";
			this.score.Size = new System.Drawing.Size(112, 16);
			this.score.TabIndex = 0;
			this.score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TetrisGridContainer
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.controlPanel,
																		  this.gridPanel});
			this.DockPadding.All = 2;
			this.Name = "TetrisGridContainer";
			this.Size = new System.Drawing.Size(504, 472);
			this.controlPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private System.Windows.Forms.Panel gridPanel;
		private System.Windows.Forms.Panel controlPanel;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label score;
		private System.Windows.Forms.Label preview;
		private System.Windows.Forms.Label sc;		
		private System.Windows.Forms.Label paused;
		private System.Windows.Forms.Label label2;

		internal TetrisGrid grid;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lines;

	}
}
