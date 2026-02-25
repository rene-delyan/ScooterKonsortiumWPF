using Microsoft.EntityFrameworkCore;
using ScooterKonsortium;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterKonsortiumWPF.ViewModel {
    public class ChargingStationViewModel : ViewModelBase {
        private readonly Chargingstation  mModel;
        private readonly ScooterDbContext mContext;
        public ChargingStationViewModel (Chargingstation model, ScooterDbContext dbContext)
        {
            mModel = model;
            mContext = dbContext;
        }

        public long   Id   => mModel.Id;
        public string Name => mModel.Name;

        public int PosX {
            get => mModel.PosX;
            set {
                if (mModel.PosX != value) {
                    mModel.PosX = value;
                    OnPropertyChanged ();
                    OnPropertyChanged (nameof (DrawX));
                    mContext.SaveChanges (); //Änderung in der DB speichern
                }
            }
        }

        public int PosY {
            get => mModel.PosY;
            set {
                if (mModel.PosY != value) {
                    mModel.PosY = value;
                    OnPropertyChanged ();
                    OnPropertyChanged (nameof (DrawY));
                    mContext.SaveChanges (); //Änderung in der DB speichern
                }
            }
        }

        public double DrawX => PosX * 20;
        public double DrawY => PosY * 20;
    }
}
