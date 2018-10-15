using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WordSearchConsole;

namespace WordSearchTest
{
	[TestClass]
	public class WordSearchTest
	{
		[TestMethod]
		public void Test()
		{
			WordSearch wordSearch = new WordSearch();

			Assert.AreEqual(1, wordSearch.ReturnInt(1));
		}
	}
}
