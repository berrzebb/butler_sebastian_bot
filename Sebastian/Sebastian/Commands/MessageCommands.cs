using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Sebastian
{
    public class MessageCommands : ICommands
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
        private async Task Received(string Name, Message message)
        {
            if (Commands.ContainsKey(Name))
            {
                await Commands[Name].CallAsync(message);
            }
        }
        public MessageCommands(ITelegramBotClient Bot)
        {
            CurrentBot = Bot;
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            RegisterCommand("/도움말", "도움말을 보여드립니다.", ShowHelp);
            RegisterCommand("/드래곤슬레이브", "스펠을 사용합니다(?!)", DragonSlave);

        }
        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
                var message = messageEventArgs.Message;

                if (message == null || message.Type != MessageType.TextMessage) return;
                Logger.WriteLog("Received {0}", message.Text);
                await Received(message.Text, message);
        }
        internal async void DragonSlave(object sender)
            {
                Message message = sender as Message;
                var spell = @"황혼보다 어두운자? 몰라 그런거...";
                await Bot.SendTextMessageAsync(message.Chat.Id, spell, replyMarkup: new ReplyKeyboardHide());

            }
        internal async void ShowHelp(object sender)
        {
            Message message = sender as Message;
            StringBuilder usage = new StringBuilder("사용법은 다음과 같습니다.\n");
            foreach (var command in Commands)
            {
                usage.AppendLine(string.Format("{0} - ({1})\n", command.Value.Name, command.Value.Description));
            }
            await Bot.SendTextMessageAsync(message.Chat.Id, usage.ToString(),
                replyMarkup: new ReplyKeyboardHide());
        }
    }
}
