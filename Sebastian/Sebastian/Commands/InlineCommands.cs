using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Sebastian
{
    public class InlineCommands : ICommands
    {
        private readonly ITelegramBotClient CurrentBot;
        public ITelegramBotClient Bot => CurrentBot;

        public InlineCommands(ITelegramBotClient Bot)
        {
            this.CurrentBot = Bot;
            Bot.OnInlineQuery += BotOnInlineQueryReceived;
            Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;

        }
        private async void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            await Logger.WriteLogAsync($"Received choosen inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            await Logger.WriteLogAsync("Received Inline Query :{0}", inlineQueryEventArgs.InlineQuery.Id);
        }
    }
}
