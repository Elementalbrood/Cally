/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/5/2013
 * Time: 3:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Ants
{
	public class Driver
	{
		Drawer bob;
		World antWorld;
		
		public Driver()
		{
			antWorld = new World(60, 60);
			//bob = new Drawer(antWorld.m_world);
		}
		
		public void run()
		{
			while(true)
			{
				antWorld.run();
				bob.draw();
			}
		}
	}
}
