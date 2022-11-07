using System;
using System.Collections.Generic;
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
        string input,output;
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
            MessageBox.Show("You want to open existing file.");
        }

        private void CanOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("You want to save a file.");
        }
        private void CanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void menu_clear(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Do you want to Clear?");
        }

        private void menu_exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Do you want to Exit?");
        }

        private void btn_evaluate(object sender, RoutedEventArgs e)
        {
            string mathExp1 = txtName.Text.ToString();
            MainProg startingPoint = new MainProg();
            this.output = startingPoint.finalResult(this.input);

            if (!string.IsNullOrWhiteSpace(txtName.Text) && !txtExp.Items.Contains(txtName.Text))
            {
                txtExp.Items.Add(">"+this.input + "\n=" + this.output.ToString());
                txtName.Clear();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.input= txtName.Text.ToString();
            this.output = this.input;

        }
    }
}
