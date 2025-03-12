namespace DungeonExplorer
{
    /// <summary>
    /// Stronger enemy, takes 2 hits to defeat.
    /// </summary>
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
    
    /// <summary>
    /// Weak enemy that can be taken out in a singular hit.
    /// </summary>
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