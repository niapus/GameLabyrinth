using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elementaria
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Create_Map_Single_Value()
        {
            var result = MapCreator.CreateMap("S");
            Assert.IsTrue(result[0, 0] is Stone);
        }

        [TestCase("S", true)]
        [TestCase("C", true)]
        [TestCase("P", true)]
        [TestCase(" ", true)]
        [TestCase("B", true)]

        public void Create_Map_Find_Element(string value, bool result)
        {
            var element = MapCreator.CreateMap(value);
            Assert.AreEqual((element[0, 0] is Stone || element[0, 0] is Box || element[0, 0] is Player
                || element[0, 0] is Chest || element[0, 0] is null), result);
        }


    }

}
