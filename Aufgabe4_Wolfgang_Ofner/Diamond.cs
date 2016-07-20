// ----------------------------------------------------------------------- 
// <copyright file="Diamond.cs" company="FHWN"> 
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
    /// Class of the object Diamond.
    /// </summary>
    public class Diamond : GeometricObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Diamond"/> class.
        /// </summary>
        /// <param name="name">String contains the name of the object.</param>
        /// <param name="border_color">String contains the border color of the object.</param>
        /// <param name="padding_color">String contains the padding color of the object.</param>
        /// <param name="left">Integer contains the distance to the left border of the object.</param>
        /// <param name="top">Integer contains the distance to the right border of the object.</param>
        /// <param name="level">Integer contains the Level of the object.</param>
        /// <param name="rows">Odd Integer contains the rows of the object.</param>
        internal Diamond(string name, string border_color, string padding_color, int left, int top, int level, int rows)
            : base(name, border_color, padding_color, left, top, level)
        {
            this.Rows = rows;
            double a = Math.Sqrt(2 * ((rows / (double)2) * (rows / (double)2)));
            this.Coverage = 4 * a;
            this.Area = rows * (double)rows / 2;
        }

        /// <summary>
        /// Gets or sets the value of the rows of the object.
        /// </summary>
        internal int Rows { get; set; }

        /// <summary>
        /// Method to draw diamonds.
        /// </summary>
        /// <param name="diamond">Object which will be drawn.</param>
        internal override void Draw(IRenderable diamond)
        {
            diamond.DrawDiamond(this);
        }
    }
}
