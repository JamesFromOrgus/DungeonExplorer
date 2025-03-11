using System;

namespace DungeonExplorer
{
    public enum CombatDecision {
        Attack,
        Defend,
    }
    public class Combatant
    {
        protected static readonly Random Generator = new Random();
        protected readonly string _name = "BaseCombatant";
        private int _health;
        protected int _maxHealth;
        protected int _damage;
        
        public bool Stunned { get; protected set; }
        public bool Parrying { get; protected set; }

        public int Health
        {
            get => _health;
            set => _health = Math.Max(0, Math.Min(_maxHealth, value));
        }

        public string Name
        {
            get => _name;
        }

        public bool TakeDamage(int damage)
        {
            if (Parrying)
            {
                Display.Write($"{Name} parries the attack!");
                return false;
            }
            Health -= damage;
            Display.Write($"{Name} took {damage} damage.");
            return true;
        }

        private void Stun()
        {
            Stunned = true;
        }

        public void Attack(Combatant target)
        {
            Display.Write($"{Name} swings at {target.Name}");
            bool success = target.TakeDamage(_damage);
            if (!success) Stun();
        }

        public void Defend()
        {
            Parrying = true;
            Display.Write($"{Name} readies their defence!");
        }

        protected CombatDecision GetDecision()
        {
            if (Generator.Next(3) == 0)
            {
                return CombatDecision.Defend;
            }
            return CombatDecision.Attack;
        }

        public void Fight(Combatant opponent)
        {
            Parrying = false;
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