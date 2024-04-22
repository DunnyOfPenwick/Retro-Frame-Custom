# Retro-Frame-Custom
 Sample mod to alter Retro-Frame

When adding or removing textures, remember to add/remove them in the mod builder as well.

### Structure of Overlay Panel Content (current as of v1.1.1)
- OverlayPanel (default BackgroundTexture is 'Frame')
    - MainPanel
        - LeftPanel
            - CharacterPanel (default BackgroundTexture is 'HeadFrame')
                - HeadPanel (BackgroundTexture gets periodically refreshed from the standard big HUD character panel when needed)
                   - HeadShadePanel (to be used by future mod)
                       - HeadTintPanel (normally transparent, can flash colors to indicate character status)
                - NameLabel
            - InventoryButtonPanel
            - InteractionModeButtonPanel
            - VitalsPanel (default BackgroundTexture is 'VitalsFrame')
                - HUDVitalsBars
            - ActiveEffectsPanel
                - ActiveEffectsRowPanel_ (where _ is 0 through 5)
                    - DescriptionLabel
                    - Icon_ (where _ is 0 to 2, so 3 icons per row)
                        - IconCutout (default BackgroundTexture 'IconCutout')
            - LeftPanelPauseGameOverlay (Darkens left panel when game paused)
        - InstSpellIconContainer (this is the icon for any instantaneous spell that is briefly shown at the bottom of the screen)
            - InstSpellIcon
            - InstSpellIconOverlay
        - InstSpellLabel (where the name of the instantaneous spell is shown)
        - RightPanel
            - ActionsPanel
                - SpellsButtonPanel (The default BackgroundTexture for the buttons is clipped from internal storage)
                - UseButtonPanel
                - WeaponButtonPanel
                - TransportButtonPanel
                - MapButtonPanel
                - RestButtonPanel
            - HotkeysPanel
                - _ (where _ is the panel number, 0-9) (default BackgroundTexture is 'HotkeyButton')
                    - IconContainer
                        - Icon
                        - IconCutout (default BackgroundTexture is 'HotkeyIconCutout')
                        - Animation (for the animated swirl around magic items)
                        - ItemCountLabel
                    - CharLabel (This is the label that shows the key bound to the hotkey)
                    - DescriptionLabel
            - CompassPanel (default BackgroundTexture clipped from internal storage)
                - CompassPointerPanel (default BackgroundTexture is from an array of 32 textures from internal storage, swapped on Update)
            - TogglePanelButtonPanel (default BackgroundTexture is 'Switch')
            - PriorButtonPanel (default BackgroundTexture is 'Prior')
            - NextButtonPanel (default BackgroundTexture is 'Next')
            - RightPanelPauseGameOverlay (Darkens right panel when game paused)
        - ErrorLogIcon (default BackgroundTexture is 'ErrorLogIcon')
            - ErrorLogCountLabel
        - ViewPanel
            - TopBorderPanel (default BackgroundTexture is 'TopBorder')
            - BottomBorderPanel (default BackgroundTexture is 'BottomBorder')
    - ToolTipContainerPanel (needed so tooltips scale correctly)
