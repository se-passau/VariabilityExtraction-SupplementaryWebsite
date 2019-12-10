using Dune;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dune
{
    public class MiniShell
    {
        DuneApplication da;
        string xmlPath;
        string baseDirMini;
        string constraint;
        string miniLocation;
        bool methodWildcard;
        bool templateWildcard;

        public MiniShell(DuneApplication da, string xmlPath, string baseDirMini, string constraint,
            bool methodWildcard, bool templateWildcard, string miniLocation)
        {
            this.da = da;
            this.xmlPath = xmlPath;
            this.baseDirMini = baseDirMini;
            this.constraint = constraint;
            this.methodWildcard = methodWildcard;
            this.templateWildcard = templateWildcard;
            this.miniLocation = miniLocation;
        }

        public void run()
        {
            bool quit = false;

            while(!quit)
            {
                Console.Write("DuneAnalyzer>");
                String input = Console.ReadLine().Trim();
                string[] args = input.Split(new char[] { ' ' });
                switch (input.ToLowerInvariant()[0])
                {
                    case 'q':
                        quit = true;
                        break;
                    case 'p':
                        if (args[1] == "--all" && args.Length == 2)
                            Console.Write(ProgramUtil.generateMiniFileContent(da));
                        else if (args[1] == "--all") {
                            VariationPoint vaPo;
                            if (!tryGetVariationPoint(args[2], da, out vaPo))
                                break;

                            StringBuilder sb = new StringBuilder();
                            ProgramUtil.generateVariationPointContent(vaPo, vaPo.getIdentifyer(), sb, new StringBuilder());
                            Console.Write(sb);
                        } else if (args[1] == "--filtered" && args.Length == 2) 
                        {
                            foreach(KeyValuePair<string, VariationPoint> vaPo in da.varPoints)
                            {
                                StringBuilder alternatives = new StringBuilder();
                                ProgramUtil.alternativesToString(ProgramUtil.filter(vaPo.Value.alternatives), vaPo.Value.getIdentifyer(), alternatives, new StringBuilder());
                                Console.Write(alternatives.ToString());
                            }
                        } else if (args[1] == "--filtered")
                        {
                            VariationPoint vaPo;
                            if (!tryGetVariationPoint(args[2], da, out vaPo))
                                break;

                            StringBuilder alternatives = new StringBuilder();
                            ProgramUtil.alternativesToString(ProgramUtil.filter(vaPo.alternatives), vaPo.getIdentifyer(), alternatives, new StringBuilder());
                            Console.Write(alternatives.ToString());
                        }
                        break;

                    case 'r':
                        if (args.Length == 4)
                        {
                            VariationPoint vaPo;
                            if (!tryGetVariationPoint(args[1], da, out vaPo))
                                break;

                            string old = args[2];
                            string newVariableName = args[3];

                            // Special symbol to delete words
                            if (newVariableName == "_")
                                newVariableName = "";

                            Dictionary<string, DuneFeature> tmp = new Dictionary<string, DuneFeature>();
                            foreach(KeyValuePair<string, DuneFeature> va in vaPo.alternatives)
                            {
                                if (va.Key.Contains(">"))
                                {
                                    String[] split = va.Key.Split(new string[] { "<", ">" }, StringSplitOptions.None);

                                    tmp.Add(split[0] + "<" + split[1].Replace(","+old, newVariableName) + ">", va.Value);
                                }
                                else
                                    tmp.Add(va.Key, va.Value);
                            }

                            vaPo.alternatives = tmp;
                        }
                        break;
                    case 's':
                        if (args[1] == "--all")
                            File.WriteAllText(miniLocation, ProgramUtil.generateMiniFileContent(da));
                        else if (args[1] == "--filtered")
                            File.WriteAllText(miniLocation, ProgramUtil.generateFilteredMiniFileContent(da));
                        break;
                    case 'd':
                        if (args.Length == 3)
                        {
                            VariationPoint vaPo;
                            if (!tryGetVariationPoint(args[1], da, out vaPo))
                                break;
                            vaPo.alternatives.Remove(vaPo.alternatives.ElementAt(int.Parse(args[2])).Key);
                        }
                        break;


                    case 'c':
                        {
                            VariationPoint vaPo;
                            if (!tryGetVariationPoint(args[1], da, out vaPo))
                                break;

                            string newTemplate = parseTemplateArg(args);

                            Dictionary<string, DuneFeature> newAlternatives = ProgramUtil.getAlternativesRecursive(newTemplate);
                            vaPo.defaultValue = newTemplate;
                            vaPo.alternatives = newAlternatives;

                            if (newAlternatives.Count == 0)
                            {
                                writeConsoleWarning("Warning: Given template could not be found.");
                            }
                        }
                        break;
                    case 'a':
                        {
                            VariationPoint vaPo;
                            if (!tryGetVariationPoint(args[1], da, out vaPo))
                                break;

                            string newTemplate = parseTemplateArg(args);

                            Dictionary<string, DuneFeature> newAlternatives = ProgramUtil.getAlternativesRecursive(newTemplate);

                            int oldCount = vaPo.alternatives.Count;
                            vaPo.alternatives = vaPo.alternatives.Union(newAlternatives).ToDictionary(x => x.Key, x => x.Value);
                            int newCount = vaPo.alternatives.Count;

                            if (newAlternatives.Count == 0)
                            {
                                writeConsoleWarning("Warning: Given template could not be found.");
                            }

                            if (oldCount == newCount)
                            {
                                writeConsoleWarning("Warning: No new templates found for this input.");
                            }
                        }
                        break;
                    default:
                        writeConsoleWarning("Warning: Invalid command.");
                        break;

                }
            }
        }

        private void writeConsoleWarning(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DuneAnalyzer>" + txt);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private string parseTemplateArg(string[] args)
        {
            var onlyTemplate = args.Skip(2);
            return String.Join(String.Empty, onlyTemplate);
        }

        private bool tryGetVariationPoint(string name, DuneApplication da, out VariationPoint vaPo)
        {
            bool succeded = da.varPoints.TryGetValue(name, out vaPo);
            if (!succeded)
            {
                writeConsoleWarning("Error: Invalid VariationPoint(VariationPoint names are case sensitive).");
            }
            return succeded;
        }
    }
}
