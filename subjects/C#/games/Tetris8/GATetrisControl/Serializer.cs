using System;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace GATetrisControl
{
	public class Serializer
	{
		public static object GetFromFile(string fileName)
		{
			string dir = Application.UserAppDataPath;
			string path = dir + @"\"+fileName;

			if(!File.Exists(path))
				return null;

			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);

			try
			{
				object o = bf.Deserialize(fs);
				fs.Close();
				return o;
			}
			catch(Exception ex)
			{
				fs.Close();
				MessageBox.Show("Could not deserialize object due to:\n"+ex.Message);
				return null;
			}
		}

		public static bool SaveToFile(object o, string fileName)
		{
			string dir = Application.UserAppDataPath;
			string path = dir + @"\"+fileName;

			FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
			BinaryFormatter bf = new BinaryFormatter();

			try
			{
				bf.Serialize(fs, o);
				fs.Close();
				return true;
			}
			catch(Exception ex)
			{
				fs.Close();
				MessageBox.Show("Could not serialize object due to:\n"+ex.Message);
				return false;
			}
		}
	}
}
