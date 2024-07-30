using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mods3012.OnlineFix.OnlineFixPatchMod.Patchers
{
    internal static class OnlineFixPatcher
    {
        internal static void Patch()
        {
            if (Plugin.ConfigEntries.Active.Value == true)
            {
                PreloadOnlineFix();
            }
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        private static void PreloadOnlineFix()
        {
            bool hasFoundAssembly = false;
            bool hasFoundMethodInAssembly = false;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            int i = 0;
            while (i < assemblies.Length)
            {
                Type type = assemblies[i].GetType("OnlineFix.Main");
                if (type != null)
                {
                    Plugin.Logger.LogMessage("Found assembly by online-fix.me in memory, searching for method...");
                    hasFoundAssembly = true;
                    MethodInfo method = type.GetMethod("InitWithBeepInEx");
                    if (method != null)
                    {
                        Plugin.Logger.LogMessage("Found method in assembly, calling it...");
                        hasFoundMethodInAssembly = true;
                        method.Invoke(null, null);
                        break;
                    }
                    break;
                }
                else
                {
                    i++;
                }
            }
            if (!hasFoundAssembly)
            {
                MessageBox(IntPtr.Zero, "Could not find Custom.dll in the modules!", "Error", 16U);
                Environment.Exit(4919);
            }
            if (!hasFoundMethodInAssembly)
            {
                MessageBox(IntPtr.Zero, "Could not find needed method in Custom.dll!", "Error", 16U);
                Environment.Exit(4919);
            }
        }
    }

}
