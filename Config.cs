using RedLoader;

namespace BroadcastMessage;

public static class Config
{
    public static ConfigCategory Category { get; private set; }

    public static ConfigEntry<bool> EnableLogging { get; private set; }
    public static ConfigEntry<bool> CheckNamePrinting { get; private set; }
    public static ConfigEntry<bool> PrintSentChatEvent { get; private set; }
    public static ConfigEntry<string> DiscordBotToken { get; private set; }
    public static ConfigEntry<Int64> DiscordChannelId { get; private set; }

    public static void Init()
    {
        Category = ConfigSystem.CreateFileCategory("BroadcastMessage", "BroadcastMessage", "BroadcastMessage.cfg");

        EnableLogging = Category.CreateEntry(
           "enable_logging_broadcast",
           true,
           "Enable Logging To Console",
           "Enable Logging Of Logging Statements To The Console");

        CheckNamePrinting = Category.CreateEntry(
           "enable_check_name_broadcast",
           true,
           "Enable Logging To Console",
           "Enable Logging Of Logging Statements To The Console");

        PrintSentChatEvent = Category.CreateEntry(
           "enable_print_chat_event_broadcast",
           true,
           "Enable ChatEvent ToString Printing To Console",
           "Enable ChatEvent ToString Printing To Console");

        DiscordBotToken = Category.CreateEntry(
           "discord_bot_token_broadcast",
           "TOKEN",
           "Discord Bot Token",
           "Enter Discord Bot Token");

        DiscordChannelId = Category.CreateEntry(
           "discord_channel_id_broadcast",
           (Int64)0,
           "Discord Channel ID",
           "Enter Discord Channel ID");
    }

    // Same as the callback in "CreateSettings". Called when the settings ui is closed.
    public static void OnSettingsUiClosed()
    {
    }
}