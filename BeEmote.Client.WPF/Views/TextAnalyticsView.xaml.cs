using BeEmote.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BeEmote.Client.WPF
{
    /// <summary>
    /// Interaction logic for TextAnalyticsView.xaml
    /// </summary>
    public partial class TextAnalyticsView : UserControl
    {
        private AppManager _AppManager;

        public TextAnalyticsView(AppManager _AppManager)
        {
            InitializeComponent();
            this._AppManager = _AppManager;
            DataContext = _AppManager;
        }

        private void SendTextButton_Click(object sender, RoutedEventArgs e)
        {
            HandleTextAnalyticsApiCall();
        }

        private async void HandleTextAnalyticsApiCall()
        {
            // Pick the image path from the url field
            // and set the corresponding property in the view model
            _AppManager.TextToAnalyse = TextToAnalyseTextBox.Text;

            // Send the request
            await _AppManager.StartTextAnalytics();
            // Data updates with Bindings
        }
    }
}
