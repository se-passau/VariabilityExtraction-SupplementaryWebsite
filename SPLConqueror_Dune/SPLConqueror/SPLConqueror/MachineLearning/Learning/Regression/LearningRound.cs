﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPLConqueror_Core;

namespace MachineLearning.Learning.Regression
{
    public class LearningRound
    {
        public double learningError = Double.MaxValue;
        public double validationError = Double.MaxValue;
        public double learningError_relative = Double.MaxValue;
        public double validationError_relative = Double.MaxValue;
        private List<Feature> featureSet = new List<Feature>();
        public List<Feature> FeatureSet
        {
            get { return featureSet; }
            set { featureSet = value; }
        }
        public int round = 0;
        public TimeSpan elapsedTime = new TimeSpan(0);
        public double modelComplexity
        {
            get
            {
                double complexity = 0;
                foreach (var feature in featureSet)
                {
                    complexity += feature.getNumberOfParticipatingOptions();
                }
                return complexity;
            }
        }
        public Feature bestCandidate = null;
        public int bestCandidateSize = 1;
        public double bestCandidateScore = 0;
        public string terminationReason = null;

        /// <summary>
        /// Prints the information learned in a round.
        /// </summary>
        /// <returns>All relevant information of the current round as string.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(round + ";");
            for (int i = 0; i < featureSet.Count; i++)
            {
                Feature f = featureSet[i];
                sb.Append(f.ToString());
                if (i < featureSet.Count - 1)
                    sb.Append(" + ");
            }
            sb.Append(";");

            sb.Append(learningError + ";");
            sb.Append(learningError_relative + ";");
            sb.Append(validationError + ";");
            sb.Append(validationError_relative + ";");
            sb.Append(elapsedTime.TotalSeconds + ";");
            sb.Append(modelComplexity + ";");
            sb.Append(bestCandidate + ";");
            sb.Append(bestCandidateSize + ";");
            sb.Append(bestCandidateScore + ";");
            //sb.Append(string.Format("{0};", ));

            return sb.ToString();
        }


        /// <summary>
        /// Parse string representation of a learning round to a LearningRound object.
        /// </summary>
        /// <param name="learningRoundAsString">LearningRound as string.</param>
        /// <param name="vm">Variability model the LearningRound belongs to.</param>
        /// <returns>LearningRound object that has the data of the string representation.</returns>
        public static LearningRound FromString(string learningRoundAsString, VariabilityModel vm)
        {
            LearningRound learningRound = new LearningRound();
            string[] data = learningRoundAsString.Split(new char[] { ';' });
            learningRound.round = int.Parse(data[0].Trim());
            List<Feature> featureSetFromString = new List<Feature>();
            string[] featureExpressions = data[1].Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string featureExpression in featureExpressions)
            {
                Feature toAdd = new Feature(featureExpression.Split(new char[] { '*' }, 2)[1], vm);
                toAdd.Constant = double.Parse(featureExpression.Split(new char[] { '*' }, 2)[0].Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-us"));
                featureSetFromString.Add(toAdd);
            }
            learningRound.featureSet = featureSetFromString;
            learningRound.learningError = double.Parse(data[2].Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            learningRound.learningError_relative = double.Parse(data[3].Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            learningRound.validationError = double.Parse(data[4].Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            learningRound.validationError_relative = double.Parse(data[5].Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            learningRound.elapsedTime = TimeSpan.FromSeconds(double.Parse(data[6].Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-us")));
            Feature bestCandidateFromString = new Feature(data[8], vm);
            learningRound.bestCandidate = bestCandidateFromString;
            learningRound.bestCandidateSize = int.Parse(data[9].Trim());
            try
            {
                learningRound.bestCandidateScore = double.Parse(data[10].Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            }
            catch (OverflowException overF)
            {
                GlobalState.logError.logLine("Error in analysing of the learning round.");
                GlobalState.logError.logLine(overF.Source + " -> " + overF.Message);
                learningRound.bestCandidateScore = Double.MaxValue;
            }
            return learningRound;
        }

        internal LearningRound(List<Feature> featureSet, double learningError, double validationError, int round)
        {
            this.featureSet = featureSet;
            this.learningError = learningError;
            this.validationError = validationError;
            this.round = round;
        }

        internal LearningRound() { }

    }
}
