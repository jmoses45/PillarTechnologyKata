using System;
using System.Collections.Generic;
using System.Drawing;

namespace WordSearch
{
	public class WordSearchMain
	{
		static WordSearchConsole wordSearchConsole;

		[STAThread]
		static void Main(string[] args)
		{
			wordSearchConsole = new WordSearchConsole();
		}
	}
}
