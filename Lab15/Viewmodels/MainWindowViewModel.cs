using FluentValidation;
using Lab15.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Lab15.Viewmodels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Model = new RegisterViewModel();
            RegisterCommand = new RegisterCommand();
        }

        public RegisterViewModel Model { get; set; }
        public ICommand RegisterCommand { get; set; }
    }

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();




            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .Must(x => x != x.ToLower())
                .WithMessage("haslo musi zawierac co najmniej 1 duza litere")
                .Must(x => x != x.ToUpper())
                .WithMessage("haslo musi zawierac co najmniej jedna mala litere")
                .Matches(@".*\d.*")
                .WithMessage("haslo musi zawierac co najmniej jedna cyfre");
         

            RuleFor(x => x.IsHuman)
                .Must(x => x)
                .WithMessage("Msuisz potwierdzic ze jestes czlowiekiem");
        }
    }

    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _email;

        public string Email {
            get{ return _email; }
            set
            {
                if(_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            } 
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }
        private bool _isHuman;

        public bool IsHuman
        {
            get { return _isHuman; }
            set
            {
                if (_isHuman != value)
                {
                    _isHuman = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsHuman"));
                }
            }
        }

        private string _errors;

        public string Errors
        {
            get { return _errors; }
            set
            {
                if (_errors != value)
                {
                    _errors = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Errors"));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
