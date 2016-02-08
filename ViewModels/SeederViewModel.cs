using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThroneSeed.ViewModels
{
    public class SeederViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string SeedValueBackingField;

        public SeederViewModel()
        {
        }

        public string SeedValue
        {
            get { return SeedValueBackingField; }
            set
            {
                SeedValueBackingField = value;
                NotifyPropertyChanged("SeedValue");
                NotifyPropertyChanged("IsValidSeed");
            }
        }

        private void NotifyPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public bool IsValidSeed
        {
            get
            {
                if(string.IsNullOrWhiteSpace(SeedValueBackingField) || SeedValueBackingField.Length > 10)
                {
                    return false;
                }

                long result = 0;
                return long.TryParse(SeedValueBackingField, out result);
            }
        }
    }
}
