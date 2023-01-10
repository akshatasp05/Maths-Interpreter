using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace VinayWPF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            int i = 0;
            //Interpreter interpreter = new Interpreter();
            List<double> xArr = Interpreter.plotNumbersX;
            List<double> yArr = Interpreter.plotNumbersY;

            OxyPlot.WindowsForms.PlotView pv = new PlotView();
            pv.Location = new Point(0,0);
            pv.Size = new Size(900, 600);
            this.Controls.Add(pv);

            pv.Model = new PlotModel {Title = "Equation Plot" };
            
            
            LinearAxis xaxis = new LinearAxis();
            LinearAxis yaxis = new LinearAxis();

            yaxis.PositionAtZeroCrossing = true;
            yaxis.MajorGridlineStyle = LineStyle.Dot;

            xaxis.Position = AxisPosition.Bottom;
            xaxis.MajorGridlineStyle = LineStyle.Dot;
            pv.Model.Axes.Add(xaxis);
            pv.Model.Axes.Add(yaxis);

            AreaSeries fs= new AreaSeries();
            pv.Model.Series.Clear();
            while (i <= xArr.Count - 1)
            {
               
                fs.Points.Add(new DataPoint(xArr[i], yArr[i]));
                
                i += 1;
            }
            
            //fs.Points.Add(new DataPoint(1, 3));
            //fs.Points.Add(new DataPoint(3, 6));
            //fs.Points.Add(new DataPoint(4, 8));
            pv.Model.Series.Add(fs);


        }
    }
}
