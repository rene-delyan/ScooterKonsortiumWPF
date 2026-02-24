using ScooterKonsortium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ScooterKonsortiumWPF.ViewModel {
    public class OperativeViewModel : ViewModelBase {
        //Private properties
        private readonly MainViewModel       mMainViewModel;
        private          ScooterViewModel    mSelectedScooter;

        //Public properties
        public double                       mScale => 20;

        //Memberfunctions
        public ObservableCollection<ScooterViewModel> Scooters {
            get;
        }
        public ScooterViewModel SelectedScooter {
            get => mSelectedScooter;
            set {
                mSelectedScooter = value;
                OnPropertyChanged ();
            }
        }

        //Commands
        public ICommand MoveCommand {
            get;
        }

        //Constructor
        public OperativeViewModel (MainViewModel main)
        {
            this.mMainViewModel = main;

            using var context = new ScooterDbContext ();

            var scootersFromDb = context.Scooters.ToList ();

            Scooters = new ObservableCollection<ScooterViewModel> (
                scootersFromDb.Select (s => new ScooterViewModel (s))
            );
            MoveCommand = new RelayCommand (MoveScooter);
        }

        //Functions
        private void MoveScooter ()
        {
            if (SelectedScooter == null)
                return;

            SelectedScooter.PosX += 1;
            SelectedScooter.PosY += 1;
        }
    }
}
