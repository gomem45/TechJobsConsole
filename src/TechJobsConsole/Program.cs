

using System;
using System.Collections.Generic;
using System.Text;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        Console.WriteLine("Search all fields not yet implemented.");
                    }
                    else
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string input = Console.ReadLine();
                choiceIdx = int.Parse(input);

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }


        // Since the Job Lists that are stored in a CSV file are 
        // retrieved in a Dictionary list, and the new field added 
        // to the job record cannot change PrintJobs function.
        //
        // Reading documentation how to implements StringBuilder method
        // https://www.tutorialsteacher.com/csharp/csharp-stringbuilder
        //
        // A StringBuilder method is needed to add different search
        // results. As the user is typing a TechJob menu selection
        public static StringBuilder StoreResults(StringBuilder selection, int numChars)
        {
            if (selection.Length == numChars)
            {
                return selection;
            }
            else
            {
                return StoreResults(selection.Append(" "), numChars);
            }
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            //Console.WriteLine("printJobs is not implemented yet");

            // If there is more than one 
            // Job postition
            if (someJobs.Count != 0)
            {   
                // Loop though each Job using the key, value stored
                // in the Dictionary method, and pass the search result
                foreach (Dictionary<string, string> listJob in someJobs)
                {
                    Console.WriteLine("********************");

                    // Loop though the User menu selection
                    foreach (string key in listJob.Keys)
                    {
                        // Create a StringBuilder to store the search
                        // results. 
                        StringBuilder jobResults = new StringBuilder();

                        // First grab the key (selection) that has been passed. 
                        jobResults.Append(key);

                        // Then use the new StringBuilder object to store 
                        // search results that helper function returns which
                        // includes the search term and the maximum characters
                        jobResults = StoreResults(jobResults, 30);
                        Console.WriteLine(jobResults + " : " + listJob[key]);
                    }

                    // Separate each Job result
                    Console.WriteLine("\n");
                }

                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("No Search results to displsay!"); ;
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
