using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Sebastian
{
    internal class Sebastian
    {
        public static readonly TelegramBotClient Bot = new TelegramBotClient("285067809:AAGKEGiNYp5MOLEDFHxKZE47dBzzzfE09Sk");
        public IList<ICommands> Commands = new List<ICommands>();
        public Sebastian()
        {
            Logger.WriteLog("집사 세바스찬 초기화...");

            Bot.OnReceiveError += BotOnReceiveError;
            Logger.WriteLog("명령어 등록 시작...");
            Commands.Add(new MessageCommands(Bot));
            Commands.Add(new CallbackCommands(Bot));
            Commands.Add(new InlineCommands(Bot));
            Logger.WriteLog("명령어 등록 완료...");
            Logger.WriteLog("정보를 얻습니다...");
            var me = Bot.GetMeAsync().Result;

            Console.Title = me.Username;
            Logger.WriteLog("집사 세바스찬 초기화 완료...");

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


        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Debugger.Break();
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
