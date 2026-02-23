using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScooterKonsortiumWPF.ViewModel {
    public class OperativeViewModel : ViewModelBase {
        private readonly MainViewModel mainViewModel;

        public ICommand BackToMainCommand { get; }

        public OperativeViewModel (MainViewModel main)
        {
            this.mainViewModel = main;

            BackToMainCommand = new RelayCommand (mainViewModel.ShowMainMenu);
        }
    }
}
