using System;
using System.Globalization;

namespace GeoCoordinatePortable
{
    /// <summary>
    /// Represents a geographical location that is determined by latitude and longitude
    /// coordinates. May also include altitude, accuracy, speed, and course information.
    /// </summary>
    public class GeoCoordinate : IEquatable<GeoCoordinate>
    {
        private double TheCourse;
        private double TheHorizontalAccuracy;
        private double TheLatitude;
        private double TheLongitude;
        private double TheSpeed;
        private double TheVerticalAccuracy;
        public static readonly GeoCoordinate isUnknown = new GeoCoordinate();

        /// <summary>
        /// Initializes a new instance of GeoCoordinate that has no data fields set.
        /// </summary>
        public GeoCoordinate()
            : this(double.NaN, double.NaN)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the GeoCoordinate class from latitude and longitude data.
        /// </summary>
        /// <param name="_latitude">The latitude of the location. May range from -90.0 to 90.0. </param>
        /// <param name="_longitude">The longitude of the location. May range from -180.0 to 180.0.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Latitude or longitude is out of range.</exception>
        public GeoCoordinate(double _latitude, double _longitude)
            : this(_latitude, _longitude, double.NaN)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the GeoCoordinate class from latitude, longitude, and altitude data.
        /// </summary>
        /// <param name="_latitude">Latitude. May range from -90.0 to 90.0.</param>
        /// <param name="_longitude">Longitude. May range from -180.0 to 180.0</param>
        /// <param name="_altitude">The altitude in meters. May be negative, 0, positive, or Double.NaN, if unknown.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     latitude, longitude or altitude is out of range.
        /// </exception>
        public GeoCoordinate(double _latitude, double _longitude, double _altitude)
            : this(_latitude, _longitude, _altitude, double.NaN, double.NaN, double.NaN, double.NaN)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the GeoCoordinate class from latitude, longitude, altitude, horizontal accuracy,
        ///     vertical accuracy, speed, and course.
        /// </summary>
        /// <param name="_latitude">The latitude of the location. May range from -90.0 to 90.0.</param>
        /// <param name="_longitude">The longitude of the location. May range from -180.0 to 180.0.</param>
        /// <param name="_altitude">The altitude in meters. May be negative, 0, positive, or Double.NaN, if unknown.</param>
        /// <param name="_horizontalAccuracy">
        ///     The accuracy of the latitude and longitude coordinates, in meters. Must be greater
        ///     than or equal to 0. If a value of 0 is supplied to this constructor, the HorizontalAccuracy property will be set to
        ///     Double.NaN.
        /// </param>
        /// <param name="_verticalAccuracy">
        ///     The accuracy of the altitude, in meters. Must be greater than or equal to 0. If a value
        ///     of 0 is supplied to this constructor, the VerticalAccuracy property will be set to Double.NaN.
        /// </param>
        /// <param name="_speed">
        ///     The speed measured in meters per second. May be negative, 0, positive, or Double.NaN, if unknown.
        ///     A negative speed can indicate moving in reverse.
        /// </param>
        /// <param name="_course">
        ///     The direction of travel, rather than orientation. This parameter is measured in degrees relative
        ///     to true north. Must range from 0 to 360.0, or be Double.NaN.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     If latitude, longitude, horizontalAccuracy, verticalAccuracy, course is out of range.
        /// </exception>
        public GeoCoordinate(double _latitude, double _longitude, double _altitude, double _horizontalAccuracy,
            double _verticalAccuracy, double _speed, double _course)
        {
            Latitude = _latitude;
            Longitude = _longitude;
            Altitude = _altitude;
            HorizontalAccuracy = _horizontalAccuracy;
            VerticalAccuracy = _verticalAccuracy;
            Speed = _speed;
            Course = _course;
        }

        /// <summary>
        ///     Gets or sets the latitude of the GeoCoordinate.
        /// </summary>
        /// <returns>
        ///     Latitude of the location.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Latitude is set outside the valid range.</exception>
        public double Latitude
        {
            get { return TheLatitude; }
            set
            {
                if (value > 90.0 || value < -90.0)
                {
                    throw new ArgumentOutOfRangeException("Latitude", "Argument must be in range of -90 to 90");
                }
                TheLatitude = value;
            }
        }

        /// <summary>
        ///     Gets or sets the longitude of the GeoCoordinate.
        /// </summary>
        /// <returns>
        ///     The longitude.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Longitude is set outside the valid range.</exception>
        public double Longitude
        {
            get { return TheLongitude; }
            set
            {
                if (value > 180.0 || value < -180.0)
                {
                    throw new ArgumentOutOfRangeException("Longitude", "Argument must be in range of -180 to 180");
                }
                TheLongitude = value;
            }
        }

        /// <summary>
        ///     Gets or sets the accuracy of the latitude and longitude that is given by the GeoCoordinate, in meters.
        /// </summary>
        /// <returns>
        ///     The accuracy of the latitude and longitude, in meters.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">HorizontalAccuracy is set outside the valid range.</exception>
        public double HorizontalAccuracy
        {
            get { return TheHorizontalAccuracy; }
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException("HorizontalAccuracy", "Argument must be non negative");
                TheHorizontalAccuracy = value == 0.0 ? double.NaN : value;
            }
        }

