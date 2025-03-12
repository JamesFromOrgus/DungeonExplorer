using System;

namespace DungeonExplorer
{
    /// <summary>
    /// All the different choices that can be made in the turn-based combat system, by NPC or player.
    /// </summary>
    public enum CombatDecision {
        Attack,
        Defend,
    }
    /// <summary>
    /// Base class for anything that can partake in combat: players and enemies.
    /// </summary>
    public class Combatant
    {
        protected static readonly Random Generator = new Random();
        protected string _name = "BaseCombatant";
        protected int _health;
        protected int _maxHealth;
        protected int _damage;
        
        public bool Stunned { get; protected set; }
        public bool Parrying { get; protected set; }

        /// <summary>
        /// Cap health between 0 and its maximum possible value.
        /// </summary>
        public int Health
        {
            get => _health;
            set => _health = Math.Max(0, Math.Min(_maxHealth, value));
        }
        
        /// <summary>
        /// Return true if combatant is dead.
        /// </summary>
        public bool Dead { get => _health <= 0;  }

        public string Name
        {
            get => _name;
        }
        
        /// <summary>
        /// Alter health appropriately based on whether attack was defended.
        /// </summary>
        public bool TakeDamage(int damage)
        {
            if (Parrying)
            {
                Display.Write($"{Name} parries the attack!");
                return false;
            }
            Health -= damage;
            Display.Write($"{Name} takes {damage} damage.");
            return true;
        }

        /// <summary>
        /// Prevent combatant from taking its next turn.
        /// </summary>
        private void Stun()
        {
            Display.Write($"{Name} is stunned.");
            Stunned = true;
        }

        /// <summary>
        /// Attempt to deal damage to opponent, and stun if the attack is parried.
        /// </summary>
        public void Attack(Combatant target)
        {
            Display.Write($"{Name} swings at {target.Name}");
            bool success = target.TakeDamage(_damage);
            if (!success) Stun();
        }

        /// <summary>
        /// Set parry flag so the game knows to stun opponent if they are foolish enough to attack.
        /// </summary>
        public void Defend()
        {
            Parrying = true;
            Display.Write($"{Name} readies their defence!");
        }
        
        /// <summary>
        /// Provides the decision of the combatant. Meant to be overridden in Player class to enable them to make a
        /// choice rather than random chance. Default behaviour is 1/3 chance of defence, 2/3 chance of attack,
        /// offering some variety in NPC patterns.
        /// </summary>
        protected virtual CombatDecision GetDecision()
        {
            if (Generator.Next(3) == 0)
            {
                return CombatDecision.Defend;
            }
            return CombatDecision.Attack;
        }
        
        /// <summary>
        /// Play out a single turn of combat and pass off the next turn to the opponent if they are alive.
        /// </summary>
        public void Fight(Combatant opponent)
        {
            Parrying = false;
            if (Dead)
            {
                Display.Write($"{Name} succumbs to their injuries.");
                return;
            }

            if (Stunned)
            {
                Stunned = false;
                Display.Write($"{Name} is stunned, turn skipped!");
                opponent.Fight(this);
                return;
            }
            CombatDecision decision = GetDecision();
            switch (decision)
            {
                case CombatDecision.Attack:
                {
                    Attack(opponent);
                    opponent.Fight(this);
                    break;
                }
                case CombatDecision.Defend:
                {
                    Defend();
                    opponent.Fight(this);
                    break;
                }
            }
        }
    }
}