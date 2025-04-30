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
    public abstract class Combatant
    {
        private readonly string _name;
        private int _health;
        private readonly int _maxHealth;

        protected Combatant(string name, int health)
        {
            _name = name;
            _maxHealth = health;
            _health = health;
        }
        
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
        public bool Dead => _health <= 0;

        public string Name => _name;
        
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
            Display.Write($"{Name} takes {damage} damage. [{Health}/{_maxHealth}hp]");
            return true;
        }

        /// <summary>
        /// Restore to full health.
        /// </summary>
        public void Heal()
        {
            Heal(_maxHealth);
        }

        /// <summary>
        /// Heal by a defined amount.
        /// </summary>
        public void Heal(int amount)
        {
            Health += amount;
            Display.Write($"Restored to {Health} health.");
        }

        /// <summary>
        /// Prevent combatant from taking its next turn.
        /// </summary>
        public void Stun()
        {
            Display.Write($"{Name} is stunned.");
            Stunned = true;
        }

        /// <summary>
        /// Attempt to deal damage to opponent, and stun if the attack is parried.
        /// </summary>
        public abstract void Attack(Combatant target);

        /// <summary>
        /// Set parry flag so the game knows to stun opponent if they are foolish enough to attack.
        /// </summary>
        public void Defend()
        {
            Parrying = true;
            Display.Write($"{Name} readies their defence!");
        }

        /// <summary>
        /// Provides the decision of the combatant. Meant to be overridden in subclasses to provide either AI behaviour
        /// or player choice that will control the actions of the combatant.
        /// </summary>
        protected abstract CombatDecision GetDecision();
        
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