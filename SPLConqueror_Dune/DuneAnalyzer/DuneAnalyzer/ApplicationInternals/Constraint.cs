using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dune
{
    abstract class Constraint
    {
        public string matchingVarPoint = "";

        public string[] connection;

        public string target;

        /// <summary>
        /// Tests whether the provided DuneFeature object is valid for the constraint.
        /// </summary>
        /// <param name="value">The DuneFeature to validate.</param>
        /// <returns>TRUE if the DuneFeature is valid for the constraint, FALSE otherwise.</returns>
        public abstract bool isApplicable(DuneFeature value);
        
    }
}
