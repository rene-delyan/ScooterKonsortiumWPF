using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScooterKonsortiumWPF.ViewModel {
    public class MainMenuViewModel : ViewModelBase {
        private readonly MainViewModel mainViewModel;

        public ICommand ShowSetupCommand     { get; }
        public ICommand ShowOperativeCommand { get; }
        public ICommand ExitCommand          { get; }

        public MainMenuViewModel (MainViewModel main)
        {
            this.mainViewModel = main;
            ShowSetupCommand     = new RelayCommand (mainViewModel.ShowSetup);
            ShowOperativeCommand = new RelayCommand (mainViewModel.ShowOperative);
            ExitCommand          = new RelayCommand (() => System.Windows.Application.Current.Shutdown ());
        }
    }
}
