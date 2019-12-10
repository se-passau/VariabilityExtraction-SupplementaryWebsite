using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dune
{
    class SimpleConstraint : Constraint
    {

        public static string SIMPLECONSTRAINT = "SIMPLECONSTRAINT";
        private string line;

        private string valueToMatch;

        public SimpleConstraint(string line)
        {
            this.line = line;

            String[] differentParts = line.Split(';');
            String[] target = differentParts[0].Split('.');

            connection = new string[target.Length - 2];

            this.target = target[target.Length - 1];

            matchingVarPoint = target[0];
            Array.Copy(target, 1, connection, 0, target.Length - 2);

            valueToMatch = differentParts[1];

        }

        /// <inheritdoc />
        public override bool isApplicable(DuneFeature value)
        {
            if (value.GetType() != typeof(DuneClass) && value.GetType() != typeof(DuneVariable))
                return false;


            // go over connection to indentify the innerClass/member variable with the constraint
            foreach (String con in connection)
            {
                if (value.GetType() == typeof(DuneClass))
                    value = ((DuneClass)value).getInnerElement(con);
                //else if (value.GetType() == typeof(DuneVariable))
                //   value = ((DuneVariable)value).getInnerElement(con);

                if (value == null)
                    return false;

            }

            DuneFeature targetFeature = ((DuneClass)value).getInnerElement(target);

            if (targetFeature.GetType() == typeof(DuneVariable))
            {
                return ((DuneVariable)targetFeature).InitialValue.Value.Equals(this.valueToMatch);
            }


            return false;
        }
        
    }
}
