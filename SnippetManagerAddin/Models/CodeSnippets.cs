using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SnippetManagerAddin
{

    [XmlRoot(ElementName = "SnippetTypes", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
    public class SnippetTypes
    {
        [XmlElement(ElementName = "SnippetType", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public List<string> SnippetType { get; set; }
    }

    [XmlRoot(ElementName = "Header", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
    public class Header
    {
        [XmlElement(ElementName = "Title", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public string Title { get; set; }
        [XmlElement(ElementName = "Shortcut", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public string Shortcut { get; set; }
        [XmlElement(ElementName = "Description", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Author", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public string Author { get; set; }
        [XmlElement(ElementName = "SnippetTypes", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public SnippetTypes SnippetTypes { get; set; }
    }

    [XmlRoot(ElementName = "Literal", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
    public class Literal
    {
        [XmlElement(ElementName = "ID", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public string ID { get; set; }
        [XmlElement(ElementName = "ToolTip", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public string ToolTip { get; set; }
        [XmlElement(ElementName = "Function", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public string Function { get; set; }
        [XmlElement(ElementName= "Default", Namespace= "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
		public string Default { get; set; }
    }

    [XmlRoot(ElementName = "Declarations", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
    public class Declarations
    {
        [XmlElement(ElementName = "Literal", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public List<Literal> Literal { get; set; }
    }

    [XmlRoot(ElementName = "Code", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
    public class Code
    {
        [XmlAttribute(AttributeName = "Language")]
        public string Language { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Snippet", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
    public class Snippet
    {
        [XmlElement(ElementName = "Declarations", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public Declarations Declarations { get; set; }
        [XmlElement(ElementName = "Code", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public Code Code { get; set; }
    }

    [XmlRoot(ElementName = "CodeSnippet", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
    public class CodeSnippet
    {
        [XmlElement(ElementName = "Header", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public Header Header { get; set; }
        [XmlElement(ElementName = "Snippet", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public Snippet Snippet { get; set; }
        [XmlAttribute(AttributeName = "Format")]
        public string Format { get; set; }
    }

    [XmlRoot(ElementName = "CodeSnippets", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
    public class CodeSnippets
    {
        [XmlElement(ElementName = "CodeSnippet", Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
        public CodeSnippet CodeSnippet { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

}

