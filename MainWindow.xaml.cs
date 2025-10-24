using System.Windows;
using System.Windows.Controls;

namespace ContractClaim
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowHomePage();
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Content.ToString())
                {
                    case "Submit Claim":
                        MainFrame.Navigate(new SubmitClaimPage());
                        break;
                    case "Approve Claims":
                        MainFrame.Navigate(new ApproveClaimsPage());
                        break;
                    case "View Status":
                        MainFrame.Navigate(new StatusPage());
                        break;
                }
            }
        }

        private void ShowHomePage()
        {
            var stackPanel = new StackPanel();
            stackPanel.VerticalAlignment = VerticalAlignment.Center;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;

            var welcomeText = new TextBlock();
            welcomeText.Text = "Welcome to Contract Claim Management System";
            welcomeText.FontSize = 28;
            welcomeText.FontWeight = FontWeights.Bold;
            welcomeText.Foreground = System.Windows.Media.Brushes.DarkBlue;
            welcomeText.TextAlignment = TextAlignment.Center;
            welcomeText.Margin = new Thickness(0, 0, 0, 20);

            var subText = new TextBlock();
            subText.Text = "Select an option from the sidebar to get started";
            subText.FontSize = 16;
            subText.Foreground = System.Windows.Media.Brushes.Gray;
            subText.TextAlignment = TextAlignment.Center;

            stackPanel.Children.Add(welcomeText);
            stackPanel.Children.Add(subText);

            var page = new Page();
            page.Background = System.Windows.Media.Brushes.White;
            page.Content = stackPanel;
            MainFrame.Navigate(page);
        }
    }
}