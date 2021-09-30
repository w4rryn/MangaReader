using System;

namespace SourcePluginSDK
{
    [Serializable]
    public class PluginMetadata
    {
        public PluginMetadata(string author, string version, string description, string website)
        {
            Author = author;
            Version = version;
            Description = description;
            Website = website;
        }

        public string Author { get; }
        public string Version { get; }
        public string Description { get; }
        public string Website { get; }
    }
}