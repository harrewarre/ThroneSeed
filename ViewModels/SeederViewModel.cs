using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThroneSeed.ViewModels
{
    public class SeederViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string SeedValueBackingField;
        private string SeedFilePath;

        public SeederViewModel()
        {
            SeedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "nuclearthrone", "seed.txt");
            SeedValue = LoadSeed();
        }

        public void SetSeed(string newSeed)
        {
            using (var writer = new StreamWriter(SeedFilePath, false))
            {
                writer.WriteLine(newSeed);
            }
        }

        public void ClearSeed()
        {
            if(File.Exists(SeedFilePath))
            {
                File.Delete(SeedFilePath);
            }

            SeedValue = string.Empty;
        }

        private string LoadSeed()
        {
            if(!File.Exists(SeedFilePath))
            {
                return string.Empty;
            }

            using (var reader = new StreamReader(SeedFilePath))
            {
                return reader.ReadToEnd().Trim();
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
    }
}
