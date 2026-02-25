using ScooterKonsortium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ScooterKonsortiumWPF.ViewModel {
    public class OperativeViewModel : ViewModelBase {
        //Private properties
        private readonly MainViewModel       mMainViewModel;
        private          ScooterDbContext    mDbContext;
        private          ScooterViewModel    mSelectedScooter;

        public ICommand ChangePositionCommand        { get; }
        public ICommand BringScooterToStationCommand { get; }

        //Public properties
        public int ChangePosX { 
            get; set; 
        }
        public int ChangePosY {
            get; set;
        }

        //Memberfunctions
        public ObservableCollection<ScooterViewModel> Scooters {
            get;
        }
        public ScooterViewModel SelectedScooter {
            get => mSelectedScooter;
            set {
                mSelectedScooter = value;
                OnPropertyChanged ();
                OnPropertyChanged (nameof (GetCorrectColor));
            }
        }

        public ObservableCollection<ChargingStationViewModel> ChargingStations {
            get;
        }

        public System.Windows.Media.Brush GetCorrectColor {
            get {
                if (SelectedScooter == null)
                    return System.Windows.Media.Brushes.Black;
                if (SelectedScooter.CurrentBattery > 60)
                    return System.Windows.Media.Brushes.Green;
                else if (SelectedScooter.CurrentBattery <= 60 && SelectedScooter.CurrentBattery > 35)
                    return System.Windows.Media.Brushes.Yellow;
                else if (SelectedScooter.CurrentBattery <= 35)
                    return System.Windows.Media.Brushes.Red;
                else
                    return System.Windows.Media.Brushes.Black;
            }
        }

        //Constructor
        public OperativeViewModel (MainViewModel main)
        {
            this.mMainViewModel = main;

            mDbContext = new ScooterDbContext ();
            var scootersFromDb = mDbContext.scooters.ToList ();

            Scooters = new ObservableCollection<ScooterViewModel> (
                scootersFromDb.Select (s => new ScooterViewModel (s, mDbContext))
            );

            var chargingStationsFromDb = mDbContext.chargingStations.ToList ();
            ChargingStations = new ObservableCollection<ChargingStationViewModel> (
                chargingStationsFromDb.Select (cs => new ChargingStationViewModel (cs, mDbContext))
            );

            ChangePositionCommand        = new RelayCommand (MoveScooter);
            BringScooterToStationCommand = new RelayCommand (BringScooterToStation);
        }

        //Functions
        private void MoveScooter ()
        {
            //Max X = 19 Max Y = 32
            if (SelectedScooter == null)
                return;

            int distance = 0;
            int chargeCost = 0;
            

            distance = CalcDistance(SelectedScooter.PosX, SelectedScooter.PosY,ChangePosX,ChangePosY);
            chargeCost = distance * 2;
            if (ChangePosX > 19 || ChangePosX < 0 || ChangePosY > 32 || ChangePosY < 0)
            {
                MessageBox.Show("Gültige Koordinaten eingeben! X (0-19) | Y (0 - 32)");
            }
            else if(SelectedScooter.PosX == ChangePosX && SelectedScooter.PosY == ChangePosY)
            {
                MessageBox.Show("Die Koordinaten müssen sich vom aktuellen Standort unterscheiden!");
            }

            else if(SelectedScooter.CurrentBattery > 30 && (SelectedScooter.CurrentBattery - chargeCost) > 0)
            {
               
                SelectedScooter.PosX = ChangePosX;
                SelectedScooter.PosY = ChangePosY;
                SelectedScooter.CurrentBattery -= chargeCost;
                MessageBox.Show("Der Scooter " + SelectedScooter.Id + " ist erfolgreich am Ziel (" + SelectedScooter.PosX + "/" + SelectedScooter.PosY + ") angekommen!\nNeue Reichweite: " + SelectedScooter.CurrentBattery + "%");
            }
            else if(SelectedScooter.CurrentBattery <= 30)
            {
                MessageBox.Show("Scooter " + SelectedScooter.Id + " hat nicht genug Reichweite für eine Fahrt und muss zur nächsten Ladestation gebracht werden!\nAktuelle Reichweite: " + SelectedScooter.CurrentBattery);
            }
            else if(SelectedScooter.CurrentBattery < chargeCost) 
            {
                MessageBox.Show("Scooter " + SelectedScooter.Id + " hat nicht genug Reichweite für diese Fahrt! Bitte eine geringere Distanz auswählen oder zur nächsten Ladestation bringen.\n+" +
                                "Aktuelle Reichweite: " + SelectedScooter.CurrentBattery + "\nBenötigte Reichweite: " + chargeCost);
            }
            //SelectedScooter.CurrentBattery
        }

        private void BringScooterToStation ()
        {
            if (SelectedScooter == null)
                return;
            //var station = mDbContext.chargingStations.FirstOrDefault ();
            var station = CalcClosestStation();
       
            if (station != null) {
                SelectedScooter.PosX = station.PosX;
                SelectedScooter.PosY = station.PosY;
                SelectedScooter.CurrentBattery = 100;
                MessageBox.Show ("Der Scooter " + SelectedScooter.Id + " wurde zur nächsten Ladestation gebracht und vollständig aufgeladen!");
            }
        }
        private int CalcDistance(int oldX,int oldY,int newX,int newY)
        {
            int distance = 0;
            distance = Math.Abs (oldX - newX) + Math.Abs (oldY - newY);
            return distance;
        }
        private ChargingStationViewModel CalcClosestStation()
        {
            int min = int.MaxValue;
            int distance = 0;

            ChargingStationViewModel nearestStation = null;
            
            foreach (var station in ChargingStations) 
            {
                distance = CalcDistance(SelectedScooter.PosX,SelectedScooter.PosY,station.PosX,station.PosY);
                if(distance < min)
                {
                    min = distance;
                    nearestStation = station;
                }
            }

            return nearestStation;
        }

       
    }
}
