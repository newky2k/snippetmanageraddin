using System;
using MonoDevelop.Components;

namespace SnippetManagerAddin
{
	public class FileDialog : FileEntry
	{

		public string ShowDialog()
		{
			var result = ShowBrowseDialog("", null);

			return result;
		}
	}
}
