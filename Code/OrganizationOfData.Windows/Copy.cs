namespace OrganizationOfData.Windows
{
    /// <summary>
    /// A static class containing function for deep copying an object
    /// </summary>
    public static class Copy
    {
        public static T DeepCopy<T>(T obj)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
