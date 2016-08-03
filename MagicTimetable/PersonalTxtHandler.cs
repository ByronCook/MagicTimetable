using System.Collections.Generic;
using System.IO;
using Android.OS;

namespace MagicTimetable
{
    class PersonalTxtHandler
    {
        public string SdCardPath { get; set; }
        public string FilePath { get; set; }

        public PersonalTxtHandler()
        {
            SdCardPath = Environment.ExternalStorageDirectory.Path;
            FilePath = Path.Combine(SdCardPath, "TimeTableList.txt");
        }

        public void WriteTxtFile(List<string> artistNames)
        {
            File.Delete(FilePath);
            if (!File.Exists(FilePath))
            {
                using (var write = new StreamWriter(FilePath, true))
                {
                    foreach (var artistName in artistNames)
                    {
                        write.Write(artistName + System.Environment.NewLine);
                    }
                }
            }
            ReadTxtFile();
        }

        public List<string> ReadTxtFile()
        {
            var selectedArtistsList = new List<string>();
            if (File.Exists(FilePath))
            {
                var file = File.ReadAllLines(FilePath);
                for (var i = 0; i < file.Length; i++)
                {
                    selectedArtistsList.Add(file[i]);
                }
            }
            return selectedArtistsList;
        }
    }
}