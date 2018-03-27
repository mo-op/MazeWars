using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Labyrinthe
{
    class Combattant
    {
        int x = 0;
        int y = 0;
        int health = 100;
        public Stack<Case> path = new Stack<Case>();
        int id;

        public List<Item> listItem = new List<Item>();
        public List<Case> visitedCase = new List<Case>();

        public Combattant(int _x, int _y, int _id)
        {
            x = _x;
            y = _y;
            id = _id;
        }

        public int getId()
        {
            return id;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public int getHealth()
        {
            return health;
        }

        public void setX(int _x)
        {
            x = _x;
        }

        public void setY(int _y)
        {
            y = _y;
        }

        public void setHealth(int _health)
        {
            health = _health;
        }

        public bool check(Case toAnalyse)
        {
            for(int i = 0; i < visitedCase.Count(); i++)
            {
                if (visitedCase[i].getX() == toAnalyse.getX() && visitedCase[i].getY() == toAnalyse.getY())
                {
                    return true;
                }
            }
            return false;
        }

        public void attack(Combattant opponant, Item damage)
        {
            opponant.setHealth(opponant.getHealth() - damage.getDamage());
        }

        public void run(Labyrinthe lab)
        {

            path.Push(new Case(x, y));

            while (lab.winner == 0)
            {
                               
                Thread.Sleep(1500);

                Boolean visited = false;

                //We add the case if it has not been visited
                for (int i = 0; i < visitedCase.Count(); i++)
                {
                    if (visitedCase[i].getX() == x && visitedCase[i].getY() == y)
                    {
                        visited = true;
                    }
                }

                if (visited == false)
                {
                    path.Push(new Case(x, y));
                }
                    

                    //If it has not we add it to the list
                    if (visited == false)
                    {
                        visitedCase.Add(new Case(x, y));
                    }

                    //Determine where he can go
                    List<Case> possibility = new List<Case>();

                    //check north, south, east and west

                    //North
                    if (y + 1 < lab.getWeight())
                    {
                    if(check(new Case(x, y + 1)) != true)
                    {
                        if (lab.getCase(x, y + 1) != null)
                        {
                            if (lab.getCase(x, y + 1).GetType().ToString() == "Labyrinthe.Exit")
                            {
                                lock (lab)
                                {
                                    lab.winner = id;
                                    Console.Write(id);
                                }
                            }
                            if (lab.getCase(x, y + 1).GetType().ToString() != "Labyrinthe.Wall")
                            {
                                possibility.Add(new Case(x, y + 1));
                            }
                        }
                        else
                        {
                            possibility.Add(new Case(x, y + 1));
                        }
                    }
                    }


                //South
                if (y - 1 < lab.getWeight() && y - 1 > 0)
                {
                    if (check(new Case(x, y - 1)) != true)
                    {
                        if (lab.getCase(x, y - 1) != null)
                        {
                            if (lab.getCase(x, y - 1).GetType().ToString() == "Labyrinthe.Exit")
                            {
                                lock (lab)
                                {
                                    lab.winner = id;
                                }
                            }
                            if (lab.getCase(x, y - 1).GetType().ToString() != "Labyrinthe.Wall")
                            {
                                possibility.Add(new Case(x, y - 1));
                            }
                        }
                        else
                        {
                            possibility.Add(new Case(x, y - 1));
                        }
                    }
                }

                //East
                if (x + 1 < lab.getWeight())
                {
                    if (check(new Case(x + 1, y)) != true)
                    {
                        if (lab.getCase(x + 1, y) != null)
                        {
                            if (lab.getCase(x + 1, y).GetType().ToString() == "Labyrinthe.Exit")
                            {
                                lock (lab)
                                {
                                    lab.winner = id;
                                }
                            }
                            if (lab.getCase(x + 1, y).GetType().ToString() != "Labyrinthe.Wall")
                            {
                                possibility.Add(new Case(x + 1, y));
                            }
                        }
                        else
                        {
                            possibility.Add(new Case(x + 1, y));
                        }
                    }
                }

                    //West
                    if (x - 1 < lab.getWeight() && x - 1 > 0)
                    {
                    if (check(new Case(x - 1, y)) != true)
                    {
                        if (lab.getCase(x - 1, y) != null)
                        {
                            if (lab.getCase(x - 1, y).GetType().ToString() == "Labyrinthe.Exit")
                            {
                                lock (lab)
                                {
                                    lab.winner = id;
                                }
                                
                            }
                            if (lab.getCase(x - 1, y).GetType().ToString() != "Labyrinthe.Wall")
                            {
                                possibility.Add(new Case(x - 1, y));
                            }
                        }
                        else
                        {
                            possibility.Add(new Case(x - 1, y));
                        }
                    }
                }
                
                //He plays
                lock (lab)
                {

                    if (possibility.Count() != 0)
                    {
                        Random r = new Random();
                        int move = r.Next(0, possibility.Count());


                        //If the target case is empty the combattant move to this case
                        if (lab.getCase(possibility[move].getX(), possibility[move].getY()) == null)
                        {
                            lab.setCase(x, y, null);

                            x = possibility[move].getX();
                            y = possibility[move].getY();

                            lab.setCase(x, y, this);
                        }
                        else
                        {
                            switch (lab.getCase(possibility[move].getX(), possibility[move].getY()).GetType().ToString())
                            {
                                case "Labyrinthe.Item":
                                    listItem.Add((Item)lab.getCase(possibility[move].getX(), possibility[move].getY()));

                                    lab.setCase(x, y, null);

                                    x = possibility[move].getX();
                                    y = possibility[move].getY();

                                    lab.setCase(x, y, this);

                                    break;

                                case "Labyrinthe.Combattant":
                                    if (listItem.Count > 0)
                                    {
                                        Combattant opponant = (Combattant)lab.getCase(possibility[move].getX(), possibility[move].getY());
                                        //We search the best item
                                        Item maxDamage = null;

                                        int indice = 0;
                                        for (int i = 0; i < listItem.Count; i++)
                                        {
                                            if (maxDamage == null)
                                            {
                                                maxDamage = listItem[i];
                                                indice = i;
                                            }
                                            else
                                            {
                                                if (maxDamage.getDamage() < listItem[i].getDamage())
                                                    
                                                maxDamage = listItem[i];
                                                indice = i;
                                            }
                                        }
                                        this.attack(opponant, maxDamage);

                                        maxDamage.setDamage();

                                        if (maxDamage.getDamage() == 0)
                                        {
                                            listItem.RemoveAt(indice);
                                        }
                                    }
                                    else
                                    {
                                        lab.setCase(x, y, null);

                                        x = possibility[move].getX();
                                        y = possibility[move].getY();

                                        lab.setCase(x, y, this);
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        
                        lab.setCase(x, y, null);

                        

                        x = path.Peek().getX();
                        y = path.Peek().getY();

                        

                        path.Pop();

                        lab.setCase(x, y, this);
                    }
                    lab.display();
                }
            }
        }
    }
}
