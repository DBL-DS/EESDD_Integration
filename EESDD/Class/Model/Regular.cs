using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class Regular:User
    {
        private string gender;
        private float height;
        private float weight;
        private int age;
        private int driAge;
        private string career;
        private string contact;
        private ExpCluster expCluster;
        private string expFile;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public int DriAge
        {
            get { return driAge; }
            set { driAge = value; }
        }

        public string Career
        {
            get { return career; }
            set { career = value; }
        }

        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        public ExpCluster ExpCluster
        {
            get { return expCluster; }
            set { expCluster = value; }
        }

        public string ExpFile
        {
            get { return expFile; }
            set { expFile = value; }
        }
    }
}
