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
        private static StreamWriter writeLog = File.AppendText("History.txt");
        private static StreamWriter writeToTextFile = new StreamWriter("Results.txt");
        private static string messenger = "";
        private static string msg = "", diagnostics = "";

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
                /*
                 * Need To figure out how to store to history
                string[] readLog = File.ReadAllLines("History.txt");
                string logToString = "";
                foreach (string item in readLog)
                {
                    logToString += item;
                }
                
                Console.WriteLine(logToString + readLog.Length.ToString());
                */
                SpeechSynthesizer mySpeaker = new SpeechSynthesizer();

                mySpeaker.Rate = 2;

                string welcome = "What word would you like solved?";
                mySpeaker.SpeakAsync(welcome);
                Console.WriteLine(welcome);


                string inoracia = Console.ReadLine().ToLower();

                char[] processInoracia = inoracia.ToCharArray();

                GivePermutations(processInoracia);

                writeToTextFile.Close();

                mySpeaker.Rate = 2;
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
                            
                            // For helping write to User Diag
                            msg += item + " ";
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
                    msg = "No results were found";
                    // To be executed if no result was found
                    badNews = String.Format("Couldn't find a possible match for \"{0}\"\n", inoracia);
                    mySpeaker.SpeakAsync(badNews);
                    Console.WriteLine(badNews);
                    

                }

                

                // To Be Finished Later
                // This whole section is unnecessary, but i am still going to keep it for reasons
                // Unknown to me... lol

                //int count = 1;
                //string[] line2;
                //string[] line1;

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




                // Remember to write  usage log to text file
                
                DateTime myDate = DateTime.Now;

                string usageLog = String.Format("[{0}]: \n\tSearched: {1} \n\t\tFound: {2}", myDate, inoracia, msg);
                
                writeLog.WriteLine(usageLog);
                
                writeLog.Close();


                SystemSounds.Asterisk.Play();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                // Never delete previous contents of writeLog
                // Create string to get the contents of the 
                // File at startup and add to it.. creates 
                // Some kind of history
            }
            catch (Exception e)
            {
                
                // Protect User from Feeling Stupid
                SpeechSynthesizer speakerOne = new SpeechSynthesizer();
                speakerOne.SpeakAsync(e.Message);
                Console.WriteLine(e.Message);


                // Write Error To text File
                diagnostics = e.Message;
                DateTime myDate = DateTime.Now;
                string myString = String.Format("[{0}]:\n\t{2}\n\t\t{1}", myDate, diagnostics, "Error Message:");
                writeLog.WriteLine(myString);
                
                Console.ReadLine();
            }

            finally
            {
                
                
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

