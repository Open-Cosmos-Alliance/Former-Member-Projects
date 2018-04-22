using System;

using WitchcraftOS.Witchcraftfx.Textscreen;

namespace WitchcraftOS.Witchcraftfx.LangManagement
{
    public static class Lang
    {
        public static string lang = string.Empty;
        public static string Get(string word)
        {
            string original = word;
            word = word.ToLower();
            if (lang == "en-US")
            {
                return original;
            }
            else if (lang == "de-DE")
            {
                // Language Selector
                if (word == "language") return "Sprache";
                // General
                else if (word == "press any key to continue") return "Drücke eine Taste, um fortzufahren";
                else if (word == "date") return "Datum";
                else if (word == "time") return "Uhrzeit";
                // Debug Screen
                else if (word == "debug informations") return "Debug Informationen";
                else if (word == "quick selfcheck") return "Schneller Selbsttest";
                else if (word == "memory") return "Speicher";
                else if (word == "enabled") return "Aktiviert";
                else if (word == "disabled") return "Deaktiviert";
                else if (word == "quick command-reference") return "Kurze Befehls-Referenz";
                else if (word == "it may not work to shut down correctly") return "Möglicherweiße funktioniert das herunterfahren nicht ordnungsgemäß";
                // Help Screen
                else if (word == "displays this command-reference") return "Zeigt diese Befehls-Referenz an";
                else if (word == "displays the given text") return "Zeigt den angegebenen Text an";
                else if (word == "displays a new line (crlf)") return "Zeigt eine leere Zeile an (CrLf)";
                else if (word == "sends the shutdown signal to the computer") return "Fährt den Computer herunter";
                else if (word == "displays several debug informations") return "Zeigt verschiedene Debug-Informationen an";
                else if (word == "executes an witchcraft applet") return "Führt eine Witchcraft-Anwendung aus";
                else if (word == "displays a clock") return "Zeigt eine Uhr an";
                // Witchcraft Applications
                else if (word == "press any key to close the application") return "Drücke eine Taste, um die Anwendung zu schließen";
                else if (word == "press q to close the application") return "Drücke Q, um die Anwendung zu schließen";
            }
            return original;
        }
    }
}
