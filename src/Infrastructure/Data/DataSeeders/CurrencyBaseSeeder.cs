using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace Infrastructure.Data.DataSeeders;

/// <summary>
/// A class responsible for seeding currency base data into the database from an Excel worksheet.
/// </summary>
public class CurrencyBaseSeeder : ExcelDataSeeder<CurrencyBase>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyBaseSeeder"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    /// <param name="excelFilePath">The path where the Excel file is located.</param>
    public CurrencyBaseSeeder(CarRentalDbContext context, ILogger<CurrencyBaseSeeder> logger, string excelFilePath)
        : base(context, logger, excelFilePath)
    {
    }

   /// <summary>
    /// Gets the index of the worksheet that contains the currency base data.
    /// </summary>
    protected override int WorksheetIndex => 4;

    /// <summary>
    /// Extracts currency base data from the specified worksheet.
    /// </summary>
    /// <param name="worksheet">The worksheet to extract data from.</param>
    /// <returns>A list of currencies bases.</returns>
    protected override List<CurrencyBase> GetDataFromWorkSheet(ExcelWorksheet worksheet)
    {
        var currencies = new List<CurrencyBase>();
        var rowCount = worksheet.Dimension.Rows;
        for (int row = 2; row <= rowCount; row++)
        {
            var code = this.GetValueFromCell(worksheet, row, 2);
            var currency = new CurrencyBase(code);

            currencies.Add(currency);
        }

        return currencies;
    }
}