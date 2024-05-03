using Microsoft.Extensions.Logging;

using OfficeOpenXml;

namespace Infrastructure.Data.DataSeeders;

/// <summary>
/// A class responsible for seeding location translation data into the database from an Excel worksheet.
/// </summary>
public class LocationTranslationSeeder : ExcelDataSeeder<LocationTranslation>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocationTranslationSeeder"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    /// <param name="excelFilePath">The path where the Excel file is located.</param>
    public LocationTranslationSeeder(CarRentalDbContext context, ILogger<LocationTranslationSeeder> logger, string excelFilePath)
        : base(context, logger, excelFilePath)
    {
    }

    /// <summary>
    /// Gets the index of the worksheet that contains the location translation data.
    /// </summary>
    protected override int WorksheetIndex => 2;

    /// <summary>
    /// Extracts vehicle data from the specified worksheet.
    /// </summary>
    /// <param name="worksheet">The worksheet to extract data from.</param>
    /// <returns>A list of locations translations.</returns>
    protected override List<LocationTranslation> GetDataFromWorkSheet(ExcelWorksheet worksheet)
    {
        var locations = new List<LocationTranslation>();
        var rowCount = worksheet.Dimension.Rows;
        for (int row = 2; row <= rowCount; row++)
        {
            string name = this.GetValueFromCell(worksheet, row, 3);
            string city = this.GetValueFromCell(worksheet, row, 4);
            string country = this.GetValueFromCell(worksheet, row, 5);
            int locationId = int.Parse(this.GetValueFromCell(worksheet, row, 1));
            int languageId = int.Parse(this.GetValueFromCell(worksheet, row, 2));

            var location = new LocationTranslation(locationId, languageId, name, city, country);

            locations.Add(location);
        }

        return locations;
    }
}