using System;
using IllusionPlugin;

namespace ButtSaber
{
    // Most BeatSaber plugins derive from the IPA IPlugin interface.
    public class Plugin : IPlugin
    {

        public string Name => "ButtSaber";
        public string Version => "0.0.1";

        // When the application starts up, we'll want to hook the haptics functions.
        public void OnApplicationStart()
        {
            throw new NotImplementedException();
        }

        public void OnApplicationQuit()
        {
            throw new NotImplementedException();
        }

        public void OnLevelWasLoaded(int level)
        {
            throw new NotImplementedException();
        }

        public void OnLevelWasInitialized(int level)
        {
            throw new NotImplementedException();
        }
     
        public void OnUpdate()
        {
            throw new NotImplementedException();
        }

        public void OnFixedUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
