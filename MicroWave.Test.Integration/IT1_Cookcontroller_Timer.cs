using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;
using NUnit.Framework.Internal;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT1_Cookcontroller_Timer
    {
        private IUserInterface _fakeuserInterface;
        private CookController _uut;
        private IDisplay _fakedisplay;
        private ITimer _timer;
        private IPowerTube _fakepowerTube;
        private IOutput _fakeoutput;

        [SetUp]
        public void SetUp()
        {
            _fakedisplay = Substitute.For<IDisplay>();
            _timer = new Timer();
            _fakeuserInterface = Substitute.For<IUserInterface>();
            _fakeoutput = Substitute.For<IOutput>();
            _fakepowerTube = Substitute.For<IPowerTube>();
            _uut = new CookController(_timer, _fakedisplay, _fakepowerTube, _fakeuserInterface);
        }


        [Test]
        public void Cookcontroller_StartCooking_()
        {
            //Act
            _uut.StartCooking(12,12);

            Thread.Sleep(1000);

            // Assert
            _fakedisplay.Received().ShowTime(0,11);

        }
    }
}
