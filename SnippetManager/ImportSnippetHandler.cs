using System;
using Mono.Addins;
using MonoDevelop.Components.Commands;

namespace SnippetManager
{
	public class ImportSnippetHandler : CommandHandler
	{
		protected override void Run()
		{
			

			var fDlg = new FileDialog();


			var result = fDlg.ShowDialog();


			if (string.IsNullOrWhiteSpace(result))
			{
				
			}

		}

		protected override void Update(CommandInfo info)
		{

		}
	}
}
