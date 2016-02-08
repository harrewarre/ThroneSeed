using System;
using System.ComponentModel;
using System.IO;

namespace ThroneSeed.ViewModels
{
    public class SeederViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string SeedValueBackingField;
        private string SeedFilePath;

        public string SeedMessageBackingField;

        public SeederViewModel()
        {
            SeedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "nuclearthrone", "seed.txt");
            SeedValue = LoadSeed();
            SeedMessage = "Set or change the seed using the input below.";
        }

        public void SetSeed(string newSeed)
        {
            try
            {
                using (var writer = new StreamWriter(SeedFilePath, false))
                {
                    writer.WriteLine(newSeed);
                }

                SeedMessage = string.Format("Seed set to {0} !", newSeed);
            }
            catch (Exception ex)
            {
                SeedMessage = "Failed to write seed to seedfile!";
                Console.Write(ex);
            }
        }

        public void ClearSeed()
        {
            try
            {
                if (File.Exists(SeedFilePath))
                {
                    File.Delete(SeedFilePath);
                }

                SeedValue = string.Empty;
                SeedMessage = "Seed cleared!";
            }
            catch (Exception ex)
            {
                SeedMessage = "Failed to clear seedfile!";
                Console.Write(ex);
            }
        }

        private string LoadSeed()
        {
            try
            {
                if (!File.Exists(SeedFilePath))
                {
                    return string.Empty;
                }

                using (var reader = new StreamReader(SeedFilePath))
                {
                    return reader.ReadToEnd().Trim();
                }
            }
            catch (Exception ex)
            {
                SeedMessage = "Failed to load seed from seedfile!";
                Console.Write(ex);

                return string.Empty;
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

        public string SeedMessage
        {
            get { return SeedMessageBackingField; }
            set
            {
                SeedMessageBackingField = value;
                NotifyPropertyChanged("SeedMessage");
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
