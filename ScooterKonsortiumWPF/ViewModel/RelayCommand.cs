using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ScooterKonsortiumWPF {
    public class RelayCommand : ICommand {
        private readonly Action      mExecute;
        private readonly Func<bool>? mCanExceute;

        public RelayCommand (Action execute, Func<bool>? canExceute = null)
        {
            this.mExecute = execute;
            this.mCanExceute = canExceute;
        }

        public bool CanExecute (object? parameter) => mCanExceute?.Invoke () ?? true;

        public void Execute (object? parameter) => mExecute ();
        
        public event EventHandler? CanExecuteChanged;
    }
}
