using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AMSExplorer
{
    public class TransferEntry : INotifyPropertyChanged
    {
        private readonly SynchronizationContext syncContext;

        public TransferEntry(SynchronizationContext mysyncContext)
        {
            syncContext = mysyncContext;
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private TransferType _Type;
        public TransferType Type
        {
            get => _Type;
            set
            {
                if (value != _Type)
                {
                    _Type = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private TransferState _State;
        public TransferState State
        {
            get => _State;
            set
            {
                if (value != _State)
                {
                    _State = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _Progress;
        public double Progress
        {
            get => _Progress;
            set
            {
                if (value != _Progress)
                {
                    _Progress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _ProgressText;
        public string ProgressText
        {
            get => _ProgressText;
            set
            {
                if (value != _ProgressText)
                {
                    _ProgressText = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Nullable<DateTime> _SubmitTime;
        public Nullable<DateTime> SubmitTime
        {
            get => _SubmitTime;
            set
            {
                if (value != _SubmitTime)
                {
                    _SubmitTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Nullable<DateTime> _StartTime;
        public Nullable<DateTime> StartTime
        {
            get => _StartTime;
            set
            {
                if (value != _StartTime)
                {
                    _StartTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _EndTime;
        public string EndTime
        {
            get => _EndTime;
            set
            {
                if (value != _EndTime)
                {
                    _EndTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _DestLocation;
        public string DestLocation
        {
            get => _DestLocation;
            set
            {
                if (value != _DestLocation)
                {
                    _DestLocation = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool processedinqueue { get; set; }  // true if we want to process in the queue. Otherwise, we don't wait and we do paralell transfers
        public CancellationTokenSource tokenSource { get; set; }
        public Guid Id { get; set; }

        private string _ErrorDescription;
        public string ErrorDescription
        {
            get => _ErrorDescription;
            set
            {
                if (value != _ErrorDescription)
                {
                    _ErrorDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string p = "")
        {
            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChangedEventHandler handler = PropertyChanged;

                    if (syncContext != null)
                        syncContext.Post(_ => handler(this, new PropertyChangedEventArgs(p)), null);
                    else
                        handler(this, new PropertyChangedEventArgs(p));

                }
                catch
                {

                }
            }
        }
    }

}
