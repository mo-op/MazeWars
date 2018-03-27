using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinthe
{
    class Item
    {
        int damage = 0;

        public Item()
        {
            Random r = new Random();
            damage = r.Next(0, 10);
        }
        
        public int getDamage()
        {
            return damage;
        }

        public void setDamage()
        {
            damage -= 1;
        }
    }
}
