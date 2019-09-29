using RegisterMember.Models;
using RegisterMember.Service;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Configuration;

namespace RegisterMember.App
{
    class Program
    {
        static void Main()
        {
            EnableLogging();

            try
            {
                Registration registration = Registration.Instance;
                RegistrationInterface registrationInterface = new RegistrationInterface(registration);

                Member member = registrationInterface.GetMemberInformation();
                if (member != null)
                {
                    var result = registration.RegisterMember(member);
                    if (result)
                    {
                        Console.WriteLine("Registration successful. Thank you");
                        Log.Information(String.Format("New successful registration {0}", member.FirstName + member.Surname));
                    }
                    else
                    {
                        Console.WriteLine("Registration failed");
                        Log.Error("Registration failed");
                    }
                }
                else
                {
                    Console.WriteLine("Registration failed");
                    Log.Error("Member information was empty");
                }
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private static void EnableLogging()
        {
            string logFilePath = ConfigurationManager.AppSettings["LogFile"];
            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .WriteTo.File(new CompactJsonFormatter(), logFilePath, rollingInterval: RollingInterval.Day)
                                .CreateLogger();
            Log.Information(String.Format("Application Started at {0}", DateTime.UtcNow));
        }

    }
}
