using Newtonsoft.Json;

namespace Portfolio.Models
{
    public class WordModel
    {
        [JsonProperty("word")]
        public string Word { get; set; }
        public string Phonetic { get; set; }
        public List<Phonetic> Phonetics { get; set; }

        [JsonProperty("meanings")]
        public List<Meaning> Meanings { get; set; }
        public License License { get; set; }
        public List<string> SourceUrls { get; set; }
    }

    public class Phonetic
    {
        public string Text { get; set; }
        public string Audio { get; set; }
    }

    public class Meaning
    {
        [JsonProperty("partOfSpeech")]
        public string PartOfSpeech { get; set; }

        [JsonProperty("definitions")]
        public List<Definition> Definitions { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> Antonyms { get; set; }
    }

    public class Definition
    {
        [JsonProperty("definition")]
        public string DefinitionText { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> Antonyms { get; set; }
    }

    public class License
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
