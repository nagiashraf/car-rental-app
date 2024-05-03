using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace Infrastructure.Data.DataSeeders;

/// <summary>
/// An abstract class that provides a template method for seeding data from an Excel file into a database context.
/// </summary>
/// <typeparam name="TEntity">The type of the entity to be seeded.</typeparam>
public abstract partial class ExcelDataSeeder<TEntity>
    where TEntity : class
{
    private readonly CarRentalDbContext context;
    private readonly ILogger<ExcelDataSeeder<TEntity>> logger;
    private readonly FileInfo excelFileInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExcelDataSeeder{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The database context to seed data into.</param>
    /// <param name="logger">The logger to use for logging messages.</param>
    /// <param name="excelFilePath">The path where the Excel file is located.</param>
    protected ExcelDataSeeder(CarRentalDbContext context, ILogger<ExcelDataSeeder<TEntity>> logger, string excelFilePath)
    {
        this.context = context;
        this.logger = logger;
        this.excelFileInfo = new FileInfo(excelFilePath);
    }

    /// <summary>
    /// Gets the index of the worksheet in the Excel file that contains the data to be seeded.
    /// </summary>
    protected abstract int WorksheetIndex { get; }

    /// <summary>
    /// A template method that seeds data from the Excel file into the database context asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>This method follows the Template Method Pattern.</remarks>
    public async Task SeedAsync()
    {
        if (this.context.Set<TEntity>().Any())
        {
            this.LogDataAlreadySeeded(typeof(TEntity).Name);
            return;
        }

        List<TEntity> entities = await this.ReadFromExcelFileAsync();
        this.context.Set<TEntity>().AddRange(entities);
        await this.context.SaveChangesAsync();
        this.LogDataSeededSuccessfully(typeof(TEntity).Name);
    }

    /// <summary>
    /// Gets data from the specified Excel worksheet.
    /// </summary>
    /// <param name="worksheet">The Excel worksheet containing the data.</param>
    /// <returns>A list of TEntity entities parsed from the worksheet.</returns>
    protected abstract List<TEntity> GetDataFromWorkSheet(ExcelWorksheet worksheet);

    /// <summary>
    /// Gets the string value from the specified Excel cell.
    /// </summary>
    /// <param name="worksheet">The Excel worksheet containing the cell.</param>
    /// <param name="row">The row index of the cell.</param>
    /// <param name="column">The column index of the cell.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    protected string GetValueFromCell(ExcelWorksheet worksheet, int row, int column)
    {
        var value = worksheet.Cells[row, column].Value.ToString();
        return value is null ? throw new InvalidOperationException() : value;
    }

    /// <summary>
    /// Reads data from the Excel file asynchronously and converts it into a list of entities.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    private async Task<List<TEntity>> ReadFromExcelFileAsync()
    {
        using var package = new ExcelPackage(this.excelFileInfo);
        await package.LoadAsync(this.excelFileInfo);
        var worksheet = package.Workbook.Worksheets[this.WorksheetIndex];
        return this.GetDataFromWorkSheet(worksheet);
    }

    [LoggerMessage(0, LogLevel.Information, "{EntityName} data already seeded")]
    private partial void LogDataAlreadySeeded(string entityName);

    [LoggerMessage(1, LogLevel.Information, "{EntityName} data was seeded successfully")]
    private partial void LogDataSeededSuccessfully(string entityName);
}