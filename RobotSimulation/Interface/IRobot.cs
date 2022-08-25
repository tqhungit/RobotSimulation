using RobotSimulation.Enums;

namespace RobotSimulation.Interface
{
    public interface IRobot
    {
        Position Position { get; set; }
        Face Face { get; set; }

        string Message { get;set; }

        bool Place(Position position, Face face);

        bool Move();

        bool Left();

        bool Right();

        string Report();
    }
}
