using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransformerLibrary;
using System.IO;

namespace TransformerLibraryTests
{
    [TestClass]
    public class TransformerTests
    {
        [TestMethod]
        public void AddStringString()
        {
            Transformer transformer = new Transformer();
            transformer.Add("key", "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddNullString()
        {
            Transformer transformer = new Transformer();
            transformer.Add(null, "value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddStringNull()
        {
            Transformer transformer = new Transformer();
            transformer.Add("key", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddBadString()
        {
            Transformer transformer = new Transformer();
            transformer.Add("abc_def", "value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddStringBad()
        {
            Transformer transformer = new Transformer();
            transformer.Add("key", "   ");
        }

        [TestMethod]
        public void TransformReaderWriter()
        {
            Transformer transformer = new Transformer();
            transformer.Add("key", "KEY");
            transformer.Add("value", "VALUE");
            using (StreamWriter writer = new StreamWriter(@"TestFiles\Test1Output.txt"))
            using (StreamReader reader = new StreamReader(@"TestFiles\Test1Input.txt"))
            {
                transformer.Transform(reader, writer);
            }
        }
    }
}
