using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Sebastian
{
    public class Command
    {
        public string Description;
        public AliasString Name;
        public Action<Result> command;
        public void Call(Result message)
        {
            command?.Invoke(message);
        }
        public async Task CallAsync(Result message)
        {
            await Task.Run(() => Call(message));
        }
    }
}
