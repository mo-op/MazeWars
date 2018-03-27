using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinthe
{
    class Labyrinthe
    {

        public List<Combattant> listCombattant = new List<Combattant>();
        public List<Thread> threadCombattant = new List<Thread>();
        Object[,] board = null;
        int lenght = 0;
        int weight = 0;
        public int winner = 0;
        bool exit = false;

        public Labyrinthe(string filePath)
        {
            string line;
            int freecase = 0;

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);

            String[] size = file.ReadLine().Split(' ');

            this.lenght = int.Parse(size[0]);
            this.weight = int.Parse(size[1]);

            //creation of the matrix
            this.board = new Object[lenght, weight];
            int i = 0;

            //filling of the matrix
            while ((line = file.ReadLine()) != null)
            {
                    for (int d = 0; d < weight; d++)
                    {   
                        switch (line[d])
                        {
                            case '0':
                                this.board[i, d] = null;
                                freecase++;
                                break;
                            case '1':
                                this.board[i, d] = factory.getWall();
                                break;

                            case '2':
                                this.board[i, d] = factory.getExit();
                                break;
                        }
                    }
                i += 1;
            }

            file.Close();

            //Determine the number of items
            int nbItem = freecase / 10;
            for (int d = 0; d < nbItem; d++)
            {
                factory.getItem(this);
            }
            
            //Determine the number of combattant
            int nbCombattant = freecase / 100;
            if(nbCombattant < 2)
            {
                nbCombattant = 2;
            }

            for(int d = 0; d < nbCombattant; d++)
            {
                Combattant a = factory.getCombattant(this, d + 1);        
                this.listCombattant.Add(a);
                Thread th = new Thread(() => a.run(this));
                threadCombattant.Add(th);
                th.Start();
            }
            
            Thread timer = new Thread(this.timer);
            timer.Start();
            while(winner == 0)
            {
                
            }
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("this is the end the player : " + winner +" won the game\n\n");
        }

        public void display()
        {
            Console.Clear();

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Object:      |   Health:     |  Damage:     | Offensive: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(6, 0);
            Console.Write(listCombattant[0].listItem.Count);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(25, 0);
            Console.Write(listCombattant[0].getHealth());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(41, 0);
            int maxDamage = 0;

            for (int i = 0; i < listCombattant[0].listItem.Count; i++)
            {
                if (maxDamage < listCombattant[0].listItem[i].getDamage())
                    maxDamage = listCombattant[0].listItem[i].getDamage();
            }

            Console.Write(maxDamage);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(56, 0);
            bool offensive = true;
            if(listCombattant[0].listItem.Count == 0)
            {
                offensive = false;
            }
            Console.Write(offensive);
            Console.WriteLine("\n\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, 2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Object:      |   Health:     |  Damage:     | Offensive:  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(6, 2);
            Console.Write(listCombattant[2].listItem.Count);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(25, 2);
            Console.Write(listCombattant[2].getHealth());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(41, 2);
            maxDamage = 0;

            for (int i = 0; i < listCombattant[2].listItem.Count; i++)
            {
                if (maxDamage < listCombattant[2].listItem[i].getDamage())
                    maxDamage = listCombattant[2].listItem[i].getDamage();
            }
            Console.Write(maxDamage);
            Console.SetCursorPosition(56, 2);
            offensive = true;
            Console.ForegroundColor = ConsoleColor.Gray;
            if (listCombattant[2].listItem.Count == 0)
            {
                offensive = false;
            }
            Console.Write(offensive);
            Console.WriteLine("\n\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Items will be taken back every 5 seconds\n");

            for (int i = 0; i < lenght; i++)
            {
                for (int d = 0; d < weight; d++)
                {
                    
                    if (this.board[i, d] != null)
                    {
                        switch(this.board[i, d].GetType().ToString())
                        {
                            case "Labyrinthe.Wall":
                                Console.Write('#');
                                break;

                            case "Labyrinthe.Exit":
                                Console.Write('+');
                                break;

                            case "Labyrinthe.Combattant":
                                Console.Write('@');
                                break;

                            case "Labyrinthe.Item":
                                Console.Write('$');
                                break;
                        }  
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine("");
            }
        }

        public Object getCase(int x, int y)
        {
            return this.board[x, y];
        }

        public void setCase(int x, int y, Object a)
        {
            this.board[x, y] = a;
        }

        public int getLength()
        {
            return lenght;
        }

        public int  getWeight()
        {
            return weight;
        }

        public void timer()
        {
            while (winner == 0)
            {
                int x = 0;
                int y = 0;

                Thread.Sleep(5000);
                lock (this)
                {
                    for (int i = 0; i < listCombattant.Count; i++)
                    {
                        for (int d = 0; d < listCombattant[i].listItem.Count; d++)
                        {
                            Console.Write(listCombattant[i].listItem.Count);
                            Thread.Sleep(1000);
                            do
                            {
                                Random rnd = new Random();
                                x = rnd.Next(0, this.getLength());
                                y = rnd.Next(0, this.getWeight());
                            }
                            while (this.getCase(x, y) != null);
                            this.board[x, y] = listCombattant[i].listItem[d];
                            listCombattant[i].listItem.RemoveAt(d);

                        }
                    }
                }
            }
        }
    }
}
