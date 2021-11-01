using Majenka.Csv;
using NUnit.Framework;
using System;
using System.Globalization;

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

                Assert.AreEqual(9789384850753, row["Serial Number"].ToLong());
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
                CsvRow row;

                for (int i = 0; i < 59; i++)
                {
                    row = reader.ReadRow();
                    Assert.NotNull(row);

                    int? col0Value = row[0].ToInt();
                    Assert.AreEqual(i, col0Value);

                    if(i == 38)
                    {
                        DateTime? date = row[1].ToDateTime();
                        string study = row[2];
                        string studyLink = row[3];
                        string journal = row[4];
                        string studyType = row[5];
                        string factors = row[6];
                        bool? influential = row[7].ToBoolean();
                        string excerpt = row[8];
                        string measureofEvidence = row[9];
                        string addedOn = row[10];

                        Assert.AreEqual(date, new DateTime(2020, 03, 23));
                        Assert.AreEqual(study, "Scientific and ethical basis for social-distancing interventions against COVID-19");
                        Assert.AreEqual(studyLink, "https://www.sciencedirect.com/science/article/pii/S1473309920301900");
                        Assert.AreEqual(journal, "Lancet Infect Dis");
                        Assert.AreEqual(studyType, "Modeling Study");
                        Assert.AreEqual(factors, "school closure, and workplace distancing");
                        Assert.AreEqual(influential, true);
                        Assert.AreEqual(excerpt, "The authors considered three infectivity scenarios (basic reproduction number [R0] of 1·5, 2·0, or 2·5) and assumed between 7·5% and 50·0% of infections were asymptomatic ;The combined intervention, in which quarantine, school closure, and workplace distancing were implemented, was the most effective: compared with the baseline scenario of no interventions, the combined intervention reduced the estimated median number of infections by 99·3% (IQR 92·6–99·9) when R0 was 1·5, by 93·0% (81·5–99·7) when R0 was 2·0, and by 78·2% (59·0–94·4) when R0 was 2·5.");
                        Assert.AreEqual(measureofEvidence, "Countries: Singapore; Timeline: -");
                        Assert.AreEqual(addedOn, "5/27/20");
                    }
                }

                row = reader.ReadRow();
                Assert.IsNull(row);
            }
        }

        [Test]
        public void TestSampleFormatedTextFile()
        {
            using (var reader = new CsvReader("../../../SampleFormatedText.csv"))
            {
                CsvRow row;

                for (int i = 0; i < 11; i++)
                {
                    row = reader.ReadRow();
                    Assert.NotNull(row);

                    int? col0Value = row[0].ToInt();
                    Assert.AreEqual(i, col0Value);

                    if (i == 4)
                    {
                        DateTime? date = row[1].ToDateTime();
                        string study = row[2];
                        string studyLink = row[3];
                        string journal = row[4];
                        string studyType = row[5];
                        string factors = row[6];
                        bool? influential = row[7].ToBoolean();
                        string excerpt = row[8];
                        string measureofEvidence = row[9];
                        string addedOn = row[10];

                        Assert.AreEqual(date, new DateTime(2020, 05, 05));
                        Assert.AreEqual(study, "Evaluation of effects of public health interventions on COVID-19 transmission for Pakistan: A mathematical simulation study");
                        Assert.AreEqual(studyLink, "http://medrxiv.org/cgi/content/short/2020.04.30.20086447v1?rss=1");
                        Assert.AreEqual(journal, "MedRxiv");
                        Assert.AreEqual(studyType, "Modeling");
                        Assert.AreEqual(factors, "self-isolation of symptomatic cases");
                        Assert.AreEqual(influential, true);
                        Assert.AreEqual(excerpt, "Under this scenario, total expected number of cumulative case fatalities is 671,596.\r\nWhile the self-isolation strategy (Scenario 2) yielded approximately half as many case fatalities as the no intervention strategy, this showed the second highest number of cumulative case fatalities at 341,359.");
                        Assert.AreEqual(measureofEvidence, "Countries: Pakistan; Timeline: beginning on April 5th, 2020 run for one calendar year");
                        Assert.AreEqual(addedOn, "5/28/20");
                    }
                }

                row = reader.ReadRow();
                Assert.IsNull(row);
            }
        }
    }
}