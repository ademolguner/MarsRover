using Microsoft.Extensions.DependencyInjection;
using StudyCase.Entities;
using StudyCase.Enums;
using StudyCase.Interfaces;
using StudyCase.Managers;
using System;

namespace StudyCase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region  Service provider Dependecy Injection 

            var serviceProvider = new ServiceCollection()
               .AddSingleton<IRover, MarsRover>()
               .AddSingleton<IRoverManager, MarsRoverManager>()
               .BuildServiceProvider();

            #endregion


            var plateau = new Plateau(5, 5);
            var location = new Location(1, 2);

            var rover = serviceProvider.GetService<IRover>();
            if (rover != null)
            {
                rover.SetPlateau(plateau);
                rover.SetLocation(location, Rotation.N);

                var roverManager = serviceProvider.GetService<IRoverManager>();
                if (roverManager != null)
                {
                    roverManager.ExecuteCommand("LMLMLMLMM");
                    Console.WriteLine(roverManager.GetStatusText());

                    rover.SetLocation(3, 3, Rotation.E);
                    roverManager.ExecuteCommand("MMRMMRMRRM");
                    Console.WriteLine(roverManager.GetStatusText());
                }
            }

            Console.ReadKey();
        }
    }
}
