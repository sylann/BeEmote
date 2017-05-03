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
        /// Indicates that the presentation page has been closed.
        /// The app is ready to go.
        /// </summary>
        private bool AppInitialized = false;

        /// <summary>
        /// Contains the application context
        /// </summary>
        private AppManager _AppManager;

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
            // Hide the Navigation bar
            HideNavBar();
            MainFrame.Content = new PresentationView();
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

        #endregion

        #region Events

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HideNavBar();
            MainFrame.Content = new PresentationView();
            // Reset
            AppInitialized = false;
            DataContext = null;
            _AppManager = null;
        }

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmotionButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset the application
            //_AppManager = new AppManager();
            // Link it to the view
            //DataContext = _AppManager;
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
            // Reset the application
            //_AppManager = new AppManager();
            // Link it to the view
            //DataContext = _AppManager;
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
            // Only for the first time on the présentation page
            if (!AppInitialized)
            {
                ShowNavBar();
                MainFrame.Content = null;
                AppInitialized = true;
            }
        }

        #endregion
    }
}
