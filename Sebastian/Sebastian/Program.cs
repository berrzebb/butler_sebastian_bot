using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Sebastian
{
    internal class Sebastian
    {
        public static readonly TelegramBotClient Bot = new TelegramBotClient(Settings.Default.APIKey);
        public IDictionary<UpdateType,ICommands> Commands = new Dictionary<UpdateType,ICommands>();
        public Sebastian()
        {
            Logger.WriteLog("집사 세바스찬 초기화...");
            Bot.OnUpdate += OnUpdate;
            Bot.OnReceiveError += OnReceiveError;
            Logger.WriteLog("명령어 등록 시작...");
            Commands.Add(UpdateType.MessageUpdate,new MessageCommands(Bot));
            Commands.Add(UpdateType.CallbackQueryUpdate,new CallbackCommands(Bot));
            Commands.Add(UpdateType.InlineQueryUpdate,new InlineCommands(Bot));
            Logger.WriteLog("명령어 등록 완료...");
            Logger.WriteLog("정보를 얻습니다...");
            var me = Bot.GetMeAsync().Result;

            Console.Title = me.Username;
            Logger.WriteLog("집사 세바스찬 초기화 완료...");

        }

        private void OnUpdate(object sender, UpdateEventArgs e)
        {
            switch (e.Update.Type)
            {
                case UpdateType.MessageUpdate:
                    Commands[UpdateType.MessageUpdate].RaiseCommand(sender, e.Update.Message);
                    break;
                case UpdateType.EditedMessage:
                    Commands[UpdateType.MessageUpdate].RaiseCommand(sender, e.Update.EditedMessage);
                    break;
                case UpdateType.InlineQueryUpdate:
                    Commands[UpdateType.InlineQueryUpdate].RaiseCommand(sender, e.Update.InlineQuery);
                    break;
                case UpdateType.ChosenInlineResultUpdate:
                    Commands[UpdateType.ChosenInlineResultUpdate].RaiseCommand(sender, e.Update.ChosenInlineResult);
                    break;
                case UpdateType.CallbackQueryUpdate:
                    Commands[UpdateType.CallbackQueryUpdate].RaiseCommand(sender, e.Update.CallbackQuery);
                    break;
            }
        }

        public void Start()
        {
            Logger.WriteLog("집사 세바스찬 가동합니다.");
            Bot.StartReceiving();
        }
        public void Stop()
        {
            Logger.WriteLog("집사 세바스찬 가동중지합니다.");
            Bot.StopReceiving();
        }


        private void OnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            //Debugger.Break();
        }
    }
    class Program
    {

        static Sebastian butler = new Sebastian();
        static void Main(string[] args)
        {
            butler.Start();
            Console.ReadLine();
            butler.Stop();
        }
    }
}