        /// <summary>
        ///     Gets or sets the accuracy of the altitude given by the GeoCoordinate, in meters.
        /// </summary>
        /// <returns>
        ///     The accuracy of the altitude, in meters.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">VerticalAccuracy is set outside the valid range.</exception>
        public double VerticalAccuracy
        {
            get { return TheVerticalAccuracy; }
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException("VerticalAccuracy", "Argument must be non negative");
                TheVerticalAccuracy = value == 0.0 ? double.NaN : value;
            }
        }

        /// <summary>
        ///     Gets or sets the speed in meters per second.
        /// </summary>
        /// <returns>
        ///     The speed in meters per second. The speed must be greater than or equal to zero, or Double.NaN.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Speed is set outside the valid range.</exception>
        public double Speed
        {
            get { return TheSpeed; }
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException("speed", "Argument must be non negative");
                TheSpeed = value;
            }
        }

        /// <summary>
        ///     Gets or sets the heading in degrees, relative to true north.
        /// </summary>
        /// <returns>
        ///     The heading in degrees, relative to true north.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Course is set outside the valid range.</exception>
        public double Course
        {
            get { return TheCourse; }
            set
            {
                if (value < 0.0 || value > 360.0)
                    throw new ArgumentOutOfRangeException("course", "Argument must be in range 0 to 360");
                TheCourse = value;
            }
        }

        /// <summary>
        ///     Gets a value that indicates whether the GeoCoordinate does not contain latitude or longitude data.
        /// </summary>
        /// <returns>
        ///     true if the GeoCoordinate does not contain latitude or longitude data; otherwise, false.
        /// </returns>
        public bool IsUnknown => Equals(isUnknown);

        /// <summary>
        ///     Gets the altitude of the GeoCoordinate, in meters.
        /// </summary>
        /// <returns>
        ///     The altitude, in meters.
        /// </returns>
        public double Altitude { get; set; }

        /// <summary>
        ///     Determines if the GeoCoordinate object is equivalent to the parameter, based solely on latitude and longitude.
        /// </summary>
        /// <returns>
        ///     true if the GeoCoordinate objects are equal; otherwise, false.
        /// </returns>
        /// <param name="_other">The GeoCoordinate object to compare to the calling object.</param>
        public bool Equals(GeoCoordinate _other)
        {
            if (ReferenceEquals(_other, null))
                return false;

            var num = Latitude;

            if (!num.Equals(_other.Latitude))
                return false;

            num = Longitude;

            return num.Equals(_other.Longitude);
        }

        /// <summary>
        ///     Determines whether two GeoCoordinate objects refer to the same location.
        /// </summary>
        /// <returns>
        ///     true, if the GeoCoordinate objects are determined to be equivalent; otherwise, false.
        /// </returns>
        /// <param name="_left">The first GeoCoordinate to compare.</param>
        /// <param name="_right">The second GeoCoordinate to compare.</param>
        public static bool operator ==(GeoCoordinate _left, GeoCoordinate _right)
        {
            if (ReferenceEquals(_left, null))
                return ReferenceEquals(_right, null);

            return _left.Equals(_right);
        }

        /// <summary>
        ///     Determines whether two GeoCoordinate objects correspond to different locations.
        /// </summary>
        /// <returns>
        ///     true, if the GeoCoordinate objects are determined to be different; otherwise, false.
        /// </returns>
        /// <param name="_left">The first GeoCoordinate to compare.</param>
        /// <param name="_right">The second GeoCoordinate to compare.</param>
        public static bool operator !=(GeoCoordinate _left, GeoCoordinate _right)
        {
            return !(_left == _right);
        }

        /// <summary>
        ///     Returns the distance between the latitude and longitude coordinates that are specified by this GeoCoordinate and
        ///     another specified GeoCoordinate.
        /// </summary>
        /// <returns>
        ///     The distance between the two coordinates, in meters.
        /// </returns>
        /// <param name="_other">The GeoCoordinate for the location to calculate the distance to.</param>
        public double GetDistanceTo(GeoCoordinate _other)
        {
            if (double.IsNaN(Latitude) || double.IsNaN(Longitude) || double.IsNaN(_other.Latitude) ||
                double.IsNaN(_other.Longitude))
            {
                throw new ArgumentException("Argument latitude or longitude is not a number");
            }

            var d1 = Latitude * (Math.PI / 180.0);
            var num1 = Longitude * (Math.PI / 180.0);
            var d2 = _other.Latitude * (Math.PI / 180.0);
            var num2 = _other.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        /// <summary>
        ///     Serves as a hash function for the GeoCoordinate.
        /// </summary>
        /// <returns>
        ///     A hash code for the current GeoCoordinate.
        /// </returns>
        public override int GetHashCode()
        {
            return Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }

        /// <summary>
        ///     Determines if a specified GeoCoordinate is equal to the current GeoCoordinate, based solely on latitude and
        ///     longitude.
        /// </summary>
        /// <returns>
        ///     true, if the GeoCoordinate objects are equal; otherwise, false.
        /// </returns>
        /// <param name="_obj">The object to compare the GeoCoordinate to.</param>
        public override bool Equals(object _obj)
        {
            return Equals(_obj as GeoCoordinate);
        }

        /// <summary>
        ///     Returns a string that contains the latitude and longitude.
        /// </summary>
        /// <returns>
        ///     A string that contains the latitude and longitude, separated by a comma.
        /// </returns>
        public override string ToString()
        {
            if (this == isUnknown)
            {
                return "Unknown";
            }

            return
                $"{Latitude.ToString("G", CultureInfo.InvariantCulture)}, {Longitude.ToString("G", CultureInfo.InvariantCulture)}";
        }
    }
}
// This was downloaded from https://github.com/Nima-Jamalian/GeoCoordinatePortable