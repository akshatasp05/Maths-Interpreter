using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinayWPF;

namespace VinayWPF
{
    public class Parser
    {
        public List<string> input, div;
        public int currentIndex = -1;
        public int isEnded = 0, isSytaxError = 0;
        public string res, currentchar;
        public int countParenOpen = 0, countParenClose = 0, actualcountParenOpen = 0, actualcountParenClose = 0;

        Tokens T1 = new Tokens();
        Nodes N1 = new Nodes();
        public Parser(List<string> input)
        {
            this.input = input;
            this.Next();
        }
        public void Next()
        {

            if (this.currentIndex != this.input.Count - 1)
            {
                this.currentIndex = this.currentIndex + 1;
            }
            else
            {
                this.isEnded = 1;

            }


        }
        public string CurrentValue()
        {
            return this.input[this.currentIndex];
        }
        public string RaiseError()
        {
            this.isSytaxError = 1;
            return "Syntax Error";
        }
        public string Parse()
        {
            if (this.CurrentValue() == "\0")
            {
                return null;
            }
            foreach (string str in this.input)
            {
                if (str.Equals("("))
                {
                    actualcountParenOpen = actualcountParenOpen + 1;
                }
                if (str.Equals(")"))
                {
                    actualcountParenClose = actualcountParenClose + 1;
                }
            }
            if (actualcountParenOpen != actualcountParenClose)
            {
                return "Syntax Error";
            }
            //storing the result of an expression
            res = this.Expression();


            //Console.WriteLine(this.CurrentValue());

            /*
            if (this.CurrentValue() != "\0")
                this.RaiseError();0
            */
            this.currentchar = this.CurrentValue();
            if (this.currentchar.Equals(T1.assignment) && this.isEnded != 1)
            {
                this.Next();
                res = N1.AssignmentNode(res, this.Expression());
            }
            if (this.currentchar.Equals(T1.lessThanOrEqual) && this.isEnded != 1)
            {
                this.Next();
                res = N1.LessThanOrEqual(res, this.Parse());
            }
            if (this.currentchar.Equals(T1.lessThan) && this.isEnded != 1)
            {
                this.Next();
                res = N1.LessThan(res, this.Parse());
            }
            if (this.currentchar.Equals(T1.greaterThanOrEqual) && this.isEnded != 1)
            {
                this.Next();
                res = N1.GreaterThanOrEqual(res, this.Parse());
            }
            if (this.currentchar.Equals(T1.greaterThan) && this.isEnded != 1)
            {
                this.Next();
                res = N1.GreaterThan(res, this.Parse());
            }
            if (this.currentchar.Equals(T1.equality) && this.isEnded != 1)
            {
                this.Next();
                res = N1.EqualityNode(res, this.Parse());
            }
            if (res.Contains("Syntax Error") || this.isSytaxError == 1)
            {
                return "Syntax Error";
            }


            /*
            foreach(char c in res)
            {
                if(c.Equals('('))
                {
                    countParenOpen = countParenOpen + 1;
                }
                if(c.Equals(')'))
                {
                    countParenClose = countParenClose + 1;
                }
            }
            */

            if (this.isEnded != 1)
            {
                return "Syntax Error";
            }
            return res;

        }

        public string Expression()
        {
            res = this.Term();
            if (res.Contains("Syntax Error") || this.isSytaxError == 1)
            {
                res = "Syntax Error";
            }
            else
            {
                this.currentchar = this.CurrentValue();
                while (this.isEnded != 1 && (this.currentchar.Equals(T1.plus) || this.currentchar.Equals(T1.subtract) || this.currentchar.Equals(T1.dot)) || this.currentchar.Equals(T1.cross))
                {

                    if (this.currentchar.Equals(T1.plus))
                    {
                        this.Next();
                        res = N1.AddNode(res, this.Term()); // for example -  add node 2 + 3
                    }
                    else if ((this.currentchar.Equals(T1.subtract)))
                    {
                        this.Next();
                        res = N1.SubtractNode(res, this.Term());  // for example -  add node 2 - 3
                    }
                    else if ((this.currentchar.Equals(T1.dot)))
                    {
                        this.Next();
                        res = N1.dotNode(res, this.Term());  // for example -  add node 2 - 3
                    }
                    else if ((this.currentchar.Equals(T1.cross)))
                    {
                        this.Next();
                        res = N1.crossNode(res, this.Term());  // for example -  add node 2 - 3
                    }



                }
            }

            return res;
        }
        //this function checks for the multiplication and division because of the presedence 
        public string Term()
        {
            res = this.Factor();
            //Console.WriteLine(res);
            if (res.Contains("Syntax Error") || this.isSytaxError == 1)
            {
                res = "Syntax Error";
            }
            else
            {
                this.currentchar = this.CurrentValue();
                while (this.isEnded != 1 && (this.currentchar.Equals(T1.multiplication) || this.currentchar.Equals(T1.division) || this.currentchar.Equals(T1.expo)) || this.currentchar.Equals(T1.seperator))
                {

                    if (this.currentchar.Equals(T1.multiplication))
                    {
                        this.Next();
                        res = N1.MultiplyNode(res, this.Term()); // for example -  add node 2 * 3
                    }
                    else if ((this.currentchar.Equals(T1.division)))
                    {
                        this.Next();

                        res = N1.DivideNode(res, this.Term()); // for example -  add node 2 / 3
                    }
                    else if ((this.currentchar.Equals(T1.expo)))
                    {
                        this.Next();
                        res = N1.ExpononetNode(res, this.Term()); // for example -  add node 2 ^ 3
                    }

                    else if ((this.currentchar.Equals(T1.seperator)))
                    {
                        this.Next();
                        res = N1.seperatorNode(res, this.Term()); // for example -  add node 2 ^ 3
                    }




                }
            }

            return res;

        }
        //this function checks for number, left parenthesis then for the right parenthesis and then for single + or - operator
        public string Factor()

