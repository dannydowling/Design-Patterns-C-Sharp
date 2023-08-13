//using Android.Graphics.Drawables;
//using Android.Graphics;
//using Android.Hardware;
//using Android.Util;
//using Android.Widget;
//using System;
//using Xamarin.Essentials;
//using Xamarin.Forms;
//using static Android.Widget.ImageView;
//using Android.Content;
//using Android.Content.Res;

//namespace Density.Business_Layer.Repositories
//{
//    public class LocationObject
//    {
//        public float Latitude { get; set; }
//        public float Longitude { get; set; }
//        public float Altitude { get; set; }
//    }

//    public class DestinationObject : LocationObject
//    { }

//    public class CompassClass
//    {
//        // this is used to point an arrow bitmap that's passed in, to coordinates and altitude.
//        // the arrow bitmap is rendered directly for speed to the UI via an imageview.
//        // the LocationObject could be thought of as the phone, and the DestinationObject, the thing to go to.


//        public Bitmap modifiedArrow { get; set; }

//        public ImageView _arrow; // the temporary image displayed on screen
//        public Label fieldBearing; // North, South, East, West
//        public Context context; // give the imageview access to the UI

//        public event EventHandler<SensorChangedEventArgs> SensorEventHandler;

//        public CompassClass(DestinationObject destinationObject, Bitmap arrow)
//        {
//            // how fast to update the UI
//            SensorSpeed speed = SensorSpeed.UI;

//            try
//            {
//                // start it if it's not monitoring the compass, raise an event when the compass reading changes
//                if (!Compass.IsMonitoring) Compass.Start(speed);
//                Compass.ReadingChanged += onSensorChanged;
//            }

//            catch (FeatureNotSupportedException fnsEx)
//            {
//                Console.WriteLine(fnsEx.Message + "feature not supported on device");
//            }

//            void onSensorChanged(object sender, CompassChangedEventArgs compassEvent)
//            {

//                var reading = compassEvent.Reading;
//                if (reading == null) return;

//                // the way the phone is pointing is the sender
//                LocationObject fromDirectionBearing = sender as LocationObject;
//                if (fromDirectionBearing == null) return;

//                float baseAzimuth = 0.00F;

//                GeomagneticField geoField = new GeomagneticField(
//                   fromDirectionBearing.Latitude,
//                    fromDirectionBearing.Longitude,
//                    fromDirectionBearing.Altitude,
//                    DateTime.Now.Millisecond);

//                baseAzimuth -= geoField.Declination; // converts magnetic north into true north

//                // Store the bearingTo in the bearTo variable
//                double bearingToLatittude = calculateTheta(fromDirectionBearing.Latitude, destinationObject.Latitude);
//                double bearingToLongitude = calculateTheta(fromDirectionBearing.Longitude, destinationObject.Longitude);
//                double bearingToAltitude = calculateTheta(fromDirectionBearing.Altitude, destinationObject.Altitude);

//                // if the bearing is smaller than 0, add 360 to correct the orientation (such as making it spin clockwise).
//                if (bearingToLatittude < 0)
//                { bearingToLatittude = bearingToLatittude + 360; }

//                if (bearingToLongitude < 0)
//                { bearingToLongitude = bearingToLongitude + 360; }

//                if (bearingToAltitude < 0)
//                { bearingToLatittude = bearingToAltitude + 360; }

//                // where we choose to point it
//                double directionLatittude = bearingToLatittude - baseAzimuth;
//                double directionLongitude = bearingToLongitude - baseAzimuth;
//                double directionAltitude = bearingToAltitude - baseAzimuth;

//                // If the direction is smaller than 0, add 360 to get the rotation clockwise.
//                if (directionLatittude < 0)
//                { directionLatittude = directionLatittude + 360; }

//                if (directionLongitude < 0)
//                { directionLongitude = directionLongitude + 360; }

//                if (directionAltitude < 0)
//                { directionLatittude = directionAltitude + 360; }

//                try
//                {
//                    // trying to update the view directly on the page without returning an image that then has to be rendered.

//                    _arrow = new ImageView(context);

//                    //ToDo: get the arrow from the resources

