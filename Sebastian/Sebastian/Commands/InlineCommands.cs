using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Sebastian
{
    public class InlineCommands : CommandsBase
    {
        public InlineCommands(ITelegramBotClient Bot) : base(Bot)
        {
            RegisterCommand(new string[] { "@도움말" }, "도움말을 보여드립니다.", ShowHelp);
        }
        public override async void RaiseCommand(object sender, object obj)
        {
            if(obj is ChosenInlineResult)
            {
                await OnChosenInlineResultReceived(sender, new Result() { data = obj });
            }
            else if(obj is InlineQuery)
            {
                await OnInlineQueryReceived(sender,new Result() { data = obj });
            }
        }
        private async Task OnChosenInlineResultReceived(object sender, Result result)
        {
            ChosenInlineResult chosenInlineResult = (ChosenInlineResult)result.data;
            await Logger.WriteLogAsync($"Received choosen inline result: {chosenInlineResult.ResultId}");
        }

        private async Task OnInlineQueryReceived(object sender, Result result)
        {
            InlineQuery inlineQuery = (InlineQuery)result.data;
            if (inlineQuery == null) return;
            try
            {
                Logger.WriteLog("Received {0}", inlineQuery.Query);
                await Received(inlineQuery.Query, result);
            }
            catch
            {
                await Bot.SendTextMessageAsync(inlineQuery.Id, "이해 할 수 없는 명령이군요. 주인님.", replyMarkup: new ReplyKeyboardHide());
            }
            await Logger.WriteLogAsync("Received Inline Query :{0}", inlineQuery.Id);
        }
        internal async void ShowHelp(Result result)
        {
            InlineQuery message = result.data as InlineQuery;
            StringBuilder usage = new StringBuilder("인라인 명령어 사용법은 다음과 같습니다.\n");
            foreach (var command in Commands)
            {
                usage.AppendLine(string.Format("{0} - ({1})\n", command.Value.Name.ToString(), command.Value.Description));
            }
            await Bot.SendTextMessageAsync(message.Id, usage.ToString(),
                replyMarkup: new ReplyKeyboardHide());
        }
    }
}
