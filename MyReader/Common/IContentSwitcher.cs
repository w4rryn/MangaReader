using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MyReader.Common
{
    internal interface IContentSwitcher
    {
        void SwitchContent(Page p);
    }
}