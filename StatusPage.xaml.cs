using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ContractClaim
{
    public partial class StatusPage : Page
    {
        private ObservableCollection<StatusItem> statusList = new ObservableCollection<StatusItem>();

        public StatusPage()
        {
            InitializeComponent();
            LoadStatusData();
        }

        private void LoadStatusData()
        {
            statusList.Clear();
            statusList.Add(new StatusItem() { Id = 1, Name = "Dr. John Smith", Total = "$900.00", Status = "Approved", Progress = 100 });
            statusList.Add(new StatusItem() { Id = 2, Name = "Prof. Jane Doe", Total = "$750.00", Status = "Pending", Progress = 50 });
            statusList.Add(new StatusItem() { Id = 3, Name = "Dr. Mike Johnson", Total = "$1000.00", Status = "Rejected", Progress = 100 });
            statusList.Add(new StatusItem() { Id = 4, Name = "Dr. Sarah Wilson", Total = "$600.00", Status = "Pending", Progress = 50 });

            lstStatus.ItemsSource = statusList;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadStatusData();
            MessageBox.Show("Status updated!", "Refresh",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class StatusItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Total { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }

        public Brush StatusColor
        {
            get
            {
                switch (Status)
                {
                    case "Approved":
                        return new SolidColorBrush(Color.FromRgb(39, 174, 96)); // Green
                    case "Rejected":
                        return new SolidColorBrush(Color.FromRgb(231, 76, 60)); // Red
                    default:
                        return new SolidColorBrush(Color.FromRgb(243, 156, 18)); // Orange
                }
            }
        }
    }
}