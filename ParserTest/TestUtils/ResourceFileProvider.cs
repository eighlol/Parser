using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;

namespace ParserTest.TestUtils
{
    public class ResourceFileProvider
    {
        private static readonly string AppData = "AppData\\";
        private static readonly string Resources = "Resources\\";

        public static T GetParsedObjectFromResourceFile<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);

            var serializer = new XmlSerializer(typeof(T));
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var result = (T)serializer.Deserialize(fileStream);
                return result;
            }
        }

        public static string GetFilePath(string fileName)
        {
            return Path.Combine(GetProjectRoot(), AppData, Resources, fileName);
        }

        public static string GetProjectRoot()
        {
            string projectName = TestConstants.ProjectName;
            var dirInfo = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
           
            while (!dirInfo.Name.Equals(projectName))
            {
                dirInfo = dirInfo.Parent;
            }

            return dirInfo.FullName;
        }
    }
}
