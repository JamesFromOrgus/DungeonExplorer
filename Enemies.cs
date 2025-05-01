namespace DungeonExplorer
{
    public static class Enemies
    {
        /// <summary>
        /// Stronger enemy that is more likely to defend.
        /// </summary>
        public static Creature Shade()
        {
            return new Creature("Shade", 45, 35, 2);
        }

        /// <summary>
        /// Weak enemy that can be taken out in a singular hit with the right tools.
        /// </summary>
        public static Creature Ghoul()
        {
            return new Creature("Ghoul", 30, 20, 3);
        }
        
        /// <summary>
        /// Weak enemy that can be taken out in a singular hit with the right tools.
        /// </summary>
        public static Vampire Boss()
        {
            return new Vampire("Masked Man", 150, 40, 4, .5);
        }
    }
}