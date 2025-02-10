using GamePrototype.Units.Enemies;

namespace GamePrototype.Units
{
    public class HardUnitFactory : UnitFactoryBase
    {
        public override Unit CreateEnemy()
        {
            return new Orc("Strong Orc", 50, 50, 10); // Создаем сильного орка
        }
    }
}