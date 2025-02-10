using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Units;
using GamePrototype.Utils;
using System;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();

        public void StartGame()
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        #region Game Loop

        private void Initialize()
        {
            Console.WriteLine("Welcome, player!");

            Console.WriteLine("Choose difficulty: 1 - Easy, 2 - Hard");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid input. Choose: 1 - Easy, 2 - Hard");
            }
            GameDifficulty difficulty = (choice == 1) ? GameDifficulty.Easy : GameDifficulty.Hard;

            _dungeon = DungeonBuilder.BuildDungeon(difficulty);

            Console.WriteLine("Enter your name");
            _player = UnitFactoryDemo.CreatePlayer(Console.ReadLine());
            Console.WriteLine($"Hello {_player.Name}");
        }

        private void StartGameLoop()
        {
            var currentRoom = _dungeon;

            while (currentRoom.IsFinal == false)
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success)
                {
                    Console.WriteLine("Game over!");
                    return;
                }
                DisplayRouteOptions(currentRoom);
                while (true)
                {
                    if (Enum.TryParse<Direction>(Console.ReadLine(), out var direction))
                    {
                        currentRoom = currentRoom.Rooms[direction];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong direction!");
                    }
                }
            }
            Console.WriteLine($"Congratulations, {_player.Name}");
            Console.WriteLine("Result: ");
            Console.WriteLine(_player.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Loot != null)
            {
                _player.AddItemToInventory(currentRoom.Loot);
            }
            if (currentRoom.Enemy != null)
            {
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    _player.HandleCombatComplete();
                    LootEnemy(currentRoom.Enemy);
                }
                else
                {
                    success = false;
                }
            }

            void LootEnemy(Unit enemy)
            {
                _player.AddItemsFromUnitToInventory(enemy);
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} - {(int)room.Key}\t");
            }
        }


        #endregion
    }
}
