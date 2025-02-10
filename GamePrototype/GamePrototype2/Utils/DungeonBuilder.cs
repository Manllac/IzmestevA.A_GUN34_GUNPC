using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public static class DungeonBuilder
    {
        public static DungeonRoom BuildDungeon(GameDifficulty difficulty)
        {
            UnitFactoryBase unitFactory = difficulty == GameDifficulty.Easy
               ? new EasyUnitFactory()
               : new HardUnitFactory();
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", unitFactory.CreateEnemy());
            var emptyRoom = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootStoneRoom = new DungeonRoom("Loot1", new Grindstone("Stone",20));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1",30));

            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

            emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);

            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }
}
