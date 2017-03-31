using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using InterviewQuestion;

namespace SeatPickerTest
{
    [TestClass]
    public class SeatPickerTests
    {
        private List<bool> seats;
        private ISeatPicker seatPicker = new SeatPicker();

        [TestMethod]
        public void GetDeveloperName()
        {
            Assert.AreEqual("Nathan Plotts", seatPicker.DeveloperName);
        }

        [TestMethod]
        public void SingleSeatTotal_SingleOpen_Return1()
        {
            seats = new List<bool>(new bool[] { false });

            Assert.AreEqual(1, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SingleSeatTotal_SeatsFull_ThrowException()
        {
            seats = new List<bool>(new bool[] { true });

            seatPicker.GetSeat(seats);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MultipleSeatsTotal_SeatsFull_ThrowException()
        {
            seats = new List<bool>(new bool[] { true, true, true, true });

            seatPicker.GetSeat(seats);
        }

        [TestMethod]
        public void MultipleSeatsTotal_AllOpen_Return1()
        {
            seats = new List<bool>(new bool[] { false, false, false, false });

            Assert.AreEqual(1, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_AllTakenExceptFirst_Return1()
        {
            seats = new List<bool>(new bool[] { false, true, true, true, true, true });

            Assert.AreEqual(1, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_AllTakenExceptLast_Return7()
        {
            seats = new List<bool>(new bool[] { true, true, true, true, true, true, false });

            Assert.AreEqual(7, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_AllTakenExcept5AndLast_Return7()
        {
            seats = new List<bool>(new bool[] { true, true, true, true, false, true, false });

            Assert.AreEqual(7, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_FirstTaken_ReturnLast()
        {
            seats = new List<bool>(new bool[] { true, false, false, false, false });

            Assert.AreEqual(5, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void OddSeatsTotal_FirstLastTaken_ReturnMiddle()
        {
            seats = new List<bool>(new bool[] { true, false, false, false, true });

            Assert.AreEqual(3, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void EvenSeatsTotal_FirstLastTaken_ReturnRightOfMiddle()
        {
            seats = new List<bool>(new bool[] { true, false, false, false, false, true });

            Assert.AreEqual(4, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_5Taken_Return4()
        {
            seats = new List<bool>(new bool[] { true, true, false, false, false, true, false, true, true, false });

            Assert.AreEqual(4, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_EvenlySpaced_Return8()
        {
            seats = new List<bool>(new bool[] { true, false, true, false, true, false, true, false });

            Assert.AreEqual(8, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_2EmptyGroups_Return8()
        {
            seats = new List<bool>(new bool[] { true, false, false, false, true, false, false, false, false, true });

            Assert.AreEqual(8, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_2EmptyGroupsEndOpen_Return8()
        {
            seats = new List<bool>(new bool[] { true, false, false, false, true, false, false, false });

            Assert.AreEqual(8, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_OddFrontEvenBack_Return1()
        {
            seats = new List<bool>(new bool[] { false, false, false, true, false, false });

            Assert.AreEqual(1, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_EvenFrontEvenBack_Return1()
        {
            seats = new List<bool>(new bool[] { false, false, true, false });

            Assert.AreEqual(1, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_OddFrontOddBack_Return1()
        {
            seats = new List<bool>(new bool[] { false, true, false });

            Assert.AreEqual(1, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_OddFrontEvenBack_Return4()
        {
            seats = new List<bool>(new bool[] { false, true, false, false });

            Assert.AreEqual(4, seatPicker.GetSeat(seats));
        }

        [TestMethod]
        public void MultipleSeatsTotal_EvenFrontOddBack_Return6()
        {
            seats = new List<bool>(new bool[] { false, false, true, false, false, false });

            Assert.AreEqual(6, seatPicker.GetSeat(seats));
        }
    }
}
