using ScooterKonsortium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScooterKonsortiumWPF.ViewModel {
    public class SetupViewModel : ViewModelBase {
        private readonly MainViewModel mainViewModel;
        //Command properties
        public ICommand AddLoadingStationCommand { get; }
        public ICommand AddScooterCommand        { get; }
        public ICommand AddCompanyCommand        { get; }

        //Input properties for new station
        private string mNewStationName;
        private int    mNewStationPosX;
        private int    mNewStationPosY;
        private int    mNewStationCapacity;

        //Public properties for new station input
        public string NewStationName {
            get => mNewStationName;
            set {
                mNewStationName = value;
                OnPropertyChanged ();
            }
        }
        public int    NewStationX {
            get => mNewStationPosX;
            set {
                mNewStationPosX = value;
                OnPropertyChanged ();
            }
        }
        public int    NewStationY {
            get => mNewStationPosY;
            set {
                mNewStationPosY = value;
                OnPropertyChanged ();
            }
        }
        public int    NewStationCapacity {
            get => mNewStationCapacity;
            set {
                mNewStationCapacity = value;
                OnPropertyChanged ();
            }
        }

        //Input properties for new company
        private string mNewCompanyName;
        private string mNewCompanyLoadStationName;
        private double mNewCompanyCostPerKm;
        private string mNewCompanyMailAddress;
        private string mNewCompanyPhoneNumber;

        //Public properties for new company input
        public string NewCompanyName {
            get => mNewCompanyName;
            set {
                mNewCompanyName = value;
                OnPropertyChanged ();
            }
        }
        public string NewCompanyLoadStationName {
            get => mNewCompanyLoadStationName;
            set {
                mNewCompanyLoadStationName = value;
                OnPropertyChanged ();
            }
        }
        public double NewCompanyCostPerKm {
            get => mNewCompanyCostPerKm;
            set {
                mNewCompanyCostPerKm = value;
                OnPropertyChanged ();
            }
        }
        public string NewCompanyMailAddress {
            get => mNewCompanyMailAddress;
            set {
                mNewCompanyMailAddress = value;
                OnPropertyChanged ();
            }
        }
        public string NewCompanyPhoneNumber {
            get => mNewCompanyPhoneNumber;
            set {
                mNewCompanyPhoneNumber = value;
                OnPropertyChanged ();
            }
        }

        //Input properties for new scooter
        private int    mNewScooterPosX;
        private int    mNewScooterPosY;
        private string mNewScooterCompanyName;

        //Public properties for new scooter input
        public int    NewScooterPosX {
            get => mNewScooterPosX;
            set {
                mNewScooterPosX = value;
                OnPropertyChanged ();
            }
        }
        public int    NewScooterPosY {
            get => mNewScooterPosY;
            set {
                mNewScooterPosY = value;
                OnPropertyChanged ();
            }
        }
        public string NewScooterCompanyName {
            get => mNewScooterCompanyName;
            set {
                mNewScooterCompanyName = value;
                OnPropertyChanged ();
            }
        }

        //Public properties to enable/disable certain input fields
        public static bool DoesACompanyExist {
            get {
                using var context = new ScooterDbContext ();
                return context.companies.Any ();
            }
        }

        public static bool DoesALoadingStationExist {
            get {
                using var context = new ScooterDbContext ();
                return context.chargingStations.Any ();
            }
        }

        //Public properties to fill ListBoxes and ComboBoxes
        public List<string> ChargingStations {
            get {
                using var context = new ScooterDbContext ();
                return context.chargingStations.Select (s => s.Name).ToList ();
            }
        }

        //Constructor
        public SetupViewModel (MainViewModel main)
        {
            if (main == null)
                throw new ArgumentNullException (nameof (main));
            this.mainViewModel = main;

            AddLoadingStationCommand = new RelayCommand (AddLoadingStationExecute);
            AddCompanyCommand        = new RelayCommand (AddCompanyExecute);
            AddScooterCommand        = new RelayCommand (AddScooterExecute);
        }

        private void AddLoadingStationExecute ()
        {
            using var context = new ScooterDbContext ();
            var station = new Chargingstation { Name = NewStationName, PosX = NewStationX, PosY = NewStationY, Capacity = NewStationCapacity };
            context.chargingStations.Add (station);
            context.SaveChanges ();
        }

        private void AddCompanyExecute ()
        {
            using var context = new ScooterDbContext ();
            var company = new Company { Name = NewCompanyName, LoadStationName = NewCompanyLoadStationName, CostPerKm = NewCompanyCostPerKm, Email = NewCompanyMailAddress, Hotline = NewCompanyPhoneNumber };
            context.companies.Add (company);
            context.SaveChanges ();
        }

        private void AddScooterExecute ()
        {
            using var context = new ScooterDbContext ();
            int companyId = context.companies.Where (c => c.Name == NewScooterCompanyName).Select (c => c.Id).FirstOrDefault (); 
            var scooter = new Scooter { CompanyId = companyId, PosX = NewScooterPosX, PosY = NewScooterPosY};
            context.scooters.Add (scooter);
            context.SaveChanges ();
        }
    }
}
