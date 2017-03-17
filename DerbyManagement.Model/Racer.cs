using DerbyManagement.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DerbyManagement.Model
{
    public class Racer : ModelBase, IModificationHistory
    {
        public Racer()
        {
            Divisions = new List<Division>();
            Runs = new List<Run>();
        }

        private int racerId;
        private int carNumber;
        private string carName;
        private string ownerLastName;
        private string ownerFirstName;
        private List<Division> divisions;
        private List<Run> runs;
        private DateTime dateModified;
        private DateTime dateCreated;
        private bool isDirty;


        public int RacerId
        {
            get { return racerId; }
            set
            {
                if (value == racerId) return;
                racerId = value;
                RaisePropertyChanged("RacerId");
            }
        }

        [Required]
        [Range(10, 200, ErrorMessage = "Car Number must be between 10 and 200")]
        public int CarNumber
        {
            get { return carNumber; }
            set
            {
                if (value == carNumber) return;
                carNumber = value;
                RaisePropertyChanged("CarNumber");
            }
        }

        [Required]
        [StringLength(255, ErrorMessage = "Car Name maximum length is 255 characters")]
        public string CarName
        {
            get { return carName; }
            set
            {
                if (value == carName) return;
                carName = value;
                RaisePropertyChanged("CarName");
            }
        }

        [Required]
        [StringLength(255, ErrorMessage = "Owner Last Name maximum length is 255 characters")]
        public string OwnerLastName
        {
            get { return ownerLastName; }
            set
            {
                if (value == ownerLastName) return;
                ownerLastName = value;
                RaisePropertyChanged("OwnerLastName");
            }
        }

        [Required]
        [StringLength(255, ErrorMessage = "Owner First Name maximum length is 255 characters")]
        public string OwnerFirstName
        {
            get { return ownerFirstName; }
            set
            {
                if (value == ownerFirstName) return;
                ownerFirstName = value;
                RaisePropertyChanged("OwnerFirstName");
            }
        }

        [MustHaveOneElement(ErrorMessage = "At least one Division is required")]
        public List<Division> Divisions
        {
            get { return divisions; }
            set
            {
                if (value == divisions) return;
                divisions = value;
                RaisePropertyChanged("Divisions");
            }
        }

        public List<Run> Runs
        {
            get { return runs; }
            set
            {
                if (value == runs) return;
                runs = value;
                RaisePropertyChanged("Runs");
            }
        }

        public DateTime DateModified
        {
            get { return dateModified; }
            set
            {
                if (value == dateModified) return;
                dateModified = value;
                RaisePropertyChanged("DateModified");
            }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set
            {
                if (value == dateCreated) return;
                dateCreated = value;
                RaisePropertyChanged("DateCreated");
            }
        }

        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                if (value == isDirty) return;
                isDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }

    }
}
