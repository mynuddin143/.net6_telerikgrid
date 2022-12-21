using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using System.IO;

namespace JMC {
	public class Serializer {
		private static string GetAppPath() {
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.Replace(@"file:///", ""));
		}

		public static void SerializeList<T>(List<T> objects, string filename) {
			XmlSerializer xs = new XmlSerializer(typeof(List<T>));
			using (TextWriter tw = new StreamWriter(Path.Combine(GetAppPath(), filename))) {
				xs.Serialize(tw, objects);
				tw.Close();
			}
		}

		public static void SerializeList<T>(List<T> objects) {
			SerializeList(objects, typeof(T).Name + ".xml");
		}

		public static List<T> DeserializeList<T>(string filename) {
			try {
				string path = Path.Combine(GetAppPath(), filename);
				if (File.Exists(path)) {
					using (StreamReader sr = new StreamReader(path)) {
						// This call causes the Xml Serializer to be created but never destroyed.  
						// Therefore this method shouldn't be used in a situation where it is called repeatedly.
						XmlSerializer xs = new XmlSerializer(typeof(List<T>), null, new Type[] { typeof(T) }, null, null);
						return (List<T>)xs.Deserialize(sr);
					}
				}
				return new List<T>();
			} catch (Exception ex) {
				//Logging.Log(Logging.Level.Error, MethodBase.GetCurrentMethod(), ex.Message, ex);
				return null;
			}
		}

		public static List<T> DeserializeList<T>() {
			return DeserializeList<T>(typeof(T).Name + ".xml");
		}

		public static void Serialize<T>(object o, string filename) {
			XmlSerializer xs = new XmlSerializer(typeof(T));
			using (TextWriter tw = new StreamWriter(Path.Combine(GetAppPath(), filename))) {
				xs.Serialize(tw, o);
				tw.Close();
			}
		}

		public static void Serialize<T>(object o) {
			Serialize<T>(o, typeof(T).Name + ".xml");
		}

		public static T Deserialize<T>(string filename) {
			try {
				string path = Path.Combine(GetAppPath(), filename);
				if (File.Exists(path)) {
					using (StreamReader sr = new StreamReader(path)) {
						XmlSerializer xs = new XmlSerializer(typeof(T));
						return (T)xs.Deserialize(sr);
					}
				}
				return default(T);
			} catch (Exception ex) {
				//Logging.Log(Logging.Level.Error, MethodBase.GetCurrentMethod(), ex.Message, ex);
				return default(T);
			}
		}

		public static T Deserialize<T>() {
			return Deserialize<T>(typeof(T).Name + ".xml");
		}
	}
}
