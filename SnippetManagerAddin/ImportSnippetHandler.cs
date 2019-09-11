using System;
using Mono.Addins;
using MonoDevelop.Components;
using MonoDevelop.Components.Commands;
using MonoDevelop.Components.Extensions;
using MonoDevelop.Core;
using MonoDevelop.Ide;

namespace SnippetManagerAddin
{
	public class ImportSnippetHandler : CommandHandler
	{
		protected override void Run()
		{

            var dlg = new SelectFileDialog(GettextCatalog.GetString("Select XSLT Stylesheet"))
            {
                TransientFor = IdeApp.Workbench.RootWindow,
            };

            dlg.AddFilter(new SelectFileDialogFilter(
                GettextCatalog.GetString("Snippet Files"),
                new string[] { "*.snippet" },
                new string[] { "text/xml", "application/xml" }
            ));

            dlg.AddAllFilesFilter();

            if (dlg.Run())
            {
                var result = dlg.SelectedFile;


                if (string.IsNullOrWhiteSpace(result))
                {
                    var codeTemp = new MonoDevelop.Ide.CodeTemplates.CodeTemplate();


                }
            }



		}

		protected override void Update(CommandInfo info)
		{

		}
	}
}
