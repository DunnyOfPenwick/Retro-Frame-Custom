using System;
using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using DaggerfallWorkshop.Game.UserInterface;


namespace RetroCustom
{
    public class RetroCustomMod : MonoBehaviour
    {
        private static Mod mod;

        private Panel overlay;


        [Invoke(StateManager.StateTypes.Start, 0)]
        public static void Init(InitParams initParams)
        {
            mod = initParams.Mod;

            var go = new GameObject(mod.Title);
            go.AddComponent<RetroCustomMod>();

            mod.IsReady = true;
        }


        void Start()
        {
            Debug.Log("Start(): Custom Retro-Frame");

            Mod retroFrame = ModManager.Instance.GetMod("Retro-Frame");
            if (retroFrame == null)
                return; //Retro-Frame not installed, just exit.

            //Get the overlay Panel from the Retro-Frame mod
            retroFrame.MessageReceiver("getOverlay", null, (string message, object data) => { overlay = (Panel)data; });
            if (overlay == null)
                return;


            ReplaceFrameTexture();

            ReplaceBorderTextures();

            ReplaceHotkeyButtonTextures();

            FormatCharacterNameLabel();

            HideCharacterStatusFlashPanel();

            HideActiveEffectIconCutouts();

            HideHotkeyIconCutouts();
        }


        private void Update()
        {
            //Retro-Frame constantly updates hotkey label formats, so we must reformat every frame.
            FormatHotkeyButtonText();
        }


        void ReplaceFrameTexture()
        {
            Panel panel = GetPanel(""); //Getting the top-level overlay panel
            panel.BackgroundTexture = mod.GetAsset<Texture2D>("Frame");
        }


        void ReplaceBorderTextures()
        {
            Panel top = GetPanel("MainPanel,ViewPanel,TopBorderPanel");
            top.BackgroundTexture = mod.GetAsset<Texture2D>("TopBorder");

            Panel bottom = GetPanel("MainPanel,ViewPanel,BottomBorderPanel");
            bottom.BackgroundTexture = mod.GetAsset<Texture2D>("BottomBorder");
        }


        void ReplaceHotkeyButtonTextures()
        {
            for (int buttonIndex = 0; buttonIndex < 10; ++buttonIndex)
            {
                Panel buttonPanel = GetPanel("MainPanel,RightPanel,HotkeysPanel," + buttonIndex);
                buttonPanel.BackgroundTexture = mod.GetAsset<Texture2D>("HotkeyButton");
            }
        }


        void FormatCharacterNameLabel()
        {
            TextLabel nameLabel = GetTextLabel("MainPanel,LeftPanel,CharacterPanel,NameLabel");
            nameLabel.TextColor = Color.yellow;
            nameLabel.ShadowPosition = Vector2.zero; //no shadow
        }


        void HideCharacterStatusFlashPanel()
        {
            Panel shadeAndTintPanel = GetPanel("MainPanel,LeftPanel,CharacterPanel,HeadPanel,HeadShadePanel");
            shadeAndTintPanel.Enabled = false;
        }


        void HideActiveEffectIconCutouts()
        {
            //These are the textures that cover the icons in the active effects panel (left side) to make
            //them look more circular.

            //6 rows, 3 icons per row
            for (int row = 0; row <= 5; ++row)
            {
                for (int col = 0; col <= 2; ++col)
                {
                    Panel cutout = GetPanel("MainPanel,LeftPanel,ActiveEffectsPanel,ActiveEffectsRowPanel" + row + ",Icon" + col + ",IconCutout");
                    cutout.Enabled = false;
                }
            }

        }

        void HideHotkeyIconCutouts()
        {
            //These are the textures that cover the icons on the hotkey buttons to make
            //them look more circular.

            for (int buttonIndex = 0; buttonIndex < 10; ++buttonIndex)
            {
                Panel cutout = GetPanel("MainPanel,RightPanel,HotkeysPanel," + buttonIndex + ",IconContainer,IconCutout");
                cutout.Enabled = false;
            }
        }


        void FormatHotkeyButtonText()
        {
            for (int buttonIndex = 0; buttonIndex < 10; ++buttonIndex)
            {
                TextLabel charLabel = GetTextLabel("MainPanel,RightPanel,HotkeysPanel," + buttonIndex + ",CharLabel");
                charLabel.TextColor = Color.yellow;
                charLabel.ShadowPosition = Vector2.zero; //no text shadow

                TextLabel descriptionLabel = GetTextLabel("MainPanel,RightPanel,HotkeysPanel," + buttonIndex + ",DescriptionLabel");
                descriptionLabel.TextColor = Color.yellow;
                descriptionLabel.ShadowPosition = Vector2.zero; //no text shadow
            }
        }




        Panel GetPanel(string pathString)
        {
            return (Panel)GetScreenComponent(pathString);
        }


        TextLabel GetTextLabel(string pathString)
        {
            return (TextLabel)GetScreenComponent(pathString);
        }


        BaseScreenComponent GetScreenComponent(string pathString)
        {
            Panel parent = overlay;
            BaseScreenComponent component = parent;

            if (pathString == null || pathString.Trim().Length == 0)
                return component; //just return the top-level overlay panel

            string[] path = pathString.Split(',');
            for (int i = 0; i < path.Length; ++i)
            {
                string item = path[i].Trim();
                component = FindChild(parent, item);
                if (i < path.Length - 1)
                    parent = (Panel)component;
            }

            return component;
        }


        BaseScreenComponent FindChild(Panel parent, string childTag)
        {
            foreach (BaseScreenComponent child in parent.Components)
            {
                if (child.Tag != null && child.Tag.ToString().Equals(childTag, StringComparison.OrdinalIgnoreCase))
                    return child;
            }

            throw new Exception($"Could not find child component with tag '{childTag}'.");
        }



    } //class RetroCustomMod



} //namespace RetroCustom
