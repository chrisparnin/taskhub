using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GATetrisControl
{
	[ToolboxItem(false)]
	public class BestPlayers : System.Windows.Forms.Form
	{

		public BestPlayers()
		{
			InitializeComponent();
		}


		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		private void ok_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void reset_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Are you sure you want to reset the top 10 players?",
				"GATetris", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			if(dr != DialogResult.Yes)
				return;

			BestPlayersCollection coll = new BestPlayersCollection();

			//populate the list view with the new values
			players.Items.Clear();
			coll.PopulateListView(players, null);

			//serialize the new collection
			coll.Save("Top10.dat");
		}

		#region Generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.players = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.ok = new System.Windows.Forms.Button();
			this.reset = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// players
			// 
			this.players.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader1,
																					  this.columnHeader2,
																					  this.columnHeader3});
			this.players.FullRowSelect = true;
			this.players.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.players.HideSelection = false;
			this.players.Location = new System.Drawing.Point(8, 8);
			this.players.MultiSelect = false;
			this.players.Name = "players";
			this.players.Size = new System.Drawing.Size(280, 168);
			this.players.TabIndex = 0;
			this.players.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Score";
			this.columnHeader2.Width = 87;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Lines";
			this.columnHeader3.Width = 88;
			// 
			// ok
			// 
			this.ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ok.Location = new System.Drawing.Point(216, 184);
			this.ok.Name = "ok";
			this.ok.TabIndex = 0;
			this.ok.Text = "&Ok";
			this.ok.Click += new System.EventHandler(this.ok_Click);
			// 
			// reset
			// 
			this.reset.Location = new System.Drawing.Point(136, 184);
			this.reset.Name = "reset";
			this.reset.TabIndex = 1;
			this.reset.Text = "&Reset";
			this.reset.Click += new System.EventHandler(this.reset_Click);
			// 
			// BestPlayers
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.ok;
			this.ClientSize = new System.Drawing.Size(296, 215);
			this.Controls.Add(this.reset);
			this.Controls.Add(this.ok);
			this.Controls.Add(this.players);
			this.DockPadding.All = 5;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BestPlayers";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GATetris - BestPlayers";
			this.ResumeLayout(false);

		}
		#endregion


		internal System.Windows.Forms.ListView players;
		private System.Windows.Forms.Button ok;

		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button reset;
		private System.ComponentModel.Container components = null;

	}
}
