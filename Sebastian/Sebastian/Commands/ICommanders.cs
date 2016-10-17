using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace Sebastian
{
    public interface ICommands
    {
        ITelegramBotClient Bot { get; }
    }
}
