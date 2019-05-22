using System;

namespace TärningsSimulator
{
    internal class Program
    {
        private static int _totalNumbersOfDicesFromUserInput;
        private static int _totalNumberOfTimesDicesWereThrown;

        private static void Main(string[] args)
        {
            MenuSwitch();
            Console.ReadKey();
        }

        private static void MenuSwitch()
        {
            var programIsRunning = true;

            do
            {
                PrintMenuSelection();
                var userInputMenuOptionSelection = Console.ReadLine();

                switch (userInputMenuOptionSelection)
                {
                    case "1":
                        StartGame();
                        break;
                    case "2":
                        programIsRunning = false;
                        break;
                    default:
                        PrintInvalidMenuSelectionInputValueMessage();
                        break;
                }
            } while (programIsRunning);
        }

        private static void PrintMenuSelection()
        {
            Console.WriteLine("\tVälkommen till tärningssimulatorn!");
            Console.WriteLine("\tSkapat av Adil Ahmad 19/05/2019\n");
            Console.WriteLine("\t[1] Spela");
            Console.WriteLine("\t[2] Avsluta");
            Console.Write("\tVälj: ");
        }

        private static void StartGame()
        {
            Console.Clear();
            if (UserGaveCorrectInputValuesForNumberOfDicesToBeThrownAccordingToTheRules())
            {
                GenerateDices();
            }
            Console.WriteLine("\n\tKlicka på nån knapp för att återvända till startmenyn");
            Console.ReadKey();
            Console.Clear();
        }

        private static bool UserGaveCorrectInputValuesForNumberOfDicesToBeThrownAccordingToTheRules()
        {
            Console.WriteLine("\tMinst 1 och max 5 tärningar");
            Console.Write("\tAntal tärningar: ");
            if (!int.TryParse(Console.ReadLine(), out _totalNumbersOfDicesFromUserInput) || _totalNumbersOfDicesFromUserInput < 1 || _totalNumbersOfDicesFromUserInput > 5)
            {
                Console.WriteLine("\n\tDu kan bara skriva ett tal med siffror!\n\tFår bara välja minst 1 eller max 5 tärningar!");
                return false;
            }

            return true;
        }

        private static void GenerateDices()
        {
            var random = new Random();
            _totalNumberOfTimesDicesWereThrown = 0;
            var totalSumOfAllDiceResults = 0;

            Console.WriteLine("\n\tOm någon tärning får resultatet 6 så adderas detta inte till summan,\n\t utan två nya tärningar kommet att slås. Detta kommer att meddelas när/om det sker.\n\n");

            for (var d = 1; d <= _totalNumbersOfDicesFromUserInput; d++)
            {
                var diceResult = random.Next(1, 7);
                if (diceResult == 6)
                {
                    totalSumOfAllDiceResults = GenerateTwoNewDices(random, totalSumOfAllDiceResults);
                    continue;
                }

                _totalNumberOfTimesDicesWereThrown++;
                totalSumOfAllDiceResults += diceResult;
                Console.WriteLine($"\t{_totalNumberOfTimesDicesWereThrown}|Tärningsslag: {diceResult}\t\tSumma av alla tärnings slag hittils: {totalSumOfAllDiceResults}");
            }

            Console.WriteLine("\t********************************************************************************");
            Console.WriteLine($"\tAntal tärningsslag: {_totalNumberOfTimesDicesWereThrown}\t\tTotal summa: {totalSumOfAllDiceResults}");
        }

        private static int GenerateTwoNewDices(Random random, int totalSumOfAllDiceResults)
        {
            Console.WriteLine("\n\t------------------------------------------------------------------------------");
            Console.WriteLine("\t\t En 6 har slagits nu kommer två nya täningsslags att generas!");
            for (var i = 0; i < 2; i++)
            {
                var diceResult = random.Next(1, 7);
                if (diceResult == 6)
                {
                    totalSumOfAllDiceResults = GenerateTwoNewDices(random, totalSumOfAllDiceResults);
                    continue;
                }

                _totalNumberOfTimesDicesWereThrown++;
                totalSumOfAllDiceResults += diceResult;
                Console.WriteLine($"\t{_totalNumberOfTimesDicesWereThrown}|Extra tärningsslag: {diceResult}\t\tSumma av alla tärnings slag hittils: {totalSumOfAllDiceResults}");
            }
            Console.WriteLine("\t\tTvå nya tärningar har kastas klart!!");
            Console.WriteLine("\t------------------------------------------------------------------------------\n");
            return totalSumOfAllDiceResults;
        }

        private static void PrintInvalidMenuSelectionInputValueMessage()
        {
            Console.Clear();
            Console.WriteLine("\tMata in en meny val '1' eller '2'.\n");
        }
    }
}
