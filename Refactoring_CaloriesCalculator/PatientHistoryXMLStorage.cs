using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Refactoring_CaloriesCalculator
{
    public class PatientHistoryXMLStorage
    {
        private XmlDocument document;
        private Patient patient;

        public PatientHistoryXMLStorage()
        {
            this.document = document = new XmlDocument();
        }

        public static string PatientsHistoryFileLocation =>
            Assembly
                .GetEntryAssembly().Location
                .Replace("Refactoring_CaloriesCalculator.exe", "PatientsHistory.xml");

        private void AddNewPatient()
        {
            var thisPatient = document.DocumentElement.FirstChild.CloneNode(false);
            thisPatient.Attributes["ssn"].Value = patient.SSN;
            thisPatient.Attributes["firstName"].Value = patient.FirstName;
            thisPatient.Attributes["lastName"].Value = patient.LastName;

            var measurement = document.DocumentElement.FirstChild["measurement"].CloneNode(true);
            SetMeasurementValues(measurement);
            thisPatient.AppendChild(measurement);
            document.FirstChild.AppendChild(thisPatient);
        }

        private void SetMeasurementValues(XmlNode measurement)
        {
            measurement.Attributes["date"].Value = DateTime.Now.ToString();
            measurement["height"].FirstChild.Value = patient.HeightInInches.ToString();
            measurement["weight"].FirstChild.Value = patient.WeightInPounds.ToString();
            measurement["age"].FirstChild.Value = patient.Age.ToString();
            measurement["dailyCaloriesRecommended"].FirstChild.Value = patient.DailyCaloriesRecommended().ToString();
            measurement["idealBodyWeight"].FirstChild.Value = patient.IdealBodyWeight().ToString();
            measurement["distanceFromIdealWeight"].FirstChild.Value = patient.DistanceFromIdealWeight().ToString();
        }

        private XmlNode FindPatientNode()
        {
            XmlNode patientNode = null;

            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name == "ssn" && attrib.Value == patient.SSN)
                    {
                        patientNode = node;
                    }
                }
            }

            return patientNode;
        }

        private void CreatePatientsHistoryXmlFirstTime()
        {
            document.LoadXml($@"<?xml version=""1.0"" encoding=""utf-8"" ?> 
                <PatientsHistory>
                    <patient ssn=""{patient.SSN}"" firstName=""{patient.FirstName}"" lastName=""{patient.LastName}""> 
                        <measurement date=""{DateTime.Now}"">
                            <height>{patient.HeightInInches}</height>
                            <weight>{patient.WeightInPounds}</weight>
                            <age>{patient.Age}</age> 
                            <dailyCaloriesRecommended>{patient.DailyCaloriesRecommended()}</dailyCaloriesRecommended> 
                            <idealBodyWeight>{patient.IdealBodyWeight()}</idealBodyWeight> 
                            <distanceFromIdealWeight>{patient.DistanceFromIdealWeight()}</distanceFromIdealWeight> 
                        <!--Another measurement -->
                        </measurement> 
                    </patient>
                <!--Another patient --> 
                </PatientsHistory>
                ");
        }

        private void LoadPatientsHistoryFile()
        {
            document.Load(PatientsHistoryFileLocation);
        }

        public void Save(Patient patient)
        {
            this.patient = patient;
            bool fileCreated = true;

            try
            {
                LoadPatientsHistoryFile();
            }
            catch (FileNotFoundException)
            {
                fileCreated = false;
            }

            if (!fileCreated)
            {
                CreatePatientsHistoryXmlFirstTime();
            }
            else
            {
                var patientNode = FindPatientNode();

                if (patientNode == null)
                {
                    AddNewPatient();
                }
                else
                {
                    var measurement = patientNode.FirstChild.CloneNode(true);
                    SetMeasurementValues(measurement);
                    patientNode.AppendChild(measurement);
                }
            }

            document.Save(PatientsHistoryFileLocation);
        }
    }
}
