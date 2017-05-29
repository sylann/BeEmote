using BeEmote.Core;
using BeEmote.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BeEmote.Client.WPF
{
    /// <summary>
    /// Interaction logic for EmotionView.xaml
    /// </summary>
    public partial class EmotionView : UserControl
    {
        #region Private Fields

        /// <summary>
        /// The application manager instance of the Emotion ViewModel
        /// </summary>
        private EmotionManager emotionApp;

        /// <summary>
        /// The last directory that contained a valid ImagePath
        /// </summary>
        private String memorizedDirectory;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates which is currently selected
        /// among the existing faces of an image.
        /// </summary>
        public int SelectedFaceIndex { get; set; }

        /// <summary>
        /// Contains the image to access to the initial size
        /// </summary>
        public BitmapImage InitialEmotionImage { get; set; }

        /// <summary>
        /// Container for the rectangles outside of the view
        /// </summary>
        public List<Rectangle> ImageRectangles { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Init the view, bring the Application context
        /// down from the main window, and set it as DataContext
        /// </summary>
        public EmotionView()
        {
            InitializeComponent();
            emotionApp = new EmotionManager();
            DataContext = emotionApp;
            // Listen to window resizing to update rectangles size
            ImageContainer.SizeChanged += OnCanvasSizeChanged;
            // Dummy data for testing purposes
            emotionApp.ImagePath = "http://s.eatthis-cdn.com/media/images/ext/543627202/happy-people-friends.jpg";
        }

        #endregion

        #region Private Methods (Common)

        /// <summary>
        /// Sends the request with the provided image,
        /// then handles the result.
        /// </summary>
        private async void HandleEmotionApiCall()
        {
            // Send the request
            await emotionApp.Start();
            // Data updates with Bindings,
            // Handle the result only if it is consistent
            if (emotionApp.State != RequestStates.EmptyResult)
            {
                ClearCanvas(RectangleContainer);
                AddRectanglesToList(emotionApp.Response.Faces);
                AddRectanglesToCanvas(ImageRectangles, RectangleContainer);
                SelectFaceRectangle(ImageRectangles[0]); // Select 1st face per default
                LoadCurrentFaceStats();
            }
        }

        /// <summary>
        /// Clears the current content of the canvas,
        /// Sets (or resets) the <see cref="ImageRectangles"/> container,
        /// Sets the <see cref="EmotionImage"/> according to the <see cref="ImagePath"/>
        /// </summary>
        private void LoadEmotionImage()
        {
            ResetFaceStats();
            emotionApp.Reset(); // removes fail messages
            ClearCanvas(RectangleContainer);

            // new image
            try
            {
                var uri = new Uri(emotionApp.ImagePath, UriKind.Absolute);
                InitialEmotionImage = new BitmapImage(uri);
            }
            catch (Exception)
            {
                InitialEmotionImage = null;// reset any existing source
            }
            finally
            {
                EmotionImage.Source = InitialEmotionImage;
            }
        }

        /// <summary>
        /// Sets the Emotion elements of the Image Analysis part,
        /// with the data corresponding to the currently selected face.
        /// </summary>
        private void LoadCurrentFaceStats()
        {
            var currentFace = emotionApp.Response.Faces.ElementAt(SelectedFaceIndex);
            DominantEmotionLabel.Content = currentFace.GetDominantEmotion();
            AngerLabel.Content = currentFace.Scores.AngerHR;
            ContemptLabel.Content = currentFace.Scores.ContemptHR;
            DisgustLabel.Content = currentFace.Scores.DisgustHR;
            FearLabel.Content = currentFace.Scores.FearHR;
            HappinessLabel.Content = currentFace.Scores.HappinessHR;
            NeutralLabel.Content = currentFace.Scores.NeutralHR;
            SadnessLabel.Content = currentFace.Scores.SadnessHR;
            SurpriseLabel.Content = currentFace.Scores.SurpriseHR;
        }

        /// <summary>
        /// Resets the Emotion elements of the Image Analysis part to null
        /// </summary>
        private void ResetFaceStats()
        {
            SelectedFaceLabel.Content = null;
            DominantEmotionLabel.Content = null;
            AngerLabel.Content = null;
            ContemptLabel.Content = null;
            DisgustLabel.Content = null;
            FearLabel.Content = null;
            HappinessLabel.Content = null;
            NeutralLabel.Content = null;
            SadnessLabel.Content = null;
            SurpriseLabel.Content = null;
        }

        /// <summary>
        /// Opens a File selection dialog and returns the path to a
        /// selected element or null if none were selected.
        /// </summary>
        /// <returns>Nullable string</returns>
        private string GetLocalFilePath()
        {
            // Open the file browsing dialog
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = memorizedDirectory ?? @"c:\",
                Filter = String.Format("Image Files({0})|{0}", "*.jpg;*.png;*.jpeg;*.jpe;*.gif;*.bmp"),
                FilterIndex = 2,
                CheckPathExists = true
            };
            return openFileDialog.ShowDialog() == false
                ? null                      // No file selected (cancel or closed)
                : openFileDialog.FileName;  // File selected
        }

        #endregion

        #region Private Methods (Rectangles)

        /// <summary>
        /// Create a <see cref="Rectangle"/> for each face found on the image,
        /// Add them in the <see cref="ImageRectangles"/> container.
        /// </summary>
        /// <param name="emotion">The response from the API</param>
        private void AddRectanglesToList(List<Face> faces)
        {
            // Init or reset
            ImageRectangles = new List<Rectangle>();
            // Add a rectangle in the canvas, for each faces found in the image (according to the API response)
            int i = 0;
            foreach (Face face in faces)
            {
                Rectangle newFaceRectangle = MakeFaceRectangle(i++, face);
                // Add the rectangle to the list
                ImageRectangles.Add(newFaceRectangle);
            }
        }

        /// <summary>
        /// Builds a <see cref="Rectangle"/> element from the information
        /// stored in The <see cref="Face"/> object.
        /// Also adds binding in the element.
        /// </summary>
        /// <param name="i">Unique identifier</param>
        /// <param name="face">The corresponding <see cref="Face"/></param>
        /// <returns></returns>
        private Rectangle MakeFaceRectangle(int i, Face face)
        {
            // Create a new rectangle
            var faceBox = new Rectangle()
            {
                Uid = i.ToString(),
                Cursor = Cursors.Hand,
                Stroke = (SolidColorBrush)FindResource("WhiteBrush"),
                Fill = new SolidColorBrush(Colors.Transparent),
                StrokeThickness = 3
            };
            SetPositionAndSize(faceBox, face);
            faceBox.MouseDown += OnFaceRectangleSelected;

            return faceBox;
        }

        /// <summary>
        /// Puts the stroke of each rectangles to default application white.
        /// </summary>
        private void ResetFaceSelection()
        {
            foreach (Rectangle re in ImageRectangles)
                re.Stroke = (SolidColorBrush)FindResource("WhiteBrush");
        }

        /// <summary>
        /// Sets the color of the rectangle to the application Blue,
        /// And update the <see cref="SelectedFaceIndex"/>.
        /// </summary>
        /// <param name="re">Target rectangle</param>
        private void SelectFaceRectangle(Rectangle re)
        {
            re.Stroke = (SolidColorBrush)FindResource("BackgroundBlueBrush");
            SelectedFaceLabel.Content = SelectedFaceIndex = int.Parse(re.Uid);
        }

        #endregion

        #region Private Methods (Canvas)

        /// <summary>
        /// Add rectangles in the dedicated canvas for each face found on the image.
        /// </summary>
        /// <param name="emotion">The emotion response object</param>
        private void AddRectanglesToCanvas(List<Rectangle> rectangles, Canvas canvas)
        {
            foreach (var rectangle in rectangles)
                canvas.Children.Add(rectangle);
        }

        /// <summary>
        /// Clear the rectangles from the provided <paramref name="canvas"/> parameter
        /// </summary>
        private void ClearCanvas(Canvas canvas)
        {
            canvas.Children.Clear();
        }

        /// <summary>
        /// Updates the position, width and height of the <paramref name="rectangle"/>,
        /// according to the proportions of the image and its container.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="face"></param>
        private void SetPositionAndSize(Rectangle rectangle, Face face)
        {
            var drawer = new RectangleDrawer();
            // Reposition Left
            rectangle.SetValue(Canvas.LeftProperty, drawer.LeftResize(
                face.FaceRectangle.Left,
                ImageContainer.ActualWidth,
                EmotionImage.ActualWidth,
                InitialEmotionImage.Width));
            // Reposition Top
            rectangle.SetValue(Canvas.TopProperty, drawer.TopResize(
                face.FaceRectangle.Top,
                ImageContainer.ActualHeight,
                EmotionImage.ActualHeight,
                InitialEmotionImage.Height));
            // Reposition Width
            rectangle.Width = drawer.WidthResize(
                face.FaceRectangle.Width,
                InitialEmotionImage.Width,
                EmotionImage.ActualWidth);
            // Reposition Height
            rectangle.Height = drawer.HeightResize(
                face.FaceRectangle.Height,
                InitialEmotionImage.Height,
                EmotionImage.ActualHeight);
        }

        #endregion

        #region Events

        /// <summary>
        /// The action executed when The user clicks on the "Analyze" Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalyzeImageButton_Click(object sender, RoutedEventArgs e)
        {
            HandleEmotionApiCall();
        }

        /// <summary>
        /// Visually identifies the face rectangle on which the user clicked,
        /// and reloads the corresponding data.
        /// </summary>
        /// <param name="sender">The clicked <see cref="Rectangle"/></param>
        /// <param name="e">Not used</param>
        /// <returns></returns>
        private void OnFaceRectangleSelected(object sender, EventArgs e)
        {
            ResetFaceSelection();
            SelectFaceRectangle((Rectangle)sender);
            LoadCurrentFaceStats();
        }

        /// <summary>
        /// Updates the size and position of each face rectangle in the canvas,
        /// </summary>
        /// <param name="sender">The <see cref="ImageContainer"/> canvas</param>
        /// <param name="e">Not used</param>
        private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ImageRectangles?.Count > 0)
                foreach (Rectangle re in ImageRectangles)
                {
                    int index = int.Parse(re.Uid);
                    Face face = emotionApp.Response.Faces[index];
                    SetPositionAndSize(re, face);
                }
        }

        /// <summary>
        /// Opens the file dialog window to select a file of type image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Open file selection dialog. Gets resolved on box closed.
            // This will trigger ImagePathTextBox_TextChanged
            emotionApp.ImagePath = GetLocalFilePath();

            // Useful directory to memorize
            memorizedDirectory = emotionApp.ImageFolder;
        }

        /// <summary>
        /// Reload Image Source when text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImagePathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadEmotionImage();
        }

        #endregion
    }
}
