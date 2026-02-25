using ScooterKonsortium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterKonsortiumWPF.ViewModel
{
    public class ScooterViewModel : ViewModelBase {
        private readonly Scooter _model;
        private readonly ScooterDbContext _context;

        public ScooterViewModel (Scooter model)
        {
            _model = model;
            _context = new ScooterDbContext ();
        }

        public int Id => _model.Id;

        public int PosX {
            get => _model.PosX;
            set {
                if (_model.PosX != value) {
                    _model.PosX = value;
                    OnPropertyChanged ();
                    OnPropertyChanged (nameof (DrawX));
                    _context.SaveChanges (); //Änderung in der DB speichern
                }
            }
        }

        public int PosY {
            get => _model.PosY;
            set {
                if (_model.PosY != value) {
                    _model.PosY = value;
                    OnPropertyChanged ();
                    OnPropertyChanged (nameof (DrawY));
                    _context.SaveChanges (); //Änderung in der DB speichern
                }
            }
        }

        public double DrawX => PosX * 20;
        public double DrawY => PosY * 20;
    }
}
