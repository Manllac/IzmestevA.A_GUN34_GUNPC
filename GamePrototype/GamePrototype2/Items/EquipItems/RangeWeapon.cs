using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using static System.Net.Mime.MediaTypeNames;

public class RangeWeapon : EquipItem

{
    public uint Ammo { get; private set; }
    public uint Damage { get; }
    public uint Range { get; }

    public RangeWeapon(uint damage, uint maxDurability, uint ammo, string name)
         : base(maxDurability, name)  
    {
        Damage = damage; 
        Ammo = ammo;
    }

    public override EquipSlot Slot => EquipSlot.RangeWeapon; 
}