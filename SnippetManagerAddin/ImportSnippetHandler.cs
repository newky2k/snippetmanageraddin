using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                try
                {
                    var result = dlg.SelectedFile;

                    if (!string.IsNullOrWhiteSpace(result) && File.Exists(result))
                    {
                        
                         CodeSnippets snippet;

                        using (var txtReader = new XmlTextReader(result))
                        {
                            var xmlSerializer = new XmlSerializer(typeof(CodeSnippets));
                            snippet = xmlSerializer.Deserialize(txtReader) as CodeSnippets;

                            var title = snippet.CodeSnippet;
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

                        
                        var templates = CodeTemplateService.GetCodeTemplates("text/x-csharp");

                        var exist = templates.FirstOrDefault(x => x.Shortcut.Equals(vs4mCodeTemplae.Shortcut));

                        if (exist != null)
                        {
                            return;
                        }

                        CodeTemplateService.SaveTemplate(vs4mCodeTemplae);

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                
            }



		}

		protected override void Update(CommandInfo info)
		{

		}
	}
}
