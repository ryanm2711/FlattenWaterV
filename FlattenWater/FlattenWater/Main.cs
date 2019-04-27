using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using System.Runtime.InteropServices;

namespace FlattenWater
{
    public class Main : Script
    {
        private bool HasScriptBeenActivated;
        public ScriptSettings iniConfig;
        private Keys ActivationKey;
        private bool UseShiftKey;
        private int GetHashKey(string value) { return Function.Call<int>(Hash.GET_HASH_KEY, value); }
        private bool FlattenWaterCheatActivated;
        private float WaterFloatInput;
        private bool ShouldBeOnDefault;

        public Main()
        {
            KeyDown += OnKeyDown;
            Tick += OnTick;
            iniConfig = ScriptSettings.Load("scripts\\FlattenWater.ini");
            ActivationKey = iniConfig.GetValue("General", "ActivationKey", Keys.F6);
            UseShiftKey = iniConfig.GetValue("General", "UseShiftKey", false);
            ShouldBeOnDefault = iniConfig.GetValue("General", "ActivateScriptByDefault", false);
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (ShouldBeOnDefault && !HasScriptBeenActivated)
            {
                Function.Call((Hash)0xB96B00E976BE977F, 0.0f); // _SET_CURRENT_INTENSITY
                HasScriptBeenActivated = true;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (UseShiftKey)
            {
                if (e.KeyCode == Keys.Shift && e.KeyCode == ActivationKey)
                {
                    if (!HasScriptBeenActivated)
                    {
                        Function.Call((Hash)0xB96B00E976BE977F, 0.0f); // _SET_CURRENT_INTENSITY
                        UI.Notify("~b~FlattenWater ~g~V ~w~has been activated.");
                        HasScriptBeenActivated = true;
                    }
                    else
                    {
                        Function.Call((Hash)0x5E5E99285AE812DB); // _RESET_CURRENT_INTENSITY
                        UI.Notify("~b~FlattenWater ~g~V ~w~has been deactivated.");
                        HasScriptBeenActivated = false;
                    }
                }
            }
            else
            {
                if (e.KeyCode == ActivationKey)
                {
                    if (!HasScriptBeenActivated)
                    {
                        Function.Call((Hash)0xB96B00E976BE977F, 0.0f); // _SET_CURRENT_INTENSITY
                        UI.Notify("~b~FlattenWater ~g~V ~w~has been activated.");
                        HasScriptBeenActivated = true;
                    }
                    else
                    {
                        Function.Call((Hash)0x5E5E99285AE812DB); // _RESET_CURRENT_INTENSITY
                        UI.Notify("~b~FlattenWater ~g~V ~w~has been deactivated.");
                        HasScriptBeenActivated = false;
                    }
                }
            }
        }

        private void TextBox(string text, bool emitsound)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
            Function.Call((Hash)0x238FFE5C7B0498A6, 0, 0, emitsound, -1); // The hash being used here is: END_TEXT_COMMAND_DISPLAY_HELP        }
        }
    }
}
