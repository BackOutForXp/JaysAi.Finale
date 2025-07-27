// Neural v3.1 — InputDialog.xaml.cs
using System.Windows;

namespace JaysAi.Finale.UI
{
    public partial class InputDialog : Window
    {
        public string ResponseText { get; private set; } = string.Empty;

        public InputDialog(string prompt)
        {
            InitializeComponent();
            PromptText.Text = prompt;
            InputBox.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            ResponseText = InputBox.Text.Trim();
            DialogResult = true;
        }
    }
}
