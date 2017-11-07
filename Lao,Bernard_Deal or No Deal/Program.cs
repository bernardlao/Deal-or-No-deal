using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lao_Bernard_Assignmentno8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Deal or No Deal!!!!");
            while (true)
            {
                Console.Write("Do you want to play? Yes or No: ");
                string response = Console.ReadLine();
                if (response.Equals("YES", StringComparison.CurrentCultureIgnoreCase))
                {
                    DealorNoDeal();
                    Thread.Sleep(1000);
                    Console.WriteLine("Thank you for playing");
                    break;
                }
                else if (response.Equals("NO", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("Bye!");
                    break;
                }
                else
                    Console.WriteLine("Wrong input. choose between YES or NO.");
            }
        }
        public static void DealorNoDeal()
        {//Main Game
            int x = 0;
            string[] bCase = BriefCase();
            bool[] opened = new bool[26];
            Console.WriteLine("Please choose your briefcase from 1 to 26");
            int choice = CheckChoice();
            string playersCase = bCase[choice - 1];
            bCase[choice - 1] = "Player's Choice";
            opened[choice - 1] = true;
            for (int i = 0; i < bCase.Length; i++)
                Console.WriteLine((i + 1) + " = " + (opened[i] ? bCase[i] : "?"));
            while (true)
            {
                Console.Write("Choose " + (6 - x) + " briefcase  to open one by one: ");
                for (int i = 0; i < 6 - x; i++)
                {
                    int choice2 = 0;
                    while (true)
                    {
                        choice2 = CheckChoice();
                        if (bCase[choice2 - 1].Equals("Player's Choice"))
                            Console.WriteLine("You cannot open your chosen briefcase yet. Try again: ");
                        else if (opened[choice2 - 1] == true)
                            Console.WriteLine("You already opened that case. Choose again: ");
                        else
                            break;
                    }
                    Console.Clear();
                    opened[choice2 - 1] = true;
                    for (int j = 0; j < bCase.Length; j++)
                        Console.WriteLine((j + 1) + " = " + (opened[j] ? bCase[j] : "?"));
                    string reaction = RandomReaction(bCase[choice2 - 1].ToString());//reaction here
                    Console.WriteLine(reaction);
                }
                string deal = BankersDeal(opened, bCase, playersCase);
                if (deal.Contains('-'))
                {
                    string[] casesLeft = deal.Split('-');
                    Console.WriteLine("There are two case left.\nPress 1 to choose your case\nPress 2 to choose the other one");
                    Console.Write("Decide wisely: ");
                    while (true)
                    {
                        string which = Console.ReadLine();
                        if (which == "1")
                        {
                            Console.Clear();
                            Thread.Sleep(1000);
                            Console.WriteLine("You choose your case, you  won " + casesLeft[0] + "\nThe other case contains " + casesLeft[1]);
                            break;
                        }
                        else if (which == "2")
                        {
                            Console.Clear();
                            Thread.Sleep(1000);
                            Console.WriteLine("You choose the other case you won " + casesLeft[1] + "\nYour first case contains " + casesLeft[0]);
                            break;
                        }
                        else
                            Console.Write("You can only choose between 1 and 2: ");
                    }
                    break;
                }
                Console.WriteLine("Riiiiiiiiiiinnnggggg!!!!!!!!"); Thread.Sleep(1000);
                Console.WriteLine("\nHello, banker....."); Thread.Sleep(1000);
                Console.WriteLine("\nOkay. Thank you banker"); Thread.Sleep(1000);
                Console.WriteLine("\nThe banker offer you " + deal);
                string response = OfferResponse();
                if (response.Equals("Deal"))
                {
                    Console.WriteLine("You take the banker's offer and won " + deal); Thread.Sleep(1000);
                    Console.WriteLine("Your case contains: " + playersCase); Thread.Sleep(1000);
                    break;
                }
                else
                {
                    if (x < 5)
                        x++;
                }
            }
        }
        public static string BankersDeal(bool[] opened, string[] bCase, string playersCase)
        {//Banker
            string temp = playersCase;
            for (int i = 0; i < bCase.Length; i++)
                if (opened[i] == false)
                    temp += "." + bCase[i];
            string[] caseLeftStr = temp.Split('.');
            if (caseLeftStr.Length == 2)
                return temp = caseLeftStr[0] + "-" + caseLeftStr[1];
            for (int i = 0; i < caseLeftStr.Length; i++)
                caseLeftStr[i] = caseLeftStr[i].Replace(",", "");

            int[] caseLeftInt = new int[caseLeftStr.Length];
            for (int i = 0; i < caseLeftInt.Length; i++)
                caseLeftInt[i] = Convert.ToInt32(caseLeftStr[i]);

            int num1 = caseLeftInt.Max();
            int num2 = caseLeftInt.Min();
            string decision = (((num1 - num2) / 2) + num2).ToString();
            if (Convert.ToInt32(decision) >= 1000000)
                decision = "700000";
            if (decision.Length > 3)
            {
                if (decision.Length > 6)
                    decision = decision.Insert(decision.Length - 6, ",");
                decision = decision.Insert(decision.Length - 3, ",");
            }
            return decision;
        }
        public static string OfferResponse()
        {
            Console.Write("Is it a Deal or No Deal: ");
            while (true)
            {
                string response = Console.ReadLine();
                if (response.Equals("DEAL", StringComparison.CurrentCultureIgnoreCase))
                    return response = "Deal";
                else if (response.Equals("NO DEAL", StringComparison.CurrentCultureIgnoreCase))
                    return response = "No";
                else
                    Console.Write("You can only choose between Deal or No deal. Try again: ");
            }
        }
        public static int CheckChoice()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if ((choice < 27) && (choice > 0))
                    {
                        return choice;
                    }
                    else
                        Console.WriteLine("Invalid input you can only choose 1 to 26: ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input you can only choose a number from 1 to 26");
                }
            }
        }
        public static string[] BriefCase()
        {
            Random r = new Random();
            string[] caseAmount = { "2,000,000", "1,000,000", "750,000", "500,000", "400,000", "300,000", "200,000", "100,000", "75,000", "50,000", "40,000", "30,000", "20,000", "10,000", "5,000", "2,500", "2,000", "1,750", "1,500", "1,250", "1,000", "750", "500", "100", "10", "1" };
            int[] randomNum = new int[26];
            string[] bCase = new string[26];
            for (int i = 0; i < randomNum.Length; i++)
            {
                int num = r.Next(1, 27);
                if (IsExist(randomNum, num))
                    i--;
                else
                    randomNum[i] = num;
            }
            for (int i = 0; i < randomNum.Length; i++)
                bCase[i] = caseAmount[randomNum[i] - 1];
            return bCase;
        }
        public static bool IsExist(int[] randomNum, int num)
        {
            for (int i = 0; i < randomNum.Length; i++)
            {
                if (randomNum[i] == num)
                    return true;
            }
            return false;
        }
        public static string RandomReaction(string valueOfCase)
        {
            int value = Convert.ToInt32(valueOfCase.Replace(",",""));
            if (value < 100000)
                valueOfCase = "\nNice. You open the case with lesser amount";
            else if (value >= 100000 && value <= 500000)
                valueOfCase = "\nNot bad.";
            else if (value > 500000 && value < 1000000)
                valueOfCase = "\nOh my, I think this is not your lucky day.";
            else if (value == 1000000)
                valueOfCase = "\nOh sorry, you opened the second to the biggest amount";
            else if (value == 2000000)
                valueOfCase = "\nMy god. You already opened the biggest amount.";
            return valueOfCase;
        }
    }
}
