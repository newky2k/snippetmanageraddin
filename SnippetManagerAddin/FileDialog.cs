using System;
using MonoDevelop.Components;
using MonoDevelop.Components.Extensions;

namespace SnippetManagerAddin
{
	public class FileDialog : FileEntry
	{

		public string ShowDialog()
		{
            this.Action = FileChooserAction.FileFlags;

            this.FileFilters.AddFilter(new SelectFileDialogFilter("Snipter"));

            var result = ShowDialog();

			return result;
		}
	}
}
