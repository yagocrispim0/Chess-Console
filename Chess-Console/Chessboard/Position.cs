﻿using System;

namespace Chessboard
{
    internal class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        // Constructors
        public Position()
        {
        }
        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        // Print position
        public override string ToString()
        {
            return Line
                + ", "
                + Column;
        }
    }
}
