using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace Sebastian
{
    public interface ICommands
    {
        ITelegramBotClient Bot { get; }
        IReadOnlyDictionary<AliasString, Command> Commands { get; }
        void RaiseCommand(object sender,object result);
    }
}
