using System;
using Modding;
using UnityEngine;

namespace HK_More_Charm_Notches
{
    public class HK_More_Charm_NotchesMod : Mod, ITogglableMod
    {
        private static HK_More_Charm_NotchesMod? _instance;

        internal static HK_More_Charm_NotchesMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(HK_More_Charm_NotchesMod)} was never constructed");
                }
                return _instance;
            }
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public HK_More_Charm_NotchesMod() : base("More Charm Notches")
        {
            _instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");
            ModHooks.GetPlayerIntHook += ModHooks_GetPlayerIntHook;
            ModHooks.SetPlayerIntHook += ModHooks_SetPlayerIntHook;
            Log("Initialized");
        }


        public void Unload()
        {
            Log("Unloading");
            ModHooks.GetPlayerIntHook -= ModHooks_GetPlayerIntHook;
            ModHooks.SetPlayerIntHook -= ModHooks_SetPlayerIntHook;
        }

        private int ModHooks_SetPlayerIntHook(string name, int orig)
        {
            if (name == "charmSlots")
            {
                Log($"charmSlots updated to {orig}; current is {PlayerData.instance.charmSlots} (modded to {PlayerData.instance.charmSlots * 2})");
            }

            return orig;
        }

        private int ModHooks_GetPlayerIntHook(string name, int orig)
        {
            if (name == "charmSlots")
            {
                return orig * 2;
            }
            else
            {
                return orig;
            }
        }

    }
}
