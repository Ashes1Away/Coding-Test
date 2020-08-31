using FluentAssertions;
using Moq;
using NUnit.Framework;
using StudentRecords.Models;
using StudentRecords.WebApi.Storage;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace StudentRecords.Test
{
    public class MapperTests
    {

        private Mock<IFileSystem> _fileMock;
        private string _rawJsonReturn;
        private string _rawCourseReturn;
        private IReadOnly _readOnly;

        private Student _expectedStudent;
        private IEnumerable<Course> _expectedCourse;

        [SetUp]
        public void Setup()
        {
            SetupExpected();
            SetupReturns();
            _fileMock = new Mock<IFileSystem>();
            _fileMock.Setup(x => x.File.ReadAllText(It.IsRegex(".*students.*"))).Returns(_rawJsonReturn);
            _fileMock.Setup(x => x.File.ReadAllText(It.IsRegex(".*Course.*"))).Returns(_rawCourseReturn);
            _readOnly = new JsonAccessor(_fileMock.Object);
        }

        [Test]
        public void StudentGetsMappedCorrectly()
        {
            //Assign
            var expected = _expectedStudent;

            //Act
            var result = _readOnly.GetAllStudents();

            //Assert
            result.First().Should().BeEquivalentTo(expected);
            _fileMock.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CourseGetsMappedCorrectly()
        {
            //Assign
            var expected = _expectedCourse;

            //Act
            var result = _readOnly.GetAllCourses();

            //Assert
            result.Should().BeEquivalentTo(expected);
            _fileMock.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
        }

        private void SetupExpected()
        {
            _expectedStudent = new Student
            {
                StudentId = 77777701,
                FirstName = "Test",
                LastName = "Test",
                KnownAs = "Test",
                DisplayName = "Test 1 Test",
                DateOfBirth = DateTime.Parse("2000-04-01T00:00:00"),
                Gender = "F",
                UniversityEmail = "Test.Test19@mail.bcu.ac.uk",
                NetworkId = "S77777701",
                HomeOrOverseas = "H",
                CourseEnrolment = new List<Enrolment>
                {
                    new Enrolment
                    {
                        EnrolmentId = "77777701/7",
                        AcademicYear = "2020/1",
                        YearOfStudy = 0,
                        Occurrence = "JAN2S",
                        ModeOfAttendance = "FULL TIME",
                        EnrolmentStatus = "E",
                        CourseEntryDate = DateTime.Parse("2020-02-05T00:00:00"),
                        ExpectedEndDate = DateTime.Parse("2021-09-06T00:00:00"),
                        Course = new Course
                        {
                            CourseCode = "PT0129Z",
                            CourseName = "MA Interior Design (extended) (BCUIC)"
                        }
                    }
                }
            };

            _expectedCourse = new List<Course>
            {
                new Course
                {
                     CourseCode = "CP0007",
                    CourseName = "Certificate of Attendance in Subject Knowledge Enhancement in Mathematics"
                },
                new Course
                {
                    CourseCode = "CP0012N",
                    CourseName = "Medical Ultrasound Modules (PT)"
                }
            };
        }

        private void SetupReturns()
        {
            _rawJsonReturn = @"[
            {
                ""StudentId"": ""77777701"",
                ""FirstName"": ""Test"",
                ""LastName"": ""Test"",
                ""KnownAs"": ""Test"",
                ""DisplayName"": ""Test 1 Test"",
                ""DateOfBirth"": ""2000-04-01T00:00:00"",
                ""Gender"": ""F"",
                ""UniversityEmail"": ""Test.Test19@mail.bcu.ac.uk"",
                ""NetworkId"": ""S77777701"",
                ""HomeOrOverseas"": ""H"",
                ""CourseEnrolment"": [
                {
                    ""EnrolmentId"": ""77777701/7"",
                    ""AcademicYear"": ""2020/1"",
                    ""YearOfStudy"": ""0"",
                    ""Occurrence"": ""JAN2S"",
                    ""ModeOfAttendance"": ""FULL TIME"",
                    ""EnrolmentStatus"": ""E"",
                    ""CourseEntryDate"": ""2020-02-05T00:00:00"",
                    ""ExpectedEndDate"": ""2021-09-06T00:00:00"",
                    ""Course"":
                    {
                        ""CourseCode"": ""PT0129Z"",
                        ""CourseName"": ""MA Interior Design (extended) (BCUIC)""
                    }
                }]
            }]";

            _rawCourseReturn = @"[
             {
                ""CourseCode"": ""CP0007"",
                ""CourseName"": ""Certificate of Attendance in Subject Knowledge Enhancement in Mathematics""
              },
              {
                ""CourseCode"": ""CP0012N"",
                ""CourseName"": ""Medical Ultrasound Modules (PT)""
              }]";
        }
    }
}
