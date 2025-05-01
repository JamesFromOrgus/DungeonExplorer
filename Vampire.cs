namespace DungeonExplorer
{
    /// <summary>
    /// Life-stealing enemy used for the final boss.
    /// </summary>
    public class Vampire : Creature
    {
        private readonly double _lifesteal;

        public Vampire(string name, int health, int damage, int aggression, double lifesteal) : base(name, health, damage,
            aggression)
        {
            _lifesteal = lifesteal;
        }
        
        /// <summary>
        /// Attempt to damage opponent, stun if failed. Life-steal part of inflicted damage.
        /// </summary>
        public override void Attack(Combatant target)
        {
            Display.Write($"{Name} swings at {target.Name}");
            bool success = target.TakeDamage(_damage);
            if (!success) Stun();
            else
            {
                Display.Write($"{Name} heals for {(int)(_lifesteal*100)}% of inflicted damage.");
                Heal((int)(_damage*_lifesteal));
            }
        }
    }
}