using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Mono.Addins;
using MonoDevelop.Components;
using MonoDevelop.Components.Commands;
using MonoDevelop.Components.Extensions;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.CodeTemplates;

namespace SnippetManagerAddin
{
	public class ImportSnippetHandler : CommandHandler
	{
		protected override void Run()
		{

            var dlg = new FileDialog();

            var result = dlg.ShowDialog();

            if (!string.IsNullOrWhiteSpace(result))
            {
                
                try
                {

                    if (!string.IsNullOrWhiteSpace(result) && File.Exists(result))
                    {

                        CodeSnippets snippet;

                        using (var txtReader = new XmlTextReader(result))
                        {
                            var xmlSerializer = new XmlSerializer(typeof(CodeSnippets));
                            snippet = xmlSerializer.Deserialize(txtReader) as CodeSnippets;

                        }

                        var vs4mCodeTemplae = new CodeTemplate()
                        {
                            Shortcut = snippet.CodeSnippet.Header.Shortcut,
                            Description = snippet.CodeSnippet.Header.Description,
                            Code = snippet.CodeSnippet.Snippet.Code.Text,
                            CodeTemplateType = CodeTemplateType.Unknown,
                        };

                        if (snippet.CodeSnippet.Snippet.Code.Language.Equals("csharp"))
                        {
                            vs4mCodeTemplae.Group = "C#";
                            vs4mCodeTemplae.MimeType = "text/x-csharp";
                        }

                        var templates = CodeTemplateService.GetCodeTemplates(vs4mCodeTemplae.MimeType);

                        var exist = templates.FirstOrDefault(x => x.Shortcut.Equals(vs4mCodeTemplae.Shortcut));

                        if (exist != null)
                        {
                            var confirm = MessageService.Confirm("Snippet Exists", $"A code snippet already exists with the shortcut {vs4mCodeTemplae.Shortcut}. Do you want to overwrite it?", AlertButton.Yes);

                            if (!confirm)
                                return;
                            else
                                CodeTemplateService.DeleteTemplate(exist);
                        }

                        foreach (var aDeclaration in snippet.CodeSnippet.Snippet.Declarations.Literal)
                        {
                            vs4mCodeTemplae.AddVariable(new CodeTemplateVariable()
                            {
                                Name = aDeclaration.ID,
                                ToolTip = aDeclaration.ToolTip,
                                Function = aDeclaration.Function,
                                Default = aDeclaration.Default,
                            });
                        }

                        var outPutPath = Path.GetDirectoryName(result);
                        var outPutFile = Path.Combine(outPutPath, $"{vs4mCodeTemplae.Shortcut}.template.xml");


                        if (File.Exists(outPutFile))
                            File.Delete(outPutFile);

                        CodeTemplateService.SaveTemplate(vs4mCodeTemplae);

                        MessageService.ShowMessage($"Import complete. {Environment.NewLine} You may need to restart Visual Studio to activate the snippet");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError("Import Error Occured", ex);
                }
            }
		}

		protected override void Update(CommandInfo info)
		{

		}
	}
}
