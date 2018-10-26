﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnityEditor.PackageManager.ValidationSuite.Tests
{
    class PackageBinaryZippingTests
    {
        [Test]
        public void ZipPackageBinariesCreatesCorrectZip()
        {
            var testFolder = "ZipPackageBinariesCreatesCorrectZip";
            if (Directory.Exists(testFolder))
                Directory.Delete(testFolder, true);

            Directory.CreateDirectory(testFolder);
            string zipFilePath;
            Assert.IsTrue(PackageBinaryZipping.TryZipPackageBinaries(ApiValidationTests.testPackageRoot + "TestPackage_AsmdefWithTypeAdd", "TestPackage_AsmdefWithTypeAdd", "0.1.0", testFolder, out zipFilePath), "ZipPackageBinaries failed");

            var destPath = Path.Combine(testFolder, "zipContents");
            var expectedPaths = new[]
            {
                Path.Combine(destPath, "Unity.PackageValidationSuite.EditorTests.TestPackage_AsmdefWithTypeAdd.dll"),
                Path.Combine(destPath, "Unity.PackageValidationSuite.EditorTests.TestPackage_AsmdefWithTypeAdd.NewAsmdef.dll")
            };
            Assert.IsTrue(PackageBinaryZipping.Unzip(zipFilePath, destPath), "Unzip failed");
            var actualPaths = Directory.GetFiles(destPath, "*", SearchOption.AllDirectories);
            Assert.That(actualPaths, Is.EquivalentTo(expectedPaths));
        }

        [Test]
        public void ZipPackageBinariesCreatesEmptyZipOnEmptyPackage()
        {
            var testFolder = "ZipPackageBinariesCreatesEmptyZipOnEmptyPackage";
            if (Directory.Exists(testFolder))
                Directory.Delete(testFolder, true);

            Directory.CreateDirectory(testFolder);
            string zipFilePath;
            Assert.IsTrue(PackageBinaryZipping.TryZipPackageBinaries(ApiValidationTests.testPackageRoot + "TestPackage_Empty", "TestPackage_Empty", "0.1.0", testFolder, out zipFilePath), "ZipPackageBinaries failed");

            var destPath = Path.Combine(testFolder, "zipContents");
            Assert.IsTrue(PackageBinaryZipping.Unzip(zipFilePath, destPath), "Unzip failed");
            Assert.IsFalse(Directory.Exists(destPath));
        }
    }
}
