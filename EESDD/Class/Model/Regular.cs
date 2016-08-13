using EESDD.Class.Control;
using EESDD.Module.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class Regular:User
    {
        public Regular() { }

        public Regular(
            string name,
            string password,
            string realName,
            string gender,
            float height,
            float weight,
            int age,
            int driAge,
            string career,
            string contact)
        {
            this.Name = name;
            this.Password = password;
            this.RealName = realName;
            this.gender = gender;
            this.height = height;
            this.weight = weight;
            this.age = age;
            this.driAge = driAge;
            this.career = career;
            this.contact = contact;
            this.expFile = ExpManager.GetFileName(name);
        }

        private string gender;
        private float height;
        private float weight;
        private int age;
        private int driAge;
        private string career;
        private string contact;
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

        public string ExpFile
        {
            get { return expFile; }
            set { expFile = value; }
        }
    }
}
