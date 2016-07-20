// ----------------------------------------------------------------------- 
// <copyright file="Program.cs" company="FHWN"> 
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
    /// Header of the class Program.
    /// </summary>
    /// <param></param>
    internal class Program
    {
        /// <summary>
        /// Contains buffer height.
        /// </summary>
        private static int windowheight = 0;

        /// <summary>
        /// Contains buffer width.
        /// </summary>
        private static int windowwidth = 0;

        /// <summary>
        /// List contains all created objects in sorted by their level.
        /// </summary>
        private static List<GeometricObject> sorted = new List<GeometricObject>();

        /// <summary>
        /// List with the sorted objects.
        /// </summary>
        private static List<GeometricObject> sortedWithCompare = new List<GeometricObject>();

        /// <summary>
        /// String contains all available ConsoleColor colors.
        /// </summary>
        private static string[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));

        /// <summary>
        /// Main(string[] args).
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        private static void Main(string[] args)
        {
            Console.BufferHeight = Console.LargestWindowHeight - 10;
            Console.BufferWidth = Console.LargestWindowWidth - 10;
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WindowWidth = Console.LargestWindowWidth - 10;

            windowheight = Console.BufferHeight;
            windowwidth = Console.BufferWidth;

            bool breakout = false;
            bool exit_programm = false;
            bool exit = false;
            int drawing_color = 0;
            do
            {
                int some_input = 0;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("For creating a new object enter \"A\".");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("For drawing all inserted object press \"P\".");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("To end the programm enter \"exit\".");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("For help about this programm enter \"help\".");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("To delete or change one of your objects enter \"D\".");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("To sort all objects after theire area, coverage or distance enter \"S\".");
                Console.ResetColor();

                string input_action = Delete_spaces(Console.ReadLine());

                #region Create
                if (input_action.ToUpper().Equals("A"))
                {
                    some_input++;
                    breakout = false;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nIf you want to create a circle enter \"C\".");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("If you want to create a diamond enter \"D\".");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("If you want to create a rectangle enter \"R\".");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("If you want to go back enter \"B\".");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("For help enter \"help\".");
                        Console.ResetColor();

                        string input_object = Delete_spaces(Console.ReadLine());
                        Console.WriteLine();

                        if (input_object.ToUpper().Equals("B"))
                        {
                            break;
                        }

                        if (input_object.ToUpper().Equals("C") || input_object.ToUpper().Equals("D") || input_object.ToUpper().Equals("R"))
                        {
                            Create(input_object);
                            Console.WriteLine();
                            break;
                        }

                        if (input_object.ToUpper().Equals("HELP"))
                        {
                            Object_Help();
                            continue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Your input is invalid!");
                            Console.ResetColor();
                            continue;
                        }
                    }
                    while (breakout == false);
                }
                #endregion

                #region Print
                if (input_action.ToUpper().Equals("P"))
                {
                    do
                    {
                        some_input++;

                        if (sorted.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nYou must enter at least one object before drawing!\n");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nIf you want to print all your objects enter \"P\".");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("If you want to move a object enter \"M\".");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("If you want to go back enter \"B\".");
                            Console.ResetColor();

                            string input_print = Delete_spaces(Console.ReadLine());

                            if (input_print.ToUpper().Equals("P"))
                            {
                                exit = false;

                                do
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("Available inputs: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\"H\" for help, Tab key to switch between monochrom and color and \"E\" for exit");
                                    Console.ResetColor();

                                    if (drawing_color == 0)
                                    {
                                        for (int i = 0; i < sorted.Count; i++)
                                        {
                                            sorted[i].Draw(new MonochromRenderer());
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < sorted.Count; i++)
                                        {
                                            sorted[i].Draw(new ColorRenderer());
                                        }
                                    }

                                    Console.ResetColor();
                                    ConsoleKeyInfo input_drawing;
                                    input_drawing = Console.ReadKey();

                                    switch (input_drawing.Key)
                                    {
                                        case ConsoleKey.E:
                                            Console.Clear();
                                            exit = true;
                                            break;

                                        case ConsoleKey.H:
                                            Drawing_Help();
                                            Console.WriteLine("Press any key to reprint all your objects.");
                                            Console.ReadKey();
                                            break;

                                        case ConsoleKey.Tab:

                                            if (drawing_color == 0)
                                            {
                                                drawing_color = 1;
                                            }
                                            else
                                            {
                                                drawing_color = 0;
                                            }

                                            break;

                                        default:

                                            break;
                                    }
                                }
                                while (exit == false);
                            }

                            if (input_print.ToUpper().Equals("M"))
                            {
                                int some_input_move = 0;
                                breakout = false;

                                do
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nTo move an object enter \"objectname.move\".");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("To see all available names enter \"N\".");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("For more details from one object enter \"objectname.info\".");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("If you want to go back enter \"B\".");
                                    Console.ResetColor();

                                    string input_move_menu = Delete_spaces(Console.ReadLine());

                                    if (input_move_menu.Contains("."))
                                    {
                                        some_input_move++;
                                        string[] split_name = input_move_menu.Split(new char[] { '.' });
                                        if (split_name.Length != 2 || string.IsNullOrWhiteSpace(split_name[1]) || (!split_name[1].ToUpper().Equals("INFO") && !split_name[1].ToUpper().Equals("MOVE")))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nYour input is invalid. The correct input looks like: name.info or name.change!");
                                            Console.ResetColor();
                                            continue;
                                        }

                                        #region Info
                                        if (split_name[1].ToUpper().Equals("INFO"))
                                        {
                                            for (int i = 0; i < sorted.Count; i++)
                                            {
                                                if (split_name[0].Equals(sorted[i].Name))
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                    Console.WriteLine("\nThe border color of the object {0} is: {1}", split_name[0], sorted[i].Border);
                                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                    Console.WriteLine("The padding color of the object {0} is: {1}", split_name[0], sorted[i].Padding);
                                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                    Console.WriteLine("The distance to the left border of the object {0} is: {1}", split_name[0], sorted[i].Left);
                                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                    Console.WriteLine("The distance to the top border of the object {0} is: {1}", split_name[0], sorted[i].Top);
                                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                    Console.WriteLine("The level of the object {0} is: {1}", split_name[0], sorted[i].Level);

                                                    if (sorted[i] is Circle)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                        Console.WriteLine("The shape of the object {0} is: circle", split_name[0]);
                                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                        Console.WriteLine("The radius of the object {0} is: {1}", split_name[0], ((Circle)sorted[i]).Radius);
                                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                        Console.WriteLine("The area of the object {0} is: {1}", split_name[0], sorted[i].Area);
                                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                        Console.WriteLine("The coverage of the object {0} is: {1}\n", split_name[0], sorted[i].Coverage);
                                                        Console.ResetColor();
                                                        break;
                                                    }

                                                    if (sorted[i] is Diamond)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                        Console.WriteLine("The shape of the object {0} is: diamond", split_name[0]);
                                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                        Console.WriteLine("The rows of the object {0} is: {1}", split_name[0], ((Diamond)sorted[i]).Rows);
                                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                        Console.WriteLine("The area of the object {0} is: {1}", split_name[0], sorted[i].Area);
                                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                        Console.WriteLine("The coverage of the object {0} is: {1}\n", split_name[0], sorted[i].Coverage);
                                                        Console.ResetColor();
                                                        break;
                                                    }

                                                    if (sorted[i] is Rectangle)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                        Console.WriteLine("The shape of the object {0} is: rectangle", split_name[0]);
                                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                        Console.WriteLine("The height of the object {0} is: {1}", split_name[0], ((Rectangle)sorted[i]).Height);
                                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                        Console.WriteLine("The width of the object {0} is: {1}", split_name[0], ((Rectangle)sorted[i]).Width);
                                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                        Console.WriteLine("The area of the object {0} is: {1}", split_name[0], sorted[i].Area);
                                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                        Console.WriteLine("The coverage of the object {0} is: {1}\n", split_name[0], sorted[i].Coverage);
                                                        Console.ResetColor();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Move
                                        if (split_name[1].ToUpper().Equals("MOVE"))
                                        {
                                            int index_of_moving_object = 0;                         // to know which object should be moved

                                            do
                                            {
                                                exit = false;

                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.Write("Available inputs: ");
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("left, right, up, down, pageup, pagedown, H for Help, Tab key to switch between monochrom and color and E for Exit.");
                                                Console.ResetColor();

                                                if (drawing_color == 0)
                                                {
                                                    for (int i = 0; i < sorted.Count; i++)
                                                    {
                                                        sorted[i].Draw(new MonochromRenderer());
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < sorted.Count; i++)
                                                    {
                                                        sorted[i].Draw(new ColorRenderer());
                                                    }
                                                }

                                                Console.ResetColor();
                                                ConsoleKeyInfo input_drawing_move;
                                                input_drawing_move = Console.ReadKey();

                                                for (int i = 0; i < sorted.Count; i++)
                                                {                                                       // take object which will be moved
                                                    if (split_name[0].Equals(sorted[i].Name))
                                                    {
                                                        index_of_moving_object = i;
                                                    }
                                                }

                                                switch (input_drawing_move.Key)
                                                {
                                                    case ConsoleKey.DownArrow:

                                                        if (sorted[index_of_moving_object] is Rectangle)
                                                        {
                                                            if (sorted[index_of_moving_object].Top + ((Rectangle)sorted[index_of_moving_object]).Height < windowheight - 1)
                                                            {                                                   // if distance to the top + height is < windowsheigt --> when der is space to move down
                                                                sorted[index_of_moving_object].Top += 1;        // distance to the top + 1
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("The distance to the bottom border can't be lower than 1!\n");
                                                                Console.ResetColor();
                                                                exit = true;
                                                            }
                                                        }

                                                        if (sorted[index_of_moving_object] is Circle)
                                                        {
                                                            if (sorted[index_of_moving_object].Top + (((Circle)sorted[index_of_moving_object]).Radius * 2) + 1 < windowheight - 1)
                                                            {                                               // if there is space to move to the right
                                                                sorted[index_of_moving_object].Top += 1;   // distance to the left + 1
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("The distance to the bottom border can't be lower than 1!\n");
                                                                Console.ResetColor();
                                                                exit = true;
                                                            }
                                                        }

                                                        if (sorted[index_of_moving_object] is Diamond)
                                                        {
                                                            if (sorted[index_of_moving_object].Top + ((Diamond)sorted[index_of_moving_object]).Rows < windowheight - 1)
                                                            {                                               // if there is space to move to the right
                                                                sorted[index_of_moving_object].Top += 1;   // distance to the left + 1
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("The distance to the bottom border can't be lower than 1!\n");
                                                                Console.ResetColor();
                                                                exit = true;
                                                            }
                                                        }

                                                        break;

                                                    case ConsoleKey.E:
                                                        Console.Clear();
                                                        exit = true;
                                                        break;

                                                    case ConsoleKey.H:
                                                        Console.Clear();
                                                        Moving_Help();
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\nPress any key to reprint all your objects.");
                                                        Console.ResetColor();
                                                        Console.ReadKey();

                                                        break;

                                                    case ConsoleKey.LeftArrow:

                                                        if (sorted[index_of_moving_object].Left > 1)
                                                        {                                                       // if there is space to move to left
                                                            sorted[index_of_moving_object].Left -= 1;           // distance to the left - 1
                                                        }
                                                        else
                                                        {
                                                            Console.Clear();
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("The distance to the left border can't be lower than 1!\n");
                                                            Console.ResetColor();
                                                            exit = true;
                                                        }

                                                        break;
                                                    case ConsoleKey.PageDown:

                                                        if (sorted[index_of_moving_object].Level > 0)
                                                        {
                                                            sorted[index_of_moving_object].Level -= 1;      // level -1 
                                                            Sort_level(sorted[index_of_moving_object]);           // add the object with the changed level to the list sorted (needed if object1.level == 2, object2.level == 2 than object2.level -1, objects must be switched in the list)
                                                            sorted.RemoveAt(index_of_moving_object + 1);    // delete the object which level was decreased
                                                        }
                                                        else
                                                        {
                                                            Console.Clear();
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("The level can't be lower than 0!\n");
                                                            Console.ResetColor();
                                                            exit = true;
                                                        }

                                                        break;

                                                    case ConsoleKey.PageUp:

                                                        sorted[index_of_moving_object].Level += 1;      // level + 1 
                                                        Sort_level(sorted[index_of_moving_object]);           // add the object with the changed level to the list sorted 
                                                        sorted.RemoveAt(index_of_moving_object);        // delete the object which level was increased
                                                        break;

                                                    case ConsoleKey.RightArrow:

                                                        if (sorted[index_of_moving_object] is Rectangle)
                                                        {
                                                            if (sorted[index_of_moving_object].Left + ((Rectangle)sorted[index_of_moving_object]).Width < windowwidth - 1)
                                                            {                                               // if there is space to move to the right
                                                                sorted[index_of_moving_object].Left += 1;   // distance to the left + 1
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("The distance to the right border can't be lower than 1!\n");
                                                                Console.ResetColor();
                                                                exit = true;
                                                            }
                                                        }

                                                        if (sorted[index_of_moving_object] is Circle)
                                                        {
                                                            if (sorted[index_of_moving_object].Left + (((Circle)sorted[index_of_moving_object]).Radius * 2) + 1 < windowwidth - 1)
                                                            {                                               // if there is space to move to the right
                                                                sorted[index_of_moving_object].Left += 1;   // distance to the left + 1
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("The distance to the right border can't be lower than 1!\n");
                                                                Console.ResetColor();
                                                                exit = true;
                                                            }
                                                        }

                                                        if (sorted[index_of_moving_object] is Diamond)
                                                        {
                                                            if (sorted[index_of_moving_object].Left + ((Diamond)sorted[index_of_moving_object]).Rows < windowwidth - 1)
                                                            {                                               // if there is space to move to the right
                                                                sorted[index_of_moving_object].Left += 1;   // distance to the left + 1
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("The distance to the right border can't be lower than 1!\n");
                                                                Console.ResetColor();
                                                                exit = true;
                                                            }
                                                        }

                                                        break;

                                                    case ConsoleKey.Tab:

                                                        if (drawing_color == 0)
                                                        {
                                                            drawing_color = 1;
                                                        }
                                                        else
                                                        {
                                                            drawing_color = 0;
                                                        }

                                                        break;

                                                    case ConsoleKey.UpArrow:

                                                        if (sorted[index_of_moving_object].Top > 1)
                                                        {                                                       // if there is space to move up
                                                            sorted[index_of_moving_object].Top -= 1;            // distance to the top -1
                                                        }
                                                        else
                                                        {
                                                            Console.Clear();
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("The distance to the top border can't be lower than 1!\n");
                                                            Console.ResetColor();
                                                            exit = true;
                                                        }

                                                        break;

                                                    default:

                                                        break;
                                                }
                                            }
                                            while (exit == false);
                                        }
                                        #endregion
                                    }

                                    if (input_move_menu.ToUpper().Equals("N"))
                                    {
                                        some_input_move++;
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("\nFollowing names are available:");
                                        Console.ResetColor();

                                        for (int i = 0; i < sorted.Count; i++)
                                        {                                               // prints all available names
                                            Console.WriteLine(sorted[i].Name);
                                        }
                                    }

                                    if (input_move_menu.ToUpper().Equals("B"))
                                    {
                                        break;
                                    }

                                    if (some_input_move == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nWrong input!");
                                        Console.ResetColor();
                                        continue;
                                    }
                                }
                                while (breakout == false);
                            }

                            if (input_print.ToUpper().Equals("B"))
                            {
                                Console.WriteLine();
                                break;
                            }

                            if (!input_print.ToUpper().Equals("B") && !input_print.ToUpper().Equals("M") && !input_print.ToUpper().Equals("P"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nWrong input!. Available inputs are \"P\", \"M\" or \"B\".");
                                Console.ResetColor();
                            }
                        }
                    }
                    while (breakout == false);
                }
                #endregion

                #region Exit
                if (input_action.ToUpper().Equals("EXIT"))
                {
                    some_input++;
                    exit_programm = true;
                }
                #endregion

                #region Help
                if (input_action.ToUpper().Equals("HELP"))
                {
                    some_input++;
                    Menu_Help();
                }
                #endregion

                #region Delete
                if (input_action.ToUpper().Equals("D"))
                {
                    do
                    {
                        some_input++;
                        int some_input_delete = 0;

                        if (sorted.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nYou must enter at least one object before you can delete or change one!\n");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nTo see all available names enter \"N\".");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("For more details from one object enter \"objectname.info\".");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("If you want to change an object enter \"objectname.change\".");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("If you want to delete an object enter \"objectname.delete\".");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("If you want to go back enter \"B\".");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("For help enter \"help\".");
                            Console.ResetColor();

                            string input_delete = Delete_spaces(Console.ReadLine());

                            if (input_delete.ToUpper().Equals("N"))
                            {
                                some_input_delete++;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nFollowing names are available:");
                                Console.ResetColor();

                                for (int i = 0; i < sorted.Count; i++)
                                {
                                    Console.WriteLine(sorted[i].Name);
                                }
                            }

                            if (input_delete.Contains("."))
                            {
                                some_input_delete++;
                                string[] split_name = input_delete.Split(new char[] { '.' });
                                if (split_name.Length != 2 || string.IsNullOrWhiteSpace(split_name[1]) || (!split_name[1].ToUpper().Equals("INFO") && !split_name[1].ToUpper().Equals("CHANGE") && !split_name[1].ToUpper().Equals("DELETE")))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nYour input is invalid. The correct input looks like: name.info or name.change!");
                                    Console.ResetColor();
                                    continue;
                                }

                                if (split_name[1].ToUpper().Equals("INFO"))
                                {
                                    for (int i = 0; i < sorted.Count; i++)
                                    {
                                        if (split_name[0].Equals(sorted[i].Name))
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.WriteLine("\nThe border color of the object {0} is: {1}", split_name[0], sorted[i].Border);
                                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                            Console.WriteLine("The padding color of the object {0} is: {1}", split_name[0], sorted[i].Padding);
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.WriteLine("The distance to the left border of the object {0} is: {1}", split_name[0], sorted[i].Left);
                                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                            Console.WriteLine("The distance to the top border of the object {0} is: {1}", split_name[0], sorted[i].Top);
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.WriteLine("The level of the object {0} is: {1}", split_name[0], sorted[i].Level);

                                            if (sorted[i] is Circle)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                Console.WriteLine("The shape of the object {0} is: circle", split_name[0]);
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine("The radius of the object {0} is: {1}", split_name[0], ((Circle)sorted[i]).Radius);
                                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                Console.WriteLine("The area of the object {0} is: {1}", split_name[0], sorted[i].Area);
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine("The coverage of the object {0} is: {1}\n", split_name[0], sorted[i].Coverage);
                                                Console.ResetColor();
                                                break;
                                            }

                                            if (sorted[i] is Diamond)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                Console.WriteLine("The shape of the object {0} is: diamond", split_name[0]);
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine("The rows of the object {0} is: {1}", split_name[0], ((Diamond)sorted[i]).Rows);
                                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                Console.WriteLine("The area of the object {0} is: {1}", split_name[0], sorted[i].Area);
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine("The coverage of the object {0} is: {1}\n", split_name[0], sorted[i].Coverage);
                                                Console.ResetColor();
                                                break;
                                            }

                                            if (sorted[i] is Rectangle)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                Console.WriteLine("The shape of the object {0} is: rectangle", split_name[0]);
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine("The height of the object {0} is: {1}", split_name[0], ((Rectangle)sorted[i]).Height);
                                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                Console.WriteLine("The width of the object {0} is: {1}", split_name[0], ((Rectangle)sorted[i]).Width);
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine("The area of the object {0} is: {1}", split_name[0], sorted[i].Area);
                                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                Console.WriteLine("The coverage of the object {0} is: {1}\n", split_name[0], sorted[i].Coverage);
                                                Console.ResetColor();
                                                break;
                                            }
                                        }
                                        else if (i == sorted.Count - 1)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nName not found. Please enter a valid name!");
                                            Console.ResetColor();
                                            break;
                                        }
                                    }
                                }

                                if (split_name[1].ToUpper().Equals("DELETE"))
                                {
                                    for (int i = 0; i < sorted.Count; i++)
                                    {
                                        if (split_name[0].Equals(sorted[i].Name))
                                        {
                                            sorted.Remove(sorted[i]);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\nObject sucessfully deleted!\n");
                                            Console.ResetColor();

                                            Paint_after_action();

                                            break;
                                        }
                                        else if (i == sorted.Count - 1)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nThe entered name can't be found!\n");
                                            Console.ResetColor();
                                            break;
                                        }
                                    }

                                    if (sorted.Count < 1)
                                    {                                                                               // leaves the delete menu if the deleted object was the last in the list
                                        breakout = true;
                                    }
                                }

                                if (split_name[1].ToUpper().Equals("CHANGE"))
                                {
                                    bool changed;
                                    string input_change_new_string = string.Empty;
                                    int input_change_new_int = 0;

                                    do
                                    {
                                        bool name_not_found = false;
                                        changed = false;
                                        int change_modus = 0;                   // to know if the name of the changing object was found
                                        int index_of_changing_object = 0;

                                        for (int i = 0; i < sorted.Count; i++)
                                        {
                                            if (split_name[0].Equals(sorted[i].Name))
                                            {
                                                index_of_changing_object = i;
                                                change_modus++;                 // signals to go to the input of the property
                                                break;
                                            }

                                            if (i == sorted.Count - 1)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("\nName not found. Please enter a valid name!");
                                                Console.ResetColor();
                                                name_not_found = true;
                                                changed = true;
                                                break;
                                            }
                                        }

                                        if (change_modus == 1)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("\nEnter the property of the object, which you want to change or enter \"help\" to \nopen the help menu.");
                                            Console.ResetColor();

                                            string input_change_property = Delete_spaces(Console.ReadLine());
                                            if (input_change_property.ToUpper().Equals("HELP"))
                                            {
                                                Change_Property_Help();
                                                continue;
                                            }

                                            switch (input_change_property.ToUpper())
                                            {
                                                case "NAME":
                                                    do
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\nEnter the new name of your object.");
                                                        Console.ResetColor();
                                                        input_change_new_string = Delete_spaces(Console.ReadLine());

                                                        if (input_change_new_string.Length < 2 || input_change_new_string.ToUpper().Equals("EXIT") || input_change_new_string.ToUpper().Equals("HELP") || input_change_new_string.Contains("."))
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("\nYour input is invalid! Your name must contain at least two signs and can't \nbe \"exit\", \"help\" or can't contain \".\"!");
                                                            Console.ResetColor();
                                                            break;
                                                        }

                                                        for (int i = 0; i < sorted.Count; i++)
                                                        {
                                                            if (i == index_of_changing_object && sorted.Count > 1)
                                                            {
                                                                continue;
                                                            }

                                                            if (sorted[i].Name.Equals(input_change_new_string))
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("Your entered name was already given to an other object. Try an other one!");
                                                                Console.ResetColor();
                                                                break;
                                                            }

                                                            if (i == sorted.Count - 1)
                                                            {
                                                                sorted[index_of_changing_object].Name = input_change_new_string;
                                                                changed = true;
                                                            }
                                                        }
                                                    }
                                                    while (changed == false);

                                                    break;

                                                case "BORDER":
                                                    do
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\nEnter the new border color of your object or enter \"help\" to open the help \nmenu to see all available colors.");
                                                        Console.ResetColor();
                                                        input_change_new_string = Delete_spaces(Console.ReadLine());

                                                        if (input_change_new_string.ToUpper().Equals("HELP"))
                                                        {
                                                            Color_Help();
                                                            continue;
                                                        }

                                                        for (int i = 0; i < 16; i++)
                                                        {
                                                            if (input_change_new_string.ToUpper().Equals(colorNames[i].ToUpper()))
                                                            {
                                                                changed = true;
                                                            }

                                                            if (i == 15 && changed == false)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\nYou haven't entered a valid color! You can only use ConsoleColor colors.");
                                                                Console.ResetColor();
                                                            }

                                                            if (changed == true)
                                                            {
                                                                sorted[index_of_changing_object].Border = input_change_new_string;
                                                            }
                                                        }
                                                    }
                                                    while (changed == false);

                                                    break;

                                                case "PADDING":
                                                    do
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\nEnter the new border color of your object or enter \"help\" to open the help \nmenu to see all available colors.");
                                                        Console.ResetColor();
                                                        input_change_new_string = Delete_spaces(Console.ReadLine());

                                                        if (input_change_new_string.ToUpper().Equals("HELP"))
                                                        {
                                                            Color_Help();
                                                            continue;
                                                        }

                                                        for (int i = 0; i < 16; i++)
                                                        {
                                                            if (input_change_new_string.ToUpper().Equals(colorNames[i].ToUpper()))
                                                            {
                                                                changed = true;
                                                            }

                                                            if (i == 15 && changed == false)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\nYou haven't entered a valid color! You can only use ConsoleColor colors.");
                                                                Console.ResetColor();
                                                            }

                                                            if (changed == true)
                                                            {
                                                                sorted[index_of_changing_object].Padding = input_change_new_string;
                                                            }
                                                        }
                                                    }
                                                    while (changed == false);

                                                    break;

                                                case "LEFT":
                                                    do
                                                    {
                                                        int max_distance_left = windowwidth - (((Circle)sorted[index_of_changing_object]).Radius * 2) - 1;         // windowwidth - 2* radius - middle point
                                                        int max_distance_top = windowheight - (((Circle)sorted[index_of_changing_object]).Radius * 2) - 1;
                                                        int max_distance = 0;

                                                        if (max_distance_left < max_distance_top)
                                                        {                                                               // checks the max distance with considering the radius
                                                            max_distance = max_distance_left;
                                                        }
                                                        else
                                                        {
                                                            max_distance = max_distance_top;
                                                        }

                                                        try
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                                            Console.WriteLine("\nEnter the distance from the left border.");
                                                            Console.ResetColor();

                                                            input_change_new_string = Delete_spaces(Console.ReadLine());
                                                            input_change_new_int = Convert.ToInt32(input_change_new_string);

                                                            if (sorted[index_of_changing_object] is Circle)
                                                            {
                                                                if (input_change_new_int >= 1 && input_change_new_int < max_distance)
                                                                {                                                                            // < max_distance because there is 1 space to the right
                                                                    sorted[index_of_changing_object].Left = input_change_new_int;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", max_distance);
                                                                    Console.ResetColor();
                                                                    continue;
                                                                }
                                                            }

                                                            if (sorted[index_of_changing_object] is Diamond)
                                                            {
                                                                max_distance_left = windowwidth - ((Diamond)sorted[index_of_changing_object]).Rows;
                                                                max_distance_top = windowheight - ((Diamond)sorted[index_of_changing_object]).Rows;

                                                                if (max_distance_top < max_distance_left)
                                                                {
                                                                    max_distance = max_distance_top;
                                                                }
                                                                else
                                                                {
                                                                    max_distance = max_distance_left;
                                                                }

                                                                if (input_change_new_int >= 1 && input_change_new_int < max_distance)
                                                                {                                                                               // < max_distance because there is 1 space to the right
                                                                    sorted[index_of_changing_object].Left = input_change_new_int;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", max_distance);
                                                                    Console.ResetColor();
                                                                    continue;
                                                                }
                                                            }

                                                            if (sorted[index_of_changing_object] is Rectangle)
                                                            {
                                                                if (input_change_new_int >= 1 && input_change_new_int <= windowwidth - 1 - ((Rectangle)sorted[index_of_changing_object]).Width)
                                                                {
                                                                    sorted[index_of_changing_object].Left = input_change_new_int;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", windowwidth - ((Rectangle)sorted[index_of_changing_object]).Width);
                                                                    Console.ResetColor();
                                                                    continue;
                                                                }
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("\nYou must enter an integer >= 1!");
                                                            Console.ResetColor();
                                                        }
                                                    }
                                                    while (changed == false);
                                                    break;

                                                case "TOP":
                                                    do
                                                    {
                                                        int max_distance = windowheight - (((Circle)sorted[index_of_changing_object]).Radius * 2) - 1;            // - 2 == middle point 

                                                        try
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                                            Console.WriteLine("\nEnter the distance from the top border.");
                                                            Console.ResetColor();

                                                            input_change_new_string = Delete_spaces(Console.ReadLine());
                                                            input_change_new_int = Convert.ToInt32(input_change_new_string);

                                                            if (sorted[index_of_changing_object] is Circle)
                                                            {
                                                                if (input_change_new_int >= 1 && input_change_new_int < max_distance)
                                                                {                                                                                // < max_distance because there is 1 space to the bottom
                                                                    sorted[index_of_changing_object].Top = input_change_new_int;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", max_distance);
                                                                    Console.ResetColor();
                                                                    continue;
                                                                }
                                                            }

                                                            if (sorted[index_of_changing_object] is Diamond)
                                                            {
                                                                max_distance = windowheight - ((Diamond)sorted[index_of_changing_object]).Rows;

                                                                if (input_change_new_int >= 1 && input_change_new_int < max_distance)
                                                                {                                                                               // < max_distance because there is 1 space to the bottom
                                                                    sorted[index_of_changing_object].Top = input_change_new_int;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", max_distance);
                                                                    Console.ResetColor();
                                                                    continue;
                                                                }
                                                            }

                                                            if (sorted[index_of_changing_object] is Rectangle)
                                                            {
                                                                if (input_change_new_int >= 1 && input_change_new_int <= windowheight - 1 - ((Rectangle)sorted[index_of_changing_object]).Height)
                                                                {
                                                                    sorted[index_of_changing_object].Top = input_change_new_int;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", windowheight - ((Rectangle)sorted[index_of_changing_object]).Height);
                                                                    Console.ResetColor();
                                                                    continue;
                                                                }
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("\nYou must enter an integer >= 1!");
                                                            Console.ResetColor();
                                                        }
                                                    }
                                                    while (changed == false);
                                                    break;

                                                case "LEVEL":
                                                    do
                                                    {
                                                        try
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                                            Console.WriteLine("\nEnter the new level of your object.");
                                                            Console.ResetColor();

                                                            input_change_new_string = Delete_spaces(Console.ReadLine());

                                                            input_change_new_int = Convert.ToInt32(input_change_new_string);

                                                            if (input_change_new_int >= 0)
                                                            {
                                                                int level_old = sorted[index_of_changing_object].Level;                         // level of the object before changing
                                                                sorted[index_of_changing_object].Level = input_change_new_int;                  // changing
                                                                Sort_level(sorted[index_of_changing_object]);

                                                                if (input_change_new_int < level_old)
                                                                {
                                                                    sorted.RemoveAt(index_of_changing_object + 1);
                                                                }
                                                                else
                                                                {
                                                                    sorted.RemoveAt(index_of_changing_object);
                                                                }

                                                                changed = true;
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\nThe level must be >= 0 and smaller than 2.147.483.648!");
                                                                Console.ResetColor();
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("\nYou must enter an integer >= 0 and smaller than 2.147.483.648!");
                                                            Console.ResetColor();
                                                        }
                                                    }
                                                    while (changed == false);

                                                    break;

                                                case "RADIUS":
                                                    if (sorted[index_of_changing_object] is Circle)
                                                    {
                                                        int max_radius_height = ((windowheight - sorted[index_of_changing_object].Top) / 2) - 1;        // Radius * 2 + middlepoint 
                                                        int max_radius_width = ((windowwidth - sorted[index_of_changing_object].Left) / 2) - 1;         // Radius * 2 + middlepoint 
                                                        int max_radius = 0;
                                                        if (max_radius_height <= max_radius_width)
                                                        {
                                                            max_radius = max_radius_height;
                                                        }
                                                        else
                                                        {
                                                            max_radius = max_radius_width;
                                                        }

                                                        do
                                                        {
                                                            try
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                                Console.WriteLine("\nEnter the new radius from your object.");
                                                                Console.ResetColor();

                                                                input_change_new_string = Delete_spaces(Console.ReadLine());

                                                                input_change_new_int = Convert.ToInt32(input_change_new_string);

                                                                if (input_change_new_int >= 2 && input_change_new_int <= max_radius)
                                                                {
                                                                    ((Circle)sorted[index_of_changing_object]).Radius = input_change_new_int;
                                                                    sorted[index_of_changing_object].Area = input_change_new_int * input_change_new_int * Math.PI;
                                                                    sorted[index_of_changing_object].Coverage = 2 * input_change_new_int * Math.PI;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe radius must be >= 2 and <= {0}", max_radius);
                                                                    Console.ResetColor();
                                                                }
                                                            }
                                                            catch (Exception)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\nYou must enter an integer >= 2! and <= {0}", max_radius);
                                                                Console.ResetColor();
                                                            }
                                                        }
                                                        while (changed == false);
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("\nYour selected object is no circle. So you can't change the radius!");
                                                        Console.ResetColor();
                                                    }

                                                    break;

                                                case "ROWS":
                                                    if (sorted[index_of_changing_object] is Diamond)
                                                    {
                                                        int max_rows_height = windowheight - sorted[index_of_changing_object].Top - 1;
                                                        int max_rows_width = windowwidth - sorted[index_of_changing_object].Left - 1;
                                                        int max_rows = 0;

                                                        if (max_rows_height <= max_rows_width)
                                                        {
                                                            max_rows = max_rows_height;
                                                        }
                                                        else
                                                        {
                                                            max_rows = max_rows_width;
                                                        }

                                                        do
                                                        {
                                                            try
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                                Console.WriteLine("\nEnter the rows from your diamond.");
                                                                Console.ResetColor();

                                                                input_change_new_string = Delete_spaces(Console.ReadLine());
                                                                input_change_new_int = Convert.ToInt32(input_change_new_string);

                                                                if (input_change_new_int >= 3 && input_change_new_int % 2 == 1 && input_change_new_int <= max_rows)
                                                                {
                                                                    ((Diamond)sorted[index_of_changing_object]).Rows = input_change_new_int;
                                                                    double a = Math.Sqrt(2 * ((input_change_new_int / (double)2) * (input_change_new_int / (double)2)));
                                                                    sorted[index_of_changing_object].Area = input_change_new_int * (double)input_change_new_int / 2;
                                                                    sorted[index_of_changing_object].Coverage = 4 * a;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe rows must be >=3 and <= {0} and also an odd number!\n", max_rows);
                                                                    Console.ResetColor();
                                                                }
                                                            }
                                                            catch (Exception)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\nYou must enter an odd integer >= 3 and <= {0}!\n", max_rows);
                                                                Console.ResetColor();
                                                                continue;
                                                            }
                                                        }
                                                        while (changed == false);
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("\nYour selected object is no diamond. So you can't change the rows!");
                                                        Console.ResetColor();
                                                    }

                                                    break;

                                                case "HEIGHT":

                                                    if (sorted[index_of_changing_object] is Rectangle)
                                                    {
                                                        int max_height = windowheight - sorted[index_of_changing_object].Top - 1;

                                                        do
                                                        {
                                                            try
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                                Console.WriteLine("\nEnter the height from your rectangle.");
                                                                Console.ResetColor();

                                                                input_change_new_string = Delete_spaces(Console.ReadLine());
                                                                input_change_new_int = Convert.ToInt32(input_change_new_string);

                                                                if (input_change_new_int >= 1 && input_change_new_int <= max_height)
                                                                {
                                                                    ((Rectangle)sorted[index_of_changing_object]).Height = input_change_new_int;
                                                                    ((Rectangle)sorted[index_of_changing_object]).Area = ((Rectangle)sorted[index_of_changing_object]).Width * input_change_new_int;
                                                                    ((Rectangle)sorted[index_of_changing_object]).Coverage = (((Rectangle)sorted[index_of_changing_object]).Width + input_change_new_int) * 2;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe height must be >= 1 and <= {0}!\n", max_height);
                                                                    Console.ResetColor();
                                                                }
                                                            }
                                                            catch (Exception)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\nYou must enter an integer >= 1! and <= {0}\n", max_height);
                                                                Console.ResetColor();
                                                                continue;
                                                            }
                                                        }
                                                        while (changed == false);
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("\nYour selected object is no rectangle. So you can't change the height!");
                                                        Console.ResetColor();
                                                    }

                                                    break;

                                                case "WIDTH":

                                                    int max_width = windowwidth - sorted[index_of_changing_object].Left - 1;
                                                    if (sorted[index_of_changing_object] is Rectangle)
                                                    {
                                                        do
                                                        {
                                                            try
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("\nEnter the width from your rectangle.");
                                                                Console.ResetColor();

                                                                input_change_new_string = Delete_spaces(Console.ReadLine());
                                                                input_change_new_int = Convert.ToInt32(input_change_new_string);

                                                                if (input_change_new_int >= 1 && input_change_new_int <= max_width)
                                                                {
                                                                    ((Rectangle)sorted[index_of_changing_object]).Width = input_change_new_int;
                                                                    sorted[index_of_changing_object].Area = ((Rectangle)sorted[index_of_changing_object]).Height * input_change_new_int;
                                                                    sorted[index_of_changing_object].Coverage = (((Rectangle)sorted[index_of_changing_object]).Height + input_change_new_int) * 2;
                                                                    changed = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("\nThe width must be >= 1 and <= {0}!\n", max_width);
                                                                    Console.ResetColor();
                                                                }
                                                            }
                                                            catch (Exception)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\nYou must enter an integer >= 1 and <= {0}!\n", max_width);
                                                                Console.ResetColor();
                                                                continue;
                                                            }
                                                        }
                                                        while (changed == false);
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("\nYour selected object is no rectangle. So you can't change the width!");
                                                        Console.ResetColor();
                                                    }

                                                    break;

                                                default:
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("\nYour entered property is invalid!");
                                                    Console.ResetColor();
                                                    break;
                                            }
                                        }

                                        if (changed == true && name_not_found == false)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\nProperty sucessfully changed!");
                                            Console.ResetColor();

                                            Paint_after_action();
                                        }
                                    }
                                    while (changed == false);
                                }
                            }
                #endregion

                            if (input_delete.ToUpper().Equals("B"))
                            {
                                Console.WriteLine();
                                break;
                            }

                            if (input_delete.ToUpper().Equals("HELP"))
                            {
                                some_input_delete++;
                                Delete_Menu_Help();
                                continue;
                            }

                            if (some_input_delete == 0)
                            {
                                if (sorted.Count == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nWrong input!\n");
                                    Console.ResetColor();
                                    continue;
                                }
                            }
                        }
                    }
                    while (breakout == false);
                }

                #region Sort
                if (input_action.ToUpper().Equals("S"))
                {
                    some_input++;

                    if (sorted.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou must enter at least one object before sorting!\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        do
                        {
                            string input;
                            int mode;
                            breakout = false;

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nTo sort all your objects after their area enter \"A\".");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("To sort all your objects after their coverage enter \"C\".");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To sort all your objects after their distance enter \"D\".");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("To leave the menu enter \"B\".");
                            Console.ResetColor();

                            input = Delete_spaces(Console.ReadLine());

                            if (input.ToUpper().Equals("A"))
                            {
                                sortedWithCompare.Sort(new Area());
                                mode = 0;
                            }
                            else if (input.ToUpper().Equals("C"))
                            {
                                sortedWithCompare.Sort(new Coverage());
                                mode = 1;
                            }
                            else if (input.ToUpper().Equals("D"))
                            {
                                sortedWithCompare.Sort(new Distance());
                                mode = 2;
                            }
                            else if (input.ToUpper().Equals("B"))
                            {
                                Console.WriteLine();
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Wrong input! Possible inputs are \"A\", \"B\", \"C\" or \"D\"!.");
                                Console.ResetColor();
                                continue;
                            }

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nIf you want to print ascending enter \"A\".");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("If you want to print descending enter \"D\".");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("If you want to leave the menu enter \"B\".");
                            Console.ResetColor();

                            input = Delete_spaces(Console.ReadLine());
                            Console.WriteLine();

                            if (input.ToUpper().Equals("A"))
                            {
                                if (mode == 0)
                                {
                                    for (int i = 0; i < sortedWithCompare.Count; i++)
                                    {
                                        Console.WriteLine("Name: {0}, Area: {1} ", sortedWithCompare[i].Name, sortedWithCompare[i].Area);
                                    }
                                }
                                else if (mode == 1)
                                {
                                    for (int i = 0; i < sortedWithCompare.Count; i++)
                                    {
                                        Console.WriteLine("Name: {0}, Coverage: {1}", sortedWithCompare[i].Name, sortedWithCompare[i].Coverage);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < sortedWithCompare.Count; i++)
                                    {
                                        Console.WriteLine("Name: {0}, Distance: {1} ", sortedWithCompare[i].Name, sortedWithCompare[i].Distance);
                                    }
                                }
                            }
                            else if (input.ToUpper().Equals("D"))
                            {
                                sortedWithCompare.Reverse();

                                if (mode == 0)
                                {
                                    for (int i = 0; i < sortedWithCompare.Count; i++)
                                    {
                                        Console.WriteLine("Name: {0}, Area: {1} ", sortedWithCompare[i].Name, sortedWithCompare[i].Area);
                                    }
                                }
                                else if (mode == 1)
                                {
                                    for (int i = 0; i < sortedWithCompare.Count; i++)
                                    {
                                        Console.WriteLine("Name: {0}, Coverage: {1}", sortedWithCompare[i].Name, sortedWithCompare[i].Coverage);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < sortedWithCompare.Count; i++)
                                    {
                                        Console.WriteLine("Name: {0}, Distance: {1}", sortedWithCompare[i].Name, sortedWithCompare[i].Distance);
                                    }
                                }
                            }
                            else if (input.ToUpper().Equals("B"))
                            {
                                Console.WriteLine();
                                break;
                            }
                        }
                        while (breakout == false);
                    }
                }
                #endregion

                if (some_input == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYour input is invalid!\n");
                    Console.ResetColor();
                }
            }
            while (exit_programm == false);

            Environment.Exit(0);
        }

        /// <summary>
        /// Method to create an object with all needed parameter.
        /// </summary>
        /// <param name="type">String contains the type of the element which should be created.</param>
        private static void Create(string type)
        {
            bool breakout = false;
            int left = 0;
            int top = 0;
            int level = 0;
            int radius = 0;
            int height = 0;
            int width = 0;
            int rows = 0;
            string typ_of_Object = string.Empty;
            string name = string.Empty;
            string border_color = string.Empty;
            string padding_color = string.Empty;

            #region Name
            do
            {
                breakout = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Enter the name of your object or enter \"help\" to open the help menu.");
                Console.ResetColor();
                name = Delete_spaces(Console.ReadLine());

                #region Help
                if (name.ToUpper().Equals("HELP"))
                {
                    Name_Help();
                    continue;
                }
                #endregion

                #region False input
                if (name.Length < 2 || name.ToUpper().Equals("EXIT") || name.ToUpper().Equals("HELP") || name.Contains("."))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYour input is invalid! Your name must contain at least two signs and can't \nbe \"exit\", \"help\" or can't contain \".\"!\n");
                    Console.ResetColor();
                    continue;
                }
                #endregion

                breakout = true;

                if (sorted.Count > 0)
                {
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        if (sorted[i].Name.Equals(name))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nYour input is invalid! Your have already entered this name!\n");
                            Console.ResetColor();
                            breakout = false;
                        }
                    }
                }
            }
            while (breakout == false);
            #endregion

            #region Border color
            do
            {
                breakout = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nEnter the border color or enter \"help\" to see all available colors.");
                Console.ResetColor();
                border_color = Delete_spaces(Console.ReadLine());

                if (border_color.ToUpper().Equals("HELP"))
                {
                    Color_Help();
                    continue;
                }

                for (int i = 0; i < 16; i++)
                {                                                                       // checks in the insert color is a ConsoleColor
                    if (border_color.ToUpper().Equals(colorNames[i].ToUpper()))
                    {
                        breakout = true;
                        break;
                    }

                    if (i == 15)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou haven't entered a valid color! You can only use ConsoleColor colors.");
                        Console.ResetColor();
                    }
                }
            }
            while (breakout == false);
            #endregion

            #region Padding color
            do
            {
                breakout = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nEnter the padding color or enter \"help\" to see all available colors.");
                Console.ResetColor();
                padding_color = Delete_spaces(Console.ReadLine());

                if (padding_color.ToUpper().Equals("HELP"))
                {
                    Color_Help();
                    continue;
                }

                for (int i = 0; i < 16; i++)
                {                                                                       // checks in the insert color is a ConsoleColor
                    if (padding_color.ToUpper().Equals(colorNames[i].ToUpper()))
                    {
                        breakout = true;
                        break;
                    }

                    if (i == 15)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou haven't entered a valid color! You can only use ConsoleColor colors.");
                        Console.ResetColor();
                    }
                }
            }
            while (breakout == false);
            #endregion

            #region Left border
            do
            {
                breakout = false;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nEnter the distance from the left border or enter \"help\" to open the help menu.");
                    Console.ResetColor();

                    string left_with_help = Delete_spaces(Console.ReadLine());

                    if (left_with_help.ToUpper().Equals("HELP"))
                    {
                        Left_Help();
                        continue;
                    }

                    left = Convert.ToInt32(left_with_help);

                    if (type.ToUpper().Equals("C"))
                    {
                        if (left >= 1 && left < windowwidth - 4)
                        {                                                    // -4 because auf cirlce radius + 1 space right 
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", windowwidth - 4);
                            Console.ResetColor();
                            continue;
                        }
                    }

                    if (type.ToUpper().Equals("D"))
                    {
                        if (left >= 1 && left < windowwidth - 3)
                        {                                                                                                       // -3 because auf diamond rows  
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", windowwidth - 3);            // -3 because auf rows
                            Console.ResetColor();
                            continue;
                        }
                    }

                    if (type.ToUpper().Equals("R"))
                    {
                        if (left >= 1 && left < windowwidth - 2)
                        {                                                                                                           // -2 because auf rectangle width     
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", windowwidth - 2);
                            Console.ResetColor();
                            continue;
                        }
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYou must enter an integer which is at least >=1!");
                    Console.ResetColor();
                }
            }
            while (breakout == false);
            #endregion

            #region Top border
            do
            {
                breakout = false;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nEnter the distance from the top border or enter \"help\" to open the help menu.");
                    Console.ResetColor();

                    string top_with_help = Delete_spaces(Console.ReadLine());

                    if (top_with_help.ToUpper().Equals("HELP"))
                    {
                        Top_Help();
                        continue;
                    }

                    top = Convert.ToInt32(top_with_help);

                    if (type.ToUpper().Equals("C"))
                    {
                        if (top >= 1 && top < windowheight - 6)
                        {                                                       // -6 because auf cirlce + 1 space right   
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", windowheight - 6);
                            Console.ResetColor();
                            continue;
                        }
                    }

                    if (type.ToUpper().Equals("D"))
                    {
                        if (top >= 1 && top < windowheight - 4)
                        {                                                       // -4 because auf diamond rows + 1 space right    
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", windowheight - 4);            // -4 because auf cirlce radius
                            Console.ResetColor();
                            continue;
                        }
                    }

                    if (type.ToUpper().Equals("R"))
                    {
                        if (top >= 1 && top < windowheight - 2)
                        {                                                       // -2 because auf rectangle width + 1 space right  
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe distance must be >= 1 and smaller than {0}!", windowheight - 2);
                            Console.ResetColor();
                            continue;
                        }
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYou must enter an integer which is at least >=1!");
                    Console.ResetColor();
                }
            }
            while (breakout == false);
            #endregion

            #region Level
            do
            {
                breakout = false;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nEnter the level from your object or enter \"help\" to open the help menu.");
                    Console.ResetColor();

                    string level_with_help = Delete_spaces(Console.ReadLine());

                    if (level_with_help.ToUpper().Equals("HELP"))
                    {
                        Level_Help();
                        continue;
                    }

                    level = Convert.ToInt32(level_with_help);

                    if (level >= 0)
                    {
                        breakout = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nThe level must be >= 0 and smaller than 2.147.483.648!");
                        Console.ResetColor();
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYou must enter an integer >= 0 and smaller than 2.147.483.648!");
                    Console.ResetColor();
                }
            }
            while (breakout == false);
            #endregion

            #region Radius
            if (type.ToUpper().Equals("C"))
            {
                Console.WriteLine();
                int max_radius_height = (windowheight - top) / 2;         // -1 because of space to the bottom border
                int max_radius_width = (windowwidth - left) / 2;          // - because of space to the right border
                int max_radius = 0;
                if (max_radius_height >= max_radius_width)
                {
                    max_radius = max_radius_width;
                }
                else
                {
                    max_radius = max_radius_height;
                }

                do
                {
                    breakout = false;
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Enter the radius from your circle or enter \"help\" to enter the help menu.");
                        Console.ResetColor();

                        string radius_with_help = Delete_spaces(Console.ReadLine());

                        if (radius_with_help.ToUpper().Equals("HELP"))
                        {
                            Radius_Help(max_radius);
                            continue;
                        }

                        radius = Convert.ToInt32(radius_with_help);

                        if (radius >= 2 && radius <= max_radius)
                        {
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe radius must be >= 2 and <= {0}\n", max_radius);
                            Console.ResetColor();
                        }
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou must enter an integer >= 2! and <= {0}\n", max_radius);
                        Console.ResetColor();
                    }
                }
                while (breakout == false);

                Circle circle = new Circle(name, border_color, padding_color, left, top, level, radius);
                if (sorted.Count == 0)
                {
                    sorted.Add(circle);
                }
                else
                {
                    Sort_level(circle);
                }

                sortedWithCompare.Add(circle);
            }
            #endregion

            #region Row
            if (type.ToUpper().Equals("D"))
            {
                Console.WriteLine();
                int max_rows_height = windowheight - top - 1;
                int max_rows_width = windowwidth - left - 1;
                int max_rows = 0;

                if (max_rows_height <= max_rows_width)
                {
                    max_rows = max_rows_width;
                }
                else
                {
                    max_rows = max_rows_height;
                }

                do
                {
                    breakout = false;
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Enter the rows from your diamond or enter \"help\" to open the help menu.");
                        Console.ResetColor();

                        string rows_with_help = Delete_spaces(Console.ReadLine());

                        if (rows_with_help.ToUpper().Equals("HELP"))
                        {
                            Rows_Help(max_rows);
                            continue;
                        }

                        rows = Convert.ToInt32(rows_with_help);

                        if (rows >= 3 && rows % 2 == 1 && rows <= max_rows)
                        {
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe rows must be >=3 and <= {0} and also an odd number!\n", max_rows);
                            Console.ResetColor();
                        }
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou must enter an odd integer >= 3 and <= {0}!\n", max_rows);
                        Console.ResetColor();
                        continue;
                    }
                }
                while (breakout == false);

                Diamond diamond = new Diamond(name, border_color, padding_color, left, top, level, rows);
                if (sorted.Count == 0)
                {
                    sorted.Add(diamond);
                }
                else
                {
                    Sort_level(diamond);
                }

                sortedWithCompare.Add(diamond);
            }
            #endregion

            #region Height
            if (type.ToUpper().Equals("R"))
            {
                Console.WriteLine();
                int max_height = windowheight - top - 1;
                int max_width = windowwidth - left - 1;
                do
                {
                    breakout = false;
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Enter the height from your rectangle or enter \"help\" to open the helt menu.");
                        Console.ResetColor();

                        string height_with_help = Delete_spaces(Console.ReadLine());

                        if (height_with_help.ToUpper().Equals("HELP"))
                        {
                            Height_Help(max_height);
                            continue;
                        }

                        height = Convert.ToInt32(height_with_help);

                        if (height >= 1 && height <= max_height)
                        {
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe height must be >= 1 and <= {0}!\n", max_height);
                            Console.ResetColor();
                        }
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou must enter an integer >= 1! and <= {0}\n", max_height);
                        Console.ResetColor();
                        continue;
                    }
                }
                while (breakout == false);
            #endregion

            #region Width
                do
                {
                    Console.WriteLine();
                    breakout = false;

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter the width from your rectangle or enter \"help\" to open the help menu.");
                        Console.ResetColor();

                        string width_with_help = Delete_spaces(Console.ReadLine());

                        if (width_with_help.ToUpper().Equals("HELP"))
                        {
                            Width_Help(max_width);
                            continue;
                        }

                        width = Convert.ToInt32(width_with_help);
                        if (width >= 1 && width <= max_width)
                        {
                            breakout = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe width must be >= 1 and <= {0}!\n", max_width);
                            Console.ResetColor();
                        }
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou must enter an integer >= 1 and <= {0}!\n", max_width);
                        Console.ResetColor();
                        continue;
                    }
                }
                while (breakout == false);

                Rectangle rectangle = new Rectangle(name, border_color, padding_color, left, top, level, height, width);

                if (sorted.Count == 0)
                {
                    sorted.Add(rectangle);
                }
                else
                {
                    Sort_level(rectangle);
                }

                sortedWithCompare.Add(rectangle);
            }
                #endregion

            Paint_after_action();
        }

        /// <summary>
        /// Method to insert the new object to the list sorted, sorted after their level.
        /// </summary>
        /// <param name="object_xy">Object, which will be added to the list sorted.</param>
        private static void Sort_level(GeometricObject object_xy)
        {
            for (int i = 0; i < sorted.Count; i++)
            {
                if (object_xy.Level < sorted[i].Level)
                {                                               // if next element.level > element.level
                    sorted.Insert(i, object_xy);
                    break;
                }
                else if (i == sorted.Count - 1)
                {                                               // if element.level > all elements
                    sorted.Add(object_xy);
                    break;
                }
            }
        }

        /// <summary>
        /// Method to delete spaces before the first letter and after the last one.
        /// </summary>
        /// <param name="input">String contains the input from the user.</param>
        /// <returns>The input without spaces before the first letter and after the last one.</returns>
        private static string Delete_spaces(string input)
        {
            int length = input.Length;
            bool empty_test;

            for (int i = 0; i < length; i++)
            {                                                                           // loop for deleting all spaces after the last letter
                empty_test = input.Substring(input.Length - 1, 1).Equals(" ");          // checks if the last char of the string is a space
                if (empty_test == true)
                {                                                                       // if a space was found
                    input = input.Remove(input.Length - 1);                             // delete the last char
                }
                else
                {                                                                       // if no space was found --> all spaces were removed (or the string contained non)
                    length = input.Length;                                              // input has a new length after removing the spaces
                    break;                                                              // breaks the loop
                }
            }

            for (int i = 0; i < length; i++)
            {                                                                           // loop for deleting all spaces before the first letter
                empty_test = input.Substring(0, 1).Equals(" ");                         // checks if the first char of the string is a space
                if (empty_test == true)
                {                                                                       // if a space was found
                    input = input.Remove(0, 1);                                         // deletes the first char
                }
                else
                {                                                                       // if no space was found --> all spaces were removed (or the string contained non)
                    break;                                                              // breaks the loop
                }
            }

            return input;                                                               // returns the input without the spaces
        }

        /// <summary>
        /// Method paints all objects after an action und sets the curser after the last object.
        /// </summary>
        private static void Paint_after_action()
        {
            Console.Clear();

            int index_of_deepest_object = 0;

            for (int i = 0; i < sorted.Count; i++)
            {                                                       // paints all objects
                sorted[i].Draw(new ColorRenderer());
            }

            if (sorted.Count > 1)
            {                                                           // if there are more than 1 object
                for (int i = 1; i < sorted.Count; i++)
                {
                    if (sorted[i].Top > sorted[index_of_deepest_object].Top)
                    {                                                   // if current object.Top is bigger than the biggest top
                        if (sorted[i] is Circle)
                        {
                            index_of_deepest_object = i;
                            Console.SetCursorPosition(0, sorted[i].Top + 2 + (2 * ((Circle)sorted[i]).Radius));
                        }

                        if (sorted[i] is Diamond)
                        {
                            index_of_deepest_object = i;
                            Console.SetCursorPosition(0, sorted[i].Top + 2 + ((Diamond)sorted[i]).Rows);
                        }

                        if (sorted[i] is Rectangle)
                        {
                            index_of_deepest_object = i;
                            Console.SetCursorPosition(0, sorted[i].Top + 2 + ((Rectangle)sorted[i]).Height);
                        }
                    }
                }

                if (index_of_deepest_object == 0)
                {                                                       // if the first object hast the biggest .Top
                    if (sorted[0] is Circle)
                    {
                        Console.SetCursorPosition(0, sorted[0].Top + 2 + (2 * ((Circle)sorted[0]).Radius));
                    }

                    if (sorted[0] is Diamond)
                    {
                        Console.SetCursorPosition(0, sorted[0].Top + 2 + ((Diamond)sorted[0]).Rows);
                    }

                    if (sorted[0] is Rectangle)
                    {
                        Console.SetCursorPosition(0, sorted[0].Top + 2 + ((Rectangle)sorted[0]).Height);
                    }
                }
            }
            else
            {                                                               // if there is only one object
                if (sorted[0] is Circle)
                {
                    Console.SetCursorPosition(0, sorted[0].Top + 2 + (2 * ((Circle)sorted[0]).Radius));
                }

                if (sorted[0] is Diamond)
                {
                    Console.SetCursorPosition(0, sorted[0].Top + 2 + ((Diamond)sorted[0]).Rows);
                }

                if (sorted[0] is Rectangle)
                {
                    Console.SetCursorPosition(0, sorted[0].Top + 2 + ((Rectangle)sorted[0]).Height);
                }
            }
        }

        /// <summary>
        /// Method contains help for the menu.
        /// </summary>
        private static void Menu_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nWith this programm you can create circles, diamonds and rectangles by \nhitting the \"A\" key.\n");
            Console.WriteLine("After hitting the \"P\" key, the program will paint all your objects. \n(You must have entered at least one object.)\n");
            Console.WriteLine("To end the program enter \"exit\".\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for the delete menu.
        /// </summary>
        private static void Delete_Menu_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nIn this menu you can delete and change your objects.\n");
            Console.WriteLine("If you want to delete one of your objects enter objectname.delete, for example MyCircle.delete.\n");
            Console.WriteLine("If you want detailed information from your object enter objectname.info, for example MyCircle.info.\n");
            Console.WriteLine("If you want to know which object are available enter \"N\".\n");
            Console.WriteLine("If you want to change one of your objects enter objectname.change, for example MyCircle.change.\n");
            Console.WriteLine("If you want to leave the menu enter \"B\".\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for the moving menu.
        /// </summary>
        private static void Moving_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("You can move your object by hitting the LeftArrow key, RightArrow key, \nUpArrow key and DownArrow key.");
            Console.WriteLine("You can change the level by hitting the PageUp and PageDown key. With \nthe PageUp key you can increase the level of your object and with the PageDown \nkey you can decrease it.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nIn the monochrom mode the colors look like:\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Black and white will be drawn as black and white.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("All Dark colors will be drawn as dark gray");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("All other colors will be drawn as gray.\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for the drawing.
        /// </summary>
        private static void Drawing_Help()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("This part of the programm draws all your objects. To switch between color and monochrom hit the Tab key. If you want to go back enter \"exit\".\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nIn the monochrom mode the colors look like:\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Black and white will be drawn as black and white.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("All Dark colors will be drawn as dark gray");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("All other colors will be drawn as gray.\n");

            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for the property change.
        /// </summary>
        private static void Change_Property_Help()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYou can enter the following properties:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("name: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("The name must contain at least two signs and can't be help, exit or \ncan't contain a \".\".");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nborder: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("The border property tells the color from the border of your object. \nYou can use all ConsoleColor colors.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\npadding: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("The border property tells the color from the border of your object. \nYou can use all ConsoleColor colors.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nleft: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("The left property is the distance to the left boarder of the windows \nand must contain an integer which is bigger than 1.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\ntop: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("The top property is the distance to the left boarder of the windows \nand must contain an integer which is bigger than 1.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nlevel: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("The level proberty tells on which level your object will be drawn. \nObjects with a high level might be cover objects with a small on. Valid inputs \nare integer >=0.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nradius: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("If your selected Object is a circle you can change the radius. Your \ninput must be at least 2 und has to be an integer");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nrows: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("If your selected Object is a diamond you can change the amount of rows. \nYour input must be an odd integer and hast to be >= 3.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nwidth: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("If your selected Object is a rectangle you can change the width. Your \ninput must be an integer >=1.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nhight: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("If your selected Object is a rectangle you can change the width. Your \ninput must be an integer >=1.");
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for the object creation.
        /// </summary>
        private static void Object_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Here you must decide wich object should be created.\n");
            Console.WriteLine("To create a circle enter \"C\".\n");
            Console.WriteLine("To create a diamond enter \"D\".\n");
            Console.WriteLine("To create a rectangle enter \"R\".\n");
            Console.WriteLine("If you want to go back to the menu enter \"B\".");
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for the name rules.
        /// </summary>
        private static void Name_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter a name for your object. The name is needed to delete and change the \nobject later.\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains all available colors.
        /// </summary>
        private static void Color_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nFollowing colors are valid: ");
            for (int i = 0; i < 16; i++)
            {                                   // prints all available colors with the name of the color in its color
                switch (i)
                {
                    case 0:
                        Console.BackgroundColor = ConsoleColor.White;           // black and dark blue font color on black background color is unreadable
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Black");
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("DarkBlue");
                        break;
                    case 2:
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("DarkGreen");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("DarkCyan");

                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("DarkRed");

                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("DarkMagenta");
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("DarkYellow");
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Gray");
                        break;
                    case 8:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("DarkGray");
                        break;
                    case 9:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Blue");
                        break;
                    case 10:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Green");
                        break;
                    case 11:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Cyan");
                        break;
                    case 12:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Red");
                        break;
                    case 13:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Magenta");
                        break;
                    case 14:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Yellow");
                        break;
                    case 15:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("White");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                }
            }
        }

        /// <summary>
        /// Method contains help for input of the distance to the left border.
        /// </summary>
        private static void Left_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter an integer which is >= 1 and < {0} if hou have a rectangle, < {1} if you \nhave a diamond and < {2} if you have a circle. \nThe object will be painted as far \nfrom the top window border as high your entered number is. \nConsider that the max values could be smaller in case of bigger top distances \nand object sizes.\n", windowwidth - 2, windowwidth - 4, windowwidth - 6);
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for input of the distance to the left border.
        /// </summary>
        private static void Top_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter an integer which is >= 1 and < {0} if hou have a rectangle, < {1} if you \nhave a diamond and < {2} if you have a circle. \nThe object will be painted as far \nfrom the top window border as high your entered number is.  \nConsider that the max values could be smaller in case of bigger top distances and object sizes.\n", windowheight - 2, windowheight - 4, windowheight - 6);
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for input of the level of the object.
        /// </summary>
        private static void Level_Help()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter an integer which is >= 0 and smaller than 2.147.483.648. The object will \nbe painted on the entered level. \nObjects with a higher level may cover objects with a lower one. If there are \ntwo objects on the same level, the later inserted will be drawn later.\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for input of the radius of the object.
        /// </summary>
        /// <param name="max_radius">Contains the biggest possible radius.</param>
        private static void Radius_Help(int max_radius)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter an integer which is >= 2 and smaller than {0}.\n", max_radius);
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for input of the rows of the object.
        /// </summary>
        /// /// <param name="max_rows">Contains the biggest amount of rows.</param>
        private static void Rows_Help(int max_rows)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter an odd integer which is >= 3 and smaller than {0}.\n", max_rows);
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for input of the height of the object.
        /// </summary>
        /// /// <param name="max_height">Contains the biggest possible height.</param>
        private static void Height_Help(int max_height)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter an integer which is >= 0 and smaller than {0}.\n", max_height);
            Console.ResetColor();
        }

        /// <summary>
        /// Method contains help for input of the width of the object.
        /// </summary>
        /// /// <param name="max_width">Contains the biggest possible width.</param>
        private static void Width_Help(int max_width)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter an integer which is >= 0 and smaller than {0}.\n", max_width);
            Console.ResetColor();
        }
    }
}