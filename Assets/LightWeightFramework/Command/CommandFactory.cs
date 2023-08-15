using System;
using LightWeightFramework.Controller;

namespace WorkShop.LightWeightFramework.Command
{
    public class CommandFactory
    {
        public Command CreateCommand(IController controller) 
        {
            return (Command)Activator.CreateInstance(typeof(Command), controller);
        }
    }
}