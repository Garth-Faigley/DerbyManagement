using DerbyManagement.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DerbyManagement.Model
{
    public class Racer : INotifyPropertyChanged, IModificationHistory
    {
        public Racer()
        {
            Divisions = new List<Division>();
            Runs = new List<Run>();
        }

        private int racerId;
        private int carNumber;
        private string carName;
        private string ownerName;
        private List<Division> divisions;
        private List<Run> runs;
        private DateTime dateModified;
        private DateTime dateCreated;
        private bool isDirty;


        public int RacerId
        {
            get { return racerId; }
            set {
                racerId = value;
                RaisePropertyChanged("RacerId");
            }
        }

        public int CarNumber
        {
            get { return carNumber; }
            set {
                carNumber = value;
                RaisePropertyChanged("CarNumber");
            }
        }

        [StringLength(255, ErrorMessage = "Car Name maximum length is 255 characters")]
        public string CarName
        {
            get { return carName; }
            set {
                carName = value;
                RaisePropertyChanged("CarName");
            }
        }

        [StringLength(255, ErrorMessage = "Owner Name maximum length is 255 characters")]
        public string OwnerName
        {
            get { return ownerName; }
            set
            {
                ownerName = value;
                RaisePropertyChanged("OwnerName");
            }
        }

        public List<Division> Divisions
        {
            get { return divisions; }
            set {
                divisions = value;
                RaisePropertyChanged("Divisions");
            }
        }

        public List<Run> Runs
        {
            get { return runs; }
            set {
                runs = value;
                RaisePropertyChanged("Runs");
            }
        }

        public DateTime DateModified
        {
            get { return dateModified; }
            set
            {
                dateModified = value;
                RaisePropertyChanged("DateModified");
            }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set
            {
                dateCreated = value;
                RaisePropertyChanged("DateCreated");
            }
        }

        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                isDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
