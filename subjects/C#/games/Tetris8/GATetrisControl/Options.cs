using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GATetrisControl
{
	[ToolboxItem(false)]
	public class Options : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		internal System.Windows.Forms.PropertyGrid properties;

		internal Settings settings;
		private System.Windows.Forms.Button defaultButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Options(GATetris tetris)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			settings = (Settings)tetris.grid.settings.Clone();
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

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			properties.SelectedObject = settings;
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.properties = new System.Windows.Forms.PropertyGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.defaultButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// properties
			// 
			this.properties.CommandsVisibleIfAvailable = true;
			this.properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.properties.LargeButtons = false;
			this.properties.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.properties.Location = new System.Drawing.Point(0, 0);
			this.properties.Name = "properties";
			this.properties.Size = new System.Drawing.Size(328, 325);
			this.properties.TabIndex = 0;
			this.properties.ViewBackColor = System.Drawing.SystemColors.Window;
			this.properties.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.defaultButton);
			this.panel1.Controls.Add(this.okButton);
			this.panel1.Controls.Add(this.cancelButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 325);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(328, 40);
			this.panel1.TabIndex = 1;
			// 
			// defaultButton
			// 
			this.defaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.defaultButton.Location = new System.Drawing.Point(168, 8);
			this.defaultButton.Name = "defaultButton";
			this.defaultButton.TabIndex = 2;
			this.defaultButton.Text = "&Default";
			this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(88, 8);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 1;
			this.okButton.Text = "&Ok";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(248, 8);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 0;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// Options
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(328, 365);
			this.Controls.Add(this.properties);
			this.Controls.Add(this.panel1);
			this.Name = "Options";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void defaultButton_Click(object sender, System.EventArgs e)
		{
			settings.InitDefault();
			properties.Refresh();
		}
	}
}
