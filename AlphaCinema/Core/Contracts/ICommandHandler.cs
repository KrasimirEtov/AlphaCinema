﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinema.Core.Contracts
{
	public interface ICommandHandler
	{
		List<string> Input();
	}
}
