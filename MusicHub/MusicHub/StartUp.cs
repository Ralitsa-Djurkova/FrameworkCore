namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context = 
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
            Console.WriteLine("Db Created!!!");
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder(); 
            var albumsInfo = context.Albums
                 .Where(a => a.ProducerId == producerId)
                 .Select(a => new
                 {
                     AlbumsName = a.Name,
                     ReleasData = a.ReleaseDate.ToString("MM/dd/yyyy"),
                     ProduserName = a.Name,
                     Song = a.Songs
                     .Select(a => new
                     {
                         SongName = a.Name,
                         Price = a.Price.ToString("f2"),
                         Writer = a.Writer.Name
                     })
                     .OrderByDescending(a => a.SongName)
                     .ThenBy(a => a.Writer)
                     .ToArray(),
                     TotalAlbumPrice = a.Price.ToString("f2")

                 })
                 .ToArray();

            foreach(var album in albumsInfo)
            {

            }
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            throw new NotImplementedException();
        }
    }
}
