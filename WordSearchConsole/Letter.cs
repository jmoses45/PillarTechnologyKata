using System;
using System.Drawing;

namespace WordSearch
{
	public class Letter
	{ 
		private string mLetter;
		private Point mPosition;

		public string letter { get { return mLetter; } set { mLetter = value; } }
		public Point position { get { return mPosition; } set { mPosition = value; } }

		public Letter()
		{

		}

		public Letter(string letter, Point position)
		{
			this.letter = letter;
			this.position = position;
		}

		public override bool Equals(object obj)
		{
			bool result = false;

			Letter neighborLetter = obj as Letter;

			if ((neighborLetter != null) &&
				this.letter.Equals(neighborLetter.letter) &&
				this.position.Equals(neighborLetter.position))
			{
				result = true;
			}

			return result;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
