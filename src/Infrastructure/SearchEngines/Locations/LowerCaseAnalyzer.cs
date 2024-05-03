using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Util;

namespace Infrastructure.SearchEngines.Locations;

public class LowerCaseAnalyzer : Analyzer
{
    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
    {
        Tokenizer keywordTokenizer = new KeywordTokenizer(reader);
        TokenStream filter = new LowerCaseFilter(LuceneVersion.LUCENE_48, keywordTokenizer);
        return new TokenStreamComponents(keywordTokenizer, filter);
    }
}