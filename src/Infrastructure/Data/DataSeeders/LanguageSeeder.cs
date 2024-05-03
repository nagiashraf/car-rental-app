using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace Infrastructure.Data.DataSeeders;

/// <summary>
/// A class responsible for seeding language data into the database from an Excel worksheet.
/// </summary>
public class LanguageSeeder : ExcelDataSeeder<Language>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageSeeder"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    /// <param name="excelFilePath">The path where the Excel file is located.</param>
    public LanguageSeeder(CarRentalDbContext context, ILogger<LanguageSeeder> logger, string excelFilePath)
        : base(context, logger, excelFilePath)
    {
    }

    /// <summary>
    /// Gets the index of the worksheet that contains the language data.
    /// </summary>
    protected override int WorksheetIndex => 0;

    /// <summary>
    /// Extracts language data from the specified worksheet.
    /// </summary>
    /// <param name="worksheet">The worksheet to extract data from.</param>
    /// <returns>A list of languages.</returns>
    protected override List<Language> GetDataFromWorkSheet(ExcelWorksheet worksheet)
    {
        var languages = new List<Language>();
        var rowCount = worksheet.Dimension.Rows;
        for (int row = 2; row <= rowCount; row++)
        {
            string code = this.GetValueFromCell(worksheet, row, 2);
            string englishName = this.GetValueFromCell(worksheet, row, 3);
            string nativeName = this.GetValueFromCell(worksheet, row, 4);
            string flagImagePath = this.GetValueFromCell(worksheet, row, 5);
            bool isRtl = bool.Parse(this.GetValueFromCell(worksheet, row, 6));

            var language = new Language(code, englishName, nativeName, flagImagePath, isRtl);

            languages.Add(language);
        }

        return languages;
    }
}