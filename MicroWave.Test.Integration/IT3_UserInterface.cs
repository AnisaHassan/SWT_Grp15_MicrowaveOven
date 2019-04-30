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
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
    [TestFixture]
   public class IT3_UserInterface
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;
        private UserInterface _uut;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _fakeoutput;


        [SetUp]
        public void SetUp()
        {

            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();


            _timer = new Timer();
            _powerTube = new PowerTube(_fakeoutput);
            _door = new Door();
            _light = new Light(_fakeoutput);
            _fakeoutput = Substitute.For<IOutput>();
            _display = new Display(_fakeoutput);


            _cookController = new CookController(_timer, _display,_powerTube, _uut);
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light,
                _cookController);

            
        }

        [TestCase(7, 350)]
        [TestCase(9, 450)]
        [TestCase(3, 150)]
        public void TestDisplaysCorrectPower(int times, int power)
        {
            for (int i = 0; i < times; ++i)
                _powerButton.Press();

            _fakeoutput.Received().OutputLine($"Display shows: {power} W");
        }

        [Test]
        public void Display_PressPowerButton_ShowPower()
        {

            _powerButton.Press();

            _fakeoutput.Received().OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void Display_PressTimeButton_ShowTime()
        {

            _powerButton.Press();
            _timeButton.Press();


            _fakeoutput.Received().OutputLine($"Display shows: 01:00");
        }
        //[Test]
        //public void Display_TurnOn()
        //{
        //    _powerButton.Press();
        //    _timeButton.Press();
        //    _startCancelButton.Press();
        //    _fakeoutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));


        //}
        //[Test]
        //public void Display_CookingDone_ClearDisplay()
        //{

        //    _powerButton.Press();
        //    _timeButton.Press();
        //    _startCancelButton.Press();
        //    _uut.CookingIsDone();
        //    //_timer.Expired += Raise.EventWith(this, EventArgs.Empty);
        //    _fakeoutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));


        //   // _fakeoutput.Received().OutputLine($"Display cleared");

        //}

        //[Test]
        //public void Display_CancelCooking_ClearDisplay()
        //{

        //    _powerButton.Press();
        //    _timeButton.Press();
        //    _startCancelButton.Press();
        //    _startCancelButton.Press();




        //   // _fakeoutput.Received().OutputLine($"Display cleared");

        //}
    }
}
