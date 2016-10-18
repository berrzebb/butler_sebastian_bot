using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Sebastian
{
    public abstract class CommandsBase : ICommands
    {
        protected readonly ITelegramBotClient CurrentBot;
        protected IDictionary<AliasString, Command> CurrentCommands = new Dictionary<AliasString,Command>();
        public ITelegramBotClient Bot => CurrentBot;
        public IReadOnlyDictionary<AliasString, Command> Commands { get { return new ReadOnlyDictionary<AliasString,Command>(CurrentCommands); } }

        public CommandsBase(ITelegramBotClient bot)
        {
            this.CurrentBot = bot;
        }
        protected void RegisterCommand(string Name, string Description, Action<Result> command) => RegisterCommand(new string[] { Name }, Description, command);
        protected void RegisterCommand(string[] Name, string Description, Action<Result> command)
        {
            AliasString Key = new AliasString(Name);
            if (!CurrentCommands.ContainsKey(Key))
            {
                CurrentCommands.Add(Key, new Command() { Name = Key, Description = Description, command = command });
            }
            else
            {
                throw new InvalidOperationException("이미 명령어가 추가되어있습니다.");
            }
        }
        protected async Task Received(string Name, Result result)
        {
            AliasString Key = Commands.Keys.Where(x => x.Contains(Name)).First();
            if (Key != null)
            {
                Logger.WriteLog("Received Command {0} Parameter {1}", result.args[0], string.Join(" ", result.args.Skip(1).ToArray()));
                await Commands[Key].CallAsync(result);
            }
        }

        public virtual async void RaiseCommand(object sender, object result) => await Task.Delay(1);
    }
}
