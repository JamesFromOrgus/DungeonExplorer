namespace DungeonExplorer
{
    public class Shade : Combatant
    {
        public Shade()
        {
            _maxHealth = 50;
            _health = _maxHealth;
            _damage = 7;
            _name = "Shade";
        }
    }

    public class Ghoul : Combatant
    {
        public Ghoul()
        {
            _maxHealth = 30;
            _health = _maxHealth;
            _damage = 5;
            _name = "Ghoul";
        }
    }
}