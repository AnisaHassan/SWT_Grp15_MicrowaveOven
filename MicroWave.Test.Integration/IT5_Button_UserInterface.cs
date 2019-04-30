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
    class IT5_Button_UserInterface
    {

        public class IT5_UserInterface_Button
        {


            private IUserInterface _ui;


            private ITimer _timer;
            private IPowerTube _powertube;

            private IButton _powerButton;
            private IButton _timerButton;
            private IButton _startButton;
            private IDoor _door;
            private ILight _light;
            private IOutput _output;
            private ICookController _cooker;
            private IDisplay _display;
        
  


        [SetUp]
            public void SetUp()
            {

                _timer = Substitute.For<ITimer>();


                _powerButton = new Button();
                _startButton = new Button();
                _timerButton = new Button();
               
                _output = Substitute.For<IOutput>();

                _display = new Display(_output);

                _door = new Door();
                _light = new Light(_output);
                _powertube = new PowerTube(_output);
                _cook = new CookController(_timer, _display, _powertube);
                _ui = new UserInterface(_powerButton, _timerButton, _startButton, _door, _display, _light, _cook);

            }

            [TestCase(1, 50)]
            [TestCase(2, 100)]
            [TestCase(3, 150)]
            [TestCase(7, 350)]
            [TestCase(10, 500)]
            [TestCase(12, 600)]
            [TestCase(14, 700)]
            public void TestDisplaysCorrectPower(int times, int power)
            {
                for (int i = 0; i < times; ++i)
                    _powerButton.Press();

                _output.Received(1).OutputLine($"Display shows: {power} W");
            }

            
            
            public void UserInterface_Button_WasPowerPressed_100times()
            {
                for (int i = 0; i < 100; i++)
                {
                    _powerButton.Press();

                }

                _output.Received().OutputLine("Display shows: 50 W");
            }

            [Test]
            public void UserInterface_Button_WasTimePressed()
            {
                _powerButton.Press();
                _timerButton.Press();
                _output.Received().OutputLine("Display shows: 01:00");

            }

            
          
        }
    }
}
      