using System.Windows;
using System.Windows.Input;

namespace BeEmote.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields
        
        /// <summary>
        /// Indicates if the application is in the home screen.
        /// </summary>
        private bool _isAppInPresentationMode;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor. Initialize pages' instance
        /// and start on the presentation screen
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            GoToPresentationMode();
        }

        #endregion

        #region Private Methods (Page Navigation)

        /// <summary>
        /// Sets the height of the <see cref="NavBar"/> and the margin
        /// of the frame to make it invisible.
        /// </summary>
        private void HideNavBar()
        {
            NavBar.Height = 0;
            MainFrame.Margin = new Thickness(0, 0, 0, 0);
        }

        /// <summary>
        /// Sets the height of the <see cref="NavBar"/> and the margin
        /// of the frame to make it visible.
        /// </summary>
        private void ShowNavBar()
        {
            NavBar.Height = 30;
            MainFrame.Margin = new Thickness(0, 30, 0, 0);
        }
        
        /// <summary>
        /// Put the application in presentation mode (home).
        /// Hides the navigation bar, instantiate Presentation user control.
        /// </summary>
        private void GoToPresentationMode()
        {
            HideNavBar();
            MainFrame.Content = new PresentationView();
            _isAppInPresentationMode = true;
        }

        /// <summary>
        /// Put the application out of presentation mode (home).
        /// Shows the navigation bar and reset MainFrame.Content.
        /// </summary>
        private void GetOffPresentationMode()
        {
            ShowNavBar();
            MainFrame.Content = null;
            _isAppInPresentationMode = false;
        }

        #endregion

        #region Events

        /// <summary>
        /// When clicking on the BeEmote button in the navigation bar,
        /// Display the presentation screen and hide the navigation bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            GoToPresentationMode();
        }

        /// <summary>
        /// When clicking on the Emotion button in the navigation bar,
        /// switch content to <see cref="EmotionView"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmotionButton_Click(object sender, RoutedEventArgs e)
        {
            // Use the emotion control
            if (MainFrame.Content?.GetType() != typeof(EmotionView))
                MainFrame.Content = new EmotionView();
        }

        /// <summary>
        /// When clicking on the Text Analytics button in the navigation bar,
        /// switch content to <see cref="TextAnalyticsView"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            // Use the text analytics control
            if (MainFrame.Content?.GetType() != typeof(TextAnalyticsView))
                MainFrame.Content = new TextAnalyticsView();
        }

        /// <summary>
        /// When clicking on the presentation screen,
        /// hides it and show the navigation bar instead.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isAppInPresentationMode)
                GetOffPresentationMode();
        }

        #endregion
    }
}
