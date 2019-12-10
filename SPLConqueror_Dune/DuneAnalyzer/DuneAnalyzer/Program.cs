using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;
using SPLConqueror_Core;
using Dune.util;

namespace Dune
{
    static class Program
    {

        // The path of the xml-file to read the dependencies from
        //static String PATH = @"C:\all.xml";
        static String XML_PATH = @"E:\SPLConqueror_Dune\all1.xml";

        //public static String DEBUG_PATH = @"C:\DebugOutput\";
        public static String DEBUG_PATH = @"E:\SPLConqueror_Dune\DebugOutput\";

        public static String MINI_FILE_LOCATION = "";

        public static String RESULT_DIR = "";

        public static String CONSTRAINT_FILE = "";

        public const bool INCLUDE_CLASSES_FROM_STD = false;

        public static char SPLIT_SYMBOL = '=';
        
        public static char VARPOINT_SPLIT_SYMBOL = ';';

        public static String MAIN_FILE_DUNE_APPLICATION = "";

        public static String BASE_DIR_DUNE_APPLICATION = "";

        public static String BASE_DIR_DUNE_APPLICATION_MINI_VERSION = "";

        public static String TARGET_DIR_FOR_MINI_FILE = "";

        public static String NEW_C_MAKE_LISTS = "";

        public static bool USE_DUCK_TYPING = true;

        public static bool INCLUDE_CONSTRUCTORS = false;

		public static bool REPLACE_UNKNOWN_CLASSES_METHOD_WILDCARD = false;

		public static bool REPLACE_UNKNOWN_CLASSES_TEMPLATE_WILDCARD = false;

        public static bool USE_INTERACTIVE_SHELL = false;

        public static Logger infoLogger = null;

        /// <summary>
        /// Prints the usage of the plugin.
        /// </summary>
        private static void printUsage() {
            Console.WriteLine ("Usage: <PathToXMLFile> <PathToDebugOutput>");
            Console.WriteLine ("PathToXMLFile\t The absolute path to the xml file containing the documentation.");
            Console.WriteLine ("PathToDebugOutput\t The absolute path to the directory, where the debug output should be written to.");
        }


        /// <summary>
        /// The main-method of the Dune-plugin. This calls the corresponding <code>XMLParser</code>-methods.
        /// </summary>
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");


            // If the path is not given as argument, use the path specified in this file.
            if (args.Length > 0)
            {

                if (args.Length == 1 && args[0].EndsWith(".txt"))
                {
                    parseConfigFile(args[0]);

                    if (Directory.Exists(BASE_DIR_DUNE_APPLICATION_MINI_VERSION))
                        Directory.Delete(BASE_DIR_DUNE_APPLICATION_MINI_VERSION, true);

                    copyDuneApplicationToMiniLocation(BASE_DIR_DUNE_APPLICATION, BASE_DIR_DUNE_APPLICATION_MINI_VERSION);

					DuneApplication da = new DuneApplication(XML_PATH, BASE_DIR_DUNE_APPLICATION_MINI_VERSION, CONSTRAINT_FILE
					                                         , REPLACE_UNKNOWN_CLASSES_METHOD_WILDCARD, REPLACE_UNKNOWN_CLASSES_TEMPLATE_WILDCARD);

                    // TODO Flag in starter file
                    if (USE_INTERACTIVE_SHELL)
                    {
                        MiniShell shell = new MiniShell(da, XML_PATH, BASE_DIR_DUNE_APPLICATION_MINI_VERSION, CONSTRAINT_FILE,
                            REPLACE_UNKNOWN_CLASSES_METHOD_WILDCARD, REPLACE_UNKNOWN_CLASSES_TEMPLATE_WILDCARD, MINI_FILE_LOCATION);
                        shell.run();
                    }
                    else
                    {

                        writeAlternativesForVariationPoints(da, RESULT_DIR);

                        generateVariants(da, RESULT_DIR);
                    }
                    Environment.Exit(-1);
                }
                else
                {
                    XML_PATH = args[0];
                    if (args.Length == 1)
                    {
                        DEBUG_PATH = args[1];

                        // Add an additional directory separator if it was not included by the user.
                        DEBUG_PATH = DEBUG_PATH.EndsWith(Path.DirectorySeparatorChar.ToString()) ? DEBUG_PATH : DEBUG_PATH + Path.DirectorySeparatorChar;
                    }

                    infoLogger = new DuneAnalyzationLogger(DEBUG_PATH + "analyzation.log");


                    String duneStructure = "";
                    String mainFileCaseStudy = "";
                    String constraintFile = "";

                    if (args.Length > 2)
                    {
                        duneStructure = args[1];
                        mainFileCaseStudy = args[2];

                        if (args.Length > 3)
                            constraintFile = args[3];

                        DuneApplication da = new DuneApplication(duneStructure, mainFileCaseStudy, constraintFile);

                    }
                }
            }
            else
            {
                System.Console.WriteLine("No path passed as argument. Aborting...");
                printUsage ();
                Environment.Exit (-1);
            }

