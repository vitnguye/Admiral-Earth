using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BaiscAdEarth
{
    public class NodeManager
    {
        LinkedList<Node> Nodes;
        int nodesRemaining;
        public static SoundEffect upgradeSnd;
        public int cash{get; set;}
        
        //Creates four nodes in pre-defined places
        public NodeManager(Game game)
        {
            Nodes = new LinkedList<Node>();
            int xpos = 100;
            for (int count = 0; count < 6; count++)
            {
                Node temp = new Node(xpos, 100, game);
                Nodes.AddLast(temp);
                xpos += 100;
            }
            nodesRemaining = 6;
        }

        //Create four nodes in chosen locations
        public NodeManager(int x1, int y1, int x2, int x3, int x4, int x5, int x6, Game game)
        {
            Nodes = new LinkedList<Node>();
            Node temp1 = new Node(x1, y1, game);
            Nodes.AddLast(temp1);
            Node temp2 = new Node(x2, y1, game);
            Nodes.AddLast(temp2);
            Node temp3 = new Node(x3, y1, game);
            Nodes.AddLast(temp3);
            Node temp4 = new Node(x4, y1, game);
            Nodes.AddLast(temp4);
            Node temp5 = new Node(x5, y1, game); 
            Nodes.AddLast(temp5);
            Node temp6 = new Node(x6, y1, game); 
            Nodes.AddLast(temp6);
            nodesRemaining = 6;
            upgradeSnd = game.Content.Load<SoundEffect>(@"Sounds/upgrade");
            cash = 1000;
            //I've decided to try 6 nodes -Vic
        }

        //public int nodenum()
        //{
        
        //}

        //Updates the nodes to check for collisions
        public void update(EnemyManager director, Player p)
        {
            LinkedList<Node> Removals = new LinkedList<Node>();
            
            foreach (Node curr in Nodes) 
            {
                if (curr.updateCollision(director))
                {
                    //Destroy the node
                    Removals.AddFirst(curr);
                    nodesRemaining--;
                }

                curr.updateUpgrades(director, p, this);
            }

            foreach (Node curr in Removals)
            {
                Nodes.Remove(curr);
            }
        }

        public bool isGameOver()
        {
            if (nodesRemaining <= 0) return true;
            return false;
        }

        public bool isClicked(MouseState state, Game game)
        {
            foreach (Node curr in Nodes)
            {
                //If click is on Node
                if (curr.imageBox.Contains(state.X, state.Y))
                {
                    //Go through nodes and deactivate them
                    foreach (Node incurr in Nodes)
                    {
                        incurr.isActive = false;
                    }
                    
                    //Activate the clicked node
                    curr.isActive = true;
                    return true;
                }

                // Check to see buttons are clicked
                if (curr.isActive)
                {
                    Rectangle ORec = new Rectangle((int)curr.OPos.X, (int)curr.OPos.Y, curr.OButton.Width, curr.OButton.Height);
                    Rectangle DRec = new Rectangle((int)curr.DPos.X, (int)curr.DPos.Y, curr.DButton.Width, curr.DButton.Height);
                    Rectangle RRec = new Rectangle((int)curr.RPos.X, (int)curr.RPos.Y, curr.RButton.Width, curr.RButton.Height);

                    if (ORec.Contains(state.X, state.Y))
                    {
                        //Add Offense Tower if not already there
                        if (curr.numberUpgrades < 3 && !curr.hasO && cash >= 1000)
                        {
                            curr.Trait1 = new Offense(curr, game);
                            curr.numberUpgrades++;
                            curr.hasO = true;
                            cash -= 1000;
                            upgradeSnd.Play(1.0f, 0f, 0f);
                        }
                        //else if (cash >= 1000)
                        //{
                        //    cash -= 1000;
                        //    upgradeSnd.Play(1.0f, 0f, 0f);
                           
                        //}
                        
                        return true;
                    }

                    if (DRec.Contains(state.X, state.Y))
                    {
                        //Add Defense Tower if not already there
                        if (curr.numberUpgrades < 3 && !curr.hasD && cash >= 600)
                        {
                            curr.Trait2 = new Defense(curr, game);
                            curr.numberUpgrades++;
                            curr.hasD = true;
                            upgradeSnd.Play(1.0f, 0f, 0f);
                            cash -= 600;
                        }
                        
                        return true;
                    }

                    if (RRec.Contains(state.X, state.Y))
                    {
                        //Add Resource Tower if not already there
                        if (curr.numberUpgrades < 3 && !curr.hasP && cash >= 300)
                        {
                            curr.Trait3 = new Production(curr, game);
                            curr.numberUpgrades++;
                            curr.hasP = true;
                            upgradeSnd.Play(1.0f, 0f, 0f);
                            cash -= 300;
                            
                        }
                        //else if (cash >= 1000)
                        //{
                        //    cash -= 300;
                        //    upgradeSnd.Play(1.0f, 0f, 0f);                      
                        //}
                        return true;
                        
                    }
                }
                
            }
            return false;
        }

        public void draw(SpriteBatch sp, Game game, SpriteFont sf)
        {
            foreach (Node curr in Nodes)
            {
                curr.draw(sp, game, sf);
                curr.drawUpgrades(sp, game);
            }
        }
    }
}
