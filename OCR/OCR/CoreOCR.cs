using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;
using IronPdf;

namespace OCR
{
    public class CoreOCR
    {
        private readonly IronTesseract _ironTesseract;

        public CoreOCR(LanguageEnum language)
        {
            var config = new TesseractConfiguration();
            config.TesseractVersion = TesseractVersion.Tesseract5;
            _ironTesseract = new IronTesseract(config);
            _ironTesseract.Language = language.Map();
        }

        public async Task<List<OCRResponse>> GetTextFromImagesAsync(string path)
        {
            var files = Directory.GetFiles(path);
            var response = new List<OCRResponse>(files.Count());

            foreach (var item in files)
            {
                using (var Input = new OcrInput(item))
                {
                    var result = await _ironTesseract.ReadAsync(Input);

                    response.Add(new OCRResponse
                    {
                        Text = result.Text,
                        FilePath = item,
                        Language = _ironTesseract.Language.Map()
                    });
                }
            }

            return response;
        }

        public OCRResponse GetTextFromBytes(byte[] file)
        {
            using (var Input = new OcrInput(file))
            {
                var result = _ironTesseract.Read(Input);

                return new OCRResponse
                {
                    Text = result.Text,
                    FilePath = "none",
                    Language = _ironTesseract.Language.Map()
                };
            }
        }
    }
}
