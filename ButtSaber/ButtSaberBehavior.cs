using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using Buttplug;
using Buttplug.Client;
using Buttplug.Client.Connectors;
using Buttplug.Server.Managers.UWPBluetoothManager;

namespace ButtSaber
{
    // IPA systems work by injecting Behaviours (oh you Europeans) into Unity. These are basically
    // just the same as Unity game scripts that get added to the scheduler.
    class ButtSaberBehavior : MonoBehaviour
    {
        private HookUtil _hooks;
        private ButtplugClient _client;
        private void Start()
        {
            try
            {
                Console.WriteLine("testing");
                _hooks = new HookUtil();
                _hooks.StartHooking<HapticFeedbackController, ButtSaberHooks>("HapticFeedbackControllerOverride",
                    "Rumble", "Rumble");

                //var connector = new ButtplugWebsocketConnector(new Uri("ws://localhost:12345"));
                var connector = new ButtplugEmbeddedConnector("Test Server");
                connector.Server.AddDeviceSubtypeManager((logMgr) => new UWPBluetoothManager(logMgr));
                //var connector = new ButtplugClientIPCConnector();
                _client = new ButtplugClient("ButtSaber", connector);
                Console.WriteLine("trying to connect");
                _client.ConnectAsync().Wait();
                _client.DeviceAdded += (obj, dev) =>
                {
                    Console.WriteLine(dev.Device.Name);
                };
                Console.WriteLine("connected?");
            }
            catch (Exception e)
            {
                Console.WriteLine("Shit fucked up.");
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                if (e.GetType() == typeof(AggregateException))
                {
                    Console.WriteLine(e.InnerException);
                    Console.WriteLine(e.InnerException.GetType());
                    Console.WriteLine(e.InnerException.Message);
                    Console.WriteLine(e.InnerException.StackTrace);
                }
                
            }
        }

        void OnDestroy()
        {
            _hooks.UnHookAll();
        }
    }
}
