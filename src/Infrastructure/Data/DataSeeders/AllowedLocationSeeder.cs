using Microsoft.Extensions.Logging;

using OfficeOpenXml;

namespace Infrastructure.Data.DataSeeders;

/// <summary>
/// A class responsible for seeding allowed location data into the database from an Excel worksheet.
/// </summary>
public class AllowedLocationSeeder : ExcelDataSeeder<AllowedLocation>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllowedLocationSeeder"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    /// <param name="excelFilePath">The path where the Excel file is located.</param>
    public AllowedLocationSeeder(CarRentalDbContext context, ILogger<AllowedLocationSeeder> logger, string excelFilePath)
        : base(context, logger, excelFilePath)
    {
    }

    /// <summary>
    /// Gets the index of the worksheet that contains the allowed location data.
    /// </summary>
    protected override int WorksheetIndex => 3;

    /// <summary>
    /// Extracts vehicle data from the specified worksheet.
    /// </summary>
    /// <param name="worksheet">The worksheet to extract data from.</param>
    /// <returns>A list of allowed locations.</returns>
    protected override List<AllowedLocation> GetDataFromWorkSheet(ExcelWorksheet worksheet)
    {
        var allowedLocations = new List<AllowedLocation>();
        var rowCount = worksheet.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++)
        {
            var pickupLocationId = int.Parse(this.GetValueFromCell(worksheet, row, 1));
            var dropoffLocationId = int.Parse(this.GetValueFromCell(worksheet, row, 2));

            var allowedLocation = new AllowedLocation(pickupLocationId, dropoffLocationId);

            allowedLocations.Add(allowedLocation);
        }

        return allowedLocations;
    }
}