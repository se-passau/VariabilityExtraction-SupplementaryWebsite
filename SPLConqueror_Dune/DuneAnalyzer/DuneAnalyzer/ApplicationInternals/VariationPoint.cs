using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dune
{

    /// <summary>
    /// Representation of variation points being defined in the applications.
    /// 
    /// See example:
    /// \\VARIATIONPOINT(dimensionality; const int; dim; 2)
    /// -- the elements are 
    /// --- name of the variation point
    /// --- type of the element in the application
    /// --- name of the variable used in the application
    /// --- default value
    /// 
    /// </summary>
    public class VariationPoint
    {

        public static string VARIATIONPOINTCONSTANT = "VARIATIONPOINT";

        private string name;
        private string varNameInApplication;
        public string defaultValue;

        public Dictionary<String, DuneFeature> alternatives = new Dictionary<String, DuneFeature>();

        public int line;


        private DuneFeature defaultClass;

        private bool hasDuneClass = true;


        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public VariationPoint(String content, int _line)
        {
            string[] parts = content.Split(Program.VARPOINT_SPLIT_SYMBOL);
            name = parts[0];
            varNameInApplication = parts[1];
            defaultValue = parts[2];

            line = _line;

            //identifyDuneClass(type);

        }

        internal string getIdentifyer()
        {
            return name + "_VarPoint_" + line;
        }

        //private void identifyDuneClass(string type)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
