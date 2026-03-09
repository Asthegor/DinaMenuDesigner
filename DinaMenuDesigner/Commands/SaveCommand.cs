using System.Windows.Input;

namespace DinaMenuDesigner.Commands
{
    public class SaveCommand(Action<object?> execute, Func<object?, bool>? canExecute) : ICommand
    {
        private readonly Action<object?> _execute = execute;
        private readonly Func<object?, bool>? _canExecute = canExecute;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
