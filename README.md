# csv
CSV reader
----------

CSV reader/parser for dotnet core (C#).
Capable of handling large files since it uses a StreamReader to read a char at a time.
Handles escaped characters and paragraph text (with carriage returns and line feed chars).

Feel free to use, copy and adapt it.

Data used in tests:

Sample100.csv data from https://www.appsloveworld.com/sample-csv-file/

SampleLongText.csv  = Effectiveness of case isolation_isolation of exposed individuals to prevent secondary transmission.csv from Kaggle.com

Example 1 - reading a string array
----------------------------------
{    
    var lines = new string[] { "Car,Miles,Year", "Ford,\"77,000\",2006", "Audi,\"45,000\",2012" };
    
    using (var reader = new CsvReader(lines))
    {
        CsvRow row;
    
        while ((row = reader.ReadRow()) != null)
        {
            var car = row["Car"].ToString();
            var year = row["Year"].ToInt();
            var miles = row["Miles"].ToDouble();
    
            Console.WriteLine("Car\tYear\tMiles");
            Console.WriteLine($"{car}\t{year}\t{miles}");
        }
    }
}

Example 2 - reading a file.
---------------------------
{
    var filename = "cars.csv";
    
    using (var reader = new CsvReader(filename)
    {
        CsvRow row;
    
        while ((row = reader.ReadRow()) != null)
        {
            var car = row["Car"].ToString();        
            var year = row["Year"].ToInt();
            
            var miles = row[2].ToDouble(); // Can access value by row index. Useful if no header row.
    
            Console.WriteLine("Car\tYear\tMiles");
            Console.WriteLine($"{car}\t{year}\t{miles}");
        }
    }
}
