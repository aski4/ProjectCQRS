using OCR;
using System;

namespace ConsoleClient
{
    class Program
    {
        private const string SEPARATOR = "=========================================================";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            if(args.Length > 0)
            {
                var basepath = args[1];
                
                var ocr = new CoreOCR(LanguageEnum.Russian);
                var result = ocr.GetTextFromImagesAsync(basepath).GetAwaiter().GetResult();

                foreach (var resp in result)
                {
                    Console.WriteLine(SEPARATOR);
                    Console.WriteLine(resp.FilePath + "\n");
                    Console.WriteLine(resp.Text);
                    Console.WriteLine(SEPARATOR);
                }
            }
        }
    }
}
