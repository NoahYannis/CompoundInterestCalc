using System;

namespace CompoundInterestCalc
{
    public static class CompoundInterestCalculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Compound Interest Calculator --------------------------------------" + "\n");
            CalculatorModel calculatorModel = GetUserInput();

            Console.WriteLine();
            Console.Write($"In {calculatorModel.NumberOfYears} years and with an interest rate of {calculatorModel.InterestRate * 100}%, " +
                $"your initial sum of ${calculatorModel.StartingSum} will be worth $" + CalculateCompoundInterestSum(calculatorModel));

            Console.ReadKey();
        }

        public static CalculatorModel GetUserInput()
        {
            CalculatorModel calculatorModel = new CalculatorModel();

            Console.Write("Enter the starting sum: ");
            calculatorModel.StartingSum = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter monthly contributions: ");
            calculatorModel.MonthlyContribution = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the interest rate: ");
            calculatorModel.InterestRate = Convert.ToInt32(Console.ReadLine()) * 0.01;

            Console.Write("Enter the number of years: ");
            calculatorModel.NumberOfYears = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Do you want to consider inflation? (1) Yes | (2) No ");
            ConsoleKeyInfo considerInflation = Console.ReadKey(true);

            if (considerInflation.Key == ConsoleKey.D1)
            {
                calculatorModel.ConsiderInflation = true;
                Console.Write("     Enter inflation rate: ");
                calculatorModel.InflationRate = Convert.ToInt32(Console.ReadLine()) * 0.01;
            }

            Console.WriteLine("Do you want to consider taxes? (1) Yes | (2) No ");
            ConsoleKeyInfo considerTaxes = Console.ReadKey(true);

            if (considerTaxes.Key == ConsoleKey.D1)
            {
                calculatorModel.ConsiderTaxes = true;
                Console.Write("     Enter tax rate: ");
                calculatorModel.TaxRate = Convert.ToInt32(Console.ReadLine()) * 0.01;

            }

            return calculatorModel;
        }

        public static string CalculateCompoundInterestSum(CalculatorModel calculatorModel)
        {
            string result = string.Empty;

            double monthlyInterestRate = calculatorModel.InterestRate / 12;
            int totalMonths = calculatorModel.NumberOfYears * 12;

            if (calculatorModel.ConsiderInflation)
            {
                if (calculatorModel.ConsiderTaxes)
                {
                    double sum = (calculatorModel.StartingSum * Math.Pow(1 + calculatorModel.InterestRate - calculatorModel.InflationRate, calculatorModel.NumberOfYears)
                        + calculatorModel.MonthlyContribution * ((Math.Pow(1 + monthlyInterestRate, totalMonths) - 1) / monthlyInterestRate)) * (1 - calculatorModel.TaxRate);
                    return sum.ToString("N2");
                }

                return (calculatorModel.StartingSum * Math.Pow(1 + calculatorModel.InterestRate - calculatorModel.InflationRate, calculatorModel.NumberOfYears)
                    + calculatorModel.MonthlyContribution * ((Math.Pow(1 + monthlyInterestRate, totalMonths) - 1) / monthlyInterestRate)).ToString("N2");
            }
            else if (calculatorModel.ConsiderTaxes)
            {
                double sum = (calculatorModel.StartingSum * Math.Pow(1 + calculatorModel.InterestRate - calculatorModel.InflationRate, calculatorModel.NumberOfYears) 
                    + calculatorModel.MonthlyContribution * ((Math.Pow(1 + monthlyInterestRate, totalMonths) - 1) / monthlyInterestRate)) * (1 - calculatorModel.TaxRate);
                return sum.ToString("N2");
            }
            else
            {

                double calc = calculatorModel.StartingSum * Math.Pow(1 + calculatorModel.InterestRate, calculatorModel.NumberOfYears)
                    + calculatorModel.MonthlyContribution * ((Math.Pow(1 + monthlyInterestRate, totalMonths) - 1) / monthlyInterestRate);
                return calc.ToString("N2");
            }
        }
    }
}
