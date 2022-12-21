using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC {
	public static class ParseExtensions {
		public static bool IsNull(this object input) {
			return Object.ReferenceEquals(input, null);
		}

		public static bool jIsEmpty(this string input) {
			return string.IsNullOrEmpty(input);
		}

		public static bool IsMin(this DateTime input) {
			return input.ToDate() == DateTime.MinValue;
		}

		public static bool IsNullOrMin(this DateTime? input) {
			return Object.ReferenceEquals(input, null) || input.ToDate() == DateTime.MinValue;
		}

		public static bool IsNullOrZero(this long? input) {
			return Object.ReferenceEquals(input, null) || input.ToLong() == 0;
		}

		public static bool ToBool(this object input) {
			bool output = false;

			if (!input.IsNull()) {
				if (input.ToString() == "true,false" || input.ToString() == "X" || input.ToString() == "Y" || input.ToString() == "1") {
					return true;
				}

				bool.TryParse(input.ToString(), out output);
			}

			return output;
		}

		public static string TrimNull(this string input) {
			return (input ?? string.Empty).Trim();
		}

		public static float ToFloat(this object input) {
			float output = 0;

			if (!input.IsNull()) {
				float.TryParse(input.ToString(), out output);
			}

			return output;
		}

		public static double ToDouble(this object input) {
			double output = 0;

			if (!input.IsNull()) {
				double.TryParse(input.ToString(), out output);
			}

			return output;
		}

		public static decimal ToDecimal(this object input) {
			decimal output = 0;

			if (!input.IsNull()) {
				decimal.TryParse(input.ToString(), out output);
			}

			return output;
		}

		public static long ToLong(this object input) {
			long output = 0;

			if (!input.IsNull()) {
				long.TryParse(input.ToString(), out output);
			}

			return output;
		}
		
		public static string NullToEmptyString(this long? input) {
			return input.IsNull() ? string.Empty : input.ToLong().ToString();
		}

		public static int ToInt(this object input) {
			int output = 0;

			if (!input.IsNull()) {
				if (input is decimal || input is Decimal || input is decimal? || input is Decimal?) {
					return (int)Math.Round((decimal)input, 0, MidpointRounding.AwayFromZero);
				} else if (input is double || input is Double || input is double? || input is Double?) {
					return (int)Math.Round((double)input, 0, MidpointRounding.AwayFromZero);
				}

				int.TryParse(input.ToString(), out output);
			}

			return output;
		}

		public static DateTime ToDate(this object input) {
			DateTime output = DateTime.MinValue;

			if (!input.IsNull()) {
				DateTime.TryParse(input.ToString(), out output);
			}

			return output;
		}

		public static DateTime? ToNullableDate(this object input) {
			DateTime output = DateTime.MinValue;

			if (input.IsNull() || input.ToString().jIsEmpty() || input.ToDate() == DateTime.MinValue) {
				return null;
			}

			DateTime.TryParse(input.ToString(), out output);

			return output;
		}

		public static DateTime MaxOrToday(this IEnumerable<DateTime> source) {
			if (source.Count() == 0)
				return DateTime.Now;
			else
				return source.Max();
		}

		public static bool? ToNullableBool(this object input) {
			bool output = false;

			if (input.IsNull() || input.ToString().jIsEmpty()) {
				return null;
			}

			bool.TryParse(input.ToString(), out output);

			return output;
		}

		public static decimal? ToNullableDecimal(this object input) {
			decimal output = 0;

			if (input.IsNull() || input.ToString().jIsEmpty()) {
				return null;
			}

			decimal.TryParse(input.ToString(), out output);

			return output;
		}

		public static long? ToNullableLong(this object input) {
			long output = 0;

			if (input.IsNull() || input.ToString().jIsEmpty()) {
				return null;
			}

			long.TryParse(input.ToString(), out output);

			return output;
		}

		public static int? ToNullableInt(this object input) {
			int output = 0;

			if (input.IsNull() || input.ToString().jIsEmpty()) {
				return null;
			}

			int.TryParse(input.ToString(), out output);

			return output;
		}

    public static string ToSAP(this DateTime input) {
      if (input == DateTime.MinValue) {
        return string.Empty;
      }

      return input.ToString("yyyy-MM-dd");
    }

    public static DateTime FromSAP(this string input) {
      return input.IsNull() ? DateTime.MinValue : input.ToDate();
    }

		public static string MinValueToEmptyString(this DateTime input, string dateFormat) {
			if (input == DateTime.MinValue) {
				return string.Empty;
			}

			if (dateFormat.ToLower().Contains("h:mm") && input.ToString("HH:mm") == "00:00") {
				dateFormat = dateFormat.Replace("HH:mm", string.Empty);
				dateFormat = dateFormat.Replace("H:mm", string.Empty);
				dateFormat = dateFormat.Replace("hh:mm", string.Empty);
				dateFormat = dateFormat.Replace("h:mm", string.Empty);
				return input.ToString(dateFormat);
			}

			return input.ToString(dateFormat);
		}

		public static string MinValueToEmptyString(this DateTime? input, string dateFormat) {
			if (input.IsNull() || input.ToDate() == DateTime.MinValue) {
				return string.Empty;
			}

			if (dateFormat.ToLower().Contains("h:mm") && input.ToDate().ToString("HH:mm") == "00:00") {
				dateFormat = dateFormat.Replace("HH:mm", string.Empty);
				dateFormat = dateFormat.Replace("H:mm", string.Empty);
				dateFormat = dateFormat.Replace("hh:mm", string.Empty);
				dateFormat = dateFormat.Replace("h:mm", string.Empty);
				return input.ToDate().ToString(dateFormat);
			}

			return input.ToDate().ToString(dateFormat);
		}

		public static string ZeroToEmptyString(this int input) {
			return input <= 0 ? string.Empty : input.ToString();
		}

		public static string ZeroToEmptyString(this int input, string stringFormat) {
			return input <= 0 ? string.Empty : input.ToString(stringFormat);
		}

		public static string ZeroToEmptyString(this long input) {
			return input <= 0 ? string.Empty : input.ToString();
		}

		public static string ZeroToEmptyString(this long input, string stringFormat) {
			return input <= 0 ? string.Empty : input.ToString(stringFormat);
		}

		public static string ZeroToEmptyString(this decimal input) {
			return input <= 0 ? string.Empty : input.ToString();
		}

		public static string ZeroToEmptyString(this decimal input, string stringFormat) {
			return input <= 0 ? string.Empty : input.ToString(stringFormat);
		}

		public static string ZeroToEmptyString(this decimal? input, string stringFormat) {
			return (input.IsNull() || input <= 0) ? string.Empty : input.ToDecimal().ToString(stringFormat);
		}
	}
}
