using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT4_Timer
    {
        private ITimer _uut;
        private ICookController _cookController;

        [SetUp]
        public void Setup()
        {
            _cookController = Substitute.For<ICookController>();
            _uut = new Timer();

        }
    }
}
