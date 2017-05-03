using BeEmote.Core;
using BeEmote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public BitmapImage EmotionImageBitmap { get; set; }

        /// <summary>
        /// Container for the rectangles outside of the view
        /// </summary>
        public List<Rectangle> ImageRectangles { get; set; }

        #endregion

        #region Constructor

        public EmotionView(AppManager _AppManager)
        {
            InitializeComponent();
            this._AppManager = _AppManager;
            DataContext = _AppManager;
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the needed data into the Api Object and send the request.
        /// Then handles the result
        /// </summary>
        private async void HandleEmotionApiCall()
        {
            ClearCanvas();
            ImageRectangles = new List<Rectangle>();
            // Pick the image path from the url field
            // and set the corresponding property in the view model
            _AppManager.ImagePath = ImagePathTextBox.Text;
            // Instantiate the image and put it in the view
            EmotionImage.Source = EmotionImageBitmap = new BitmapImage(new Uri(_AppManager.ImagePath, UriKind.Absolute));
            
            // Send the request
            await _AppManager.StartEmotion(); //ex:  http://s.eatthis-cdn.com/media/images/ext/543627202/happy-people-friends.jpg
            // Data updates with Bindings,
            // Handle the result only if it is consistent
            if (_AppManager.State != AwaitStatus.EmptyResult && _AppManager.EmotionResponse.Faces.Count > 0)
            {
                SelectedFaceIndex = 0;
                AddRectanglesToList(_AppManager.EmotionResponse.Faces);
                AddRectanglesToCanvas();
            }
        }

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
                // Create a new rectangle
                Rectangle re = new Rectangle();
                // Configure it
                re.SetBinding(Canvas.LeftProperty, new Binding() { Source = HorizontalResize(face.FaceRectangle.Left) });
                re.SetBinding(Canvas.TopProperty, new Binding() { Source = VerticalResize(face.FaceRectangle.Top) });
                re.SetBinding(WidthProperty, new Binding() { Source = HorizontalResize(face.FaceRectangle.Width) });
                re.SetBinding(HeightProperty, new Binding() { Source = VerticalResize(face.FaceRectangle.Height) });
                // Add visual borders
                re.Stroke = new SolidColorBrush(Colors.White);
                re.StrokeThickness = 3;
                re.Name = $"face{i++}"; // Review: Could allow to identify each face 

                // Add the rectangle to the list
                ImageRectangles.Add(re);
            }
        }

        /// <summary>
        /// Add rectangles in the dedicated canvas for each face found on the image.
        /// </summary>
        /// <param name="emotion">The emotion response object</param>
        private void AddRectanglesToCanvas() => ImageRectangles.ForEach(r => RectangleContainer.Children.Add(r));

        /// <summary>
        /// Clear the rectangles from the canvas
        /// </summary>
        private void ClearCanvas() => RectangleContainer.Children.Clear();

        /// <summary>
        /// Re Evaluates a vertical dimension after a resize of the canvas
        /// </summary>
        /// <param name="val">A vertical dimension (top position or height)</param>
        /// <returns>The new dimension</returns>
        private double VerticalResize(double val) => val / (EmotionImageBitmap.Height / EmotionImage.ActualHeight);

        /// <summary>
        /// Re Evaluates a horizontal dimension after a resize of the canvas
        /// </summary>
        /// <param name="val">A horizontal dimension (left position or width)</param>
        /// <returns>The new dimension</returns>
        private double HorizontalResize(double val) => val / (EmotionImageBitmap.Width / EmotionImage.ActualWidth);

        #endregion
    }
}
