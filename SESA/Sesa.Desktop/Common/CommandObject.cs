using System.Windows.Input;

namespace Sesa.Desktop.Common
{
    public class CommandObject
    {
        public CommandObject(ICommand command, string key)
        {
            Command = command;
            Key = key;
        }
        public ICommand Command { get; set; }
        public string Key { get; set; }
    }
}
