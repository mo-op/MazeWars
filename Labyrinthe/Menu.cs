using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace Labyrinthe

{
    class Menu
    {
        public void startTheShow()
        {
            Console.WriteLine("The Labyrinth Fight");
            Console.WriteLine();
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. View Credits");
            Console.WriteLine("3. Exit");
            int chosen = 0;
            do
            {
                var result = Console.ReadLine();
                int choice = Convert.ToInt32(result);
                switch (choice)
                {
                    case 1:
                        Labyrinthe labyrinthe = new Labyrinthe("./test.txt");
                        chosen = 1;
                        break;
                    case 2:
                        Console.WriteLine("This game is made by Manasa and Maxime, IBO-2/ ESILV.");
                        //chosen = 1;
                        break;
                    case 3:
                        chosen = 1;
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Enter 1,2, or 3.");
                        break;
                }
            } while (chosen == 0);

            Menu m = new Menu();
            m.startTheShow();

            Console.ReadKey();
        }
    }
}