        {
            try
            {

                this.currentchar = this.CurrentValue();

                if (currentchar.Equals(T1.leftParenthesis))
                {

                    this.Next();
                    res = this.Expression();

                    this.currentchar = this.CurrentValue();
                    if (!this.currentchar.Equals(T1.rightParenthesis))
                    {
                        return this.RaiseError();
                    }
                    this.Next();
                    return res;
                }


                else if (this.currentchar.Contains(T1.number))
                {
                    this.Next();


                    return N1.NumberNode(this.currentchar.Split(":")[1].Trim());

                }
                /*
                else if (this.currentchar.Equals(T1.plus) && (this.isEnded != 1))
                {
                    this.Next();


                    return N1.AdditionNode(this.Factor()); ////to add node +2

                }
                */
                //checking for negation
                else if (this.currentchar.Equals(T1.subtract) && (this.isEnded != 1))
                {
                    this.Next();

                    return N1.SubtractionNode(this.Factor());//to add node -2
                }
                else if (this.currentchar.Contains(T1.variable))
                {
                    this.Next();
                    return N1.VariableNode(this.currentchar.Split(":")[1].Trim());
                }
                //checking for plot function
                else if (this.currentchar.Contains(T1.plot) && this.isEnded != 1) //check for plots
                {
                    this.Next();

                    if (!this.input[this.input.Count-1].Equals(T1.rightParenthesis))
                    {
                        return this.RaiseError();
                    }
                    return N1.plotNode(this.currentchar, this.Factor());

                }
                //checking for bisection
                else if (this.currentchar.Contains(T1.bisec) && this.isEnded != 1) //check for plots
                {
                    this.Next();

                    if (!this.input[this.input.Count - 1].Equals(T1.rightParenthesis))
                    {
                        return this.RaiseError();
                    }
                    return N1.bisecNode(this.currentchar, this.Factor());

                }
                //check for differentiation
                else if (this.currentchar.Contains(T1.diff) && this.isEnded != 1) //check for plots
                {
                    this.Next();

                    if (!this.input[this.input.Count - 1].Equals(T1.rightParenthesis))
                    {
                        return this.RaiseError();
                    }
                    return N1.diffNode(this.currentchar, this.Factor());

                }
                //check for newton ramphson method
                else if (this.currentchar.Contains(T1.root) && this.isEnded != 1)
                {
                    this.Next();

                    if (!this.input[this.input.Count - 1].Equals(T1.rightParenthesis))
                    {
                        return this.RaiseError();
                    }
                    return N1.rootNode(this.currentchar, this.Factor());

                }
                //checking for vector
                else if (this.currentchar.Contains(T1.vector) && this.isEnded != 1) //check for plots
                {
                    this.Next();

                    if (!this.input[this.input.Count - 1].Equals(T1.rightParenthesis))
                    {
                        return this.RaiseError();
                    }
                    return N1.vecNode(this.currentchar, this.Factor());

                }
                //checking for sqrt function
                else if (this.currentchar.Contains(T1.sqrt) && this.isEnded != 1) //check for plots
                {
                    this.Next();


                    return N1.sqrtNode(this.currentchar, this.Factor());

                }


                return this.RaiseError();

            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Expression");
                return null;
            }
        }

    }
}

/*BNF RUles
 <parse>::=variable -> <expr> | <expr> >= <parse> | <expr> <= <parse> | <expr> == <parse> | <expr> < <parse> | <expr> > <parse> | <expr>
<expr> ::= <term>
| <expr> + <term>
| <expr> - <term>
<term> ::= <factor>             factor - numbers or ( a+b)                               term - number1 * number2 or number1/number2 or number1^number2
| <term> * <factor>         
| <term> / <factor>
| <term>^<factor>
|<factor>
<factor> ::= <double> | <expr> | <variable name> | '(' <expr> ')' | plot<factor> | vector<factor>
<double> ::= <digit> | <double><digit>
<digit> ::= 0|1|2|3|4|5|6|7|8|9 | | negative  numbers | float numbers 
<variable name> ::= any variable name
 *
 */