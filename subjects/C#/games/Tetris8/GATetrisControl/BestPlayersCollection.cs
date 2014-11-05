using System;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Diagnostics;

namespace GATetrisControl
{
	[Serializable]
	public class BestPlayersCollection : CollectionBase
	{

		public BestPlayersCollection()
		{
			//init 10 default players
			Player p;
			for(int i = 0; i < 10; i++)
			{
				p = new Player();
				p.score = i*100+100;
				p.lines = i+1;
				Add(p);
			}
		}


		public Player Add(Player value)
		{
			InnerList.Add(value as object);			
			return value;
		}

		public void AddRange(Player[] values)
		{
			foreach(Player ip in values)
				Add(ip);
		}

		public void Remove(Player value)
		{
			InnerList.Remove(value as object);			
		}

		public void Insert(int index, Player value)
		{
			InnerList.Insert(index, value as object);			
		}

		public bool Contains(Player value)
		{
			return InnerList.Contains(value as object);
		}

		public Player this[int index]
		{
			get 
			{ 
				return (InnerList[index] as Player);
			}
		}

		public int IndexOf(Player value)
		{
			return InnerList.IndexOf(value);
		}


		public static BestPlayersCollection Restore(string fileName)
		{
			return Serializer.GetFromFile(fileName) as BestPlayersCollection;
		}

		public void Save(string fileName)
		{
			Serializer.SaveToFile(this, fileName);
		}


		public void Sort()
		{
			InnerList.Sort();
		}


		public void PopulateListView(ListView view, Player select)
		{
			Player p;

			for(int i = Count - 1; i >= 0; i--)
			{
				p = this[i];

				ListViewItem lvi = new ListViewItem(p.name);
				lvi.SubItems.Add(p.score.ToString());
				lvi.SubItems.Add(p.lines.ToString());

				if(p.Equals(select))
					lvi.Selected = true;

				view.Items.Add(lvi);
			}
		}

	}
}
