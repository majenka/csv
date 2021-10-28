using Majenka.Csv;
using NUnit.Framework;
using System;

namespace Csv.Test
{
    public class TestCsvReader
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSample100File()
        {
            using (var reader = new CsvReader("../../../Sample100.csv"))
            {
                CsvRow row = reader.ReadRow();
                Assert.NotNull(row);

                long? serialNumber = row["Serial Number"].ToLong();
                Assert.AreEqual(9788189999599, serialNumber);
                Assert.AreEqual(9788189999599, row[0].ToLong());

                int? leave = row["Leave"].ToInt();
                Assert.AreEqual(0, leave);
                Assert.AreEqual(0, row[4].ToInt());

                // skip 20 rows
                for (int i = 0; i < 20; i++)
                {
                    row = reader.ReadRow();
                    Assert.NotNull(row);
                }

                serialNumber = row["Serial Number"].ToLong();
                Assert.AreEqual(9789384850753, serialNumber);
                Assert.AreEqual(9789384850753, row[0].ToLong());

                string companyName = row["Company Name"];
                Assert.AreEqual("BHAGYA KE RAHASYA (HINDI) SECRET OF DESTINY", companyName);
                Assert.AreEqual("BHAGYA KE RAHASYA (HINDI) SECRET OF DESTINY", row[1].ToString());

                string employeeMarkme = row["Employee Markme"];
                Assert.AreEqual("DEEP TRIVEDI", employeeMarkme);
                Assert.AreEqual("DEEP TRIVEDI", row[2].ToString());

                string description = row["Description"];
                Assert.AreEqual("AATMAN INNOVATIONS PVT LTD", description);
                Assert.AreEqual("AATMAN INNOVATIONS PVT LTD", row[3].ToString());

                leave = row["Leave"].ToInt();
                Assert.AreEqual(1, leave);
                Assert.AreEqual(1, row[4].ToInt());
            }
        }

        [Test]
        public void TestSampleLongTextFile()
        {
            using (var reader = new CsvReader("../../../SampleLongText.csv"))
            {
                // ,Date,Study,Study Link,Journal,Study Type,Factors,Influential,Excerpt,Measure of Evidence,Added on

                CsvRow row = reader.ReadRow();
                Assert.NotNull(row);

                // skip 20 rows
                for (int i = 0; i < 20; i++)
                {
                    row = reader.ReadRow();
                    Assert.NotNull(row);
                }

            }
        }
    }
}