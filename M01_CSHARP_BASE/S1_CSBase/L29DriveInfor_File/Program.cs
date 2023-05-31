namespace L29DriveInfor_File
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //GetDriveInformation.GetDriveInfo();
            //TestWriteAllText.WriteAllText();
            TestDirectory.ListDirectoryFromMyDocs();
        }
    }

    public class TestDirectory
    {
        public static void ListDirectoryFromMyDocs()
        {
            string directoryMyDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] files = Directory.GetFiles(directoryMyDocs);
            string[] directories = Directory.GetDirectories(directoryMyDocs);

            Console.WriteLine("Files: ");
            foreach(string file in files)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("\nDirectories: ");
            foreach(string directory in directories)
            {
                Console.WriteLine(directory);
            }
        }
    }
    public class TestWriteAllText
    {
        public static void WriteAllText()
        {
            string fileName = "TestCSFile.txt";
            string fileContent = "Xin chào, đây là xuanphuc.space!    " + DateTime.Now.ToString();

            var directoryMyDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullPath = Path.Combine(directoryMyDoc, fileName);

            if(File.Exists(fullPath))
            {
                //File.AppendAllText(fullPath, fileContent);
                string allTextFromFile = File.ReadAllText(fullPath);
                Console.WriteLine($"File content: {allTextFromFile}");
            } else
            {
                File.WriteAllText(fullPath, fileContent);

            }


            Console.WriteLine($"File directory: {directoryMyDoc}{Path.DirectorySeparatorChar}{fileName}");
        }
    }

    public class GetDriveInformation
    {
        public static void GetDriveInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach(DriveInfo driveInfo in allDrives)
            {
                Console.WriteLine("Drive name:           {0, 25}", driveInfo.Name);
                Console.WriteLine("Drive type:           {0, 25}", driveInfo.DriveType);
                if(driveInfo.IsReady)
                {
                    Console.WriteLine("Volume lable:         {0, 25}", driveInfo.VolumeLabel);
                    Console.WriteLine("Drive fomat:          {0, 25}", driveInfo.DriveFormat);
                    Console.WriteLine("Available free space: {0, 25} bytes", driveInfo.AvailableFreeSpace);
                    Console.WriteLine("Total free space:     {0, 25} bytes", driveInfo.TotalFreeSpace);
                    Console.WriteLine("Total size:           {0, 25} bytes", driveInfo.TotalSize);
                }
                Console.WriteLine("--------------------------\n");
            }
        }
    }
}