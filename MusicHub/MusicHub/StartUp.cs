namespace MusicHub
{
    using System;
    using System.Globalization;
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
            //Performer performer = new Performer();

            //Test your solutions here
            //string result = ExportAlbumsInfo(context, 9);
            //Console.WriteLine(result);

            string result = ExportSongsAboveDuration(context, 4);
            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumInfo = context
                .Albums
                .ToArray()
                .Where(x => x.ProducerId == producerId)
                .OrderByDescending(x => x.Price)
                .Select(x => new
                {
                    AlbumName = x.Name,
                    ReleaseDate = x.ReleaseDate.ToString("MM/dd/yyyy"),
                    ProducerName = x.Producer.Name,
                    Songs = x.Songs
                    .Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("f2"),
                        Writer = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.Writer)
                    .ToArray(),

                    TotalAlbumPrice = x.Price.ToString("f2")

                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            
            foreach (var album in albumInfo)
            {
               

                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine($"-Songs: ");

                int i = 1;
                foreach (var song in album.Songs)
                {
                    sb
                        .AppendLine($"---#{i++}")
                        .AppendLine($"---SongName: {song.SongName}")
                        .AppendLine($"---Price: {song.Price}")
                        .AppendLine($"---Writer: {song.Writer}");
                }

                sb.AppendLine($"-AlbumPrice: {album.TotalAlbumPrice}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb = new StringBuilder();

            var songs = context.Songs
                 .ToArray()
                 .Where(x => x.Duration.TotalSeconds > duration)
                 .Select(x => new
                 {
                     SongName = x.Name,
                     Witer = x.Writer,
                     Performer = x.SongPerformers
                     .ToArray()
                     .Select(sp => $"{sp.Performer.FirstName}" +
                     $" {sp.Performer.LastName}")
                     .FirstOrDefault(),
                     AlbumProducer = x.Album.Producer.Name,
                     Duration = x.Duration.ToString("c", CultureInfo.InvariantCulture)

                 })
                 .OrderBy(x => x.SongName)
                 .ThenBy(x => x.Witer)
                 .ThenBy(x => x.Performer)
                 .ToArray();
            int i = 1;
            foreach (var song in songs)
            {
                sb
                    .AppendLine($"#{i++}")
                    .AppendLine($"---SongName:{song.SongName}")
                    .AppendLine($"---Writer: {song.Witer.Name}")
                    .AppendLine($"---Performer: {song.Performer}")
                    .AppendLine($"---AlbumProducer: {song.AlbumProducer}")
                    .AppendLine($"---Duration: {song.Duration}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
