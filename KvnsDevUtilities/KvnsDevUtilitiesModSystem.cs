using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace KvnsDevUtilities
{
    public class KvnsDevUtilitiesModSystem : ModSystem
    {

        // Called on server and client
        // Useful for registering block/entity classes on both sides
        public override void Start(ICoreAPI api)
        {
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            api.ChatCommands.Create("makemehungry")
                .WithDescription("Sets player satiety to 0")
                .RequiresPrivilege(Privilege.chat)
                .RequiresPlayer()
                .HandleWith((args) =>
                {
                    var player = args.Caller.Entity.GetBehavior<EntityBehaviorHunger>();
                    player?.DairyLevel = 0;
                    player?.FruitLevel = 0;
                    player?.GrainLevel = 0;
                    player?.ProteinLevel = 0;
                    player?.VegetableLevel = 0;
                    player?.Saturation = 0;
                    return TextCommandResult.Success();
                });
        }

        public override void StartClientSide(ICoreClientAPI api)
        {

            api.ChatCommands.Create("itemattr")
                .WithDescription("Prints the item attributes of the currently held item.")
                .RequiresPrivilege(Privilege.chat)
                .RequiresPlayer()
                .HandleWith((args) =>
                {
                    string msg = args?.Caller?.Player?.InventoryManager?.ActiveHotbarSlot?.Itemstack?.ItemAttributes.ToString() ?? "Can't get ItemAttributes. No item selected?";

                    api.SendChatMessage(msg);
                    return TextCommandResult.Success();
                });
            api.ChatCommands.Create("attr")
                .WithDescription("Prints the attributes of the currently held.")
                .RequiresPrivilege(Privilege.chat)
                .RequiresPlayer()
                .HandleWith((args) =>
                {
                    string msg = args?.Caller?.Player?.InventoryManager?.ActiveHotbarSlot?.Itemstack?.Attributes.ToJsonToken() ?? "Can't get ItemAttributes. No item selected?";

                    api.SendChatMessage(msg);
                    return TextCommandResult.Success();
                });
        }

    }
}
