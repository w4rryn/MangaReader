using System;
using System.Collections.Generic;
using System.Text;

namespace SourcePluginSDK
{
    public interface ISourceFactory
    {
        PluginSource CreateSource();
    }
}