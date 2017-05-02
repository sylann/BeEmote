using BeEmote.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

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

        /// <summary>
        /// TODO: Add rectangles in the dedicated canvas for each face found on the image.
        /// This method assumes that there actually are faces.
        /// </summary>
        /// <param name="emotion">The emotion response object</param>
        private void AddRectangleFace(EmotionApiResponse emotion)
        {
            // Add a rectangle in the canvas, for each faces found in the image (according to the API response)
            foreach (Face face in emotion.Faces)
            {
                // Create a new rectangle
                Rectangle re = new Rectangle();
                // Configure it
                re.SetValue(Canvas.LeftProperty, face.FaceRectangle.Left);
                re.SetValue(Canvas.TopProperty, face.FaceRectangle.Top);
                re.Width = face.FaceRectangle.Width;
                re.Height = face.FaceRectangle.Height;
                // Add visual border
                //re.
                // Add the rectangle to the canvas
                // canvas.Children.Add(rectangle);
            }
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
            AppInitialized = false;
            MainFrame.Content = new PresentationView();
        }

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmotionButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EmotionView();
        }

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
