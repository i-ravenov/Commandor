using Commandor.Models;

namespace Commandor.Data
{
    public class MockCommandorRepo : ICommandorRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle & Pan" },
                new Command { Id = 1, HowTo = "Cut bread", Line = "Get a knife", Platform = "Knife & Pan" },
                new Command { Id = 2, HowTo = "Make a cup of tea", Line = "Boil water", Platform = "Kettle & cup" }
            };
            return commands;
        }

        public Command GetCommandById(int Id)
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle & Pan" };

        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
