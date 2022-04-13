using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private bool isDecimal;
        private bool isOperator;
        private bool isNegative;
        private string equation;

        private void NumericClicked(object sender, EventArgs e)
        {
            try
            {
                var buttonNum = sender as Button;
                double output = Convert.ToDouble(Output.Text + 1);
                isOperator = false;

                if (Output.Text != "0")
                {
                    Output.Text += buttonNum.Text;
                    equation += buttonNum.Text;
                }
                else
                {
                    Output.Text = buttonNum.Text;
                    equation += buttonNum.Text;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
            // History.Text = equation; *For debugging purposes only
        }


        private void DotClicked(object sender, EventArgs e)
        {
            try
            {
                var buttonDot = sender as Button;
                if (!isDecimal)
                {
                    if (Output.Text.Length == 1 && Output.Text == "0")
                    {
                        equation += "0" + buttonDot.Text;
                    }
                    else
                    {
                        equation += buttonDot.Text;
                    }
                    isDecimal = true;
                    Output.Text += buttonDot.Text;

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
            // History.Text = equation; *For debugging purposes only

        }


        private void ClearClicked(object sender, EventArgs e)
        {
            try
            {
                Output.Text = "0";
                equation = "";
                isDecimal = false;
                isOperator = false;
                isNegative = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
            // History.Text = equation; *For debugging purposes only

        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (Output.Text != "0")
                {
                    if (equation[equation.Length - 1] == '.')
                    {
                        isDecimal = false;
                    }

                    if (equation[equation.Length - 1] == '-')
                    {
                        isNegative = false;
                    }

                    if (Output.Text.Length == 1)
                    {
                        Output.Text = "0";
                    }
                    else
                    {
                        Output.Text = Output.Text.Remove(Output.Text.Length - 1, 1);
                    }
                    equation = equation.Remove(equation.Length - 1, 1);
                }

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
            // History.Text = equation; *For debugging purposes only

        }

        private void OperatorClicked(object sender, EventArgs e)
        {
            var buttonOperator = sender as Button;
            string operation = buttonOperator.Text.ToString();
            if (operation == "x") operation = "*";

            try
            {
                isDecimal = false;

                if (equation.Length == 0)
                {
                    equation += "0";
                }

                if (!isOperator)
                {

                    if (operation == "-" && Output.Text == "0")
                    {
                        if (!isNegative)
                        {
                            Output.Text = "-";
                            isNegative = true;
                            equation += operation;
                        }
                    }
                    else
                    {
                        isOperator = true;
                        equation += operation;
                        Output.Text = "0";
                    }
                }
                else
                {
                    equation = equation.Remove(equation.Length - 1, 1);
                    equation += operation;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
            // History.Text = equation; *For debugging purposes only

        }

        private void EqualsClicked(object sender, EventArgs e)
        {
            try
            {
                DataTable solver = new DataTable();
                double res;

                if (isDecimal)
                {
                    var result = solver.Compute(equation, "");
                    res = Convert.ToDouble(result);
                }
                else
                {
                    var result = solver.Compute(equation + ".0", "");
                    res = Convert.ToDouble(result);
                }

                Output.Text = res.ToString();
                isOperator = false;
                equation = Output.Text;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
            // History.Text = equation; *For debugging purposes only

        }
    }
}
