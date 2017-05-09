using BeEmote.Services;
using System;
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
        #region Private Fields

        /// <summary>
        /// The application manager instance of the TextAnalytics ViewModel
        /// </summary>
        private TextAnalyticsManager TextAnalyticsApp;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor: Init components, set context, init global events.
        /// Optionnaly set dummy data.
        /// </summary>
        public TextAnalyticsView()
        {
            InitializeComponent();
            TextAnalyticsApp = new TextAnalyticsManager();
            DataContext = TextAnalyticsApp;
            TextToAnalyseBox.GotFocus += OnTextToAnalyseGotFocus;
            TextToAnalyseBox.LostFocus += OnTextToAnalyseLostFocus;
            TextAnalyticsApp.TextToAnalyse = "Input some text here...";
            
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Call the TextAnalytics request method.
        /// The binding in the WPF automatically handles the result.
        /// </summary>
        private async void HandleTextAnalyticsApiCall()
        {
            // Send the request
            await TextAnalyticsApp.Start();
        }

        #endregion
        
        #region Events

        /// <summary>
        /// The action that is executed when clicking on the Analyze button
        /// (Sends the TextAnalytics request).
        /// </summary>
        /// <param name="sender">Analyze button</param>
        /// <param name="e">Not used</param>
        private void SendTextButton_Click(object sender, RoutedEventArgs e)
        {
            HandleTextAnalyticsApiCall();
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
            TextAnalyticsApp.TextToAnalyse = "";
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

            if (String.IsNullOrWhiteSpace(TextAnalyticsApp.TextToAnalyse))
                TextAnalyticsApp.TextToAnalyse = "Input some text here...";
        }

        #endregion
    }
}
