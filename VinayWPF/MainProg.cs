using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            /* if (tokens.Count == 0)
             {
                 Console.WriteLine("Undefined Token");
                 continue;
             }
            */
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

*/             //Console.WriteLine(string.Join(",", tokens));

            Parser p1 = new Parser(tokens);
            Tree = p1.Parse();
            if (Tree.Equals("Syntax Error"))
            {
                //Console.WriteLine("Syntax Error");
                return "Syntax Error";


            }
            //Console.WriteLine(Tree);

            ans = interpreter.interpret(Tree);
            //Console.WriteLine(ans);

            return ans;
        }
    }
}
