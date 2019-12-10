﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPLConqueror_Core
{
    /// <summary>
    /// Representation of the non-functional property that is considered in the experiements.
    /// </summary>
    public class NFProperty : IEquatable<NFProperty>
    {
        private String name;

        /// <summary>
        /// The name of the non-functional property. This name should be unique among all considered non-functional properties.
        /// </summary>
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Creates a new property with the given name.
        /// </summary>
        /// <param name="NFPname">Name of the property.</param>
        public NFProperty(string NFPname)
        {
            this.name = NFPname;
        }

        /// <summary>
        /// Returns the string representation of the property. 
        /// </summary>
        /// <returns>The name of the property.</returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Returns a hash code of this property. The code is computed based on the name of the property. 
        /// </summary>
        /// <returns>The hash code of this property. </returns>
        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }

        /// <summary>
        /// Compares two properties based on the names of the properties. 
        /// </summary>
        /// <param name="other">The property to compare with.</param>
        /// <returns>True if both properties have the same name.</returns>
        public bool Equals(NFProperty other)
        {
            return this.name.Equals(other.name);
        }


        static NFProperty defaultProp = new NFProperty("default");

        /// <summary>
        /// Returns the default NFP-property, whose name is 'default'.
        /// </summary>
        public static NFProperty DefaultProperty { get { return defaultProp; } }
    }
}
