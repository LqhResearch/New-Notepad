using System.IO;

namespace Notepad_Library
{
    public class FileNotepad
    {
        public string FileName { get; set; } //Include name and extension
        public bool IsFileSaved { get; set; }
        public string FileLocation { get; set; } //Use to store the file location

        public void InitializeNewFile()
        {
            FileName = "Untitled.txt";
            IsFileSaved = true;
        }

        private void UpdateFileStatus()
        {
            string fileName = FileLocation.Substring(FileLocation.LastIndexOf("\\") + 1);
            this.FileName = fileName;
            this.IsFileSaved = true;
        }

        public string OpenFile(string fileLocation)
        {
            string content;
            this.FileLocation = fileLocation;
            Stream stream = File.Open(fileLocation, FileMode.Open, FileAccess.ReadWrite);
            using (StreamReader sr = new StreamReader(stream))
                content = sr.ReadToEnd();
            UpdateFileStatus();
            return content;
        }

        public void SaveFile(string fileLocation, string[] lines)
        {
            this.FileLocation = fileLocation;
            Stream stream = File.Open(fileLocation, FileMode.OpenOrCreate, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(stream))
            {
                foreach (string line in lines)
                    sw.WriteLine(line);
            }
            UpdateFileStatus();
        }
    }
}
