using GamePrototype.Items.EquipItems;

namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem
    {
        private readonly uint _durabilityRestore;
        public override bool Stackable => false;

        public Grindstone(string name, uint durabilityRestore) : base(name) 
        {
            _durabilityRestore = durabilityRestore; 
        }
        public void Use(Weapon weapon)
        {
            weapon.Repair(_durabilityRestore);
        }
    }
}
