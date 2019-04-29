using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class IT4_Door_UserInterface
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private IDoor _door;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;
        private UserInterface _uut;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _door = new Door();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();

            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light,
                _cookController);
        }

        [Test]
        public void UserInterface_DoorOpen_LightOn()
        {

            _door.Open();


            _light.Received().TurnOn();
        }

        [Test]
        public void UserInterface_DoorClose_LightOff()
        {

            _door.Open();
            _door.Close();


            _light.Received().TurnOff();

        }

        [Test]
        public void UserInterface_DoorOpenPowersetup_LightOn()
        {

            _powerButton.Press();
            _door.Open();


            _light.Received().TurnOn();
        }
        [Test]
        public void UserInterface_DoorOpenPowersetup_DisplayClear()
        {

            _powerButton.Press();
            _door.Open();


            _display.Received().Clear();
        }
        [Test]
        public void UserInterface_DoorOpenTimesetup_DisplayClear()
        {

            _powerButton.Press();
            _timeButton.Press();
            _door.Open();


            _display.Received().Clear();
        }

        [Test]
        public void UserInterface_DoorOpenCooking_PowertubeOff()
        {

            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _door.Open();


            _cookController.Received().Stop();

        }
    }
}