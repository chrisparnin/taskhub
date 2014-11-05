using System;
using System.Diagnostics;
using System.Reflection;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing.Design;

namespace GATetrisControl
{
	[Serializable,DefaultProperty("Columns")]
	internal class Settings : ICloneable
	{

		public Settings()
		{
			InitDefault();
		}



		#region ICloneable Members

		public object Clone()
		{
			//get the type of this instance
			Type type = GetType();

			//create binding flags object
			BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic |
									BindingFlags.Public;
			
			//get all the fields of this instance
			FieldInfo[] fields = type.GetFields(flags);

			//create new settings object
			Settings sett = new Settings();

			//now copy all the fields to the new object
			foreach(FieldInfo fi in fields)
			{
				fi.SetValue(sett, fi.GetValue(this));
			}

			return sett;
		}

		#endregion


		public void ApplySettings()
		{
			Debug.Assert(container != null, "Should have a valid reference to a GATetris object");
			if(needResize)
			{
				container.UpdateSize();
				needResize = false;
			}
		}

		public void InitDefault()
		{
			leftTColor = 
				lineColor = 
				lThunderColor = 
				rightTColor = 
				rThunderColor = 
				squareColor = 
				triangleColor = SystemColors.ControlDark;

			lightBorder = SystemColors.Control;
			darkBorder = SystemColors.ControlDarkDark;
			tetrisBackground = SystemColors.Window;

            leftKey = Keys.Left;
			rightKey = Keys.Right;
			downKey = Keys.Down;
			rotateKey = Keys.Up;
			pauseKey = Keys.P;

			squareWidth = 20;
			columns = 10;
			rows = 20;

			rotateDirection = RotateDirection.ClockWise;

			needResize = true;
		}


		public static Settings Restore()
		{
			return Serializer.GetFromFile("GATetris.dat") as GATetrisControl.Settings;
		}

		public void Save()
		{
			Serializer.SaveToFile(this, "GATetris.dat");
		}





        [Category("Figure Colors")]
        [Description("The LeftT figure back color")]
		public Color LeftTBackColor
		{
			get
			{
				return leftTColor;
			}
			set
			{
				leftTColor = value;
			}
		}
		protected bool ShouldSerializeLeftTBackColor()
		{
			return leftTColor != SystemColors.ControlDark;
		}

		[Category("Figure Colors")]
		[Description("The RightT figure back color")]
		public Color RightTBackColor
		{
			get
			{
				return rightTColor;
			}
			set
			{
				rightTColor = value;
			}
		}
		protected bool ShouldSerializeRightTBackColor()
		{
			return rightTColor != SystemColors.ControlDark;
		}

		[Category("Figure Colors")]
		[Description("The LThunder figure back color")]
		public Color LThunderBackColor
		{
			get
			{
				return lThunderColor;
			}
			set
			{
				lThunderColor = value;
			}
		}
		protected bool ShouldSerializeLThunderBackColor()
		{
			return lThunderColor != SystemColors.ControlDark;
		}

		[Category("Figure Colors")]
		[Description("The RThunder figure back color")]
		public Color RThunderBackColor
		{
			get
			{
				return rThunderColor;
			}
			set
			{
				rThunderColor = value;
			}
		}
		protected bool ShouldSerializeRThunderBackColor()
		{
			return rThunderColor != SystemColors.ControlDark;
		}

		[Category("Figure Colors")]
		[Description("The Square figure back color")]
		public Color SquareBackColor
		{
			get
			{
				return squareColor;
			}
			set
			{
				squareColor = value;
			}
		}
		protected bool ShouldSerializeSquareBackColor()
		{
			return squareColor != SystemColors.ControlDark;
		}

		[Category("Figure Colors")]
		[Description("The Line figure back color")]
		public Color LineBackColor
		{
			get
			{
				return lineColor;
			}
			set
			{
				lineColor = value;
			}
		}
		protected bool ShouldSerializeLineBackColor()
		{
			return lineColor != SystemColors.ControlDark;
		}

		[Category("Figure Colors")]
		[Description("The Triangle figure back color")]
		public Color TriangleBackColor
		{
			get
			{
				return triangleColor;
			}
			set
			{
				triangleColor = value;
			}
		}
		protected bool ShouldSerializeTriangleBackColor()
		{
			return triangleColor != SystemColors.ControlDark;
		}


		[Category("Tetris Colors")]
		[Description("Light border of a single square")]
		public Color LightBorder
		{
			get
			{
				return lightBorder;
			}
			set
			{
				lightBorder = value;
			}
		}
		protected bool ShouldSerializeLightBorder()
		{
			return lightBorder != SystemColors.Control;
		}

