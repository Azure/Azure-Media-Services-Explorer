using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AMSExplorer
{
    public class TransformEntry : INotifyPropertyChanged
    {
        private readonly SynchronizationContext syncContext;

        public TransformEntry(SynchronizationContext mysyncContext)
        {
            syncContext = mysyncContext;
        }

        public string _Name;
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

        public string _Description;
        public string Description
        {
            get => _Description;
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int _Outputs;
        public int Outputs
        {
            get => _Outputs;
            set
            {
                if (value != _Outputs)
                {
                    _Outputs = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int _Jobs;
        public int Jobs
        {
            get => _Jobs;
            set
            {
                if (value != _Jobs)
                {
                    _Jobs = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _LastModified;
        public string LastModifiedOn
        {
            get => _LastModified;
            set
            {
                if (value != _LastModified)
                {
                    _LastModified = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /*
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string p = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
        */

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
