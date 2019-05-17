using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Reflection;
using Harmony;
using UnityEngine.XR;

namespace ButtSaber
{
    public class Main
    {
        private static StreamWriter _outFile;
        private static NamedPipeClientStream _stream;
        static void Load()
        {
            /*
            _outFile = new StreamWriter("C:\\Users\\qdot\\bstest.txt");
            _outFile.AutoFlush = true;
            */
            try
            {
                _stream = new NamedPipeClientStream("GVRPipe");
                _stream.Connect();
            }
            catch (Exception ex)
            {
                // _outFile.WriteLine(ex.ToString());
            }

            var harmony = HarmonyInstance.Create("com.nonpolynomial.buttsaber");

            // _outFile.WriteLine("Patching assemblies");
            try
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                // _outFile.WriteLine(ex.ToString());
            }

            // _outFile.WriteLine("Patched assemblies");
        }

        // Output a string of "[l|r],[number]\n" over IPC. No reason to deal with
        // something like pbufs for this.
        //
        // [number] for BeatSaber will be a float between 0-1. We'll use the Vive
        // interpretation of this, which is a multiplier against 4000 microseconds.
        // See https://github.com/ValveSoftware/openvr/wiki/IVRSystem::TriggerHapticPulse
        // for more info. This is weird.
        //
        // It might be worth trying to hook this at the OVR/Oculus API level at some
        // point to make this a more generic solution, but that will mean translating
        // Oculus haptic clips for games that haven't moved to the new API and I don't
        // wanna.
        [HarmonyPatch(typeof(VRPlatformHelper), "TriggerHapticPulse")]
        static class TriggerHapticPulse_Exfiltration_Patch
        {
            static void Postfix(XRNode node, float strength = 1f)
            {
                var hand = node == XRNode.LeftHand ? "l" : "r";
                var msg = $"{hand},{strength.ToString()}\n";
                var b = Encoding.ASCII.GetBytes(msg);
                _stream.Write(b, 0, b.Length);
            }
        }
        
    }
}
