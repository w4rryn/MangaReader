using MyReader.Common;
using SourcePluginSDK;
using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MyReader.Core
{
    [Serializable]
    public class PluginLoader
    {
        public PluginLoader()
        {
            Plugins = new List<PluginSource>();
        }

        public List<PluginSource> Plugins { get; }

        private static Assembly GetAssemblyFromFile(string file)
        {
            var rawData = File.ReadAllBytes(file);
            var assembly = Assembly.Load(rawData);
            if (assembly != null)
            {
                return assembly;
            }
            throw new ArgumentException("Assembly could not be loaded");
        }

        private static string[] GetPluginFilesFromDirectory()
        {
            var path = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(Path.Join(path, App.PLUGIN_DIR), "*.dll");
            return files;
        }

        private static Type GetTypeFromAssemblyFile(string file)
        {
            Assembly assembly = GetAssemblyFromFile(file);
            TypeInfo info = assembly.DefinedTypes.Single(x => x.Name == "Main");
            Type type = assembly.GetType(info.FullName);
            return type;
        }

        private void AddPluginInstanceToLoadedPlugins(Type type)
        {
            var plug = (PluginSource)Activator.CreateInstance(type);
            Plugins.Add(plug);
        }

        public void LoadPlugins()
        {
            var assemblyFiles = GetPluginFilesFromDirectory();
            foreach (var file in assemblyFiles)
            {
                Type type = GetTypeFromAssemblyFile(file);
                if (type != null)
                {
                    AddPluginInstanceToLoadedPlugins(type);
                }
            }
        }

        public async Task SearchAllSourcesAsync(string rawText, Action<Manga> searchCallback)
        {
            var tasks = new List<Task>();
            foreach (PluginSource plug in Plugins)
            {
                tasks.Add(Task.Run(() => plug.SearchManga(rawText, searchCallback)));
            }
            await Task.WhenAll(tasks);
        }

        public async Task LoadPanelsFromChapter(Manga manga, Chapter chapter, Action<Panel> panelLoadedCallback)
        {
            await Task.Run(() => manga.Source.GetPanels(manga, chapter, panelLoadedCallback));
        }
    }
}