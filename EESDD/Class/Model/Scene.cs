using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    public class Scene
    {
        public Scene()
        {
            this.enable = true;
            this.vissimModule = false;
        }

        public Scene(string name, string title,
            string description, string picture,
            List<string> areaTitle, bool vissimModule)
        {
            this.name = name;
            this.title = title;
            this.description = description;
            this.picture = picture;
            this.areaTitle = areaTitle;
            this.vissimModule = vissimModule;
            this.enable = true;
        }

        private string name;
        private string title;
        private string description;
        private string picture;
        private List<string> areaTitle;
        private bool vissimModule;
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

        public List<string> AreaTitle
        {
            get { return areaTitle; }
            set { areaTitle = value; }
        }

        public bool VissimModule
        {
            get { return vissimModule; }
            set { vissimModule = value; }
        }

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
    }
}
