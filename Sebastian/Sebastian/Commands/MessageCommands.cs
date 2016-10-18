using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Sebastian
{
    public class MessageCommands : CommandsBase
    {
        public MessageCommands(ITelegramBotClient Bot) : base(Bot)
        {
            RegisterCommand(new string[] { "세바스찬", "바틀러", "집사", "Sebastian" }, "부르셨습니까 주인님", CallButler);
            RegisterCommand("/도움말", "도움말을 보여드립니다.", ShowHelp);
            RegisterCommand(new string[]{"/찾아줘", "/검색" },"정보를 찾아드립니다.", Search);
            RegisterCommand("/드래곤슬레이브", "스펠을 사용합니다(?!)", DragonSlave);

        }
        public override async void RaiseCommand(object sender, object result) => await OnMessageReceived(sender, new Result() { data = result });
        private async Task OnMessageReceived(object sender, Result result)
        {
            Message message = result.data as Message;
            if (message == null || message.Type != MessageType.TextMessage) return;
            try
            {
                result.args = message.Text.Split(' ');
                await Received((string)result.args[0], result);
            }
            catch
            {
            //    await Bot.SendTextMessageAsync(message.Chat.Id, "이해 할 수 없는 명령이군요. 주인님.", replyMarkup: new ReplyKeyboardHide());
            }
        }
        internal async void CallButler(Result result)
        {
            Message message = result.data as Message;
            await Bot.SendTextMessageAsync(message.Chat.Id, "부르셨습니까, 당신의 편의를 봐드립니다.", replyMarkup: new ReplyKeyboardHide());
        }
        internal async void DragonSlave(Result result)
        {
            Message message = result.data as Message;
            var spell = @"황혼보다 어두운자? 몰라 그런거...";
            await Bot.SendTextMessageAsync(message.Chat.Id, spell, replyMarkup: new ReplyKeyboardHide());
        }
        internal async void Search(Result result)
        {
            Message message = result.data as Message;
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            var query = string.Join("", result.args.Skip(1).ToArray());
            var keyboard = new InlineKeyboardMarkup(new []
            {
                new InlineKeyboardButton("나무위키") {Url=new StringBuilder("https://namu.wiki/search/").Append(query).ToString() },
                new InlineKeyboardButton("구글"){Url=new StringBuilder("http://www.google.co.kr/search?complete=1&hl=ko&q=").Append(query).ToString() },
                new InlineKeyboardButton("Im Feeling Lucky") { Url=new StringBuilder("http://www.google.com/search?ie=UTF-8&oe=UTF-8&sourceid=navclient&gfns=1&hl=ko&q=").Append(query).ToString() }
            });
            await Task.Delay(500);
            await Bot.SendTextMessageAsync(message.Chat.Id, "어디서 찾아드릴까요? 주인님", replyMarkup: keyboard);
        }
        internal async void ShowHelp(Result result)
        {
            Message message = result.data as Message;
            StringBuilder usage = new StringBuilder("사용법은 다음과 같습니다.\n");
            foreach (var command in Commands)
            {
                usage.AppendLine(string.Format("{0} - ({1})\n", command.Value.Name.ToString(), command.Value.Description));
            }
            usage.AppendLine("= Inline 명령어 =");
            usage.AppendLine("@gif - (gif 검색)");
            usage.AppendLine("@vid - (비디오 검색)");
            usage.AppendLine("@pic - (Yandex 사진 검색)");
            usage.AppendLine("@bing - (빙 이미지 검색)");
            usage.AppendLine("@wiki - (위키페디아 검색)");
            usage.AppendLine("@imdb - (IMDB 검색)");
            usage.AppendLine("@bold - (Make Bold)");
            usage.AppendLine("@youtube - (유튜브 검색)");
            usage.AppendLine("@sticker - (스티커 검색)");

            await Bot.SendTextMessageAsync(message.Chat.Id, usage.ToString(),
                replyMarkup: new ReplyKeyboardHide());
        }
    }
}
