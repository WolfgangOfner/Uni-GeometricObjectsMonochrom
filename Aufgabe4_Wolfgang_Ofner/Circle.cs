// ----------------------------------------------------------------------- 
// <copyright file="Circle.cs" company="FHWN"> 
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
    /// Class of the object circle.
    /// </summary>
    public class Circle : GeometricObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="name">String contains the name of the object.</param>
        /// <param name="border_color">String contains the border color of the object.</param>
        /// <param name="padding_color">String contains the padding color of the object.</param>
        /// <param name="left">Integer contains the distance to the left border of the object.</param>
        /// <param name="top">Integer contains the distance to the right border of the object.</param>
        /// <param name="level">Integer contains the Level of the object.</param>
        /// <param name="radius">Integer contains the radius of the object.</param>
        internal Circle(string name, string border_color, string padding_color, int left, int top, int level, int radius)
            : base(name, border_color, padding_color, left, top, level)
        {
            this.Radius = radius;
            this.Coverage = 2 * radius * Math.PI;
            this.Area = radius * radius * Math.PI;
        }

        /// <summary>
        /// Gets or sets the value of the radius of the object.
        /// </summary>
        internal int Radius { get; set; }

        /// <summary>
        /// Method to draw circles.
        /// </summary>
        /// <param name="circle">Object which will be drawn.</param>
        internal override void Draw(IRenderable circle)
        {
            circle.DrawCircle(this);
        }
    }
}