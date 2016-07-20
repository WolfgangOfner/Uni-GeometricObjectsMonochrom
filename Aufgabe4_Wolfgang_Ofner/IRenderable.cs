// ----------------------------------------------------------------------- 
// <copyright file="IRenderable.cs" company="FHWN"> 
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
    /// Interface for drawing objects.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Method to draw a circle.
        /// </summary>
        /// <param name="circle">Object which will be drawn.</param>
        void DrawCircle(Circle circle);

        /// <summary>
        /// Method to draw a circle.
        /// </summary>
        /// <param name="diamond">Object which will be drawn.</param>
        void DrawDiamond(Diamond diamond);

        /// <summary>
        /// Method to draw a circle.
        /// </summary>
        /// <param name="rectangle">Object which will be drawn.</param>
        void DrawRectangle(Rectangle rectangle);
    }
}
