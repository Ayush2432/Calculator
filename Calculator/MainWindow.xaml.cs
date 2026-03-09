using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		double lastnumber, currentnumber, result;
		SelectedOperator m_selectedoperator;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void acButton_Click(object sender, RoutedEventArgs e)
		{
			resultLabel.Content = "0";
			result = 0;
			lastnumber = 0;
		}

		private void negativeButton_Click(object sender, RoutedEventArgs e)
		{
			if (double.TryParse(resultLabel.Content.ToString(), out currentnumber))
			{
				currentnumber = currentnumber * -1;
				resultLabel.Content = currentnumber.ToString();
			}
		}

		#region Numbers Click events

		// so instead of writing the same code for each button, we can write it once and assign it to all number buttons
		private void numbersButton_Click(object sender, RoutedEventArgs e)
		{
			int selectednumber = 0;

			if(sender is Button button)
			{
				selectednumber = int.Parse(button.Content.ToString());
			}

			if(resultLabel.Content.ToString() == "0")
			{
				resultLabel.Content = $"{selectednumber}";
			}
			else
			{
				resultLabel.Content = $"{resultLabel.Content}{selectednumber}";
			}
		}
		#endregion


		// same for operations, we can write one event handler for all operations buttons and assign it to them
		#region Operations Events
		private void operationButton_Click(object sender, RoutedEventArgs e)
		{
			if(double.TryParse(resultLabel.Content.ToString(), out lastnumber))
			{
				resultLabel.Content = "0";
			}

			if(sender is Button button)
			{
				switch (button.Content.ToString())
				{
					case "+":
						m_selectedoperator = SelectedOperator.Addition;
						break;
					case "-":
						m_selectedoperator = SelectedOperator.Subtraction;
						break;
					case "*":
						m_selectedoperator = SelectedOperator.Multiplication;
						break;
					case "/":
						m_selectedoperator = SelectedOperator.Division;
						break;
				}
			}
		}

		private void decimalButton_Click(object sender, RoutedEventArgs e)
		{
			if(resultLabel.Content.ToString().Contains("."))
			{
				// do nothing if there is already a decimal point
			}
			else
			{
				resultLabel.Content = $"{resultLabel.Content}.";
			}
		}

		private void equalsButton_Click(object sender, RoutedEventArgs e)
		{
			double newnumber;
			if(double.TryParse(resultLabel.Content.ToString(), out newnumber))
			{
				switch (m_selectedoperator)
				{
					case SelectedOperator.Addition:
						result = lastnumber + newnumber;
						break;
					case SelectedOperator.Subtraction:
						result = lastnumber - newnumber;
						break;
					case SelectedOperator.Multiplication:
						result = lastnumber * newnumber;
						break;
					case SelectedOperator.Division:
						if (newnumber == 0)
						{
							MessageBox.Show("Cannot divide by zero", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
							result = 0;
							break;
						}
						result = lastnumber / newnumber;
						break;
				}
				resultLabel.Content = result.ToString();
			}
		}
		private void percentageButton_Click(object sender, RoutedEventArgs e)
		{
			double tempnumber;
			if (double.TryParse(resultLabel.Content.ToString(), out tempnumber))
			{
				tempnumber = tempnumber / 100;
				if(lastnumber != 0)
				{
					tempnumber = tempnumber * lastnumber;
				}

				resultLabel.Content = tempnumber.ToString();
			}
		} 
		#endregion
	}

    public enum SelectedOperator
	{
		Addition,
		Subtraction,
		Multiplication,
		Division
	}
}