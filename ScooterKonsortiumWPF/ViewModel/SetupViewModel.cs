using ScooterKonsortium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ScooterKonsortiumWPF.ViewModel {
    public class SetupViewModel : ViewModelBase {
        private readonly MainViewModel mainViewModel;
        private readonly ScooterDbContext mSetupContext;
        //Command properties
        public ICommand AddLoadingStationCommand { get; }
        public ICommand AddScooterCommand        { get; }
        public ICommand AddCompanyCommand        { get; }

        #region Input properties for new station
        private string mNewStationName;
        private int?   mNewStationPosX;
        private int?   mNewStationPosY;
        private int    mNewStationCapacity;

        public string NewStationName {
            get => mNewStationName;
            set {
                mNewStationName = value;
                OnPropertyChanged ();
            }
        }
        public int?   NewStationX {
            get => mNewStationPosX;
            set {
                mNewStationPosX = value;
                OnPropertyChanged ();
            }
        }
        public int?   NewStationY {
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

        #endregion

        #region Input properties for new company
        private string mNewCompanyName;
        private string mNewCompanyLoadStationName;
        private double mNewCompanyCostPerKm;
        private string mNewCompanyMailAddress;
        private string mNewCompanyPhoneNumber;
        
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

#endregion

        #region Input properties for new scooter

        private int?   mNewScooterPosX;
        private int?   mNewScooterPosY;
        private string mNewScooterCompanyName;

        //Public properties for new scooter input
        public int? NewScooterPosX {
            get => mNewScooterPosX;
            set {
                mNewScooterPosX = value;
                OnPropertyChanged ();
            }
        }
        public int? NewScooterPosY {
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

        #endregion

        #region Public properties to enable/disable certain input fields
        public bool DoesACompanyExist {
            get {
                return mSetupContext.companies.Any ();
            }
        }

        public bool DoesALoadingStationExist {
            get {
                return mSetupContext.chargingStations.Any ();
            }
        }

        #endregion

        #region OCs to fill UI
        //Public properties to fill ListBoxes and ComboBoxes
        public ObservableCollection<Chargingstation> ChargingStations {
            get;
            set;
        }

        public ObservableCollection<Company> Companies {
            get;
            set;
        }

        public ObservableCollection<Scooter> Scooters {
            get;
            set;
        }
        #endregion

        #region Properties for selected ComboBox Entry
        //Public properties for selected ComboBox Entry
        public Chargingstation SelectedStation {
            get;
            set;
        }

        public Company SelectedCompany {
            get;
            set;
        }
        #endregion

        #region Constructor and Functions
        public SetupViewModel (MainViewModel main)
        {
            if (main == null)
                throw new ArgumentNullException (nameof (main));
            this.mainViewModel = main;
            mSetupContext = new ScooterDbContext ();

            AddLoadingStationCommand = new RelayCommand (AddLoadingStationExecute);
            AddCompanyCommand        = new RelayCommand (AddCompanyExecute);
            AddScooterCommand        = new RelayCommand (AddScooterExecute);

            mSetupContext.Database.EnsureCreated (); //Stellt sicher, dass die Datenbank und Tabellen existieren

            ChargingStations = new ObservableCollection<Chargingstation> (
                mSetupContext.chargingStations
                       .ToList ()
            );

            Companies = new ObservableCollection<Company> (
                mSetupContext.companies
                       .ToList ()
            );

            Scooters = new ObservableCollection<Scooter> (
                mSetupContext.scooters
                       .ToList ()
            );
        }

        private void AddLoadingStationExecute ()
        {
            var station = new Chargingstation { Name     = NewStationName, 
                                                PosX     = NewStationX ?? -1, 
                                                PosY     = NewStationY ?? -1, 
                                                Capacity = NewStationCapacity };
            mSetupContext.chargingStations.Add (station);
            mSetupContext.SaveChanges ();
            
            ChargingStations.Add (station); //Aktualisiert die ObservableCollection, damit die neue Ladestation sofort in der UI erscheint
        }

        private void AddCompanyExecute ()
        {
            if (SelectedStation == null) {
                MessageBox.Show ("Bitte wählen Sie zuerst eine Ladestation aus, bevor Sie eine Firma hinzufügen.", 
                                 "Fehler", 
                                 MessageBoxButton.OK, 
                                 MessageBoxImage.Error);
                return;
            }
            var company = new Company { Name            = NewCompanyName, 
                                        LoadStationName = SelectedStation.Name, 
                                        CostPerKm       = NewCompanyCostPerKm, 
                                        Email           = NewCompanyMailAddress, 
                                        Hotline         = NewCompanyPhoneNumber };
            mSetupContext.companies.Add (company);
            mSetupContext.SaveChanges ();

            Companies.Add (company);
        }

        private void AddScooterExecute ()
        {
            if (SelectedCompany == null) {
                MessageBox.Show ("Bitte wählen Sie zuerst eine Firma aus, bevor Sie einen Scooter hinzufügen.", 
                                 "Fehler", 
                                 MessageBoxButton.OK, 
                                 MessageBoxImage.Error);
                return;
            }
            int companyId = mSetupContext.companies.Where (c => c.Name == SelectedCompany.Name).Select (c => c.Id).FirstOrDefault (); 
            var scooter = new Scooter { CompanyId = companyId, 
                                        PosX      = NewScooterPosX ?? -1, 
                                        PosY      = NewScooterPosY ?? -1};
            mSetupContext.scooters.Add (scooter);
            mSetupContext.SaveChanges ();

            Scooters.Add (scooter);
        }
        #endregion
    }
}
