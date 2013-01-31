/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/3/2013
 * Time: 9:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Ants
{
	class World
	{
		public static List<Worldable> stuff = new List<Worldable>();
		public static List<Food> foodz = new List<Food>();
		
		public static char[,] m_world;
		private int width, height;
		private int num_ants;
		private int num_food;
		
		public AntHill anthill, anthill2;
		
		public World(int w, int h)
		{
			width = w;
			height = h;
			m_world = new char[w,h];
			
			num_food = 1000;
			init();
		}
		
		private void init()
		{
			anthill = new AntHill(10,10);
			stuff.Add(anthill);
			anthill2 = new AntHill(width-1, height-1);
			stuff.Add(anthill2);

			anthill.enemyHill = anthill2;
			anthill2.enemyHill = anthill;
			
			Random joe = new Random();
			for(int i = 0; i < width; i++)
			{
				for(int j = 0; j < height; j++)
				{
					m_world[i,j] = '_';
				}
			}
			
			int x,y;
			
			for(int i = 0; i < num_food; i++)
			{
				x = joe.Next()%width;
				y = joe.Next()%height;
				foodz.Add(new Food(x, y));
				//stuff.Add(new Food(x, y));
			}
		}
		
		public void run()
		{
			Random joe = new Random();
			int counter = 0;
			while(foodz.Count > 0)
			{
				if(counter%20 == 0)
				{
					int x, y;
					x = joe.Next()%width;
					y = joe.Next()%height;
					foodz.Add(new Food(x, y));
					//stuff.Add(new Food(x, y));
				}
				
				anthill.run();
				anthill2.run();
				printWorld();
				System.Threading.Thread.Sleep(1000);
				counter++;
			}
		}
		
		private void updateGrid(List<Worldable> s)
		{
			int x, y;
			
			foreach(Worldable w in s)
			{
				x = w.getX();
				y = w.getY();
				//TODO clean up this bad code D :
				//My code is bad an i should feel bad
				if(w.type == "Food")
				{
					//m_world[x,y] = '^';
				}
				else if(w.type == "Anthill")
				{
					m_world[x,y] = 'H';
				}
				else if(w.type == "Ant")
				{
					m_world[x,y] = 'A';
				}
				
				if(w.hasObjects)
				{
					updateGrid(w.getObjects());
				}
			}
		}
		
		private void constructGrid()
		{
			for(int i = 0; i < width; i++)
			{
				for(int j = 0; j < height; j++)
				{
					m_world[i, j] = ' ';
				}
			}
			
			int x,y;
			
			updateGrid(stuff);
			
			foreach(Food f in foodz)
			{
				x = f.getX();
				y = f.getY();
				
				m_world[x,y] = '^';
			}
		}
		
		public void printWorld()
		{
			Console.Clear();
			constructGrid();
			
			String sworld = "";
			
			Console.WriteLine(anthill.toString());
			Console.WriteLine(anthill2.toString());
			for(int i = 0; i < width; i++)
			{
				String line = "";
				for(int j = 0; j < height-1; j++)
				{
					line += m_world[i,j] + "";
				}
				sworld += line + m_world[i, height-1] + '\n';
				//Console.WriteLine(line + m_world[i,height-1]);
			}
			sworld += "\n";
			Console.WriteLine(sworld);
		}
	}
}
