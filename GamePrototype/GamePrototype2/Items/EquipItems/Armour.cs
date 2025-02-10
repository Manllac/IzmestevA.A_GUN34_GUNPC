using GamePrototype.Utils;
using System;

namespace GamePrototype.Items.EquipItems
{
    public sealed class Armour : EquipItem
    {
        public Armour(uint defence, uint durability, string name) : base(durability, name) => Defence = defence;

        public uint Defence { get; }

        public void ReduceDurability(uint amount)
        {
            if (Durability > 0)
            {
                Durability -= amount;
                Console.WriteLine($"{Name} durability decreased by {amount}. Current durability: {Durability}");

                if (Durability == 0)
                {
                    Console.WriteLine($"{Name} is broken and removed from the inventory.");
                }
            }
        }

        public override EquipSlot Slot => EquipSlot.Armour;
    }
}
