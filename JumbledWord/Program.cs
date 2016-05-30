using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
namespace Inoracia
{
    class Program
    {
        private static StreamWriter writeToTextFile = new StreamWriter("Results.txt");
        private static string messenger = "";
        /*
        private static string myDictionary = "CHEESE";
        
        private static char[] conDict = myDictionary.ToCharArray();
         
        private static string[] dictionary = {"FISHERMAN", "FOOD", "FOOL", "SHOE"};
        */
        static void Main(string[] args)
        {
            try
            {


                Console.WriteLine("Please Enter A Word");
                string inoracia = Console.ReadLine().ToLower();
                char[] processInoracia = inoracia.ToCharArray();
                GivePermutations(processInoracia);
                
                writeToTextFile.Close();
                Console.WriteLine("\t\tDone!\n\n\tSearching for possible matches...\n");

                //Console.ReadLine();


                //StreamReader readFromResult = new StreamReader("Results.txt");
                //StreamReader readFromTextFile = new StreamReader("Dictionary.txt");

                //string line1 = "";
                string[] resultList = File.ReadAllLines("Results.txt");
                string[] dictionaryList = File.ReadAllLines("Dictionary.txt");
                //string[] listOne;
                //string[] listTwo;
                
                foreach (string item in resultList)
                {
                    foreach (string correctWord in dictionaryList)
                    {
                        if (item == correctWord)
                        {
                            messenger = String.Format("\t\tFound: {0}!", item);
                            //break;

                            if (messenger != "")
                            {
                                Console.WriteLine(messenger);
                            }
                            //else
                            
                        }
                        
                    }
                    /*
                    if (messenger != "")
                    {
                        break;
                    }
                     * */
                }
                if (messenger == "")
                {
                    Console.WriteLine("Couldn't find a possible match for \"{0}\"\n", inoracia);
                }
                SystemSounds.Asterisk.Play();
                Console.WriteLine("\nPress any key to continue...");
                //int count = 1;
                //string[] line2;
                Console.ReadKey();


                // To Be Finished Later
                    // This whole section is unnecessary, but i am still going to keep it for reasons
                    // Unknown to me... lol

                //while ((line1 = readFromResult.ReadLine()) != null)
                //{
                /*
                for (int i = 0; i < count; i++)
                {
                    
                    if (readFromResult.ReadLine() == null)
                    {
                        break;
                    }
                    liner1[i] = readFromResult.ReadLine();

                    Console.WriteLine(liner1[i]);

                    Console.ReadLine();
                    count++;
                }
                */


                /*   while ((line2 = readFromTextFile.ReadLine()) != null)
                   {
                       if (line1 == line2)
                       {
                           Console.WriteLine("Found: {0}!", line1);
                           Console.ReadLine();
                       }
                   }
               */
                // }

                //readFromResult.Close();
                //readFromTextFile.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }




        private static void Jumble(ref char one, ref char two)
        {
            if (one == two) return;

            one ^= two;
            two ^= one;
            one ^= two;
        }
        public static void GivePermutations(char[] entry)
        {
            int k = entry.Length;
            GetPermutations(entry, 0, k);
        }
        private static void GetPermutations(char[] entry, int depthOfRecursion, int maxDepth)         
        {
            if (depthOfRecursion == maxDepth)
            {
                
                Console.Write("\t");
                Console.WriteLine(entry);
                writeToTextFile.WriteLine(entry);

   
                
                
            }
            else
            {
                for (int i = depthOfRecursion; i < maxDepth; i++)
                {
                    Jumble(ref entry[depthOfRecursion], ref entry[i]);
                    GetPermutations(entry, depthOfRecursion + 1, maxDepth);
                    Jumble(ref entry[depthOfRecursion], ref entry[i]);
                }
            }
            
            
        }
    }
}

