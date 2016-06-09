using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

// Added Text To Speech Reference Just To Learn
using System.Speech;
using System.Speech.Synthesis;

namespace Inoracia
{
    class Program
    {
        private static StreamWriter writeToTextFile = new StreamWriter("Results.txt");
        private static string messenger = "";
        /*
         * ============================================================================
         *                             REDUNDANT CODE
         * ============================================================================
        private static string myDictionary = "CHEESE";
        
        private static char[] conDict = myDictionary.ToCharArray();
         
        private static string[] dictionary = {"FISHERMAN", "FOOD", "FOOL", "SHOE"};
         * 
        */
        static void Main(string[] args)
        {
            try
            {

                SpeechSynthesizer mySpeaker = new SpeechSynthesizer();

                mySpeaker.Rate = 1;

                string welcome = "What word would you like solved?";
                mySpeaker.SpeakAsync(welcome);
                Console.WriteLine(welcome);


                string inoracia = Console.ReadLine().ToLower();

                char[] processInoracia = inoracia.ToCharArray();

                GivePermutations(processInoracia);

                writeToTextFile.Close();


                string searchMessage = String.Format("\tSearching possible matches for ");
                Console.Write(searchMessage);
                mySpeaker.Speak(searchMessage);
                for (int i = 0; i < processInoracia.Length; i++)
                {
                    // Make Console spell out your input
                    Console.Write(processInoracia[i].ToString());
                    mySpeaker.Speak(processInoracia[i].ToString());

                }
                Console.WriteLine();


                /* REDUNDANT NOW.. STILL KEEPING IT TO REMIND ME OF MY HUSTLE
                //Console.ReadLine();
                //StreamReader readFromResult = new StreamReader("Results.txt");
                //StreamReader readFromTextFile = new StreamReader("Dictionary.txt");

                //string line1 = "";
                
                //string[] listOne;
                //string[] listTwo;
                */

                // OPEN Result.txt and Dictionary.txt FOR READING
                string[] resultList = File.ReadAllLines("Results.txt");
                string[] dictionaryList = File.ReadAllLines("Dictionary.txt");


                // Take every line in resultList and compare with every line in dictionaryList
                foreach (string item in resultList)
                {

                    foreach (string correctWord in dictionaryList)
                    {
                        if (item == correctWord)
                        {
                            // Set the Contents of messenger to the item found
                            messenger = String.Format("\t\tFound: {0}!", item);
                            //break;

                            if (messenger != "")
                            {
                                mySpeaker.SpeakAsync(messenger);
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
                string badNews = "";
                if (messenger == "")
                {
                    // To be executed if no result was found
                    badNews = String.Format("Couldn't find a possible match for \"{0}\"\n", inoracia);
                    mySpeaker.SpeakAsync(badNews);
                    Console.WriteLine(badNews);
                    

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

                //wanted to use System.IO.StreamReader initially... No longer needed
                //readFromResult.Close();
                //readFromTextFile.Close();
            }
            catch (Exception e)
            {
                // Protect User from Feeling Stupid
                SpeechSynthesizer speakerOne = new SpeechSynthesizer();
                speakerOne.SpeakAsync(e.Message);
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

            finally
            {
                // Remember to write  usage log to text file
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
                
                //Console.Write("\t");
                //Console.WriteLine(entry);
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

