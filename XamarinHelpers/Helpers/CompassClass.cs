using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Hardware;
using Android.Util;
using Android.Widget;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Widget.ImageView;

namespace CompassClass
{
    // the compass class uses the android internal compass and a destination object.
    // it'll rotate the image for the compass to point to the destination.
    // in another project, I've added the Mono.Android dependency which is what's missing in this project. And why all the errors.

    public class LocationObject
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
    }

    public class DestinationObject : LocationObject
    { }

    public class CompassClass
    {
        public ImageView arrow;
        public Label fieldBearing;

        public event EventHandler<SensorChangedEventArgs> SensorEventHandler;

        public CompassClass(DestinationObject destinationObject)
        {
            // how fast to update the UI
            SensorSpeed speed = SensorSpeed.UI;

            try
            {
                // start it if it's not monitoring the compass, raise an event when the compass reading changes
                if (!Compass.IsMonitoring) Compass.Start(speed);
                Compass.ReadingChanged += onSensorChanged;
            }

            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine(fnsEx.Message + "feature not supported on device");
            }

            void onSensorChanged(object sender, CompassChangedEventArgs compassEvent)
            {

                var reading = compassEvent.Reading;
                if (reading == null) return;

                // the way the phone is pointing is the sender
                LocationObject fromDirectionBearing = sender as LocationObject;
                if (fromDirectionBearing == null) return;

                float baseAzimuth = 0.00F;

                GeomagneticField geoField = new GeomagneticField(
                   fromDirectionBearing.Latitude,
                    fromDirectionBearing.Longitude,
                    fromDirectionBearing.Altitude,
                    DateTime.Now.Millisecond);

                baseAzimuth -= geoField.Declination; // converts magnetic north into true north

                // Store the bearingTo in the bearTo variable
                double bearingToLatittude = calculateTheta(fromDirectionBearing.Latitude, destinationObject.Latitude);
                double bearingToLongitude = calculateTheta(fromDirectionBearing.Longitude, destinationObject.Longitude);
                double bearingToAltitude = calculateTheta(fromDirectionBearing.Altitude, destinationObject.Altitude);

                // if the bearing is smaller than 0, add 360 to correct the orientation (such as making it spin clockwise).
                if (bearingToLatittude < 0)
                { bearingToLatittude = bearingToLatittude + 360; }

                if (bearingToLongitude < 0)
                { bearingToLongitude = bearingToLongitude + 360; }

                if (bearingToAltitude < 0)
                { bearingToLatittude = bearingToAltitude + 360; }

                // where we choose to point it
                double directionLatittude = bearingToLatittude - baseAzimuth;
                double directionLongitude = bearingToLongitude - baseAzimuth;
                double directionAltitude = bearingToAltitude - baseAzimuth;

                // If the direction is smaller than 0, add 360 to get the rotation clockwise.
                if (directionLatittude < 0)
                { directionLatittude = directionLatittude + 360; }

                if (directionLongitude < 0)
                { directionLongitude = directionLongitude + 360; }

                if (directionAltitude < 0)
                { directionLatittude = directionAltitude + 360; }

                rotateImageView(arrow, 1, directionLatittude);
                rotateImageView(arrow, 1, directionLongitude);
                rotateImageView(arrow, 1, directionAltitude);

                // set the field indication text
                string bearingText = "N";

                if ((360 >= baseAzimuth && baseAzimuth >= 337.5) || (0 <= baseAzimuth && baseAzimuth <= 22.5)) bearingText = "N";
                else if (baseAzimuth > 22.5 && baseAzimuth < 67.5) bearingText = "NE";
                else if (baseAzimuth >= 67.5 && baseAzimuth <= 112.5) bearingText = "E";
                else if (baseAzimuth > 112.5 && baseAzimuth < 157.5) bearingText = "SE";
                else if (baseAzimuth >= 157.5 && baseAzimuth <= 202.5) bearingText = "S";
                else if (baseAzimuth > 202.5 && baseAzimuth < 247.5) bearingText = "SW";
                else if (baseAzimuth >= 247.5 && baseAzimuth <= 292.5) bearingText = "W";
                else if (baseAzimuth > 292.5 && baseAzimuth < 337.5) bearingText = "NW";
                else bearingText = "?";

                fieldBearing.Text = bearingText;

            }

            static double Mod(double a, double b)
            {
                return a - b * Math.Floor(a / b);
            }

            double calculateTheta(float DirectionA, float DirectionB)
            {
                // first bring the coordinates into imagining them on a sphere by mapping them with pi.

                double fromRadians = DirectionA * Math.PI / 180;
                double toRadians = DirectionB * Math.PI / 180;


                // then calculate the angle theta between where the phone is pointing and the coordinates on the sphere
                double theta = Math.Atan2(
                    Math.Cos(toRadians), (Math.Cos(fromRadians) * Math.Sin(toRadians))
                    - (Math.Sin(fromRadians) * Math.Cos(toRadians)
                    ));
                theta = Mod(theta, 2 * Math.PI);
                return theta * 180 / Math.PI;
            }
        }

        private void rotateImageView(ImageView arrow, int drawable, double rotate)
        {

            // Decode the drawable into a bitmap
            Bitmap bmpArrow = BitmapFactory.DecodeResource(arrow.Resources,
                    drawable);

            // Get the width/height of the drawable
            DisplayMetrics dm = new DisplayMetrics();
            int widthArrow = bmpArrow.Width;
            int heightArrow = bmpArrow.Height;

            // Initialize a new Matrix
            Matrix matrix = new Matrix();

            // Decide on how much to rotate
            var rotateFloat = float.Parse((rotate % 360).ToString());

            // Actually rotate the image
            matrix.PostRotate(rotateFloat, widthArrow, heightArrow);

            // recreate the new Bitmap via a couple conditions
            Bitmap rotatedBitmap = Bitmap.CreateBitmap(bmpArrow, 0, 0, widthArrow, heightArrow, matrix, true);
            //BitmapDrawable bmd = new BitmapDrawable( rotatedBitmap );

            //imageView.setImageBitmap( rotatedBitmap );
            arrow.SetImageDrawable(new BitmapDrawable(arrow.Resources, rotatedBitmap));
            arrow.SetScaleType(ScaleType.Center);
        }
    }
}
