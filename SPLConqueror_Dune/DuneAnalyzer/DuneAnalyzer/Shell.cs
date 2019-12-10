﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Dune.util;

namespace Dune
{
    class Shell
    {

        static string[][] help = {
            new string[] {"Command", "Abreviation", "Description"},
            new string[] {"analyze <argument>", "a", "Analyzes the given arguments and returns the possible alternatives found by the programm in the current state."},
            new string[] {"getClassWithName <argument>", "g", "Searches for the class with the given name."},
            new string[] {"help", "?", "Prints all commands and the according description."},
            new string[] {"fileAnalyzation", "f", "Analyzes the files given in the output directory."},
            new string[] {"quit", "q", "Quits the shell and will terminate the program."}
        };

        public static void showShell()
        {
            bool quit = false;
            while (!quit)
            {
                System.Console.Write("Dune>");
                string input = System.Console.ReadLine();
                int firstSpace = input.IndexOf(' ');
                string command;
                string arguments = "";
                if (firstSpace < 0)
                {
                    command = input;
                }
                else
                {
                    command = input.Substring(0, firstSpace);
                    arguments = input.Substring(firstSpace, input.Length - firstSpace).Trim();
                }

                switch (command.ToLower())
                {
                case "getClassesWithName":
                case "g":
                    foreach (DuneClass f in XMLParser.getClassesWithName(arguments))
                    {
                        System.Console.WriteLine(f.getFeatureName());
                    }
                    break;
                case "analyze":
                case "a":
                    arguments = arguments.Replace("<", " <");
                    arguments = arguments.Replace(">", " >");

                    List<string> result = ProgramUtil.getAlternativesRecursive(arguments).Keys.ToList();

                    if (result != null)
                    {
                        foreach (string s in result)
                        {
                            System.Console.WriteLine(s);
                        }
                        System.Console.WriteLine("Found " + result.Count + " possible alternative(s).");
                    }
                    else
                    {
                        System.Console.WriteLine("The class wasn't found.");
                    }
                    break;
                case "iniGeneration":
                case "i":
                    StreamReader inFile = new System.IO.StreamReader(Program.DEBUG_PATH + "IniInput.txt");
                    int count = 0;

                    List<List<string>> glResult = new List<List<string>>();
                    Dictionary<String, List<String>> resultsByVariablePoints = new Dictionary<string, List<string>>();
                    List<string> iniFilePlaceHolder = new List<string>();

                    // Find the alternatives of those classes
                    while (!inFile.EndOfStream)
                    {
                        String line = inFile.ReadLine().Trim();
                        if (!line.Equals(""))
                        {
                            String[] tokens = line.Split(Program.SPLIT_SYMBOL);
                            iniFilePlaceHolder.Add(tokens[0].Trim());
                            List<String> analyzationResult = ProgramUtil.getAlternativesRecursive(tokens[1].Trim()).Keys.ToList();
                            resultsByVariablePoints.Add(tokens[1].Trim(), new List<string>());

                            if (analyzationResult != null)
                            {
                                glResult.Add(analyzationResult);
                            }
                            else
                            {
                                glResult.Add(new List<string>());
                            }

                            foreach (String oneAlternative in analyzationResult)
                                resultsByVariablePoints[tokens[1].Trim()].Add(oneAlternative);
                        }
                    }

                    Program.generateVariabilityModel(resultsByVariablePoints);

                    // Use the whole information and generate the output (ini-files)
                    int[] configCount = new int[glResult.Count];

                    bool stop = false;
                    while (!stop)
                    {
                        // Print the current configuration
                        StreamWriter outp = new System.IO.StreamWriter(Program.DEBUG_PATH + "diffusion_" + String.Format("{0:0000}", count) +".ini");
                        count++;
                        for (int j = 0; j < configCount.Length; j++)
                        {
                            outp.WriteLine(iniFilePlaceHolder[j] + " " + Program.SPLIT_SYMBOL + " " + glResult[j][configCount[j]]);
                        }
                        outp.Close();

                        // Check if there is another configuration
                        bool backwards = true;
                        int i = configCount.Length - 1;
                        while (i >= 0 && i < configCount.Length)
                        {
                            if (backwards)
                            {
                                if (configCount[i] < glResult[i].Count - 1)
                                {
                                    configCount[i]++;
                                    backwards = false;
                                    i++;
                                } else
                                {
                                    i--;
                                }
                            } else
                            {
                                configCount[i] = 0;
                                i++;
                            }
                        }

                        if (i == -1 && backwards)
                        {
                            stop = true;
                        }

                    }


                    inFile.Close();
                    break;
                case "fileAnalyzation":
                case "f":
                    StreamReader inputFile = new System.IO.StreamReader(Program.DEBUG_PATH + "classesInDiffusion.txt");
                    StreamReader compFile = new System.IO.StreamReader(Program.DEBUG_PATH + "minimalSetClasses.txt");
                    StreamWriter output = new System.IO.StreamWriter(Program.DEBUG_PATH + "analyzation.txt");
                    StreamWriter positives = new System.IO.StreamWriter(Program.DEBUG_PATH + "positives.txt");

                    List<List<string>> globalResult = new List<List<string>>();
                    Dictionary<String, List<String>> resultsByVariabilityPoints = new Dictionary<string, List<string>>();

                    while (!inputFile.EndOfStream)
                    {
                        String line = inputFile.ReadLine().Trim();
                        if (!line.Equals(""))
                        {
                            Console.WriteLine("Identify Alternatives for class  " + line);
                            List<String> analyzationResult = ProgramUtil.getAlternativesRecursive(line).Keys.ToList();
                            resultsByVariabilityPoints.Add(line, new List<string>());

                            if (analyzationResult != null)
                            {
                                globalResult.Add(analyzationResult);
                            }
                            else
                            {
                                globalResult.Add(new List<string>());
                            }

                            foreach(String oneAlternative in analyzationResult)
                                resultsByVariabilityPoints[line].Add(oneAlternative);

                        }
                    }

                    Program.generateVariabilityModel(resultsByVariabilityPoints);

                    int c = 0;
                    int foundMin = 0;
                    int notFound = 0;
                    while (!compFile.EndOfStream)
                    {
                        String l = compFile.ReadLine();

                        if (!l.Trim().Equals(""))
                        {
                            switch (containsName(l, globalResult.ElementAt(c)))
                            {
                            case 1:
                                foundMin++;
                                break;
                            case 0:
                                foundMin++;
                                output.WriteLine("This classes name was found: " + l);
                                break;
                            case -1:
                                notFound++;
                                output.WriteLine(l);
                                break;
                            }
                        }
                        else
                        {
                            output.WriteLine(foundMin + "; " + notFound + "; " + globalResult.ElementAt(c).Count);
                            foundMin = 0;
                            notFound = 0;
                            c++;
                        }
                    }

                    // Write the whole set of positives in a file
                    foreach (List<string> results in globalResult)
                    {
                        foreach (string localResult in results)
                        {
                            positives.WriteLine(localResult);
                        }
                        positives.WriteLine();
                    }

                    output.Flush();
                    output.Close();
                    inputFile.Close();
                    compFile.Close();
                    positives.Flush();
                    positives.Close();
                    break;
                case "help":
                case "?":
                    printHelp();
                    break;
                case "quit":
                case "q":
                    quit = true;
                    break;
                }
            }
        }