            XMLParser.parse(XML_PATH);

            Shell.showShell();


            System.Console.WriteLine("Press a button to close the window.");
            System.Console.ReadKey();
        }

        private static bool copyDuneApplicationToMiniLocation(string SourcePath, string DestinationPath)
        {
			SourcePath = SourcePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? SourcePath : SourcePath + Path.DirectorySeparatorChar;
			DestinationPath = DestinationPath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? DestinationPath : DestinationPath + Path.DirectorySeparatorChar;
			                                 

            try
            {
                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    foreach (string files in Directory.GetFiles(SourcePath))
                    {
                        FileInfo fileInfo = new FileInfo(files);
						fileInfo.CopyTo(string.Format(@"{0}" + Path.DirectorySeparatorChar + "{1}", DestinationPath, fileInfo.Name), true);
                    }

                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(drs);
                        if (copyDuneApplicationToMiniLocation(drs, DestinationPath + directoryInfo.Name) == false)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private static void generateVariants(DuneApplication da, string RESULT_DIR)
        {
            int numberOfVariants = 1;

            // identify all alternatives without ??
		String unfilteredMiniFileContent = ProgramUtil.generateMiniFileContent(da);
           File.WriteAllText(RESULT_DIR + "unfilteredmini.mini", unfilteredMiniFileContent);


            foreach(KeyValuePair<String, VariationPoint> varP in da.varPoints)
            {
                Dictionary<String, DuneFeature> resultsUnfiltered = ProgramUtil.getAlternativesRecursive(varP.Value.defaultValue);

                Dictionary<String, DuneFeature> filtered = ProgramUtil.filter(resultsUnfiltered);
                varP.Value.alternatives = filtered;

                numberOfVariants = numberOfVariants * filtered.Count;

            }

            //List<Variant> allVariants = new List<Variant>();
            //for(int i = 0; i < numberOfVariants; i++)
            //{
            //    allVariants.Add(new Variant());
            //}

            Dictionary<VariationPoint, int> varPo = new Dictionary<VariationPoint, int>();

            foreach(KeyValuePair<String, VariationPoint> varP in da.varPoints)
            {
                varPo.Add(varP.Value, varP.Value.alternatives.Count);
            }

            List<KeyValuePair<VariationPoint, int>> varPList = varPo.ToList();

            double[,] allAlternatives =  getFullFactorial(varPList, numberOfVariants);

            for (int i = 0; i < allAlternatives.GetLength(0); i++)
            {
                String curr = "";

                for (int j = 0; j < allAlternatives.GetLength(1); j++)
                    curr = curr + allAlternatives.GetValue(new int[] { i, j }) + ";";



                File.AppendAllText(RESULT_DIR + "variants2.txt", curr + Environment.NewLine);


            }

            //for (int i = 0; i < allVariants.Count; i++)
            //{
            //    int varPIndex = 0;
            //    foreach (KeyValuePair<VariationPoint,int> varP in varPList)
            //    {
            //        int alternative = (int)allAlternatives[i, varPIndex];
            //        string value = varP.Key.alternatives[alternative];
            //        allVariants[i].selectedAlternatives.Add(varP.Key, value);

            //        varPIndex++;
            //    }
            //}


            //foreach (KeyValuePair<String, VariationPoint> varP in da.varPoints)
            //{
            //    int repitions = allVariants.Count / powerTwo;

            //    int currIndex = 0;

            //    for (int i = 0; i < allVariants.Count; i++)
            //    {

            //        for(int repi = 0; repi < repitions; repitions++)
            //        {
            //            Variant variant = allVariants[i + repi];
            //            variant.selectedAlternatives.Add(varP.Value, varP.Value.alternatives[currIndex]);
            //            //variant.selectedAlternatives.Add(varP.Value, ""+currIndex);
            //        }

            //        currIndex = ((currIndex +1) % varP.Value.alternatives.Count);
            //    }

            //    powerTwo = powerTwo * 2;
            //}

            //Console.WriteLine( allVariants.Count);

           

            String miniFileContent = ProgramUtil.generateMiniFileContent(da);

            

            File.WriteAllText(MINI_FILE_LOCATION, miniFileContent);

            //writeVariantsToCSV(allVariants, Program.RESULT_DIR);

            updateCMakeLists(NEW_C_MAKE_LISTS);

        }

        private static void updateCMakeLists(string NEW_C_MAKE_LISTS)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("add_executable(\"diffusion\" diffusion.cc)");
            sb.AppendLine("add_test(NAME \"diffusion\"");
            sb.AppendLine("     COMMAND ./diffusion diffusion.ini)");
            sb.AppendLine("target_link_dune_default_libraries(\"diffusion\")");
            sb.AppendLine("add_dune_gmp_flags(\"diffusion\")");
            sb.AppendLine("add_dune_ug_flags(\"diffusion\")");
            sb.AppendLine("dune_add_system_test(SOURCE diffusion.cc");
            sb.AppendLine("             BASENAME diffusion");
            sb.AppendLine("             INIFILE diffusion.mini)");

            File.WriteAllText(NEW_C_MAKE_LISTS, sb.ToString());

        }

        private static double[,] getFullFactorial(List<KeyValuePair<VariationPoint, int>> numberOfAlternatives, int numberAlternatives)
        {
            double[][] fullFactorial = new double[numberAlternatives][];

            Dictionary<VariationPoint, List<int>> elementValuePairs = new Dictionary<VariationPoint, List<int>>();
            foreach (KeyValuePair<VariationPoint, int> vf in numberOfAlternatives)
            {
                elementValuePairs[vf.Key] = Enumerable.Range(0, vf.Key.alternatives.Count).ToList();
            }

            int[] positions = new int[numberOfAlternatives.Count];

            int featureToIncrement = 0;
            bool notIncremented = true;

            int iterator = 0;

            do
            {
                notIncremented = true;

                // tests whether all combinations are computed
                if (featureToIncrement == numberOfAlternatives.Count)
                {
                    double[,] tmp = new double[fullFactorial.Length,fullFactorial[0].Length];
                    for (int i = 0; i < fullFactorial.Length; i++)
                    {
                        for (int j = 0; j < fullFactorial[0].Length; j++)
                        {
                            tmp[i,j] = fullFactorial[i][j];
                        }
                    }

                    return tmp;
                }
                fullFactorial[iterator] = generateRow(elementValuePairs, positions);
                iterator++;

                do
                {
                    if (positions[featureToIncrement] == numberOfAlternatives.ElementAt(featureToIncrement).Value - 1)
                    {
                        positions[featureToIncrement] = 0;
                        featureToIncrement++;
                    }
                    else
                    {
                        positions[featureToIncrement] = positions[featureToIncrement] + 1;
                        notIncremented = false;
                        featureToIncrement = 0;
                    }
                } while (notIncremented && !(featureToIncrement == numberOfAlternatives.Count));
            } while (true);
        }

		private static double[] generateRow(Dictionary<VariationPoint, List<int>> numberOfLevels, int[] positions)
		{
			double[] run = new double[numberOfLevels.Count];

			for (int i = 0; i < numberOfLevels.Count; i++)
			{
				run[i] = numberOfLevels.ElementAt(i).Value[positions[i]];
			}
			return run;
		}

        private static void writeAlternativesForVariationPoints(DuneApplication da, string RESULT_DIR)
        {
           StringBuilder sb = new StringBuilder();

           sb.AppendLine("XML File " + Program.XML_PATH);
           sb.AppendLine("MAIN_FILE_DUNE_APPLICATION " + Program.BASE_DIR_DUNE_APPLICATION + Program.MAIN_FILE_DUNE_APPLICATION);
            
                
           foreach (KeyValuePair<string, VariationPoint> varPoint in da.varPoints)
           {
                List<String> result = varPoint.Value.alternatives.Keys.ToList();
                sb.AppendLine("original value " + varPoint.Value.defaultValue);
                foreach(String s in result)
                {
                    sb.AppendLine(s);  
                }
                sb.AppendLine("-----------------------");
            }
            StreamWriter sw = File.CreateText(Program.RESULT_DIR + "result.txt");
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();
        }

        private static void parseConfigFile(string file)
        {
            if (!File.Exists(file))
            {
                System.Console.WriteLine("Config file does not exists.");


            }
            else
            {
                // Set the current directory for supporting relative paths
                Directory.SetCurrentDirectory(Path.GetDirectoryName(file));

                String[] readText = File.ReadAllText(file).Split(Environment.NewLine.ToCharArray());
                for(int i = 0; i < readText.Length; i++)
                {
                    String[] curr = readText[i].Trim().Split(' ');
                    if(curr.Length == 2)
                    {
                        String identifyer = curr[0];
                        String path = curr[1];

                        switch (identifyer)
                        {
                            case "XML_PATH":
                                XML_PATH = path;
                                break;
                            case "DEBUG_PATH":
                                DEBUG_PATH = path;
                                infoLogger = new DuneAnalyzationLogger(DEBUG_PATH + "analyzation.log");
                                break;
                            case "MAIN_FILE_DUNE_APPLICATION":
                                MAIN_FILE_DUNE_APPLICATION = path;
                                break;
                            case "CONSTRAINT_FILE":
                                CONSTRAINT_FILE = path;
                                break;
                            case "RESULT_DIR":
                                RESULT_DIR = path;
                                break;
                            case "MINI_FILE_LOCATION":
                                MINI_FILE_LOCATION = path;
                                break;

                            case "NEW_C_MAKE_LISTS":
                                NEW_C_MAKE_LISTS = path;
                                break;

                            case "BASE_DIR_DUNE_APPLICATION":
                                BASE_DIR_DUNE_APPLICATION = path;
                                break;

                            case "BASE_DIR_DUNE_APPLICATION_MINI_VERSION":
                                BASE_DIR_DUNE_APPLICATION_MINI_VERSION = path;
                                break;

							case "REPLACE_UNKNOWN_CLASSES_METHOD_WILDCARD":
								REPLACE_UNKNOWN_CLASSES_METHOD_WILDCARD = Boolean.Parse(path);
								break;

							case "REPLACE_UNKNOWN_CLASSES_TEMPLATE_WILDCARD":
								REPLACE_UNKNOWN_CLASSES_TEMPLATE_WILDCARD = Boolean.Parse(path);
								break;

                            case "USE_INTERACTIVE_SHELL":
                                USE_INTERACTIVE_SHELL = Boolean.Parse(path);
                                break;

                            default:
                                System.Console.WriteLine("Error in config File. The command "+identifyer+" is unknown.");
                                break;
                        }

                    }
                }


            }


        }

        internal static void generateVariabilityModel(Dictionary<string, List<string>> resultsByVariabilityPoints)
        {
            VariabilityModel varModel = new VariabilityModel("DuneCaseStudy");


            foreach (KeyValuePair<String, List<String>> resultForOne in resultsByVariabilityPoints)
            {
                BinaryOption alternativeParent = new BinaryOption(varModel, "group" + resultForOne.Key);
                alternativeParent.Optional = false;
                alternativeParent.Parent = varModel.Root;
                alternativeParent.OutputString = "NoOutput";
                varModel.addConfigurationOption(alternativeParent);

                List<BinaryOption> elementsOfGroup = new List<BinaryOption>();
                foreach (String alternative in resultForOne.Value)
                {
                    BinaryOption oneAlternative = new BinaryOption(varModel, alternative);
                    oneAlternative.Optional = false;
                    oneAlternative.OutputString = alternative;
                    oneAlternative.Parent = alternativeParent;
                    varModel.addConfigurationOption(oneAlternative);
                    elementsOfGroup.Add(oneAlternative);
                }

                foreach (BinaryOption alternative in elementsOfGroup)
                {
                    foreach (BinaryOption other in elementsOfGroup)
                    {
                        if (alternative.Equals(other))
                            continue;

                        alternative.Excluded_Options.Add(new List<ConfigurationOption>() { other });
                    }
                }
            }

            varModel.saveXML(DEBUG_PATH + varModel.Name+".xml");
        }
    }
}
