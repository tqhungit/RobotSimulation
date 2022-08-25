using RobotSimulation.Contants;
using RobotSimulation.Enums;
using RobotSimulation.Interface;

namespace RobotSimulation.Models
{
    public class RobotOperation
    {
        private readonly IRobot _robot;

        public RobotOperation(IRobot robot)
        {
            _robot = robot;
        }


        /// <summary>
        /// Execute the command from input
        /// </summary>
        /// <param name="args"></param>
        /// <returns>The output base on the command</returns>
        public string DoCommand(string args)
        {
            var instruction = GetInput(args);
            string response = string.Empty;

            if (!instruction.IsValid)
                return "Invalid command.";
            switch (instruction.Action)
            {
                case Command.Place:
                    if (_robot.Place(instruction.Position, instruction.Face))
                    {
                        response = "Place Done.";
                    }
                    else
                    {
                        response = _robot.Message;
                    }
                    break;
                case Command.Move:
                    if (_robot.Move())
                    {
                        response = "Move Done.";
                    }
                    else
                    {
                        response = _robot.Message;
                    }
                    break;
                case Command.Left:
                    if (_robot.Left())
                    {
                        response = "Turn Left Done.";
                    }
                    else
                    {
                        response = _robot.Message;
                    }
                    break;
                case Command.Right:
                    if (_robot.Right())
                    {
                        response = "Turn Right Done.";
                    }
                    else
                    {
                        response = _robot.Message;
                    }
                    break;
                case Command.Report:
                    response = _robot.Report();
                    break;
                default:
                    response = "Invalid command.";
                    break;
            }

            return response;
        }


        /// <summary>
        /// Get the instruction from input
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Instruction GetInput(string args)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(args))
                    return new Instruction(false);

                var inputs = args.Split(" ");

                if (inputs.Any())
                {
                    var instruction = new Instruction(true);

                    if (Enum.TryParse<Command>(inputs[0], true, out var action))
                    {
                        instruction.Action = action;
                        if (action == Command.Place)
                        {
                            GetPosition(inputs[1], ref instruction);
                        }
                        else if (inputs.Length > 1)
                        {
                            instruction.IsValid = false;
                        }
                    }
                    else
                    {
                        instruction.IsValid = false;
                    }

                    return instruction;
                }

                return new Instruction(false);
            }
            catch
            {
                return new Instruction(false);
            }
        }

        /// <summary>
        /// Get position from input
        /// </summary>
        /// <param name="args"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        public bool GetPosition(string args, ref Instruction instruction)
        {
            try
            {
                var inputs = args.Split(",");

                if (inputs.Any() && inputs.Length == 3)
                {
                    instruction.Position = new Position()
                    {
                        X = int.Parse(inputs[0]),
                        Y = int.Parse(inputs[1])
                    };
                    if (Enum.TryParse<Face>(inputs[2], true, out var _face))
                    {
                        instruction.Face = _face;
                        
                        instruction.IsValid = true;
                        return true;
                    }
                }
                instruction.IsValid = false;
                return false;
            }
            catch
            {
                instruction.IsValid = false;
                return false;
            }
        }
    }
}
