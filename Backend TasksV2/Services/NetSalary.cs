using Backend.Helpers;
using System;

namespace Backend.Services
{
    public class NetSalary : INetSalary
    {
        //We could use reflection instead but can be slow, so we use a feature included in 4.5 or beyond using CallerFilePath attribute
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public void CalculateNetSalary()
        {
            //Get gross salary input from user
            double grossSalary = 0;
            bool grossSalary_correctInput = false;
            do
            {
                Console.Write("Please enter gross salary: ");
                grossSalary_correctInput = Double.TryParse(Console.ReadLine(), out grossSalary);
                if (!grossSalary_correctInput)
                {
                    Console.WriteLine("Please enter a correct number.");
                    log.Error("User entered an invalid input as gross salary.");
                }
            } while (!grossSalary_correctInput);

            Console.WriteLine();
            log.Debug($"User entered {grossSalary}(IDR) as gross salary.");

            double totalTax = 0;
            if (grossSalary <= 1000)
            {
                log.Debug("No tax since there is no taxation for any amount lower or equal to 1000 Imaginaria Dolars (IDR)");
            }
            else
            {
                log.Debug("Tax shall be calculated since gross salary is greater than 1000 Imaginaria Dolars (IDR)");

                double incomeTax = GetIncomeTax(grossSalary);
                log.Debug($"Calculated income tax of {incomeTax} (IDR)");
                totalTax += incomeTax;

                double socialContributions = GetSocialContributions(grossSalary);
                log.Debug($"Calculated social contributions of {socialContributions} (IDR)");
                totalTax += socialContributions;

                /*Add more taxes if required below this line following the examples above using the same structure
                 *
                 *double %tax = GetTax(%taxName, grossSalary, %percentage, %maxCap, %minCap);
                 *log.Debug(%taxMessage);
                 *totalTax += %tax;
                 * 
                 * Examples from Task 2
                 * double incomeTax = GetTax("income tax", grossSalary, 10, grossSalary, 1000);
                 * double socialContributions = GetTax("social contribution", grossSalary, 15, 3000, 1000);
                 */
            }

            double netSalary = grossSalary - totalTax;
            Console.WriteLine($"Total tax = {totalTax}(IDR)");
            Console.WriteLine($"Calculated net salary of {netSalary} (IDR)\n");
            log.Debug($"Total tax = {totalTax}(IDR)");
            log.Debug($"Calculated net salary of {netSalary} (IDR)");

            Console.Write("Press any key to continue . . .");
            Console.ReadKey();
        }

        private double GetIncomeTax(double grossSalary)
        {
            //Income tax of 10% is incurred to the excess (amount above 1000)
            double excessIncome = grossSalary - 1000;
            double incomeTax = 0.10 * excessIncome;
            Console.WriteLine($"10% tax out of {excessIncome}(IDR) => {incomeTax}(IDR)");
            log.Debug($"10% tax out of {excessIncome}(IDR) => {incomeTax}(IDR)");
            return incomeTax;
        }

        private double GetSocialContributions(double grossSalary)
        {
            //Social contributions of 15 % are expected to be made as well.As for the previous case, 
            //the taxable income is whatever is above 1000 IDR but social contributions never apply to amounts higher than 3000.

            //There is a salary cap for the social contribution, get the minimum of grossSalary or 3000
            double contributionAmount = Math.Min(grossSalary, 3000) - 1000;
            double socialContribution = 0.15 * contributionAmount;
            Console.WriteLine($"15% tax out of {contributionAmount}(IDR) => {socialContribution}(IDR)");
            log.Debug($"15% tax out of {contributionAmount}(IDR) => {socialContribution}(IDR)");

            return socialContribution;
        }

        //For further taxes addition a general method can be implemented - Take this as an example
        private double GetTax(string taxName, double grossSalary, double percentage, double maxCap, double minCap = 0)
        {
            //Minimum capacity shall be greater or equal than 0
            minCap = Math.Max(minCap, 0);

            //contributionAmount is the amount of IDR for the tax
            double contributionAmount = Math.Min(grossSalary, maxCap) - minCap;

            double tax = percentage / 100.0 * contributionAmount;

            Console.WriteLine($"{percentage}% {taxName} out of {contributionAmount}(IDR) => {tax}(IDR)");
            log.Debug($"{percentage}% {taxName} out of {contributionAmount}(IDR) => {tax}(IDR)");

            return tax;
        }
    }
}
