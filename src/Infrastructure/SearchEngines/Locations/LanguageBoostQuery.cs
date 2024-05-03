using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Queries;
using Lucene.Net.Search;

namespace Infrastructure.SearchEngines.Locations;

/// <summary>
/// A custom score query that boosts the relevance score of documents based on their language, giving preference to the specified preferred language and English as a secondary choice.
/// </summary>
public class LanguageBoostQuery : CustomScoreQuery
{
    private const float PreferredLanguageBoostFactor = 100;
    private const float EnglishLanguageBoostFactor = 10;
    private const int EnglishLanguageId = 1;
    private readonly int preferredLanguageId;

    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageBoostQuery"/> class.
    /// </summary>
    /// <param name="subQuery">The subquery to be boosted.</param>
    /// <param name="preferredLanguageId">The preferred language ID of the user.</param>
    public LanguageBoostQuery(Query subQuery, int preferredLanguageId)
        : base(subQuery)
        => this.preferredLanguageId = preferredLanguageId;

    /// <summary>
    /// Gets the custom score provider for the query.
    /// </summary>
    /// <param name="context">The atomic reader context.</param>
    /// <returns>A <see cref="LanguageBoostScoreProvider"/> instance.</returns>
    protected override CustomScoreProvider GetCustomScoreProvider(AtomicReaderContext context)
        => new LanguageBoostScoreProvider(context, this.preferredLanguageId);

    private sealed class LanguageBoostScoreProvider : CustomScoreProvider
    {
        private readonly int preferredLanguageId;

        public LanguageBoostScoreProvider(AtomicReaderContext context, int preferredLanguageId)
            : base(context)
            => this.preferredLanguageId = preferredLanguageId;

        public override float CustomScore(int doc, float subQueryScore, float valSrcScore)
        {
            Document document = this.m_context.AtomicReader.Document(doc);
            int documentLanguageId = int.Parse(document.Get(LuceneLocationSearcher.IndexField.LanguageId.ToString()));

            if (documentLanguageId == this.preferredLanguageId)
            {
                return subQueryScore * PreferredLanguageBoostFactor;
            }

            if (this.preferredLanguageId != EnglishLanguageId && documentLanguageId == EnglishLanguageId)
            {
                return subQueryScore * EnglishLanguageBoostFactor;
            }

            return subQueryScore;
        }
    }
}