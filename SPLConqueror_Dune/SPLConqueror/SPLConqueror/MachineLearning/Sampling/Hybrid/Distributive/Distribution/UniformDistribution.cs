﻿using System.Collections.Generic;

namespace MachineLearning.Sampling.Hybrid.Distributive
{
    /// <summary>
    /// This class represents the uniform distribution, where it is equally likely to pick one of all buckets.
    /// </summary>
    public class UniformDistribution : IDistribution
    {
        /// <summary>
        /// See <see cref="Distribution.CreateDistribution(List{double})"/>.
        /// </summary>
        public Dictionary<double, double> CreateDistribution(List<double> allBuckets)
        {
            Dictionary<double, double> result = new Dictionary<double, double>();

            double probabilityPerBucket = 1.0 / allBuckets.Count;

            foreach (double d in allBuckets)
            {
                result[d] = probabilityPerBucket;
            }

            return result;
        }

        /// <summary>
        /// See <see cref="Distribution.GetName"/>.
        /// </summary>
        public string GetName()
        {
            return "UNIFORM";
        }

    }
}
