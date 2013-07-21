using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUploadApi.model;
using DataUploadApi.repository;
using NUnit.Framework;

namespace DataUploadApiTests
{
    [TestFixture]
    public class PECBuilderTests
    {

        [TestCase]
        public void can_set_test_header_fields()
        {
            PECBuilder builder = new PECBuilder();
            builder.addHeaderField("Request Year:", "2013");
            builder.addHeaderField("Test:", "281");

            PECTest test = builder.build();

            Assert.That(test.RequestYear, Is.EqualTo(2013));
            Assert.That(test.Test, Is.EqualTo(281));

        }

    }
}
