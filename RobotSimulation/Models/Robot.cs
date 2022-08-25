using RobotSimulation.Contants;
using RobotSimulation.Enums;
using RobotSimulation.Interface;

namespace RobotSimulation.Models
{
    public class Robot : IRobot
    {
        private Position _position;
        private Face _face;
        private string _message;

        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Face Face
        {
            get { return _face; }

            set { _face = value; }
        }

        public string Message
        {
            get { return _message; }

            set { _message = value; }
        }


        /// <summary>
        /// Place the Robot on the table
        /// </summary>
        /// <param name="position"></param>
        /// <param name="face"></param>
        /// <returns></returns>
        public bool Place(Position position, Face face)
        {
            if (IsInTable(position.X, position.Y))
            {
                _position = new Position() { X = position.X, Y = position.Y };
                _face = face;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Move forward if possible
        /// </summary>
        /// <returns></returns>
        public bool Move()
        {
            if (IsPlaced())
            {
                int newX = GetNewX();
                int newY = GetNewY();

                if (IsInTable(newX, newY))
                {
                    _position.X = newX;
                    _position.Y = newY;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Turn left
        /// </summary>
        /// <returns></returns>
        public bool Left()
        {
            if (IsPlaced())
            {
                var n = (int)_face;
                n = n - 1;
                if (n == -1) n = 3;
                if (n == 4) n = 0;

                _face = (Face)n;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Turn right
        /// </summary>
        /// <returns></returns>
        public bool Right()
        {
            if (IsPlaced())
            {
                var n = (int)_face;
                n = n + 1;
                if (n == -1) n = 3;
                if (n == 4) n = 0;

                _face = (Face)n;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gerenate the report
        /// </summary>
        /// <returns></returns>
        public string Report()
        {
            if (IsPlaced())
            {
                return $"{_position.X},{_position.Y},{_face.ToString().ToUpper()}";
            }
            else
            {
                return "Robot has not placed on table.";
            }
        }

        /// <summary>
        /// Check if the Robot has been placed
        /// </summary>
        /// <returns></returns>
        public bool IsPlaced()
        {
            if (_position != null)
                return true;

            _message = "Robot cannot execute the command until it has been placed.";
            return false;
        }


        private bool IsInTable(int x, int y)
        {
            if (x < 0
                || y < 0
                || x >= Constants.TABLE_SIZE
                || y >= Constants.TABLE_SIZE)
            {
                _message = "Robot cannot execute the command - out of table.";
                return false;
            }
            return true;
        }



        private int GetNewX()
        {
            if (_face == Face.East)
            {
                return _position.X + 1;
            }
            else
            {
                if (_face == Face.West)
                {
                    return _position.X - 1;
                }
            }
            return _position.X;
        }

        private int GetNewY()
        {
            if (_face == Face.North)
            {
                return _position.Y + 1;
            }
            else
            {
                if (_face == Face.South)
                {
                    return _position.Y - 1;
                }
            }
            return _position.Y;
        }
    }
}
