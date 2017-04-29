using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeEmote.Core;
using BeEmote.Services;

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

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor. Initialize pages's instance
        /// and open on presentation page
        /// </summary>
        public MainWindow()
        {
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

        private void EmotionButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EmotionView();
        }

        private void TextAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TextAnalyticsView();
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
