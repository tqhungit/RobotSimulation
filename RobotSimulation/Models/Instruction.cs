using RobotSimulation.Enums;

namespace RobotSimulation.Models
{
    public class Instruction
    {
        public Instruction(bool isValid)
        {
            IsValid = isValid;
        }
        public Command Action { get; set; }

        public Position Position { get; set; }

        public Face Face { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }
    }
}
