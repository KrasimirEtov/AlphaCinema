﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
	public interface ICityServices
	{
		List<Guid> GetIDs();
		List<string> GetCityNames(List<Guid> cityIDs);
	}
}
