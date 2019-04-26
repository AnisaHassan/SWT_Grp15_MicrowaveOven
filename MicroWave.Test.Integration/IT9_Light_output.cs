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
    class IT9_Light_output
    {
        private ILight _uut_light;

        [SetUp]
        public void Setup()
        {
            Output output = new Output();
            _uut_light = new Light(output);
        }

        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        {
            _uut_light.TurnOn();
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            _uut_light.TurnOn();
            _uut_light.TurnOff();
        }

        [Test]
        public void TurnOn_WasOn_CorrectOutput()
        {
            _uut_light.TurnOn();
            _uut_light.TurnOn();

        }

        [Test]
        public void TurnOff_Was_Off_CorrectOutput()
        {
            _uut_light.TurnOff(); ;
        }
    }
}