        /// <summary>
        /// This method tries to find the right class. If multiple classes are found, the user has to select one. This method returns <code>null</code> if no class has been found or the input is invalid.
        /// </summary>
        /// <param name="feature">the class to search for</param>
        /// <returns>the selected <code>DuneClass</code>. <code>null</code> is returned if no class has been found or the input is invalid</returns>
        private static DuneFeature findFeature(string feature)
        {
            List<DuneFeature> dfs = XMLParser.getAllFeaturesByName(feature);

            if (dfs.Count == 0)
            {
                return null;
            }

            // If there is only one choice, the selection is obvious
            if (dfs.Count == 1)
            {
                return dfs.ElementAt(0);
            }

            System.Console.WriteLine("Multiple classes were found. Please select one by entering the number.");

            int count = 0;
            // In this case multiple classes were found
            foreach (DuneClass df in dfs)
            {
                System.Console.WriteLine(count + ": " + df.getFeatureName());
                count++;
            }

            System.Console.Write("Which one do you want to choose? ");
            string input = System.Console.ReadLine().Trim();
            int selected = 0;
            if (Int32.TryParse(input, out selected) && selected >= 0 && selected < count)
            {
                return dfs.ElementAt(selected);
            }
            else
            {
                System.Console.WriteLine("Selection not valid...aborting.");
                return null;
            }


        }

        /// <summary>
        /// Returns if the given name is included in the array either with the template or without.
        /// </summary>
        /// <param name="name">the name of the feature to search for</param>
        /// <param name="array">the array to search in</param>
        /// <returns><code>1</code> if the name including the template is found in the array; <code>0</code> if only the name is found; <code>-1</code> otherwise</returns>
        private static int containsName(string name, List<string> array)
        {
            if (array.Contains(name))
            {
                return 1;
            }
            else
            {
                if (name.Contains('<'))
                {
                    name = name.Substring(0, name.IndexOf('<'));
                }

                foreach (string comp in array)
                {
                    if (comp == null || comp.Equals("") || !comp.Contains('<'))
                    {
                        continue;
                    }

                    string compName = comp.Substring(0, comp.IndexOf('<'));
                    if (name.Equals(compName))
                    {
                        return 0;
                    }
                }
            }
            return -1;
        }

        static void printHelp()
        {
            foreach (string[] s in help)
            {
                System.Console.Write(s[0]);
                for (int i = 1; i < s.Length; i++)
                {
                    System.Console.Write("\t \t" + s[i]);
                }
                System.Console.WriteLine();
            }
        }
    }
}
