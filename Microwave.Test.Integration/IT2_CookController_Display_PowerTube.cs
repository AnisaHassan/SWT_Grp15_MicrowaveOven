using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT2_CookController_Display_PowerTube
    {
        //Mangler denne klasse ikke flere tests?

        private IUserInterface _fakeuserInterface;
        private ICookController _uut;
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _fakeoutput;

        [SetUp]
        public void SetUp()
        {


            _fakeuserInterface = Substitute.For<IUserInterface>();
            _fakeoutput = Substitute.For<IOutput>();
            _timer = new Timer();
            _powerTube = new PowerTube(_fakeoutput);
            _display = new Display(_fakeoutput);
            _uut = new CookController(_timer, _display, _powerTube);

        }


        [Test]
        public void Cookcontroller_Display_Showtime()
        {
            //Act
            _uut.StartCooking(500, 60);
            Thread.Sleep(1000);

            //Assert
            _fakeoutput.Received().OutputLine($"Display shows: 00:59");

        }

        //Vi har her fundet at powerTube ikke kan tage værdier over 100, da power skal være mellem 1-100. 
        //Vi har i klassen PowerTube lavet det om, så 100 svarer til 700W

        //[Test]
        //public void Cookcontroller_Powetube_turnon_over_100()
        //{
        //    _uut.StartCooking(150, 60);

        //    _fakeoutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("150")));
        //}

        [Test]
        public void Cookcontroller_Powetube_turnon_Under_100()
        {
            _uut.StartCooking(50, 60);

            _fakeoutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("7 %")));
        }
     

        [Test]
        public void CookController_StopCooking_TurnOff()
        {
            _uut.StartCooking(50, 60);

            _uut.Stop();
            Thread.Sleep(1100);
            _fakeoutput.Received().OutputLine("PowerTube turned off");

        }


      

    }
}
