using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace ContractClaim
{
    public partial class SubmitClaimPage : Page
    {
        private string selectedFilePath = string.Empty;

        public SubmitClaimPage()
        {
            InitializeComponent();
            txtHours.TextChanged += CalculateTotal;
            txtRate.TextChanged += CalculateTotal;
        }

        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            try
            {
                double hours = 0;
                double rate = 0;

                double.TryParse(txtHours.Text, out hours);
                double.TryParse(txtRate.Text, out rate);

                double total = hours * rate;
                txtTotal.Text = $"Total Amount: ${total:F2}";
            }
            catch (Exception)
            {
                txtTotal.Text = "Total Amount: $0.00";
            }
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Documents|*.pdf;*.docx;*.xlsx|All Files|*.*";
            openFileDialog.Title = "Select Supporting Document";

            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                if (fileInfo.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("File size exceeds 5MB limit.", "File Too Large",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedFilePath = openFileDialog.FileName;
                txtFileName.Text = fileInfo.Name;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter lecturer name.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double hours;
                double rate;

                if (!double.TryParse(txtHours.Text, out hours) || hours <= 0)
                {
                    MessageBox.Show("Please enter valid hours worked.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!double.TryParse(txtRate.Text, out rate) || rate <= 0)
                {
                    MessageBox.Show("Please enter valid hourly rate.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Simulate saving claim
                double totalAmount = hours * rate;
                string claimInfo = $"Claim Submitted!{Environment.NewLine}{Environment.NewLine}" +
                                 $"Name: {txtName.Text}{Environment.NewLine}" +
                                 $"Hours: {hours}{Environment.NewLine}" +
                                 $"Rate: ${rate:F2}{Environment.NewLine}" +
                                 $"Total: ${totalAmount:F2}{Environment.NewLine}" +
                                 $"File: {(string.IsNullOrEmpty(selectedFilePath) ? "None" : Path.GetFileName(selectedFilePath))}";

                MessageBox.Show(claimInfo, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting claim: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtHours.Text = "0";
            txtRate.Text = "0";
            txtNotes.Clear();
            txtFileName.Text = "No file chosen";
            selectedFilePath = string.Empty;
        }
    }
}