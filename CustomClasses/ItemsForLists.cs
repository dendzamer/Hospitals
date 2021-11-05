using System;
using System.Collections.Generic;

namespace CustomClasses
{
    public class ItemsForLists
    {
        public static string[] GetStates()
        {
            string[] states = new string[]{ "AL", "AK", "AZ", "AR", "CA",
             "CO", "CT", "DE", "DC", "FL", "GA", "HI",
              "ID", "IL", "IN", "IA", "KS", "KY", "LA",
               "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT",
                "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND",
                 "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN",
                  "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"};
            return states;
        }

        public static string[] GetDepartments()
        {
            string[] departments = new string[] {
                "", "---other---", "ER", "ICU", "Cath lab", "Psych", "Case management", "Endoscopy/GI", "Dialysis", "Oncology/Chemo", "Interventional radiology", "Medical Surgical/Telemetry", "OR", "L&D", "PACU", "Post partum/Mother and baby", "Stepdown", "Pediatrics"};

                return departments;
        }

        public static int[] GetRatings()
        {
            int[] ratings = new int[]{
                1, 2, 3, 4, 5
            };

            return ratings;
        }

        public static string[] GetNurseTypes()
        {
            string[] nurseTypes = new string[]{
                "Staff Nurse", "Travel Nurse"
            };

            return nurseTypes;
        }

        public static string[] GetSalaries()
        {
            string[] salaries = new string[] {
                "n/a", "$25-50", "$50-75", "$75-100", "$100+"
            };

            return salaries;
        }
    }
}