//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//---------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AMSExplorer
{
    public class AssetEntryV3 : INotifyPropertyChanged
    {

        private readonly SynchronizationContext syncContext;

        public AssetEntryV3(SynchronizationContext mysyncContext)
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

        public string _StorageAccountName;
        public string StorageAccountName
        {
            get => _StorageAccountName;
            set
            {
                if (value != _StorageAccountName)
                {
                    _StorageAccountName = value;
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

        public Guid AssetId { get; set; }

        public string _AlternateId;
        public string AlternateId
        {
            get => _AlternateId;
            set
            {
                if (value != _AlternateId)
                {
                    _AlternateId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string _Type;
        public string Type
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


        private string _Size;
        public string Size
        {
            get => _Size;
            set
            {
                if (value != _Size)
                {
                    _Size = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private long _SizeLong;
        public long SizeLong
        {
            get => _SizeLong;
            set
            {
                if (value != _SizeLong)
                {
                    _SizeLong = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Bitmap _DynamicEncryption;
        public Bitmap DynamicEncryption
        {
            get => _DynamicEncryption;
            set
            {
                if (value != _DynamicEncryption)
                {
                    _DynamicEncryption = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _DynamicEncryptionMouseOver;
        public string DynamicEncryptionMouseOver
        {
            get => _DynamicEncryptionMouseOver;
            set
            {
                if (value != _DynamicEncryptionMouseOver)
                {
                    _DynamicEncryptionMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Bitmap _Publication = null;

        public Bitmap Publication
        {
            get => _Publication;
            set
            {
                if (value != _Publication)
                {
                    _Publication = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int? _Filters = null;
        public int? Filters
        {
            get => _Filters;
            set
            {
                if (value != _Filters)
                {
                    _Filters = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _FiltersMouseOver;
        public string FiltersMouseOver
        {
            get => _FiltersMouseOver;
            set
            {
                if (value != _FiltersMouseOver)
                {
                    _FiltersMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _PublicationMouseOver;
        public string PublicationMouseOver
        {
            get => _PublicationMouseOver;
            set
            {
                if (value != _PublicationMouseOver)
                {
                    _PublicationMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _LocatorExpirationDate;
        public string LocatorExpirationDate
        {
            get => _LocatorExpirationDate;
            set
            {
                if (value != _LocatorExpirationDate)
                {
                    _LocatorExpirationDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _LocatorExpirationDateWarning;
        public bool LocatorExpirationDateWarning
        {
            get => _LocatorExpirationDateWarning;
            set
            {
                if (value != _LocatorExpirationDateWarning)
                {
                    _LocatorExpirationDateWarning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _AssetWarning;
        public bool AssetWarning
        {
            get => _AssetWarning;
            set
            {
                if (value != _AssetWarning)
                {
                    _AssetWarning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Created;
        public string Created
        {
            get => _Created;
            set
            {
                if (value != _Created)
                {
                    _Created = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _LastModified;
        public string LastModified
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
