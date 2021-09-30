using System;
using System.Collections.Generic;
using System.Text;

namespace SourcePluginSDK.Domain
{
    public abstract class Chapter
    {
        protected Chapter(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}