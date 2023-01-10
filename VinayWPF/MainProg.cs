using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace VinayWPF
{
    public class MainProg
    {
        public string finalResult(string input){
            string ans = "";

            List<string> tokens;
            string Tree;

            Lexer lexer = new Lexer(input);
            Interpreter interpreter = new Interpreter();
            tokens = lexer.GenerateTokens();
             if (tokens.Count == 0)
             {
                return "Invalid Expression";
                 //continue;
             }
            
            if (tokens[0].Contains("Invalid Varaible:"))
            {
                return tokens[0];
               
            }

            if (tokens[0].Contains("Undefined Token:"))
            {
                return tokens[0];
                
            }
            /*
             foreach (string token in tokens)
             {
                 var splittedToken = token.Split(":");

                 if (splittedToken[0].Equals("Number"))
                 {


                     Console.WriteLine($"Token ID {splittedToken[0]} Token Value {long.Parse(splittedToken[1])}");
                 }
                 else
                 {
                     Console.WriteLine($"Token ID {splittedToken[0]}");
                 }

             }

*/             //Console.WriteLine();
           // return string.Join(",", tokens);
            
            Parser p1 = new Parser(tokens);
            Tree = p1.Parse();
            if (Tree.Equals("Syntax Error"))
            {
                //Console.WriteLine("Syntax Error");
                Lexer.plotTrue = 0;
                return "Syntax Error";


            }
            //Console.WriteLine(Tree);
           //return Tree;
            
           ans = interpreter.interpret(Tree);
            //Console.WriteLine(ans);

            return ans;
            
            
        }
    }

    public class MainViewModel
    {
        public MainViewModel()
        {
            this.Title = "Test Plot";
            this.Points = new List<DataPoint>
            {
                new DataPoint(0, 4),
                new DataPoint(10, 13),
                new DataPoint(20, 15),
                new DataPoint(30, 16),
                new DataPoint(40, 12),
                new DataPoint(50, 12)
            };
        }
        public string Title { get; private set; }
        public IList<DataPoint> Points { get; private set; }
    }
}
