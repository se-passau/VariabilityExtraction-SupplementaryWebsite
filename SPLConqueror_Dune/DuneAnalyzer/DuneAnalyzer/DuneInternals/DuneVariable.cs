using System;
using System.Collections.Generic;

namespace Dune
{
    /// <summary>
    /// This class represents a variable in dune (e.g., a constaint variable within a struct)
    /// </summary>
    public class DuneVariable : DuneFeature
    {
        // The name of the variable
        private String variableName;
        // The initial value of the variable
        private DuneValue initialValue;
        private String type;

        internal DuneValue InitialValue
        {
            get
            {
                return initialValue;
            }

            set
            {
                initialValue = value;
            }
        }

        /// <summary>
        /// The constructor of this class creates a new variable by its variable and its initial value.
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="initialValue"></param>
        public DuneVariable (String variableName, String type, String definition, DuneValue initialValue)
        {
            this.variableName = variableName;
            this.type = type;
            this.initialValue = initialValue;
        }

        public override string getFeatureName()
        {
            return variableName;
        }

        public override string getFeatureNameWithoutTemplate()
        {
            return getFeatureName();
        }

        public override string getFeatureNameWithoutTemplateAndNamespace()
        {
            return getFeatureName();
        }

        public override string getNamespace()
        {
            return String.Empty;
        }

        public override string getReference()
        {
            return String.Empty;
        }

        public override Dictionary<string, DuneFeature> getVariability()
        {
            Dictionary<string, DuneFeature> variability = new Dictionary<string, DuneFeature>();
            variability.Add("value", this.initialValue);
            return variability;
        }
    }
}

