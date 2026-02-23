using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ScooterKonsortiumWPF.ViewModel
{
    public class MainViewModel : ViewModelBase {
        
        private ViewModelBase mCurrentViewModel;

        public ViewModelBase CurrentViewModel {
            get => mCurrentViewModel;
            set {
                mCurrentViewModel = value;
                OnPropertyChanged ();
            }
        }
        public MainMenuViewModel MainMenuVM {
            get;
        }
        public SetupViewModel SetupVM {
            get;
        }
        public OperativeViewModel OperativeVM {
            get;
        }

        public RelayCommand ShowMainMenuCommand {
            get;
        }
        public RelayCommand ShowSetupCommand {
            get;
        }
        public RelayCommand ShowOperativeCommand {
            get;
        }
        public RelayCommand ExitCommand {
            get;
        }

        public MainViewModel ()
        {
            MainMenuVM = new MainMenuViewModel (this);
            SetupVM = new SetupViewModel (this);
            OperativeVM = new OperativeViewModel (this);

            ShowMainMenuCommand = new RelayCommand (() => ShowMainMenu ());
            ShowSetupCommand = new RelayCommand (() => ShowSetup ());
            ShowOperativeCommand = new RelayCommand (() => ShowOperative ());
            ExitCommand = new RelayCommand (() => Application.Current.Shutdown ());

            CurrentViewModel = MainMenuVM;
        }

        public void ShowMainMenu ()
            => CurrentViewModel = MainMenuVM;

        public void ShowSetup ()
            => CurrentViewModel = SetupVM;

        public void ShowOperative ()
            => CurrentViewModel = OperativeVM;
    }

}
