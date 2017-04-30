using BeEmote.Services;
using System;

namespace BeEmote.Client.ConsoleUI
{

    class Program
    {
        #region Private Members

        /// <summary>
        /// Possible choices of action for the user in hte menu.
        /// </summary>
        private enum UserChoices
        {
            None = 0,
            StartEmotionAPI = 1,
            StartTextAnalyticsAPI = 2,
            EndSession = 3
        }

        /// <summary>
        /// Contains the Application manager.
        /// </summary>
        private static AppManager Application;

        /// <summary>
        /// When the user ask to quit this will set to false.
        /// </summary>
        private static bool SessionIsNotOver = true;

        /// <summary>
        /// Indicates the current user choice between possible <see cref="UserChoices"/>.
        /// </summary>
        private static UserChoices UserChoice = UserChoices.None;

        #endregion

        /// <summary>
        /// Main Program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Application = new AppManager();
            Application.Init();

            // Basic testing/using interface for the console
            // Wait for correct user input
            // - choice of service (Emotion, Text Analytics, Quit)
            // --- if Emotion, ask image path
            // --- Check image exists
            // --- if exists, start Emotion service
            // --- else ask image again
            // --- if nothing entered go back to choice of service
            // -
            // --- if Text Analytics, ask to input text
            // --- start Text analytics service on Enter
            // --- if no text go back to choice of service
            // -
            // - Service provides results
            // - after that, the prompt stays in the same service
            // - user can enter another content
            // - if nothing entered, back to choice of service
            while (SessionIsNotOver)
            {
                DebugWarning("Enter session loop");
                // Set the user choice if none is actually set.
                // This allows the main loop to flow at the end of a service
                // until the user explicitely chose to change service
                if (UserChoice == UserChoices.None)
                    AskInputFromUser();

                // Act according to the user's choice
                switch (UserChoice)
                {
                    case UserChoices.EndSession:
                        EndSession();
                        break;
                    case UserChoices.StartEmotionAPI:
                        AskImagePathFromUser();
                        break;
                    case UserChoices.StartTextAnalyticsAPI:
                        AskTextFromUser();
                        break;
                    default:
                        break;
                }
                // loop
            }
            // The program ends.
        }

        #region Main Methods

        /// <summary>
        /// Inform the user that he needs to enter some text,
        /// to send it to the Text Analytics API, so that he can get information on it.
        /// Then Wait for their input.
        /// - No text entered => get out of this service
        /// - text entered => starts service and then ask again when response received
        /// </summary>
        private static async void AskTextFromUser()
        {
            Console.WriteLine("You chosed, Text Analytics...");
            Console.WriteLine("Please enter your text (type nothing and press enter to get back to menu):");
            string input = Console.ReadLine();

            // Check if the user decides to get back
            if (string.IsNullOrWhiteSpace(input))
                UserChoice = UserChoices.None;
            else
            {
                // Start the application with the input text
                Application.TextToAnalyse = input;
                await Application.StartTextAnalytics();
            }
        }

        /// <summary>
        /// Inform the user that he needs to enter a valid image path
        /// Then wait for their input.
        /// - No path entered => get out of this service
        /// - path entered => starts the service and then ask again when response is received
        /// </summary>
        private static async void AskImagePathFromUser()
        {
            Console.WriteLine("You chosed, Emotion...");

            // Valid examples
            // "http://s.eatthis-cdn.com/media/images/ext/543627202/happy-people-friends.jpg";
            // @"G:\MyPics\self\20161214_124159";

            Console.WriteLine("Please enter a valid image path (type nothing and press enter to get back to menu):");
            while (true)
            {
                DebugWarning("Enter image path loop");
                string input = Console.ReadLine();
                // If nothing entered, quit back to the menu
                if (string.IsNullOrWhiteSpace(input))
                {
                    UserChoice = UserChoices.None;
                    break;
                }
                // else make sure the image path is accepted by the application
                else if (Application.SetImagePath(input))
                {
                    // Starts the service when the image path provided is valid
                    await Application.StartEmotion();
                    break;
                }
                else
                    Console.Write("This image path is not valid! Try again: ");
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// If the user chose to quit, Set <see cref="SessionIsNotOver"/> to false,
        /// and say goodbye!
        /// </summary>
        private static void EndSession()
        {
            SessionIsNotOver = false;
            Console.WriteLine("Closing the program.\nHave a nice day!");
        }

        /// <summary>
        /// Ask the user to chose between the available <see cref="UserChoices"/>.
        /// Loops until the userInput actually match one of these.
        /// </summary>
        private static void AskInputFromUser()
        {
            string userInput;
            int countTries = 0;
            bool ok = false;

            // Verify that the userInput fit the program understanding
            // Until a proper input is provided
            do
            {
                DebugWarning("Enter action choice loop");
                switch (countTries)
                {
                    case 0:
                        Console.WriteLine("May you be so kind as to provide some input?\nThis program is dying waiting for it!\nIt desperately needs some input... Please?");
                        Console.WriteLine("1 - Start the Emotion service\n2 - Start the Text Analytics service\n3 - Quit the program");
                        break;
                    case 3:
                        Console.WriteLine("How come you can't pick the right answer between 1, 2 and 3?");
                        break;
                    case 6:
                        Console.WriteLine("This is a desperate case. I'm out, continue trying all the possible characters if you want, I've got a lot of time you know...");
                        break;
                    case 9:
                        Console.WriteLine("Just to warn you, I will no longer advertise you with intelligible messages.");
                        break;
                    default:
                        Console.Write("Try again: ");
                        break;
                } // Computer have fun with wrong input...
                userInput = Console.ReadLine();
                countTries++;
                // Is the user input correct and defined in the valid user choices?
                ok = Enum.TryParse<UserChoices>(userInput, out UserChoice)
                  && Enum.IsDefined(typeof(UserChoices), (UserChoices)UserChoice);
            }
            while (!ok);
            // UserChoice is valid and set
            Console.WriteLine((UserChoices)UserChoice);
        }

        /// <summary>
        /// Console.WriteLine the message with a yellow color.
        /// </summary>
        /// <param name="message"></param>
        private static void DebugWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        #endregion
    }
}
