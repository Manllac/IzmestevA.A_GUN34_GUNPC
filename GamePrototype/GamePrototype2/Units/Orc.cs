namespace GamePrototype.Units.Enemies
{
    public class Orc : Unit
    {
        public Orc(string name, uint health, uint maxHealth, uint baseDamage)
            : base(name, health, maxHealth, baseDamage) { }

        protected override uint CalculateAppliedDamage(uint damage) => damage;

        public override uint GetUnitDamage() => BaseDamage;

        public override void HandleCombatComplete() { }
    }
}
