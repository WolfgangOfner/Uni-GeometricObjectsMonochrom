// ----------------------------------------------------------------------- 
// <copyright file="Area.cs" company="FHWN"> 
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
    /// Class to sort objects after their area.
    /// </summary>
    internal class Area : IComparer<GeometricObject>
    {
        /// <summary>
        /// Method to compare the area of two objects.
        /// </summary>
        /// <param name="object_one">First object to compare.</param>
        /// <param name="object_two">Second object to compare.</param>
        /// <returns>Sorted objects.</returns>
        public int Compare(GeometricObject object_one, GeometricObject object_two)
        {
            if (object_one.Area > object_two.Area)
            {
                return 1;
            }

            if (object_one.Area < object_two.Area)
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