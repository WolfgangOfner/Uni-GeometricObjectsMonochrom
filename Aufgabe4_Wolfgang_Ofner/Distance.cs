// ----------------------------------------------------------------------- 
// <copyright file="Distance.cs" company="FHWN"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This program works with geometric objects.</summary> 
// <author>Wolfgang Ofner</author> 
// -----------------------------------------------------------------------
namespace Aufgabe4_Wolfgang_Ofner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to sort objects after their Distance to the left top corner.
    /// </summary>
    internal class Distance : IComparer<GeometricObject>
    {
        /// <summary>
        /// Method to compare the distance of two objects.
        /// </summary>
        /// <param name="object_one">The first object.</param>
        /// <param name="object_two">The second object.</param>
        /// <returns>Sorted objects.</returns>
        public int Compare(GeometricObject object_one, GeometricObject object_two)
        {
            if (object_one.Distance > object_two.Distance)
            {
                return 1;
            }

            if (object_one.Distance < object_two.Distance)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
