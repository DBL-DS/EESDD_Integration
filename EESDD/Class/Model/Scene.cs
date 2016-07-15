using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class Scene
    {
        public Scene()
        {
            
        }

        private string name;
        private string title;
        private string description;
        private string picture;
        private List<string> areaTitle;

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

        public List<string> AreaTitle
        {
            get { return areaTitle; }
            set { areaTitle = value; }
        }
    }
}
