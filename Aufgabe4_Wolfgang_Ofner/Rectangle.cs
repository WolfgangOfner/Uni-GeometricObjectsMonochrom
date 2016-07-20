// ----------------------------------------------------------------------- 
// <copyright file="Rectangle.cs" company="FHWN"> 
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
    /// Class for rectangles.
    /// </summary>
    public class Rectangle : GeometricObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="name">String contains the name of the object.</param>
        /// <param name="border">String contains the border color of the object.</param>
        /// <param name="padding">String contains the padding color of the object.</param>
        /// <param name="left">Integer contains the distance to the left border of the object.</param>
        /// <param name="top">Integer contains the distance to the right border of the object.</param>
        /// <param name="level">Integer contains the Level of the object.</param>
        /// <param name="length">String contains the length of the object.</param>
        /// <param name="width">String contains the width of the object.</param>
        internal Rectangle(string name, string border, string padding, int left, int top, int level, int length, int width)
            : base(name, border, padding, left, top, level)
        {
            this.Height = length;
            this.Width = width;
            this.Coverage = (length + width) * 2;
            this.Area = length * width;
        }

        /// <summary>
        /// Gets or sets the value of the height of the object.
        /// </summary>
        internal int Height { get; set; }

        /// <summary>
        /// Gets or sets the value of the width of the object.
        /// </summary>
        internal int Width { get; set; }

        /// <summary>
        /// Method to draw a rectangle.
        /// </summary>
        /// <param name="rectangle">Object which will be drawn.</param>
        internal override void Draw(IRenderable rectangle)
        {
            rectangle.DrawRectangle(this);
        }
    }
}