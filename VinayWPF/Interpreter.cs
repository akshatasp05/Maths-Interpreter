using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Windows.Media;

namespace VinayWPF

{
    public class Interpreter
    {

        Stack<double> Operands = new Stack<double>(); //stack for number
        Stack<Char> Operations = new Stack<Char>(); // st
        Stack<double> OperandsPlot = new Stack<double>(); //stack for number
        Stack<Char> OperationsPlot = new Stack<Char>();                                         // ack for operators
        List<double> div_num = new List<double>();
       public static List<double> plotNumbersX = new List<double>();
       public static List<double> plotNumbersY = new List<double>();
        List<double> plotNumbersOutput = new List<double>();
        List<double> parameters = new List<double>();
        List<string> variables = new List<string>();
        List<List<double>> vectors = new List<List<double>>();
        List<List<double>> vectorsResult = new List<List<double>>();
        private Dictionary<Char, List<double>> plotValues = new Dictionary<Char, List<double>>();
        public static Dictionary<string, double> variablesStored = new Dictionary<string, double>();
        Tokens T1 = new Tokens();
        string[] temp_string;
        int div_count = 1, plotFunction = 0, requestForPlot = 0,isVectorOperation=0,isDimensionSame=1;
        string variable,updateVariable="",var="";
        double output, div_exp, temp_num1, upperbound, lowerbound, scale, stepCount, valueOfx;
       // List<double> vecValues = new List<double>();
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
                        Interpreter.variablesStored[variables[0]] = num1;
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
                    case '@':
                        long num = Convert.ToInt64(Operands.Pop());
                        long start = 1, end = num / 2;
                        long mid = 0, ans = 0, sqr = 0;
                        while (start <= end)
                        {
                            mid = (start + end) / 2;
                            sqr = mid * mid;
                            if (sqr == num)
                            {
                                return mid;
                            }
                            else if (sqr <= num)
                            {
                                start = mid + 1;
                                ans = mid;
                            }
                            else
                            {
                                end = mid - 1;
                            }
                        }
                        return ans;


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
                    this.updateVariable = "";
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
                    this.variables.Add(this.variable);
                    i = i - 1;
                    if (!Interpreter.variablesStored.ContainsKey(this.variable))
                    {
                        //this.updateVariable = this.variable;
                        Interpreter.variablesStored[this.variable] = 0;
                        
                    }
                    else
                    {
                        Operands.Push(Interpreter.variablesStored[this.variable]);
                    }
                }
                else if (Char.IsLetter(c) && this.plotFunction == 1)
                {
                    //this.plotFunction = 0;
                    plotValues[c] = new List<double>();
                    Operands.Push(this.valueOfx);
                }


