using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Derpy.Karma
{
    [Group("karma")]
    [Summary("Show and manage people's karma")]
    public class KarmaModule : ModuleBase<SocketCommandContext>
    {
        private readonly Service _service;

        public KarmaModule(Service service) => _service = service;

        [Command]
        [Alias("show")]
        public Task ShowKarma() => ShowKarma(Context.User);

        [Command]
        [Alias("show")]
        [Summary("Shows the current karma value for an user")]
        public async Task ShowKarma(SocketUser user)
        {
            await ReplyAsync($"Karma for {user.Username} is **{await _service.GetKarma(user)}**.");
        }

        [Command("add")]
        [RequireOwner]
        [Summary("Give 1 point of karma to an user")]
        public Task AddKarma(SocketUser user)
        {
            _service.AddKarma(user);
            return Task.CompletedTask;
        }

        [Command("stats")]
        [Summary("Shows all time stats")]
        public async Task GetStats()
        {
            var (userCount, karmaTotal) = await _service.GetStats();
            await ReplyAsync($"There are {userCount} users with karma for a total of {karmaTotal} karma.");
        }
    }
}
