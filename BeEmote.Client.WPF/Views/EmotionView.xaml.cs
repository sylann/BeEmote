using BeEmote.Core;
using BeEmote.Services;
using System;
using System.Collections.ObjectModel;
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

        private AppManager _AppManager;

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
        /// <param name="_AppManager"></param>
        public EmotionView(AppManager _AppManager)
        {
            InitializeComponent();
            this._AppManager = _AppManager;
            DataContext = _AppManager;
            // Listen to window resizing to update rectangles size
            ImageContainer.SizeChanged += OnCanvasSizeChanged;
            // Dummy data for testing purposes
            _AppManager.ImagePath = "http://s.eatthis-cdn.com/media/images/ext/543627202/happy-people-friends.jpg";
        }

        #endregion

        #region Private Methods (Common)

        /// <summary>
        /// Sends the request with the provided image,
        /// then handles the result.
        /// </summary>
        private async void HandleEmotionApiCall()
        {
            LoadEmotionImage();
            // Send the request
            await _AppManager.StartEmotion();
            // Data updates with Bindings,
            // Handle the result only if it is consistent
            if (_AppManager.State != AwaitStatus.EmptyResult)
            {
                AddRectanglesToList(_AppManager.EmotionResponse.Faces);
                AddRectanglesToCanvas();
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
            ClearCanvas();
            ImageRectangles = new List<Rectangle>();

            // Instantiate the image and put it in the view
            var uri = new Uri(_AppManager.ImagePath, UriKind.Absolute);
            EmotionImage.Source = InitialEmotionImage = new BitmapImage(uri);
        }

        /// <summary>
        /// Sets the Emotion elements of the Image Analyses part,
        /// with the data corresponding to the currently selected face.
        /// </summary>
        private void LoadCurrentFaceStats()
        {
            var currentFace = _AppManager.EmotionResponse.Faces.ElementAt(SelectedFaceIndex);
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

        #endregion

        #region Private Methods (Rectangles)

        /// <summary>
        /// Create a <see cref="Rectangle"/> for each face found on the image,
        /// Add them in the <see cref="ImageRectangles"/> container.
        /// </summary>
        /// <param name="emotion">The response from the API</param>
        private void AddRectanglesToList(List<Face> faces)
        {
            int i = 0;
            // Add a rectangle in the canvas, for each faces found in the image (according to the API response)
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
        private void AddRectanglesToCanvas()
        {
            foreach (var rectangle in ImageRectangles)
                RectangleContainer.Children.Add(rectangle);
        }

        /// <summary>
        /// Clear the rectangles from the canvas
        /// </summary>
        private void ClearCanvas()
        {
            RectangleContainer.Children.Clear();
        }

        /// <summary>
        /// Updates the position, width and height of the <paramref name="rectangle"/>,
        /// according to the proportions of the image and its container.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="face"></param>
        private void SetPositionAndSize(Rectangle rectangle, Face face)
        {
            rectangle.SetValue(Canvas.LeftProperty, LeftResize(face.FaceRectangle.Left));
            rectangle.SetValue(Canvas.TopProperty, TopResize(face.FaceRectangle.Top));
            rectangle.Width = WidthResize(face.FaceRectangle.Width);
            rectangle.Height = HeightResize(face.FaceRectangle.Height);
        }

        /// <summary>
        /// Updates the distance between the left of the faceBox
        /// and the left of the<see cref="ImageContainer"/>
        /// </summary>
        /// <param name="top">Current distance</param>
        /// <returns>Updated distance</returns>
        private object LeftResize(int left)
        {
            double offset = (ImageContainer.ActualWidth - EmotionImage.ActualWidth) / 2;
            return offset + WidthResize(left);
        }

        /// <summary>
        /// Updates the distance between the top of the faceBox
        /// and the top of the<see cref="ImageContainer"/>
        /// </summary>
        /// <param name="top">Current distance</param>
        /// <returns>Updated distance</returns>
        private object TopResize(int top)
        {
            double offset = (ImageContainer.ActualHeight - EmotionImage.ActualHeight) / 2;
            return offset + HeightResize(top);
        }

        /// <summary>
        /// Re Evaluates a horizontal dimension after a resize of the canvas
        /// </summary>
        /// <param name="val">A horizontal dimension (left position or width)</param>
        /// <returns>The new dimension</returns>
        private double WidthResize(double val)
        {
            double HorizontalRatio = InitialEmotionImage.Width / EmotionImage.ActualWidth;
            return val / HorizontalRatio;
        }

        /// <summary>
        /// Re Evaluates a vertical dimension after a resize of the canvas
        /// </summary>
        /// <param name="val">A vertical dimension (top position or height)</param>
        /// <returns>The new dimension</returns>
        private double HeightResize(double val)
        {
            double VerticalRatio = InitialEmotionImage.Height / EmotionImage.ActualHeight;
            return val / VerticalRatio;
        }

        #endregion

        #region Events

        /// <summary>
        /// The action executed when The user clicks on the "Send Image" Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendImageButton_Click(object sender, RoutedEventArgs e)
        {
            HandleEmotionApiCall();
        }

        /// <summary>
        /// Visually identifies the face rectangle on which the user clicked,
        /// and reloads the corresponding analyse data.
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
                    Face face = _AppManager.EmotionResponse.Faces[index];
                    SetPositionAndSize(re, face);
                }
        }

        #endregion
    }
}