                else if (c.Equals('('))
                {
                    Operations.Push(c);
                }
                else if (c.Equals(')'))
                {
                    while (Operations.Peek() != '(' && Operations.Count()!=0)//check all the operators till '(' is reached
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
            if (expr.StartsWith("plot(") || expr.Contains("plot("))
            {
                Interpreter.plotNumbersX = new List<double>(); ;
                Interpreter.plotNumbersY = new List<double>(); ;
                this.plotFunction = 1; //to differentiate betweem plot expression symbols and variables
                this.requestForPlot = 1; // to identify request came for plot 
                expr = expr.Trim().Remove(0, 4);
                temp_string = expr.Split("##");
                int freqOpen = temp_string[0].Where(x => (x == '(')).Count();
                int freqClose = temp_string[0].Where(x => (x == ')')).Count();
                if (freqOpen > freqClose)
                {
                    expr = temp_string[0].Trim() + ')';
                }
                else
                {
                    expr = temp_string[0].Trim();
                }
                /*if (temp_string.Length == 5)
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
                */
                //Replace Code here
                if (temp_string.Length == 5)
                {
                    scale = Double.Parse(temp_string[4].Trim(')').Trim('('));
                    stepCount = Double.Parse(temp_string[3].Trim().Replace('~', '-').Trim('(').Trim(')'));
                    upperbound = Double.Parse(temp_string[2].Trim().Replace('~', '-').Trim('(').Trim(')'));
                    lowerbound = Double.Parse(temp_string[1].Trim().Replace('~', '-').Trim('(').Trim(')'));
                }

                else if (temp_string.Length == 4)
                {
                    scale = 0.5;
                    stepCount = Double.Parse(temp_string[3].Trim(')'));
                    upperbound = Double.Parse(temp_string[2].Trim().Replace('~', '-').Trim('(').Trim(')'));
                    lowerbound = Double.Parse(temp_string[1].Trim().Replace('~', '-').Trim('(').Trim(')'));
                }

                else if (temp_string.Length == 3)
                {
                    scale = 0.5;
                    stepCount = 1;
                    upperbound = Double.Parse(temp_string[2].Trim().Replace('~', '-').Trim('(').Trim(')'));
                    lowerbound = Double.Parse(temp_string[1].Trim().Replace('~', '-').Trim('(').Trim(')'));
                }

                else if(temp_string.Length == 2)
                {
                    scale = 0.5;
                    stepCount = 1;
                    lowerbound = Double.Parse(temp_string[1].Trim().Replace('~', '-').Trim('(').Trim(')'));
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
                   Interpreter.plotNumbersX.Add(j);
                   Interpreter.plotNumbersY.Add(output);
                }
                /*foreach (double item in plotNumbersY)
                {
                    Console.WriteLine(item);
                }*/


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
                    this.updateVariable = "";
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
                    if (this.variable.Equals("vec"))
                    {
                        this.isVectorOperation = 1;
                        List<double> vecValues = new List<double>();
                        i = i + 1;
                        while (expr[i] != ')')
                        {


                            char c1 = expr[i];

                            if (c1.Equals('('))
                            {
                                i = i + 1;
                                continue;
                            }
                            else if (c1.Equals('#'))
                            {
                                i = i + 1;
                                if (c1.Equals('#'))
                                {
                                    continue;
                                }
                            }

                            else if (c1.Equals('~'))
                            {
                                number = "-";
                                if (expr.Length != i)
                                {
                                    i = i + 1;
                                }
                                if (Char.IsDigit(expr[i]))
                                {
                                    c1 = expr[i];
                                    while (Char.IsDigit(c1) || c.Equals('.'))
                                    {

                                        number = number + c1;
                                        i = i + 1;

                                        if (i < expr.Length)
                                        {
                                            c1 = expr[i];

                                        }
                                        else
                                        {
                                            break;
                                        }


                                    }
                                    if (expr[i].Equals(')'))
                                    {
                                        if (i < expr.Length)
                                        {
                                            vecValues.Add(double.Parse(number));
                                            i = i + 1;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        i = i - 1;
                                    }
                                    vecValues.Add(double.Parse(number));
                                }
                                else
                                {
                                    return "Invalid Expression";
                                }
                            }

                            else if (Char.IsDigit(c1))
                            {

                                number = "";
                                //conditions for multiple digit numbers
                                while (Char.IsDigit(c1) || c.Equals('.'))
                                {

                                    number = number + c1;
                                    i = i + 1;

                                    if (i < expr.Length)
                                    {
                                        c1 = expr[i];

                                    }
                                    else
                                    {
                                        break;
                                    }


                                }
                                i = i - 1;
                                vecValues.Add(double.Parse(number));
                            }
                            else if (Char.IsLetter(c1))
                            {
                                var = "";

                                while (Char.IsLetterOrDigit(c1) || c1.Equals('_'))
                                {
                                    var = var + c1;
                                    i = i + 1;
                                    if (i < expr.Length)
                                    {
                                        c1 = expr[i];

                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                i = i - 1;
                                vecValues.Add(Interpreter.variablesStored[var]);
                            }
                            else if (c1.Equals(')'))
                            {
                                // this.vectors.Add(vecValues);
                                continue;
                            }
                            else
                            {
                                return "Invalid Expression";
                            }
                            i = i + 1;
                        }
                        this.vectors.Add(vecValues);
                        //vecValues.Clear();
                        while (expr[i] == ')')
                        {
                            if (i != expr.Length - 1)
                            {
                                i = i + 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (expr[i] == ')')
                        {
                            i = i + 1;
                            continue;
                        }
                        i = i - 1;

                    }
                    else
                    {
                        this.variables.Add(this.variable);
                        if (!Interpreter.variablesStored.ContainsKey(this.variable))
                        {
                            //this.updateVariable = this.variable;
                            Interpreter.variablesStored[this.variable] = 0;
                        }
                        else
                        {
                            Operands.Push(Interpreter.variablesStored[this.variable]);
                        }
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
                else if (c.Equals(')') && this.isVectorOperation == 0)
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
                    if (Operations.Count != 0)
                    {
                        while (Operations.Peek() != '(')//check all the operators till '(' is reached
                        {
                            output = Calculate(Operands, Operations);
                            Operands.Push(output); //push result back to stack
                            if(Operations.Count ==0)
                            {
                                break;
                            }
                        }
                        if (Operations.Count != 0)
                        {
                            Operations.Pop();
                        }
                    }
                }

                else
                {
                    Operations.Push(c);
                }

            }
            if (this.isVectorOperation == 1)
            {
                string test = "";

                /*foreach(List<double> vector in this.vectors)
                {
                    foreach(var value in vector)
                        {
                        test = test + value.ToString();
                    }
                }
                */

                int dimensionOfArray = this.vectors[0].Count;
                foreach (List<double> vector in this.vectors)
                {

                    if (vector.Count != dimensionOfArray)
                    {
                        isDimensionSame = 0;
                        break;
                    }
                }
                if (isDimensionSame == 0)
                {
                    return "Dimension of Vectors is not same";
                }
                vectorsResult.Add(vectors[0]);
                vectors.RemoveAt(0);
                if (Operands.Count == 0)
                {
                    while (Operations.Count() != 0 && vectors.Count != 0)
                    {
                        char ch = Operations.Pop();
                        if (ch == '(')
                        {
                            //Operations.Pop();
                            continue;
                        }
                        if (ch.Equals('+'))
                        {
                            List<List<double>> tempvectorResults = new List<List<double>>(vectorsResult);
                            // tempvectorResults = vectorsResult.Clone();
                            //  var res = vectorsResult[0].Zip(vectors[0], (n, w) => new { reusltVec = n, vec = w });

                            int vecElement = 0;
                            /* foreach (var a in res)
                             {
                                 tempvectorResults[0][vecElement] = a.reusltVec + a.vec; //resultvec first vecotr vec//second veotr
                                 vecElement++;
                             }
                            */
                            int resCounter = 0, vecCounter = 0;
                            if (vectors.Count == 0 || vectorsResult.Count == 0)

                            {
                                return "Invalid Expression";
                            }
                            while (resCounter < dimensionOfArray && vecCounter < dimensionOfArray)
                            {
                                vectorsResult[0][resCounter] = vectorsResult[0][resCounter] + vectors[0][vecCounter];
                                resCounter++;
                                vecCounter++;
                            }
                            vectors.RemoveAt(0);
                            //vectorsResult = tempvectorResults;
                        }
                        else if (ch.Equals('-'))
                        {
                            List<List<double>> tempvectorResults = new List<List<double>>(vectorsResult);

                            //var res = vectorsResult[0].Zip(vectors[0], (n, w) => new { reusltVec = n, vec = w });
                            int vecElement = 0;
                            int resCounter = 0, vecCounter = 0;
                            if (vectors.Count == 0 || vectorsResult.Count == 0)

                            {
                                return "Invalid Expression";
                            }
                            while (resCounter < dimensionOfArray && vecCounter < dimensionOfArray)
                            {
                                vectorsResult[0][resCounter] = vectorsResult[0][resCounter] - vectors[0][vecCounter];
                                resCounter++;
                                vecCounter++;
                            }
                            vectors.RemoveAt(0);
                            vectorsResult = tempvectorResults;
                        }
                        else if (ch.Equals('.'))
                        {
                            double sum1 = 0;
                            int incCounter = 0;
                            if (vectors.Count == 0 || vectorsResult.Count == 0)

                            {
                                return "Invalid Expression";
                            }
                            while (incCounter < dimensionOfArray)
                            {
                                sum1 = sum1 + vectorsResult[0][incCounter] * vectors[0][incCounter];
                                incCounter++;
                            }
                            vectors.RemoveAt(0);
                            if (vectors.Count == 0)
                            {
                                return sum1.ToString();
                            }
                            Operands.Push(sum1);
                        }
                        else if (ch.Equals('*'))
                        {
                            double sum1 = 0;
                            int incCounter = 0;
                            if (vectors.Count == 0 || vectorsResult.Count == 0)

                            {
                                return "Invalid Expression";
                            }
                            if (dimensionOfArray == 3)
                            {
                                double firstRow = vectorsResult[0][0];
                                double secondRow = vectorsResult[0][1];
                                double thirdRow = vectorsResult[0][2];
                                vectorsResult[0][0] = secondRow * vectors[0][2] - thirdRow * vectors[0][1];
                                vectorsResult[0][1] = thirdRow * vectors[0][0] - firstRow * vectors[0][2];
                                vectorsResult[0][2] = firstRow * vectors[0][1] - secondRow * vectors[0][0];
                            }
                            else if (dimensionOfArray == 2)
                            {
                                vectorsResult[0][0] = vectorsResult[0][0] * vectors[0][1] - vectorsResult[0][0] * vectors[0][1];

                            }
                            vectors.RemoveAt(0);

                        }


                    }
                    return string.Join(",", vectorsResult[0]);
                }
                /*
                else
                {
                    while (Operations.Count() != 0 && vectors.Count != 0)
                    {
                        char ch = Operations.Pop();
                        if (ch == '*')
                        {
                            //Operations.Pop();
                            double num1 = 0;
                            int incCounter = 0;
                            if (Operands.Count() != 0)
                            {
                                num1 = Operands.Pop();
                            }
                            while (incCounter < dimensionOfArray)
                            {
                                vectorsResult[0][incCounter] = vectorsResult[0][incCounter] * num1;
                                incCounter++;
                            }
                        }
                        else
                        {
                            return "Invalid Expression";
                        }
                    }
                    return string.Join(",", vectorsResult[0]);

                }
                */
            }
            else
            {
                while (Operations.Count() != 0)
                {
                    output = Calculate(Operands, Operations);
                    Operands.Push(output);   //push final result back to stack
                }
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