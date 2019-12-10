using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dune
{
    public class DuneApplication
    {

        private string duneStructureFile = "";
        private string mainFileCaseStudy = "";
        private string constraintFile = "";

		private bool replaceUnknownClassesMethods = false;

		private bool replaceUnknownTemplateClasses = false;

        private bool hasConstraintFile = true;

        public Dictionary<String, VariationPoint> varPoints = new Dictionary<String, VariationPoint>();




        private List<Constraint> constraints = new List<Constraint>();

		public DuneApplication(string duneStructure, string mainFileCaseStudy, string constraintFile
		                       , bool replaceUnknownMethodClasses = false, bool replaceUnknownTemplateClasses = false)
        {
            this.duneStructureFile = duneStructure;
            this.mainFileCaseStudy = mainFileCaseStudy;
            this.constraintFile = constraintFile;
			this.replaceUnknownClassesMethods = replaceUnknownMethodClasses;
			this.replaceUnknownTemplateClasses = replaceUnknownTemplateClasses;
            if (constraintFile.Length == 0)
                hasConstraintFile = false;

            parseDuneStructre(duneStructureFile);

            varPoints = parseApplicationMainClass(Program.BASE_DIR_DUNE_APPLICATION_MINI_VERSION + Program.MAIN_FILE_DUNE_APPLICATION);

            constraints = parseConstraintFile(constraintFile);

            findAlternatives(varPoints);

            applyConstraints(varPoints, constraints);

        }

        private void findAlternatives(Dictionary<string, VariationPoint> varPoints)
        {
            foreach(VariationPoint vP in varPoints.Values)
            {
                Dictionary<String, DuneFeature> result = ProgramUtil.getAlternativesRecursive(vP.defaultValue);
                vP.alternatives = result;

            }
        }

        private void applyConstraints(Dictionary<string, VariationPoint> varPoints, List<Constraint> constraints)
        {
            foreach (Constraint c in constraints)
            {
                if(c.GetType() == typeof(SimpleConstraint))
                {
                    Dictionary<String, DuneFeature> validAlternative = new Dictionary<string, DuneFeature>();

                    VariationPoint relevantVarPoint = varPoints[c.matchingVarPoint];
                    if (relevantVarPoint != null)
                    {
                        foreach (KeyValuePair<String,DuneFeature> alternative in relevantVarPoint.alternatives)
                        {
                            if (c.isApplicable(alternative.Value))
                            {
                                validAlternative.Add(alternative.Key, alternative.Value);
                            }
                        }
                    }
                    relevantVarPoint.alternatives = validAlternative;

                }
                else if(c.GetType() == typeof(ComplexConstraint))
                {

                }
            }



        }

        private List<Constraint> parseConstraintFile(string constraintFile)
        {
            List<Constraint> constraints = new List<Constraint>();

            try
            {
                string[] lines = System.IO.File.ReadAllLines(constraintFile);

                foreach (string line in lines) {
                    
                    if (line.ToUpper().Contains(SimpleConstraint.SIMPLECONSTRAINT.ToUpper()))
                    {
                        string relevantPart = line.Split(new String[] { SimpleConstraint.SIMPLECONSTRAINT }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                        Program.infoLogger.log("simple constraint: " + relevantPart);
                        constraints.Add(new SimpleConstraint(relevantPart));
                    }
                    else if (line.ToUpper().Contains(ComplexConstraint.COMPLEXCONSTRAINT.ToUpper()))
                    {
                        string relevantPart = line.Split(new String[] { ComplexConstraint.COMPLEXCONSTRAINT }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                        Program.infoLogger.log("complex constraint: " + relevantPart);
                        constraints.Add(new ComplexConstraint(relevantPart));
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return constraints;
        }

        private Dictionary<String, VariationPoint> parseApplicationMainClass(string mainFileCaseStudy)
        {
            Dictionary<String, VariationPoint> varPoints = new Dictionary<String, VariationPoint>();

            StringBuilder newContent = new StringBuilder();

            try
            {
                string[] readText = File.ReadAllLines(mainFileCaseStudy);

                bool nextLineIsDefaultFeature = false;
                VariationPoint lastVarPoint = null;

                for(int i = 0; i < readText.Length; i++)
                {
                    String line = readText[i];

                    if (nextLineIsDefaultFeature)
                    {
                        line = line.Replace(lastVarPoint.defaultValue, lastVarPoint.getIdentifyer());

                        newContent.AppendLine(line);

                        nextLineIsDefaultFeature = false;
                    }
                    else
                    {
                        newContent.AppendLine(line);
                    }

                    if (line.Contains(VariationPoint.VARIATIONPOINTCONSTANT))
                    {
                        string relevantPart = line.Split(new String[] { VariationPoint.VARIATIONPOINTCONSTANT }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                        relevantPart = relevantPart.Substring(1, relevantPart.Length - 2).Trim();

                        Program.infoLogger.log("variant point: "+relevantPart);

                        VariationPoint varPoint = new VariationPoint(relevantPart,i);
                        varPoints.Add(varPoint.Name, varPoint);
                        nextLineIsDefaultFeature = true;
                        lastVarPoint = varPoint;

                    }
                    File.WriteAllText(mainFileCaseStudy, newContent.ToString());
                       
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return varPoints;
        }

        private void parseDuneStructre(string duneStructureFile)
        {
			XMLParser.parse(duneStructureFile, replaceUnknownClassesMethods, replaceUnknownTemplateClasses);
        }


    }
}
