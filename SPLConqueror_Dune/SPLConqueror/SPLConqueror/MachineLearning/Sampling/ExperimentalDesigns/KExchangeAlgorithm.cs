﻿using System;
using System.Collections.Generic;
using System.Linq;
using ILNumerics;
using Accord.Math;
using SPLConqueror_Core;

namespace MachineLearning.Sampling.ExperimentalDesigns
{
    public class KExchangeAlgorithm : ExperimentalDesign
    {

        private double[,] matrix;
        private int sampleSize;

        private Dictionary<NumericOption, int> numberOfLevels;

        private int k;
        private bool rescale = false;
        private double epsilon = 1E-5;

        private Dictionary<NumericOption, int> optionToIndex = new Dictionary<NumericOption, int>();
        private Dictionary<NumericOption, List<double>> optionToValues = new Dictionary<NumericOption, List<double>>();

        public override string getName()
        {
            return "KEXCHANGE";
        }

        public override string getTag()
        {
            return "KEX";
        }

        public KExchangeAlgorithm(List<NumericOption> options)
            : base(options)
        {
        }

        public KExchangeAlgorithm(int sampleSize = 100, int k = 5)
        {
            this.sampleSize = sampleSize;
            this.k = k;
        }

        public override bool computeDesign()
        {
            return compute(this.sampleSize, this.k);
        }

        public override void setSamplingParameters(Dictionary<string, string> parameterNameToValue)
        {
            if (parameterNameToValue.ContainsKey("sampleSize"))
            {
                sampleSize = parseFromParameters(parameterNameToValue, "sampleSize");
            }
            if (parameterNameToValue.ContainsKey("k"))
            {
                k = parseFromParameters(parameterNameToValue, "k");
            }
        }

        public bool compute(int _sampleSize, int _k)
        {
            if (options.Count == 0)
                return false;

            // set number of possible levels for each VariableFeature
            numberOfLevels = new Dictionary<NumericOption, int>();
            foreach (NumericOption numOption in options)
            {
                optionToIndex.Add(numOption, optionToIndex.Count);

                List<double> allValuesOfOption = numOption.getAllValues();
                optionToValues.Add(numOption, allValuesOfOption);

                int posLevels = allValuesOfOption.Count;

                if (!rescale || posLevels <= 4) // include all levels if # <= 4
                {
                    numberOfLevels.Add(numOption, posLevels);
                }
                else
                {
                    numberOfLevels.Add(numOption, Convert.ToInt32(4 + Math.Round(Math.Sqrt(posLevels) / 2.0))); // rescale
                }
            }

            // get full factorial design
            double[,] fullFactorial = getFullFactorial(numberOfLevels);

            fullFactorial = filterByNonBooleanconstraints(fullFactorial);

            // create initial random design
            Random rnd = new Random(1);
            Dictionary<int, int> usedCandidates = new Dictionary<int, int>();
            matrix = new double[_sampleSize, options.Count];

            // chose initial candidates from full factorial
            do
            {
                if (usedCandidates.Count >= _sampleSize)
                    break;

                int candidate = rnd.Next(0, fullFactorial.GetLength(0));

                if (!usedCandidates.Values.Contains(candidate))
                {
                    int tmp = usedCandidates.Count;
                    setRowOfMatrixTo(matrix, tmp, fullFactorial, candidate);
                    usedCandidates.Add(tmp, candidate);
                }
            } while (true);

            // Calculate dispersion matrix
            ILArray<double> dispersion = calculateDispersion(matrix);

            // start of k-Exchange algorithm
            bool couplesWithPositiveDelta = true;


            while (couplesWithPositiveDelta)
            {

                Dictionary<int, double> variances = new Dictionary<int, double>();
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    variances.Add(i, calcVarianceForRunIL(getRowFromMatrix(matrix, i), dispersion));
                }

                couplesWithPositiveDelta = false;

                foreach (KeyValuePair<int, double> v in variances.OrderByDescending(p => p.Value).Take(_k))
                {

                    // Get deltas for exchange
                    Dictionary<int, double> deltaF = getAllDeltas(usedCandidates[v.Key], fullFactorial);

                    // Get rid of duplicates
                    deltaF = deltaF.Where(c => !usedCandidates.Values.Contains(c.Key)).ToDictionary(dict => dict.Key, dict => dict.Value);

                    // Get exchange candidates
                    KeyValuePair<int, double> eF = deltaF.OrderByDescending(p => p.Value).First();

                    if (eF.Value > epsilon)
                    {
                        setRowOfMatrixTo(matrix, v.Key, fullFactorial, eF.Key);
                        usedCandidates[v.Key] = eF.Key;
                        dispersion = calculateDispersion(matrix);
                        couplesWithPositiveDelta = true;
                    }
                }

            }

