using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ContractClaim
{
    public partial class ApproveClaimsPage : Page
    {
        private ObservableCollection<ClaimItem> claimsList = new ObservableCollection<ClaimItem>();

        public ApproveClaimsPage()
        {
            InitializeComponent();
            LoadSampleClaims();
        }

        private void LoadSampleClaims()
        {
            claimsList.Clear();
            claimsList.Add(new ClaimItem() { Id = 1, Name = "Dr. John Smith", Hours = 20, Rate = "$45.00", Total = "$900.00", Status = "Pending" });
            claimsList.Add(new ClaimItem() { Id = 2, Name = "Prof. Jane Doe", Hours = 15, Rate = "$50.00", Total = "$750.00", Status = "Pending" });
            claimsList.Add(new ClaimItem() { Id = 3, Name = "Dr. Mike Johnson", Hours = 25, Rate = "$40.00", Total = "$1000.00", Status = "Pending" });

            lstClaims.ItemsSource = claimsList;
        }

        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                UpdateClaimStatus((int)button.Tag, "Approved");
            }
        }

        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                UpdateClaimStatus((int)button.Tag, "Rejected");
            }
        }

        private void UpdateClaimStatus(int claimId, string newStatus)
        {
            var claim = claimsList.FirstOrDefault(c => c.Id == claimId);
            if (claim != null)
            {
                claim.Status = newStatus;
                MessageBox.Show($"Claim {newStatus.ToLower()} successfully!", "Success",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadSampleClaims();
            MessageBox.Show("Claims list refreshed!", "Refresh",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class ClaimItem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public string Rate { get; set; }
        public string Total { get; set; }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}