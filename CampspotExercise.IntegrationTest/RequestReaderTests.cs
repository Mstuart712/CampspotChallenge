using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampspotExercise.IntegrationTest
{
    [TestClass]
    public class RequestReaderTests
    {
        [TestMethod]
        public void ReadRequest_ValidRequest_ReturnsTrue()
        {
            string FilePath = "..\\..\\..\\RequestFiles\\valid-request.json";
            string FileContents = File.ReadAllText(FilePath);
            var test = new RequestReader();
            var e = test.ReadRequest(FileContents);
            Assert.AreEqual(e, true);
        }

        [TestMethod]
        public void ReadRequest_NoCampsites_ReturnsFalse()
        {
            string FilePath = "..\\..\\..\\RequestFiles\\no-campsites.json";
            string FileContents = File.ReadAllText(FilePath);
            var test = new RequestReader();
            var e = test.ReadRequest(FileContents);
            Assert.AreEqual(e, false);
        }
        [TestMethod]
        public void ReadRequest_NoCampsiteProperty_ReturnsFalse()
        {
            string FilePath = "..\\..\\..\\RequestFiles\\no-campsite-property.json";
            string FileContents = File.ReadAllText(FilePath);
            var test = new RequestReader();
            var e = test.ReadRequest(FileContents);
            Assert.AreEqual(e, false);
        }

        [TestMethod]
        public void ReadRequest_NoReservationProperty_ReturnsFalse()
        {
            string FilePath = "..\\..\\..\\RequestFiles\\no-reservation-property.json";
            string FileContents = File.ReadAllText(FilePath);
            var test = new RequestReader();
            var e = test.ReadRequest(FileContents);
            Assert.AreEqual(e, false);
        }

        [TestMethod]
        public void ReadRequest_NoSearchDates_ReturnsFalse()
        {
            string FilePath = "..\\..\\..\\RequestFiles\\no-search-dates.json";
            string FileContents = File.ReadAllText(FilePath);
            var test = new RequestReader();
            var e = test.ReadRequest(FileContents);
            Assert.AreEqual(e, false);
        }

        [TestMethod]
        public void ReadRequest_NoSearchProperty_ReturnsFalse()
        {
            string FilePath = "..\\..\\..\\RequestFiles\\no-search-property.json";
            string FileContents = File.ReadAllText(FilePath);
            var test = new RequestReader();
            var e = test.ReadRequest(FileContents);
            Assert.AreEqual(e, false);
        }
    }
}