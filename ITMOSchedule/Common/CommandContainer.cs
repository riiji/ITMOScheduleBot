namespace ITMOSchedule.Common
{
    public class CommandContainer
    {
        public CommandContainer(string name, string[] args)
        {
            Name = name;
            Args = args;
        }

        public string Name { get; set; }

        public string[] Args { get; set; }
    }
}