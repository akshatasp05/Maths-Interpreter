using OxyPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VinayWPF;

namespace VinayWPF
{
    public class Lexer
    {
        private string input;
        private int currentIndex = -1;
        private char[] WHITESPACE = { ' ', '\n', '\t', };
        private string[] reservedWords = { "if", "while", "for", "foreach", "switch", "break", "continue", "else", "plot" ,"sqrt","vecs","bisec","diff"};
        //private char[] specialCharaters = { '(', '@', ')', '-', '+','*','/','%','' };
        private int equalityCheck = 0, plotCheck = 0, plotChecked = 0,vecCheked=0,bisecChecked=0, diffChecked=0, rootChecked=0;
        public static int plotTrue = 0; //to check if plot function runs
        Tokens T1 = new Tokens();
        List<string> tokens = new List<string>();
        public string[] temp_string;
        bool isValidExp;
        public Lexer(string input)
        {
            Lexer.plotTrue = 0;
            this.input = input;
            plotCheck = 0;
            this.Next();
        }
        //Get current character from a given input
        private char CurrentChar()
        {

            if (this.currentIndex != this.input.Length)
            {
                return this.input[this.currentIndex];

            }
            return '\0';
        }

        //Go to the next character
        private void Next()
        {
            if (this.currentIndex != this.input.Length)
            {
                this.currentIndex = this.currentIndex + 1;
            }

        }


