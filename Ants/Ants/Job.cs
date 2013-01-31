/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/3/2013
 * Time: 9:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Ants
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
}
