using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            {
                return BaseDamage + weapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if(item is EquipItem equipItem)
    {
                if (_equipment.ContainsKey(equipItem.Slot))
                {
                   
                    EquipItem oldItem = _equipment[equipItem.Slot];
                    _equipment[equipItem.Slot] = equipItem; 

                   
                    if (!Inventory.TryAdd(oldItem))
                    {
                        Console.WriteLine($"Inventory is full! Dropping {oldItem.Name}");
                    }

                    Console.WriteLine($"Replaced {oldItem.Name} with {equipItem.Name} in slot {equipItem.Slot}");
                }
                else
                {
                   
                    _equipment[equipItem.Slot] = equipItem;
                    Console.WriteLine($"Equipped {equipItem.Name} in slot {equipItem.Slot}");
                }
            }
    else
            {
               
                if (!Inventory.TryAdd(item))
                {
                    Console.WriteLine($"Inventory of {Name} is full");
                }
            }
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion)
            {
                Health += healthPotion.HealthRestore;
            }

            else if (economicItem is Grindstone grindstone)
            {
                if (_equipment.TryGetValue(EquipSlot.Weapon, out var equipItem) && equipItem is Weapon weapon)
                {
                    grindstone.Use(weapon);
                    Inventory.TryRemove(grindstone); 
                    Console.WriteLine($"{Name} using {grindstone.Name}, weapon has regained its durability.");
                }
            }
        }

        public EquipItem GetEquipItem(EquipSlot slot)
        {
            _equipment.TryGetValue(slot, out var item);
            return item;
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                damage -= (uint)(damage * (armour.Defence / 100f));
            }
            return damage;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}
