namespace GamePrototype.Units
{
    public class EasyUnitFactory : UnitFactoryBase
    {
        public override Unit CreateEnemy()
        {
            return new Goblin("Weak Goblin", 20, 20, 3); 
        }
    }
}