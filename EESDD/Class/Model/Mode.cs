using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class Mode
    {
        public Mode()
        {
            this.enable = true;
        }

        public Mode(string name, string title,
            string description, string picture)
        {
            this.name = name;
            this.title = title;
            this.description = description;
            this.picture = picture;
            this.enable = true;
        }

        private string name;
        private string title;
        private string description;
        private string picture;
        private bool enable;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
    }
}
