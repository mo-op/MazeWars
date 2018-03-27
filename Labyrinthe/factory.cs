using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinthe
{
    class factory
    {
        public static Exit getExit()
        {
            return new Exit();
        }

        public static Wall getWall()
        {
            return new Wall();
        }

        public static void getItem(Labyrinthe lab)
        {
            int x;
            int y;
            do
            {
                Random rnd = new Random();
                x = rnd.Next(0, lab.getLength());
                y = rnd.Next(0, lab.getWeight());
            }
            while (lab.getCase(x, y) != null);
            lab.setCase(x, y, new Item());
        }

        public static Combattant getCombattant(Labyrinthe lab, int id)
        {
            int x;
            int y;
            do
            {
                Random rnd = new Random();
                x = rnd.Next(0, lab.getLength());
                y = rnd.Next(0, lab.getWeight());
            }
            while (lab.getCase(x, y) != null);
            Combattant a = new Combattant(x, y, id);
            lab.setCase(x , y, a);
            lab.listCombattant.Add(a);
            return a;
        }
    }
}
