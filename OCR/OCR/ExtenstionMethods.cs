using IronOcr;

namespace OCR
{
    public static class ExtenstionMethods
    {
        public static OcrLanguage Map(this LanguageEnum language)
        {
            switch (language)
            {
                case LanguageEnum.Russian:
                    return OcrLanguage.Russian;
                case LanguageEnum.English:
                    return OcrLanguage.English;
                case LanguageEnum.Spanish:
                    return OcrLanguage.Spanish;
                default:
                    return OcrLanguage.English;
            }
        }

        public static LanguageEnum Map(this OcrLanguage language)
        {
            switch (language)
            {
                case OcrLanguage.Russian:
                case OcrLanguage.RussianBest:
                case OcrLanguage.RussianFast:
                    return LanguageEnum.Russian;
                case OcrLanguage.Spanish:
                case OcrLanguage.SpanishBest:
                case OcrLanguage.SpanishFast:
                case OcrLanguage.SpanishOld:
                case OcrLanguage.SpanishOldBest:
                case OcrLanguage.SpanishOldFast:
                    return LanguageEnum.Spanish;
                case OcrLanguage.English:
                case OcrLanguage.EnglishBest:
                case OcrLanguage.EnglishFast:
                default:
                    return LanguageEnum.English;
            }
        }
    }
}
