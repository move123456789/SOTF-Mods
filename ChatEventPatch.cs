﻿using HarmonyLib;

namespace BroadcastMessage;

[HarmonyPatch]
public class Patch
{
    [HarmonyPatch(typeof(CoopPlayerCallbacks), "OnEvent", new Type[] { typeof(ChatEvent) })]
    [HarmonyPostfix]
    public static void PostfixOnEvent(ChatEvent evnt)
    {
        if (BroadCastExtras.hostMode != BroadCastExtras.SimpleSaveGameType.Multiplayer) { return; }
        Misc.Msg("Postfix OnEvent Loaded");

        // Check if the chatEvent has data.
        if (evnt != null && !string.IsNullOrEmpty(evnt.Message))
        {
            string username = BroadcastInfo.VerifyName(evnt.Sender);
            if (username.ToLower().StartsWith("[discord]") || username.ToLower().StartsWith("[ds]")) { return; }
            Misc.Msg($"Username: {username}");
            Misc.Msg($"Message: {evnt.Message}");
            Misc.Msg($"NetworkId: {evnt.Sender}");

            BroadcastInfo.botManager.SendCommand($"{username}: {evnt.Message}");
        }
        else
        {
            Misc.Msg("OnEvent Postfix: chatEvent is null or does not have a valid message.");
        }
    }
}