﻿namespace AlphaCinema.Core.Contracts
{
	public interface IAlphaCinemaConsole
	{
		string ReadLine();
		void Write(string message);
		void WriteLine(string message);
		void Clear();
		void ReadKey();
	}
}