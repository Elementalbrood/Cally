/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/4/2013
 * Time: 11:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace testing
{
	class Job
	{
		private string name = "";
		private int time;
		
		public Job()
		{
			name = "Idle";
		}
		
		public Job(string j)
		{
			name = j;
			setTime();
		}
			
		public string toString()
		{
			string message = "";
			message += name;
			return message;
		}
		
		public void setTime()
		{
			if(name == "Food")
			{
				time = 20;
			}
			else if(name == "Water")
			{
				time = 10;
			}
			else if(name == "Sticks")
			{
				time = 100;
			}
		}
		
		public string getName()
		{
			return name;
		}
		
		public int getTime()
		{
			return time;
		}
	}
	
	class Worldable
	{
		private int x, y;
		public Worldable(int v, int w)
		{
			x = v;
			y = w;
		}
		
		public int getX()
		{
			return x;
		}
		
		public int getY()
		{
			return y;
		}
		
		public void incX(int i)
		{
			x += i;
		}
		
		public void incY(int i)
		{
			y += i;
		}
		
		protected void moveTo(int new_x, int new_y)
		{
			if(new_x < getX())
			{
				incX(-1);
			}
			else if(new_x > getX())
			{
				incX(1);
			}
			
			if(new_y < getY())
			{
				incY(-1);
			}
			else if(new_y > getY())
			{
				incY(1);
			}
		}	
		
		protected bool atLoc(int new_x, int new_y)
		{
			if(new_x == getX() && new_y == getY())
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public static double distance(int x1, int y1, int x2, int y2)
		{
			return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
		}
	}
	
	class Food : Worldable
	{
		private int food_amount;
		
		public Food(int x, int y) : base(x, y)
		{
			food_amount = 50;	
		}
		
		public Food(int v, int w, int f) : base(v, w)
		{
			food_amount = f;
		}
		
		public int getFoodAmount()
		{
			return food_amount;
		}
		
		public void setFoodAmount(int i)
		{
			food_amount = i;
		}
		
		public void incFoodAmount(int i)
		{
			food_amount += i;
		}
		
		public void gatherFood(int x)
		{
			if(food_amount - x < 0)
			{
				food_amount = 0;
			}
			if(food_amount > 0)
			{
				food_amount -= x;
			}
		}
		
		public string toString()
		{
			String message = "";
			
			message += getX() + ", " + getY() + " food amount: " + food_amount;
			
			return message;
		}
	}
	
	class AntHill : Worldable
	{
		public List<Ant> antz = new List<Ant>();
		
		private int food_storage;
		private static int init_id = 0;
		private int anthill_id = 0;
		private int num_food_ants = 4;
		private int num_ants;
		int food_counter = 10, max_count = 10;
		
		public AntHill(int x, int y) : base(x, y)
		{
			food_storage = 150;
			anthill_id = init_id++;
		}
		
		public void incFood(int i)
		{
			food_storage += i;
		}
		
		public int getFood()
		{
			return food_storage;
		}
		
		public void run()
		{
			foreach(Ant ant in antz)
			{
				ant.run();
				foreach(Food f in World.foodz)
				{
					if(f.getFoodAmount() == 0)
					{
						if(World.foodz.Remove(f));
						break;
					}
				}
			}
			
			if(food_storage >= 10)
			{
				food_storage -= 10;
				num_food_ants = num_ants++;
				if(num_ants > antz.Count)
				{
					antz.Add(new Ant(getX(), getY(), this));
				}
			}
		}
		
		public int getNumFoodAnts()
		{
			return num_food_ants;
		}
		
		public string toString()
		{
			string message = "";
			
			message += "AntHill: " + " F: " + food_storage + ", " + num_ants;
			
			return message;
		}
	}
	
	class Ant : Worldable
	{
		private int food_capacity = 5;
		
		bool hasFood;
		string status;
		
		private static int init_id = 0;
		private int ant_id = 0;
		
		private Job current_job;
		private AntHill anthill;
		
		private Food nearestFood;
		
		public Ant(int x, int y, AntHill a) : base(x,y)
		{
			hasFood = false;
			status = "";
			anthill = a;
			
			nearestFood = null;
			
			current_job = new Job();
		}
		
		public void run()
		{
			if(current_job.getName() == "Idle")
			{
				current_job = assignJob();
			}
			doJob();
		}
		
		public Job getJob()
		{
			return current_job;
		}
		
		private Job assignJob()
		{
			Job temp = new Job();
			int food_ants = 0;
			int water_ants = 0;
			int stick_ants = 0;
			int idle_ants = 0;
			
			foreach(Ant bob in anthill.antz)
			{
				if(bob.getJob().getName() == "Idle")
				{
					idle_ants++;
				}
				else if(bob.getJob().getName() == "Food")
				{
					food_ants++;
				}
				else if(bob.getJob().getName() == "Water")
				{
					water_ants++;
				}
				else if(bob.getJob().getName() == "Sticks")
				{
					stick_ants++;
				}
			}
			
			if(food_ants < anthill.getNumFoodAnts())
			{
				temp = new Job("Food");
			}
			
			return temp;
		}
		
		private void doJob()
		{
			if(current_job.getName() == "Food")
			{
				if(hasFood)
				{
					moveTo(anthill.getX(), anthill.getY());
					status = "Moving to anthill";
					if(atLoc(anthill.getX(), anthill.getY()))
					{
						hasFood = false;
						anthill.incFood(food_capacity);
						current_job = new Job();
						status = "";
					}
				}
				else
				{
					findnearestfood();
					status = "Moving to nearest food";
					nearestFood = findnearestfood();
					moveTo(nearestFood.getX(), nearestFood.getY());
				}
				if(atLoc(nearestFood.getX(), nearestFood.getY()) && !hasFood)
				{
					gatherFood();
				}
			}
		}
		
		//TODO make it so ants dont have infina vision
		private Food findnearestfood()
		{
			double dist = 0;
			double min_dist = 100000;
			Food close = World.foodz[0];
			foreach(Food f in World.foodz)
			{
				dist = Worldable.distance(getX(), getY(), f.getX(), f.getY());
				if(dist < min_dist)
				{
					min_dist = dist;
					close = f;
				}
			}
			return close;
		}
		
		private void gatherFood()
		{
			nearestFood.gatherFood(food_capacity);
			status = "Gathered food";
			hasFood = true;
		}
	}
	
	class World
	{
		
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
			
			num_food = 50;
			init();
		}
		
		private void init()
		{
			anthill = new AntHill(0,0);
			anthill2 = new AntHill(width-1, height-1);
			Random joe = new Random();
			for(int i = 0; i < width; i++)
			{
				for(int j = 0; j < height; j++)
				{
					m_world[i,j] = '_';
				}
			}
			
			for(int i = 0; i < num_food; i++)
			{
				foodz.Add(new Food(joe.Next()%width, joe.Next()%height));
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
					foodz.Add(new Food(joe.Next()%width, joe.Next()%height));
				}
				
				anthill.run();
				anthill2.run();
				printWorld();
				System.Threading.Thread.Sleep(1000);
				counter++;
			}
		}
		
		
		private void constructGrid()
		{
			for(int i = 0; i < width; i++)
			{
				for(int j = 0; j < height; j++)
				{
					m_world[i, j] = '_';
				}
			}
			
			int x,y;
			
			foreach(Food f in foodz)
			{
				x = f.getX();
				y = f.getY();
				m_world[x,y] = '^';
			}
			
			foreach(Ant a in anthill.antz)
			{
				x = a.getX();
				y = a.getY();
				m_world[x,y] = 'A';
			}
			
			foreach(Ant a in anthill2.antz)
			{
				x = a.getX();
				y = a.getY();
				m_world[x, y] = 'B';
			}
			
			m_world[0,0] = 'H';
			m_world[width-1, height-1] = 'H';
		}
		
		public void printWorld()
		{
			Console.Clear();
			constructGrid();
			
			String sworld = "";
			
			Console.WriteLine(anthill.toString());
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
			//Console.WriteLine("");
			Console.WriteLine(sworld);

		}
	}
	
	class Program
	{
		public static void Main(string[] args)
		{
			World bob = new World(60, 60);
			bob.run();
			
			// TODO: Implement Functionality Here
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}