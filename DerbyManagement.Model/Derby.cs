using DerbyManagement.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DerbyManagement.Model
{
    public class Derby : INotifyPropertyChanged, IModificationHistory
    {
        public Derby()
        {
            Divisions = new List<Division>();
        }

        private int derbyId;
        private string name;
        private DateTime date;
        private int lanes;
        private bool hasChampionship;
        private int divisionPlacesToAdvance;
        private ScoringType scoringType;
        private List<Division> divisions;
        private DateTime dateModified;
        private DateTime dateCreated;
        private bool isDirty;

        public int DerbyId
        {
            get { return derbyId; }
            set {
                derbyId = value;
                RaisePropertyChanged("DerbyId");
            }
        }

        [StringLength(255, ErrorMessage = "Derby Name maximum length is 255 characters")]
        public string Name
        {
            get { return name; }
            set {
                name = value;
                RaisePropertyChanged("DerbyId");
            }
        }

        public DateTime Date
        {
            get { return date; }
            set {
                date = value;
                RaisePropertyChanged("Date");
            }
        }

        public int Lanes
        {
            get { return lanes; }
            set {
                lanes = value;
                RaisePropertyChanged("Lanes");
            }
        }

        public bool HasChampionship
        {
            get { return hasChampionship; }
            set {
                hasChampionship = value;
                RaisePropertyChanged("HasChampionship");
            }
        }

        public int DivisionPlacesToAdvance
        {
            get { return divisionPlacesToAdvance; }
            set {
                divisionPlacesToAdvance = value;
                RaisePropertyChanged("DivisionPlacesToAdvance");
            }
        }

        public ScoringType ScoringType
        {
            get { return scoringType; }
            set {
                scoringType = value;
                RaisePropertyChanged("ScoringType");
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
