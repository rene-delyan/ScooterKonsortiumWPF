using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScooterKonsortiumWPF.ViewModel {
    public class SetupViewModel : ViewModelBase {
        private readonly MainViewModel mainViewModel;

        public ICommand BackToMainCommand {
            get;
        }

        public SetupViewModel (MainViewModel main)
        {
            if (main == null)
                throw new ArgumentNullException (nameof (main));
            this.mainViewModel = main;

            BackToMainCommand = new RelayCommand (mainViewModel.ShowMainMenu);
        }
    }
}
