using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Contract an item is able to fulfill in order to be used in combat against enemies.
    /// </summary>
    interface IOffensive
    {
        bool Attack(Combatant enemy);
    }
}
