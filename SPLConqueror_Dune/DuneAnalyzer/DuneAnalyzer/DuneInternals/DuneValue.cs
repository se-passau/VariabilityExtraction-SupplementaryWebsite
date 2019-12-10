using System;
using System.Collections.Generic;

namespace Dune
{
    /// <summary>
    /// This class represents a value of a variable. 
    /// </summary>
    public class DuneValue : DuneFeature
    {
        private string value;

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public DuneValue (string value)
        {
            this.value = value.Replace("=", "").Trim();
        }

        private void parseValue(string value)
        {
            
        }

        public override string getFeatureName()
        {
            return String.Empty;
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
            Dictionary<string, DuneFeature> result = new Dictionary<string, DuneFeature>();
            result.Add("value", this);
            return result;
        }
    }
}

