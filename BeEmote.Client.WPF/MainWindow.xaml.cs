using BeEmote.Services;
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
        /// Contains the application context
        /// </summary>
        private AppManager _AppManager;

        /// <summary>
        /// Indicates if the application is in the home screen.
        /// </summary>
        private bool IsAppInPresentationMode;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor. Initialize pages's instance
        /// and open on presentation page
        /// </summary>
        public MainWindow()
        {
            _AppManager = new AppManager();
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
        /// Hides the navigation bar, instantiate Presentation usercontrol.
        /// </summary>
        private void GoToPresentationMode()
        {
            HideNavBar();
            MainFrame.Content = new PresentationView();
            IsAppInPresentationMode = true;
        }

        /// <summary>
        /// Put the application out of presentation mode (home).
        /// Shows the navigation bar and reset MainFrame.Content.
        /// </summary>
        private void GetOffPresentationMode()
        {
            ShowNavBar();
            MainFrame.Content = null;
            IsAppInPresentationMode = false;
        }

        #endregion

        #region Events

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            GoToPresentationMode();
        }

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmotionButton_Click(object sender, RoutedEventArgs e)
        {
            // Use the emotion control
            MainFrame.Content = new EmotionView(_AppManager);
        }

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            // Use the text analytics control
            MainFrame.Content = new TextAnalyticsView(_AppManager);
        }

        /// <summary>
        /// When clicking on the presentation page,
        /// hides it and show the navigation bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsAppInPresentationMode)
                GetOffPresentationMode();
        }

        #endregion
    }
}
