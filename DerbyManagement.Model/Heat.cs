using DerbyManagement.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace DerbyManagement.Model
{
    public class Heat : ModelBase, IModificationHistory
    {
        public Heat()
        {
            Runs = new List<Run>();
        }

        private int heatId;
        private Division division;
        private int divisionId;
        private int sequence;
        private HeatStatus status;
        private List<Run> runs;
        private DateTime dateModified;
        private DateTime dateCreated;
        private bool isDirty;


        public int HeatId
        {
            get { return heatId; }
            set {
                heatId = value;
                RaisePropertyChanged("HeatID");
            }
        }

        public Division Division
        {
            get { return division; }
            set {
                division = value;
                RaisePropertyChanged("Division");
            }
        }

        public int DivisionId
        {
            get { return divisionId; }
            set {
                divisionId = value;
                RaisePropertyChanged("DivisionId");
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

        public HeatStatus Status
        {
            get { return status; }
            set {
                status = value;
                RaisePropertyChanged("Status");
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

    }
}
