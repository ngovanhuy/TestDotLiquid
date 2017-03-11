using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DotLiquid.Exceptions;
using DotLiquid.Util;
using System;

namespace DotLiquid.Tags
{
    /// <summary>
    /// The Extends tag is used in conjunction with the Block tag to provide template inheritance.
    /// For further syntax and usage please refer to
    /// <see cref="http://docs.djangoproject.com/en/dev/topics/templates/#template-inheritance"/>
    /// </summary>
    [Serializable]
    public class Layout : DotLiquid.Block
    {
        public static readonly Regex LayoutSyntax = new Regex("{%(\\s?|\\s*)layout (\\'|\\\"|)[a-zA-Z0-9_ .]*(\\'|\\\"|)(\\s|\\s*)?%}");
        public static readonly Regex NoneLayoutSyntax = new Regex("{%(\\s?|\\s*)layout none(\\s|\\s*)?%}");

        private static readonly Regex Syntax = new Regex(string.Format(@"^({0})", Liquid.QuotedFragment), RegexOptions.Compiled);

        private string _templateName;

        public string TemplateNameExpr { get; set; }

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            Match syntaxMatch = Syntax.Match(markup);

            if (syntaxMatch.Success)
            {
                _templateName = syntaxMatch.Groups[1].Value;
                if (_templateName != "none")
                    TemplateNameExpr = (string)ExpressionUtility.Parse(_templateName);
            }
            else
            {
                throw new SyntaxException(Liquid.ResourceManager.GetString("LayoutTagSyntaxException"));
            }

            base.Initialize(tagName, markup, tokens);
        }

        internal void AssertTagRulesViolation(List<IRenderable> rootNodeList)
        {
            if (!(rootNodeList[0] is Layout))
                throw new SyntaxException(Liquid.ResourceManager.GetString("LayoutTagMustBeFirstTagException"));

            if (NodeList.Count(o => o is Layout) > 0)
                throw new SyntaxException(Liquid.ResourceManager.GetString("LayoutTagCanBeUsedOneException"));
        }

        protected override void AssertMissingDelimitation()
        {
        }

        public override void Render(Context context, TextWriter result)
        {
            RenderAll(NodeList, context, result);
        }
    }
}