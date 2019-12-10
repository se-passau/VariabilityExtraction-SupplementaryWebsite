﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dune
{
    /// <summary>
    /// The DuneFeature is an interface for the different type of features, namely enum and class.
    /// </summary>
    public abstract class DuneFeature
    {
        /// <summary>
        /// Returns the name of the feature with its template.
        /// </summary>
        /// <returns>the name of the feature</returns>
        public abstract String getFeatureName();

        /// <summary>
        /// Returns the variability of the enum/class.
        /// </summary>
        /// <returns>the whole list of variability that was found by the program</returns>
        public abstract Dictionary<String, DuneFeature> getVariability();

        /// <summary>
        /// Returns the namespace of the feature.
        /// </summary>
        /// <returns>the namespace of the feature</returns>
        public abstract String getNamespace();

        /// <summary>
        /// Returns the reference of the feature.
        /// </summary>
        /// <returns>the reference of the feature</returns>
        public abstract String getReference();

        /// <summary>
        /// Returns the name of the feature without its template.
        /// </summary>
        /// <returns>the name of the feature</returns>
        public abstract String getFeatureNameWithoutTemplate();

        /// <summary>
        /// Returns the name of the feature without its template and its namespace.
        /// </summary>
        /// <returns>the name of the feature</returns>
        public abstract String getFeatureNameWithoutTemplateAndNamespace();


        public TemplateTree tempTree;

        public bool isNotParsable;

    }
}
