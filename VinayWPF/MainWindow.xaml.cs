using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace VinayWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string input, output;
        //List<variable> Variables = new List<variable>();
       
        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewExecuted, CanNew));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenExecuted, CanOpen));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveExecuted, CanSave));
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("You want to create new file.");

           
        }

        private void CanNew(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

         

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("You want to open existing file.");
            string filename = "", contents="";
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            if (!txtExp.Items.IsEmpty)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Do you want to Save?", "Save?", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    this.save();
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    //do something else
                    txtExp.Items.Clear();


                }
            }
            else
            {
                txtExp.Items.Clear();
            }
            
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;
                contents = File.ReadAllText(filename);
                txtExp.Items.Add(contents);

            }
            MainWin.Title = System.IO.Path.GetFileNameWithoutExtension(filename); ;
        }

        private void CanOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("You want to save a file.");
            /*
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string fileText = "";
                string filename = dialog.FileName;
                
                
                for(int i =0; i< txtExp.Items.Count; i++)
                {
                    fileText = fileText + txtExp.Items[i].ToString() + "\n";
                }

                File.WriteAllText(filename, fileText);
                MainWin.Title = System.IO.Path.GetFileNameWithoutExtension(filename);

            }
            */
            this.save();
        }
        private void CanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void menu_clear(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Do you want to Clear?");
            txtExp.Items.Clear();
        }
        private void save()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string fileText = "";
                string filename = dialog.FileName;


                for (int i = 0; i < txtExp.Items.Count; i++)
                {
                    fileText = fileText + txtExp.Items[i].ToString() + "\n";
                }

                File.WriteAllText(filename, fileText);
                MainWin.Title = System.IO.Path.GetFileNameWithoutExtension(filename);

            }
        }
        private void menu_exit(object sender, RoutedEventArgs e)
        {
            if (!txtExp.Items.IsEmpty)
            {
                //MessageBox.Show("Do you want to Exit?");
                //SaveExecuted(sender, e);
                MessageBoxResult dialogResult = MessageBox.Show("Do you want to Save?", "Save?", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    this.save(); 
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    //do something else
                    MainWin.Close();
                }
            }
            else
            {
                MainWin.Close();
            }

           
        }

        
        private void btn_evaluate(object sender, RoutedEventArgs e)
        {


             

            string mathExp1 = txtName.Text.ToString();
            MainProg startingPoint = new MainProg();
            this.output = startingPoint.finalResult(this.input);

            if (!string.IsNullOrWhiteSpace(txtName.Text) && !txtExp.Items.Contains(txtName.Text))
            {
                txtExp.Items.Add(">" + this.input + "\n=" + this.output.ToString());
                txtName.Clear();
            }

            if (Interpreter.variablesStored.Count != 0)
            {
                List<variable> Variables = new List<variable>();
                foreach (var items in Interpreter.variablesStored)
                {
                    ///Variables.Clear();
                    Variables.Add(new variable(){ name = items.Key, value = items.Value });
                   
                    //VariableTable.Rows.Add(items.Key, items.Value);
                }
                VariableTable.AutoGenerateColumns = false;
                VariableTable.ItemsSource = Variables;
                //VariableTable.Style.;
            }

            int plotTrue = Lexer.plotTrue;

            if (plotTrue == 1)
            {
                Form1 f1 = new Form1();
                f1.Show();
            }
            //MyDictionary = Interpreter.variablesStored;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.input = txtName.Text.ToString();
            this.output = this.input;

        }

    }

    
    public class variable
    {
        public string name { get; set; }
        public double value { get; set; }
    }
    
}
    