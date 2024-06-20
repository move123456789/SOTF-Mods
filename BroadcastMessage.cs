﻿using Sons.Gui;
using Sons.Multiplayer;
using SonsSdk;


namespace BroadcastMessage;

public class BroadcastMessage : SonsMod
{
    public BroadcastMessage()
    {

        // Uncomment any of these if you need a method to run on a specific update loop.
        //OnUpdateCallback = OnUpdate;
        //OnLateUpdateCallback = MyLateUpdateMethod;
        //OnFixedUpdateCallback = MyFixedUpdateMethod;
        //OnGUICallback = MyGUIMethod;

        // Uncomment this to automatically apply harmony patches in your assembly.
        HarmonyPatchAll = true;
    }

    protected override void OnInitializeMod()
    {
        // Do your early mod initialization which doesn't involve game or sdk references here
        Config.Init();
    }

    protected override void OnSdkInitialized()
    {
        // Do your mod initialization which involves game or sdk references here
        // This is for stuff like UI creation, event registration etc.
        BroadcastMessageUi.Create();

        // Add in-game settings ui for your mod.
        // SettingsRegistry.CreateSettings(this, null, typeof(Config));
    }

    protected override void OnGameStart()
    {
        // So I Can Get HostMode Correctly
        SonsSdk.SdkEvents.OnInWorldUpdate.Subscribe(BroadCastExtras.CheckHostModeOnWorldUpdate);
        BroadCastEvents.OnHostModeGotten += BroadCastExtras.OnHostModeGottenCorrectly;
    }

    protected override void OnApplicationQuit() // Works Good For Dedicated Server, Would be Best if it was on leave World On MulitplayerHost
    {
        Misc.Msg("OnApplicationQuit");
        try
        {
            BroadcastInfo.StopBot();
            BroadcastInfo.KillMonoBehavior();
            BroadCastEvents.OnHostModeGotten -= BroadCastExtras.OnHostModeGottenCorrectly;
        } catch (Exception ex) { Misc.Msg("Cathced Error"); }
    }

    internal static void OnLeaveWorld()
    {
        Misc.Msg("OnLeaveWorld");
        BroadcastInfo.StopBot();
        BroadcastInfo.KillMonoBehavior();
        BroadCastEvents.OnHostModeGotten -= BroadCastExtras.OnHostModeGottenCorrectly;
    }
}