using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinayWPF
{
    public class Nodes
    {
        public string Node1, Node2;



        public string NumberNode(string Node1)
        {
            this.Node1 = Node1;

            return this.Node1;
        }
        public string AddNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "(" + this.Node1 + "+" + this.Node2 + ")";
        }
        public string SubtractNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "(" + this.Node1 + "-" + this.Node2 + ")";
        }
        public string MultiplyNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "(" + this.Node1 + "*" + this.Node2 + ")";
        }
        public string DivideNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "(" + this.Node1 + "/" + this.Node2 + ")";
        }
        public string ExpononetNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "(" + this.Node1 + "^" + this.Node2 + ")";
        }
        public string AdditionNode(string Node1)
        {
            this.Node1 = Node1;

            return "(" + "+" + this.Node1 + ")";
        }
        public string SubtractionNode(string Node1)
        {
            this.Node1 = Node1;

            //  return "("+"-" + this.Node1+")";
            return "(" + "~" + this.Node1 + ")";
        }
        public string AssignmentNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return "(" + this.Node1 + "=" + this.Node2 + ")";
        }
        public string VariableNode(string Node1)
        {
            this.Node1 = Node1;


            //  return "("+"-" + this.Node1+")";
            return this.Node1;
        }
        public string LessThanOrEqual(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return "(" + this.Node1 + "<=" + this.Node2 + ")";
        }
        public string LessThan(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + "<" + this.Node2;
        }
        public string GreaterThanOrEqual(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + ">=" + this.Node2;
        }
        public string GreaterThan(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + ">" + this.Node2;
        }
        public string EqualityNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + "==" + this.Node2;
        }
        public string plotNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + this.Node2;
        }
        public string bisecNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return  this.Node1 + this.Node2 ;
        }
        public string diffNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 +"("+ this.Node2+")";
        }
        public string rootNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + this.Node2;
        }
        public string vecNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 +"(" +this.Node2 +")";
        }
        public string dotNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + "." + this.Node2 ;
        }
        public string crossNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + "&" + this.Node2;
        }
        public string seperatorNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return this.Node1 + "##" + this.Node2;
        }
        public string sqrtNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;

            //  return "("+"-" + this.Node1+")";
            return '('+this.Node1 + this.Node2+')';
        }
    }
}