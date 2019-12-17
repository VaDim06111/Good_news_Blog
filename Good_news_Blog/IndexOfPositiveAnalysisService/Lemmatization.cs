﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Core;

namespace IndexOfPositiveAnalysisService
{
    public class Lemmatization : ILemmatization
    {
        public async Task<string> GetLemmaFromText(string text)
        {
            string result = "";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=502d75baf06585ce1c8cb4e8184d70fe99a82756"))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "application/json");

                        request.Content = new StringContent("[ { \"text\" : \"" + text + "\" } ]");
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var response = await httpClient.SendAsync(request);
                        result = await response.Content.ReadAsStringAsync();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public Dictionary<string, int> GetWordsDictionaryFromLemma(string jsonLemma)
        {
            Dictionary<string, int> wordsDictionary = new Dictionary<string, int>();

            try
            {
                var dictionary = JsonConvert.DeserializeObject<List<DeserialiseModel>>(jsonLemma);
                var annotation = dictionary[0].annotations;

                foreach (var lemma in annotation.lemma)
                {
                    if (lemma.value != "")
                    {
                        if (wordsDictionary.ContainsKey(lemma.value))
                        {
                            wordsDictionary[lemma.value] += 1;
                        }
                        else
                        {
                            wordsDictionary[lemma.value] = 1;
                        }
                    }
                }

                return wordsDictionary;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Dictionary<string, string> GetBasicValues()
        {
            try
            {
                var fileData = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\.." + "\\.." + "\\.." + "\\.." + @"\IndexOfPositiveAnalysisService" + @"\AFINN-ru.json");
                var baseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileData);

                return baseDictionary;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<double> GetIndexOfPositive(string text)
        {
            try
            {
                var baseDictionary = GetBasicValues();

                string[] masText;
                var summResult = 0;
                var countResult = 0;

                if (text.Length > 1500)
                {
                    masText = new string[text.Length / 1500 + 1];
                    for (int i = 0; i < masText.Length; i++)
                    {
                        masText[i] = text.Substring(i * 1400, text.Length - i * 1500 > 1500 ? 1500 : text.Length - i * 1500);
                    }

                    for (int i = 0; i < masText.Length; i++)
                    {
                        var jsonLemma = await GetLemmaFromText(masText[i]);
                        var dictionary = GetWordsDictionaryFromLemma(jsonLemma);
                        var summ = 0;
                        var count = 0;

                        foreach (var word in dictionary.Keys)
                        {
                            if (baseDictionary.ContainsKey(word))
                            {
                                summ += Convert.ToInt32(baseDictionary[word]) * dictionary[word];
                                count += dictionary[word];
                            }
                        }

                        summResult += summ;
                        countResult += count;
                    }
                }
                else
                {
                    var jsonLemma = await GetLemmaFromText(text);
                    var dictionary = GetWordsDictionaryFromLemma(jsonLemma);

                    foreach (var word in dictionary.Keys)
                    {
                        if (baseDictionary.ContainsKey(word))
                        {
                            summResult += Convert.ToInt32(baseDictionary[word]) * dictionary[word];
                            countResult += dictionary[word];
                        }
                    }
                }

                return (double)summResult / countResult;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
