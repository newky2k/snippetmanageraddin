using System;
using MonoDevelop.Components;
using MonoDevelop.Components.Extensions;
using MonoDevelop.Core;

namespace SnippetManagerAddin
{
	public class FileDialog : FileEntry
	{

		public string ShowDialog()
		{
            this.Action = FileChooserAction.Open;

            this.FileFilters.AddFilter(new SelectFileDialogFilter(
                GettextCatalog.GetString("Snippet Files"),
                new string[] { "*.snippet" }));

            var result = base.ShowBrowseDialog("", null);

			return result;
		}
	}
}
