using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class T1_UserInterface_Button
    {
        private UserInterface _uut;
        private Button _powerButton;
        private Button _startCancelButton;
        private Button _timeButton;
        private IDoor _door;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;
       

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();

            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }


        [Test]
        public void OnPowerPressed_starts_powerLevel_50()
        {
            _powerButton.Press();
            _display.Received().ShowPower(50);

        }

        [TestCase(1, 50)]
        [TestCase(2, 100)]
        [TestCase(3, 150)]
        [TestCase(4, 200)]
        [TestCase(5, 250)]
        [TestCase(10, 500)]
        [TestCase(14, 700)]
        [TestCase(16, 50)]
        public void UserInterface_OnPowerPressed_PowerIsCorrect(int timespressed, int expected)
        {
            //Act
            //Each time the powerbutton is pressed from 0 times to 16 times, _powerButton shall be pressed.
            for (int i = 0; i < timespressed; i++)
            {
                _powerButton.Press();
            }

            //Assert
            _display.Received().ShowPower(expected);
        }


        [Test]
        public void OnTimePressed_starts_time_1()
        {
            //act
            _powerButton.Press();
            _timeButton.Press();
            //Assert
            _display.Received().ShowTime(1, 0);
        }


        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(50, 50)]
        public void UserInterface_OnTimePressed_TimeIsCorrect(int timespressed, int expected)
        {
            //Act
            _powerButton.Press();
            for (int i = 0; i < timespressed; i++)
            {
                _timeButton.Press();
            }

            //Assert
            _display.Received().ShowTime(expected, 0);
        }

        [Test]
        public void OnStartCancelPressed_LightOff()
        {
            //Act
            _powerButton.Press();
            _startCancelButton.Press();

            //Assert
            _light.Received().TurnOff();
        }


        [Test]
        public void OnStartCancelPressed_DisplayClear()
        {
            //Act
            _powerButton.Press();
            _startCancelButton.Press();

            //Assert
            _display.Received().Clear();
        }

        [Test]
        public void OnStartCancelPressed_LightOn()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _light.Received().TurnOn();
        }

        [Test]
        public void OnStartCancelPressed_StartCooking()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _cookController.Received().StartCooking(50, 60);
        }

        [Test]
        public void OnStartCancelPressed_StopCooking()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            //start
            _startCancelButton.Press();
            //stop
            _startCancelButton.Press();

            //Assert
            _cookController.Received().Stop();
        }
    }
}
