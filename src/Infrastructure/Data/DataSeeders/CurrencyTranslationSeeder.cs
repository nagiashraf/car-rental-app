using Microsoft.Extensions.Logging;

using OfficeOpenXml;

namespace Infrastructure.Data.DataSeeders;

/// <summary>
/// A class responsible for seeding currency translation data into the database from an Excel worksheet.
/// </summary>
public class CurrencyTranslationSeeder : ExcelDataSeeder<CurrencyTranslation>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyTranslationSeeder"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    /// <param name="excelFilePath">The path where the Excel file is located.</param>
    public CurrencyTranslationSeeder(CarRentalDbContext context, ILogger<CurrencyTranslationSeeder> logger, string excelFilePath)
        : base(context, logger, excelFilePath)
    {
    }

    /// <summary>
    /// Gets the index of the worksheet that contains the currency translation data.
    /// </summary>
    protected override int WorksheetIndex => 5;

    /// <summary>
    /// Extracts currency translation data from the specified worksheet.
    /// </summary>
    /// <param name="worksheet">The worksheet to extract data from.</param>
    /// <returns>A list of currencies translations.</returns>
    protected override List<CurrencyTranslation> GetDataFromWorkSheet(ExcelWorksheet worksheet)
    {
        var currencies = new List<CurrencyTranslation>();
        var rowCount = worksheet.Dimension.Rows;
        for (int row = 2; row <= rowCount; row++)
        {
            string name = this.GetValueFromCell(worksheet, row, 3);
            int currencyId = int.Parse(this.GetValueFromCell(worksheet, row, 1));
            int languageId = int.Parse(this.GetValueFromCell(worksheet, row, 2));

            var currency = new CurrencyTranslation(name, currencyId, languageId);

            currencies.Add(currency);
        }

        return currencies;
    }
}