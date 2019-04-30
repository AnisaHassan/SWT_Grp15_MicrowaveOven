using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    class IT2_powertube_output
    
        {
            private IPowerTube uut;
            private IOutput output;

            [SetUp]
            public void Setup()
            {
                output = new Output();
                uut = new PowerTube(output);
            }

            [Test]
            public void TurnOn_WasOff_CorrectOutput()
            {
                uut.TurnOn(50);

            }

            [Test]
            public void TurnOff_WasOn_CorrectOutput()
            {
                uut.TurnOn(50);
                uut.TurnOff();

            }

            [Test]
            public void TurnOff_WasOff_NoOutput()
            {
                uut.TurnOff();

            }
        }

    }