		[Category("Tetris Colors")]
		[Description("Dark border of a single square")]
		public Color DarkBorder
		{
			get
			{
				return darkBorder;
			}
			set
			{
				darkBorder = value;
			}
		}
		protected bool ShouldSerializeDarkBorder()
		{
			return darkBorder != SystemColors.ControlDarkDark;
		}

		[Category("Tetris Colors")]
		[Description("Tetris grid background")]
		public Color TetrisBackground
		{
			get
			{
				return tetrisBackground;
			}
			set
			{
				tetrisBackground = value;
			}
		}
		protected bool ShouldSerializeTetrisBackground()
		{
			return tetrisBackground != SystemColors.Window;
		}



		[Category("Keyboard")]
		[Description("The key value for moving figures left")]
		public Keys LeftKey
		{
			get
			{
				return leftKey;
			}
			set
			{
				leftKey = value;
			}
		}

        protected bool ShouldSerializeLeftKey()
		{
			return leftKey != Keys.Left;
		}
        
        [Category("Keyboard")]
		[Description("The key value for moving figures right")]
		public Keys RightKey
		{
			get
			{
				return rightKey;
			}
			set
			{
				rightKey = value;
			}
		}
		protected bool ShouldSerializeRightKey()
		{
			return rightKey != Keys.Right;
		}


		[Category("Keyboard")]
		[Description("The key value for moving figures down")]
		public Keys DownKey
		{
			get
			{
				return downKey;
			}
			set
			{
				downKey = value;
			}
		}
		protected bool ShouldSerializeDownKey()
		{
			return downKey != Keys.Down;
		}


		[Category("Keyboard")]
		[Description("The key value for rotating figures")]
		public Keys RotateKey
		{
			get
			{
				return rotateKey;
			}
			set
			{
				rotateKey = value;
			}
		}
		protected bool ShouldSerializeRotateKey()
		{
			return rotateKey != Keys.Up;
		}

		[Category("Keyboard")]
		[Description("The key value for pausing game")]
		public Keys PauseKey
		{
			get
			{
				return pauseKey;
			}
			set
			{
				pauseKey = value;
			}
		}
		protected bool ShouldSerializePauseKey()
		{
			return pauseKey != Keys.P;
		}

		


		[Category("Dimensions")]
		[Description("The width(height) of a single square in the tetris grid. Varies from 10 to 30")]
		[DefaultValue(20)]
		public int SingleSquareWidth
		{
			get
			{
				return squareWidth;
			}
			set
			{
				if(value > 30 || value < 10)
					return;
				if(squareWidth == value)
					return;
				squareWidth = value;
				needResize = true;
			}
		}

		[Category("Dimensions")]
		[Description("The number of columns in the tetris grid. Varies from 10 to 20")]
		[DefaultValue(10)]
		public int Columns
		{
			get
			{
				return columns;
			}
			set
			{
				if(value > 20 || value < 10)
					return;
				if(columns == value)
					return;
				columns = value;
				needResize = true;
			}
		}

		[Category("Dimensions")]
		[Description("The number of rows in the tetris grid. Varies from 20 to 30")]
		[DefaultValue(20)]
		public int Rows
		{
			get
			{
				return rows;
			}
			set
			{
				if(value > 30 || value < 20)
					return;
				if(rows == value)
					return;
				rows = value;
				needResize = true;
			}
		}

		

		[Description("The rotate direction."), DefaultValue(RotateDirection.ClockWise)]		
		public RotateDirection RotateDirection
		{
			get
			{
				return rotateDirection;
			}
			set
			{
				rotateDirection = value;
			}
		}


		[NonSerialized] internal GATetris container;
		[NonSerialized] internal bool needResize;

        //figure colors
		internal Color leftTColor;
		internal Color lineColor;
		internal Color lThunderColor;
		internal Color rightTColor;
		internal Color rThunderColor;
		internal Color squareColor;
		internal Color triangleColor;

		//rotate direction
		internal RotateDirection rotateDirection;

		//tetris colors
		internal Color lightBorder;
		internal Color darkBorder;
		internal Color tetrisBackground;

        //keys
		internal Keys leftKey;
		internal Keys rightKey;
		internal Keys downKey;
		internal Keys rotateKey;
		internal Keys pauseKey;

        //metrics
		internal int squareWidth;
		internal int columns;
		internal int rows;

	}
}
