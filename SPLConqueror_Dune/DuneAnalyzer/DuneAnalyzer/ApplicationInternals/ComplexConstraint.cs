using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dune
{
    class ComplexConstraint : Constraint
    {

        public static string COMPLEXCONSTRAINT = "COMPLEXCONSTRAINT";
        private string line;

        public ComplexConstraint(string line)
        {
            this.line = line;

            String[] differentParts = line.Split(';');
            String[] target = differentParts[0].Split('.');

            matchingVarPoint = target[0];

        }

        /// <inheritdoc />
        public override bool isApplicable(DuneFeature value)
        {
            throw new NotImplementedException();
        }
    }
}
