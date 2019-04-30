using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Test.Integration;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicroWave.Test.Integration
{
    [TestFixture]
    public class IT3_CookController
    {
        private UserInterface _userInterface;
        private CookController _uut;

        private Button _powerButton;
        private Button _startCancelButton;
        private Button _timeButton;

        private IDoor _door;
        private ILight _light;
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;


        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();

           

            _uut = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _uut);
        }

        //[TestCase(12, 12)]
        //public void StartCooking_OutputShowsPower_PowerIsCorrect(int power, int time)
        //{
        //    _uut.StartCooking(power, time);
        //    _output.Received().OutputLine(Arg.Is<string>(str => str.Contains(power + " W")));
        //}


        [Test]
        public void Cookcontroller_StartCooking_PowertubeOn()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _powerTube.Received().TurnOn(50);
        }
    }
}
