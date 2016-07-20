// ----------------------------------------------------------------------- 
// <copyright file="MonochromRenderer.cs" company="FHWN"> 
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
    /// Class to draw objects in black and white.
    /// </summary>
    internal class MonochromRenderer : IRenderable
    {
        /// <summary>
        /// Method to draw circles.
        /// </summary>
        /// <param name="circle">Object which will be drawn.</param>
        public void DrawCircle(Circle circle)
        {
            string[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));
            int border = 0;
            int padding = 0;
            int radius = circle.Radius;

            for (int i = 0; i < 16; i++)
            {                                                                       // takes the index of the needed border color
                if (circle.Border.ToUpper().Equals(colorNames[i].ToUpper()))
                {
                    border = i;
                    break;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                if (circle.Padding.ToUpper().Equals(colorNames[i].ToUpper()))
                {                                                                   // takes the index of the needed padding color
                    padding = i;
                    break;
                }
            }

            int printed_objects = 0;                                                    // counts how many X are printed in one row
            bool printed_all_objects = false;                                           // true if printed objects == 2* radius - 1
            int set_rows = 0;

            if (radius == 2)
            {
                for (int i = 0; i <= radius; i++)
                {
                    for (int j = 0; j <= 2 * radius; j++)
                    {
                        Console.SetCursorPosition(circle.Left + j, circle.Top + i);

                        if (j == 2 - i || j == 2 + i)
                        {
                            Color(border);
                            Console.WriteLine("#");
                        }

                        if (j > radius - i && j < radius + i)
                        {
                            Color(padding);
                            Console.WriteLine("#");
                        }
                    }

                    if (i == 2)
                    {
                        int distance_to_middle = 1;
                        for (int ii = i + 1; ii <= 2 * radius; ii++)
                        {
                            for (int jj = 0; jj <= 2 * radius; jj++)
                            {
                                Console.SetCursorPosition(circle.Left + jj, circle.Top + ii);
                                if (jj == radius - distance_to_middle || jj == distance_to_middle + radius)
                                {
                                    Color(border);
                                    Console.WriteLine("#");
                                }

                                if (jj > radius - distance_to_middle && jj < distance_to_middle + radius)
                                {
                                    Color(padding);
                                    Console.WriteLine("#");
                                }
                            }

                            distance_to_middle--;
                        }
                    }
                }
            }
            else
            {
                // top down inclusive the middle
                for (int i = 0; i < (2 * radius) - 3; i++)
                {
                    set_rows = i;
                    if (printed_objects != (2 * radius) - 1)
                    {                                                                       // resets ptrinted objects to 0 if the last rows doesn't contain 2 * radius -1 objects, needed not to gain a diamond                                           
                        printed_objects = 0;
                    }
                    else
                    {                                                                       // else bool = true, to know that this is the maximum size of the row length
                        printed_all_objects = true;
                    }

                    for (int j = 0; j < (2 * radius) + 1; j++)
                    {
                        Console.SetCursorPosition(circle.Left + j, circle.Top + i);
                        if (printed_all_objects == true)
                        {
                            if (j == 0 || j == (2 * radius))
                            {                                                               // prints the border at the same position around the middle to gain a cirlce and not a diamond
                                Color(border);
                                Console.WriteLine("#");
                            }

                            if (j > 0 && j < 2 * radius)
                            {                                                               // if curser is between the border print padding
                                Color(padding);
                                Console.WriteLine("#");
                            }

                            if (radius == 4 && i == (2 * radius) - 4)
                            {
                                set_rows = 5;
                                Console.SetCursorPosition(circle.Left + j, circle.Top + set_rows);

                                if (j == 0 || j == (2 * radius))
                                {                                                               // prints the border at the same position around the middle to gain a cirlce and not a diamond
                                    Color(border);
                                    Console.WriteLine("#");
                                }

                                if (j > 0 && j < 2 * radius)
                                {                                                               // if curser is between the border print padding
                                    Color(padding);
                                    Console.WriteLine("#");
                                }
                            }
                        }
                        else
                        {
                            if (j == radius - (radius / 2) - i || j == radius + (radius / 2) + i)
                            {                                                                               // prints the border, steps every row one step futher away from the middle
                                Color(border);
                                printed_objects++;
                                Console.WriteLine("#");
                            }

                            if (j > radius - (radius / 2) - i && j < radius + (radius / 2) + i)
                            {                                                                               // if curser is between the border
                                if (i == 0)
                                {                                                                           // border from the first row
                                    Color(border);
                                }
                                else
                                {
                                    Color(padding);
                                }

                                printed_objects++;                                                          // contains how many objects were printed in the row
                                Console.WriteLine("#");
                            }
                        }
                    }
                }

                if (radius % 2 == 1)
                {
                    if (radius == 3)
                    {                                                                       // two additional rows are needed around the middle
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < (2 * radius) + 1; j++)
                            {
                                Console.SetCursorPosition(circle.Left + j, circle.Top + set_rows + i + 1);
                                if (j == 0 || j == (2 * radius))
                                {                                                               // prints the border at the same position around the middle to gain a cirlce and not a diamond
                                    Color(border);
                                    Console.WriteLine("#");
                                }

                                if (j > 0 && j < 2 * radius)
                                {                                                               // if curser is between the border print padding
                                    Color(padding);
                                    Console.WriteLine("#");
                                }
                            }
                        }

                        set_rows += 2;                                                       // increases set_rows by 2 because of the one additional row which is needed with radius == 3
                    }

                    if (radius == 5)
                    {                                                                       // one additional row is needed around the middle
                        for (int j = 0; j < (2 * radius) + 1; j++)
                        {
                            Console.SetCursorPosition(circle.Left + j, circle.Top + set_rows + 1);
                            if (j == 0 || j == (2 * radius))
                            {                                                               // prints the border at the same position around the middle to gain a cirlce and not a diamond
                                Color(border);
                                Console.WriteLine("#");
                            }

                            if (j > 0 && j < 2 * radius)
                            {                                                               // if curser is between the border print padding
                                Color(padding);
                                Console.WriteLine("#");
                            }
                        }

                        set_rows++;                                                         // increases set_rows by 1 because of the one additional row which is needed with radius == 5
                    }

                    // after the rows around the middle down to the bottom
                    for (int i = 0; i <= radius / 2; i++)
                    {
                        for (int j = 0; j < 2 * radius; j++)
                        {
                            Console.SetCursorPosition(circle.Left + j, circle.Top + set_rows + i + 1);

                            if (j > i + 1 && j < (2 * radius) - i)
                            {                                                                               // if curser is between the border
                                if (i == radius / 2 || j == (2 * radius) - i - 1)
                                {                                                                           // border from the first row
                                    Color(border);
                                }
                                else
                                {
                                    Color(padding);
                                }

                                Console.WriteLine("#");
                            }

                            if (j == i + 1)
                            {
                                Color(border);
                                Console.WriteLine("#");
                            }
                        }
                    }
                }
                else
                {       // after the rows around the middle down to the bottom
                    for (int i = 0; i < radius / 2; i++)
                    {
                        for (int j = 0; j < 2 * radius; j++)
                        {
                            Console.SetCursorPosition(circle.Left + j, circle.Top + set_rows + i + 1);
                            if (j > i + 1 && j < (2 * radius) - i)
                            {                                                                               // if curser is between the border
                                if (i == (radius / 2) - 1 || j == (2 * radius) - i - 1)
                                {                                                                           // border from the first row
                                    Color(border);
                                }
                                else
                                {
                                    Color(padding);
                                }

                                Console.WriteLine("#");
                            }

                            if (j == i + 1)
                            {
                                Color(border);
                                Console.WriteLine("#");
                            }
                        }
                    }
                }
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Method to draw rectangles.
        /// </summary>
        /// <param name="rectangle">Object which will be drawn.</param>
        public void DrawRectangle(Rectangle rectangle)
        {
            string[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));
            int border = 0;
            int padding = 0;
            int height = rectangle.Height;
            int width = rectangle.Width;

            for (int i = 0; i < 16; i++)
            {
                if (rectangle.Border.ToUpper().Equals(colorNames[i].ToUpper()))
                {                                                                       // takes the index from the needed border color
                    border = i;
                    break;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                if (rectangle.Padding.ToUpper().Equals(colorNames[i].ToUpper()))
                {                                                                       // takes the index from the needed padding color
                    padding = i;
                    break;
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {                                                                       // first and last row, first and last column (border)
                    Console.SetCursorPosition(rectangle.Left + j, rectangle.Top + i);
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        Color(border);
                        Console.WriteLine("#");
                    }
                    else
                    {                                                                   // padding
                        Color(padding);
                        Console.WriteLine("#");
                    }
                }
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Method to draw diamonds.
        /// </summary>
        /// <param name="diamond">Object which will be drawn.</param>
        public void DrawDiamond(Diamond diamond)
        {
            string[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));
            int border = 0;
            int padding = 0;
            int rows = diamond.Rows;

            for (int i = 0; i < 16; i++)
            {
                if (diamond.Border.ToUpper().Equals(colorNames[i].ToUpper()))
                {                                                               // checks on which index of the array the suitable color stands
                    border = i;
                    break;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                if (diamond.Padding.ToUpper().Equals(colorNames[i].ToUpper()))
                {                                                                   // checks on which index of the array the suitable color stands 
                    padding = i;
                    break;
                }
            }

            int middle = (rows - 1) / 2;                                        // middle of the diamond

            for (int i = 0; i <= middle; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Console.SetCursorPosition(diamond.Left + j, diamond.Top + i);   // sets curser to to the place with the right distance to the top and left border

                    if (j == middle - i || j == middle + i)
                    {                                                               // to go everey row one step further away from the middle to draws the border
                        Color(border);                              // chooses border color
                        Console.WriteLine("#");
                    }

                    if (j > middle - i && j < middle + i)
                    {                                                               // if curser is between the border
                        Color(padding);                             // chooses padding color
                        Console.WriteLine("#");
                    }
                }
            }

            int counter = 1;

            for (int i = middle + 1; i < rows; i++)
            {                                                                       // starts one row under the middle
                for (int j = 0; j < rows; j++)
                {
                    Console.SetCursorPosition(diamond.Left + j, diamond.Top + i);   // sets curser one row under the middle

                    if (j == i - middle || j == (2 * middle) - counter)
                    {                                                               // goes every row one step closer to the middle to draw the border
                        Color(border);                              // chooses the border color
                        Console.WriteLine("#");
                    }

                    if (j > i - middle && j < (2 * middle) - counter)
                    {                                                               // if curser is between the border
                        Color(padding);                             // chooses padding color
                        Console.WriteLine("#");
                    }
                }

                counter++;                                                          // increases every row
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Method to change the font color. Needed for the drawing.
        /// </summary>
        /// <param name="color">Contains the enumeration number of the needed color.</param>
        internal static void Color(int color)
        {
            switch (color)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 9:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 10:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 11:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 12:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 13:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 14:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 15:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}
