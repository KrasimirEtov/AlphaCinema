﻿using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class EnterUser : DisplayBaseCommand
    {
        private IUserServices userServices;
        private IProjectionsServices projectionsServices;
        private IWatchedMovieServices watchedMovieServices;

        public EnterUser(ICommandProcessor commandProcessor, 
            IItemSelector selector, 
            IUserServices userServices, 
            IProjectionsServices projectionsServices,
            IWatchedMovieServices watchedMovieServices) : base(commandProcessor, selector)
        {
            this.userServices = userServices;
            this.projectionsServices = projectionsServices;
            this.watchedMovieServices = watchedMovieServices;
        }

        public override void Execute(List<string> parameters)
        {
            int offSetFromTop = int.Parse(parameters[parameters.Count - 2]);
            int startingRow = int.Parse(parameters[parameters.Count - 1]);
            string cityID = parameters[7];
            string movieID = parameters[3];
            string openHourID = parameters[1];
            List<string> displayItems = new List<string>
            {
                parameters[0],
                "Retry",
                "Back",
                "Home"
            };
            string enterUsername = "Format: Username(50) | Age";
            selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
            selector.PrintAtPosition(enterUsername, startingRow * 1 + offSetFromTop, false);
            string user = selector.ReadAtPosition(startingRow * 2 + offSetFromTop, enterUsername, false, 70);
            displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());
            string[] userDetails = user.Split('|');
            try
            {
                if (userDetails.Length != 2)
                {
                    throw new ArgumentException("Please enter valid count of required parameters");
                }

                string userName = userDetails[0];

                if (!int.TryParse(userDetails[1].Trim(), out int userAge))
                {
                    throw new ArgumentException("User Age must be integer number");
                }
                selector.PrintAtPosition(new string(' ', enterUsername.Length), startingRow * 1 + offSetFromTop, false);


                string newUserId = userServices.AddNewUser(userName, userAge).Id.ToString();
                //Избираме резержация на база на Града, Филма и Часа
                string reservationId = projectionsServices.GetID(cityID, movieID, openHourID).ToString();
                watchedMovieServices.AddNewWatchedMovie(newUserId, reservationId);

                string successfullyAdded = $"Your Reservation for {openHourID} has been Added";
                selector.PrintAtPosition(successfullyAdded, startingRow * 1 + offSetFromTop, false);
                Thread.Sleep(2000);
                selector.PrintAtPosition(new string(' ', successfullyAdded.Length), startingRow * 1 + offSetFromTop, false);
                commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    string wrongParametersDetals = ex.Message;
                    selector.PrintAtPosition(new string(' ', enterUsername.Length), startingRow * 1 + offSetFromTop, false);
                    selector.PrintAtPosition(wrongParametersDetals, startingRow * 4 + offSetFromTop, false);
                    string selected = selector.DisplayItems(displayItems);
                    selector.PrintAtPosition(new string(' ', wrongParametersDetals.Length), startingRow * 4 + offSetFromTop, false);
                    if (selected == "Retry")
                    {
                        commandProcessor.ExecuteCommand(parameters);
                    }
                    else if (selected == "Back")
                    {
                        commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
                    }
                    else if (selected == "Home")
                    {
                        commandProcessor.ExecuteCommand(parameters.Skip(9).ToList());
                    }
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}