//                    _arrow.SetImageBitmap(arrow);
//                    rotateImageView(_arrow, 1, directionLatittude);
//                    rotateImageView(_arrow, 1, directionLongitude);
//                    rotateImageView(_arrow, 1, directionAltitude);
//                }

//                // if that doesn't work, return a new image
//                catch (AndroidException)
//                {
//                    rotateBmp(arrow, directionLatittude);
//                    rotateBmp(arrow, directionLongitude);
//                    rotateBmp(arrow, directionAltitude);
//                }

//                // set the field indication text
//                string bearingText = "N";

//                if ((360 >= baseAzimuth && baseAzimuth >= 337.5) || (0 <= baseAzimuth && baseAzimuth <= 22.5)) bearingText = "N";
//                else if (baseAzimuth > 22.5 && baseAzimuth < 67.5) bearingText = "NE";
//                else if (baseAzimuth >= 67.5 && baseAzimuth <= 112.5) bearingText = "E";
//                else if (baseAzimuth > 112.5 && baseAzimuth < 157.5) bearingText = "SE";
//                else if (baseAzimuth >= 157.5 && baseAzimuth <= 202.5) bearingText = "S";
//                else if (baseAzimuth > 202.5 && baseAzimuth < 247.5) bearingText = "SW";
//                else if (baseAzimuth >= 247.5 && baseAzimuth <= 292.5) bearingText = "W";
//                else if (baseAzimuth > 292.5 && baseAzimuth < 337.5) bearingText = "NW";
//                else bearingText = "?";

//                fieldBearing.Text = bearingText;

//            }

//            static double Mod(double a, double b)
//            {
//                return a - b * Math.Floor(a / b);
//            }

//            double calculateTheta(float DirectionA, float DirectionB)
//            {
//                // first bring the coordinates into imagining them on a sphere by mapping them with pi.

//                double fromRadians = DirectionA * Math.PI / 180;
//                double toRadians = DirectionB * Math.PI / 180;

//                // then calculate the angle theta between where the phone is pointing and the coordinates on the sphere
//                double theta = Math.Atan2(
//                    Math.Cos(toRadians), (Math.Cos(fromRadians) * Math.Sin(toRadians))
//                                       - (Math.Sin(fromRadians) * Math.Cos(toRadians)
//                    ));
//                theta = Mod(theta, 2 * Math.PI);
//                return theta * 180 / Math.PI;
//            }
//        }

//        private void rotateBmp(Bitmap arrow, double rotate)
//        {
//            int widthArrow = arrow.Width;
//            int heightArrow = arrow.Height;

//            Matrix matrix = new Matrix();
//            var rotateFloat = float.Parse((rotate % 360).ToString());

//            // actually rotate the image
//            matrix.PostRotate(rotateFloat, widthArrow, heightArrow);

//            // recreate the new Bitmap transformed by the matrix.
//            modifiedArrow = Bitmap.CreateBitmap(arrow, 0, 0, widthArrow, heightArrow, matrix, true);
//        }

//        private void rotateImageView(ImageView _arrow, int drawable, double rotate)
//        {

//            // decode the drawable into a bitmap
//            Bitmap bmpArrow = BitmapFactory.DecodeResource(_arrow.Resources,
//                    drawable);

//            // get the width/height of the drawable
//            int widthArrow = bmpArrow.Width;
//            int heightArrow = bmpArrow.Height;

//            // initialize a new Matrix
//            Matrix matrix = new Matrix();

//            // decide on how much to rotate
//            var rotateFloat = float.Parse((rotate % 360).ToString());

//            // actually rotate the image
//            matrix.PostRotate(rotateFloat, widthArrow, heightArrow);

//            // recreate the new Bitmap via a couple conditions
//            Bitmap rotatedBitmap = Bitmap.CreateBitmap(bmpArrow, 0, 0, widthArrow, heightArrow, matrix, true);

//            _arrow.SetImageBitmap(rotatedBitmap);
//            _arrow.SetImageDrawable(new BitmapDrawable(_arrow.Resources, rotatedBitmap));
//            _arrow.SetScaleType(ScaleType.Center);
//        }
//    }
//}

