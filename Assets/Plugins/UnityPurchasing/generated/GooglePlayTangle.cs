#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("oUujIVemIYDinej3OQU1OG8wGVMFVJQ4WQs920if8YfIW2FL4G4eGK9Lbr347EcUpYnOYVk32s6hOj2HB71g9YRn2ARt3EYzHnghfNmWVZUlHifzUpGoV1naj7XXSj8kYvHait1v7M/d4Ovkx2ulaxrg7Ozs6O3uFjn9Qg6mY9lO8Lsl5XnVCXq9/PrOWvfAXcS+WwQ9E23PD9cmM9OwaDu4MR1TRrwhGZlllbBP2mZr7QiUb+zi7d1v7Ofvb+zs7WQFemjUvf8nUb05zAvdsoBJbrGDYj8Go+uNPVsWiMQeYnegUoOR2RpAnOKlTG1z31QuHpxToEhdMZ14XjWXj8s4MHV/bCH1AQnVxOXVSPrXK+MuTCUEOdcek4UIPd+y0O/u7O3s");
        private static int[] order = new int[] { 8,6,4,13,4,8,9,11,13,9,10,11,13,13,14 };
        private static int key = 237;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
