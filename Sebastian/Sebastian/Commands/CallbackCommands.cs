using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Sebastian
{
    public class CallbackCommands : ICommands
    {
        private readonly ITelegramBotClient CurrentBot;
        public ITelegramBotClient Bot => CurrentBot;

        private readonly Dictionary<string, Command> Commands = new Dictionary<string, Command>();
        public void RegisterCommand(string Name, string Description, Action<object> command)
        {
            if (!Commands.ContainsKey(Name))
            {
                Commands.Add(Name, new Command() { Name = Name, Description = Description, command = command });
            }
            else
            {
                throw new InvalidOperationException("이미 명령어가 추가되어있습니다.");
            }
        }
        public CallbackCommands(ITelegramBotClient Bot)
        {
            this.CurrentBot = Bot;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

        }


        private async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        { 
            await Bot.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id,
                $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
        }

    }
}
