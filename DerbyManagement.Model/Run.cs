using DerbyManagement.Model.Interfaces;
using System;

namespace DerbyManagement.Model
{
    public class Run : ModelBase, IModificationHistory
    {
        private int runId;
        private Heat heat;
        private int heatId;
        private Racer racer;
        private int racerId;
        private int lane;
        private int place;
        private DateTime dateModified;
        private DateTime dateCreated;
        private bool isDirty;

        public int RunId
        {
            get { return runId; }
            set {
                runId = value;
                RaisePropertyChanged("RunId");
            }
        }

        public Heat Heat
        {
            get { return heat; }
            set {
                heat = value;
                RaisePropertyChanged("Heat");
            }
        }

        public int HeatId
        {
            get { return heatId; }
            set {
                heatId = value;
                RaisePropertyChanged("HeatId");
            }
        }

        public Racer Racer
        {
            get { return racer; }
            set {
                racer = value;
                RaisePropertyChanged("Racer");
            }
        }

        public int RacerId
        {
            get { return racerId; }
            set
            {
                racerId = value;
                RaisePropertyChanged("RacerId");
            }
        }

        public int Lane
        {
            get { return lane; }
            set {
                lane = value;
                RaisePropertyChanged("Lane");
            }
        }

        public int Place
        {
            get { return place; }
            set
            {
                place = value;
                RaisePropertyChanged("Place");
            }
        }

        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set {
                time = value;
                RaisePropertyChanged("Time");
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
