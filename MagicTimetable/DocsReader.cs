using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace MagicTimetable
{
    class DocsReader
    {
        public List<Artist> CreateIdList()
        {
            var artistsList = new List<Artist>();
            const string url = "https://docs.google.com/spreadsheets/d/1cNISQ0VuUx-9yTPsPnfM35px0LrPm1zrP4tgZxkZ2Bs/pub?output=tsv";
            var fileList = GetCsv(url);

            var rowResult = fileList.Split('\r');

            if (rowResult.Count() < 2)
            {
                return artistsList;
            }

            foreach (var item in rowResult)
            {
                if (rowResult.ElementAt(0) == item)
                {
                    continue;
                }
                var artistData = item.Replace("\n", "").Split('\t');
                artistsList.Add(new Artist
                {
                    Name = artistData[0],
                    SetStartTime = Convert.ToDateTime(artistData[1]),
                    SetEndTime = Convert.ToDateTime(artistData[2]),
                    Stage = artistData[3],
                    Day = Convert.ToInt32(artistData[4]),
                    EventName = artistData[5]
                });
            }
            return artistsList;
        }
        public string GetCsv(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            var resp = (HttpWebResponse)req.GetResponse();

            var sr = new StreamReader(resp.GetResponseStream());
            var results = sr.ReadToEnd();
            sr.Close();

            return results;
        }

        public List<Artist> CreateLineupIdList()
        {
            var artistsList = new List<Artist>();
            const string url = "https://docs.google.com/spreadsheets/d/1cNISQ0VuUx-9yTPsPnfM35px0LrPm1zrP4tgZxkZ2Bs/pub?gid=140555639&single=true&output=tsv";
            var fileList = GetCsv(url);

            var rowResult = fileList.Split('\r');

            foreach (var item in rowResult)
            {
                var artistData = item.Replace("\n", "").Split('\t');
                artistsList.Add(new Artist
                {
                    Name = artistData[0],
                    Stage = artistData[1],
                    EventName = artistData[2]
                });
            }
            return artistsList;
        }
    }
}