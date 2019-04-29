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
    class IT2_CookController
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

            _display =new Display();
            _timer = new Timer();
            _userInterface = Substitute.For<IUserInterface>();
            _output = Substitute.For<IOutput>();
            _uut = new CookController(_timer, _display, _powerTube);
            
        }

       


        [Test]
        public void Cookcontroller_StartCooking_()
        {
            //Act
            
            //Assert
            _powerTube.Received().TurnOn(50);
        }
    }
}
