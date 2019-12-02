using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Monsters
{
    // **************************************************
    //
    // Title: Monsters
    // Description: Demonstration of classes and objects
    // Author: Radtke, Jacob
    // Dated Created: 11/11/2019
    // Last Modified: 12/1/2019
    //
    // **************************************************    
    class Program
    {
        static void Main(string[] args)
        {
            //
            // initialize monster list
            //
            //List<Monster> monsters = InitializeMonsterList();
            List<Monster> monsters = ReadMonsterList();

            //
            // call methods
            //
            DisplayMenuScreen(monsters);

        }

        static List<Monster> InitializeMonsterList()
        {
            //List<int> myInts = new List<int>();
            //myInts.Add(3);
            //myInts.Add(22);

            //List<int> yourInts = new List<int>()
            //{
            //    3,
            //    22
            //};

            //Monster myMonster = new Monster();
            //myMonster.Name = "John";
            //myMonster.Age = 44;
            //myMonster.Attitude = Monster.EmotionalState.angry;

            //Monster yourMonster = new Monster()
            //{
            //    Name = "John",
            //    Age = 44,
            //    Attitude = Monster.EmotionalState.angry
            //};

            //
            // create a list of monsters
            //
            List<Monster> monsters = new List<Monster>()
            {

                new Monster()
                {
                    Name = "Sid",
                    Age = 145,
                    Attitude = Monster.EmotionalState.happy
                },

                new Monster()
                {
                    Name = "Lucy",
                    Age = 125,
                    Attitude = Monster.EmotionalState.bored
                },
                new Monster("Fred", 210, Monster.EmotionalState.angry)
            };

            Console.WriteLine(monsters[0]);

            return monsters;
        }
        static List<Monster> ReadMonsterList()
        {
            List<Monster> monsters = new List<Monster>();
            //
            // read all m,onsters into string array
            //
            string[] monstersString = File.ReadAllLines("Data\\Data.txt");

            //
            // create monster list
            //
            string[] monsterProperties;
            foreach (string monster in monstersString)
            {
                monsterProperties = monster.Split(',');

                Monster newMonster = new Monster();
                newMonster.Name = monsterProperties[0];
                int.TryParse(monsterProperties[0], out int age);
                newMonster.Age = age;

                Enum.TryParse(monsterProperties[2], out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;

                monsters.Add(newMonster);
             
            }



            return monsters;
        }

        static void WriteMonsterList(List<Monster> monsters)
        {
            //
            // create array of string
            //
            
            string monsterString;
            string[] monstersString = new string[monsters.Count];
            for (int index = 0; index < monsters.Count; index++)
            {
                monsterString =
                   monsters[index].Name + "," +
                   monsters[index].Age + "," +
                   monsters[index].Attitude;

                monstersString[index] = monsterString;
            }

            //
            // write array to data file
            //
            File.WriteAllLines("Data\\Data.txt", monstersString);
           
        }

        static void DisplayWriteToDataFile(List<Monster> monsters)
        {
            DisplayScreenHeader("Write to Data File");

            //warn the user about the operation
            DisplayContinuePrompt();

            WriteMonsterList(monsters);

            Console.WriteLine();
            Console.WriteLine("Data Written correctly");


            DisplayContinuePrompt();
        }

        static void DisplayMenuScreen(List<Monster> monsters)
        {
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("a) List All Monsters");
                Console.WriteLine("b) View Monster Detail");
                Console.WriteLine("c) Add Monster");
                Console.WriteLine("d) Delete Monster");
                Console.WriteLine("e) Update Monster");
                Console.WriteLine("f) Write Monsters to Data File ");
                Console.WriteLine("q) Quit");
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayAllMonsters(monsters);
                        break;

                    case "b":
                        DisplayViewMonsterDetail(monsters);
                        break;

                    case "c":
                        DisplayAddMonster(monsters);
                        break;

                    case "d":
                        DisplayDeleteMonster(monsters);
                        break;

                    case "e":
                        DisplayUpdateMonster(monsters);
                        break;

                    case "f":
                        DisplayWriteToDataFile(monsters);
                        break;

                    case "g":
                        DisplayFilterByAttitude(monsters);
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }


            } while (!quitApplication);
        }

        static void DisplayFilterByAttitude(List<Monster> monsters)
        {
            List<Monster> filterMonsters = new List<Monster>();
            Monster.EmotionalState selectedAttitude = Monster.EmotionalState.happy;

            DisplayScreenHeader("Filter by Attitude");

            //
            // add monsters with the selected attitude to a new list
            //
            foreach (Monster monster in monsters)
            {
                if (monster.Attitude == selectedAttitude)
                {
                    filterMonsters.Add(monster);
                }


            }
            filterMonsters = monsters.Where(m => m.Attitude == selectedAttitude).ToList();
            filterMonsters = filterMonsters.OrderBy(m => m.Name).ToList();
            //
            //display new list
            //

            Console.WriteLine($"{selectedAttitude} Monsters");
            Console.WriteLine("\t***************************");
            foreach (Monster monster in filterMonsters)
            {
                MonsterInfo(monster);
                Console.WriteLine();
                Console.WriteLine("\t***************************");
            }



            DisplayContinuePrompt();
        }
        static void DisplayUpdateMonster(List<Monster> monsters)
        {
            bool validResponse = false;
            Monster selectedMonster = null;

            do
            {
                DisplayScreenHeader("Update Monster");

                //
                // display all monster names
                //
                Console.WriteLine("\tMonster Names");
                Console.WriteLine("\t-------------");
                foreach (Monster monster in monsters)
                {
                    Console.WriteLine("\t" + monster.Name);
                }

                //
                // get user monster choice
                //
                Console.WriteLine();
                Console.Write("\tEnter name:");
                string monsterName = Console.ReadLine();

                //
                // get monster object
                //

                foreach (Monster monster in monsters)
                {
                    if (monster.Name == monsterName)
                    {
                        selectedMonster = monster;
                        validResponse = true;
                        break;
                    }
                }

                //
                // feedback for wrong name choice
                //
                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select a correct name.");
                    DisplayContinuePrompt();
                }

                //
                // update monster
                //


            } while (!validResponse);


            //
            // updata monster
            //
            string userResponse;
            Console.WriteLine("\tReading to update. Press enter to keep the current info.");
            Console.Write($"\tCurrent Name: {selectedMonster.Name} New Name: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                selectedMonster.Name = userResponse;
            }

            Console.Write($"\tCurrent Age: {selectedMonster.Age} New Age: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                int.TryParse(userResponse, out int age);
                selectedMonster.Age = age;
            }

            Console.Write($"\tCurrent Attitude: {selectedMonster.Attitude} New Attitude: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                Enum.TryParse(userResponse, out Monster.EmotionalState attitude);
                selectedMonster.Attitude = attitude;
            }



            DisplayContinuePrompt();
        }

        static void DisplayDeleteMonster(List<Monster> monsters)
        {
            DisplayScreenHeader("Delete Monster");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // delete monster
            //
            if (selectedMonster != null)
            {
                monsters.Remove(selectedMonster);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedMonster.Name} deleted");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{monsterName} not found");
            }

            DisplayContinuePrompt();
        }

        static void DisplayViewMonsterDetail(List<Monster> monsters)
        {
            DisplayScreenHeader("Monster Detail");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // display monster detail
            //
            Console.WriteLine();
            Console.WriteLine("\t*********************");
            MonsterInfo(selectedMonster);
            Console.WriteLine("\t*********************");

            DisplayContinuePrompt();
        }

        static void DisplayAddMonster(List<Monster> monsters)
        {
            Monster newMonster = new Monster();

            DisplayScreenHeader("Add Monster");

            //
            // add monster object property values
            //
            Console.Write("\tName: ");
            newMonster.Name = Console.ReadLine();
            Console.Write("\tAge: ");
            int.TryParse(Console.ReadLine(), out int age);
            newMonster.Age = age;
            Console.Write("\tAttitude: ");
            Enum.TryParse(Console.ReadLine(), out Monster.EmotionalState attitude);
            newMonster.Attitude = attitude;

            //
            // echo new monster properties
            //
            Console.WriteLine("\tNew Monster's Properties");
            MonsterInfo(newMonster);
            DisplayContinuePrompt();

            monsters.Add(newMonster);
        }

        static void DisplayAllMonsters(List<Monster> monsters)
        {
            DisplayScreenHeader("All Monsters");

            Console.WriteLine("\t***************************");
            foreach (Monster monster in monsters)
            {
                MonsterInfo(monster);
                Console.WriteLine();
                Console.WriteLine("\t***************************");
            }

            DisplayContinuePrompt();
        }

        static void MonsterInfo(Monster monster)
        {
            Console.WriteLine($"\tName: {monster.Name}");
            Console.WriteLine($"\tAge: {monster.Age}");
            Console.WriteLine($"\tAttitude: {monster.Attitude}");
            Console.WriteLine("\t" + monster.Greeting());
        }

        #region HELPER METHODS

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Calculator");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Calculator!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
