// ----------------------------------------------------------------------- 
// <copyright file="GeometricObject.cs" company="FHWN"> 
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
    /// Base class Geometric Object.
    /// </summary>
    public abstract class GeometricObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeometricObject"/> class.
        /// </summary>
        /// <param name="name">String contains the name of the object.</param>
        /// <param name="border">String contains the border color of the object.</param>
        /// <param name="padding">String contains the padding color of the object.</param>
        /// <param name="left">Integer contains the distance to the left border of the object.</param>
        /// <param name="top">Integer contains the distance to the right border of the object.</param>
        /// <param name="level">Integer contains the Level of the object.</param>
        internal GeometricObject(string name, string border, string padding, int left, int top, int level)
        {
            this.Name = name;
            this.Border = border;
            this.Padding = padding;
            this.Left = left;
            this.Top = top;
            this.Level = level;
            this.Distance = Math.Sqrt((left * left) + (top * top));
        }

        /// <summary>
        /// Gets or sets the value of the border color.
        /// </summary>
        internal string Border { get; set; }

        /// <summary>
        /// Gets or sets the value of the padding color.
        /// </summary>
        internal string Padding { get; set; }

        /// <summary>
        /// Gets or sets the value of distance to the left border.
        /// </summary>
        internal int Left { get; set; }

        /// <summary>
        /// Gets or sets the value of distance to the top border.
        /// </summary>
        internal int Top { get; set; }

        /// <summary>
        /// Gets or sets the value of the level of the object.
        /// </summary>
        internal int Level { get; set; }

        /// <summary>
        /// Gets or sets the value of the name of the object.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the coverage of the object.
        /// </summary>
        internal double Coverage { get; set; }

        /// <summary>
        /// Gets or sets the value of the area of the object.
        /// </summary>
        internal double Area { get; set; }

        /// <summary>
        /// Gets or sets the distance to the left top corner.
        /// </summary>
        internal double Distance { get; set; }

        /// <summary>
        /// Abstract Method to draw objects.
        /// </summary>
        /// <param name="obj">Object which will be drawn.</param>
        internal abstract void Draw(IRenderable obj);
    }
}
