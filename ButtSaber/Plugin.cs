using System;
using IllusionPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ButtSaber
{
    // Most BeatSaber plugins derive from the IPA IPlugin interface.
    public class Plugin : IPlugin
    {

        public string Name => "ButtSaber";
        public string Version => "0.0.1";

        // This comes from RumbleEnhancer. I think it's a list of all of the Game Scenes in
        // BeatSaber? Not sure if it'll be useful, since I don't mind making vibrations in Buttplug
        // happen during menus.
        private readonly string[] GameplaySceneNames = { "DefaultEnvironment", "BigMirrorEnvironment", "TriangleEnvironment", "NiceEnvironment" };

        private static ButtSaberBehavior _behavior;

        // When the application starts up, we'll want to hook the haptics functions.
        public void OnApplicationStart()
        {
            // Used in case we do want to avoid vibrating toys while in menus.
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            // Not sure why I need this but everyone seems to have it so ok.
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }


        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
        {
            
            
            try
            {
                Console.WriteLine("scene.name == " + scene.name);
                if (scene.name == "HealthWarning")
                {
                    SharedCoroutineStarter.instance.StartCoroutine(OnLoadingDidFinishGame());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            
        }

        public static System.Collections.IEnumerator OnLoadingDidFinishGame()
        {
            yield return new WaitForSeconds(0.1f);
            // Just hold onto this in a static so it's not GC'd.
            _behavior = new GameObject("ButtSaberBehavior").AddComponent<ButtSaberBehavior>();
        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) { }

        public void OnLevelWasLoaded(int level)
        {
        }

        public void OnLevelWasInitialized(int level)
        {
        }
     
        public void OnUpdate()
        {
        }

        public void OnFixedUpdate()
        {
        }
    }
}
