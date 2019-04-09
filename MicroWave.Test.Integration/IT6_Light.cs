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
    class IT6_Light
    {

        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Door _door;
        private Light _uut;
        private Display _display;
        private CookController _cookController;
        private UserInterface _userInterface;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _output;


        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _door = new Door();
            _output = Substitute.For<IOutput>();
            _uut = new Light(_output);
            _display = new Display(_output);

            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _uut,
                _cookController);

            _cookController.UI = _userInterface;

        }
    }
}
