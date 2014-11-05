using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GATetrisControl
{
	/// <summary>
	/// Summary description for About.
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private FlatControls.FButton fButton1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel mail;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public About()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			mail.Links.Add(0,mail.Text.Length,"mailto:gogouata@yahoo.com");
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(About));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.fButton1 = new FlatControls.FButton();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.mail = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(2, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(246, 54);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "GATetris version 1.0.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// fButton1
			// 
			this.fButton1.BackColor = System.Drawing.SystemColors.Window;
			this.fButton1.Location = new System.Drawing.Point(168, 218);
			this.fButton1.Name = "fButton1";
			this.fButton1.TabIndex = 0;
			this.fButton1.Text = "&Ok";
			this.fButton1.Click += new System.EventHandler(this.fButton1_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 77);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(232, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Written entirely in C#.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 90);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(232, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = ".NET Framework version 1.0 used.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 121);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(232, 40);
			this.label4.TabIndex = 4;
			this.label4.Text = "This is a freeware product, but if you want to use the tetris control itself in a" +
				"nother program you must inform me!";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mail
			// 
			this.mail.Cursor = System.Windows.Forms.Cursors.Hand;
			this.mail.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.mail.LinkColor = System.Drawing.Color.Navy;
			this.mail.Location = new System.Drawing.Point(64, 184);
			this.mail.Name = "mail";
			this.mail.Size = new System.Drawing.Size(120, 16);
			this.mail.TabIndex = 6;
			this.mail.TabStop = true;
			this.mail.Text = "gogouata@yahoo.com";
			this.mail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.mail.VisitedLinkColor = System.Drawing.Color.Brown;
			this.mail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mail_LinkClicked);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(96, 168);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 5;
			this.label5.Text = "E-mail:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// About
			// 
			this.AcceptButton = this.fButton1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(250, 248);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label5,
																		  this.mail,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.fButton1,
																		  this.label1,
																		  this.pictureBox1});
			this.DockPadding.All = 2;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About GATetris";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.About_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		private void About_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(SystemPens.ControlDarkDark,0,0,e.ClipRectangle.Width-1,
				e.ClipRectangle.Height-1);
			e.Graphics.DrawRectangle(SystemPens.ControlDarkDark,1,1,e.ClipRectangle.Width-3,
				e.ClipRectangle.Height-3);
		}

		private void fButton1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(SystemPens.ControlDarkDark,e.ClipRectangle.Left,
				e.ClipRectangle.Bottom-1,e.ClipRectangle.Right, e.ClipRectangle.Bottom-1);
		}

		private void mail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			mail.Links[mail.Links.IndexOf(e.Link)].Visited = true;
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}
	}
}
