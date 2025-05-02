using System.Diagnostics;
using System.Globalization;
using System.Reflection;
//using System.Windows.Forms;

namespace Inspector.Lib
{
    public static class Helpers
    {
       
        


        public static double ParseDouble(this string str)
        {
            return double.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
        }
        public static float ParseFloat(this string str)
        {
            return float.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
        }
        public static int ParseInt(this string str)
        {
            return int.Parse(str);
        }
        public static string ToDoubleInvariantString(this float str)
        {
            return str.ToString().Replace(",", ".");
        }

        

        public static string ReadResource(string name)
        {
            // Determine path
            var assembly = Assembly.GetExecutingAssembly();

            // Format: "{Namespace}.{Folder}.{filename}.{Extension}"

            var resourcePath = assembly.GetManifestResourceNames()
                 .Single(str => str.Contains(name));


            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
     

     

        public static double NextGaussian(this Random rand, double mean = 0, double stdDev = 1)
        {
            //Random rand = new Random(); //reuse this if you are generating many
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;
        }

        public static long[] VectorsTest(int k = 1000)
        {

            float[] data = new float[k];
            float[] data2 = new float[k];

            Random r = new Random();
            for (int i = 0; i < k; i++)
            {
                data[i] = ((float)r.NextDouble());
                data2[i] = ((float)r.NextDouble());
            }

            float[] res = new float[data.Length];
            float[] res2 = new float[data.Length];
            var sw1 = Stopwatch.StartNew();
            for (int i = 0; i < data.Length; i++)
            {
                res2[i] = data[i] * data2[i];
            }

            sw1.Stop();
            var e1 = sw1.ElapsedMilliseconds;

            var sw2 = Stopwatch.StartNew();
            for (int i = 0; i < data.Length; i += 4)
            {
                /*Vector4 v1 = new Vector4((float)data[i], (float)data[i + 1], (float)data[i + 2], data[i + 3]);
                Vector4 v2 = new Vector4((float)data2[i], (float)data2[i + 1], (float)data2[i + 2], data2[i + 3]);

                var rr = Vector4.Multiply(v1, v2);

                res[i] = rr.X;
                res[i + 1] = rr.Y;
                res[i + 2] = rr.Z;
                res[i + 3] = rr.W;*/
            }

            sw2.Stop();
            var e2 = sw2.ElapsedMilliseconds;

            for (int i = 0; i < res2.Length; i++)
            {
                if (res2[i] != res[i])
                {
                    throw new ArgumentException();
                }
            }
            return new[] { e1, e2 };
        }
    }
}
