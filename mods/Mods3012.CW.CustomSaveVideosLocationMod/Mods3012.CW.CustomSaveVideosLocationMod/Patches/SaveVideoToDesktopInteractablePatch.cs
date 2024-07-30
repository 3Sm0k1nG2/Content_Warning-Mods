using System;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace Mods3012.CW.CustomSaveVideosLocationMod.Patches
{
    [HarmonyPatch(typeof(SaveVideoToDesktopInteractable))]
    internal class SaveVideoToDesktopInteractablePatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        private static void Start_Postfix(ref string ___hoverText)
        {
            ___hoverText = "Save Video to Custom Location";
            GameObject textGO;
            try
            {
                textGO = GameObject.Find("Tools/UploadMachine2/McScreen/Content/ShowVideoState/VIDEO/VideoDone/SaveVideo/Text (TMP)");
            }
            catch (Exception e)
            {
                Plugin.Logger.LogWarning("textGO is null");
                Plugin.Logger.LogError(e);
                return;
            }
            TextMeshProUGUI tmpugui = textGO.GetComponent<TextMeshProUGUI>();
            tmpugui.text = "Save Video To\nCustom Location".ToUpper();
        }

        [HarmonyPrefix]
        [HarmonyPatch("Interact")]
        private static bool Interact_Prefix(Player player, ref CameraRecording ___m_recording)
        {
            Debug.Log("Saving video to '" + Plugin.ConfigEntries.CustomSaveVideosLocation.Value + "'...");
            string VideoSavedLocalizedString = "Saved Video to Custom Location!";
            string VideoSavedAsLocalizedString = LocalizationKeys.GetLocalizedString(LocalizationKeys.Keys.VideoSavedAs);
            string VideoFailedSaveLocalizedString = LocalizationKeys.GetLocalizedString(LocalizationKeys.Keys.VideoFailedSave);
            string OkLocalizedString = LocalizationKeys.GetLocalizedString(LocalizationKeys.Keys.Ok);
            string str;

            bool isVideoSavedSuccesfully = ___m_recording.SaveToDesktop(out str);
            if (isVideoSavedSuccesfully)
            {
                Modal.Show(VideoSavedLocalizedString, VideoSavedAsLocalizedString + "  " + str, new ModalOption[]
                {
                    new ModalOption(OkLocalizedString, null)
                }, null);
            }
            else
            {
                Modal.Show(VideoFailedSaveLocalizedString, "", new ModalOption[]
                {
                    new ModalOption(OkLocalizedString, null)
                }, null);
            }

            return false;
        }
    }
}
