using DerbyManagement.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DerbyManagement.Model
{
    public class Division : ModelBase, IModificationHistory
    {
        public Division()
        {
            Heats = new List<Heat>();
            Racers = new List<Racer>();
        }

        private int divisionId;
        private Derby derby;
        private int derbyId;
        private int sequence;
        private string name;
        private string logoPath;
        private bool includeInChampionship;
        private bool isChampionship;
        private List<Heat> heats;
        private List<Racer> racers;
        private DateTime dateModified;
        private DateTime dateCreated;
        private bool isDirty;

        public int DivisionId
        {
            get { return divisionId; }
            set
            {
                divisionId = value;
                RaisePropertyChanged("DivisionId");
            }
        }

        public Derby Derby
        {
            get { return derby; }
            set
            {
                derby = value;
                RaisePropertyChanged("Derby");
            }
        }

        public int DerbyId
        {
            get { return derbyId; }
            set {
                derbyId = value;
                RaisePropertyChanged("DerbyId");
            }
        }

        public int Sequence
        {
            get { return sequence; }
            set {
                sequence = value;
                RaisePropertyChanged("Sequence");
            }
        }

        [StringLength(255, ErrorMessage = "Division Name maximum length is 255 characters")]
        public string Name
        {
            get { return name; }
            set {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        [StringLength(1000, ErrorMessage = "LogoPath maximum length is 1000 characters")]
        public string LogoPath
        {
            get { return logoPath; }
            set {
                logoPath = value;
                RaisePropertyChanged("LogoPath");
            }
        }

        public bool IncludeInChampionship
        {
            get { return includeInChampionship; }
            set {
                includeInChampionship = value;
                RaisePropertyChanged("IncludeInChampionship");
            }
        }

        public bool IsChampionship
        {
            get { return isChampionship; }
            set
            {
                isChampionship = value;
                RaisePropertyChanged("IsChampionship");
            }
        }

        public List<Heat> Heats
        {
            get { return heats; }
            set {
                heats = value;
                RaisePropertyChanged("Heats");
            }
        }

        public List<Racer> Racers
        {
            get { return racers; }
            set {
                racers = value;
                RaisePropertyChanged("Racers");
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

    }
}