            // map configuration to valid values that can be passed to the software
            Dictionary<Tuple<NumericOption, int>, double> values = new Dictionary<Tuple<NumericOption, int>, double>();
            foreach (NumericOption opt in options)
            {
                int maxVal = numberOfLevels[opt] - 1;
                double delta = opt.Max_value - opt.Min_value;

                for (int i = 0; i <= maxVal; i++)
                {
                    double value = opt.Min_value + (i / (double)maxVal) * delta;
                    value = opt.nearestValidValue(value);
                    values.Add(Tuple.Create(opt, i), value);
                }
            }

            List<Dictionary<NumericOption, double>> configs = new List<Dictionary<NumericOption, double>>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Dictionary<NumericOption, double> run = new Dictionary<NumericOption, double>();

                int j = 0;
                foreach (NumericOption vf in options)
                {
                    run.Add(vf, values[Tuple.Create(vf, Convert.ToInt32(matrix[i, j]))]);
                    j++;
                }

                configs.Add(run);
            }

            this.selectedConfigurations = configs;

            return true;
        }

        private double[,] filterByNonBooleanconstraints(double[,] fullFactorial)
        {
            List<double[]> filteredConfigurations = new List<double[]>();

            for (int i = 0; i < fullFactorial.GetLength(0); i++)
            {
                double[] row = fullFactorial.GetRow<double>(i);
                Dictionary<NumericOption, double> config = new Dictionary<NumericOption, double>();

                foreach (KeyValuePair<NumericOption, int> option in optionToIndex)
                {
                    config.Add(option.Key, optionToValues[option.Key][(int)row[(int)option.Value]]);
                }
                if (GlobalState.varModel.NonBooleanConstraints.TrueForAll(x => x.configIsValid(config)))
                {
                    filteredConfigurations.Add(row);
                }
            }

            if (filteredConfigurations.Count == 0)
                return new double[0, 0];

            double[,] filteredAsArray = new double[filteredConfigurations.Count, filteredConfigurations[0].Length];

            for (int i = 0; i < filteredConfigurations.Count; i++)
            {
                for (int j = 0; j < filteredConfigurations[0].Length; j++)
                {
                    filteredAsArray[i, j] = filteredConfigurations[i][j];
                }
            }

            return filteredAsArray;
        }

        // generate all possible run configurations - plagiarized from FullFactorialDesign
        private double[,] getFullFactorial(Dictionary<NumericOption, int> numberOfLevels)
        {
            double[][] fullFactorial = new double[Convert.ToInt32(numberOfLevels.Values.Aggregate((a, x) => a * x))][];

            Dictionary<NumericOption, List<int>> elementValuePairs = new Dictionary<NumericOption, List<int>>();
            foreach (NumericOption vf in numberOfLevels.Keys)
            {
                elementValuePairs[vf] = Enumerable.Range(0, numberOfLevels[vf]).ToList();
            }

            int[] positions = new int[numberOfLevels.Count];

            int featureToIncrement = 0;
            bool notIncremented = true;

            int iterator = 0;

            do
            {
                notIncremented = true;

                // tests whether all combinations are computed
                if (featureToIncrement == numberOfLevels.Count)
                {
                    double[,] tmp = new double[fullFactorial.Length, fullFactorial[0].Length];
                    for (int i = 0; i < fullFactorial.Length; i++)
                    {
                        for (int j = 0; j < fullFactorial[0].Length; j++)
                        {
                            tmp[i, j] = fullFactorial[i][j];
                        }
                    }

                    return tmp;
                }
                fullFactorial[iterator] = generateRow(elementValuePairs, positions);
                iterator++;

                do
                {
                    if (positions[featureToIncrement] == numberOfLevels.ElementAt(featureToIncrement).Value - 1)
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
                } while (notIncremented && !(featureToIncrement == numberOfLevels.Count));
            } while (true);
        }

        private double[] generateRow(Dictionary<NumericOption, List<int>> numberOfLevels, int[] positions)
        {
            double[] run = new double[numberOfLevels.Count];

            for (int i = 0; i < numberOfLevels.Count; i++)
            {
                run[i] = numberOfLevels.ElementAt(i).Value[positions[i]];
            }
            return run;
        }

        private ILArray<double> calculateDispersion(double[,] matrix)
        {
            ILArray<double> tmp = matrix;
            return ((double[,])MachineLearning.Learning.Regression.FeatureSubsetSelection.toSystemMatrix<double>((ILMath.multiply(tmp, tmp.T)))).PseudoInverse();
        }

        private double[] getRowFromMatrix(double[,] matrix, int row)
        {
            double[] tmp = new double[matrix.GetLength(1)];
            int offset = 8 * matrix.GetLength(1) * row;
            int blocksize = 8 * matrix.GetLength(1);
            System.Buffer.BlockCopy(matrix, offset, tmp, 0, blocksize);

            return tmp;
        }

        private void setRowOfMatrixTo(double[,] matrix1, int row1, double[,] matrix2, int row2)
        {
            int offset1 = 8 * matrix1.GetLength(1) * row1;
            int offset2 = 8 * matrix1.GetLength(1) * row2;
            int blocksize = 8 * matrix1.GetLength(1);
            System.Buffer.BlockCopy(matrix2, offset2, matrix1, offset1, blocksize);
        }

        private double calcVarianceForRun(double[] run, double[,] candidateMatrix)
        {
            ILArray<double> vector = run;
            ILArray<double> matrix = candidateMatrix;

            ILArray<double> matrixTimes = ILMath.multiply(matrix, matrix.T);



            ILArray<double> inverse = ((double[,])MachineLearning.Learning.Regression.FeatureSubsetSelection.toSystemMatrix<double>(matrixTimes)).PseudoInverse();

            return (double)ILMath.multiply(ILMath.multiply(vector.T, inverse), vector);
        }

        private double calcVarianceForRunIL(double[] run, ILArray<double> dispersion)
        {
            ILArray<double> vector = run;
            return (double)ILMath.multiply(ILMath.multiply(vector.T, dispersion), vector);
        }

        private double calcVarianceFunction(double[] xi, double[] xj, double[,] matrix)
        {
            ILArray<double> xiIL = xi;
            ILArray<double> xjIL = xj;
            ILArray<double> matrixIL = matrix;
            ILArray<double> inverse = ((double[,])MachineLearning.Learning.Regression.FeatureSubsetSelection.toSystemMatrix<double>((ILMath.multiply(matrixIL, matrixIL.T)))).PseudoInverse();

            return (double)ILMath.multiply(ILMath.multiply(xiIL.T, inverse), xjIL);
        }

        private double calcVarianceFunctionIL(double[] xi, double[] xj, ILArray<double> dispersion)
        {
            ILArray<double> xiIL = xi;
            ILArray<double> xjIL = xj;
            return (double)ILMath.multiply(ILMath.multiply(xiIL.T, dispersion), xjIL);
        }

        private double calcDelta(double[] xi, double[] xj, double[,] matrix)
        {
            double dxi = calcVarianceForRun(xi, matrix);
            double dxj = calcVarianceForRun(xj, matrix);
            double dxixj = calcVarianceFunction(xi, xj, matrix);
            return dxj - ((dxi * dxj) - Math.Pow(dxixj, 2)) - dxi;
        }

        private double calcDeltaIL(double[] xi, double[] xj, ILArray<double> dispersion)
        {
            double dxi = calcVarianceForRunIL(xi, dispersion);
            double dxj = calcVarianceForRunIL(xj, dispersion);
            double dxixj = calcVarianceFunctionIL(xi, xj, dispersion);
            return dxj - ((dxi * dxj) - Math.Pow(dxixj, 2)) - dxi;
        }

        private Dictionary<int, double> getAllDeltas(int row, double[,] matrix)
        {
            Dictionary<int, double> deltas = new Dictionary<int, double>();
            double[] vector = getRowFromMatrix(matrix, row);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i == row)
                    continue;

                deltas.Add(i, calcDelta(vector, getRowFromMatrix(matrix, i), matrix));
            }

            return deltas;
        }

        public override string parameterIdentifier()
        {
            return "sampleSize-" + sampleSize + "_" + "k-" + k + "_";
        }
    }
}
