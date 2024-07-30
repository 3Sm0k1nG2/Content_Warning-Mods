using System.IO;
using HarmonyLib;
using UnityEngine;
using Zorro.Core;

namespace Mods3012.CW.CustomSaveVideosLocationMod.Patches
{
    [HarmonyPatch(typeof(CameraRecording))]
    internal class CameraRecordingPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("SaveToDesktop")]
        internal static bool SaveToDesktop_Prefix(ref bool __result, out string videoFileName, ref VideoHandle ___videoHandle)
        {
            string path;
            bool isUnableToGetRecordingPath = !RecordingsHandler.TryGetRecordingPath(___videoHandle, out path);
            
            if (isUnableToGetRecordingPath)
            {
                videoFileName = string.Empty;
                
                __result = false;
                return false;
            }

            videoFileName = "content_warning_" + ___videoHandle.id.ToShortString() + ".webm";
            string destFileName = Path.Combine(Plugin.ConfigEntries.CustomSaveVideosLocation.Value, videoFileName);
            
            /* 
             * Not sure what the game-dev had in mind when writing this if-statement,
             * Leaving it as it is for now.
             * (FIND ME, FIX ME)
             */
            bool isFileNonExistant = !File.Exists(path);
            if (isFileNonExistant)
            {
                Debug.LogError("Video: " + path + " Does Not Exist!");
                
                __result = false;
                return false;
            }
                
            
            try
            {
                File.Copy(path, destFileName);
            }
            catch (IOException e)
            {
                Debug.LogWarning(e.Message);
                
                __result = false;
                return false;
            }

            __result = true;
            return false;
        }
    }
}