        /*
        public void checkPlotFunc()
        {
            if (this.input.Contains("plot"))
            {

                this.temp_string = this.input.Split("=");
                if (this.temp_string.Length == 2)
                {


                    this.input = this.temp_string[1];
                }
                else
                {

                }
            }

        }
        */
        public List<string> GenerateTokens()
        {

            char currentCharacter = this.CurrentChar();

            while (!currentCharacter.Equals('\0'))
            {
                currentCharacter = this.CurrentChar();
                //Check for variable  assignment
                if (this.input.Contains("->") && equalityCheck == 0)
                {




                    equalityCheck = 1;
                    string[] temp_string = this.input.Split("->", 2);
                    bool is_digit;
                    double variableValue;
                    if (temp_string.Length == 2)
                    {

                        is_digit = Double.TryParse(temp_string[1], out variableValue);
                        /*
                        if (!Regex.IsMatch(temp_string[0], "[a-zA-Z0-9_]"))//check for special character in a string
                            {
                            //Console.WriteLine("Varable Name can only contain alphanumeric characters and not a special character");
                            tokens.Add("Invalid Varaible: Varable Name can only contain alphanumeric characters and not a special character");
                            return tokens;
                        }
                        */
                        if (temp_string[0].Trim().Any(ch => !char.IsLetterOrDigit(ch)))
                        {
                            tokens.Add("Invalid Varaible: Varable Name can only contain alphanumeric characters and not a special character");
                            return tokens;
                        }
                        if (is_digit == false)//if we assign expression to a variable
                        {
                            //Console.WriteLine("Invalid Variable Value");
                            //return new List<string>();
                            this.input = temp_string[1].Trim();
                            tokens.Add(T1.variable + ":" + temp_string[0].Trim());
                            tokens.Add(T1.assignment);
                            continue;

                        }
                        if (reservedWords.Contains(temp_string[0].Trim()))//check for  reserved words
                        {
                            //Console.WriteLine("Invalid Varaible: Variable name cannot be a reserved word");
                            tokens.Add("Invalid Varaible: Variable name cannot be a reserved word");
                            return tokens;
                        }

                        if (Char.IsDigit(temp_string[0][0]))
                        {
                            //Console.WriteLine("Invalid Varaible: Variable name cannot begin with a number");
                            tokens.Add("Invalid Varaible: Variable name cannot begin with a number");
                            return tokens;
                        }
                        tokens.Add(T1.variable + ":" + temp_string[0].Trim());
                        tokens.Add(T1.assignment);
                        tokens.Add(T1.number + ":" + temp_string[1]);
                        //Lexer.variablesStored.Add(temp_string[0], temp_string[1]);
                        break;
                    }
                }
                /*
                else if (this.input.Contains("plot") && this.input[4].Equals('(') && plotCheck == 0)
                {
                    plotCheck = 1;

                    string plotname = this.input.Substring(0, 3);
                    string leftpar = this.input.Substring(4, 1);
                    string rightpar = this.input.Substring(input.Length - 1);

                    string varPlotString = "";
                    varPlotString += this.CurrentChar();
                    this.Next();
                    int countLetters = 1;
                    while (countLetters != 4)
                    {
                        countLetters += 1;
                        varPlotString += this.CurrentChar();
                        this.Next();
                    }

                    if (varPlotString.Equals("plot") && this.CurrentChar().Equals('('))
                    {
                        tokens.Add(T1.plot.ToString());
                    }
                    else
                    {
                        tokens.Add("Undefined Token:" + this.input);
                    }
                }

                */

                //Checking if request came for plot or not

                else if (this.input.StartsWith("plot(") && plotCheck == 0 && plotChecked == 0)
                {
                    plotChecked = 1; // toexecute this if block only once
                    plotCheck = 1;
                    string varPlotString = "";
                    varPlotString += this.CurrentChar();
                    this.Next();
                    int countLetters = 1;
                    while (countLetters != 4)
                    {
                        countLetters += 1;
                        varPlotString += this.CurrentChar();
                        this.Next();
                    }
                    if (!varPlotString.Equals("plot") && (!this.CurrentChar().Equals('(') || !this.CurrentChar().Equals(' ')))
                    {
                        tokens.Clear();
                        tokens.Add("Undefined Token:" + this.input);
                        break;
                    }
                    Lexer.plotTrue += 1;
                    tokens.Add(T1.plot.ToString());
                }
                //checking for differentiation

                else if (this.input.StartsWith("diff(") && diffChecked == 0 )
                {
                   
                    string varPlotString = "";
                    varPlotString += this.CurrentChar();
                    this.Next();
                    int countLetters = 1;
                    while (countLetters != 4)
                    {
                        countLetters += 1;
                        varPlotString += this.CurrentChar();
                        this.Next();
                    }
                    if (!varPlotString.Equals("diff") && (!this.CurrentChar().Equals('(') || !this.CurrentChar().Equals(' ')))
                    {
                        tokens.Clear();
                        tokens.Add("Undefined Token:" + this.input);
                        break;
                    }
                    diffChecked = 1;
                    tokens.Add(T1.diff.ToString());
                }
                //checking for newtonRaphson Method
                else if (this.input.StartsWith("root(") && rootChecked == 0)
                {

                    string varPlotString = "";
                    varPlotString += this.CurrentChar();
                    this.Next();
                    int countLetters = 1;
                    while (countLetters != 4)
                    {
                        countLetters += 1;
                        varPlotString += this.CurrentChar();
                        this.Next();
                    }
                    if (!varPlotString.Equals("root") && (!this.CurrentChar().Equals('(') || !this.CurrentChar().Equals(' ')))
                    {
                        tokens.Clear();
                        tokens.Add("Undefined Token:" + this.input);
                        break;
                    }
                    rootChecked = 1;
                    tokens.Add(T1.root.ToString());
                }

                ///checking if request came for bisection or not
                else if (this.input.StartsWith("bisec(") && bisecChecked == 0 )
                {
                  // toexecute this if block only once
                   
                  
                    plotCheck = 1;
                    string varPlotString = "";
                    varPlotString += this.CurrentChar();
                    this.Next();
                    int countLetters = 1;
                    while (countLetters != 5)
                    {
                        countLetters += 1;
                        varPlotString += this.CurrentChar();
                        this.Next();
                    }
                    if (!varPlotString.Equals("bisec") && (!this.CurrentChar().Equals('(') || !this.CurrentChar().Equals(' ')))
                    {
                        tokens.Clear();
                        tokens.Add("Undefined Token:" + this.input);
                        break;
                    }
                    bisecChecked = 1;
                 
                    tokens.Add(T1.bisec.ToString());
                }

                else if (currentCharacter.Equals('<'))
                {
                    this.Next();
                    Char currentchar = this.CurrentChar();
                    if (currentchar.Equals('='))
                    {
                        this.tokens.Add(T1.lessThanOrEqual.ToString());
                        this.Next();
                        continue;
                    }
                    /*
                    else if (WHITESPACE.Contains(currentCharacter))
                    {
                        this.Next();
                        currentchar = this.CurrentChar();
                        if(currentchar.Equals('='))
                        {
                            this.tokens.Add(T1.lessThanOrEqual.ToString());
                            this.Next();
                            continue;
                        }
                    }
                 */
                    this.tokens.Add(T1.lessThan.ToString());
                    //this.Next();



                }
                else if (currentCharacter.Equals('>'))
                {
                    this.Next();
                    Char currentchar = this.CurrentChar();
                    if (currentchar.Equals('='))
                    {
                        this.tokens.Add(T1.greaterThanOrEqual.ToString());
                        this.Next();
                        continue;
                    }
                    /*
                    else if (WHITESPACE.Contains(currentCharacter))
                    {
                        this.Next();
                        currentchar = this.CurrentChar();
                        if (currentchar.Equals('='))
                        {
                            this.tokens.Add(T1.greaterThanOrEqual.ToString());
                            this.Next();
                            continue;
                        }
                    }
                    */
                    this.tokens.Add(T1.greaterThan.ToString());
                    //this.Next();
                }
                else if (currentCharacter.Equals('='))
                {
                    this.Next();
                    Char currentchar = this.CurrentChar();
                    if (currentchar.Equals('='))
                    {
                        this.tokens.Add(T1.equality.ToString());
                        this.Next();
                        continue;
                    }
                    /*
                    else if (WHITESPACE.Contains(currentCharacter))
                    {
                        this.Next();
                        currentchar = this.CurrentChar();
                        if (currentchar.Equals(T1.assignment))
                        {
                            this.tokens.Add(T1.equality.ToString());
                            this.Next();
                            continue;
                        }
                    }
                    */
                }

                else if (this.CurrentChar().Equals(',') && plotChecked == 1)
                {
                    this.tokens.Add(T1.seperator.ToString());
                    this.Next();
                }
                else if (this.CurrentChar().Equals(',') && (vecCheked == 1 || bisecChecked==1 || rootChecked==1))
                {
                    this.tokens.Add(T1.seperator.ToString());
                    this.Next();
                }
                else if (this.CurrentChar().Equals('.') && vecCheked == 1)
                {
                    this.tokens.Add(T1.dot.ToString());
                    this.Next();
                }
                else if (this.CurrentChar().Equals('&') && vecCheked == 1)
                {
                    this.tokens.Add(T1.cross.ToString());
                    this.Next();
                }


                else if (WHITESPACE.Contains(currentCharacter))
                {
                    this.Next();
                }
                else if (Char.IsDigit(currentCharacter) || currentCharacter.Equals('.'))
                {
                    this.tokens.Add(T1.number.ToString() + ":" + GenerateNumbers().Trim());
                    this.Next();
                }
                else if (currentCharacter.Equals('+'))
                {
                    this.tokens.Add(T1.plus.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('-'))
                {
                    this.tokens.Add(T1.subtract.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('*'))
                {
                    this.tokens.Add(T1.multiplication.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('/'))
                {
                    this.tokens.Add(T1.division.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('('))
                {
                    this.tokens.Add(T1.leftParenthesis.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals(')'))
                {
                    this.tokens.Add(T1.rightParenthesis.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('%'))
                {
                    this.tokens.Add(T1.percentage.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('^'))
                {
                    this.tokens.Add(T1.expo.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('\n'))
                {
                    this.Next();
                }
                else if (currentCharacter.Equals('\0'))
                {
                    break;
                }
                /*
                else if(Interpreter.variablesStored.ContainsKey(this.input))//check if variable exist or not 
                {
                    tokens.Add(T1.variable + ":" + this.input);
                    break;
                }
                */
                else if ((Char.IsLetter(currentCharacter) || currentCharacter.Equals('_')) && plotCheck == 0 && bisecChecked==0 && diffChecked ==0 && rootChecked==0)//check if variable exist or not 
                {
                    string variableStr = this.CurrentChar().ToString();
                    this.Next();
                    Char currentChar = this.CurrentChar();
                    while (!currentChar.Equals(' ') & ((Char.IsLetterOrDigit(currentChar)) || currentChar.Equals('_')))
                    {
                        variableStr = variableStr + currentChar;
                        this.Next();
                        currentChar = this.CurrentChar();
                    }
                    if (variableStr.Equals("sqrt"))
                    {
                        if (this.CurrentChar().Equals('('))
                        {
                            //this.Next();
                            tokens.Add(T1.sqrt.ToString());
                        }
                        else
                        {
                            tokens.Clear();
                            tokens.Add("Undefined Token:" + this.input);
                            break;
                        }
                    }
                    //for vector identification
                    else if(variableStr.Equals("vec"))
                    {
                        if (this.CurrentChar().Equals('('))
                        {
                            //this.Next();
                            this.vecCheked = 1;

                            tokens.Add(T1.vector.ToString());
                        }
                        else
                        {
                            tokens.Clear();
                            tokens.Add("Undefined Token:" + this.input);
                            break;
                        }
                    }
                    else
                    {
                        //this.currentIndex = this.currentIndex - 1;
                        if (Interpreter.variablesStored.ContainsKey(variableStr))
                        {
                            tokens.Add(T1.variable + ":" + variableStr);
                        }
                        else
                        {
                            tokens.Clear();
                            tokens.Add("Undefined Token:" + variableStr);
                            break;
                        }
                    }
                }

                else if (Char.IsLetter(currentCharacter) && (plotCheck == 1 ||bisecChecked==1 || diffChecked ==1 || rootChecked==1))
                {
                    //plotCheck = 0;
                    
                    if (!currentCharacter.Equals('x'))
                    {
                        Lexer.plotTrue = 0;
                        tokens.Clear();
                        tokens.Add("Undefined Token:" + currentCharacter);
                        break;
                    }
                    tokens.Add(T1.variable + ":" + currentCharacter);
                    this.Next();
                }

                else
                {
                    tokens.Clear();
                    tokens.Add("Undefined Token:" + this.CurrentChar());



                }

            }

            return this.tokens;
        }



        //Generate numbers with multiple digits 
        public string GenerateNumbers()
        {
            int decimalPointCnt = 0;
            var numberStr = this.CurrentChar().ToString();
            this.Next();
            var currentChar = this.CurrentChar();

            while (!currentChar.Equals(' ') & (currentChar.Equals('.') | Char.IsDigit(currentChar)))
            {

                if (currentChar.Equals('.'))
                {
                    decimalPointCnt += 1;
                }
                if (decimalPointCnt > 1)
                    break;
                numberStr += currentChar;

                this.Next();
                currentChar = this.CurrentChar();
            }
            if (numberStr.StartsWith('.'))
                numberStr = "0" + numberStr;
            if (numberStr.EndsWith('.'))
                numberStr = numberStr + "0";
            // Console.WriteLine(this.CurrentChar());
            //Decrement current index by 1 to avoid 2222+ scenario 
            this.currentIndex = this.currentIndex - 1;
            return numberStr;
        }
    }

}





/*
 Notes:

1)Why did not we go ahead with dictionary - It was rasing duplicate key error as one dictionary cannot have multiple similar keys.
 */