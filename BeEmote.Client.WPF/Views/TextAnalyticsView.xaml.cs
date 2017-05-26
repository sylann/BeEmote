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
        /// Default constructor: Init components, set context.
        /// Optionally set dummy data.
        /// </summary>
        public TextAnalyticsView()
        {
            InitializeComponent();
            TextAnalyticsApp = new TextAnalyticsManager();
            DataContext = TextAnalyticsApp;
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
        private void AnalyzeTextButton_Click(object sender, RoutedEventArgs e)
        {
            HandleTextAnalyticsApiCall();
        }
        
        #endregion
    }
}
