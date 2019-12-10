using SPLConqueror_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dune
{
    class ProgramUtil
    {
        private ProgramUtil() { }

        public static String generateMiniFileContent(DuneApplication da)
        {
            StringBuilder miniContent = new StringBuilder();

            StringBuilder varPointDefinitions = new StringBuilder();

            foreach (KeyValuePair<String, VariationPoint> varP in da.varPoints)
            {
                generateVariationPointContent(varP.Value, varP.Value.getIdentifyer(), miniContent, varPointDefinitions);
            }

            miniContent.Append(varPointDefinitions);

            return miniContent.ToString();
        }

        public static String generateFilteredMiniFileContent(DuneApplication da)
        {

            foreach(KeyValuePair<string, VariationPoint> vaPo in da.varPoints)
            {
                Dictionary<string, DuneFeature> alternatives = new Dictionary<string, DuneFeature>();
                foreach (KeyValuePair<string, DuneFeature> alt in vaPo.Value.alternatives)
                    if (!alt.Key.Contains("??")) alternatives.Add(alt.Key, alt.Value);

                vaPo.Value.alternatives = alternatives;
            }

            return generateMiniFileContent(da);
        }

        public static void generateVariationPointContent(VariationPoint vaPo, string identifyer,
            StringBuilder miniContent, StringBuilder varPointDefinitions)
        {
            StringBuilder alternatives = new StringBuilder();

            varPointDefinitions.Append(identifyer + " = ");

            alternativesToString(vaPo.alternatives, identifyer, alternatives, varPointDefinitions);

            varPointDefinitions = varPointDefinitions.Remove(varPointDefinitions.ToString().Length - 2, 2);
            varPointDefinitions.AppendLine(" | expand");

            miniContent.Append(alternatives);
        }

        public static void alternativesToString(Dictionary<String, DuneFeature> vaPoAlternatives, string identifyer,
            StringBuilder alternatives, StringBuilder varPointDefinitions)
        {
            int i = 0;
            foreach (KeyValuePair<String, DuneFeature> alternative in vaPoAlternatives)
            {
                alternatives.AppendLine(identifyer + "_" + i + " = " + alternative.Key);
                varPointDefinitions.Append("{" + identifyer + "_" + i + "} " + ",");
                i++;
            }
            alternatives.AppendLine("");
        }

        public static Dictionary<String, DuneFeature> filter(Dictionary<String, DuneFeature> alternatives)
        {
            Dictionary<String, DuneFeature> filtered = new Dictionary<String, DuneFeature>();

            foreach (KeyValuePair<String, DuneFeature> s in alternatives)
            {
                if (!s.Key.Contains("??"))
                {
                    filtered.Add(s.Key, s.Value);
                }
            }

            return filtered;
        }

        #region compute recursive alternatives
        public static Dictionary<String, DuneFeature> getAlternativesRecursive(String input)
        {
            input = input.Trim();
            bool inputHasTemplate = false;


            List<String> alternatives = new List<string>();
            DuneFeature importantClass = null;
            TemplateTree treeOfInterest = new TemplateTree();

            // split input in name and template parameters
            String name = "";
            String[] templateDefinedByUser = new String[0];

            if (input.Contains('<'))
            {
                inputHasTemplate = true;

                name = input.Substring(0, input.IndexOf('<')).Trim();
                templateDefinedByUser = getTemplateParts(input);
            }
            else
            {
                name = input;
            }
            
            // Search for internal representations of the given class...
            List<DuneClass> allOthers = new List<DuneClass>();
            foreach (DuneClass others in XMLParser.featuresWithPublicMethods)
            {
                if (others.getFeatureNameWithoutTemplate().Equals(name))
                {
                    importantClass = others;
                    allOthers.Add(others);
                }
            }

            if (allOthers.Count > 1)
            {
                Program.infoLogger.log("Potential error in getAlternativesRecursive() in the identification of the DuneClass of the given class for " + input + ".  ");
                Program.infoLogger.logLine("More than one internal class could match the given one.");
                importantClass = getDuneClassByNumberOfTemplateParameters(allOthers, templateDefinedByUser.Count());
            }


            // mapping from the default placeholder strings of the template in the strings of the given input template
            Dictionary<String, String> mapping = new Dictionary<string, string>();
            if (importantClass == null)
            {
                // input is the value of an enum
                foreach (DuneEnum currEnum in XMLParser.enums)
                {
                    foreach (String s in currEnum.getValues())
                    {
                        if (s.Equals(input))
                        {
                            importantClass = currEnum;
                        }
                    }
                }
            }
            else
            {
                List<TemplateTree> templateOfClass = ((DuneClass)importantClass).templateElements;

                String cont = "";
                for (int i = 0; i < templateOfClass.Count; i++)
                {
                    cont += templateOfClass[i].declmame_cont + " | ";

                    if (templateOfClass[i].declmame_cont.Trim().Length == 0)
                    {
                        if (mapping.ContainsKey(templateOfClass[i].deftype_cont))
                        {
                            mapping.Add(templateOfClass[i].deftype_cont + "_" + i, templateDefinedByUser[i]);
                        }
                        else
                        {
                            mapping.Add(templateOfClass[i].deftype_cont, templateDefinedByUser[i]);
                        }
                    }
                    else
                    {
                        if (mapping.ContainsKey(templateOfClass[i].declmame_cont))
                        {
                            mapping.Add(templateOfClass[i].declmame_cont + "_" + i, templateDefinedByUser[i]);
                        }
                        else
                        {
                            if (templateDefinedByUser.Count() - 1 < i)
                            {
                                if (templateOfClass[i].defaultValue == null)
                                {
                                    mapping.Add(templateOfClass[i].declmame_cont, templateOfClass[i].defval_cont);

                                }
                                else
                                {
                                    mapping.Add(templateOfClass[i].declmame_cont, templateOfClass[i].defaultValue.ToString());
                                }
                            }
                            else
                            {
                                mapping.Add(templateOfClass[i].declmame_cont, templateDefinedByUser[i]);
                            }
                        }
                    }
                }

                String s = cont;

            }

            if (importantClass == null)
            {
                Program.infoLogger.log("Potential error in getAlternativesRecursive() in the identification of the DuneClass of the given class for " + input + ".  ");
                Program.infoLogger.logLine("No internal representation for the given class could be found.");
                return new Dictionary<String, DuneFeature>();
                //System.Environment.Exit(1);
            }

            Dictionary<String, DuneFeature> alternativesFirstLevel = ((DuneFeature)importantClass).getVariability();
            Dictionary<String, DuneFeature> alternativesFirstLevelWithConcreteParameters = new Dictionary<String, DuneFeature>();

            if (inputHasTemplate)
            {
                foreach (KeyValuePair<String, DuneFeature> element in alternativesFirstLevel)
                {

                    if (((DuneClass)element.Value).templateElements.Count > 0)
                    {
                        DuneClass alternative = (DuneClass)element.Value;
                        String alternativStringWithUserInput = element.Value.getFeatureNameWithoutTemplate() + "<";
                        for (int i = 0; i < alternative.templateElements.Count; i++)
                        {

                            String nameTemplateParameter = alternative.templateElements[i].declmame_cont;

                            if (nameTemplateParameter.Trim().Length == 0)
                            {
                                if (mapping.ContainsKey(alternative.templateElements[i].deftype_cont))
                                {
                                    alternativStringWithUserInput += mapping[alternative.templateElements[i].deftype_cont];
                                }
                                else
                                {
                                    if (alternative.templateElements[i].deftype_cont.Length > 0)
                                    {
                                        if (alternative.templateElements[i].defval_cont.Length > 0)
                                            if (mapping.ContainsKey(alternative.templateElements[i].defval_cont))
                                                alternativStringWithUserInput += mapping[alternative.templateElements[i].defval_cont];
                                            else
                                                alternativStringWithUserInput += alternative.templateElements[i].defval_cont;
                                        else
                                            alternativStringWithUserInput += alternative.templateElements[i].deftype_cont;
                                    }
                                    else
                                    {
                                        String deftype_cont = alternative.templateElements[i].deftype_cont;
                                        Double res;
                                        if (Double.TryParse(deftype_cont, out res))
                                        {
                                            alternativStringWithUserInput += deftype_cont;
                                        }
                                        else
                                            alternativStringWithUserInput += "??" + nameTemplateParameter + "??";
                                    }
                                }
                            }
                            else
                            {
                                if (mapping.ContainsKey(nameTemplateParameter))
                                {
                                    alternativStringWithUserInput += mapping[nameTemplateParameter];
                                }
                                else
                                {
                                    if (alternative.templateElements[i].defval_cont.Length > 0)
                                        alternativStringWithUserInput += alternative.templateElements[i].defval_cont;
                                    else
                                        alternativStringWithUserInput += "??" + nameTemplateParameter + "??";
                                }
                            }

                            if (i < alternative.templateElements.Count - 1)
                                alternativStringWithUserInput += ",";
                            else
                                alternativStringWithUserInput += ">";
                        }
                        if (!alternativesFirstLevelWithConcreteParameters.ContainsKey(alternativStringWithUserInput))
                            alternativesFirstLevelWithConcreteParameters.Add(alternativStringWithUserInput, element.Value);
                    }
                    else
                    {
                        alternativesFirstLevelWithConcreteParameters.Add(element.Key, element.Value);
                    }
                }
            }
            else
            {

                foreach (KeyValuePair<String, DuneFeature> element in alternativesFirstLevel)
                {

                    if (element.Value.GetType() == typeof(DuneEnum))
                    {

                        alternativesFirstLevelWithConcreteParameters.Add(element.Key, element.Value);
                    }
                    else
                    {

                        DuneClass alternative = (DuneClass)element.Value;

                        String alternativStringWithUserInput = alternative.getFeatureNameWithoutTemplate();

                        if (alternative.templateElements.Count > 0)
                        {
                            alternativStringWithUserInput += " < ";

                            for (int i = 0; i < alternative.templateElements.Count; i++)
                            {

                                String nameTemplateParameter = alternative.templateElements[i].declmame_cont;

                                if (mapping.ContainsKey(nameTemplateParameter))
                                {
                                    alternativStringWithUserInput += mapping[nameTemplateParameter];
                                }
                                else
                                {
                                    if (alternative.templateElements[i].defval_cont.Length > 0)
                                        alternativStringWithUserInput += alternative.templateElements[i].defval_cont;
                                    else
                                        alternativStringWithUserInput += "??" + nameTemplateParameter + "??";
                                }


                                if (i < alternative.templateElements.Count - 1)
                                    alternativStringWithUserInput += ",";
                                else
                                    alternativStringWithUserInput += ">";
                            }

                        }

                        alternativesFirstLevelWithConcreteParameters.Add(alternativStringWithUserInput, element.Value);
                    }
                }
            }


            return alternativesFirstLevelWithConcreteParameters;
        }

        public static String[] getTemplateParts(String input)
        {
            List<char> reverseName = input.Reverse().ToList();
            int closingIndex = input.Count();
            for (int i = 0; i < reverseName.Count; i++)
            {
                if (reverseName[i].Equals('>'))
                {
                    closingIndex = i;
                    break;
                }

            }
            int templateLength = (input.Count()) + (closingIndex) - input.IndexOf('<') - 2;

            String[] templateDefinedByUser = input.Substring(input.IndexOf('<') + 1, templateLength).Split(',');

            return templateDefinedByUser;
        }

        private static DuneClass getDuneClassByNumberOfTemplateParameters(List<DuneClass> allOthers, int p)
        {
            DuneClass f = null;

            for (int i = 0; i < allOthers.Count; i++)
            {
                if (allOthers[i].getTemplateArgumentCount().getLowerBound() <= p && allOthers[i].getTemplateArgumentCount().getUpperBound() >= p)
                    if (f == null)
                        f = allOthers[i];
                    else
                        Program.infoLogger.logLine("Multiple classes found that could match with the input");
            }

            return f;
        }
        #endregion
    }
}