using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNum, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNum, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.Substraction(lastNum, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNum, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNum, newNumber);
                        break;
                }
                resultLabel.Content = newNumber == 0 && selectedOperator == SelectedOperator.Division ?
                                                                            "Błąd" : result.ToString();
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber /= 100;
                if (lastNum != 0)
                    tempNumber *= lastNum;
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNum))
            {
                lastNum *= -1;
                resultLabel.Content = lastNum.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNum = 0;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultLabel.Content.ToString() != "Błąd")
            {
                var number = sender as Button;

                var value = number.Content.ToString();

                if (resultLabel.Content.ToString() == "0")
                    resultLabel.Content = value;
                else
                    resultLabel.Content += value;
            }
        }

        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains(",") && resultLabel.Content.ToString() != "Błąd")
                resultLabel.Content += ",";
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNum))
            {
                resultLabel.Content = "0";
            }

            if (sender == multButton)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == divButton)
                selectedOperator = SelectedOperator.Division;
            if (sender == plusButton)
                selectedOperator = SelectedOperator.Addition;
            if (sender == subsButton)
                selectedOperator = SelectedOperator.Substraction;

        }

    }
}
