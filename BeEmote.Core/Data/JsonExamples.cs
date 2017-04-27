namespace BeEmote.Core
{
    /// <summary>
    /// This helper class provides example json strings for testing
    /// </summary>
    public class JsonExamples
    {
        /// <summary>
        /// Provides a typical result of the Emotion API with 7 faces.
        /// 5 Are happy, 1 is Surprised and the last is disgusted.
        /// </summary>
        /// <returns></returns>
        public static string GetEmotionAPIResult1()
        {
            return @"[
  {
    ""faceRectangle"": {
      ""height"": 147,
      ""left"": 487,
      ""top"": 265,
      ""width"": 147
    },
    ""scores"": {
      ""anger"": 2.2890103E-07,
      ""contempt"": 9.258776E-09,
      ""disgust"": 1.42670069E-06,
      ""fear"": 3.10480364E-10,
      ""happiness"": 0.9999981,
      ""neutral"": 6.72477E-08,
      ""sadness"": 1.7096481E-07,
      ""surprise"": 1.25202675E-08
    }
  },
  {
    ""faceRectangle"": {
      ""height"": 133,
      ""left"": 153,
      ""top"": 251,
      ""width"": 133
    },
    ""scores"": {
      ""anger"": 8.822849E-08,
      ""contempt"": 1.21227672E-09,
      ""disgust"": 3.47726434E-08,
      ""fear"": 1.58205934E-10,
      ""happiness"": 0.9999998,
      ""neutral"": 4.50224746E-09,
      ""sadness"": 1.116661E-09,
      ""surprise"": 2.9005351E-08
    }
  },
  {
    ""faceRectangle"": {
      ""height"": 124,
      ""left"": 761,
      ""top"": 324,
      ""width"": 124
    },
    ""scores"": {
      ""anger"": 0.00212847674,
      ""contempt"": 0.0007836892,
      ""disgust"": 0.0123382015,
      ""fear"": 4.131491E-05,
      ""happiness"": 0.960671067,
      ""neutral"": 0.0200230964,
      ""sadness"": 0.003762989,
      ""surprise"": 0.000251142774
    }
  },
  {
    ""faceRectangle"": {
      ""height"": 104,
      ""left"": 1058,
      ""top"": 268,
      ""width"": 104
    },
    ""scores"": {
      ""anger"": 6.521586E-07,
      ""contempt"": 1.28355793E-09,
      ""disgust"": 4.582384E-08,
      ""fear"": 6.703891E-11,
      ""happiness"": 0.999999046,
      ""neutral"": 9.967773E-08,
      ""sadness"": 1.67309366E-07,
      ""surprise"": 8.617963E-09
    }
  },
  {
    ""faceRectangle"": {
      ""height"": 68,
      ""left"": 900,
      ""top"": 355,
      ""width"": 68
    },
    ""scores"": {
      ""anger"": 2.28905876E-07,
      ""contempt"": 1.57636336E-07,
      ""disgust"": 1.37661251E-08,
      ""fear"": 1.46288226E-09,
      ""happiness"": 0.999860942,
      ""neutral"": 0.000138216667,
      ""sadness"": 2.16183707E-07,
      ""surprise"": 2.19334439E-07
    },
  },
  {
    ""faceRectangle"": {
        ""left"": 289,
        ""top"": 210,
        ""width"": 115,
        ""height"": 115
    },
    ""scores"": {
        ""anger"": 0.000168621511,
        ""contempt"": 0.0101682143,
        ""disgust"": 0.00007599527,
        ""fear"": 0.000499680056,
        ""happiness"": 0.0251381956,
        ""neutral"": 0.138517573,
        ""sadness"": 0.0000646412955,
        ""surprise"": 0.8253671
    }
  },
  {
    ""faceRectangle"": {
      ""left"": 337,
      ""top"": 172,
      ""width"": 211,
      ""height"": 211
    },
    ""scores"": {
      ""anger"": 0.11228478,
      ""contempt"": 0.004215555,
      ""disgust"": 0.72907275,
      ""fear"": 0.0000109508519,
      ""happiness"": 1.830695e-7,
      ""neutral"": 0.003904995,
      ""sadness"": 0.150495708,
      ""surprise"": 0.0000150869892
    }
  }
]";
        }

        /// <summary>
        /// Provides a typical result of the Emotion API with 6 faces.
        /// </summary>
        /// <returns></returns>
        public static string GetEmotionAPIResult2()
        {
            return @"[
  {
    ""faceRectangle"": {
      ""height"": 113,
      ""left"": 215,
      ""top"": 432,
      ""width"": 113
    },
    ""scores"": {
      ""anger"": 0.00007885037,
      ""contempt"": 1.76995655e-7,
      ""disgust"": 0.0000102558142,
      ""fear"": 6.92683155e-9,
      ""happiness"": 0.9999098,
      ""neutral"": 6.21828e-7,
      ""sadness"": 1.526979e-8,
      ""surprise"": 2.82483484e-7
    }
  },
  {
    ""faceRectangle"": {
      ""height"": 111,
      ""left"": 322,
      ""top"": 207,
      ""width"": 111
    },
    ""scores"": {
      ""anger"": 3.129956e-13,
      ""contempt"": 1.925739e-11,
      ""disgust"": 6.617225e-13,
      ""fear"": 5.770226e-17,
      ""happiness"": 1,
      ""neutral"": 1.25565336e-10,
      ""sadness"": 2.19273384e-14,
      ""surprise"": 8.63892128e-13
    }
  },
  {
    ""faceRectangle"": {
      ""height"": 96,
      ""left"": 191,
      ""top"": 99,
      ""width"": 96
    },
    ""scores"": {
      ""anger"": 0.000128725776,
      ""contempt"": 2.58404185e-7,
      ""disgust"": 0.000016522341,
      ""fear"": 1.03438637e-7,
      ""happiness"": 0.999853551,
      ""neutral"": 5.45430964e-7,
      ""sadness"": 1.79227754e-7,
      ""surprise"": 1.35705847e-7
    }
  },
  {
    ""faceRectangle"": {
      ""height"": 92,
      ""left"": 434,
      ""top"": 214,
      ""width"": 92
    },
    ""scores"": {
      ""anger"": 5.87925e-11,
      ""contempt"": 9.444531e-11,
      ""disgust"": 4.01519484e-10,
      ""fear"": 4.57860034e-14,
      ""happiness"": 1,
      ""neutral"": 2.43707787e-9,
      ""sadness"": 1.86669582e-12,
      ""surprise"": 5.28587035e-11
    }
  }
]";
        }

        /// <summary>
        /// Provides a typical result of the /languages query of the Text Analytics API.
        /// (Phase 1)
        /// </summary>
        /// <returns></returns>
        public static string GetTextLanguage()
        {
            // Language detection:
            return @"{
  ""documents"": [
    {
                ""id"": ""0"",
      ""detectedLanguages"": [
        {
          ""name"": ""English"",
          ""iso6391Name"": ""en"",
          ""score"": 1
        }
      ]
    }
  ],
  ""errors"": []
}";
        }

        /// <summary>
        /// Provides a typical result of the /keyPhrases query of the Text Analytics API.
        /// (Phase 2)
        /// </summary>
        /// <returns></returns>
        public static string GetTextKeyPhrases()
        {
            // Key phrases:
            return @"{
  ""documents"": [
    {
      ""keyPhrases"": [
        ""Gulliver tours Balnibarbi"",
        ""main port of Balnibarbi"",
        ""practical results"",
        ""extracting sunbeams"",
        ""softening marble"",
        ""excrement of suspicious persons"",
        ""cucumbers"",
        ""blind pursuit of science"",
        ""great resources"",
        ""political conspiracies"",
        ""bureaucracy"",
        ""guest"",
        ""manpower"",
        ""satire"",
        ""Royal Society"",
        ""ruin"",
        ""preposterous schemes"",
        ""Maldonada"",
        ""kingdom"",
        ""low-ranking courtier"",
        ""Grand Academy of Lagado"",
        ""muckraking"",
        ""use"",
        ""Laputa"",
        ""smell"",
        ""pillows""
      ],
      ""id"": ""0""
    }
  ],
  ""errors"": []
}";

        }

        /// <summary>
        /// Provides a typical result of the /sentiment query of the Text Analytics API.
        /// (Phase 3)
        /// </summary>
        /// <returns></returns>
        public static string GetTextSentiment()
        {
            // Sentiment:
            return @"{
  ""documents"": [
    {
        ""score"": 0.204437494277954,
      ""id"": ""0""
    }
  ],
  ""errors"": []
}";
        }

        /// <summary>
        /// Provides an example of text written in the english language.
        /// Can be used with the Text Analytics cognitive service.
        /// </summary>
        /// <returns></returns>
        public static string GetEnglishText()
        {
            return "Gulliver tours Balnibarbi, the kingdom ruled from Laputa, as the guest of a low-ranking courtier and sees the ruin brought about by the blind pursuit of science without practical results, in a satire on bureaucracy and on the Royal Society and its experiments. At the Grand Academy of Lagado in Balnibarbi , great resources and manpower are employed on researching completely preposterous schemes such as extracting sunbeams from cucumbers, softening marble for use in pillows, learning how to mix paint by smell, and uncovering political conspiracies by examining the excrement of suspicious persons (see muckraking). Gulliver is then taken to Maldonada, the main port of Balnibarbi, to await a trader who can take him on to Japan.";
        }
    }
}
