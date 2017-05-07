using BeEmote.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            TextToAnalyseBox.GotFocus += OnTextToAnalyseGotFocus;
            TextToAnalyseBox.LostFocus += OnTextToAnalyseLostFocus;
            _AppManager.TextToAnalyse = "Input some text here...";
        }

        private void SendTextButton_Click(object sender, RoutedEventArgs e)
        {
            HandleTextAnalyticsApiCall();
        }

        private async void HandleTextAnalyticsApiCall()
        {
            // Send the request
            await _AppManager.StartTextAnalytics();
        }

        /// <summary>
        /// Removes the PlaceHolder of the TextBlock.
        /// Also updates foreground color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTextToAnalyseGotFocus(object sender, EventArgs e)
        {
            TextToAnalyseBox.Foreground = FindResource("TextDarkBlueBrush") as SolidColorBrush;
            _AppManager.TextToAnalyse = "";
        }

        /// <summary>
        /// Insert default PlaceHolder providing some intel to the user.
        /// Also updates foreground color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTextToAnalyseLostFocus(object sender, EventArgs e)
        {
            TextToAnalyseBox.Foreground = FindResource("TextGrayBrush") as SolidColorBrush;

            if (String.IsNullOrWhiteSpace(_AppManager.TextToAnalyse))
                _AppManager.TextToAnalyse = "Input some text here...";
        }
    }
}
