using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GATetrisControl
{
	[ToolboxItem(false)]
	public class EnterName : System.Windows.Forms.Form
	{

		public EnterName()
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

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			name.SelectAll();
		}



		private void ok_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}


		#region Generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.name = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.ok = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// name
			// 
			this.name.Location = new System.Drawing.Point(8, 56);
			this.name.Name = "name";
			this.name.Size = new System.Drawing.Size(272, 20);
			this.name.TabIndex = 0;
			this.name.Text = "Anonymous";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Congratulations! You have entered the Hall of fame!";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Enter your name here:";
			// 
			// ok
			// 
			this.ok.Location = new System.Drawing.Point(208, 88);
			this.ok.Name = "ok";
			this.ok.TabIndex = 3;
			this.ok.Text = "&Ok";
			this.ok.Click += new System.EventHandler(this.ok_Click);
			// 
			// EnterName
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 119);
			this.Controls.Add(this.ok);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.name);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EnterName";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "EnterName";
			this.ResumeLayout(false);

		}
		#endregion


		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox name;
		private System.Windows.Forms.Button ok;
		private System.ComponentModel.Container components = null;

	}
}
