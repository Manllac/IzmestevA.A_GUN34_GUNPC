﻿using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;

public class Helmet : EquipItem
{
    public uint Defence { get; }

    public Helmet(uint defence, uint durability, string name) : base(durability, name)
    {
        Defence = defence;
    }

    public override EquipSlot Slot => EquipSlot.Helmet;
}
