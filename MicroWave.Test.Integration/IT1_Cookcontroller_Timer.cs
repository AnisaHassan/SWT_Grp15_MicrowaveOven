using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT1_Cookcontroller_Timer
    {
        private IUserInterface _userInterface;
        private CookController _uut;
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {

            _display = Substitute.For<IDisplay>();
            _timer = Substitute.For<ITimer>();
            _userInterface = Substitute.For<IUserInterface>();
            _output = Substitute.For<IOutput>();
            _powerTube = Substitute.For<IPowerTube>();
            _uut = new CookController(_timer, _display, _powerTube, _userInterface);
            

        }


        [Test]
        public void Cookcontroller_StartCooking_()
        {
            _uut.StartCooking(20,60);

        }


        [Test]
        public void Cookcontroller_StopCooking_()
        {
            
        }
    }
}
