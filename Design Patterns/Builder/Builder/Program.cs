using System.ComponentModel.Design.Serialization;
using System.Text;

namespace Builder
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var builder = new HtmlBuilder("ul");
            //builder.addChild("li", "hello!");
            //builder.addChild("li", "world!");
            //Console.WriteLine(builder.ToString());


        }
        
    }

    public class HtmlElement
    {
        private const int identS = 2;
        public string Nome, Texto;
        public List<HtmlElement> Elementos = new List<HtmlElement>();

        public HtmlElement()
        {
        }

        public HtmlElement(string name, string text)
        {
            Nome = name;
            Texto = text;
        }

        private string ToStringImplementation(int ident)
        {
            var sb = new StringBuilder();
            var i = new string(' ', identS * ident);
            sb.AppendLine($"{i}<{Nome}> ");

            if (!string.IsNullOrEmpty(Texto))
            {
                sb.Append(new string(' ', identS * (ident + 1)));
                sb.AppendLine(Texto);
            }

            foreach (var el in Elementos)
            {
                sb.Append(el.ToStringImplementation(ident + 1));
            }

            sb.AppendLine($"{i}</{Nome}> ");

            return sb.ToString();
        }


        public override string ToString()
        {
            return ToStringImplementation(0);
        }
    }
    public class HtmlBuilder()
    {
        private readonly string elNome;
        HtmlElement el = new HtmlElement();
            

        public HtmlBuilder(string elNome) : this()
        {
            this.elNome = elNome;
            el.Nome = elNome;
        }

        public void addChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            el.Elementos.Add(e);
        }

        public override string ToString()
        {
            return el.ToString();
        }

        public void Clear()
        {
            el = new HtmlElement{Nome = elNome};
        }
    }
}

