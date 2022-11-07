using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace VinayWPF

{
    public class Interpreter
    {

        Stack<double> Operands = new Stack<double>(); //stack for number
        Stack<Char> Operations = new Stack<Char>(); // st
        Stack<double> OperandsPlot = new Stack<double>(); //stack for number
        Stack<Char> OperationsPlot = new Stack<Char>();                                         // ack for operators
        List<double> div_num = new List<double>();
        List<double> plotNumbersX = new List<double>();
        List<double> plotNumbersY = new List<double>();
        List<double> plotNumbersOutput = new List<double>();
        List<double> parameters = new List<double>();
        private Dictionary<Char, List<double>> plotValues = new Dictionary<Char, List<double>>();
        public static Dictionary<string, double> variablesStored = new Dictionary<string, double>();
        Tokens T1 = new Tokens();
        string[] temp_string;
        int div_count = 1, plotFunction = 0, requestForPlot = 0;
        string variable;
        double output, div_exp, temp_num1, upperbound, lowerbound, scale, stepCount, valueOfx;

        public double Calculate(Stack<double> Operands, Stack<Char> Operations)
        {
            double num1, num2;
            char operation = Operations.Pop();
            if (operation.Equals('~'))
            {
                if (Operations.Peek().Equals('$'))
                {
                    return -this.valueOfx;
                }
                num1 = Operands.Pop();
                return -num1;

            }
            else
            {


                switch (operation)
                {
                    case '+':
                        num1 = Operands.Pop();
                        if (Operations.Peek().Equals('$'))
                        {
                            num2 = this.valueOfx;
                            // Operations.Pop();
                            return num1 + num2;
                        }

                        num2 = Operands.Pop();
                        return num1 + num2;
                    case '-':
                        num1 = Operands.Pop();
                        if (Operations.Peek().Equals('$'))
                        {
                            num2 = this.valueOfx;
                            //Operations.Pop();
                            return num2 - num1;
                        }
                        num2 = Operands.Pop();
                        return num2 - num1;
                    case '*':
                        num1 = Operands.Pop();
                        if (Operations.Peek().Equals('$'))
                        {
                            num2 = this.valueOfx;
                            // Operations.Pop();
                            return num1 * num2;
                        }
                        num2 = Operands.Pop();
                        return num1 * num2;
                    case '/':
                        /* div_count = 2;
                         if (Operations.Peek() == '/')
                         {
                             while (Operations.Peek() == '/')
                             {
                                 div_count = div_count + 1;
                                 operation = Operations.Pop();

                             }
                             while(div_count!=0)
                             {
                                 div_num.Add(Operands.Pop());
                                 div_count = div_count - 1;
                             }
                             div_exp=div_num[div_num.Count - 1];
                             div_num.RemoveAt(div_num.Count - 1);
                             while (div_num.Count!=0)
                             {
                                 temp_num1 = div_num[div_num.Count - 1];
                                 if (temp_num1 == 0)
                                 {
                                     Console.WriteLine("Cannot divide by zero");
                                     return 0.0;
                                 }
                                 div_num.RemoveAt(div_num.Count - 1);
                                 div_exp = div_exp / temp_num1;


                             }
                             return div_exp;
                         }
                         else
                         {*/
                        num1 = Operands.Pop();
                        if (num1 == 0)
                        {
                            Console.WriteLine("Cannot divide by zero");
                            return 0.0;
                        }
                        if (Operations.Peek().Equals('$'))
                        {
                            num2 = this.valueOfx;

                            return num2 / num1;
                        }
                        num2 = Operands.Pop();

                        return num2 / num1;
                        //}

                        return 0.0;
                    case '%':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();
                        return num2 % num1;
                    case '^':
                        num1 = Operands.Pop();
                        if (Operations.Peek().Equals('$'))
                        {
                            num2 = this.valueOfx;

                            return Math.Pow(num2, num1);
                        }
                        num2 = Operands.Pop();
                        return Math.Pow(num2, num1);

                    case '=':
                        num1 = Operands.Pop();
                        if (Operations.Peek() == '=')
                        {
                            Operations.Pop();
                            num2 = Operands.Pop();

                            if (num1 == num2)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }

                        }
                        else if (Operations.Peek() == '<')
                        {
                            num2 = Operands.Pop();
                            Operations.Pop();
                            if (num2 <= num1)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }

                        }
                        else if (Operations.Peek() == '>')
                        {
                            num2 = Operands.Pop();
                            Operations.Pop();
                            if (num2 >= num1)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }

                        }
                        Interpreter.variablesStored[variable] = num1;
                        return num1;

                    case '>':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();

                        if (num2 > num1)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }


                    case '<':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();

                        if (num2 < num1)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }


                    case '(':
                        return Operands.Pop();
                    case '$':
                        return Operands.Pop();


                }
            }
            return 0.0;
        }

        public double plotInterpreter(string expr)
        {
            string number = "";
            //Console.WriteLine(expr);
            //Console.WriteLine(this.valueOfx);
            if (!expr.StartsWith('('))
            {
                expr = '(' + expr;
            }
            if (!expr.EndsWith(')'))

            {
                expr = expr + ')';
            }
            for (int i = 0; i < expr.Length; i++)
            {
                Char c = expr[i];
                if (Char.IsDigit(c))
                {

                    number = "";
                    //conditions for multiple digit numbers
                    while (Char.IsDigit(c) || c.Equals('.'))
                    {

                        number = number + c;
                        i = i + 1;

                        if (i < expr.Length)
                        {
                            c = expr[i];

                        }
                        else
                        {
                            break;
                        }


                    }
                    i = i - 1; // decrementing i to avoid skipping of any character
                    Operands.Push(double.Parse(number));

                }//else if block for variables which have acceess to symbol table
                else if ((Char.IsLetter(c) || c.Equals('_')) && this.plotFunction == 0)
                {
                    this.variable = "";
                    while (Char.IsLetterOrDigit(c) || c.Equals('_'))
                    {
                        this.variable = this.variable + c;
                        i = i + 1;
                        if (i < expr.Length)
                        {
                            c = expr[i];

                        }
                        else
                        {
                            break;
                        }
                    }
                    i = i - 1;
                    if (!Interpreter.variablesStored.ContainsKey(this.variable))
                    {
                        Interpreter.variablesStored[this.variable] = 0;
                    }
                    else
                    {
                        Operands.Push(Interpreter.variablesStored[this.variable]);
                    }
                }
                else if (Char.IsLetter(c) && this.plotFunction == 1)
                {
                    this.plotFunction = 0;
                    plotValues[c] = new List<double>();
                    Operands.Push(this.valueOfx);
                }


                else if (c.Equals('('))
                {
                    Operations.Push(c);
                }
                else if (c.Equals(')'))
                {
                    while (Operations.Peek() != '(')//check all the operators till '(' is reached
                    {
                        output = Calculate(Operands, Operations);
                        Operands.Push(output);   //push result back to stack
                    }
                    Operations.Pop();
                }
                else
                {
                    Operations.Push(c);
                }
            }
            while (Operations.Count() != 0 && Operands.Count() != 0)
            {
                output = Calculate(Operands, Operations);
                Operands.Push(output);   //push final result back to stack
            }
            return Operands.Pop();
        }
        public string interpret(string expr)
        {
            string number = "";

            if (Interpreter.variablesStored.ContainsKey(expr))
            {
                return Interpreter.variablesStored[expr].ToString();

            }
            if (expr.StartsWith("plot("))
            {
                this.plotFunction = 1; //to differtiate betweem plot expression sybols and variables
                this.requestForPlot = 1; // to identify request came for plot 
                expr = expr.Trim().Remove(0, 4);
                temp_string = expr.Split("##");
                expr = temp_string[0].Trim() + ')';
                if (temp_string.Length == 5)
                {
                    scale = Double.Parse(temp_string[4].Trim(')'));
                    stepCount = Double.Parse(temp_string[3].Trim());
                    upperbound = Double.Parse(temp_string[2].Trim());
                    lowerbound = Double.Parse(temp_string[1].Trim());
                }
                else if (temp_string.Length == 4)
                {
                    scale = 0.5;
                    stepCount = Double.Parse(temp_string[3].Trim(')'));
                    upperbound = Double.Parse(temp_string[2].Trim());
                    lowerbound = Double.Parse(temp_string[1].Trim());
                }
                else if (temp_string.Length == 3)
                {
                    scale = 0.5;
                    stepCount = 1;
                    upperbound = Double.Parse(temp_string[2].Trim(')'));
                    lowerbound = Double.Parse(temp_string[1].Trim());
                }
                else if (temp_string.Length == 2)
                {
                    scale = 0.5;
                    stepCount = 1;
                    lowerbound = Double.Parse(temp_string[1].Trim(')'));
                    upperbound = lowerbound + 10;


                }
                else
                {
                    scale = 0.5;
                    stepCount = 1;
                    lowerbound = 1;
                    upperbound = lowerbound + 10;
                }
                //Operations.Push('$');
                for (double j = lowerbound; j <= upperbound; j = j + stepCount)
                {
                    this.plotFunction = 1;
                    this.valueOfx = j;
                    output = plotInterpreter(expr);

                    //  Console.WriteLine(j);
                    plotNumbersX.Add(j);
                    plotNumbersY.Add(output);
                }
                foreach (double item in plotNumbersY)
                {
                    Console.WriteLine(item);
                }
                return "1";
            }

            if (!expr.StartsWith('(') && plotFunction == 0)
            {
                expr = '(' + expr;
            }
            if (!expr.EndsWith(')') && plotFunction == 0)

            {
                expr = expr + ')';
            }




            for (int i = 0; i < expr.Length; i++)
            {
                Char c = expr[i];
                if (Char.IsDigit(c))
                {

                    number = "";
                    //conditions for multiple digit numbers
                    while (Char.IsDigit(c) || c.Equals('.'))
                    {

                        number = number + c;
                        i = i + 1;

                        if (i < expr.Length)
                        {
                            c = expr[i];

                        }
                        else
                        {
                            break;
                        }


                    }
                    i = i - 1; // decrementing i to avoid skipping of any character
                    Operands.Push(double.Parse(number));

                }//else if block for variables which have acceess to symbol table
                else if ((Char.IsLetter(c) || c.Equals('_')) && plotFunction == 0)
                {
                    this.variable = "";
                    while (Char.IsLetterOrDigit(c) || c.Equals('_'))
                    {
                        this.variable = this.variable + c;
                        i = i + 1;
                        if (i < expr.Length)
                        {
                            c = expr[i];

                        }
                        else
                        {
                            break;
                        }
                    }
                    i = i - 1;
                    if (!Interpreter.variablesStored.ContainsKey(this.variable))
                    {
                        Interpreter.variablesStored[this.variable] = 0;
                    }
                    else
                    {
                        Operands.Push(Interpreter.variablesStored[this.variable]);
                    }
                }
                else if (Char.IsLetter(c) && plotFunction == 1)
                {
                    plotFunction = 0;
                    plotValues[c] = new List<double>();
                    Operations.Push('$');
                }


                else if (c.Equals('('))
                {
                    Operations.Push(c);
                }
                else if (c.Equals(')'))
                {   //Code for plot function
                    /*
                    if (requestForPlot == 1)
                    {   
                        while (Operations.Peek() == '#')
                        {
                            Operations.Pop();
                            Operations.Pop();
                            parameters.Add(Operands.Pop());//poping parameters from the stack
                        }

                        //checking the number of paramaters based on the number of arguments setting the value for plot
                        if (parameters.Count == 4)
                        {
                            
                        }
                        else if (parameters.Count == 3)
                        {
                            scale = 0.5;
                            stepCount = parameters[0];
                            upperbound = parameters[1];
                            lowerbound = parameters[2];
                        }
                        else if (parameters.Count == 2)
                        {
                            scale = 0.5;
                            stepCount = 1;
                            upperbound = parameters[0];
                            lowerbound = parameters[1];
                        }
                        else if (parameters.Count == 1)
                        {
                            scale = 0.5;
                            stepCount = 1;
                            lowerbound = parameters[0];
                            upperbound = lowerbound + 15;
                        }
                        else if (parameters.Count == 0)
                        {
                            scale = 0.5;
                            stepCount = 1;
                            lowerbound = 1;
                            upperbound = 15;
                        }
                        //creating a list of input
                        for (double j = lowerbound; j <= upperbound; j = j + stepCount)
                        {

                            OperandsPlot = new Stack<double>(Operands.Reverse());
                            OperationsPlot = new Stack<Char>(Operations.Reverse());


                            this.valueOfx = j;
                                 while (OperationsPlot.Peek() != '(')//check all the operators till '(' is reached
                            {
                                output = Calculate(OperandsPlot, OperationsPlot);
                                OperandsPlot.Push(output);   //push result back to stack
                            }
                            OperationsPlot.Pop();
                            while (OperationsPlot.Count() != 0 && OperandsPlot.Count() != 0)
                            {
                                output = Calculate(OperandsPlot, OperationsPlot);
                                OperandsPlot.Push(output);   //push final result back to stack
                            }
                            plotNumbersY.Add(OperandsPlot.Pop());//creating list for x axis of plot
                            plotNumbersX.Add(j); // creating list for y axis of plot

                            //return "1";
                        }
                        foreach(double item in plotNumbersY)
                        {
                           Console.WriteLine(item);
                        }

                        return "1";
                    }//executing normal mathematical expression
                    else
                    {*/
                    while (Operations.Peek() != '(')//check all the operators till '(' is reached
                    {
                        output = Calculate(Operands, Operations);
                        Operands.Push(output);   //push result back to stack
                    }
                    Operations.Pop();

                }

                else
                {
                    Operations.Push(c);
                }

            }

            while (Operations.Count() != 0)
            {
                output = Calculate(Operands, Operations);
                Operands.Push(output);   //push final result back to stack
            }
            if (expr.Contains(T1.equality) || expr.Contains(T1.lessThan) || expr.Contains(T1.lessThanOrEqual) || expr.Contains(T1.greaterThan) || expr.Contains(T1.greaterThanOrEqual))
            {
                if (Operands.Pop() == 1)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            else
            {
                return Operands.Pop().ToString();
            }
        }

        //Evaluate given expression

    }


}