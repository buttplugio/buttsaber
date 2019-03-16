using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;

namespace ButtSaber
{
    class ButtSaberHooks
    {
        // Oddly enough, this came from Intro-Skip. Not sure where it lives in DarthMaul? It
        // basically just does what we'd expect HapticFeedbackController to do in the VR situation,
        // which we can deal with here since BeatSaber doesn't have Gamepad support anyways.
        public static IEnumerator OneShotRumbleCoroutine(XRNode node, float duration, float impulseStrength, float intervalTime = 0f)
        {
            VRPlatformHelper vr = VRPlatformHelper.instance;
            YieldInstruction waitForIntervalTime = new WaitForSeconds(intervalTime);
            float time = Time.time + 0.1f;
            while (Time.time < time)
            {
                vr.TriggerHapticPulse(node, impulseStrength);
                yield return intervalTime > 0 ? waitForIntervalTime : null;
            }
        }

        public static T GetPrivateField<T>(object obj, string fieldName)
        {
            var prop = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var value = prop.GetValue(obj);
            return (T)value;
        }

        public static void Rumble(HapticFeedbackController t, XRNode node, float duration, float impulseStrength, float intervalDuration)
        {
            // If rumble isn't on in the first place, don't forward anything.
            if (!GetPrivateField<MainSettingsModel>(t, "_mainSettingsModel").controllersRumbleEnabled)
            {
                return;
            }

            Console.WriteLine(impulseStrength);
            // We can't actually call the hooked function as PlayHooky doesn't have that capability
            // yet and will call the hook in a loop. So we simulate what it would do by passing on to VRController.
            SharedCoroutineStarter.instance.StartCoroutine(OneShotRumbleCoroutine(node, duration, impulseStrength, intervalDuration));
        }
    }
}
