using System.Windows;
using ThroneSeed.ViewModels;

namespace ThroneSeed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SeederViewModel ViewModel = new SeederViewModel();

        public MainWindow()
        {
            InitializeComponent();
            Seed.Focus();

            DataContext = ViewModel;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetSeed(Seed.Text);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearSeed();
        }
    }
}
