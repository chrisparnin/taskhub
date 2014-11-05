using System;

namespace GATetrisControl
{
	[Serializable]
	public class Player : IComparable
	{

		public Player()
		{
			name = "Default player";
			score = 0;
			lines = 0;
		}


		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;
			if(obj.GetType() != typeof(Player))
				return false;
			Player p = obj as Player;
			if(p.score == score && p.lines == lines && p.name == name)
				return true;
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}

		public override string ToString()
		{
			return "Player "+name+"; score: "+score.ToString();
		}




		public int CompareTo(object x)
		{
			if(x.GetType() != typeof(Player))
				return -1;
			Player p = x as Player;
			if(p.score >= score)
			{
				if(p.score == score)
				{
					if(p.lines <= lines)
					{
						if(p.lines == lines)
							return 0;
						else
							return -1;
					}
				}
				else
					return -1;
			}
			else
				return 1;
			return 0;
		}


		internal string name;
		internal int score;
		internal int lines;

	}
}
