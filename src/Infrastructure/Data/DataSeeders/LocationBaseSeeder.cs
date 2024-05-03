using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace Infrastructure.Data.DataSeeders;

/// <summary>
/// A class responsible for seeding location base data into the database from an Excel worksheet.
/// </summary>
public class LocationBaseSeeder : ExcelDataSeeder<LocationBase>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocationBaseSeeder"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    /// <param name="excelFilePath">The path where the Excel file is located.</param>
    public LocationBaseSeeder(CarRentalDbContext context, ILogger<LocationBaseSeeder> logger, string excelFilePath)
        : base(context, logger, excelFilePath)
    {
    }

    /// <summary>
    /// Gets the index of the worksheet that contains the location base data.
    /// </summary>
    protected override int WorksheetIndex => 1;

    /// <summary>
    /// Extracts location base data from the specified worksheet.
    /// </summary>
    /// <param name="worksheet">The worksheet to extract data from.</param>
    /// <returns>A list of locations bases.</returns>
    protected override List<LocationBase> GetDataFromWorkSheet(ExcelWorksheet worksheet)
    {
        var locations = new List<LocationBase>();
        var rowCount = worksheet.Dimension.Rows;
        for (int row = 2; row <= rowCount; row++)
        {
            var phoneNumber = this.GetValueFromCell(worksheet, row, 2);
            var location = new LocationBase(phoneNumber);

            locations.Add(location);
        }

        return locations;
    }
}