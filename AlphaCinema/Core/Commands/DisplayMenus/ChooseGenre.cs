﻿using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
	public class ChooseGenre : DisplayBaseCommand
	{
		private readonly IGenreServices genreServices;
		private readonly ICityServices cityServices;

		public ChooseGenre(ICommandProcessor commandProcessor, IItemSelector selector,
			IGenreServices genreServices, ICityServices cityServices)
			: base(commandProcessor, selector)
		{
			this.genreServices = genreServices;
			this.cityServices = cityServices;

		}

		public override void Execute(List<string> parameters)
		{
			string offSetFromTop = parameters[parameters.Count - 2];
			string startingRow = parameters[parameters.Count - 1];
			int cityID = int.Parse(parameters[1]);
			//var genreNames = this.genreServices.GetGenreNames(cityId);
			// Избираме Жанр на база на града
			var genreNames = this.cityServices.GetGenreNames(cityID);
			List<string> displayItems = new List<string>() { "Choose Genre" };

			displayItems.AddRange(genreNames);
			displayItems.Add("Back");
			displayItems.Add("Home");
			displayItems.Add(offSetFromTop);
			displayItems.Add(startingRow);

			string genreName = selector.DisplayItems(displayItems);
			if (genreName == "Back")
			{
				commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
			}
			//Изтриваме командата ChoooseMovie и извикваме отново предното menu
			else if (genreName == "Home")
			{
				commandProcessor.ExecuteCommand(parameters.Skip(3).ToList());
			}
			else
			{
				var genreID = this.genreServices.GetID(genreName);
				parameters.Insert(0, genreID.ToString());
				parameters.Insert(0, "ChooseMovie");
				commandProcessor.ExecuteCommand(parameters);
			}
		}
	}
}