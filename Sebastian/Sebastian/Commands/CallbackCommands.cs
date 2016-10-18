using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Sebastian
{
    public class CallbackCommands : CommandsBase
    {
        public CallbackCommands(ITelegramBotClient Bot) : base(Bot)
        {
        }

        public override async void RaiseCommand(object sender, object obj) => await OnCallbackQueryReceived(sender, new Result() { data = obj });
        private async Task OnCallbackQueryReceived(object sender, Result obj)
        {
            CallbackQuery callbackQuery = (CallbackQuery)obj.data;
            //await Bot.EditInlineMessageTextAsync(callbackQueryEventArgs.CallbackQuery.InlineMessageId, callbackQueryEventArgs.CallbackQuery.Data);
            await Bot.AnswerCallbackQueryAsync(callbackQuery.Id,
                $"Received {callbackQuery.Data}");
        }

    }
}
