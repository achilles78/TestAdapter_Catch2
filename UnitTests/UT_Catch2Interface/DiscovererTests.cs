﻿/** Basic Info **

Copyright: 2018 Johnny Hendriks

Author : Johnny Hendriks
Year   : 2018
Project: VSTestAdapter for Catch2
Licence: MIT

Notes: None

** Basic Info **/

using Catch2Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace UT_Catch2Interface
{
    [TestClass]
    public class DiscovererTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestGetTestsDefaultSettings()
        {
            var discoverer = new Discoverer(null);
            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(0, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsNameOnly()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--list-test-names-only *";
            settings.IncludeHidden = false; // With use of "--list-test-names-only" this parameter is effectively ignored
            var discoverer = new Discoverer(settings);
            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(6, tests.Count);

            // Also check with multiple sources
            string[] sources2 = { Path_Testset01, Path_Dummy };

            tests = discoverer.GetTests(sources2) as List<TestCase>;
            Assert.AreEqual(7, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsNameOnlyNoHidden()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--list-test-names-only";
            settings.IncludeHidden = true; // With use of "--list-test-names-only" this parameter is effectively ignored
            var discoverer = new Discoverer(settings);
            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(2, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsDefaultAll()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--list-tests *";
            settings.IncludeHidden = true;
            var discoverer = new Discoverer(settings);
            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(6, tests.Count);

            // Also check with multiple sources
            string[] sources2 = { Path_Testset01, Path_Dummy };

            tests = discoverer.GetTests(sources2) as List<TestCase>;
            Assert.AreEqual(7, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsDefaultAllNoHidden()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--list-tests *";
            settings.IncludeHidden = false;

            var discoverer = new Discoverer(settings);
            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(2, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsDefaultTag1()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--list-tests [Tag1]";

            var discoverer = new Discoverer(settings);
            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(4, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsDefaultTag1NoHidden()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--list-tests [Tag1]";
            settings.IncludeHidden = false;

            var discoverer = new Discoverer(settings);
            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(2, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsXml()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--discover *";

            var discoverer = new Discoverer(settings);

            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(6, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsXmlNoHidden()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--discover *";
            settings.IncludeHidden = false;

            var discoverer = new Discoverer(settings);

            string[] sources = { Path_Testset01 };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(2, tests.Count);
        }

        [TestMethod]
        public void TestGetTestsTimeout()
        {
            var settings = new Settings();
            settings.DiscoverCommandLine = "--sleep 5000 --discover";
            settings.DiscoverTimeout = 2000;
            settings.IncludeHidden = false;

            var discoverer = new Discoverer(settings);

            string[] sources = { Path_Dummy };
            var tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(0, tests.Count);

            // Sanity check to see tests could be discovered with less sleep
            settings.DiscoverCommandLine = "--sleep 1000 --discover";

            tests = discoverer.GetTests(sources) as List<TestCase>;

            Assert.AreEqual(1, tests.Count);
        }

        #region Helper properties

        private string Path_Dummy
        {
            get
            {
                string path = TestContext.TestRunDirectory + @"\..\..\..\ReferenceTests\_unittest64\Release\Catch_Dummy.exe";
                return Path.GetFullPath(path);
            }
        }

        private string Path_NoExist
        {
            get
            {
                string path = TestContext.TestRunDirectory + @"\..\..\..\ReferenceTests\_unittest64\Release\Catch_NoExist.exe";
                return Path.GetFullPath(path);
            }
        }

        private string Path_Testset01
        {
            get
            {
                string path = TestContext.TestRunDirectory + @"\..\..\..\ReferenceTests\_unittest64\Release\Catch_Testset01.exe";
                return Path.GetFullPath(path);
            }
        }

        private string Path_Testset02
        {
            get
            {
                string path = TestContext.TestRunDirectory + @"\..\..\..\ReferenceTests\_unittest64\Release\Catch_Testset02.exe";
                return Path.GetFullPath(path);
            }
        }

        #endregion // Helper Methods
    }
}
