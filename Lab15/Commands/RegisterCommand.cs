using Lab15.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab15.Commands
{
    public class RegisterCommand : ICommand
    {
        private RegisterViewModelValidator _validator = new RegisterViewModelValidator();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var model = parameter as RegisterViewModel;
            if(model is null)
            {
                return false;
            }
            var results = _validator.Validate(model);
            model.Errors = string.Join(" ",results.Errors);
            return results.IsValid;
        }

        public void Execute(object parameter)
        {
            var model = parameter as RegisterViewModel;
            MessageBox.Show($"Zarejstrowanao uzytkownika {model.Email}","Powodzenie",MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
