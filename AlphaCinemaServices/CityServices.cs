﻿using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaServices
{
	public class CityServices : ICityServices
	{
		private readonly IAlphaCinemaContext context;

		public CityServices(IAlphaCinemaContext context)
		{
			this.context = context;
		}

		public List<Guid> GetIDs()
		{
			var cityIDs = context
				.Cities
				.Select(c => c.Id)
				.ToList();

			return cityIDs;
		}

		public List<string> GetCityNames(List<Guid> cityIDs)
		{
			var cityNames = new List<string>();

			cityIDs.ForEach(id =>
			{
				var result = context.Cities
				.Where(c => c.Id == id)
				.Select(c => c.Name)
				.ToList();

				cityNames.Add(result[0]);
			});

			return cityNames;
		}
	}
}
