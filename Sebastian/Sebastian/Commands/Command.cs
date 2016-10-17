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
        public string Name;
        public Action<Message> command;
        public void Call(Message message)
        {
            command?.Invoke(message);
        }
        public async Task CallAsync(Message message)
        {
            await Task.Run(() => Call(message));
        }
    }
}
