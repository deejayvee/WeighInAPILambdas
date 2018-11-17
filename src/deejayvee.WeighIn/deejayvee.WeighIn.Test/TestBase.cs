using deejayvee.WeighIn.Library;
using log4net;
using log4net.Config;
using log4net.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace deejayvee.WeighIn.Test
{
    public abstract class TestBase
    {
        protected readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [OneTimeSetUp]
        public void SetupFixture()
        {
            ILoggerRepository logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        protected void TestGetSets(Type TypeToTest)
        {
            TestGetSets(TypeToTest, Activator.CreateInstance(TypeToTest));
        }

        protected void TestGetSets(Type TypeToTest, object ObjectToTest)
        {
            foreach (PropertyInfo Prop in TypeToTest.GetProperties())
            {
                if (Prop.GetCustomAttribute<IgnorePropertyInUnitTestAttribute>() == null && Prop.CanWrite)
                {
                    try
                    {
                        Type PropertyType = Prop.PropertyType;

                        if (PropertyType.Equals(typeof(int)))
                        {
                            Console.WriteLine($"Testing integer property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            int TestValue = 123;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (int)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(long)))
                        {
                            Console.WriteLine($"Testing long property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            long TestValue = 123123123;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (long)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(decimal)))
                        {
                            Console.WriteLine($"Testing decimal property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            decimal TestValue = 123.456M;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (decimal)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(double)))
                        {
                            Console.WriteLine($"Testing double property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            double TestValue = 123.456;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (double)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(string)))
                        {
                            Console.WriteLine($"Testing string property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            string TestValue = "123";
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (string)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(DateTime)))
                        {
                            Console.WriteLine($"Testing DateTime property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            DateTime TestValue = DateTime.Now;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (DateTime)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(TimeSpan)))
                        {
                            Console.WriteLine($"Testing TimeSpan property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            TimeSpan TestValue = DateTime.Now.TimeOfDay;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (TimeSpan)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(bool)))
                        {
                            Console.WriteLine($"Testing bool property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            bool TestValue = true;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (bool)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(Nullable<Boolean>)))
                        {
                            Console.WriteLine($"Testing Nullable<Boolean> property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            Nullable<Boolean> TestValue = true;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (Nullable<Boolean>)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(char)))
                        {
                            Console.WriteLine($"Testing char property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            char TestValue = 'D';
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (char)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(int?)))
                        {
                            Console.WriteLine($"Testing integer property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            int? TestValue = 123;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (int?)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(long?)))
                        {
                            Console.WriteLine($"Testing long property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            long? TestValue = 123123123;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (long?)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(decimal?)))
                        {
                            Console.WriteLine($"Testing decimal property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            decimal? TestValue = 123.456M;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (decimal?)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(double?)))
                        {
                            Console.WriteLine($"Testing double property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            double? TestValue = 123.456;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (double?)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(DateTime?)))
                        {
                            Console.WriteLine($"Testing DateTime property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            DateTime? TestValue = DateTime.Now;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (DateTime?)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(TimeSpan?)))
                        {
                            Console.WriteLine($"Testing TimeSpan property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            TimeSpan? TestValue = DateTime.Now.TimeOfDay;
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (TimeSpan?)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(Nullable<Guid>)))
                        {
                            Console.WriteLine($"Testing Nullable<Guid> property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            Nullable<Guid> testValue = Guid.NewGuid();
                            Prop.SetValue(ObjectToTest, testValue, null);
                            Assert.AreEqual(testValue, (Nullable<Guid>)Prop.GetValue(ObjectToTest, null));
                        }
                        else if (PropertyType.Equals(typeof(Guid)))
                        {
                            Console.WriteLine($"Testing Guid property \"{Prop.Name}\" in type \"{TypeToTest}\".");

                            Guid TestValue = Guid.NewGuid();
                            Prop.SetValue(ObjectToTest, TestValue, null);
                            Assert.AreEqual(TestValue, (Guid)Prop.GetValue(ObjectToTest, null));
                        }
                        else
                        {
                            Console.WriteLine($"Could not test property \"{Prop.Name}\" in type \"{TypeToTest}\" because it is of the type \"{PropertyType.Name}\"");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error testing property \"{Prop.Name}\" in type \"{TypeToTest}\"");
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
        }
    }
}
