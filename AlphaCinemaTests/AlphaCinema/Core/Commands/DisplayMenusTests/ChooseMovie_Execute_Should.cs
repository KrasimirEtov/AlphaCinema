﻿using AlphaCinema.Core.Commands.DisplayMenus;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.DisplayMenusTests
{
    [TestClass]
    public class ChooseMovie_Execute_Should
    {
        [TestMethod]
        [DataRow("Menu 1 BuyTickets 5 LogAsAdmin ShowInfo PdfExport Exit", "Titanic", "ChooseHour")]
        public void ReturnCorrectList_WhenAMovieIsSelected(string input, string selectorResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var itemSelectorMock = new Mock<IItemSelector>();
            itemSelectorMock.Setup(itemSelector => itemSelector.DisplayItems(It.IsAny<List<string>>())).Returns(selectorResult);

            var movieServicesMock = new Mock<IMovieServices>();
            movieServicesMock.Setup(movieServices => movieServices.GetID("Titanic")).Returns(1);
            movieServicesMock.Setup(movieServices => movieServices.GetMovieNamesByCityIDGenreID(1, 5)).Returns(new List<string>());

            //Act
            var chooseMovie = new ChooseMovie(itemSelectorMock.Object, movieServicesMock.Object);
            var result = chooseMovie.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu 1 BuyTickets 5 LogAsAdmin ShowInfo PdfExport Exit", "Back", "BuyTickets")]
        public void ReturnCorrectList_WhenBackIsSelected(string input, string selectorResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var itemSelectorMock = new Mock<IItemSelector>();
            itemSelectorMock.Setup(itemSelector => itemSelector.DisplayItems(It.IsAny<List<string>>()))
                .Returns(selectorResult);

            var movieServicesMock = new Mock<IMovieServices>();
            movieServicesMock.Setup(movieServices => movieServices.GetID("Titanic")).Returns(1);
            movieServicesMock
                .Setup(movieServices => movieServices.GetMovieNamesByCityIDGenreID(1, 5))
                .Returns(new List<string>());

            //Act
            var chooseMovie = new ChooseMovie(itemSelectorMock.Object, movieServicesMock.Object);
            var result = chooseMovie.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu 1 ChooseHour 5 LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "Home", "ShowInfo")]
        public void ReturnCorrectList_WhenHomeIsSelected(string input, string selectorResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var itemSelectorMock = new Mock<IItemSelector>();
            itemSelectorMock.Setup(itemSelector => itemSelector.DisplayItems(It.IsAny<List<string>>())).Returns(selectorResult);

            var movieServicesMock = new Mock<IMovieServices>();
            movieServicesMock.Setup(movieServices => movieServices.GetID("Titanic")).Returns(1);
            movieServicesMock.Setup(movieServices => movieServices.GetMovieNamesByCityIDGenreID(1, 5)).Returns(new List<string>());

            //Act
            var chooseMovie = new ChooseMovie(itemSelectorMock.Object, movieServicesMock.Object);
            var result = chooseMovie.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }
    }
}
