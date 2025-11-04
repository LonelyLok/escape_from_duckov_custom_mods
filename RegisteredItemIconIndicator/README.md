# RegisteredItemIconIndicator

## Summary
Adds a green dot indicator to item icons for registered items.

## Steps to Build the Mod
1. Install .NET SDK.
2. Update `DuckovPath` in `RegisteredItemIconIndicator.csproj`.
3. Run `dotnet build`.
4. After build, you should see a bin folder. Find `RegisteredItemIconIndicator.dll` inside the bin folder.
5. Follow https://github.com/xvrsl/duckov_modding and create a mod folder inside the game folder with the following files:
    - RegisteredItemIconIndicator.dll (From step 4)
    - 0Harmony.dll (From https://github.com/pardeike/Harmony)
    - info.ini
    - preview.png

## Demo
Shows an example of the green dot indicator displayed on registered item icon.
<img width="1602" height="721" alt="registered-item-icon-indicator-demo" src="https://github.com/user-attachments/assets/2b68a03d-9c4e-4a22-b33c-fcc5f162dce3" />
