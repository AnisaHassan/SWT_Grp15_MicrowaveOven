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
        private IButton _fakepowerButton;
        private IButton _faketimeButton;
        private IButton _fakestartCancelButton;
        private IDoor _fakedoor;
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

            _fakepowerButton = new Button();
            _faketimeButton = new Button();
            _fakestartCancelButton = new Button();


            _timer = new Timer();
            _powerTube = new PowerTube(_fakeoutput);
            _fakedoor = new Door();
            _light = new Light(_fakeoutput);
            _fakeoutput = Substitute.For<IOutput>();
            _display = new Display(_fakeoutput);


            _cookController = new CookController(_timer, _display,_powerTube, _uut);
            _uut = new UserInterface(_fakepowerButton, _faketimeButton, _fakestartCancelButton, _fakedoor, _display, _light,
                _cookController);

            
        }

        [TestCase(7, 350)]
        [TestCase(9, 450)]
        [TestCase(3, 150)]
        public void TestDisplaysCorrectPower(int times, int power)
        {
            for (int i = 0; i < times; ++i)
                _fakepowerButton.Press();

            _fakeoutput.Received().OutputLine($"Display shows: {power} W");
        }

        [Test]
        public void Display_PressPowerButton_ShowPower()
        {

            _fakepowerButton.Press();

            _fakeoutput.Received().OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void Display_PressTimeButton_ShowTime()
        {

            _fakepowerButton.Press();
            _faketimeButton.Press();


            _fakeoutput.Received().OutputLine($"Display shows: 01:00");
        }

        [Test]
        public void Display_CookingDone_ClearDisplay()
        {

            _fakepowerButton.Press();
            _faketimeButton.Press();
            _fakestartCancelButton.Press();
            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);


            _fakeoutput.Received().OutputLine($"Display cleared");

        }

        [Test]
        public void Display_CancelCooking_ClearDisplay()
        {

            _fakepowerButton.Press();
            _faketimeButton.Press();
            _fakestartCancelButton.Press();
            

            _fakeoutput.Received(1).OutputLine($"Display cleared");

        }
    }
}
