using System;

namespace DungeonExplorer
{
    /// <summary>
    /// Class for combatants that will be CPU-controlled. Cannot use items in combat and choices are randomised.
    /// </summary>
    public class Creature : Combatant
    {
        private readonly int _damage;
        private static readonly Random Generator = new Random();
        private readonly int _aggression;

        public Creature(string name, int health, int damage, int aggression) : base(name, health)
        {
            _damage = damage;
            _aggression = aggression;
        }
        
        /// <summary>
        /// Attempt to damage opponent, stun if failed.
        /// </summary>
        public override void Attack(Combatant target)
        {
            Display.Write($"{Name} swings at you.");
            bool success = target.TakeDamage(_damage);
            if (!success) Stun();
        }
        
        /// <summary>
        /// Will defend 1/aggression times, meaning higher aggression relates to greater chance of attacking.
        /// </summary>
        protected override CombatDecision GetDecision()
        {
            if (Generator.Next(_aggression) == 0)
            {
                return CombatDecision.Defend;
            }
            return CombatDecision.Attack;
        }
    }
}