using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MyReader.Core
{
    [Serializable]
    internal class Bookmarker
    {
        private const string BOOKMARKS_PATH = "bookmarks.bin";
        private Dictionary<Manga, Chapter> bookmarks;

        public Bookmarker()
        {
            if (!DoesBookmarkFileExist())
            {
                SetUpBookmarkSystem();
            }
            else
            {
                DeSerializeBookmarksFromDisk();
            }
        }

        private void SetUpBookmarkSystem()
        {
            CreateBookmarksFileIfNotExists();
            bookmarks = new Dictionary<Manga, Chapter>();
        }

        private void DeSerializeBookmarksFromDisk()
        {
            var data = File.ReadAllBytes(BOOKMARKS_PATH);
            bookmarks = DeSerializeBinaryToBookmarks(data);
        }

        public void CreateBookmark(Manga manga, Chapter chapter)
        {
            if (bookmarks.ContainsKey(manga))
            {
                bookmarks[manga] = chapter;
            }
            else
            {
                bookmarks.Add(manga, chapter);
            }
            SaveBookmarksToDisk();
        }

        private void SaveBookmarksToDisk()
        {
            var data = SerializeBookmarksToBinary();
            CreateBookmarksFileIfNotExists();
            File.WriteAllBytes(BOOKMARKS_PATH, data);
        }

        private void CreateBookmarksFileIfNotExists()
        {
            if (!DoesBookmarkFileExist())
            {
                _ = File.Create(BOOKMARKS_PATH);
            }
        }

        private bool DoesBookmarkFileExist()
        {
            return File.Exists(BOOKMARKS_PATH);
        }

        private byte[] SerializeBookmarksToBinary()
        {
            using var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, bookmarks);
            return memoryStream.ToArray();
        }

        private Dictionary<Manga, Chapter> DeSerializeBinaryToBookmarks(byte[] data)
        {
            using var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            memoryStream.Write(data, 0, data.Length);
            _ = memoryStream.Seek(0, SeekOrigin.Begin);
            return (Dictionary<Manga, Chapter>)binaryFormatter.Deserialize(memoryStream);
        }
    }
}