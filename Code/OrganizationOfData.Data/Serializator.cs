namespace OrganizationOfData.Data
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class Serializizator
    {
        public static void Serialize(object obj, string filename)
        {
            Stream ms = File.OpenWrite(filename);

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);

            ms.Flush();
            ms.Close();
            ms.Dispose();
        }

        public static object Deserialize(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fs = File.Open(filename, FileMode.Open);
            object obj = formatter.Deserialize(fs);

            fs.Flush();
            fs.Close();
            fs.Dispose();

            return obj;
        }
    }
}
