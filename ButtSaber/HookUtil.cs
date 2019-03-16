using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ButtSaber
{
    // Pretty much ripped straight out of https://github.com/PureDark/BSDarthMaul/blob/master/BSDarthMaul/DarthMaulBehavior.cs
    // Made slightly more generic because this might be handy elsewhere.
    public class HookUtil
    {
        private HookManager hookManager;
        private Dictionary<string, MethodInfo> hooks;

        public void StartHooking<U, W>(string hookName, string hookMethod, string overrideMethod)
        {
            this.hookManager = new HookManager();
            this.hooks = new Dictionary<string, MethodInfo>();
            this.Hook(hookName, typeof(U).GetMethod(hookMethod), typeof(W).GetMethod(overrideMethod));
        }

        public void UnHookAll()
        {
            foreach (string key in this.hooks.Keys)
                this.UnHook(key);
        }

        private bool Hook(string key, MethodInfo target, MethodInfo hook)
        {
            if (this.hooks.ContainsKey(key))
                return false;
            try
            {
                this.hooks.Add(key, target);
                this.hookManager.Hook(target, hook);
                Console.WriteLine($"{key} hooked!");
                return true;
            }
            catch (Win32Exception ex)
            {
                Console.WriteLine($"Unrecoverable Windows API error: {(object)ex}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to hook method, : {(object)ex}");
                return false;
            }
        }

        private bool UnHook(string key)
        {
            MethodInfo original;
            if (!this.hooks.TryGetValue(key, out original))
                return false;
            this.hookManager.Unhook(original);
            return true;
        }
    }
}
